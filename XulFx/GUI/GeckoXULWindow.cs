using System;
using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Linq;
using Gecko.IO;
using System.Runtime.InteropServices;
using Gecko.CustomMarshalers;
using SpiderMonkey;
using System.Windows.Threading;

namespace Gecko.GUI
{
    //nsIBrowserDOMWindow
    public class nsBrowserDOMWindow : nsIBrowserDOMWindow
    {
        bool nsIBrowserDOMWindow.CanClose()
        {
            return false;
        }

        bool nsIBrowserDOMWindow.IsTabContentWindow(nsIDOMWindow aWindow)
        {
            return true;
        }

        mozIDOMWindowProxy nsIBrowserDOMWindow.OpenURI(nsIURI aURI, mozIDOMWindowProxy aOpener, short aWhere, int aFlags)
        {
            return null;
        }

        nsIFrameLoaderOwner nsIBrowserDOMWindow.OpenURIInFrame(nsIURI aURI, nsIOpenURIInFrameParams @params, short aWhere, int aFlags)
        {
            return null;
        }
    }

    public class nsWindowMediatorListener : nsIWindowMediatorListener
    {
        #region nsIWindowMediatorListener
        /**
         * nsIWindowMediatorListener implementation.
         *
         * See _onTabClosed for explanation of why we needn't actually tweak any
         * actors or tables here.
         *
         * An nsIWindowMediatorListener's methods get passed all sorts of windows; we
         * only care about the tab containers. Those have 'getBrowser' methods.
         */
        void nsIWindowMediatorListener.OnWindowTitleChange(nsIXULWindow window, string newTitle)
        {
            //BrowserTabList.prototype.onWindowTitleChange = () => { };
        }

        void nsIWindowMediatorListener.OnOpenWindow(nsIXULWindow window)
        {
        }

