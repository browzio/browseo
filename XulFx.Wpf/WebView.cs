using System;
using Gecko;
using System.Linq;
using System.Windows.Interop;
using System.Diagnostics;
using Gecko.Interfaces;
using Gecko.GUI;
using Gecko.DOM;
using Gecko.Interop;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Gecko.DOM.XUL;
using Gecko.Javascript;
using System.Text;

namespace Gecko.Windows
{
    /// <summary>
    /// https://dxr.mozilla.org/mozilla-central/source/widget/windows/nsWindow.cpp#868
    /// </summary>
    public partial class WebView : HwndHost,
		IWebView,
		IKeyboardInputSink,
		nsIContextMenuListener2
	{
        #region WebView.Gecko.cs
        protected GeckoWebNavigation WebNav
        {
            get
            {
                return this.Window.QueryInterface<nsIWebNavigation>().Wrap(GeckoWebNavigation.Create);
            }
        }

        private void CreateContentView()
        {
            IntPtr hwndView = this.Handle;

            _contentView = GetOrCreateGlobalView("content", out _contentViewElement);
            //Debug.Assert(_contentView != null, "_contentView is null");
            if (_contentView == null)
                _contentView = _widget.View;

            nsIDocShell docShell = null;
            nsIWebProgress progress = null;
            try
            {
                docShell = _widget.View.QueryInterface<nsIDocShell>();

                progress = Xpcom.QueryInterface<nsIWebProgress>(docShell);
                progress.AddProgressListener(_wbGlue, (uint)(nsIWebProgressConsts.NOTIFY_STATE_ALL | nsIWebProgressConsts.NOTIFY_ALL));
            }
            finally
            {
                Xpcom.FreeComObject(ref progress);
                Xpcom.FreeComObject(ref docShell);
            }

            _eventTarget = _contentView.QueryInterface<nsIDocShell>().GetChromeEventHandlerAttribute().Wrap(GeckoDOMEventTarget.Create);
            if (_eventTarget != null)
            {
                foreach (string eventName in this.HandledDOMEvents)
                {
                    _eventTarget.AddEventListener(eventName, _wbGlue, true, true, 2);
                }
            }
        }

        protected void AssertNotPreview()
        {
            if (_previewMode)
                throw new InvalidOperationException();
        }

        #region IWebView

        [BrowsableAttribute(false)]
        public virtual GeckoWindow Window
        {
            get
            {
                if (this.Handle != IntPtr.Zero) // create control
                    return _contentView;
                return null;
            }
        }

        #endregion
        #endregion

        public Services.IWebviewListenerService WebviewListenerService { get; set; }

		private EventHandlerList _events;
		private HandleRef _parentHandle;
		private IntPtr _widgetHandle;
		private GeckoXULWindow _widget;
		private GeckoWindow _contentView;
        private WebBrowserGlue _wbGlue;
		private GeckoXULElement _contentViewElement;
		private GeckoDOMEventTarget _eventTarget;
		private bool _previewMode;
		private bool _canMoveFocus;



        public WebView()
        {
            _events = new EventHandlerList();
            //var ipl = System.Windows.Input.InputLanguageManager.GetInputLanguage(this);
            //System.Windows.Input.InputLanguageManager.SetInputLanguage(this, System.Globalization.CultureInfo.CurrentCulture);
            //System.Windows.Input.InputMethod.SetInputScope(this,new System.Windows.Input.InputScope());
            //int i = 0;
            //if (!DesignerProperties.GetIsInDesignMode(this))
            //{
            //             //if (DefaultContextMenu == null)
            //             //	DefaultContextMenu = CreateDefaultContextMenu();
            //             //this.ContextMenu = DefaultContextMenu;
            //         }
        }

		protected override void Dispose(bool disposing)
		{
			//this.ContextMenu = null;
			base.Dispose(disposing);
		}

		public IntPtr WidgetHandle
		{
			get
			{
				if (_widgetHandle == IntPtr.Zero)
				{
					GeckoXULWindow widget = _widget;
					if (widget == null)
						return IntPtr.Zero;
					_widgetHandle = widget.Instance.GetHandle();
				}
				return _widgetHandle; 
			}
		}

		public GeckoXULWindow Widget
		{
			get { return _widget; }
		}

		protected EventHandlerList Events
		{
			get { return _events; }
		}



        protected override HandleRef BuildWindowCore(System.Runtime.InteropServices.HandleRef hwndParent)
        {
            _parentHandle = hwndParent;
            //Xpcom.Initialize();
            _widget = new GeckoXULWindow();

            IntPtr hwndView = this.WidgetHandle;

            if (IntPtr.Size == 4)
            {
                //using (var kit = new nsAUTF8String())
                //{
                //    nsXULAppInfo.Instance.GetWidgetToolkitAttribute(kit);
                //}
                //var src = HwndSource.FromHwnd(hwndParent.Handle);

                Gecko.Windows.NativeMethods.SetWindowLong(hwndView, WinApi.GWL_EXSTYLE, WinApi.WS_EX_CONTROLPARENT);

                //Gecko.Windows.NativeMethods.SetWindowLong(hwndView, WinApi.GWL_STYLE, WinApi.WS_CAPTION | WinApi.WS_VISIBLE | WinApi.WS_CLIPSIBLINGS | WinApi.WS_CLIPCHILDREN | WinApi.WS_SYSMENU | WinApi.WS_THICKFRAME | WinApi.WS_OVERLAPPED);
                Gecko.Windows.NativeMethods.SetWindowLong(hwndView, WinApi.GWL_STYLE, WinApi.WS_TABSTOP | WinApi.WS_CHILD | WinApi.WS_CLIPCHILDREN | WinApi.WS_CLIPSIBLINGS);
            }
            else
            {
                Gecko.Windows.NativeMethods.SetWindowLongPtr(hwndView, WinApi.GWL_EXSTYLE, new IntPtr(WinApi.WS_EX_CONTROLPARENT));
                Gecko.Windows.NativeMethods.SetWindowLongPtr(hwndView, WinApi.GWL_STYLE, new IntPtr(WinApi.WS_TABSTOP | WinApi.WS_CHILD | WinApi.WS_CLIPCHILDREN | WinApi.WS_CLIPSIBLINGS));
            }
            Gecko.Windows.NativeMethods.SetParent(hwndView, hwndParent.Handle);
            _widget.SetPosition(0, 0);
            Gecko.Windows.NativeMethods.ShowWindow(hwndView, WinApi.SW_SHOWNA);

            //set window background
            //var result = NativeMethods.SetClassLong(hwndParent.Handle, NativeMethods.GCL_HBRBACKGROUND, NativeMethods.GetSysColorBrush(NativeMethods.COLOR_WINDOW));

            var href = new HandleRef(this, hwndView);

            WebviewListenerService.BuiltWindowCore(_widget);


            // var src = HwndSource.FromHwnd(hwndParent.Handle);
            //// KeyboardInputSite = 
            // var c = HwndSource.FromHwnd(HandleRef.ToIntPtr(href));
            return href;
        }

        protected override void DestroyWindowCore(System.Runtime.InteropServices.HandleRef hwnd)
		{
			Events.Dispose();
			_widget.Dispose();
			_widget = null;
		}

		protected override void OnGotFocus(RoutedEventArgs e)
		{
			base.OnGotFocus(e);
			if (_wbGlue != null)
			{
				GeckoWindow window = this.Window;
				if (window != null)
					_wbGlue.FocusMan.FocusedWindow = window;
				_wbGlue.Activate();
			}
				
		}
        
        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //lock (_wbGlue)
            //{
            // handled = false;
            if (_wbGlue == null)
            {
                _wbGlue = new WebBrowserGlue(this);
                _widget.Instance.SetXULBrowserWindowAttribute(_wbGlue);
                _wbGlue.ForceInitialBrowserNonRemote(null);
                _widget.WaitUntilChromeLoad();
                CreateContentView();

                _wbGlue.Activate();
                _wbGlue.Deactivate();

                WebviewListenerService.GloableViewLoadDone();

                _widget.AfterChromeLoaded();
            }

            switch (msg)
            {
                case WinApi.WM_KILLFOCUS:
                    if (_wbGlue.IsActive)
                        _wbGlue.Deactivate();
                    break;
                case WinApi.WM_MOUSEACTIVATE:
                    _wbGlue.Activate();
                    return new IntPtr(WinApi.MA_ACTIVATE);
                case WinApi.WM_ENABLE:
                    Gecko.Windows.NativeMethods.EnableWindow(_parentHandle.Handle, (long)wParam != 0L);
                    break;
                case WinApi.WM_CONTEXTMENU:
                    WmContextMenu(hwnd, wParam, lParam, ref handled);
                    return IntPtr.Zero;
            }
            var returned = base.WndProc(new IntPtr(WinApi.MA_ACTIVATE), msg, wParam, lParam, ref handled);
            return returned;

            //handled = false;
            //switch (msg)
            //{
            //    case WinApi.WM_KILLFOCUS:
            //        if (_wbGlue.IsActive)
            //            _wbGlue.Deactivate();
            //        break;

            //    case WinApi.WM_MOUSEACTIVATE:
            //        _wbGlue.Activate();
            //        handled = true;
            //        return new IntPtr(WinApi.MA_ACTIVATE);
            //        break;

            //    case WinApi.WM_ENABLE:
            //        Gecko.Windows.NativeMethods.EnableWindow(_parentHandle.Handle, (long)wParam != 0L);
            //        break;
            //    default:
            //        break;
            //}
            ////return IntPtr.Zero;

            //var returned = base.WndProc(hwnd, msg, wParam, lParam, ref handled);
            //return returned;

            //return base.WndProc(new IntPtr(WinApi.MA_ACTIVATE), msg, wParam, lParam, ref handled);
            //}
        }





