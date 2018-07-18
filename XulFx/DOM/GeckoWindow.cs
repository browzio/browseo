using Gecko.GUI;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.IO;
using Gecko.Javascript;
using Gecko.WebAPI;
using System;
using System.Runtime.InteropServices;

namespace Gecko.DOM
{
	public sealed class GeckoWindow : ComObject<nsIDOMWindow>, IGeckoObjectWrapper
	{
		private nsIXulfxDOMWindowHelper _helper;
		private mozIDOMWindow _mozDomWindow;
		private mozIDOMWindowProxy _mozWindowProxy;

		public static GeckoWindow Create(mozIDOMWindowProxy instance)
		{
			return new GeckoWindow(instance);
		}

		public static GeckoWindow Create(nsIDOMWindow instance)
		{
			return new GeckoWindow(instance);
		}

		private GeckoWindow(mozIDOMWindowProxy window)
			: base(Xpcom.QueryInterface<nsIDOMWindow>(window))
		{
			_mozWindowProxy = window;
		}

		private GeckoWindow(nsIDOMWindow window)
			: base(window)
		{
			_mozWindowProxy = this.QueryInterface<mozIDOMWindowProxy>();
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _mozWindowProxy);
			Xpcom.FreeComObject(ref _helper);
			Xpcom.FreeComObject(ref _mozDomWindow);
			base.Dispose(disposing);
		}

		public mozIDOMWindowProxy MozWindowProxy
		{
			get
			{
				if (_mozWindowProxy == null)
					throw new ObjectDisposedException(this.GetType().Name);
				return _mozWindowProxy;
			}
		}

		public mozIDOMWindow MozInstance
		{
			get
			{
				if (_mozDomWindow == null)
					_mozDomWindow = this.Helper.GetMozWindowAttribute();
				return _mozDomWindow;
			}
		}

        public nsIXulfxDOMWindowHelper Helper
        {
            get
            {
                if (_helper == null)
                {
                    // It will be thrown ObjectDisposedException, if the object is destroyed.
                    mozIDOMWindowProxy window = this.QueryInterface<mozIDOMWindowProxy>();
                    try
                    {
                        var helper = Xpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
                        helper.Init(window);
                        _helper = helper;
                    }
                    finally
                    {
                        Xpcom.FreeComObject(ref window);
                    }
                }
                return _helper;
            }
        }

        public GeckoWindowUtils WindowUtils
		{
			get
			{
				return Xpcom.QueryInterface<nsIDOMWindowUtils>(Instance).Wrap(GeckoWindowUtils.Create);
			}
		}

		/// <summary>
		/// Gets the window property of a window object points to the window object itself.
		/// </summary>
		public GeckoWindow Window
		{
			get { return Helper.GetWindowAttribute().Wrap(GeckoWindow.Create); }
		}

		/// <summary>
		/// Gets the document displayed in the window.
		/// </summary>
		public GeckoDocument Document
		{
			get { return Helper.GetDocumentAttribute().Wrap(GeckoDocument.Create); }
            //get { return this.QueryInterface<nsIDOMDocument>().Wrap(GeckoDocument.Create); }
		}

		/// <summary>
		/// Get the window root for this window. This is useful for hooking
		/// up event listeners to this window and every other window nested
		/// in the window root.
		/// </summary>
		public GeckoDOMEventTarget WindowRoot
		{
			get { return Helper.GetWindowRootAttribute().Wrap(GeckoDOMEventTarget.Create); }
		}

		/// <summary>
		/// Returns a reference to the window that opened this current window.
		/// </summary>
		public GeckoWindow Opener
		{
			get { return Helper.GetOpenerAttribute().Wrap(GeckoWindow.Create); }
			set { Helper.SetOpenerAttribute(value != null ? value.MozWindowProxy : null); }
		}

		/// <summary>
		/// Returns a reference to the parent of the current window or subframe.
		/// If a window does not have a parent, its parent property is a reference to itself.
		/// </summary>
		public GeckoWindow Parent
		{
			get { return Helper.GetParentAttribute().Wrap(GeckoWindow.Create); }
		}

