using Gecko.DOM;
using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	[ComVisible(true)]
	[Guid("A29D41D7-7930-4A80-B907-5E1CC1C4643A")]
	public interface IWebView
	{
		
		bool IsBusy { [return: MarshalAs(UnmanagedType.U1)]get; }
		bool CanGoBack { [return: MarshalAs(UnmanagedType.U1)]get; }
		bool CanGoForward { [return: MarshalAs(UnmanagedType.U1)]get; }
		string StatusText { [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.WStringMarshaler))]get; }
		string DocumentTitle { [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.WStringMarshaler))]get; }
		Uri Url { get; set; }
		GeckoWindow Window { get; }
		GeckoDocument Document { get; }

		[return: MarshalAs(UnmanagedType.U1)]
		bool GoBack();
		[return: MarshalAs(UnmanagedType.U1)]
		bool GoForward();
		void GoHome();
		void GoSearch();
		void Navigate(Uri url);
		void Navigate([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.WStringMarshaler))] string urlString);
		void Reload();
		void Reload(GeckoLoadFlags flags);
		void Stop();
		
	}
}
