using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;

// This file contains shared properties for any GUI (XUL, WPF, WinForms)
#if WINDOWS

namespace Gecko.Windows
{	
	partial class WebView

#else

namespace Gecko
{
	partial class WindowlessWebView

#endif
	{
		/// <summary>
		/// Gets or sets the URL of the current document.
		/// </summary>
		[BrowsableAttribute(false), BindableAttribute(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Uri Url
		{
			get
			{
				if (_contentView != null)
				{
					GeckoWebNavigation webNav = this.WebNav;
					if (webNav == null)
						return new Uri("about:blank");

					Uri url = webNav.CurrentUri;
					if (url != null)
						return url;
				}
				return _url ?? new Uri("about:blank");
			}
			set
			{
				Navigate(value ?? new Uri("about:blank"));
			}
		}

		[BrowsableAttribute(false)]
		public virtual bool IsBusy { get; private set; }

		/// <summary>
		/// Gets a value indicating whether a previous page in navigation history is available, which allows the GoBack method to succeed.
		/// </summary>
		[BrowsableAttribute(false)]
		public virtual bool CanGoBack
		{
			get { return WebNav.CanGoBack; }
		}

		/// <summary>
		/// Gets a value indicating whether a subsequent page in navigation history is available, which allows the GoForward method to succeed.
		/// </summary>
		[BrowsableAttribute(false)]
		public virtual bool CanGoForward
		{
			get { return WebNav.CanCanGoForward; }
		}

		/// <summary>
		/// Gets the status text of the WebView control.
		/// </summary>
		[BrowsableAttribute(false)]
		public virtual string StatusText { get; private set; }
	}
}
