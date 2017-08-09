using System;

namespace Gecko
{
	/// <summary>
	/// Provides data for the WebView.ProgressChanged event.
	/// </summary>
	public class GeckoProgressEventArgs
		: EventArgs
	{
		private readonly long _currentProgress;
		private readonly long _maximumProgress;
		private readonly long _realMaximumProgress;

		/// <summary>Creates a new instance of a <see cref="GeckoProgressEventArgs"/> object.</summary>
		public GeckoProgressEventArgs(long current, long max)
			: this(current, max, max)
		{

		}

		/// <summary>Creates a new instance of a <see cref="GeckoProgressEventArgs"/> object.</summary>
		public GeckoProgressEventArgs(long current, long max, long realMax)
		{
			_currentProgress = current;
			_maximumProgress = max;
			_realMaximumProgress = realMax;
        }

		/// <summary>
		/// Gets the number of bytes that have been downloaded.
		/// </summary>
		public long CurrentProgress
		{
			get { return _currentProgress; }
		}

		/// <summary>
		/// Gets the estimated number of bytes in the document being loaded.
		/// </summary>
		public long MaximumProgress
		{
			get { return _maximumProgress; }
		}

		/// <summary>
		/// Gets the total number of bytes in the document being loaded.
		/// </summary>
		public long RealMaximumProgress
		{
			get { return _realMaximumProgress; }
		}

	}
}
