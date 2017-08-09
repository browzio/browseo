using System;
using System.ComponentModel;

namespace Gecko
{
	public class GeckoHistoryPurgeEventArgs
		: CancelEventArgs
	{
		/// <summary>
		/// Gets the number of entries to be purged from the history.
		/// </summary>
		public readonly int Count;

		/// <summary>Creates a new instance of a <see cref="GeckoHistoryPurgeEventArgs"/> object.</summary>
		/// <param name="count"></param>
		public GeckoHistoryPurgeEventArgs(int count)
		{
			Count = count;
		}

	}
}
