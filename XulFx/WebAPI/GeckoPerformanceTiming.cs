using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.WebAPI
{
	/// <summary>
	/// The PerformanceTiming
	/// </summary>
	public sealed class GeckoPerformanceTiming : ComObject<nsIXulfxPerformanceTiming>, IGeckoObjectWrapper
	{
		public static GeckoPerformanceTiming Create(nsIXulfxPerformanceTiming instance)
		{
			return new GeckoPerformanceTiming(instance);
		}

		private GeckoPerformanceTiming(nsIXulfxPerformanceTiming instance)
			: base(instance)
		{

		}


		public DateTime NavigationStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetNavigationStartAttribute()); }
		}


		public DateTime UnloadEventStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetUnloadEventStartAttribute()); }
		}


		public DateTime UnloadEventEnd
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetUnloadEventEndAttribute()); }
		}


		public DateTime RedirectStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetRedirectStartAttribute()); }
		}


		public DateTime RedirectEnd
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetRedirectEndAttribute()); }
		}


		public DateTime FetchStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetFetchStartAttribute()); }
		}


		public DateTime DomainLookupStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetDomainLookupStartAttribute()); }
		}


		public DateTime DomainLookupEnd
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetDomainLookupEndAttribute()); }
		}


		public DateTime ConnectStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetConnectStartAttribute()); }
		}


		public DateTime ConnectEnd
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetConnectEndAttribute()); }
		}


		public DateTime SecureConnectionStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetSecureConnectionStartAttribute()); }
		}


		public DateTime RequestStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetRequestStartAttribute()); }
		}


		public DateTime ResponseStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetResponseStartAttribute()); }
		}


		public DateTime ResponseEnd
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetResponseEndAttribute()); }
		}


		public DateTime DomLoading
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetDomLoadingAttribute()); }
		}


		public DateTime DomInteractive
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetDomInteractiveAttribute()); }
		}


		public DateTime DomContentLoadedEventStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetDomContentLoadedEventStartAttribute()); }
		}


		public DateTime DomContentLoadedEventEnd
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetDomContentLoadedEventEndAttribute()); }
		}


		public DateTime DomComplete
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetDomCompleteAttribute()); }
		}


		public DateTime LoadEventStart
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetLoadEventStartAttribute()); }
		}


		public DateTime LoadEventEnd
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetLoadEventEndAttribute()); }
		}

	}
}
