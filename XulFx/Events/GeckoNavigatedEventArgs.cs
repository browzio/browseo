using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Net;
using System;

namespace Gecko
{
	public class GeckoNavigatedEventArgs : EventArgs
	{
		private Uri _url;
		private GeckoRequest _request;
		private GeckoWindow _window;
		private bool _isTopLevel;
		private bool _isSameDocument;
		private bool _isErrorPage;

		public GeckoNavigatedEventArgs(Uri url, GeckoRequest request, GeckoWindow window, bool isTopLevel, bool sameDocument, bool errorPage)
		{
			_url = url;
			_request = request;
			_window = window;
			_isTopLevel = isTopLevel;
			_isSameDocument = sameDocument;
			_isErrorPage = errorPage;
		}

		public Uri Url
		{
			get { return _url; }
		}

		public string Name
		{
			get { return _request != null ? _request.Name : null; }
		}

		public GeckoRequest Request
		{
			get { return _request; }
		}

		public GeckoWindow Window
		{
			get { return _window; }
		}

		public bool IsTopLevel
		{
			get { return _isTopLevel; }
		}

		public bool IsSameDocument
		{
			get { return _isSameDocument; }
		}

		public bool IsErrorPage
		{
			get { return _isErrorPage; }
		}
	}
}
