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
	public sealed class GeckoWindowTests : TestsBase
	{
		private WindowlessWebView _view;

		private GeckoWindow Window
		{
			get { return _view != null ? _view.Window : null; }
		}

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
		public void WindowIsEquals()
		{
			Assert.AreEqual(Window.Window, Window.Window);
		}

		[Test]
		public void UtilsIsNotNull()
		{
			Assert.NotNull(Window.WindowUtils);
		}

		[Test]
		public void WindowtAttributeIsNotNull()
		{
			Assert.NotNull(Window.Window);
		}

		[Test]
		public void DocumenttAttributeIsNotNull()
		{
			Assert.NotNull(Window.Document);
		}

		[Test]
		public void GetAndSetOpenerIsValid()
		{
			Assert.IsNull(Window.Opener);
			using(var webView = new WindowlessWebView())
			{
				Window.Opener = webView.Window;
				Assert.IsNotNull(Window.Opener);
			}
		}

		[Test]
		public void ParentAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Parent);
		}

		[Test]
		public void ContentAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Content);
		}

		[Test]
		public void FrameElementAttributeIsNotNull()
		{
			GeckoHTMLDocument document = (GeckoHTMLDocument)Window.Document;
			GeckoHTMLIFrameElement iframe = (GeckoHTMLIFrameElement)document.CreateElement("iframe");
			document.Body.AppendChild(iframe);
			GeckoElement frameElement = iframe.ContentWindow.FrameElement;

			Assert.IsNull(Window.FrameElement);
			Assert.IsNotNull(frameElement);
			Assert.AreEqual(iframe, frameElement);
		}

		[Test]
		public void TopAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Top);
		}

		[Test]
		public void SelfAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Self);
		}

		[Test]
		public void TopAndSelfAreEquals()
		{
			Assert.AreEqual(Window.Self, Window.Top);
		}

		[Test]
		public void InnerWidthAttributeIsValid()
		{
			Assert.AreEqual(0, Window.InnerWidth);
		}

		[Test]
		public void InnerHeightAttributeIsValid()
		{
			Assert.AreEqual(0, Window.InnerHeight);
		}

		[Test]
		public void OuterWidthAttributeIsValid()
		{
			Assert.AreEqual(0, Window.OuterWidth);
		}

		[Test]
		public void OuterHeightAttributeIsValid()
		{
			Assert.AreEqual(0, Window.OuterHeight);
		}

		[Test]
		public void InnerScreenYAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.InnerScreenY);
		}

		[Test]
		public void InnerScreenXAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.InnerScreenX);
		}

		[Test]
		public void GetPageXOffsetAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.PageXOffset);
		}

		[Test]
		public void GetPageYOffsetAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.PageYOffset);
		}

		[Test]
		public void GetScreenXAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.ScreenX);
		}

		[Test]
		public void GetScreenYAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.ScreenY);
		}

		[Test]
		public void GetScrollXAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.ScrollX);
		}

		[Test]
		public void GetScrollYAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.ScrollY);
		}

		[Test]
		public void GetScrollMaxXAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.ScrollMaxX);
		}

		[Test]
		public void GetScrollMaxYAttributeDoesNotThrown()
		{
			Assert.AreEqual(0, Window.ScrollMaxY);
		}

		[Test]
		public void GetDevicePixelRatioAttributeIsValid()
		{
			Assert.False(float.IsNaN(Window.DevicePixelRatio));
		}

		[Test]
		public void NameAttributeIsValid()
		{
			string windowName = "window";
			Assert.IsNullOrEmpty(Window.Name);
			Window.Name = windowName;
			Assert.AreEqual(windowName, Window.Name);
			Assert.AreEqual(windowName, Window.Evaluate("window.name"));
		}

		[Test]
		public void FullScreenAttributeIsValid()
		{
			Assert.IsFalse(Window.FullScreen);
		}

		[Test]
		public void LengthAttributeIsValid()
		{
			Assert.AreEqual(0, Window.Length);

			GeckoHTMLDocument document = (GeckoHTMLDocument)Window.Document;
			document.Body.AppendChild(document.CreateElement("iframe"));

			Assert.AreEqual(1, Window.Length);
		}

		[Test]
		public void GetSelectionIsValid()
		{
			Assert.IsNotNull(Window.Selection);
		}

		[Test]
		public void NavigatorAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Navigator);
		}

		[Test]
		public void ScreenAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Screen);
		}

		[Test]
		public void LocationAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Location);
		}

		[Test]
		public void PerformanceAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Performance);
		}

		[Test]
		public void ClosedAttributeIsNotNull()
		{
			Assert.IsNotNull(Window.Closed);
		}

		[Test]
		public void StatusAttributeIsValid()
		{
			string statusString = "new status";
			Assert.IsEmpty(Window.Status);
			Window.Status = statusString;
			Assert.AreEqual(statusString, Window.Status);
		}


	}
}
