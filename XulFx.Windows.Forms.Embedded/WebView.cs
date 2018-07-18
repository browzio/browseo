using Gecko;
using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Gecko.Windows
{
	public partial class WebView : Control, IEventedWebView,
		nsIInterfaceRequestor,
		nsIWebBrowserChrome,
		nsIWebBrowserChromeFocus,
		nsIEmbeddingSiteWindow,
		nsIXULWindow,
		nsIContextMenuListener2
	{
		private static ContextMenuStrip DefaultContextMenu;
		private WebView _widget;
		private GeckoWindow _contentView;
		private nsIWebBrowser _webBrowser;
		private nsIBaseWindow _baseWindow;
		private uint _chromeFlags;
		private uint _contextFlags;
		private string _title;
		private Uri _url;

		private GeckoXULElement _contentViewElement;

		private bool _isModal;
		private bool _exitModalFlag;
		private WebBrowserGlue _wbGlue;
		private GeckoDOMEventTarget _eventTarget;
		private bool _previewMode;
		private bool _ignoreDestroyCall;

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

		private GeckoWindow View
		{
			get
			{
				return _contentView;
			}
		}

		private nsIWebBrowser WebBrowser
		{
			get { return _webBrowser; }
		}

		private nsIBaseWindow BaseWindow
		{
			get { return _baseWindow; }
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
					_widget = this;
					_wbGlue = new WebBrowserGlue(this);
					
					CreateContentView();

					//BaseWindow.SetVisibilityAttribute(true);

					OnResize(EventArgs.Empty);
					using (var windowMediator = Xpcom.GetService2<nsIWindowMediator>(Contracts.WindowMediator))
					{
						windowMediator.Instance.RegisterWindow(this);
					}
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
			if (_baseWindow != null)
			{
				//this.Stop();

				using (var windowMediator = Xpcom.GetService2<nsIWindowMediator>(Contracts.WindowMediator))
				{
					windowMediator.Instance.UnregisterWindow(this);
				}

				nsIDocShell docShell = Xpcom.QueryInterface<nsIDocShell>(_baseWindow);
				if (docShell != null && !docShell.IsBeingDestroyed())
				{
					try
					{
						var window = Xpcom.QueryInterface<nsIDOMWindow>(docShell).Wrap(GeckoWindow.Create);
						if (window != null)
						{
							try
							{
								_ignoreDestroyCall = true;
								if (!window.Closed) window.Close();
							}
							finally
							{
								_ignoreDestroyCall = false;
								Xpcom.FreeComObject(ref window);
							}
						}
					}
					finally
					{
						Xpcom.FreeComObject(ref docShell);
					}
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

				_baseWindow.Destroy();

				//Xpcom.FreeComObject(ref CommandParams);

				//var webBrowserFocus = this.WebBrowserFocus;
				//this.WebBrowserFocus = null;
				//Xpcom.FreeComObject(ref webBrowserFocus);
				//Xpcom.FreeComObject(ref WebNav);
				Xpcom.FreeComObject(ref _baseWindow);
				Xpcom.FreeComObject(ref _webBrowser);
			}

			base.OnHandleDestroyed(e);
		}

		protected override void DestroyHandle()
		{
			if (!_ignoreDestroyCall)
			{
				base.DestroyHandle();
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (_baseWindow != null)
			{
				BaseWindow.SetPositionAndSize(0, 0, Math.Max(ClientSize.Width, 1), Math.Max(ClientSize.Height, 1), nsIBaseWindowConsts.eRepaint);
			}
		}

		protected override void OnEnter(EventArgs e)
		{
			OnActivate(e);
			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e)
		{
			OnDeactivate(e);
			base.OnLeave(e);
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
					OnActivate(EventArgs.Empty);
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

		protected virtual void OnActivate(EventArgs e)
		{
			if (BaseWindow.GetEnabledAttribute())
			{
				_wbGlue.Activate();
				BaseWindow.SetFocus();
			}
		}

		protected virtual void OnDeactivate(EventArgs e)
		{
			_wbGlue.Deactivate();
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WinApi.WM_SETFOCUS:
					OnActivate(EventArgs.Empty);
					break;
				case WinApi.WM_KILLFOCUS:
					if (this.Handle != m.WParam && !NativeMethods.IsChild(this.Handle, m.WParam))
					{
						OnDeactivate(EventArgs.Empty);
					}
					break;
				case WinApi.WM_MOUSEACTIVATE:
					m.Result = new IntPtr(WinApi.MA_ACTIVATE);
					NativeMethods.PostMessage(m.HWnd, WinApi.WM_SETFOCUS, IntPtr.Zero, IntPtr.Zero);
					break;
				case WinApi.WM_ENABLE:
					bool enable = (long)m.WParam != 0L;
					BaseWindow.SetEnabledAttribute(enable);
					NativeMethods.EnableWindow(NativeMethods.GetAncestor(this.Handle, WinApi.GA_ROOT), enable);
					break;
			}

			// Firefox 17+ can crash when handing this windows message so we just ignore it.
			if (m.Msg == 0x128 /*WM_UPDATEUISTATE*/)
				return;

			base.WndProc(ref m);
		}

		protected GeckoWindow GetOrCreateGlobalView(string viewName, out GeckoXULElement browser)
		{
			browser = null;

			_webBrowser = Xpcom.CreateInstance<nsIWebBrowser>(Contracts.WebBrowser);
			var _webBrowserFocus = (nsIWebBrowserFocus)_webBrowser;
			_baseWindow = (nsIBaseWindow)_webBrowser;
			var _webNav = (nsIWebNavigation)_webBrowser;

			_webBrowser.SetContainerWindowAttribute(this);
			_baseWindow.InitWindow(this.Handle, IntPtr.Zero, 0, 0, this.Width, this.Height);
			_baseWindow.Create();
			
			var docShell = Xpcom.QueryInterface<nsIDocShell>(_baseWindow);			
			docShell.CreateAboutBlankContentViewer(null); // fix https://bugzilla.mozilla.org/show_bug.cgi?id=1138536 

			GeckoWindow contentView = _webBrowser.GetContentDOMWindowAttribute().Wrap(GeckoWindow.Create);
			GeckoDOMEventTarget eventTarget = contentView.WindowRoot;
			docShell.SetChromeEventHandlerAttribute(eventTarget.Instance);
			Xpcom.FreeComObject(ref docShell);
			GC.KeepAlive(eventTarget);

			_baseWindow.SetVisibilityAttribute(true);

			return contentView;

		}

		protected virtual void OnHandleDomEvent(GeckoDOMEventArgs e)
		{
			_wbGlue.DispatchDOMEvent(e);
		}

		public void PrintPreview()
		{
			throw new NotImplementedException();
		}








		#region nsIInterfaceRequestor Members

		IntPtr nsIInterfaceRequestor.GetInterface(ref Guid uuid)
		{
			object obj = this;

			// note: when a new window is created, gecko calls GetInterface on the webbrowser to get a DOMWindow in order
			// to set the starting url
			if (WebBrowser != null)
			{
				if (uuid == typeof(nsIDOMWindow).GUID)
				{
					obj = this.WebBrowser.GetContentDOMWindowAttribute();
				}
				else if (uuid == typeof(nsIDOMDocument).GUID)
				{
					obj = this.WebBrowser.GetContentDOMWindowAttribute().Wrap(GeckoWindow.Create).Document.QueryInterface<nsIDOMDocument>();
				}
			}

			IntPtr ppv, pUnk = Marshal.GetIUnknownForObject(obj);

			Marshal.QueryInterface(pUnk, ref uuid, out ppv);

			Marshal.Release(pUnk);

			return ppv;
		}

		#endregion

		#region nsIWebBrowserChrome

		void nsIWebBrowserChrome.SetStatus(uint statusType, string status)
		{
			this.StatusText = status;
			this.OnStatusTextChanged(EventArgs.Empty);
		}

		nsIWebBrowser nsIWebBrowserChrome.GetWebBrowserAttribute()
		{
			return _webBrowser;
		}

		void nsIWebBrowserChrome.SetWebBrowserAttribute(nsIWebBrowser aWebBrowser)
		{
			throw new NotSupportedException();
		}

		uint nsIWebBrowserChrome.GetChromeFlagsAttribute()
		{
			return _chromeFlags;
		}

		void nsIWebBrowserChrome.SetChromeFlagsAttribute(uint aChromeFlags)
		{
			_chromeFlags = (uint)aChromeFlags;
		}

		void nsIWebBrowserChrome.DestroyBrowserWindow()
		{
			//throw new NotImplementedException();
		}

		void nsIWebBrowserChrome.SizeBrowserTo(int aCX, int aCY)
		{
			throw new NotImplementedException();
		}

		void nsIWebBrowserChrome.ShowAsModal()
		{
			try
			{
				_isModal = true;
				NativeMethods.EnableWindow(FindForm().Handle, false);
				Xpcom.ModalEventLoop(() => !_exitModalFlag);
			}
			finally
			{
				_isModal = false;
				NativeMethods.EnableWindow(FindForm().Handle, true);
			}
		}

		bool nsIWebBrowserChrome.IsWindowModal()
		{
			return _isModal;
		}

		void nsIWebBrowserChrome.ExitModalEventLoop(int aStatus)
		{
			_exitModalFlag = true;
		}

		#endregion

		#region nsIEmbeddingSiteWindow

		void nsIEmbeddingSiteWindow.SetDimensions(uint flags, int x, int y, int cx, int cy)
		{
			const int DIM_FLAGS_POSITION = 1;
			const int DIM_FLAGS_SIZE_INNER = 2;
			const int DIM_FLAGS_SIZE_OUTER = 4;

			BoundsSpecified specified = 0;
			if ((flags & DIM_FLAGS_POSITION) != 0)
			{
				specified |= BoundsSpecified.Location;
			}
			if ((flags & DIM_FLAGS_SIZE_INNER) != 0 || (flags & DIM_FLAGS_SIZE_OUTER) != 0)
			{
				specified |= BoundsSpecified.Size;
			}

			//OnWindowSetBounds(new GeckoWindowSetBoundsEventArgs(new Rectangle(x, y, cx, cy), specified));
		}

		unsafe void nsIEmbeddingSiteWindow.GetDimensions(uint flags, int* x, int* y, int* cx, int* cy)
		{
			int localX = (x != (void*)0) ? *x : 0;
			int localY = (y != (void*)0) ? *y : 0;
			int localCX = 0;
			int localCY = 0;
			if (!IsDisposed)
			{
				if ((flags & nsIEmbeddingSiteWindowConsts.DIM_FLAGS_POSITION) != 0)
				{
					Point pt = PointToScreen(Point.Empty);
					localX = pt.X;
					localY = pt.Y;
				}

				if ((this._chromeFlags & (uint)nsIWebBrowserChromeConsts.CHROME_OPENAS_CHROME) == (uint)nsIWebBrowserChromeConsts.CHROME_OPENAS_CHROME)
				{
					BaseWindow.GetSize(out localCX, out localCY);
				}
				else
				{
					localCX = ClientSize.Width;
					localCY = ClientSize.Height;
				}

				if ((flags & nsIEmbeddingSiteWindowConsts.DIM_FLAGS_SIZE_INNER) == 0)
				{
					Control topLevel = TopLevelControl;
					if (topLevel != null)
					{
						Size nonClient = new Size(topLevel.Width - ClientSize.Width, topLevel.Height - ClientSize.Height);
						localCX += nonClient.Width;
						localCY += nonClient.Height;
					}
				}
			}
			if (x != (void*)0) *x = localX;
			if (y != (void*)0) *y = localY;
			if (cx != (void*)0) *cx = localCX;
			if (cy != (void*)0) *cy = localCY;
		}

		void nsIEmbeddingSiteWindow.SetFocus()
		{
			Focus();
			if (_baseWindow != null)
			{
				BaseWindow.SetFocus();
			}
		}

		bool nsIEmbeddingSiteWindow.GetVisibilityAttribute()
		{
			return this.Visible;
		}

		void nsIEmbeddingSiteWindow.SetVisibilityAttribute(bool aVisibility)
		{
			Visible = aVisibility;
		}

		string nsIEmbeddingSiteWindow.GetTitleAttribute()
		{
			return BaseWindow.GetTitleAttribute();
		}

		void nsIEmbeddingSiteWindow.SetTitleAttribute(string aTitle)
		{

		}

		IntPtr nsIEmbeddingSiteWindow.GetSiteWindowAttribute()
		{
			return Handle;
		}

		void nsIEmbeddingSiteWindow.Blur()
		{
			NativeMethods.SetFocus(IntPtr.Zero);
		}

		#endregion

		nsIDocShell nsIXULWindow.GetDocShellAttribute()
		{
			return Xpcom.QueryInterface<nsIDocShell>(this.WebBrowser);
		}

		bool nsIXULWindow.GetIntrinsicallySizedAttribute()
		{
			Debug.Print("GetIntrinsicallySizedAttribute was called");
			return false;
		}

		void nsIXULWindow.SetIntrinsicallySizedAttribute(bool aIntrinsicallySized)
		{
			Debug.Print("SetIntrinsicallySizedAttribute was called");
		}

		nsIDocShellTreeItem nsIXULWindow.GetPrimaryContentShellAttribute()
		{
			Debug.Print("GetPrimaryContentShellAttribute was called");
			return null;
		}

		nsIDocShellTreeItem nsIXULWindow.GetContentShellById(string ID)
		{
			Debug.Print("GetContentShellById was called");
			return null;
		}

		void nsIXULWindow.AddChildWindow(nsIXULWindow aChild)
		{

		}

		void nsIXULWindow.RemoveChildWindow(nsIXULWindow aChild)
		{

		}

		void nsIXULWindow.Center(nsIXULWindow aRelative, bool aScreen, bool aAlert)
		{
			throw new NotSupportedException();
		}

		void nsIXULWindow.ShowModal()
		{
			((nsIWebBrowserChrome)this).ShowAsModal();
		}

		uint nsIXULWindow.GetZLevelAttribute()
		{
			using (var windowMediator = Xpcom.GetService2<nsIWindowMediator>(Contracts.WindowMediator))
			{
				return windowMediator.Instance.GetZLevel(this);
			}
		}

		void nsIXULWindow.SetZLevelAttribute(uint aZLevel)
		{
			using (var windowMediator = Xpcom.GetService2<nsIWindowMediator>(Contracts.WindowMediator))
			{
				windowMediator.Instance.SetZLevel(this, aZLevel);
			}
		}

		uint nsIXULWindow.GetContextFlagsAttribute()
		{
			return _contextFlags;
		}

		void nsIXULWindow.SetContextFlagsAttribute(uint aContextFlags)
		{
			_contextFlags = aContextFlags;
		}

		uint nsIXULWindow.GetChromeFlagsAttribute()
		{
			return _chromeFlags;
		}

		void nsIXULWindow.SetChromeFlagsAttribute(uint aChromeFlags)
		{
			_chromeFlags = aChromeFlags;
		}

		void nsIXULWindow.AssumeChromeFlagsAreFrozen()
		{

		}

		nsIXULWindow nsIXULWindow.CreateNewWindow(int aChromeFlags, nsITabParent aOpeningTab, mozIDOMWindowProxy aOpener)
		{
			nsIXULWindow childWindow;
			using (var appShell = Xpcom.GetService2<nsIAppShellService>(Contracts.AppShellService))
			{
				if (appShell == null)
					throw Marshal.GetExceptionForHR(GeckoError.NS_ERROR_FAILURE);

				childWindow = appShell.Instance.CreateTopLevelWindow(this, null, (uint)aChromeFlags,
					(int)nsIAppShellServiceConsts.SIZE_TO_CONTENT, (int)nsIAppShellServiceConsts.SIZE_TO_CONTENT, aOpeningTab, aOpener);
			}
	
			if ((aChromeFlags & (int)nsIWebBrowserChromeConsts.CHROME_MODAL) == (int)nsIWebBrowserChromeConsts.CHROME_MODAL)
			{
				new ModalChildWindowListener(this, childWindow).Init();
			}
			return childWindow;
		}

		nsIXULBrowserWindow nsIXULWindow.GetXULBrowserWindowAttribute()
		{
			return _wbGlue;
		}

		void nsIXULWindow.SetXULBrowserWindowAttribute(nsIXULBrowserWindow aXULBrowserWindow)
		{
			throw new NotSupportedException();
		}

		void nsIXULWindow.ApplyChromeFlags()
		{

		}

		nsITabParent nsIXULWindow.GetPrimaryTabParentAttribute()
		{
			return null;
		}

		void nsIXULWindow.TabParentAdded(nsITabParent aTab, bool aPrimary)
		{

		}

		void nsIXULWindow.TabParentRemoved(nsITabParent aTab)
		{

		}

		void nsIWebBrowserChromeFocus.FocusNextElement(bool forDocumentNavigation)
		{
			nsIDOMElement domElement = _wbGlue.FocusMan.Instance.MoveFocus(null, null, nsIFocusManagerConsts.MOVEFOCUS_FORWARD, nsIFocusManagerConsts.FLAG_RAISE);
			Xpcom.FreeComObject(ref domElement);
		}

		void nsIWebBrowserChromeFocus.FocusPrevElement(bool forDocumentNavigation)
		{
			nsIDOMElement domElement = _wbGlue.FocusMan.Instance.MoveFocus(null, null, nsIFocusManagerConsts.MOVEFOCUS_BACKWARD, nsIFocusManagerConsts.FLAG_RAISE);
			Xpcom.FreeComObject(ref domElement);
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


        public void SizeShellToWithLimit(int aDesiredWidth, int aDesiredHeight, int shellItemWidth, int shellItemHeight)
        {
            
        }
    }
}