		/// <summary>
		/// Returns a Window object for the primary content window. This is useful in XUL windows that have
		/// a &lt;browser&gt; (or tabbrowser or &lt;iframe&gt;) with type=&quot;content-primary&quot; attribute
		/// on it - the most famous example is Firefox main window, browser.xul. In such cases, content
		/// returns a reference to the Window object for the document currently displayed in the browser.\
		/// In unprivileged content (webpages), content is normally equivalent to top.
		/// </summary>
		public GeckoWindow Content
		{
			get { return Helper.GetContentAttribute().Wrap(GeckoWindow.Create); }
		}

		/// <summary>
		/// Gets this window&apos;s &lt;iframe&gt; or &lt;frame&gt; element, if it has one.
		/// </summary>
		public GeckoElement FrameElement
		{
			get { return Helper.GetFrameElementAttribute().Wrap(GeckoElement.Create); }
		}

		public void ScrollTo(int xScroll, int yScroll)
		{
			Helper.ScrollTo(xScroll, yScroll);
		}

		public void ScrollBy(int xScrollDif, int yScrollDif)
		{
			Helper.ScrollBy(xScrollDif, yScrollDif);
		}

		public void ScrollByLines(int numLines)
		{
			Helper.ScrollByLines(numLines);
		}

		public void ScrollByPages(int numPages)
		{
			Helper.ScrollByPages(numPages);
		}

		public void SizeToContent()
		{
			Helper.SizeToContent();
		}

		public GeckoWindow Top
		{
			get { return Helper.GetTopAttribute().Wrap(GeckoWindow.Create); }
		}

		public GeckoWindow Self
		{
			get { return Helper.GetSelfAttribute().Wrap(GeckoWindow.Create); }
		}

		public float InnerScreenX
		{
			get { return Helper.GetMozInnerScreenXAttribute(); }
		}

		public float InnerScreenY
		{
			get { return Helper.GetMozInnerScreenYAttribute(); }
		}

		public int InnerWidth
		{
			get { return Helper.GetInnerWidthAttribute(); }
			set { Helper.SetInnerWidthAttribute(value); }
		}

		public int InnerHeight
		{
			get { return Helper.GetInnerHeightAttribute(); }
			set { Helper.SetInnerHeightAttribute(value); }
		}

		public int OuterWidth
		{
			get { return Helper.GetOuterWidthAttribute(); }
			set { Helper.SetOuterWidthAttribute(value); }
		}

		public int OuterHeight
		{
			get { return Helper.GetOuterHeightAttribute(); }
			set { Helper.SetOuterHeightAttribute(value); }
		}

		public float DevicePixelRatio
		{
			get { return Helper.GetDevicePixelRatioAttribute(); }
		}

		public int PageXOffset
		{
			get { return Helper.GetPageXOffsetAttribute(); }
		}

		public int PageYOffset
		{
			get { return Helper.GetPageYOffsetAttribute(); }
		}

		public int ScreenX
		{
			get { return Helper.GetScreenXAttribute(); }
			set { Helper.SetScreenXAttribute(value); }
		}

		public int ScreenY
		{
			get { return Helper.GetScreenYAttribute(); }
			set { Helper.SetScreenYAttribute(value); }
		}

		public int ScrollX
		{
			get { return Helper.GetScrollXAttribute(); }
		}

		public int ScrollY
		{
			get { return Helper.GetScrollYAttribute(); }
		}

		public int ScrollMaxX
		{
			get { return Helper.GetScrollMaxXAttribute(); }
		}

		public int ScrollMaxY
		{
			get { return Helper.GetScrollMaxYAttribute(); }
		}

		public string Name
		{
			get { return nsString.Get(Helper.GetNameAttribute); }
			set { nsString.Set(Helper.SetNameAttribute, value); }
		}

		public bool FullScreen
		{
			get { return Helper.GetFullScreenAttribute(); }
			set { Helper.SetFullScreenAttribute(value); }
		}