        protected virtual void OnHandleDomEvent(GeckoDOMEventArgs e)
        {
            //Console.WriteLine("OnHandleDomEvent " + e.Type);
            _wbGlue.DispatchDOMEvent(e);
        }


        protected GeckoWindow GetOrCreateGlobalView(string viewName, out GeckoXULElement browser)
        {
            GeckoWindow viewWindow = Widget.View;
            GeckoDocument xulDocument = viewWindow.Document;
            //var doc = Xpcom.QueryInterface<nsIDOMDocument>(Widget.Instance.GetDocShellAttribute()).Wrap(GeckoDocument.Create);
            Debug.Assert(xulDocument != null, "xulDocument is null");
            Debug.Assert(xulDocument is GeckoXULDocument, "xulDocument is not GeckoXULDocument");
            //nsPIDOMWindowOuter
            //var o = viewWindow.QueryInterface<nsPIDOMWindowOuter>();
            // mozIDOMWindowProxy window = Xpcom.QueryInterface<mozIDOMWindowProxy>(o);
            // var helper = Xpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
            // helper.Init(window);
            // var docccc = helper.GetDocumentAttribute().Wrap(GeckoDocument.Create);
            // wait document loading
            while (xulDocument.DocumentElement == null)
                Xpcom.DoEvents(false);


            WebviewListenerService.GloableBrowserDocumentLoaded();


            browser = (GeckoXULElement)xulDocument.GetElementById("content");
            browser.SetAttribute("autoscroll", "false");
            //if (browser != null)
            //{
            return browser.GetProperty<nsIDOMWindow>("docShell").Wrap(GeckoWindow.Create);
            // }

            //         #region custom create no xul
            //         GeckoWindow view;
            //         browser = (GeckoXULElement)xulDocument.CreateElementNS("http://www.mozilla.org/keymaster/gatekeeper/there.is.only.xul", "browser");
            //browser.Flex = "1";
            //browser.SetAttribute("id", viewName);
            //browser.SetAttribute("type", "content-targetable");
            //xulDocument.DocumentElement.AppendChild(browser);

            //GeckoJavascriptBridge js = GeckoJavascriptBridge.GetService();
            //nsIDocShell docShell = null;
            //try
            //{
            //	while (docShell == null)
            //	{
            //		using (Variant value = js.GetProperty(browser, "docShell"))
            //		{
            //			if (value.DataType == VariantDataType.Interface || value.DataType == VariantDataType.InterfaceIs)
            //			{
            //				object dsObj = value.ToObject();
            //				docShell = Xpcom.QueryInterface<nsIDocShell>(dsObj);
            //				Xpcom.FreeComObject(ref dsObj);
            //				if (docShell != null)
            //					break;
            //			}
            //		}
            //		Xpcom.DoEvents(false);
            //	}
            //	docShell.SetIsAppTabAttribute(true);
            //	view = Xpcom.QueryInterface<nsIDOMWindow>(docShell).Wrap(GeckoWindow.Create);
            //}
            //finally
            //{
            //	Xpcom.FreeComObject(ref docShell);
            //}

            //return view;
            //         #endregion
        }




