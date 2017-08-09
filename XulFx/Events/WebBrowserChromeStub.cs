using Gecko.DOM;
using Gecko.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	internal sealed class WebBrowserChromeStub : nsIWebBrowserChrome, nsIInterfaceRequestor
	{
		private GeckoWindow _window;

		public WebBrowserChromeStub(GeckoWindow window)
		{
			_window = window;
		}

		public IntPtr GetInterface(ref Guid uuid)
		{
			if (_window != null)
			{
				if (uuid == typeof(nsIDOMWindow).GUID || uuid == typeof(nsPIDOMWindowOuter).GUID)
				{
					IntPtr pType = Xpcom.QueryInterfaceForObject(_window.Instance, uuid);
					if (pType != IntPtr.Zero)
						return pType;
				}
			}
			throw new InvalidCastException();
		}


		public void SetStatus(uint statusType, string status)
		{

		}

		public nsIWebBrowser GetWebBrowserAttribute()
		{
			throw new NotImplementedException();
		}

		public void SetWebBrowserAttribute(nsIWebBrowser aWebBrowser)
		{
			throw new NotImplementedException();
		}

		public uint GetChromeFlagsAttribute()
		{
			return (uint)nsIWebBrowserChromeConsts.CHROME_ALL;
		}

		public void SetChromeFlagsAttribute(uint aChromeFlags)
		{
			throw new NotImplementedException();
		}

		public void DestroyBrowserWindow()
		{
			throw new NotImplementedException();
		}

		public void SizeBrowserTo(int aCX, int aCY)
		{
			throw new NotImplementedException();
		}

		public void ShowAsModal()
		{
			nsIWebBrowserChrome chrome = GetChrome();
			if (chrome != null)
			{
				try
				{
					chrome.ShowAsModal();
				}
				finally
				{
					Xpcom.FreeComObject(ref chrome);
				}
			}
		}

		public bool IsWindowModal()
		{
			nsIWebBrowserChrome chrome = GetChrome();
			if (chrome != null)
			{
				try
				{
					return chrome.IsWindowModal();
				}
				finally
				{
					Xpcom.FreeComObject(ref chrome);
				}
			}
			return false;
		}

		public void ExitModalEventLoop(int aStatus)
		{
			nsIWebBrowserChrome chrome = GetChrome();
			if (chrome != null)
			{
				try
				{
					chrome.ExitModalEventLoop(aStatus);
				}
				finally
				{
					Xpcom.FreeComObject(ref chrome);
				}
			}
		}


		private nsIWebBrowserChrome GetChrome()
		{
			if (_window != null)
			{
				nsIDocShellTreeOwner treeOwner = null;
				nsIDocShell docShell = _window.QueryInterface<nsIDocShell>();
				try
				{
					treeOwner = docShell.GetTreeOwnerAttribute();
					if (treeOwner != null)
					{
						return Xpcom.QueryInterface<nsIWebBrowserChrome>(treeOwner);
					}
				}
				finally
				{
					Xpcom.FreeComObject(ref treeOwner);
					Xpcom.FreeComObject(ref docShell);
				}
			}
			return null;
		}

	}
}