		public int Length
		{
			get { return (int)Helper.GetLengthAttribute(); }
		}

		public void Print()
		{
			nsIWebBrowserPrint print = Xpcom.QueryInterface<nsIWebBrowserPrint>(Instance);
			try
			{
				print.Print(null, null);
			}
			finally
			{
				Xpcom.FreeComObject(ref print);
			}
		}

		public GeckoSelection Selection
		{
			get { return Helper.GetSelection().Wrap(GeckoSelection.Create); }
		}

		public GeckoWindowCollection Frames
		{
			get { return Helper.GetFramesAttribute().Wrap(GeckoWindowCollection.Create); }
		}

		public GeckoNavigator Navigator
		{
			get
			{
				GeckoNavigator navigator;
				nsISupports comObj = Helper.GetNavigatorAttribute();
				navigator = Xpcom.QueryInterface<nsIDOMNavigator>(comObj).Wrap(GeckoNavigator.Create);
				Xpcom.FreeComObject(ref comObj);
				return navigator;
			}
		}

		/// <summary>
		/// Returns a reference to the screen object associated with the window. The screen object is a special object for inspecting properties of the screen on which the current window is being rendered.
		/// </summary>
		public GeckoScreen Screen
		{
			get { return Helper.GetScreenAttribute().Wrap(GeckoScreen.Create); }
		}

		/// <summary>
		/// Returns a <see cref="GeckoLocation"/> object with information about the current location of the document.
		/// </summary>
		public GeckoLocation Location
		{
			get { return Helper.GetLocationAttribute().Wrap(GeckoLocation.Create); }
		}

		public bool Closed
		{
			get { return Helper.GetClosedAttribute(); }
		}

		public string Status
		{
			get { return nsString.Get(Helper.GetStatusAttribute); }
			set { nsString.Set(Helper.SetStatusAttribute, value); }
		}


