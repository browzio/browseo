using Gecko.DOM;
using Gecko.Net;
using System;

namespace Gecko
{
	public sealed class GeckoDocumentCompletedEventArgs : EventArgs
	{
		private readonly Uri _uri;
		private readonly GeckoWindow _window;
		private readonly GeckoRequest _request;

		public GeckoDocumentCompletedEventArgs(Uri uri, GeckoRequest request, GeckoWindow window)
		{
			_uri = uri;
			_window = window;
			_request = request;
		}

		public Uri Uri
		{
			get { return _uri; }
		}

		public GeckoWindow Window
		{
			get { return _window; }
		}

		public GeckoRequest Request
		{
			get { return _request; }
		}

	}
}
