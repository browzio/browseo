using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	public sealed class XPConnectService : ComObject<nsIXPConnect>, IGeckoObjectWrapper
	{

		public static XPConnectService GetService()
		{
			return Xpcom.GetService<nsIXPConnect>(Contracts.XPConnect).Wrap(XPConnectService.Create);
		}

		private static XPConnectService Create(nsIXPConnect instance)
		{
			return new XPConnectService(instance);
		}

		private XPConnectService(nsIXPConnect instance)
			: base(instance)
		{

		}

		

	}
}