        void nsIWindowMediatorListener.OnCloseWindow(nsIXULWindow window)
        {
        }
        #endregion
    }
    /// <summary>
    /// 		public WindowlessWebView(bool isChrome)
    ///		{
    ///			_url = null;
    ///			_contentView = new object();
    ///
    ///			_wbGlue = new WebBrowserGlue(this);
    ///
    ///    nsIDocShell docShell = null;
    ///    nsIWebBrowser browser = null;
    ///    nsIWebProgress progress = null;
    ///    nsIAppShellService appShell = Xpcom.GetService<nsIAppShellService>(Contracts.AppShellService);
    ///			try
    ///			{
    ///                _webNav = appShell.CreateWindowlessBrowser(isChrome).Wrap(GeckoWebNavigation.Create);
    ///    browser = _webNav.QueryInterface<nsIWebBrowser>();
    ///                _chrome = browser.GetContainerWindowAttribute();
    ///                AddToCache(this);
    ///
    ///    docShell = this.WebNav.QueryInterface<nsIDocShell>();
    ///
    ///    GeckoPrincipal principals = isChrome ? GeckoPrincipal.SystemPrincipal : GeckoPrincipal.NullPrincipal;
    ///    docShell.CreateAboutBlankContentViewer(principals.Instance);
    ///    GC.KeepAlive(principals);
    ///
    ///    progress = Xpcom.QueryInterface<nsIWebProgress>(docShell);
    ///    progress.AddProgressListener(_wbGlue, (uint)(nsIWebProgressConsts.NOTIFY_STATE_ALL | nsIWebProgressConsts.NOTIFY_ALL));
    ///
    ///    _eventTarget = this.Window.WindowRoot;
    ///                foreach (string eventName in this.HandledDOMEvents)
    ///                {
    ///        _eventTarget.AddEventListener(eventName, _wbGlue, true, true, 2);
    ///    }
    ///    }
    ///			finally
    ///			{
    ///				Xpcom.FreeComObject(ref appShell);
    ///				Xpcom.FreeComObject(ref progress);
    ///				Xpcom.FreeComObject(ref docShell);
    ///				Xpcom.FreeComObject(ref browser);
    ///			}
    ///		}
    /// </summary>
    public class GeckoXULWindow : ComObject<nsIXULWindow>, 
        //nsIBaseWindow, 
        nsIInterfaceRequestor,
        nsISupportsWeakReference,
        nsIWebProgressListener
    //nsIObserver
    {
        private ComObject<nsIBaseWindow> _baseWindow;
        private ComObject<nsIDocShell> _docshell;
        private ComObject<nsIWebProgress> _progress;
        private ComObject<nsIXULBrowserWindow> mXULBrowserWindow;
        private bool _isChromeLoaded;
        private GeckoWeakReference _weakRef;

        private static nsIXULWindow CreateTopLevelWindow()
        {
            nsIURI uri = null;
            nsIXULWindow window = null;
            nsIAppShellService appShellSvc = null;
            try
            {
                //using (var windowWatcher = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
                //{
                //    //windowWatcher.Instance.OpenWindow()
                //    //using (var creator = windowWatcher.QueryInterface<nsIWindowCreator>().AsComObject())
                //    //{
                //    //    var chrome = creator.Instance.CreateChromeWindow(null, (uint)nsIWebBrowserChromeConsts.CHROME_ALL);
                //    //    var xulWindow = Xpcom.QueryInterface<nsIXULWindow>(chrome);
                //    //}
                //}
                

                //uri = IOService.GetService().CreateNsIUri("chrome://xulfx/content/browser.xul");
                //uri = IOService.GetService().CreateNsIUri("chrome://xulfx/content/browser.xul");
                uri = IOService.GetService().CreateNsIUri("chrome://browser/content/browser.xul");
                //uri = IOService.GetService().CreateNsIUri("chrome://browser/content/abouthome/aboutHome.xhtml");
                appShellSvc = Xpcom.GetService<nsIAppShellService>(Contracts.AppShellService);
                if (appShellSvc != null)
                {

                    //appShellSvc.CreateHiddenWindow();
                    var hiddenAppShelWin = appShellSvc.GetHiddenWindowAttribute();
                    var hiddenAppShelDomWin = appShellSvc.GetHiddenDOMWindowAttribute();

                    window = appShellSvc.CreateTopLevelWindow(null,
                        uri, 
                        (uint)nsIWebBrowserChromeConsts.CHROME_ALL, 
                        nsIAppShellServiceConsts.SIZE_TO_CONTENT,
                        nsIAppShellServiceConsts.SIZE_TO_CONTENT,
                        null,
                        null);
                    appShellSvc.RegisterTopLevelWindow(window);
                    using (var windowMediator = Xpcom.GetService2<Gecko.Interfaces.nsIWindowMediator_44>("@mozilla.org/appshell/window-mediator;1"))
                    {
                        windowMediator.Instance.AddListener(new nsWindowMediatorListener());
                    }

                    using (var thisWindow = Xpcom.QueryInterface2<mozIDOMWindowProxy>(window))
                    {
                        using (var domchromeWindow = Xpcom.QueryInterface2<nsIDOMChromeWindow>(thisWindow.Instance))
                        {
                            domchromeWindow.Instance.SetOpenerForInitialContentBrowser(thisWindow.Instance);
                            domchromeWindow.Instance.SetBrowserDOMWindowAttribute(new nsBrowserDOMWindow());
                        }

                        using (var windowWatcher = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
                        {
                            windowWatcher.Instance.SetActiveWindowAttribute(thisWindow.Instance);
                        }
                    }
                }



                //using (var windowWatcher = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
                //{
                //    using (var creator = Xpcom.GetService2<Gecko.Interfaces.nsIWindowCreator>(Gecko.Contracts.AppStartup))
                //    {
                //        windowWatcher.Instance.SetWindowCreator(creator.Instance.CreateChromeWindow);
                //    }
                //    windowWatcher.Instance.SetWindowCreator(WindowCreator.Instance);
                //}
            }
            finally
            {
                Xpcom.FreeComObject(ref uri);
                Xpcom.FreeComObject(ref appShellSvc);
            }

            if (window != null)
            {
                window.SetContextFlagsAttribute((uint)0);
                return window;
            }

            throw Marshal.GetExceptionForHR(GeckoError.NS_ERROR_FAILURE);
        }

        public GeckoXULWindow()
            : base(CreateTopLevelWindow())
        {
            _baseWindow = Xpcom.QueryInterface2<nsIBaseWindow>(Instance);
            _docshell = Instance.GetDocShellAttribute().AsComObject();
            _docshell.Instance.PrepareForNewContentModel();
            //_docshell.Instance.SetChromeEventHandlerAttribute(null);
            //using (var nsXPConnect = Xpcom.GetService2<Gecko.Interfaces.nsIXPConnect>("@mozilla.org/js/xpc/XPConnect;1"))
            //{
            //    nsIPrincipal systemPrincipal = Xpcom.CreateInstance<nsIPrincipal>(Contracts.SystemPrincipal);
            //    var sbox = nsXPConnect.Instance.CreateSandbox(this.WidgetHandle, systemPrincipal);
            //}

            //try
            //{
            //    nsIPrincipal systemPrincipal = Xpcom.CreateInstance<nsIPrincipal>(Contracts.SystemPrincipal);
            //    nsIDOMStorageManager storageManager = Xpcom.GetService<nsIDOMStorageManager>("@mozilla.org/dom/sessionStorage-manager;1");

            //    using (var thisWindow = Xpcom.QueryInterface2<mozIDOMWindowProxy>(this.Instance))
            //    {
            //        var helper = Xpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
            //        helper.Init(thisWindow.Instance);
            //        var mozWindow = helper.GetMozWindowAttribute();
            //        //using (var docUri = new nsAString("chrome://browser/content/browser.xul"))
            //        //{
            //        //    //helper.GetDocumentAttribute().GetDocumentURIAttribute(docUri);
            //        //    var storage = storageManager.CreateStorage(mozWindow, systemPrincipal, docUri, false);
            //        //}
            //        //using (var docUri = new nsAString())
            //        //{
            //        //    helper.GetDocumentAttribute().GetDocumentURIAttribute(docUri);
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            //_docshell.Instance.LoadURI(IOService.GetService().CreateNsIUri("https://developer.mozilla.org/en-US/docs/Mozilla/Tech/XPCOM/Reference/Interface/nsIDocShell#loadURI()"),null, 65535,false);
            //_docshell.Instance.SetDefaultLoadFlagsAttribute
            //var domStorageManager = Xpcom.GetService<nsIDOMStorageManager>("@mozilla.org/dom/storagemanager;1");
            //var systemPrincipal = Xpcom.CreateInstance<nsIPrincipal>(Contracts.SystemPrincipal);
            //////systemPrincipal.s
            //using (var thisWindow = Xpcom.QueryInterface2<mozIDOMWindowProxy>(this.Instance))
            //{
            //    var helper = Xpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
            //    helper.Init(thisWindow.Instance);
            //    var mozWindow = helper.GetMozWindowAttribute();

            //    using (var docUri = new nsAString())
            //    {
            //        helper.GetDocumentAttribute().GetDocumentURIAttribute(docUri);
            //    }
            //}
            //var domStorageManager = Components.classes["@mozilla.org/dom/storagemanager;1"]
            // .getService(Components.interfaces.nsIDOMStorageManager);


            // _docshell.Instance.AddSessionStorage(systemPrincipal, domStorageManager.GetHashCode);

            //_docshell.Instance.PrepareForNewContentModel();
            //_docshell.Instance.ResumeRefreshURIs();
            //_docshell.Instance.SetAllowWindowControlAttribute(false);
            //_docshell.Instance.SetIsAppTabAttribute(true);
            //_docshell.Instance.SetCurrentScrollRestorationIsManualAttribute(true);
            //_docshell.Instance.SetSandboxFlagsAttribute(nsIContentSecurityPolicyConsts.WEB_MANIFEST_SRC_DIRECTIVE);

            _progress = _docshell.QueryInterface<nsIWebProgress>().AsComObject();
            if (_progress != null)
            {
                _progress.Instance.AddProgressListener(this, (uint)(nsIWebProgressConsts.NOTIFY_ALL) | (uint)(nsIWebProgressConsts.NOTIFY_STATE_ALL));
            }

            // TODO: wait until the chrome has loaded
            //Xpcom.DoEvents(); throws InvalidOperationException on WPF
            //WaitUntilChromeLoad();
        }

        public ComObject<nsIDocShell> Docshell
        {
            get
            {
                Xpcom.AssertCorrectThread();
                if (_baseWindow == null)
                    throw new ObjectDisposedException(this.GetType().Name);

                return _docshell;
            }
        }

        public ComObject<nsIBaseWindow> BaseWindow
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

        public void AfterChromeLoaded()
        {
            //var editor = _docshell.Instance.GetEditorAttribute();
            //if(editor == null)
            //{
            //    editor = Xpcom.CreateInstance<nsIEditor>(typeof(nsIEditor).GUID);
            //}
            //var wind = Instance;
            //System.Threading.Thread thread = new System.Threading.Thread(() => 
            //{
            //    System.Threading.SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));
            //    // No owner; ShowDialog to prevent exit
            //    wind.ShowModal();
            //});
            ////thread.SetCompressedStack(System.Threading.CompressedStack.GetCompressedStack());
            //thread.SetApartmentState(System.Threading.ApartmentState.STA);
            //thread.DisableComObjectEagerCleanup();
            //thread.Start();
            //thread.Join();

            // Instance.ApplyChromeFlags();

            //SyncAttributesToWidget
            nsIContentViewer cv = _docshell.Instance.GetContentViewerAttribute(); //View.QueryInterface<nsIContentViewer>();
            _docshell.Instance.BeginRestore(cv,true);
            //cv.Show();
            //nsIDOMDocument document = cv.GetDOMDocumentAttribute();
            //nsIDOMElement windowElement = document.GetActiveElementAttribute();
            // windowElement.
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
            BaseWindow.Instance.Repaint(true);
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

        #region interop
        private IntPtr _widgetHandle;
        public IntPtr WidgetHandle
        {
            get
            {
                if (_widgetHandle == IntPtr.Zero)
                {
                    //GeckoXULWindow widget = this;
                    //if (widget == null)
                    //    return IntPtr.Zero;
                    _widgetHandle = this.Instance.GetHandle();
                }
                return _widgetHandle;
            }
        }
        #endregion

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
            if (_isChromeLoaded)
            {
                //using (var sessionstore = Xpcom.QueryInterface2<nsISessionStore>("@mozilla.org/browser/sessionstore;1"))
                //{
                //    //using (var BrowserElementProxy = Xpcom.GetService2<Gecko.Interfaces.nsIDOMGlobalPropertyInitializer>("@mozilla.org/dom/browser-element-proxy;1"))
                //    //{
                //    //    var thisWindow = Xpcom.QueryInterface<mozIDOMWindowProxy>(window);
                //    //    var helper = Xpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
                //    //    helper.Init(thisWindow);

                //    //    BrowserElementProxy.Instance.Init(helper.GetMozWindowAttribute());


                //    var value = JSVal.Create(new nsAString("\"{\"version\":[\"sessionrestore\",1],\"windows\":[{\"tabs\":[{\"entries\":[{\"url\":\"about:sessionstore\",\"charset\":\"\",\"ID\":3,\"docshellID\":195,\"triggeringPrincipal_b64\":\"vQZuXxRvRHKDMXv9BbHtkAAAAAAAAAAAwAAAAAAAAEYAAAAA\",\"principalToInherit_base64\":\"vQZuXxRvRHKDMXv9BbHtkAAAAAAAAAAAwAAAAAAAAEYAAAAA\",\"triggeringPrincipal_base64\":\"SmIS26zLEdO3ZQBgsLbOywAAAAAAAAAAwAAAAAAAAEY=\",\"docIdentifier\":3,\"persist\":true}],\"lastAccessed\":1511981487128,\"hidden\":false,\"attributes\":{},\"userContextId\":0,\"index\":1,\"userTypedValue\":\"\",\"userTypedClear\":0,\"image\":null,\"iconLoadingPrincipal\":\"SmIS26zLEdO3ZQBgsLbOywAAAAAAAAAAwAAAAAAAAEY=\"}],\"selected\":1,\"_closedTabs\":[],\"busy\":false,\"width\":1858,\"height\":1139,\"screenX\":196,\"screenY\":121,\"sizemode\":\"normal\"}],\"selectedWindow\":1,\"_closedWindows\":[],\"session\":{\"lastUpdate\":1511981487130,\"startTime\":1511378613211,\"recentCrashes\":0},\"global\":{}}\"").Container);
                //    var domwindow = Xpcom.QueryInterface<nsIDOMWindow>(Instance);

                //    var val = new nsAString();
                //    sessionstore.Instance.GetWindowState(domwindow, val);
                //    //sessionstore.Instance.SetWindowValue(domwindow, new nsAString("MainWindow"), ref value);
                //    //}
                //}

                //var tabbrowser = (Gecko.DOM.GeckoXULElement)this.View.Document.GetElementById("content");
                //var tab = (Gecko.DOM.GeckoXULElement)tabbrowser.GetProperty<nsIDOMNode>("selectedBrowser").Wrap(GeckoNode.Create);
                ////var SelectedContentDocument = tab.GetProperty<nsIDOMDocument>("contentDocument").Wrap(GeckoDocument.Create);

                //var broadcaster = tab.GetProperty<nsIMessageListenerManager>("messageManager");
                return;
                //var tabbrowser = (Gecko.DOM.GeckoXULElement)this.View.Document.GetElementById("content");
                //var tab = (Gecko.DOM.GeckoXULElement)tabbrowser.GetProperty<nsIDOMNode>("selectedBrowser").Wrap(GeckoNode.Create);
                //var SelectedContentDocument = tab.GetProperty<nsIDOMDocument>("contentDocument").Wrap(GeckoDocument.Create);
                //try
                //{
                //    var head = SelectedContentDocument.GetElementsByTagName("head")[0];
                //    var script = SelectedContentDocument.CreateElement("script");
                //    script.SetAttribute("src", "resource://gre/modules/commonjs/toolkit/loader.js");
                //    head.AppendChild(script);
                //}
                //catch { }

                //var head = SelectedContentDocument.GetElementsByTagName("head");
                //SelectedContentDocument.
                //tab.Instance.get
                //var head = document.getElementsByTagName('head')[0],
                //script = document.createElement('script');

                //script.src = url;
                //head.appendChild(script);
            }
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

        #region nsIBaseWindow
        public void InitWindow(IntPtr parentNativeWindow, IntPtr parentWidget, int x, int y, int cx, int cy)
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void SetPositionDesktopPix(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void GetPosition(out int x, out int y)
        {
            throw new NotImplementedException();
        }

        public void GetSize(out int cx, out int cy)
        {
            throw new NotImplementedException();
        }

        public void SetPositionAndSize(int x, int y, int cx, int cy, uint flags)
        {
            throw new NotImplementedException();
        }

        public void GetPositionAndSize(out int x, out int y, out int cx, out int cy)
        {
            throw new NotImplementedException();
        }

        public void Repaint([MarshalAs(UnmanagedType.U1)] bool force)
        {
            throw new NotImplementedException();
        }

        public IntPtr GetParentWidgetAttribute()
        {
            throw new NotImplementedException();
        }

        public void SetParentWidgetAttribute(IntPtr value)
        {
            throw new NotImplementedException();
        }

        public IntPtr GetParentNativeWindowAttribute()
        {
            throw new NotImplementedException();
        }

        public void SetParentNativeWindowAttribute(IntPtr value)
        {
            throw new NotImplementedException();
        }

        public void GetNativeHandleAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetVisibilityAttribute()
        {
            throw new NotImplementedException();
        }

        public void SetVisibilityAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetEnabledAttribute()
        {
            throw new NotImplementedException();
        }

        public void SetEnabledAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            throw new NotImplementedException();
        }

        public IntPtr GetMainWidgetAttribute()
        {
            return WidgetHandle;
        }

        public double GetUnscaledDevicePixelsPerCSSPixelAttribute()
        {
            throw new NotImplementedException();
        }

        public double GetDevicePixelsPerDesktopPixelAttribute()
        {
            throw new NotImplementedException();
        }

        public void SetFocus()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))]
        public string GetTitleAttribute()
        {
            throw new NotImplementedException();
        }

