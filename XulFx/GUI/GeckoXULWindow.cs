using System;
using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.IO;
using System.Runtime.InteropServices;

namespace Gecko.GUI
{
	public class GeckoXULWindow : ComObject<nsIXULWindow>, nsIWebProgressListener, nsISupportsWeakReference
	{
		private ComObject<nsIBaseWindow> _baseWindow;
		private ComObject<nsIDocShell> _docshell;
		private ComObject<nsIWebProgress> _progress;
		private bool _isChromeLoaded;
		private GeckoWeakReference _weakRef;

		private static nsIXULWindow CreateTopLevelWindow()
		{
			nsIURI uri = null;
			nsIXULWindow window = null;
			nsIAppShellService appShellSvc = null;
			try
			{
				uri = IOService.GetService().CreateNsIUri("chrome://xulfx/content/window.xul");
				appShellSvc = Xpcom.GetService<nsIAppShellService>(Contracts.AppShellService);
				
				if (appShellSvc != null)
				{
					window = appShellSvc.CreateTopLevelWindow(null, uri, (uint)nsIWebBrowserChromeConsts.CHROME_ALL, nsIAppShellServiceConsts.SIZE_TO_CONTENT, nsIAppShellServiceConsts.SIZE_TO_CONTENT, null, null);
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref uri);
				Xpcom.FreeComObject(ref appShellSvc);
			}
			
			if (window != null)
				return window;

			throw Marshal.GetExceptionForHR(GeckoError.NS_ERROR_FAILURE);
		}

		public GeckoXULWindow()
			: base(CreateTopLevelWindow())
		{
			_baseWindow = Xpcom.QueryInterface2<nsIBaseWindow>(Instance);

			_docshell = Instance.GetDocShellAttribute().AsComObject();
			_progress = _docshell.QueryInterface<nsIWebProgress>().AsComObject();
			if (_progress != null)
			{
				_progress.Instance.AddProgressListener(this, (uint)(nsIWebProgressConsts.NOTIFY_ALL) | (uint)(nsIWebProgressConsts.NOTIFY_STATE_ALL));
			}
			// TODO: wait until the chrome has loaded
			//Xpcom.DoEvents(); throws InvalidOperationException on WPF
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && _progress != null)
			{
				Close();
				try
				{
					_progress.Instance.RemoveProgressListener(this);
				}
				catch (COMException e)
				{
					if (e.ErrorCode != GeckoError.NS_ERROR_FAILURE)
						throw;
				}
				finally
				{
					((IDisposable)_progress).Dispose();
					_progress = null;
				}
			}
			if (_baseWindow != null)
			{
				((IDisposable)_baseWindow).Dispose();
				_baseWindow = null;
			}
			if(_docshell != null)
			{
				((IDisposable)_docshell).Dispose();
				_docshell = null;
			}
			base.Dispose(disposing);
		}

		public void Dispose()
		{
			((IDisposable)this).Dispose();
		}

