using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Gecko;
using Gecko.DOM.HTML;
using Gecko.DOM;
using Gecko.Interop;
using Gecko.Interfaces;

namespace UnitTests
{
	[TestFixture]
	public class WindowlessWebViewTests : TestsBase
	{
		private WindowlessWebView _view;

		public override void BeforeEachTestSetup()
		{
			_view = null;
			base.BeforeEachTestSetup();
			_view = new WindowlessWebView();
		}

		public override void AfterEachTestTearDown()
		{
			if (_view != null)
				_view.Dispose();
			_view = null;
			base.AfterEachTestTearDown();
		}

		[Test]
		public void CreateAndDisposeInstance()
		{
			Assert.IsNotNull(_view);
		}

		[Test]
		public void SetInstanceSize()
		{
			GeckoWindow window = _view.Window;
			Assert.AreEqual(0, window.InnerWidth);
			Assert.AreEqual(0, window.InnerHeight);

			Assert.DoesNotThrow(() => _view.SetSize(600, 400));

			var docShell = window.QueryInterface<nsIDocShell>();
			docShell.CreateAboutBlankContentViewer(null);
			Xpcom.FreeComObject(ref docShell);

			Assert.AreEqual(600, window.InnerWidth);
			Assert.AreEqual(400, window.InnerHeight);
		}

		[Test]
		public void CallGoHome()
		{
			Assert.DoesNotThrow(() => _view.GoHome());
		}

		[Test]
		public void CallNavigateToGoogle()
		{
			Assert.DoesNotThrow(() => _view.Navigate("http://google.com"));
			while(_view.IsBusy)
			{
				Xpcom.DoEvents(false);
			}
			Uri url = _view.Url;
			Assert.True(url != null && url.Host.Contains("google."));
		}


	}
}
