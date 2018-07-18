using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gecko
{
	partial class WindowlessWebView
	{
		private sealed class WebBrowserGlue : WebBrowserGlueBase
		{
			private WeakReference _viewRef;

			private WindowlessWebView View
			{
				get
				{
					var view = _viewRef.Target as WindowlessWebView;
					if (view == null)
						throw new ObjectDisposedException(typeof(WindowlessWebView).Name);
					return view;
				}
			}


			internal WebBrowserGlue(WindowlessWebView view)
			{
				_viewRef = new WeakReference(view);
			}

			protected override GeckoWindow GlobalWindow
			{
				get { return View.Window; }
			}

			protected override IWebView WebViewControl
			{
				get { return View; }
			}

			protected override void SetStatusText(string statusText)
			{
				View.StatusText = statusText;
				View.OnStatusTextChanged(EventArgs.Empty);
			}

			protected override void SetBusy(bool state)
			{
				View.IsBusy = state;
			}

			protected override void RaiseNavigating(GeckoNavigatingEventArgs e)
			{
				View.OnNavigating(e);
			}

			protected override void RaiseFrameNavigating(GeckoNavigatingEventArgs e)
			{
				View.OnFrameNavigating(e);
			}

			protected override void RaiseRedirecting(GeckoNavigatingEventArgs e)
			{
				View.OnRedirecting(e);
			}

			protected override void RaiseNavigated(GeckoNavigatedEventArgs e)
			{
				View.OnNavigated(e);
			}

			protected override void RaiseFrameNavigated(GeckoNavigatedEventArgs e)
			{

			}

			protected override void RaiseNSSError(GeckoNSSErrorEventArgs e)
			{
				View.OnNSSError(e);
			}

			protected override void RaiseDocumentCompleted(GeckoDocumentCompletedEventArgs e)
			{
				View.OnDocumentCompleted(e);
			}

			protected override void RaiseProgressChanged(GeckoProgressEventArgs e)
			{
				View.OnProgressChanged(e);
			}

			protected override void RaiseDOMEvent(GeckoDOMEventArgs e)
			{
				View.OnHandleDomEvent(e);
			}

			internal void DispatchDOMEvent(GeckoDOMEventArgs e)
			{
				switch (e.Type)
				{
					case "keydown":
						View.OnDomKeyDown((GeckoDOMKeyEventArgs)e);
						break;
					case "keyup":
						View.OnDomKeyUp((GeckoDOMKeyEventArgs)e);
						break;
					case "keypress":
						View.OnDomKeyPress((GeckoDOMKeyEventArgs)e);
						break;
					case "mousedown":
						View.OnDomMouseDown((GeckoDOMMouseEventArgs)e);
						break;
					case "mouseup":
						View.OnDomMouseUp((GeckoDOMMouseEventArgs)e);
						break;
					case "mousemove":
						View.OnDomMouseMove((GeckoDOMMouseEventArgs)e);
						break;
					case "mouseover":
						View.OnDomMouseOver((GeckoDOMMouseEventArgs)e);
						break;
					case "mouseout":
						View.OnDomMouseOut((GeckoDOMMouseEventArgs)e);
						break;
					case "click":
						View.OnDomClick((GeckoDOMMouseEventArgs)e);
						break;
					case "dblclick":
						View.OnDomDoubleClick((GeckoDOMMouseEventArgs)e);
						break;
					case "submit":
						View.OnDomSubmit(e);
						break;
					case "compositionstart":
						View.OnDomCompositionStart(e);
						break;
					case "compositionend":
						View.OnDomCompositionEnd(e);
						break;
					case "contextmenu":
						View.OnDomContextMenu((GeckoDOMMouseEventArgs)e);
						break;
					case "DOMMouseScroll":
						View.OnDomMouseScroll((GeckoDOMMouseEventArgs)e);
						break;
					case "scroll":
						View.OnDomScroll((GeckoDOMUIEventArgs)e);
						break;
					case "wheel":
						View.OnDomWheel((GeckoDOMMouseEventArgs)e);
						break;
					case "focus":
						View.OnDomFocus(e);
						break;
					case "blur":
						View.OnDomBlur(e);
						break;
					case "load":
						View.OnLoad(e);
						break;
					case "DOMContentLoaded":
						View.OnDOMContentLoaded(e);
						break;
					case "readystatechange":
						View.OnReadyStateChange(e);
						break;
					case "change":
						View.OnDomContentChanged(e);
						break;
					case "hashchange":
						View.OnHashChange((GeckoDOMHashChangeEventArgs)e);
						break;
					case "dragstart":
						View.OnDomDragStart((GeckoDOMDragEventArgs)e);
						break;
					case "dragenter":
						View.OnDomDragEnter((GeckoDOMDragEventArgs)e);
						break;
					case "dragover":
						View.OnDomDragOver((GeckoDOMDragEventArgs)e);
						break;
					case "dragleave":
						View.OnDomDragLeave((GeckoDOMDragEventArgs)e);
						break;
					case "drag":
						View.OnDomDrag((GeckoDOMDragEventArgs)e);
						break;
					case "drop":
						View.OnDomDrop((GeckoDOMDragEventArgs)e);
						break;
					case "dragend":
						View.OnDomDragEnd((GeckoDOMDragEventArgs)e);
						break;
					case "DOMTitleChanged":
						View.OnDocumentTitleChanged(EventArgs.Empty);
						break;
					case "DOMWindowCreated":
						View.OnDOMWindowCreated(e);
						break;
					case "DOMWindowClose":
                        //WindowlessWebView view = this.View;
                        //view.OnWindowClose(e);
                        //if (view._disposing || !e.GetDefaultPrevented)
                        //{
                        //	view.IsDead = true;
                        //	view.OnWindowClosed(e);
                        //}
                        int i = 0;
						break;
				}

				//if (e is DomMessageEventArgs)
				//{
				//	Action<string> action;
				//	DomMessageEventArgs mea = (DomMessageEventArgs)e;
				//	if (_messageEventListeners.TryGetValue(e.Type, out action))
				//	{
				//		action.Invoke(mea.Message);
				//	}
				//}

				if (e != null && e.Cancelable && e.Handled)
					e.PreventDefault();
			}

			protected override void RaiseCreateWindow(GeckoCreateWindowEventArgs e)
			{
				View.OnCreateWindow(e);
			}

            public override void OnBeforeLinkTraversal(nsAStringBase originalTarget, nsIURI linkURI, nsIDOMNode linkNode, bool isAppTab, nsAStringBase retval)
            {
                retval.SetData(View.BeforeLinkTraversal(originalTarget.ToString(), linkURI, linkNode, isAppTab));
            }
        }

	}
}