        #region IKeyboardInputSi

        IKeyboardInputSite IKeyboardInputSink.KeyboardInputSite
        {
            get;
            set;
        }


        bool TranslateChar(ref MSG msg, ModifierKeys modifiers)
        {
            return false;
        }

        IKeyboardInputSite IKeyboardInputSink.RegisterKeyboardInputSink(IKeyboardInputSink sink)
        {
            throw new NotImplementedException();
        }

        bool IKeyboardInputSink.TranslateAccelerator(ref MSG msg, ModifierKeys modifiers)
        {
            return false;
            IntPtr hwndView = this.WidgetHandle;
            if (hwndView == IntPtr.Zero)
                return false;

            const int VK_TAB = 0x9;
            const int WM_KEYDOWN = 0x100;
            long wParam = (long)msg.wParam;

            if (msg.message == WM_KEYDOWN)
            {
                switch (wParam)
                {
                    case VK_TAB:
                        bool backward = modifiers == ModifierKeys.Shift;
                        try
                        {
                            GeckoElement element = this._wbGlue.FocusMan.Instance.MoveFocus(null, null,
                                backward ? (uint)nsIFocusManagerConsts.MOVEFOCUS_BACKWARD : (uint)nsIFocusManagerConsts.MOVEFOCUS_FORWARD,
                                (uint)nsIFocusManagerConsts.FLAG_BYKEY).Wrap(GeckoElement.Create);
                            if (element != null)
                            {
                                GeckoWindow window = this.Window;
                                if (window != null)
                                {
                                    GeckoDocument document = window.Document;
                                    if (document != null && element != document.DocumentElement)
                                        return true;
                                    else
                                        _canMoveFocus = false;
                                }
                            }
                        }
                        catch (System.Runtime.InteropServices.COMException)
                        {
                            this._wbGlue.Activate();
                            return true;
                        }
                        break;
                    default:

                        break;
                }
            }
          
           if (wParam >= 0x23 && wParam <= 0x28) // VK_END - VK_DOWN
            {
                NativeMethods.SendMessage(hwndView, msg.message, msg.wParam, msg.lParam);
                return true;
            }
            return false;
        }

