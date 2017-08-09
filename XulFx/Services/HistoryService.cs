using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko
{
	/// <summary>
	/// Global history. It adds functions used by the basic browser like,
	/// marking pages as typed in the URL bar, and removing pages as from the history.
	/// </summary>
	public sealed class HistoryService : ComObject<nsIBrowserHistory>, IGeckoObjectWrapper
	{
		public static HistoryService GetService()
		{
			return Xpcom.GetService<nsIBrowserHistory>(Contracts.NavHistoryService).Wrap(HistoryService.Create);
		}

		private static HistoryService Create(nsIBrowserHistory instance)
		{
			return new HistoryService(instance);
		}

		private HistoryService(nsIBrowserHistory instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Removes all existing pages from global history.
		/// Visits are removed synchronously, but pages are expired asynchronously
		/// off the main-thread.
		/// </summary>
		public void RemoveAllPages()
		{
			Instance.RemovePagesByTimeframe(0, (ulong)Utils.ToSecondsSinceEpoch(DateTime.UtcNow.AddDays(1)) * 1000UL);
		}

		/// <summary>
		/// Removes a page from global history.
		/// </summary>
		public void RemovePage(Uri url)
		{
			if (url == null)
				throw new ArgumentNullException("url");
			if (!url.IsAbsoluteUri)
				throw new ArgumentOutOfRangeException("url");
			nsIURI uri = IOService.GetService().CreateNsIUri(url.AbsoluteUri);
			try
			{
				Instance.RemovePage(uri);
			}
			finally
			{
				Xpcom.FreeComObject(ref uri);
			}
		}

	}
}
