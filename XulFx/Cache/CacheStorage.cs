using Gecko.Cache;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;

namespace Gecko.Cache
{
	public sealed class CacheStorage : ComObject<nsICacheStorage>, IGeckoObjectWrapper
	{
		public static CacheStorage Create(nsICacheStorage instance)
		{
			return new CacheStorage(instance);
		}

		private CacheStorage(nsICacheStorage instance)
			: base(instance)
		{

		}



	}
}