		/// <summary>
		/// Gets or sets a value that determines whether DNS prefetch is allowed for this subtree
		/// of the window tree. Defaults to true. Setting this will make it take
		/// effect starting with the next document loaded in the window.
		/// </summary>
		public bool AllowDNSPrefetch
		{
			get
			{
				bool value;
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					value = docShell.GetAllowDNSPrefetchAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
				return value;
			}
			set
			{
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					docShell.SetAllowDNSPrefetchAttribute(value);
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that indicating whether or not images should be loaded.
		/// </summary>
		public bool AllowImages
		{
			get
			{
				bool value;
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					value = docShell.GetAllowImagesAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
				return value;
			}
			set
			{
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					docShell.SetAllowImagesAttribute(value);
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that indicating whether to allow Javascript execution.
		/// </summary>
		public bool AllowJavascript
		{
			get
			{
				bool value;
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					value = docShell.GetAllowJavascriptAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
				return value;
			}
			set
			{
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					docShell.SetAllowJavascriptAttribute(value);
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that indicating whether or not media (audio/video) should be loaded.
		/// </summary>
		public bool AllowMedia
		{
			get
			{
				bool value;
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					value = docShell.GetAllowMediaAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
				return value;
			}
			set
			{
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					docShell.SetAllowMediaAttribute(value);
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that stating if refresh based redirects can be allowed.
		/// </summary>
		public bool AllowMetaRedirects
		{
			get
			{
				bool value;
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					value = docShell.GetAllowMetaRedirectsAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
				return value;
			}
			set
			{
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					docShell.SetAllowMetaRedirectsAttribute(value);
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that indicating whether to allow plugin execution.
		/// </summary>
		public bool AllowPlugins
		{
			get
			{
				bool value;
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					value = docShell.GetAllowPluginsAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
				return value;
			}
			set
			{
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					docShell.SetAllowPluginsAttribute(value);
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that stating if it should allow subframes (framesets/iframes) or not.
		/// </summary>
		public bool AllowSubframes
		{
			get
			{
				bool value;
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					value = docShell.GetAllowSubframesAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
				return value;
			}
			set
			{
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					docShell.SetAllowSubframesAttribute(value);
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value that indicating are plugins allowed in the current
		/// document loaded in this docshell (if there is one). This depends on whether
		/// plugins are allowed by this window itself or if the document is sandboxed
		/// and hence plugins should not be allowed.
		/// </summary>
		public bool PluginsAllowedInCurrentDoc
		{
			get
			{
				bool value;
				nsIDocShell docShell = this.QueryInterface<nsIDocShell>();
				try
				{
					value = docShell.PluginsAllowedInCurrentDoc();
				}
				finally
				{
					Xpcom.FreeComObject(ref docShell);
				}
				return value;
			}
		}

		public void LoadHtml(string content, string url)
		{
			LoadContent(content, url, "text/html");
		}

		public void LoadContent(string content, string url, string contentType)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			if (contentType == null)
				throw new ArgumentNullException("contentType");

			using (var sContentType = new nsACString(contentType))
			{
				using (var sUtf8 = new nsACString("UTF8"))
				{
					ByteArrayInputStream inputStream = null;
					try
					{
						inputStream = ByteArrayInputStream.Create(System.Text.Encoding.UTF8.GetBytes(content != null ? content : string.Empty));

						nsIURI uri = null;
						nsIDocShellLoadInfo loadInfo = null;
						nsIDocShell docShell = Xpcom.QueryInterface<nsIDocShell>(Instance);
						try
						{
							uri = IOService.GetService().CreateNsIUri(url);
							docShell.CreateLoadInfo(out loadInfo);
							docShell.LoadStream(inputStream, uri, sContentType, sUtf8, loadInfo);
						}
						finally
						{
							Xpcom.ReleaseComObject(docShell);
							Xpcom.FreeComObject(ref uri);
							Xpcom.FreeComObject(ref loadInfo);
						}
					}
					finally
					{
						if (inputStream != null)
							inputStream.Close();
					}
				}
			}
		}

		public void Back()
		{
			Helper.Back();
		}

		public void Forward()
		{
			Helper.Forward();
		}

		public void Home()
		{
			Helper.Home();
		}

		/// <summary>
		/// This method stops window loading.
		/// </summary>
		public void Stop()
		{
			Helper.Stop();
		}

		public void MoveTo(int x, int y)
		{
			Helper.MoveTo(x, y);
		}

		public void MoveBy(int xDif, int yDif)
		{
			Helper.MoveBy(xDif, yDif);
		}

		public void ResizeTo(int width, int height)
		{
			Helper.ResizeTo(width, height);
		}

		public void ResizeBy(int widthDif, int heightDif)
		{
			Helper.ResizeBy(widthDif, heightDif);
		}

		/// <summary>
		/// Evaluate javascript in the window.
		/// </summary>
		/// <param name="scriptCode">The script to evaluate in the window.</param>
		/// <exception cref="Gecko.GeckoException"></exception>
		/// <returns></returns>
		public string Evaluate(string scriptCode)
		{
            return "";// GeckoJavascriptBridge.GetService().EvaluateInWindow(this, scriptCode).CastToString();
		}

		/// <summary>
		/// The Window.getDefaultComputedStyle() gives the default computed values of all the CSS
		/// properties of an element, ignoring author styling.  That is, only user-agent and
		/// user styles are taken into account.
		/// </summary>
		/// <param name="element">The element for which to get the computed style.</param>
		/// <param name="pseudoElement">A string specifying the pseudo-element to match. Must be omitted (or null) for regular elements.</param>
		public GeckoCSSStyleDeclaration GetDefaultComputedStyle(GeckoElement element, string pseudoElement)
		{
			if (element == null)
				return null;

			using (var pseudoElt = new nsAString(pseudoElement))
			{
				return Helper.GetDefaultComputedStyle(element.Instance, pseudoElt).Wrap(GeckoCSSStyleDeclaration.Create);
			}
		}

		/// <summary>
		/// The Window.getComputedStyle() method gives the values of all the CSS properties
		/// of an element after applying the active stylesheets and resolving any basic
		/// computation those values may contain.
		/// </summary>
		/// <param name="element">The element for which to get the computed style.</param>
		/// <param name="pseudoElement">A string specifying the pseudo-element to match. Must be omitted (or null) for regular elements.</param>
		public GeckoCSSStyleDeclaration GetComputedStyle(GeckoElement element, string pseudoElement)
		{
			if (element == null)
				return null;

			using (var pseudoElt = new nsAString(pseudoElement))
			{
				return Helper.GetComputedStyle(element.Instance, pseudoElt).Wrap(GeckoCSSStyleDeclaration.Create);
			}
		}

		/// <summary>
		/// Opens a new window and loads the document specified by a given URL.
		/// </summary>
		/// <param name="url">The URL to be loaded in the newly opened window. strUrl can be an HTML document on the web, image file or any resource supported by the browser.</param>
		/// <param name="name">A string name for the new window.</param>
		/// <param name="options">An optional parameter listing the features (size, position, scrollbars, etc.) of the new window as a string.
		/// The string must not contain any whitespace, and each feature name and its value must be separated by a comma.</param>
		/// <returns>The newly created window. If the call failed, it will be null.</returns>
		public GeckoWindow Open(string url, string name, string options)
		{
			using(nsAString aUrl = new nsAString(url), aName = new nsAString(name), aOpt = new nsAString(options))
			{
				return Helper.Open(aUrl, aName, aOpt).Wrap(GeckoWindow.Create);
			}
		}

		public GeckoWindow OpenDialog(string url, string name, string options, nsISupportsArray extra)
		{
			using (nsAString aUrl = new nsAString(url), aName = new nsAString(name), aOpt = new nsAString(options))
			{
				return Helper.OpenDialog(aUrl, aName, aOpt, extra != null ? Xpcom.QueryInterface<nsISupports>(extra) : null).Wrap(GeckoWindow.Create);
			}
		}

		public void Close()
		{
			Helper.Close();
		}

		public bool DispatchEvent(GeckoEvent e)
		{
			GeckoDOMEventTarget eventTarget = this.QueryInterface<nsIDOMEventTarget>().Wrap(GeckoDOMEventTarget.Create);
			if (eventTarget == null)
				throw new InvalidOperationException();
			return eventTarget.DispatchEvent(e);
		}

		public void Find(string str, bool caseSensitive, bool backwards, bool wrapAround, bool wholeWord, bool searchInFrames, bool showDialog)
		{
			using(var aStr = new nsAString(str))
			{
				Helper.Find(aStr, caseSensitive, backwards, wrapAround, wholeWord, searchInFrames, showDialog);
			}
			
		}

		public void Focus()
		{
			Helper.Focus();
		}

		public void Blur()
		{
			Helper.Blur();
		}

		public void Alert(string text)
		{
			nsString.Set(Helper.Alert, text ?? "null");
		}

		public bool Confirm(string text)
		{
			return nsString.Pass(Helper.Confirm, text);
		}

		public string Prompt(string message = null, string initialValue = null)
		{
			using(nsAString aMessage = new nsAString(message), aValue = new nsAString(initialValue), aResult = new nsAString())
			{
				Helper.Prompt(aMessage, aValue, aResult);
				return aResult.ToString();
			}
			
		}

		public string Atob(string asciiString)
		{
			return nsString.Get(Helper.Atob, asciiString);
		}

		public string Btoa(string base64Data)
		{
			return nsString.Get(Helper.Btoa, base64Data);
		}

		public void Scroll(int xScroll, int yScroll)
		{
			Helper.Scroll(xScroll, yScroll);
		}

		public long MozPaintCount
		{
			get { return (long)Helper.GetMozPaintCountAttribute(); }
		}

		public GeckoPerformance Performance
		{
			get { return Helper.GetPerformanceAttribute().Wrap(GeckoPerformance.Create); }
		}
    }
}
