using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.WebAPI
{
	public sealed class GeckoPerformanceNavigation: ComObject<nsIXulfxPerformanceNavigation>, IGeckoObjectWrapper
	{
		public static GeckoPerformanceNavigation Create(nsIXulfxPerformanceNavigation instance)
		{
			return new GeckoPerformanceNavigation(instance);
		}

		private GeckoPerformanceNavigation(nsIXulfxPerformanceNavigation instance)
			: base(instance)
		{

		}

		public PerformanceNavigationType Type
		{
			get { return (PerformanceNavigationType)Instance.GetTypeAttribute(); }
		}

		public int RedirectCount
		{
			get { return Instance.GetRedirectCountAttribute(); }
		}

	}
}
