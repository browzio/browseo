using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko
{
	public sealed class GeckoTreeBoxObject : ComObject<nsITreeBoxObject>, IGeckoObjectWrapper
	{
		public static GeckoTreeBoxObject Create(nsITreeBoxObject instance)
		{
			return new GeckoTreeBoxObject(instance);
		}

		private GeckoTreeBoxObject(nsITreeBoxObject instance)
			: base(instance)
		{
			
		}



	}
}
