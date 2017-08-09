using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Diagnostics;

namespace Gecko.Windows
{
	internal sealed class ModalChildWindowListener : nsIWindowMediatorListener
	{
		private ComObject<nsIXULWindow> _childWindow;
		private WebView _view;

		public ModalChildWindowListener(WebView view, nsIXULWindow childWindow)
		{
			_view = view;
			_childWindow = Xpcom.QueryInterface2<nsIXULWindow>(childWindow);
		}

		public void Init()
		{
			IntPtr hwnd = _childWindow.Instance.GetHandle();
			IntPtr ownerHwnd = NativeMethods.GetAncestor(_view.Handle, WinApi.GA_ROOT);

			if (ownerHwnd != IntPtr.Zero)
			{
				if (IntPtr.Size == 4)
				{
					NativeMethods.SetWindowLong(hwnd, WinApi.GWL_HWNDPARENT, (int)ownerHwnd);
				}
				else
				{
					NativeMethods.SetWindowLongPtr(hwnd, WinApi.GWL_HWNDPARENT, ownerHwnd);
				}

				using (var windowMediator = Xpcom.GetService2<nsIWindowMediator>(Contracts.WindowMediator))
				{
					windowMediator.Instance.AddListener(this);
				}
				_view.Enabled = false;
				NativeMethods.EnableWindow(ownerHwnd, false);
			}
		}

		void nsIWindowMediatorListener.OnWindowTitleChange(nsIXULWindow window, string newTitle)
		{

		}

		void nsIWindowMediatorListener.OnOpenWindow(nsIXULWindow window)
		{

		}

		void nsIWindowMediatorListener.OnCloseWindow(nsIXULWindow window)
		{
			if (window == _childWindow.Instance)
			{
				IntPtr ownerHwnd = NativeMethods.GetAncestor(_view.Handle, WinApi.GA_ROOT);
				if (ownerHwnd != IntPtr.Zero)
				{
					NativeMethods.EnableWindow(ownerHwnd, true);
					_view.Enabled = true;
					_view.Focus();
				}

				using (var windowMediator = Xpcom.GetService2<nsIWindowMediator>(Contracts.WindowMediator))
				{
					windowMediator.Instance.RemoveListener(this);
					
				}
				((IDisposable)_childWindow).Dispose();
			}
		}
	}
}
