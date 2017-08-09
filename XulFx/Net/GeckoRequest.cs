using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.Net
{
	public class GeckoRequest : ComObject<nsIRequest>, IGeckoObjectWrapper
	{
		internal static GeckoRequest Create(nsIRequest instance)
		{
			return new GeckoRequest(instance);
		}

		protected GeckoRequest(nsIRequest instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Gets the name of the request. Often this is the URI of the request.
		/// </summary>
		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
		}

		/// <summary>
		/// Gets the error status associated with the request.
		/// </summary>
		public int Status
		{
			get { return Instance.GetStatusAttribute(); }
		}

		/// <summary>
		/// Indicates whether the request is pending. The value is true when there
		/// is an outstanding asynchronous event that will make the request no longer
		/// be pending. Requests do not necessarily start out pending; in some cases,
		/// requests have to be explicitly initiated (e.g. nsIChannel implementations
		/// are only pending once asyncOpen returns successfully).
		/// 
		/// Requests can become pending multiple times during their lifetime.
		/// </summary>
		/// <returns>
		/// Returns false if the request has reached completion (e.g., after
		/// OnStopRequest has fired).
		/// </returns>
		public bool IsPending
		{
			get { return Instance.IsPending(); }
		}

		/// <summary>
		/// Cancels the current request.  This will close any open input or
		/// output streams and terminate any async requests.  Users should
		/// normally pass NS_BINDING_ABORTED, although other errors may also
		/// be passed.  The error passed in will become the value of the
		/// status attribute.
		/// 
		/// Implementations must not send any notifications (e.g. via
		/// nsIRequestObserver) synchronously from this function. Similarly,
		/// removal from the load group (if any) must also happen asynchronously.
		/// 
		/// Requests that use nsIStreamListener must not call onDataAvailable
		/// anymore after cancel has been called.
		/// </summary>
		/// <param name="aStatus">
		/// the reason for canceling this request.
		/// 
		/// NOTE: most nsIRequest implementations expect aStatus to be a
		/// failure code; however, some implementations may allow aStatus to
		/// be a success code such as NS_OK.  In general, aStatus should be
		/// a failure code.
		/// </param>
		public void Cancel(int status)
		{
			Instance.Cancel(status);
		}

		/// <summary>
		/// Suspends the current request.  This may have the effect of closing
		/// any underlying transport (in order to free up resources), although
		/// any open streams remain logically opened and will continue delivering
		/// data when the transport is resumed.
		/// 
		/// Calling cancel() on a suspended request must not send any
		/// notifications (such as onstopRequest) until the request is resumed.
		/// 
		/// NOTE: some implementations are unable to immediately suspend, and
		/// may continue to deliver events already posted to an event queue. In
		/// general, callers should be capable of handling events even after
		/// suspending a request.
		/// </summary>
		public void Suspend()
		{
			Instance.Suspend();
		}

		/// <summary>
		/// Resumes the current request.  This may have the effect of re-opening
		/// any underlying transport and will resume the delivery of data to
		/// any open streams.
		public void Resume()
		{
			Instance.Resume();
		}


	}
}
