using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Gecko;
using Gecko.GUI;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Diagnostics;
using Gecko.DOM;
using Gecko.DOM.XUL;
using System.Runtime.InteropServices;
using Gecko.Javascript;
using Gecko.CustomMarshalers;

namespace Gecko.Windows
{
    public partial class WebView : Control,
		IEventedWebView,
		nsIContextMenuListener2
    {
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

            _contentView = GetOrCreateGlobalView("_contentView", out _contentViewElement);
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

        private static ContextMenuStrip DefaultContextMenu;
		private Uri _url;
		private GeckoXULWindow _widget;
		private GeckoWindow _contentView;
		private NativeView _nativeView;
		private WebBrowserGlue _wbGlue;
		private GeckoXULElement _contentViewElement;
		private GeckoDOMEventTarget _eventTarget;
		private bool _previewMode;

		public WebView()
		{
			if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
			{
				if (DefaultContextMenu == null)
					DefaultContextMenu = CreateDefaultContextMenu();
				base.ContextMenuStrip = DefaultContextMenu;
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.ContextMenuStrip = null;
			base.Dispose(disposing);
		}

		private GeckoXULWindow Widget
		{
			get { return _widget; }
		}

		protected WebBrowserGlueBase Glue
		{
			get { return _wbGlue; }
		}

		public virtual bool IsActive
		{
			get { return _wbGlue != null && _wbGlue.IsActive; }
		}

		#region Overridden Properties

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color BackColor
		{
			get { return base.BackColor; }
			set { base.BackColor = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get { return base.BackgroundImage; }
			set { base.BackgroundImage = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get { return base.BackgroundImageLayout; }
			set { base.BackgroundImageLayout = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Font Font
		{
			get { return base.Font; }
			set { base.Font = value; }
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.DesignMode)
			{
				string versionString =
					((AssemblyFileVersionAttribute)
					  Attribute.GetCustomAttribute(GetType().Assembly, typeof(AssemblyFileVersionAttribute))).Version;

				using (var brush = new HatchBrush(HatchStyle.SolidDiamond, Color.FromArgb(240, 240, 240), Color.White))
				{
					e.Graphics.FillRectangle(brush, this.ClientRectangle);
				}
				e.Graphics.DrawString(
					string.Format("XulFX v{0}\r\n" + "https://bitbucket.org/vmas/xulfx", versionString),
					SystemFonts.MessageBoxFont,
					Brushes.Black,
					new RectangleF(2, 2, this.Width - 4, this.Height - 4));
				e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, Width - 1, Height - 1);
			}
			base.OnPaint(e);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!this.DesignMode)
			{
				Xpcom.Initialize();
#if DEBUG
				try
				{
#endif
                    _wbGlue = new WebBrowserGlue(this);
					_widget = new GeckoXULWindow();
					_widget.WaitUntilChromeLoad();
					_widget.Instance.SetXULBrowserWindowAttribute(_wbGlue);

					IntPtr hwndView = _widget.Instance.GetHandle();

					if (IntPtr.Size == 4)
					{
						NativeMethods.SetWindowLong(hwndView, WinApi.GWL_EXSTYLE, WinApi.WS_EX_CONTROLPARENT);
						NativeMethods.SetWindowLong(hwndView, WinApi.GWL_STYLE, WinApi.WS_TABSTOP | WinApi.WS_CHILD | WinApi.WS_CLIPCHILDREN | WinApi.WS_CLIPSIBLINGS);
					}
					else
					{
						NativeMethods.SetWindowLongPtr(hwndView, WinApi.GWL_EXSTYLE, new IntPtr(WinApi.WS_EX_CONTROLPARENT));
						NativeMethods.SetWindowLongPtr(hwndView, WinApi.GWL_STYLE, new IntPtr(WinApi.WS_TABSTOP | WinApi.WS_CHILD | WinApi.WS_CLIPCHILDREN | WinApi.WS_CLIPSIBLINGS));
					}
					NativeMethods.SetParent(hwndView, this.Handle);
					_nativeView = new NativeView(this, hwndView);

					CreateContentView();

					OnResize(EventArgs.Empty);

					_widget.Show();
					NativeMethods.ShowWindow(hwndView, WinApi.SW_SHOWNA);


                    // Services.obs.notifyObservers(null, "lightweight-theme-changed", null);
#if DEBUG
                }
				catch (Exception ex)
				{
					if (Debugger.IsAttached)
						Debugger.Break();
					else
						MessageBox.Show(string.Format("{0}\r\n{1}", ex.GetType().Name, ex.StackTrace));
				}
#endif
			}
		}
		
		protected override void OnHandleDestroyed(EventArgs e)
		{	
			if (_eventTarget != null && _wbGlue != null)
			{
				//Remove Event Listener			
				foreach (string eventName in this.HandledDOMEvents)
				{
					_eventTarget.RemoveEventListener(eventName, _wbGlue, true);
				}
				((IDisposable)_eventTarget).Dispose();
				_eventTarget = null;
				_wbGlue = null;
			}
			if (_nativeView != null)
			{
				_nativeView.Dispose();
				_nativeView = null;
			}

			if(_widget != null)
			{
				_widget.Close();
				_widget.Dispose();
			}
			base.OnHandleDestroyed(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (_widget != null)
			{
				Size size = this.ClientSize;
				Widget.SetPositionAndSize(0, 0, size.Width, size.Height, true, false);
			}
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			switch (keyData & ~(Keys.Shift | Keys.Control | Keys.Alt))
			{
				case Keys.Left:
				case Keys.Down:
				case Keys.Right:
				case Keys.Up:
				case Keys.Home:
				case Keys.End:
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.Insert:
					return false;
			}

			if (!_previewMode)
			{
				GeckoMoveFocusType moveType = (keyData == (Keys.Tab | Keys.Shift) ? GeckoMoveFocusType.Backward : GeckoMoveFocusType.Forward);
				if (keyData == Keys.Tab || moveType == GeckoMoveFocusType.Backward)
				{
					_wbGlue.Activate();
					GeckoElement element = _wbGlue.FocusMan.MoveFocus(null, null, moveType, GeckoFocusFlags.ByKey);
					if (element != null)
					{
						GeckoWindow window = this.Window;
						if (window != null)
						{
							GeckoDocument document = window.Document;
							if (document != null && element != document.DocumentElement)
								return true;
						}
					}
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return false;
		}

        protected GeckoWindow GetOrCreateGlobalView(string viewName, out GeckoXULElement browser)
        {
            viewName = "content";

            GeckoWindow viewWindow = Widget.View;
            GeckoDocument xulDocument = viewWindow.Document;
            Debug.Assert(xulDocument != null, "xulDocument is null");
            Debug.Assert(xulDocument is GeckoXULDocument, "xulDocument is not GeckoXULDocument");

            // wait document loading
            while (xulDocument.DocumentElement == null)
                Xpcom.DoEvents(false);


            try
            {
                var mainWindow = xulDocument.GetElementById("main-window") as GeckoXULElement;
                if (mainWindow != null)
                {
                    //mainWindow.SetProperty("persist", "screenX screenY width height");
                    mainWindow.SetProperty("fullscreenbutton", "false");
                    mainWindow.SetProperty("sizemode", "maximized");
                    //mainWindow.SetProperty("hidechrome", "true");
                    //mainWindow.SetProperty("inactivetitlebarcolor", "green");
                }

                //titlebar-placeholder
                var titlebar = xulDocument.GetElementById("titlebar") as GeckoXULElement;
                if (titlebar != null)
                {
                    titlebar.Hidden = true;
                }


                //btns
                var btn1 = xulDocument.GetElementById("restore-button") as GeckoXULElement;
                if (btn1 != null)
                {
                    btn1.Hidden = true;
                }
                var btn2 = xulDocument.GetElementById("close-button") as GeckoXULElement;
                if (btn2 != null)
                {
                    btn2.Hidden = true;
                }
                var btn3 = xulDocument.GetElementById("minimize-button") as GeckoXULElement;
                if (btn3 != null)
                {
                    btn3.Hidden = true;
                }


                //open menu button PanelUI inside nav-bar PanelUI-popup PanelUI-button PanelUI-menu-button
                var PanelUIpopup = xulDocument.GetElementById("PanelUI-popup") as GeckoXULElement;
                //using(var result = new nsAString(""))
                //{
                //    using (var name = new nsAString("list-style-image"))
                //    {
                //        PanelUIpopup.ComputedStyle.Instance.GetPropertyPriority(name, result);
                //    }
                //}
                
                GeckoDOMEventTarget paneluiToolbarbuttonEventTarget =
                    Xpcom.QueryInterface<nsIDOMEventTarget>(PanelUIpopup.Instance).Wrap(GeckoDOMEventTarget.Create);
                paneluiToolbarbuttonEventTarget.AddEventListener("popupshown", new PanelUIListener(Widget.View, viewWindow.Document), true, true, 2);


                //PanelUI-button
                var paneluiIMacroItem = (GeckoXULElement)xulDocument.CreateElementNS("http://www.mozilla.org/keymaster/gatekeeper/there.is.only.xul", "toolbaritem");
                paneluiIMacroItem.SetAttribute("id", "paneluiIMacroItem");

                //PanelUI-menu-button
                var paneluiIMacroButton = (GeckoXULElement)xulDocument.CreateElementNS("http://www.mozilla.org/keymaster/gatekeeper/there.is.only.xul", "toolbarbutton");
                paneluiIMacroButton.SetAttribute("id", "paneluiIMacroButton");
                paneluiIMacroButton.SetAttribute("consumeanchor", "paneluiIMacroItem");
                paneluiIMacroButton.SetAttribute("tooltiptext", "Open IA Macros");
                paneluiIMacroButton.SetAttribute("class", "toolbarbutton-1 chromeclass-toolbar-additional");
                paneluiIMacroButton.SetAttribute("style", "list-style-image: url(\"chrome://xulfx/skin/browseo.png\"); -moz-image-region: auto;");
                //using (var name = new nsAString("list-style-image"))
                //{
                //    using (var value = new nsAString("url(\"chrome://browser/skin/Toolbar.png\")"))
                //    {
                //        using (var priority = new nsAString("1"))
                //        {
                //            paneluiIMacroButton.ComputedStyle.Instance.SetProperty(name, value, null);
                //        }
                //    }
                //}

                paneluiIMacroItem.AppendChild(paneluiIMacroButton);

                GeckoDOMEventTarget paneluiIMacroButtonEventTarget =
                    Xpcom.QueryInterface<nsIDOMEventTarget>(paneluiIMacroButton.Instance).Wrap(GeckoDOMEventTarget.Create);
                paneluiIMacroButtonEventTarget.AddEventListener("command", new PanelUIListener(Widget.View, viewWindow.Document,this), true, true, 2);
                
                var navBar = xulDocument.GetElementById("nav-bar") as GeckoXULElement;
                navBar.AppendChild(paneluiIMacroItem);

                //tab-view-deck
                var toolbar = xulDocument.GetElementById("toolbar-menubar") as GeckoXULElement;
                if (toolbar != null)
                {
                    toolbar.Hidden = true;
                }


                //var browserObj = xulDocument.GetElementById("browser") as GeckoXULElement;
                //if (browserObj != null)
                //{
                //    browserObj.Height = "" + browserObj.ClientHeight + browserObj.BoxObject.Y;
                //}



                //var xulDoc = Widget.View.Document as GeckoXULDocument;
                //if (xulDoc != null)
                //{
                //    var dispatchr = xulDoc.Instance.GetCommandDispatcherAttribute();
                //    var controller = dispatchr.GetControllers();
                //   var count =  controller.GetControllerCount();
                //    var oneController = controller.GetControllerAt(0);
                //    var isenabled = oneController.IsCommandEnabled("View:FullScreen");
                //}

                //var winddocShell = Xpcom.QueryInterface<nsICommandManager>(browserObj.Instance);
                //if (winddocShell != null)
                //{
                //    nsICommandManager commandMan = null;
                //    try
                //    {
                //        commandMan = Xpcom.QueryInterface<nsICommandManager>(winddocShell);
                //        if (commandMan != null)
                //        {
                //            commandMan.AddCommandObserver(new commandObserver(), "View:FullScreen");
                //        }
                //    }
                //    catch (COMException e)
                //    {
                //        if (e.ErrorCode != GeckoError.NS_ERROR_FAILURE)
                //            throw;
                //    }
                //    finally
                //    {
                //        Xpcom.FreeComObject(ref commandMan);
                //        Xpcom.FreeComObject(ref winddocShell);
                //    }
                //}
            }
            catch
            {

            }


            GeckoWindow view = null;
            browser = (GeckoXULElement)xulDocument.GetElementById(viewName);
            if (browser != null)
            {
                var window = browser.GetProperty<nsIDOMWindow>("docShell").Wrap(GeckoWindow.Create);
                return window;
            }

            browser = (GeckoXULElement)xulDocument.CreateElementNS("http://www.mozilla.org/keymaster/gatekeeper/there.is.only.xul", "browser");
            browser.Flex = "1";
            browser.SetAttribute("id", viewName);
            browser.SetAttribute("type", "content-targetable");
            xulDocument.DocumentElement.AppendChild(browser);

            GeckoJavascriptBridge js = GeckoJavascriptBridge.GetService();
            nsIDocShell docShell = null;
            try
            {
                while (docShell == null)
                {
                    using (Variant value = js.GetProperty(browser, "docShell"))
                    {
                        if (value.DataType == VariantDataType.Interface || value.DataType == VariantDataType.InterfaceIs)
                        {
                            object dsObj = value.ToObject();
                            docShell = Xpcom.QueryInterface<nsIDocShell>(dsObj);
                            Xpcom.FreeComObject(ref dsObj);
                            if (docShell != null)
                                break;
                        }
                    }
                    Xpcom.DoEvents(false);
                }
                docShell.SetIsAppTabAttribute(true);
                view = Xpcom.QueryInterface<nsIDOMWindow>(docShell).Wrap(GeckoWindow.Create);
            }
            finally
            {
                Xpcom.FreeComObject(ref docShell);
            }
            return view;
        }

        public class PanelUIListener : nsIDOMEventListener
        {
            private GeckoDocument xulDocument;
            private GeckoWindow view;
            private WebView webView;

            public PanelUIListener(GeckoWindow view, GeckoDocument document)
            {
                this.xulDocument = document;
                this.view = view;
            }

            public PanelUIListener(GeckoWindow view, GeckoDocument document, WebView webView) : this(view, document)
            {
                this.webView = webView;
            }

            public void HandleEvent([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent @event)
            {
                //var PanelUIpopup = xulDocument.GetElementById("PanelUI-menu-button") as GeckoXULElement;
                //using (var result = new nsAString(""))
                //{
                //    using (var name = new nsAString("-moz-image-region"))
                //    {
                //        PanelUIpopup.ComputedStyle.Instance.GetPropertyValue(name, result);
                //    }
                //}

                //var paneluiIMacroButton = xulDocument.GetElementById("paneluiIMacroButton") as GeckoXULElement;
                //paneluiIMacroButton.SetAttribute("style", "list-style-image: url(\"chrome://xulfx/skin/browseo.png\"); -moz-image-region: auto;");

                var args = Xpcom.QueryInterface<nsIDOMEvent>(@event).Wrap(GeckoDOMEventArgs.Create);

                switch (args.Type)
                {
                    case "popupshown":
                        var fullscreenBtn = xulDocument.GetElementById("fullscreen-button") as GeckoXULElement;
                        fullscreenBtn.Hidden = true;
                        //var e = args.CurrentTarget.CastToGeckoElement();



                        ////PanelUI-contents
                        //var contents = xulDocument.GetElementById("PanelUI-contents") as GeckoXULElement;
                        //foreach (var node in contents.ChildNodes)
                        //{
                        //   var id = node.GetStringProperty("id");
                        //    if(id == "fullscreen-button")
                        //    {

                        //    }
                        //}


                        //var helper1 = Xpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
                        //mozIDOMWindowProxy window = view.QueryInterface<mozIDOMWindowProxy>();
                        //helper1.Init(window);

                        //var PanelUIopup = xulDocument.GetElementById("PanelUI-popup") as GeckoXULElement;
                        //PanelUIopup.Hidden = false;

                        //nsIDocShell docShell = view.QueryInterface<nsIDocShell>();
                        //if (docShell != null)
                        //{
                        //    nsICommandManager commandMan = null;
                        //    try
                        //    {
                        //        commandMan = Xpcom.QueryInterface<nsICommandManager>(docShell);
                        //        if (commandMan != null)
                        //        {
                        //            commandMan.DoCommand("", null, null);
                        //        }
                        //    }
                        //    catch (COMException e)
                        //    {
                        //        if (e.ErrorCode != GeckoError.NS_ERROR_FAILURE)
                        //            throw;
                        //    }
                        //    finally
                        //    {
                        //        Xpcom.FreeComObject(ref commandMan);
                        //        Xpcom.FreeComObject(ref docShell);
                        //    }
                        //}


                        // var openPopupCommand = xulDocument.GetElementById("View:FullScreen") as GeckoXULElement;
                        //  openPopupCommand.DoCommand();

                        break;

                    case "command":
                        //var width = webView.Width;
                        //var height = webView.Height;
                        // webView.Dock = DockStyle.Top;


                        // webView.Width = width - 400;
                        //  webView.Height = height;


                        //var browserObj = xulDocument.GetElementById("browser") as GeckoXULElement;
                        //if (browserObj != null)
                        //{
                        //    browserObj.Width = "" + (browserObj.ClientWidth - 100);
                        //}

                        break;

                    default:
                        break;
                }
            }
        }


        protected virtual void OnHandleDomEvent(GeckoDOMEventArgs e)
		{
			_wbGlue.DispatchDOMEvent(e);
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == WinApi.WM_SETFOCUS)
			{
				if (_wbGlue != null)
				{
					_wbGlue.Activate();
				}
			}
		}

		protected virtual bool MozWndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WinApi.WM_SETFOCUS:
					_wbGlue.Activate();
					return false;
				case WinApi.WM_KILLFOCUS:
					if (this.Handle != m.WParam && !NativeMethods.IsChild(this.Handle, m.WParam))
					{
						_wbGlue.Deactivate();
					}
					return false;
				case WinApi.WM_MOUSEACTIVATE:
					m.Result = new IntPtr(WinApi.MA_ACTIVATE);
					NativeMethods.PostMessage(m.HWnd, WinApi.WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
					return false;
				case WinApi.WM_ENABLE:
					    NativeMethods.EnableWindow(this.FindForm().Handle, (long)m.WParam != 0L);
					return false;
				case WinApi.WM_WINDOWPOSCHANGING:
					bool fix = false;
					var pos = new WINDOWPOS();
					Marshal.PtrToStructure(m.LParam, pos);
					if (pos.x != 0 || pos.y != 0)
					{
						fix = true;
						pos.flags |= WinApi.SWP_NOMOVE;
					}
					Size size = this.ClientSize;
					if ((pos.cx ^ size.Width) != 0 || (pos.cy ^ size.Height) != 0)
					{
						fix = true;
						pos.flags |= WinApi.SWP_NOSIZE;
					}
					if (fix)
					{
						Marshal.StructureToPtr(pos, m.LParam, false);
						m.Result = new IntPtr(1);
						return true;
					}
					break;
				case WinApi.WM_CONTEXTMENU:
					WmContextMenu(ref m);
					return true;
			}
			return false;
		}

		protected virtual void WmContextMenu(ref Message m)
		{
			if (this.ContextMenuStrip == null && this.ContextMenu == null)
				return;

			nsIContextMenuListener menuListener = null;
			nsIContextMenuListener2 menuListener2 = this as nsIContextMenuListener2;
			if (menuListener2 == null)
			{
				menuListener = this as nsIContextMenuListener;
				if (menuListener == null)
					return;
			}

			int x, y;
			float cx, cy;
			Point clientPoint;
			IntPtr pos = m.LParam;
			if (pos == new IntPtr(-1))
			{
				pos = new IntPtr(NativeMethods.GetMessagePos());
			}


			x = WinApi.GetXLParam(pos);
			y = WinApi.GetYLParam(pos);
			clientPoint = this.PointToClient(new Point(x, y));

			GeckoDocument document = this.Document;
			if (document == null)
				return;

			GeckoElement element = document.GetRealElementFromPoint(clientPoint.X, clientPoint.Y, out cx, out cy);
			if (element == null)
				return;

			System.Diagnostics.Debug.Print("element: {0}", element.TagName);
			document = element.OwnerDocument;
			if (document == null)
				return;

			GeckoWindow window = document.DefaultView;
			if (window == null)
				return;

			Keys keys = Control.ModifierKeys;

			GeckoMouseEvent mouseEvent = document.CreateEvent<GeckoMouseEvent>("MouseEvent");
			mouseEvent.InitMouseEvent("contextmenu", true, true, window, 1,
				x, y, (int)cx, (int)cy,
				(keys & Keys.Control) == Keys.Control,
				(keys & Keys.Alt) == Keys.Alt,
				(keys & Keys.Shift) == Keys.Shift,
				false, (ushort)GeckoMouseButton.Right, null);

			if (!element.DispatchEvent(mouseEvent))
				return;

			var menuInfo = ContextMenuInfo.Create(mouseEvent, element);
			if (menuListener2 != null)
			{
				menuListener2.OnShowContextMenu(menuInfo.ContextFlags, menuInfo);
			}
			else// if(menuListener != null)
			{
				var node = element.QueryInterface<nsIDOMNode>();
				try
				{
					menuListener.OnShowContextMenu(menuInfo.ContextFlags, ((GeckoEvent)mouseEvent).Instance, node);
				}
				finally
				{
					Xpcom.FreeComObject(ref node);
				}
				GC.KeepAlive(mouseEvent);
			}
		}


		public void PrintPreview()
		{
			GeckoXULElement previewViewElement;
			GeckoWindow previewWindow = GetOrCreateGlobalView("_preview", out previewViewElement);

			using (var docShell = Xpcom.QueryInterface2<nsIDocShell>(previewWindow.Instance))
			{
				nsIWebBrowserPrint wbp = docShell.Instance.GetPrintPreviewAttribute();
				if (!_previewMode)
				{
					wbp.PrintPreview(wbp.GetGlobalPrintSettingsAttribute(), Window.MozWindowProxy, null);
					_previewMode = true;
					_contentViewElement.Hidden = true;
				}
				else
				{
					wbp.ExitPrintPreview();
					_previewMode = false;
					_contentViewElement.Hidden = false;
					previewViewElement.ParentNode.RemoveChild(previewViewElement);
				}
			}

		}

		#region Context menu

		private static ContextMenuStrip CreateDefaultContextMenu()
		{
			IContextMenuHandler handler;
			if (Xpcom.IsContractIDRegistered(Contracts.XulfxContextMenuHandler))
				handler = Xpcom.CreateInstance<IContextMenuHandler>(Contracts.XulfxContextMenuHandler);
			else
				handler = new ContextMenuHandler();

			var menu = new ContextMenuStrip();
			menu.ItemClicked += handler.OnClick;
			menu.Items.AddRange(new ToolStripItem[] {
				new ToolStripMenuItem("Back", null, null, "cmd_back"),
				new ToolStripMenuItem("Forward", null, null, "cmd_forward"),
				new ToolStripMenuItem("Reload", null, null, "cmd_reload"),
				new ToolStripSeparator(),
				new ToolStripMenuItem("Undo", null, null, Keys.Control | Keys.Z) { Name = "cmd_undo" },
				new ToolStripMenuItem("Redo", null, null, Keys.Control | Keys.Shift | Keys.Z) { Name = "cmd_redo" },
				new ToolStripSeparator(),
				new ToolStripMenuItem("Cut", null, null, Keys.Control | Keys.X) { Name = "cmd_cut" },
				new ToolStripMenuItem("Copy", null, null, Keys.Control | Keys.C) { Name = "cmd_copy" },
				new ToolStripMenuItem("Paste", null, null, Keys.Control | Keys.V) { Name = "cmd_paste" },
				new ToolStripMenuItem("Delete", null, null, "cmd_delete"),
				new ToolStripSeparator(),
				new ToolStripMenuItem("Copy Link Location", null, null, "cmd_copyLink"),
				new ToolStripSeparator(),
				new ToolStripMenuItem("Copy Image Location", null, null, "cmd_copyImageLocation"),
				new ToolStripMenuItem("Copy Image Contents", null, null, "cmd_copyImageContents"),
				new ToolStripSeparator(),
				new ToolStripMenuItem("Select All", null, null, Keys.Control | Keys.A) { Name = "cmd_selectAll" },
				new ToolStripSeparator(),
				new ToolStripMenuItem("View Source", null, null, "cmd_viewSource"),
				new ToolStripMenuItem("Page Properties", null, null, "cmd_pageProperties"),

			});
			return menu;
		}

		void nsIContextMenuListener2.OnShowContextMenu(uint aContextFlags, nsIContextMenuInfo info)
		{
			ContextMenuStrip contextMenu = this.ContextMenuStrip;
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
				contextMenu.Show(this, this.PointToClient(new Point(mouseEvent.ScreenX, mouseEvent.ScreenY)));
			}
		}

		#endregion

	}

}
