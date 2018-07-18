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
		protected virtual IList<string> HandledDOMEvents
		{
			get
			{
				return new List<string>(new string[] {
					"DOMTitleChanged",
					"submit",
                    "keydown",
                    "keyup",
                    "keypress",
                    "mousemove",
					"mouseover",
					"mouseout",
					"mousedown",
					"mouseup",
					"click",
					"dblclick",
					"compositionstart",
					"compositionend",
					"contextmenu",
					"DOMMouseScroll",
					"focus",
					"blur",
                    "pageshow",
					// Load event added here rather than DOMDocument as DOMDocument recreated when navigating
					// ths losing attached listener.
					"load",
					"DOMContentLoaded",
					"readystatechange",
					"change",
					"hashchange",
					"dragstart",
					"dragleave",
					"drag",
					"drop",
					"dragend",
					"DOMWindowClose", 
					"DOMWindowCreated",
					"scroll",
					"wheel",
					"fullscreenchange",

				});
			}
		}

		[BrowsableAttribute(false)]
		public virtual GeckoDocument Document
		{
			get
			{
				GeckoWindow window = this.Window;
				if (window != null)
					return window.Document;
				return null;
			}
		}

		[BrowsableAttribute(false)]
		public virtual string DocumentTitle
		{
			get
			{
				GeckoDocument document = this.Document;
				if (document != null)
					return document.Title;
				return string.Empty;
			}
		}

	}
}