        public void SetTitleAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region nsIInterfaceRequestor
        public IntPtr GetInterface(ref Guid uuid)
        {
            //if (_window != null)
            //{
            //    if (uuid == typeof(nsIDOMWindow).GUID || uuid == typeof(nsPIDOMWindowOuter).GUID)
            //    {
            //        IntPtr pType = Xpcom.QueryInterfaceForObject(_window.Instance, uuid);
            //        if (pType != IntPtr.Zero)
            //            return pType;
            //    }
            //}
            return IntPtr.Zero;
        }
        #endregion

        public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
        {
            //switch (aTopic)
            //{
            //    case "browser-delayed-startup-finished":
            //        using (var sessionStore = Xpcom.GetService2<Gecko.Interfaces.nsISessionStore>("@mozilla.org/browser/sessionstore;1"))
            //        {
            //            //var eventTarget = Xpcom.QueryInterface<nsIDOMEventListener>(sessionStore.Instance);

            //            var curstate = new nsAString();
            //            sessionStore.Instance.GetBrowserState(curstate);
            //            var curstateObj = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionstoreBrowserState>(curstate.ToString());

            //            var tabbrowser = (Gecko.DOM.GeckoXULElement)this.View.Document.GetElementById("content");
            //            var tab = tabbrowser.GetProperty<nsIDOMNode>("selectedBrowser").Wrap(GeckoNode.Create);
            //            var SelectedContentDocument = (tab as Gecko.DOM.GeckoXULElement).GetProperty<nsIDOMDocument>("contentDocument").Wrap(GeckoDocument.Create);

            //            try
            //            {
            //                //var thisWindowstate = new nsAString();
            //                //sessionStore.Instance.GetWindowState(_docshell.QueryInterface<nsIDOMWindow>(), thisWindowstate);
            //                //var thisWindowstateString = thisWindowstate.ToString();

            //                SessionstoreBrowserState thisWindow = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionstoreBrowserState>(curstate.ToString());
            //                thisWindow.selectedWindow = 1;
            //                thisWindow.windows[0] = new SessionstoreJsons_Windows();
            //                thisWindow.windows[0].busy = false;
            //                thisWindow.windows[0].selected = 1;
            //                thisWindow.windows[0].tabs = new SessionstoreJsons_Tabs[1];

            //                thisWindow.windows[0].tabs[0] = new SessionstoreJsons_Tabs();
            //                thisWindow.windows[0].tabs[0].entries = new SessionstoreJsons_Entries[1];
            //                thisWindow.windows[0].tabs[0].charset = SelectedContentDocument.CharacterSet;
            //                thisWindow.windows[0].tabs[0].hidden = "false";
            //                thisWindow.windows[0].tabs[0].index = "1";
            //                thisWindow.windows[0].tabs[0].userContextId = "0";
            //                thisWindow.windows[0].tabs[0].userTypedClear = "0";
            //                thisWindow.windows[0].tabs[0].lastAccessed = "";
            //                thisWindow.windows[0].tabs[0].iconLoadingPrincipal = "";
            //                thisWindow.windows[0].tabs[0].attributes = "";
            //                thisWindow.windows[0].tabs[0].image = "";
            //                thisWindow.windows[0].tabs[0].userTypedValue = "";

            //                thisWindow.windows[0].tabs[0].entries[0] = new SessionstoreJsons_Entries();
            //                thisWindow.windows[0].tabs[0].entries[0].charset = SelectedContentDocument.CharacterSet;
            //                thisWindow.windows[0].tabs[0].entries[0].docIdentifier = SelectedContentDocument.DocumentElement.NamespaceURI;
            //                thisWindow.windows[0].tabs[0].entries[0].docshellID = "" + _docshell.Instance.GetAppIdAttribute();
            //                thisWindow.windows[0].tabs[0].entries[0].ID = "1";
            //                thisWindow.windows[0].tabs[0].entries[0].persist = "true";
            //                thisWindow.windows[0].tabs[0].entries[0].principalToInherit_base64 = "";
            //                thisWindow.windows[0].tabs[0].entries[0].triggeringPrincipal_b64 = "";
            //                thisWindow.windows[0].tabs[0].entries[0].triggeringPrincipal_base64 = "";
            //                thisWindow.windows[0].tabs[0].entries[0].url = SelectedContentDocument.Location.Href;
                            
            //                var thisWindowString = Newtonsoft.Json.JsonConvert.SerializeObject(thisWindow);

            //                curstate = new nsAString(thisWindowString);
            //                sessionStore.Instance.SetBrowserState(curstate);



            //                curstate = new nsAString();
            //                sessionStore.Instance.GetBrowserState(curstate);
            //                var jsonStateString = curstate.ToString();
            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //        }
            //        break;

            //    default:
            //        break;
            //}
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
            if (_docshell != null)
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
    }

