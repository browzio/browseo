using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;

namespace Gecko.Cache
{
	public abstract class CacheStorageConsumptionObserverBase : nsICacheStorageConsumptionObserver, nsISupportsWeakReference
	{
		private GeckoWeakReference _weakRef;

		/// <summary>
		/// Callback invoked to answer asyncGetDiskConsumption call. Always triggered on the main thread.
		/// </summary>
		/// <param name="aDiskSize">
		/// The disk consumption in bytes.
		/// </param>
		public abstract void OnNetworkCacheDiskConsumption(long aDiskSize);

		public GeckoWeakReference WeakReference
		{
			get { return _weakRef ?? (_weakRef = new GeckoWeakReference(this)); }
			protected set { _weakRef = value; }
		}

		nsIWeakReference nsISupportsWeakReference.GetWeakReference()
		{
			return this.WeakReference;
		}

	}
}
