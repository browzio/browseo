using Gecko.DOM;
using Gecko.DOM.HTML;
using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Gecko.Windows
{
	partial class WebView
	{
		private sealed class NativeView : NativeWindow, IDisposable
		{
			private WebView _view;

			internal NativeView(WebView view, IntPtr hView)
			{
				_view = view;
				this.AssignHandle(hView);
			}

			private void Dispose(bool disposing)
			{
				if (_view != null)
				{
					this.ReleaseHandle();
					_view = null;
				}
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			~NativeView()
			{
				Dispose(false);
			}

			protected override void WndProc(ref Message m)
			{
				if (_view.MozWndProc(ref m))
					return;

				base.WndProc(ref m);
			}

		}
	}
}