    /// <summary>
    /// {
    ///      "version": ["sessionrestore", 1],
    ///      "windows": [{
    ///                  tabs: [{
    ///                     entries: [{
    ///                     
    ///                         url: about: sessionstore,
    ///                         charset: ,
    ///                         ID: 3,
    ///                         docshellID: 195,
    ///                         triggeringPrincipal_b64: vQZuXxRvRHKDMXv9BbHtkAAAAAAAAAAAwAAAAAAAAEYAAAAA,
    ///                         principalToInherit_base64: vQZuXxRvRHKDMXv9BbHtkAAAAAAAAAAAwAAAAAAAAEYAAAAA,
    ///                         triggeringPrincipal_base64: SmIS26zLEdO3ZQBgsLbOywAAAAAAAAAAwAAAAAAAAEY = ,
    ///                         docIdentifier: 3,
    ///                         persist: true
    ///                          }],
    ///                             lastAccessed: 1511981487128,
    ///                             hidden: false,
    ///                             attributes: {},
    ///                             userContextId: 0,
    ///                             index: 1,
    ///                             userTypedValue: ,
    ///                             userTypedClear: 0,
    ///                             image: null,
    ///                             iconLoadingPrincipal: SmIS26zLEdO3ZQBgsLbOywAAAAAAAAAAwAAAAAAAAEY =
    ///                         }],
    ///                    selected: 1,
    ///                    _closedTabs: [],
    ///                    busy: false,
    ///                    width: 1858,
    ///                    height: 1139,
    ///                    screenX: 196,
    ///                    screenY: 121,
    ///                    sizemode: normal
    ///      }],
    ///      "selectedWindow": 1,
    ///      "_closedWindows": [],
    ///      "session": {
    ///          "lastUpdate": 1511984697880,
    ///          "startTime": 1511984689941,
    ///          "recentCrashes": 0
    ///      },
    ///      "global": {}
    /// }
    /// </summary>
    public class SessionstoreBrowserState
    {
        public object[] version { get; set; }
        public SessionstoreJsons_Windows[] windows { get; set; }
        public int selectedWindow { get; set; }
        public SessionstoreJsons_Windows[] _closedWindows { get; set; }
    }

