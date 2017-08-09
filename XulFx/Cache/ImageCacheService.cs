using System;
using Gecko.Interop;
using Gecko.Interfaces;

namespace Gecko.Cache
{
	public sealed class ImageCacheService : ComObject<imgICache>, IGeckoObjectWrapper
	{
		public static ImageCacheService GetService()
		{
			return Xpcom.GetService<imgICache>(Contracts.ImageCache).Wrap(ImageCacheService.Create);
		}

		private static ImageCacheService Create(imgICache instance)
		{
			return new ImageCacheService(instance);
		}

		private ImageCacheService(imgICache instance)
			: base(instance)
		{

		}


		public void ClearCache(bool chrome)
		{
			Instance.ClearCache(chrome);
		}

	}
}