		private ComObject<nsIBaseWindow> BaseWindow
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_baseWindow == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _baseWindow;
			}
		}

		public string NativeHandle
		{
			get
			{
				if (_baseWindow == null) return null;
				return nsString.Get(_baseWindow.Instance.GetNativeHandleAttribute);
			}
		}

		public int Left
		{
			get
			{
				int x, y;
				BaseWindow.Instance.GetPosition(out x, out y);
				return x;
			}
			set
			{
				int x, y;
				BaseWindow.Instance.GetPosition(out x, out y);
				BaseWindow.Instance.SetPosition(value, y);
			}
		}

		public int Top
		{
			get
			{
				int x, y;
				BaseWindow.Instance.GetPosition(out x, out y);
				return y;
			}
			set
			{
				int x, y;
				BaseWindow.Instance.GetPosition(out x, out y);
				BaseWindow.Instance.SetPosition(x, value);
			}
		}

		public int Width
		{
			get
			{
				int w, h;
				BaseWindow.Instance.GetSize(out w, out h);
				return w;
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("width");

				int w, h;
				BaseWindow.Instance.GetSize(out w, out h);
				BaseWindow.Instance.SetSize(value, h, true);
			}
		}

		public int Height
		{
			get
			{
				int w, h;
				BaseWindow.Instance.GetSize(out w, out h);
				return h;
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("height");

				int w, h;
				BaseWindow.Instance.GetSize(out w, out h);
				BaseWindow.Instance.SetSize(w, value, true);
			}
		}

		public bool Visible
		{
			get
			{
				return BaseWindow.Instance.GetVisibilityAttribute();
			}
			set
			{
				BaseWindow.Instance.SetVisibilityAttribute(value);
			}
		}

		public GeckoWindow View
		{
			get
			{
				return Xpcom.QueryInterface<nsIDOMWindow>(Instance).Wrap(GeckoWindow.Create);
			}
		}

		public void WaitUntilChromeLoad()
		{
			Xpcom.ModalEventLoop(() => !_isChromeLoaded);
		}

		public void SetPosition(int x, int y)
		{
			BaseWindow.Instance.SetPosition(x, y);
		}

		public void SetSize(int width, int height, bool invalidate)
		{
			if (width < 0)
				throw new ArgumentOutOfRangeException("width");
			if (height < 0)
				throw new ArgumentOutOfRangeException("height");
			BaseWindow.Instance.SetSize(width, height, invalidate);
		}

		public void SetPositionAndSize(int x, int y, int width, int height, bool invalidate, bool delayResize)
		{
			if (width < 0)
				throw new ArgumentOutOfRangeException("width");
			if (height < 0)
				throw new ArgumentOutOfRangeException("height");

			uint flags = 0;
			if (invalidate) flags = flags | nsIBaseWindowConsts.eRepaint;
			if (delayResize) flags = flags | nsIBaseWindowConsts.eDelayResize;
			BaseWindow.Instance.SetPositionAndSize(x, y, width, height, flags);
		}

		public void Show()
		{
			this.Visible = true;
		}

		public void ShowDialog()
		{
			this.Visible = true;
			Instance.ShowModal();
		}

		public void Hide()
		{
			this.Visible = false;
		}

		public void Close()
		{
			if (_docshell != null)
			{
				GeckoWindow window = _docshell.QueryInterface<nsIDOMWindow>().Wrap(GeckoWindow.Create);
				if (window != null && !window.Closed)
					window.Close();
			}
		}

		#region nsIWebProgressListener

		private bool IsThisDomWindow(nsIWebProgress aWebProgress)
		{
			bool result = false;
			if (aWebProgress.GetIsTopLevelAttribute())
			{
				mozIDOMWindowProxy thisWindow = Xpcom.QueryInterface<mozIDOMWindowProxy>(Instance);
				try
				{
					mozIDOMWindowProxy otherWindow = null;
					try
					{
						otherWindow = aWebProgress.GetDOMWindowAttribute();
						result = thisWindow == otherWindow;
					}
					finally
					{
						Xpcom.FreeComObject(ref otherWindow);
					}
				}
				finally
				{
					Xpcom.FreeComObject(ref thisWindow);
				}
			}
			return result;
		}

		void nsIWebProgressListener.OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus)
		{
			// if the notification is not about a document finishing, then just ignore it...
			if ((aStateFlags & nsIWebProgressListenerConsts.STATE_STOP) == 0 ||
				(aStateFlags & nsIWebProgressListenerConsts.STATE_IS_NETWORK) == 0)
			{
				return;
			}

			// if this notification is not for this window then ignore it...
			if (aWebProgress != null && IsThisDomWindow(aWebProgress))
			{
				_isChromeLoaded = true;
			}
		}

		void nsIWebProgressListener.OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress)
		{
		
		}

		void nsIWebProgressListener.OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint aFlags)
		{
			if (aWebProgress != null && IsThisDomWindow(aWebProgress))
				_isChromeLoaded = false;
		}

		void nsIWebProgressListener.OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage)
		{
		
		}

		void nsIWebProgressListener.OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState)
		{
		
		}

		#endregion

		#region nsISupportsWeakReference
		
		public nsIWeakReference GetWeakReference()
		{
			return _weakRef ?? (_weakRef = new GeckoWeakReference(this));
		}

		#endregion nsISupportsWeakReference
	}
}
