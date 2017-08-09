using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Net;
using Gecko.Security;
using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	public abstract class WebBrowserGlueBase :
		nsIXULBrowserWindow,
		nsIWebProgressListener2,
		nsISupportsWeakReference,
		nsIDOMEventListener,
		nsIInterfaceRequestor
	{
		private IntPtr _pUnknown;
		private GeckoWeakReference _weakRef;
		private long _maxProgressValue;
		private bool _inActivate;

		protected WebBrowserGlueBase()
		{
			_pUnknown = Marshal.GetIUnknownForObject(this);
			Marshal.Release(_pUnknown);
			this.FocusMan = Gecko.Services.FocusService.GetService();
		}

		protected abstract GeckoWindow GlobalWindow { get; }
		protected abstract IWebView WebViewControl { get; }
		protected abstract void SetStatusText(string statusText);
		protected abstract void SetBusy(bool state);
		protected abstract void RaiseDOMEvent(GeckoDOMEventArgs e);
		protected abstract void RaiseNavigating(GeckoNavigatingEventArgs e);
		protected abstract void RaiseFrameNavigating(GeckoNavigatingEventArgs e);
		protected abstract void RaiseFrameNavigated(GeckoNavigatedEventArgs e);
		protected abstract void RaiseRedirecting(GeckoNavigatingEventArgs e);
		protected abstract void RaiseNavigated(GeckoNavigatedEventArgs e);
		protected abstract void RaiseNSSError(GeckoNSSErrorEventArgs e);
		protected abstract void RaiseDocumentCompleted(GeckoDocumentCompletedEventArgs e);
		protected abstract void RaiseProgressChanged(GeckoProgressEventArgs e);
		protected abstract void RaiseCreateWindow(GeckoCreateWindowEventArgs e);

		public Gecko.Services.FocusService FocusMan { get; private set; }

		public virtual bool IsActive { get; protected set; }

		public virtual void Activate()
		{
			if (_inActivate)
				return;

			GeckoWindow globalView = this.GlobalWindow;
			if (globalView != null)
			{
				try
				{
					_inActivate = true;
					FocusMan.WindowRaised(globalView);
					this.IsActive = true;
				}
				finally
				{
					_inActivate = false;
				}
			}
		}

		public virtual void Deactivate()
		{
			GeckoWindow globalView = this.GlobalWindow;
			if (globalView != null)
			{
				FocusMan.WindowLowered(globalView);
				this.IsActive = false;
			}
		}

		public virtual nsIWebBrowserChrome CreateWindow(out bool cancel)
		{
			var ea = new GeckoCreateWindowEventArgs();
			RaiseCreateWindow(ea);
			cancel = ea.Cancel;
			if (cancel)
				return null;

			if (ea.Window == null)
				return null;

			return new WebBrowserChromeStub(ea.Window);
		}

		#region nsISupportsWeakReference

		public virtual nsIWeakReference GetWeakReference()
		{
			if (_weakRef == null)
				_weakRef = new GeckoWeakReference(this);
			return _weakRef;
		}

		#endregion nsISupportsWeakReference

		#region nsIInterfaceRequestor

		public virtual IntPtr GetInterface(ref Guid uuid)
		{
			Type TWebView = typeof(IWebView);
			IntPtr ppv;
			if (uuid == TWebView.GUID)
			{
				IWebView webView = this.WebViewControl;
				if (webView == null)
					throw new InvalidCastException();

				return Marshal.GetComInterfaceForObject(webView, TWebView);
			}
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(_pUnknown, ref uuid, out ppv));
			return ppv;
		}

		#endregion nsIInterfaceRequestor

		#region nsIWebProgressListener2

		public virtual void OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus)
		{
			#region validity checks
			// The request parametere may be null
			if (aRequest == null)
				return;

			// Ignore ViewSource requests, they don't provide the URL
			// see: http://mxr.mozilla.org/mozilla-central/source/netwerk/protocol/viewsource/nsViewSourceChannel.cpp#114
			{
				var viewSource = Xpcom.QueryInterface<nsIViewSourceChannel>(aRequest);
				if (viewSource != null)
				{
					Xpcom.ReleaseComObject(viewSource);
					return;
				}
			}

			#endregion validity checks
			
			#region request parameters

			GeckoWindow domWindow = null;
			try
			{
				// In some cases a nsIWebProgress instance may not have an associated DOM window, then an exception is thrown.
				domWindow = aWebProgress.GetDOMWindowAttribute().Wrap(GeckoWindow.Create);
			}
			catch (InvalidCastException)
			{
				return;
			}
			catch (COMException e)
			{
				if(e.ErrorCode == GeckoError.NS_ERROR_FAILURE)
					return;
				throw;
			}
			

			Uri destUri = null;
			Uri.TryCreate(nsString.Get(aRequest.GetNameAttribute), UriKind.Absolute, out destUri);

			/* This flag indicates that the state transition is for a request, which includes but is not limited to document requests.
			 * Other types of requests, such as requests for inline content (for example images and stylesheets) are considered normal requests.
			 */
			bool stateIsRequest = ((aStateFlags & nsIWebProgressListenerConsts.STATE_IS_REQUEST) != 0);

			/* This flag indicates that the state transition is for a document request. This flag is set in addition to STATE_IS_REQUEST.
			 * A document request supports the nsIChannel interface and its loadFlags attribute includes the nsIChannel ::LOAD_DOCUMENT_URI flag.
			 * A document request does not complete until all requests associated with the loading of its corresponding document have completed.
			 * This includes other document requests (for example corresponding to HTML <iframe> elements).
			 * The document corresponding to a document request is available via the DOMWindow attribute of onStateChange()'s aWebProgress parameter.
			 */
			bool stateIsDocument = ((aStateFlags & nsIWebProgressListenerConsts.STATE_IS_DOCUMENT) != 0);

			/* This flag indicates that the state transition corresponds to the start or stop of activity in the indicated nsIWebProgress instance.
			 * This flag is accompanied by either STATE_START or STATE_STOP, and it may be combined with other State Type Flags.
			 * 
			 * Unlike STATE_IS_WINDOW, this flag is only set when activity within the nsIWebProgress instance being observed starts or stops.
			 * If activity only occurs in a child nsIWebProgress instance, then this flag will be set to indicate the start and stop of that activity.
			 * For example, in the case of navigation within a single frame of a HTML frameset, a nsIWebProgressListener instance attached to the
			 * nsIWebProgress of the frameset window will receive onStateChange() calls with the STATE_IS_NETWORK flag set to indicate the start and
			 * stop of said navigation. In other words, an observer of an outer window can determine when activity, that may be constrained to a
			 * child window or set of child windows, starts and stops.
			 */
			bool stateIsNetwork = ((aStateFlags & nsIWebProgressListenerConsts.STATE_IS_NETWORK) != 0);

			/* This flag indicates that the state transition corresponds to the start or stop of activity in the indicated nsIWebProgress instance.
			 * This flag is accompanied by either STATE_START or STATE_STOP, and it may be combined with other State Type Flags.
			 * This flag is similar to STATE_IS_DOCUMENT. However, when a document request completes, two onStateChange() calls with STATE_STOP are generated.
			 * The document request is passed as aRequest to both calls. The first has STATE_IS_REQUEST and STATE_IS_DOCUMENT set, and the second has
			 * the STATE_IS_WINDOW flag set (and possibly the STATE_IS_NETWORK flag set as well -- see above for a description of when the STATE_IS_NETWORK
			 * flag may be set). This second STATE_STOP event may be useful as a way to partition the work that occurs when a document request completes.
			 */
			bool stateIsWindow = ((aStateFlags & nsIWebProgressListenerConsts.STATE_IS_WINDOW) != 0);
			#endregion request parameters

			#region STATE_START
			/* This flag indicates the start of a request.
			 * This flag is set when a request is initiated.
			 * The request is complete when onStateChange() is called for the same request with the STATE_STOP flag set.
			 */
			if ((aStateFlags & nsIWebProgressListenerConsts.STATE_START) != 0)
			{
				bool isTopLevelWindow = aWebProgress.GetIsTopLevelAttribute();
				if (stateIsNetwork && isTopLevelWindow)
				{
					_maxProgressValue = 100;
					SetBusy(true);

					GeckoRequest request = Xpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create);
					GeckoNavigatingEventArgs ea = new GeckoNavigatingEventArgs(destUri, request, domWindow);
					RaiseNavigating(ea);

					if (ea.Cancel)
					{
						aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
						//TODO: change the following handling of cancelled request

						// clear busy state
						SetBusy(false);

						// kill any cached document and raise DocumentCompleted event

						RaiseDocumentCompleted(new GeckoDocumentCompletedEventArgs(destUri, request, domWindow));

						// clear progress bar
						RaiseProgressChanged(new GeckoProgressEventArgs(_maxProgressValue, _maxProgressValue));

						// clear status bar
						SetStatusText(string.Empty);
					}
				}
				else if (stateIsDocument && !isTopLevelWindow)
				{
					GeckoNavigatingEventArgs ea = new GeckoNavigatingEventArgs(destUri, Xpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create), domWindow);
					RaiseFrameNavigating(ea);

					if (ea.Cancel)
					{
						// TODO: test it on Linux
						if (!Xpcom.IsLinux)
							aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
					}
				}
			}
			#endregion STATE_START

			#region STATE_REDIRECTING
			/* This flag indicates that a request is being redirected.
			 * The request passed to onStateChange() is the request that is being redirected.
			 * When a redirect occurs, a new request is generated automatically to process the new request.
			 * Expect a corresponding STATE_START event for the new request, and a STATE_STOP for the redirected request.
			 */
			else if ((aStateFlags & nsIWebProgressListenerConsts.STATE_REDIRECTING) != 0)
			{

				// make sure we're loading the top-level window
				var ea = new GeckoNavigatingEventArgs(destUri, Xpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create), domWindow);
				RaiseRedirecting(ea);

				if (ea.Cancel)
				{
					aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
				}
			}
			#endregion STATE_REDIRECTING

			#region STATE_TRANSFERRING
			/* This flag indicates that data for a request is being transferred to an end consumer.
			 * This flag indicates that the request has been targeted, and that the user may start seeing content corresponding to the request.
			 */
			//else if ((aStateFlags & nsIWebProgressListenerConstants.STATE_TRANSFERRING) != 0)
			//{
			//}
			#endregion STATE_TRANSFERRING

			#region STATE_STOP
			/* This flag indicates the completion of a request.
			 * The aStatus parameter to onStateChange() indicates the final status of the request.
			 */
			else if ((aStateFlags & nsIWebProgressListenerConsts.STATE_STOP) != 0)
			{
				/* aStatus
				 * Error status code associated with the state change.
				 * This parameter should be ignored unless aStateFlags includes the STATE_STOP bit.
				 * The status code indicates success or failure of the request associated with the state change.
				 * 
				 * Note: aStatus may be a success code even for server generated errors, such as the HTTP 404 File Not Found error.
				 * In such cases, the request itself should be queried for extended error information (for example for HTTP requests see nsIHttpChannel).
				 */

				if (stateIsNetwork)
				{
					// clear busy state
					SetBusy(false);

					// kill any cached document and raise DocumentCompleted event
					RaiseDocumentCompleted(new GeckoDocumentCompletedEventArgs(destUri, Xpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create), domWindow));

					// clear progress bar
					RaiseProgressChanged(new GeckoProgressEventArgs(_maxProgressValue, _maxProgressValue));

					// clear status bar
					SetStatusText(string.Empty);
				}

				if (stateIsRequest)
				{
					if ((aStatus & 0xff0000) == ((GeckoError.NS_ERROR_MODULE_SECURITY + GeckoError.NS_ERROR_MODULE_BASE_OFFSET) << 16))
					{
						SSLStatus sslStatus = null;
						nsIChannel aChannel = null;
						nsISupports aSecInfo = null;
						nsISSLStatusProvider aSslStatusProv = null;
						try
						{
							aChannel = Xpcom.QueryInterface<nsIChannel>(aRequest);
							if (aChannel != null)
							{
								aSecInfo = aChannel.GetSecurityInfoAttribute();
								if (aSecInfo != null)
								{
									aSslStatusProv = Xpcom.QueryInterface<nsISSLStatusProvider>(aSecInfo);
									if (aSslStatusProv != null)
									{
										sslStatus = aSslStatusProv.GetSSLStatusAttribute().Wrap(SSLStatus.Create);
									}
								}
							}
						}
						finally
						{
							Xpcom.FreeComObject(ref aChannel);
							Xpcom.FreeComObject(ref aSecInfo);
							Xpcom.FreeComObject(ref aSslStatusProv);
						}

						var ea = new GeckoNSSErrorEventArgs(destUri, aStatus, sslStatus);
						RaiseNSSError(ea);
						if (ea.Handled)
						{
							aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
						}
					}
				}
			}
			#endregion STATE_STOP

		}

		public virtual void OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress)
		{
			OnProgressChange64(aWebProgress, aRequest, aCurSelfProgress, aMaxSelfProgress, aCurTotalProgress, aMaxTotalProgress);
		}

		public virtual void OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint flags)
		{
			Uri uri;
			Uri.TryCreate(nsString.Get(aLocation.GetAsciiSpecAttribute), UriKind.Absolute, out uri);
			var domWindow = aWebProgress.GetDOMWindowAttribute().Wrap(GeckoWindow.Create);

			bool sameDocument = (flags & nsIWebProgressListenerConsts.LOCATION_CHANGE_SAME_DOCUMENT) == nsIWebProgressListenerConsts.LOCATION_CHANGE_SAME_DOCUMENT;
			bool errorPage = (flags & nsIWebProgressListenerConsts.LOCATION_CHANGE_ERROR_PAGE) == nsIWebProgressListenerConsts.LOCATION_CHANGE_ERROR_PAGE;
			bool isTopLevel = aWebProgress.GetIsTopLevelAttribute();
			var ea = new GeckoNavigatedEventArgs(uri, Xpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create), domWindow, isTopLevel, sameDocument, errorPage);

			if (isTopLevel)
				this.RaiseNavigated(ea);
			else
				this.RaiseFrameNavigated(ea);
		}

		public virtual void OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage)
		{
			SetStatusText(aMessage ?? string.Empty);
			//if (aWebProgress.GetIsLoadingDocumentAttribute())
			//{
			//	SetStatusText(aMessage ?? string.Empty);
			//}
		}

		public virtual void OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState)
		{

		}

		public virtual void OnProgressChange64(nsIWebProgress aWebProgress, nsIRequest aRequest, long aCurSelfProgress, long aMaxSelfProgress, long aCurTotalProgress, long aMaxTotalProgress)
		{
			if (aMaxTotalProgress < 0)
				return;

			while (aMaxTotalProgress > _maxProgressValue)
			{
				if (aMaxTotalProgress < 5000)
					_maxProgressValue = 10000;
				else if (aMaxTotalProgress < 50000)
					_maxProgressValue = 100000;
				else if (aMaxTotalProgress < 500000)
					_maxProgressValue = 1000000;
				else
					_maxProgressValue = (_maxProgressValue << 1);
			}

			RaiseProgressChanged(new GeckoProgressEventArgs(aCurTotalProgress, _maxProgressValue, aMaxTotalProgress));

		}

		public virtual bool OnRefreshAttempted(nsIWebProgress aWebProgress, nsIURI aRefreshURI, int aMillis, bool aSameURI)
		{
			return true;
		}

		#endregion nsIWebProgressListener2

		#region nsIXULBrowserWindow

		public virtual void SetJSStatus(nsAStringBase status)
		{

		}

		public virtual void SetOverLink(nsAStringBase link, nsIDOMElement element)
		{

		}

		public virtual void OnBeforeLinkTraversal(nsAStringBase originalTarget, nsIURI linkURI, nsIDOMNode linkNode, bool isAppTab, nsAStringBase retval)
		{
			retval.SetData(originalTarget.ToString());
		}

		public virtual void ShowTooltip(int x, int y, nsAStringBase tooltip, nsAStringBase direction)
		{

		}

		public virtual void HideTooltip()
		{

		}

		public nsITabParent ForceInitialBrowserRemote()
		{
			throw new NotImplementedException();
		}

		public bool ShouldLoadURI(nsIDocShell aDocShell, nsIURI aURI, nsIURI aReferrer)
		{
			return true;
		}

		public void ForceInitialBrowserNonRemote(mozIDOMWindowProxy openerWindow)
		{
			throw new NotImplementedException();
		}

		public uint GetTabCount()
		{
			throw new NotImplementedException();
		}

		#endregion nsIXULBrowserWindow

		#region nsIDOMEventListener

		public virtual void HandleEvent(nsIDOMEvent @event)
		{
			RaiseDOMEvent(Xpcom.QueryInterface<nsIDOMEvent>(@event).Wrap(GeckoDOMEventArgs.Create));
		}

		#endregion nsIDOMEventListener

	}
}