        bool IKeyboardInputSink.TabInto(TraversalRequest request)
        {

            //if (!_previewMode)
            //{
            //	nsIDOMElement domElement;
            //	switch (request.FocusNavigationDirection)
            //	{
            //		case FocusNavigationDirection.First:
            //			_wbGlue.Activate();
            //			domElement = _wbGlue.FocusMan.Instance.MoveFocus(null, null, (uint)nsIFocusManagerConsts.MOVEFOCUS_FIRST, (uint)nsIFocusManagerConsts.FLAG_BYKEY);
            //			Xpcom.FreeComObject(ref domElement);
            //			return true;
            //		case FocusNavigationDirection.Last:
            //			_wbGlue.Activate();
            //			domElement = _wbGlue.FocusMan.Instance.MoveFocus(null, null, (uint)nsIFocusManagerConsts.MOVEFOCUS_LAST, (uint)nsIFocusManagerConsts.FLAG_BYKEY);
            //			Xpcom.FreeComObject(ref domElement);
            //			return true;
            //	}
            //}
            return false;
        }

        bool IKeyboardInputSink.OnMnemonic(ref MSG msg, ModifierKeys modifiers)
        {
            return false;
        }

        bool IKeyboardInputSink.TranslateChar(ref MSG msg, ModifierKeys modifiers)
        {
            return false;
        }

        bool IKeyboardInputSink.HasFocusWithin()
        {
            return _canMoveFocus;
        }

        #endregion

        #region nsIContextMenuListener2

        protected virtual void WmContextMenu(IntPtr hwnd, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (this.ContextMenu == null)
                return;

            int x, y;
            float cx, cy;
            Point clientPoint;
            if (lParam == new IntPtr(-1))
            {
                lParam = new IntPtr(NativeMethods.GetMessagePos());
            }


            x = WinApi.GetXLParam(lParam);
            y = WinApi.GetYLParam(lParam);
            clientPoint = this.PointFromScreen(new Point(x, y));

            GeckoDocument document = this.Document;
            if (document == null)
                return;

            GeckoElement element = document.GetRealElementFromPoint((float)clientPoint.X, (float)clientPoint.Y, out cx, out cy);
            if (element == null)
                return;

            System.Diagnostics.Debug.Print("element: {0}", element.TagName);
            document = element.OwnerDocument;
            if (document == null)
                return;

            GeckoWindow window = document.DefaultView;
            if (window == null)
                return;

            ModifierKeys keys = Keyboard.Modifiers;

            GeckoMouseEvent mouseEvent = document.CreateEvent<GeckoMouseEvent>("MouseEvent");
            mouseEvent.InitMouseEvent("contextmenu", true, true, window, 1,
                x, y, (int)cx, (int)cy,
                (keys & ModifierKeys.Control) == ModifierKeys.Control,
                (keys & ModifierKeys.Alt) == ModifierKeys.Alt,
                (keys & ModifierKeys.Shift) == ModifierKeys.Shift,
                false, (ushort)GeckoMouseButton.Right, null);

            if (element.DispatchEvent(mouseEvent))
            {
                var menuInfo = ContextMenuInfo.Create(mouseEvent, element);
                ((nsIContextMenuListener2)this).OnShowContextMenu(menuInfo.ContextFlags, menuInfo);
            }
        }

