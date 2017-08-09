using System;
using Gecko.Interop;
using System.Runtime.InteropServices;
using Gecko.Interfaces;
using Gecko.DOM;


namespace Gecko
{
	public class GeckoWindowWeakReference
	{
		private ComObject<nsIWeakReference> _instance;

		public GeckoWindowWeakReference(GeckoWindow window)
		{
			nsISupportsWeakReference win = window.QueryInterface<nsISupportsWeakReference>();
			try
			{
				_instance = win.GetWeakReference().AsComObject();
			}
			finally
			{
				Xpcom.FreeComObject(ref win);
			}
		}

		public virtual GeckoWindow Target
		{
			get
			{
				GeckoWindow rv = null;
				if (_instance != null)
				{
					Guid iid = typeof(nsIDOMWindow).GUID;
					IntPtr pUnk = _instance.Instance.QueryReferent(ref iid);
					if (pUnk != IntPtr.Zero)
					{
						try
						{
							rv = ((nsIDOMWindow)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(nsIDOMWindow))).Wrap(GeckoWindow.Create);
						}
						finally
						{
							Marshal.Release(pUnk);
						}
					}
				}
				return rv;
			}
		}

		public virtual GeckoWindow GetAliveWindow()
		{
			Xpcom.AssertCorrectThread();

			GeckoWindow window = this.Target;
			if (window == null || window.Closed)
				throw new InvalidOperationException();
			return window;
		}
	}
}
