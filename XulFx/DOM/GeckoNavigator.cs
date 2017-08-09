using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoNavigator : ComObject<nsIDOMNavigator>, IGeckoObjectWrapper
	{
		public static GeckoNavigator Create(nsIDOMNavigator instance)
		{
			return new GeckoNavigator(instance);
		}

		private GeckoNavigator(nsIDOMNavigator instance)
			: base(instance)
		{

		}


		public string AppCodeName
		{
			get { return nsString.Get(Instance.GetAppCodeNameAttribute); }
		}

		public string AppName
		{
			get { return nsString.Get(Instance.GetAppNameAttribute); }
		}

		public string AppVersion
		{
			get { return nsString.Get(Instance.GetAppVersionAttribute); }
		}

		public string BuildID
		{
			get { return nsString.Get(Instance.GetBuildIDAttribute); }
		}

		public string DoNotTrack
		{
			get { return nsString.Get(Instance.GetDoNotTrackAttribute); }
		}

		public string Language
		{
			get { return nsString.Get(Instance.GetLanguageAttribute); }
		}

		//public object MimeTypes
		//{
		//	get { return Instance.GetMimeTypesAttribute(); }
		//}

		public string Oscpu
		{
			get { return nsString.Get(Instance.GetOscpuAttribute); }
		}

		public string Platform
		{
			get { return nsString.Get(Instance.GetPlatformAttribute); }
		}

		//Instance.GetPluginsAttribute(  );

		public string Product
		{
			get { return nsString.Get(Instance.GetProductAttribute); }
		}

		public string ProductSub
		{
			get { return nsString.Get(Instance.GetProductSubAttribute); }
		}

		public string UserAgent
		{
			get { return nsString.Get(Instance.GetUserAgentAttribute); }
		}

		public string Vendor
		{
			get { return nsString.Get(Instance.GetVendorAttribute); }
		}

		public string GetVendorSub
		{
			get { return nsString.Get(Instance.GetVendorSubAttribute); }
		}

	}
}