        private static ContextMenu CreateDefaultContextMenu()
		{
			IContextMenuHandler handler;
			if (Xpcom.IsContractIDRegistered(Contracts.XulfxContextMenuHandler))
				handler = Xpcom.CreateInstance<IContextMenuHandler>(Contracts.XulfxContextMenuHandler);
			else
				handler = new ContextMenuHandler();

			var menu = new ContextMenu();
			menu.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
			menu.AddHandler(MenuItem.ClickEvent, new RoutedEventHandler(handler.OnClick));

			ItemCollection menuItems = menu.Items;
			menuItems.Add(new MenuItem() { Header = "Back", Name = "cmd_back" });
			menuItems.Add(new MenuItem() { Header = "Forward", Name = "cmd_forward" });
			menuItems.Add(new MenuItem() { Header = "Reload", Name = "cmd_reload" });
			menuItems.Add(new Separator());
			menuItems.Add(new MenuItem() { Header = "Undo", Name = "cmd_undo" });
			menuItems.Add(new MenuItem() { Header = "Redo", Name = "cmd_redo" });
			menuItems.Add(new Separator());
			menuItems.Add(new MenuItem() { Header = "Cut", Name = "cmd_cut" });
			menuItems.Add(new MenuItem() { Header = "Copy", Name = "cmd_copy" });
			menuItems.Add(new MenuItem() { Header = "Paste", Name = "cmd_paste" });
			menuItems.Add(new MenuItem() { Header = "Delete", Name = "cmd_delete" });
			menuItems.Add(new Separator());
			menuItems.Add(new MenuItem() { Header = "Copy Link Location", Name = "cmd_copyLink" });
			menuItems.Add(new Separator());
			menuItems.Add(new MenuItem() { Header = "Copy Image Location", Name = "cmd_copyImageLocation" });
			menuItems.Add(new MenuItem() { Header = "Copy Image Contents", Name = "cmd_copyImageContents" });
			menuItems.Add(new Separator());
			menuItems.Add(new MenuItem() { Header = "Select All", Name = "cmd_selectAll" });
			menuItems.Add(new Separator());
			menuItems.Add(new MenuItem() { Header = "View Source", Name = "cmd_viewSource" });
			menuItems.Add(new MenuItem() { Header = "Page Properties", Name = "cmd_pageProperties" });

			return menu;
		}

		void nsIContextMenuListener2.OnShowContextMenu(uint aContextFlags, nsIContextMenuInfo info)
		{
			ContextMenu contextMenu = this.ContextMenu;
			if (contextMenu != null)
			{
				GeckoNode node = info.GetTargetNodeAttribute().Wrap(GeckoNode.Create);
				if (node == null)
					return;

				contextMenu.Tag = new GeckoNodeWeakReference(node);

				var mouseEvent = (GeckoMouseEvent)info.GetMouseEventAttribute().Wrap(GeckoEvent.Create);
				var xulDoc = Widget.View.Document as GeckoXULDocument;
				if (xulDoc != null)
					xulDoc.PopupNode = node;

				IContextMenuUpdater updater = Xpcom.IsContractIDRegistered(Contracts.XulfxContextMenuUpdater) ? Xpcom.CreateInstance<IContextMenuUpdater>(Contracts.XulfxContextMenuUpdater) : new ContextMenuUpdater();
				try
				{
					updater.Init(contextMenu);
					updater.Update(aContextFlags, info);
				}
				finally
				{
					Xpcom.FreeComObject(ref updater);
				}
				
				contextMenu.IsOpen = true;
			}
		}

		#endregion


	}
}

		//public void PrintPreview(GeckoWindow previewWindow)
		//{
		//	//GeckoXULElement previewViewElement;
		//	//GeckoWindow previewWindow = GetOrCreateGlobalView("_preview", out previewViewElement);

		//	using (var docShell = Xpcom.QueryInterface2<nsIDocShell>(previewWindow.Instance))
		//	{
		//		nsIWebBrowserPrint wbp = docShell.Instance.GetPrintPreviewAttribute();
		//		if (!_previewMode)
		//		{
		//			wbp.PrintPreview(wbp.GetGlobalPrintSettingsAttribute(), Window.MozWindowProxy, null);
		//			_previewMode = true;
		//			//_contentViewElement.Hidden = true;
		//		}
		//		else
		//		{
		//			wbp.ExitPrintPreview();
		//			_previewMode = false;
		//			//_contentViewElement.Hidden = false;
		//			//previewViewElement.ParentNode.RemoveChild(previewViewElement);
		//		}
		//	}

		//}
