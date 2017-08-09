using System;
using System.Runtime.InteropServices;

namespace Gecko.Interop
{
	internal interface IComObject
	{
		object NativeInstance { get; }
		Type GetComObjectType();
	}
	
	public class ComObject<TInterface> : IDisposable, IEquatable<ComObject<TInterface>>, IEquatable<TInterface>, IComObject
		where TInterface : class
	{
		
		internal TInterface _instance;
		private GeckoObjectCache.CacheKey _cacheKey;

		#region ctor & dtor

		public ComObject(TInterface instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			_instance = instance;


			IGeckoObjectWrapper wrapper = this as IGeckoObjectWrapper;
			if(wrapper != null)
				_cacheKey = GeckoObjectCache.Set(instance, wrapper);
		}

		~ComObject()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(this is IGeckoObjectWrapper)
				GeckoObjectCache.Remove(_cacheKey);

			Xpcom.FreeComObject(ref _instance);
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		void IDisposable.Dispose()
		{
			if (_instance != null)
			{
				Dispose(true);
			}
		}

		#endregion

		public TInterface Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _instance;
			}
		}

		protected IntPtr Unknown
		{
			get
			{
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);

				if (_cacheKey.IUnknown != IntPtr.Zero)
					return _cacheKey.IUnknown;

				if (this is IGeckoObjectWrapper)
					throw new InvalidOperationException(); // the key must be created in the constructor

				_cacheKey = GeckoObjectCache.GetKey(_instance, this.GetType());
				return _cacheKey.IUnknown;
			}
		}

		public T QueryInterface<T>()
			where T: class
		{
			return Xpcom.QueryInterface<T>(this.Instance);
		}

		object IComObject.NativeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		Type IComObject.GetComObjectType()
		{
			return typeof(ComObject<TInterface>);
		}

		#region Equality

		public bool Equals(ComObject<TInterface> other)
		{
			if (ReferenceEquals(this, other)) return true;
			if (ReferenceEquals(null, other)) return false;
			return this.Unknown == other.Unknown;
		}

		public bool Equals(TInterface other)
		{
			if (ReferenceEquals(null, other)) return false;
			IntPtr pUnk = Marshal.GetIUnknownForObject(other);
			Marshal.Release(pUnk);
			return this.Unknown == pUnk;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			if (ReferenceEquals(null, obj)) return false;
			if (!(obj is ComObject<TInterface>)) return false;
			return this.Unknown == ((ComObject<TInterface>)obj).Unknown;
		}

		public override int GetHashCode()
		{
			return this.Unknown.GetHashCode();
		}


		#endregion


	}
}
