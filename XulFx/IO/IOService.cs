using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko
{

	public sealed class IOService : ComObject<nsIIOService2>, IGeckoObjectWrapper
	{
		public static IOService GetService()
		{
			return Xpcom.GetService<nsIIOService2>(Contracts.NetworkIOService).Wrap(IOService.Create);
		}

		private static IOService Create(nsIIOService2 instance)
		{
			return new IOService(instance);
		}

		private IOService(nsIIOService2 instance)
			: base(instance)
		{

		}


		public bool Offline
		{
			get
			{
				return Instance.GetOfflineAttribute();
			}
			set
			{
				Instance.SetOfflineAttribute(value);
			}
		}

		public nsIURI CreateNsIUri(string url)
		{
			using (var str = new nsAUTF8String(url))
			{
				return Instance.NewURI(str, null, null);
			}
		}

		internal nsIURL CreateNsIUrl(string url)
		{
			nsIURI uri = CreateNsIUri(url);
			try
			{
				return Xpcom.QueryInterface<nsIURL>(uri);
			}
			finally
			{
				Xpcom.FreeComObject(ref uri);
			}
		}

		public nsIChannel NewChannelFromUri(nsIURI uri)
		{
			return Instance.NewChannelFromURI(uri);
		}

		public nsIChannel NewChannelFromUriWithProxyFlags(nsIURI uri, nsIURI proxyUri, uint proxyFlags)
		{
			return Instance.NewChannelFromURIWithProxyFlags(uri, proxyUri, proxyFlags);
		}


		public bool ManageOfflineStatus
		{
			get
			{
				return Instance.GetManageOfflineStatusAttribute();
			}
			set
			{
				Instance.SetManageOfflineStatusAttribute(value);
			}
		}

	}

}
