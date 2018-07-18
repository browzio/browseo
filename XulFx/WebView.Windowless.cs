using System;
using System.ComponentModel;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.DOM;
using System.Collections.Generic;


namespace Gecko
{
	public partial class WindowlessWebView : Component, IEventedWebView
	{
		private static Dictionary<nsIWebBrowserChrome, WeakReference> _viewsCache;

		// stub fields
		private Uri _url; // stub (value: null)
		private object _contentView; // stub (value: new object())
		// uses fields
		private WebBrowserGlue _wbGlue;
		private GeckoWebNavigation _webNav;
		private nsIWebBrowserChrome _chrome;
		private GeckoDOMEventTarget _eventTarget;
		private bool _disposing;
		

		static WindowlessWebView()
		{
			_viewsCache = new Dictionary<nsIWebBrowserChrome, WeakReference>();
		}

		private static void AddToCache(WindowlessWebView view)
		{
			if (view._chrome == null)
				throw new InvalidOperationException();
			lock (_viewsCache)
			{
				_viewsCache[view._chrome] = new WeakReference(view);
			}
		}

		private static void RemoveFromCache(WindowlessWebView view)
		{
			lock (_viewsCache)
			{
				_viewsCache.Remove(view._chrome);
			}
		}

		internal static WebBrowserGlueBase GetGlue(nsIWebBrowserChrome chrome)
		{
			WeakReference weakRef;
			lock (_viewsCache)
			{
				if (_viewsCache.TryGetValue(chrome, out weakRef))
				{
					var view = weakRef.Target as WindowlessWebView;
					//if (view != null && view._webNav != null)
						return view._wbGlue;
				}
			}
			return null;
		}

		public WindowlessWebView()
			: this(false)
		{

		}

		public WindowlessWebView(bool isChrome)
		{
			_url = null;
			_contentView = new object();

			_wbGlue = new WebBrowserGlue(this);
		
			nsIDocShell docShell = null;
			nsIWebBrowser browser = null;
			nsIWebProgress progress = null;
			nsIAppShellService appShell = Xpcom.GetService<nsIAppShellService>(Contracts.AppShellService);
			try
			{
                _webNav = appShell.CreateWindowlessBrowser(isChrome).Wrap(GeckoWebNavigation.Create);
                browser = _webNav.QueryInterface<nsIWebBrowser>();
                _chrome = browser.GetContainerWindowAttribute();
                AddToCache(this);

                docShell = this.WebNav.QueryInterface<nsIDocShell>();

                GeckoPrincipal principals = isChrome ? GeckoPrincipal.SystemPrincipal : GeckoPrincipal.NullPrincipal;
                docShell.CreateAboutBlankContentViewer(principals.Instance);
                GC.KeepAlive(principals);

                progress = Xpcom.QueryInterface<nsIWebProgress>(docShell);
                progress.AddProgressListener(_wbGlue, (uint)(nsIWebProgressConsts.NOTIFY_STATE_ALL | nsIWebProgressConsts.NOTIFY_ALL));

                _eventTarget = this.Window.WindowRoot;
                foreach (string eventName in this.HandledDOMEvents)
                {
                    _eventTarget.AddEventListener(eventName, _wbGlue, true, true, 2);
                }
            }
			finally
			{
				Xpcom.FreeComObject(ref appShell);
				Xpcom.FreeComObject(ref progress);
				Xpcom.FreeComObject(ref docShell);
				Xpcom.FreeComObject(ref browser);
			}
		}

		~WindowlessWebView()
		{
			RemoveFromCache(this);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

            if (disposing && _webNav != null)
            {
                nsIBaseWindow baseWindow = null;
                try
                {
                    _disposing = true;
                    nsIDocShell docShell = null;
                    nsIWebProgress progress = null;
                    try
                    {
                        baseWindow = _webNav.QueryInterface<nsIBaseWindow>();
                        docShell = _webNav.QueryInterface<nsIDocShell>();
                        if (docShell != null && !docShell.IsBeingDestroyed())
                        {
                            var window = Xpcom.QueryInterface<nsIDOMWindow>(docShell).Wrap(GeckoWindow.Create);
                            if (window != null)
                            {
                                if (!window.Closed) window.Close();
                            }
                            progress = Xpcom.QueryInterface<nsIWebProgress>(docShell);
                            progress.RemoveProgressListener(_wbGlue);
                        }
                        _webNav = null;
                    }
                    finally
                    {
                        Xpcom.FreeComObject(ref progress);
                        Xpcom.FreeComObject(ref docShell);
                    }

                    if (_eventTarget != null)
                    {
                        //Remove Event Listener			
                        foreach (string eventName in this.HandledDOMEvents)
                        {
                            _eventTarget.RemoveEventListener(eventName, _wbGlue, true);
                        }
                        ((IDisposable)_eventTarget).Dispose();
                        _eventTarget = null;
                    }
                }
                finally
                {
                    _disposing = false;
                    baseWindow.Destroy();
                }
            }
        }

        protected GeckoWebNavigation WebNav
        {
            get
            {
                if (_webNav == null)
                    throw new ObjectDisposedException(this.GetType().Name);
                if (this.IsDead)
                    throw new InvalidOperationException();
                return _webNav;
            }
        }

        protected virtual bool IsDead { get; private set; }

        public virtual GeckoWindow Window
        {
            get
            {
                return this.WebNav.QueryInterface<nsIDOMWindow>().Wrap(GeckoWindow.Create);
            }
        }

        protected void AssertNotPreview()
		{

		}

		protected virtual void OnHandleDomEvent(GeckoDOMEventArgs e)
		{
			_wbGlue.DispatchDOMEvent(e);
		}

		public void SetSize(int width, int height)
		{
			nsIBaseWindow baseWindow = this.WebNav.QueryInterface<nsIBaseWindow>();
			if (baseWindow != null)
			{
				try
				{
					baseWindow.SetSize(width, height, false);
				}
				finally
				{
					Xpcom.FreeComObject(ref baseWindow);
				}
			}
		}

	}
}