    public class SessionstoreJsons_Windows
    {
        public SessionstoreJsons_Tabs[] tabs { get; set; }
        public int selected { get; set; }
        public SessionstoreJsons_Tabs[] _closedTabs { get; set; }
        public bool busy { get; set; }
        public bool width { get; set; }
        public bool height { get; set; }
        public bool screenX { get; set; }
        public bool screenY { get; set; }
        public bool sizemode { get; set; }
    }

    public class SessionstoreJsons_Tabs
    {
        public SessionstoreJsons_Entries[] entries { get; set; }
        public string lastAccessed { get; set; }
        public string hidden { get; set; }
        public string attributes { get; set; }
        public string charset { get; set; }
        public string userContextId { get; set; }
        public string index { get; set; }
        public string userTypedValue { get; set; }
        public string userTypedClear { get; set; }
        public string image { get; set; }
        public string iconLoadingPrincipal { get; set; }
    }

    public class SessionstoreJsons_Entries
    {
        public string url { get; set; }
        public string charset { get; set; }
        public string ID { get; set; }
        public string docshellID { get; set; }
        public string triggeringPrincipal_b64 { get; set; }
        public string principalToInherit_base64 { get; set; }
        public string triggeringPrincipal_base64 { get; set; }
        public string docIdentifier { get; set; }
        public string persist { get; set; }
    }

    public class SessionstoreJsons_Session
    {
        public int lastUpdate { get; set; }
        public int startTime { get; set; }
        public int recentCrashes { get; set; }
    }

    public class SessionstoreJsons_globale
    {

    }
}




