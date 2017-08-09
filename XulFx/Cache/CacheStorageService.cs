using Gecko.Cache;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;

namespace Gecko
{
	public sealed class CacheStorageService : ComObject<nsICacheStorageService>, IGeckoObjectWrapper
	{
		public static CacheStorageService GetService()
		{
			return Xpcom.GetService<nsICacheStorageService>(Contracts.CacheStorageService).Wrap(CacheStorageService.Create);
		}

		private static CacheStorageService Create(nsICacheStorageService instance)
		{
			return new CacheStorageService(instance);
		}

		private CacheStorageService(nsICacheStorageService instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Get storage where entries will be written to disk when not forbidden by
		/// response headers.
		/// </summary>
		/// <param name="lookupAppCache">
		/// When set true (for top level document loading channels) app cache will
		/// be first to check on to find entries in.
		/// </param>
		public CacheStorage GetDiskCacheStorage(nsILoadContextInfo loadContextInfo, bool lookupAppCache)
		{
			return Instance.DiskCacheStorage(loadContextInfo, lookupAppCache).Wrap(CacheStorage.Create);
		}

		/// <summary>
		/// Get storage where entries will only remain in memory, never written
		/// to the disk.
		/// 
		/// NOTE: Any existing disk entry for [URL|id-extension] will be doomed
		/// prior opening an entry using this memory-only storage.  Result of
		/// AsyncOpenURI will be a new and empty memory-only entry.  Using
		/// OPEN_READONLY open flag has no effect on this behavior.
		/// </summary>
		/// <param name="loadContextInfo">
		/// Information about the loading context, this focuses the storage JAR and
		/// respects separate storage for private browsing.
		/// </param>
		public CacheStorage GetMemoryCacheStorage(nsILoadContextInfo loadContextInfo)
		{
			return Instance.MemoryCacheStorage(loadContextInfo).Wrap(CacheStorage.Create);
		}

		/// <summary>
		/// Get storage for a specified application cache obtained using some different
		/// mechanism.
		/// </summary>
		/// <param name="loadContextInfo">
		/// Mandatory reference to a load context information.
		/// </param>
		/// <param name="applicationCache">
		/// Optional reference to an existing appcache.  When left null, this will
		/// work with offline cache as a whole.
		/// </param>
		public CacheStorage GetAppCacheStorage(nsILoadContextInfo loadContextInfo, nsIApplicationCache applicationCache)
		{
			return Instance.AppCacheStorage(loadContextInfo, applicationCache).Wrap(CacheStorage.Create);
		}

		/// <summary>
		/// Purges data we keep warmed in memory. Use for tests and for saving memory.
		/// </summary>
		public void PurgeFromMemory(uint what)
		{
			Instance.PurgeFromMemory(what);
		}

		/// <summary>
		/// Evict the whole cache.
		/// </summary>
		public void Clear()
		{
			Instance.Clear();
		}

		/// <summary>
		/// Asynchronously determine how many bytes of the disk space the cache takes.
		/// </summary>
		/// <param name="aObserver">
		/// A mandatory observer.
		/// </param>
		public void AsyncGetDiskConsumption(CacheStorageConsumptionObserverBase observer)
		{
			if (observer == null)
				throw new ArgumentNullException("observer");

			Instance.AsyncGetDiskConsumption(observer);
		}
	}

}
