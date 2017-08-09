using System;
using Gecko.Interop;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;

namespace Gecko
{
	internal static class GeckoObjectCache
	{
		internal struct CacheKey
		{
			private IntPtr _iUnknown;
			private Type _comObjectType;

			public CacheKey(IntPtr iUnknown, Type comObjectType)
			{
				_iUnknown = iUnknown;
				_comObjectType = comObjectType;
			}

			public IntPtr IUnknown
			{
				get { return _iUnknown; }
			}

			public override int GetHashCode()
			{
				return _iUnknown.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				if (obj is CacheKey)
				{
					CacheKey key = (CacheKey)obj;
					return _iUnknown == key._iUnknown && (_comObjectType.IsAssignableFrom(key._comObjectType) || key._comObjectType.IsAssignableFrom(_comObjectType));
				}
				return false;
			}
		}
	
		private static readonly Dictionary<CacheKey, WeakReference> _cache = new Dictionary<CacheKey, WeakReference>();
		private static readonly ReaderWriterLockSlim syncRoot = new ReaderWriterLockSlim();

		internal static CacheKey GetKey(object comObject, Type typeOfWrapper)
		{
			IntPtr pUnk = Marshal.GetIUnknownForObject(comObject);
			Marshal.Release(pUnk);
			return new CacheKey(pUnk, typeOfWrapper);
		}

		public static object Get<TInterface, TWrapper>(TInterface comObject)
			where TInterface : class
			where TWrapper : IGeckoObjectWrapper
		{
			WeakReference weakRef;

			CacheKey key = GetKey(comObject, typeof(TWrapper));

			syncRoot.EnterReadLock();
			try
			{
				if (!_cache.TryGetValue(key, out weakRef))
					return null;
			}
			finally
			{
				syncRoot.ExitReadLock();
			}

			return weakRef.Target;
		}

		public static CacheKey Set<TInterface>(TInterface comObject, IGeckoObjectWrapper wrapper)
			where TInterface : class
		{
			WeakReference weakRef = new WeakReference(wrapper);

			CacheKey key = GetKey(comObject, ((IComObject)wrapper).GetComObjectType());

			syncRoot.EnterWriteLock();
			try
			{
				_cache[key] = weakRef;
			}
			finally
			{
				syncRoot.ExitWriteLock();
			}
			return key;
		}

		public static void Remove(CacheKey key)
		{
			syncRoot.EnterWriteLock();
			try
			{
				_cache.Remove(key);
			}
			finally
			{
				syncRoot.ExitWriteLock();
			}

		}
	}
}
