using Gecko.Interop;
using System;
using System.Runtime.InteropServices;

namespace Gecko.IO
{
	public abstract class GeckoStream<TInterface> : System.IO.Stream, IDisposable, IEquatable<GeckoStream<TInterface>>, IEquatable<TInterface>, IComObject, IGeckoObjectWrapper
		where TInterface : class
	{

		internal TInterface _instance;
		private GeckoObjectCache.CacheKey _cacheKey;

		#region ctor & dtor

		public GeckoStream(TInterface instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			_instance = instance;

			_cacheKey = GeckoObjectCache.Set(instance, this);
		}

		~GeckoStream()
		{
			Dispose(false);
		}

		public new void Dispose()
		{
			Dispose(true);
		}

		protected override void Dispose(bool disposing)
		{
			if (_instance != null)
			{
				GeckoObjectCache.Remove(_cacheKey);

				Xpcom.FreeComObject(ref _instance);
				if (disposing)
				{
					GC.SuppressFinalize(this);
				}
			}
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
			return typeof(GeckoStream<TInterface>);
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
		
		#region Equality

		public bool Equals(GeckoStream<TInterface> other)
		{
			if (ReferenceEquals(this, other)) return true;
			if (ReferenceEquals(null, other)) return false;
			return Instance.GetHashCode() == other.Instance.GetHashCode();
		}

		public bool Equals(TInterface other)
		{
			if (ReferenceEquals(null, other)) return false;
			return _instance.GetHashCode() == other.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj)) return true;
			if (ReferenceEquals(null, obj)) return false;
			if (!(obj is GeckoStream<TInterface>)) return false;
			return Instance.GetHashCode() == ((GeckoStream<TInterface>)obj).Instance.GetHashCode();
		}

		public override int GetHashCode()
		{
			return _instance.GetHashCode();
		}

		#endregion


	}
}
