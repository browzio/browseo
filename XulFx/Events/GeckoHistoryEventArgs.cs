using System;
using System.ComponentModel;

namespace Gecko
{
	public class GeckoHistoryEventArgs
		: CancelEventArgs
	{
		/// <summary>
		/// Gets the URL of the history entry.
		/// </summary>
		public readonly Uri Url;

		/// <summary>Creates a new instance of a <see cref="GeckoHistoryEventArgs"/> object.</summary>
		/// <param name="url"></param>
		public GeckoHistoryEventArgs(Uri url)
		{
			Url = url;
		}
	}
}
