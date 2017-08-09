using System;

namespace Gecko
{
	/// <summary>Provides data for event.</summary>
	public class GeckoHistoryGotoIndexEventArgs
		: GeckoHistoryEventArgs
	{
		/// <summary>
		/// Gets the index in history of the document to be loaded.
		/// </summary>
		public readonly int Index;

		/// <summary>Creates a new instance of a <see cref="GeckoHistoryGotoIndexEventArgs"/> object.</summary>
		/// <param name="url"></param>
		/// <param name="index"></param>
		public GeckoHistoryGotoIndexEventArgs(Uri url, int index)
			: base(url)
		{
			Index = index;
		}
	}
}
