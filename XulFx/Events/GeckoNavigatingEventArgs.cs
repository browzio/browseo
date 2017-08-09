using Gecko.DOM;
using Gecko.Net;
using System;
using System.ComponentModel;

namespace Gecko
{
	public class GeckoNavigatingEventArgs
			: CancelEventArgs
	{
		private readonly Uri _uri;
		private readonly GeckoWindow _window;
		private readonly GeckoRequest _request;

		/// <summary>Creates a new instance of a <see cref="GeckoNavigatingEventArgs"/> object.</summary>
		/// <param name="value"></param>
		public GeckoNavigatingEventArgs(Uri uri, GeckoRequest request, GeckoWindow window)
		{
			_uri = uri;
			_window = window;
			_request = request;
		}

		public string Name
		{
			get { return _request != null ? _request.Name : null; }
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
