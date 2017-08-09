using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko
{
	public sealed class GeckoTreeColumn: ComObject<nsITreeColumn>, IGeckoObjectWrapper
	{
		public static GeckoTreeColumn Create(nsITreeColumn instance)
		{
			return new GeckoTreeColumn(instance);
		}

		private GeckoTreeColumn(nsITreeColumn instance)
			: base(instance)
		{

		}

	}
}
