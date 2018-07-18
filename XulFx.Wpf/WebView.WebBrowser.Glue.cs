using Gecko.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gecko.Windows
{
	partial class WebView
	{
		private sealed class WebBrowserGlue : WebBrowserGlueBase
		{
			private WebView _view;

			internal WebBrowserGlue(WebView view)
			{
				_view = view;
			}

			protected override GeckoWindow GlobalWindow
			{
				get { return _view._widget.View; }
			}

			protected override IWebView WebViewControl
			{
				get { return _view; }
			}

			public override void Activate()
			{
				_view._canMoveFocus = true;
				base.Activate();
			}

			public override void Deactivate()
			{
				_view._canMoveFocus = false;
				base.Deactivate();
			}

			protected override void SetStatusText(string statusText)
			{
				_view.StatusText = statusText;
				_view.OnStatusTextChanged(new RoutedEventArgs(WebView.StatusTextChangedEvent));
			}

			protected override void SetBusy(bool state)
			{
				_view.IsBusy = state;
			}

			protected override void RaiseNavigating(GeckoNavigatingEventArgs e)
			{
				_view.OnNavigating(e);
			}

			protected override void RaiseFrameNavigating(GeckoNavigatingEventArgs e)
			{
				_view.OnFrameNavigating(e);
			}

			protected override void RaiseRedirecting(GeckoNavigatingEventArgs e)
			{
				_view.OnRedirecting(e);
			}

			protected override void RaiseNavigated(GeckoNavigatedEventArgs e)
			{
				var webNav = _view.WebNav;
                if (webNav == null) return;//TODO ?

				_view.CanGoBack = webNav.Instance.GetCanGoBackAttribute();
				_view.CanGoForward = webNav.Instance.GetCanGoForwardAttribute();
				_view._dontCallNavigateOnUrlChanged = true;
				_view.Url = e.Url;
				_view._dontCallNavigateOnUrlChanged = false;

				_view.OnNavigated(e);
			}

			protected override void RaiseFrameNavigated(GeckoNavigatedEventArgs e)
			{
				_view.OnFrameNavigated(e);
			}

			protected override void RaiseNSSError(GeckoNSSErrorEventArgs e)
			{
				_view.OnNSSError(e);
			}

			protected override void RaiseDocumentCompleted(GeckoDocumentCompletedEventArgs e)
			{
				_view.OnDocumentCompleted(e);
			}

			protected override void RaiseProgressChanged(GeckoProgressEventArgs e)
			{
				//_view.OnProgressChanged(e);
			}

			protected override void RaiseDOMEvent(GeckoDOMEventArgs e)
			{
                //Console.WriteLine("RaiseDOMEvent " + e.Type);
                _view.OnHandleDomEvent(e);
			}

			internal void DispatchDOMEvent(GeckoDOMEventArgs e)
			{
                //Console.WriteLine("DispatchDOMEvent " + e.Type);
				switch (e.Type)
				{
                    case "pageshow":
                    
                        break;
                    case "keydown":
						_view.OnDomKeyDown((GeckoDOMKeyEventArgs)e);
						break;
					case "keyup":
						_view.OnDomKeyUp((GeckoDOMKeyEventArgs)e);
						break;
					case "keypress":
                        _view.OnDomKeyPress((GeckoDOMKeyEventArgs)e);
						break;
					case "mousedown":
						_view.OnDomMouseDown((GeckoDOMMouseEventArgs)e);
						break;
					case "mouseup":
						_view.OnDomMouseUp((GeckoDOMMouseEventArgs)e);
						break;
					case "mousemove":
						_view.OnDomMouseMove((GeckoDOMMouseEventArgs)e);
						break;
					case "mouseover":
						_view.OnDomMouseOver((GeckoDOMMouseEventArgs)e);
						break;
					case "mouseout":
						_view.OnDomMouseOut((GeckoDOMMouseEventArgs)e);
						break;
					case "click":
						_view.OnDomClick((GeckoDOMMouseEventArgs)e);
						break;
					case "dblclick":
						_view.OnDomDoubleClick((GeckoDOMMouseEventArgs)e);
						break;
					case "submit":
						_view.OnDomSubmit(e);
						break;
					case "compositionstart":
						_view.OnDomCompositionStart(e);
						break;
					case "compositionend":
						_view.OnDomCompositionEnd(e);
						break;
					case "contextmenu":
						_view.OnDomContextMenu((GeckoDOMMouseEventArgs)e);
						break;
					case "DOMMouseScroll":
						_view.OnDomMouseScroll((GeckoDOMMouseEventArgs)e);
						break;
					case "focus":
						_view.OnDomFocus(e);
						break;
					case "blur":
						_view.OnDomBlur(e);
						break;
					case "load":
						_view.OnLoad(e);
						break;
					case "DOMContentLoaded":
						_view.OnDOMContentLoaded(e);
						break;
					case "readystatechange":
						_view.OnReadyStateChange(e);
						break;
					case "change":
						_view.OnDomContentChanged(e);
						break;
					case "hashchange":
						_view.OnHashChange((GeckoDOMHashChangeEventArgs)e);
						break;
					case "dragstart":
						_view.OnDomDragStart((GeckoDOMDragEventArgs)e);
						break;
					case "dragenter":
						_view.OnDomDragEnter((GeckoDOMDragEventArgs)e);
						break;
					case "dragover":
						_view.OnDomDragOver((GeckoDOMDragEventArgs)e);
						break;
					case "dragleave":
						_view.OnDomDragLeave((GeckoDOMDragEventArgs)e);
						break;
					case "drag":
						_view.OnDomDrag((GeckoDOMDragEventArgs)e);
						break;
					case "drop":
						_view.OnDomDrop((GeckoDOMDragEventArgs)e);
						break;
					case "dragend":
						_view.OnDomDragEnd((GeckoDOMDragEventArgs)e);
						break;
					case "DOMTitleChanged":
						_view.OnDocumentTitleChanged(new RoutedEventArgs(WebView.DocumentTitleChangedEvent));
                        break;

                }

				////if (e is DomMessageEventArgs)
				////{
				////	Action<string> action;
				////	DomMessageEventArgs mea = (DomMessageEventArgs)e;
				////	if (_messageEventListeners.TryGetValue(e.Type, out action))
				////	{
				////		action.Invoke(mea.Message);
				////	}
				////}

				if (e != null && e.Cancelable && e.Handled)
					e.PreventDefault();
			}

			protected override void RaiseCreateWindow(GeckoCreateWindowEventArgs e)
			{
				_view.OnCreateWindow(e);
			}
		}
    }
}
