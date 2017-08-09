using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Gecko;
using Gecko.Interop;
using Gecko.Interfaces;
using Gecko.Net;
using Gecko.DOM;
using Gecko.Events;


namespace UnitTests
{
	[TestFixture]
	public sealed class EventHelperTests : TestsBase
	{
		private ComObject<nsIXulfxEventHelper> _eventHelper;

		private WindowlessWebView _view;

		public override void BeforeEachTestSetup()
		{
			_view = null;
			_eventHelper = null;
			base.BeforeEachTestSetup();
			_view = new WindowlessWebView();
			_eventHelper = Xpcom.CreateInstance2<nsIXulfxEventHelper>(Contracts.XulfxEventHelper);
			GeckoWindow window = _view.Window;
			_eventHelper.Instance.Init(window.MozWindowProxy);
			GC.KeepAlive(window);
		}

		public override void AfterEachTestTearDown()
		{
			if (_eventHelper != null)
				((IDisposable)_eventHelper).Dispose();
			if (_view != null)
				_view.Dispose();
			_eventHelper = null;
			_view = null;
			base.AfterEachTestTearDown();
		}


		[Test]
		public void EventHelper_CreateEvent()
		{
			nsIDOMEvent aEvent = _eventHelper.Instance.CreateEvent(new nsAString("MouseEvent"), new nsAString("mousemove"), null);
			Assert.NotNull(aEvent);
			Xpcom.FreeComObject(ref aEvent);
		}

		[Test]
		public void EventHelper_CreateEvent2()
		{
			nsIDOMEvent aEvent = _eventHelper.Instance.CreateEvent2(new nsAString("MouseEvent"), new nsAString("mousemove"), null);
			Assert.NotNull(aEvent);
			Xpcom.FreeComObject(ref aEvent);
		}

		[Test]
		public void EventHelper_CreateEvent2WithInit()
		{
			using (var initArg = new XulfxEventInitDictionary())
			{
				initArg.Add("clientX", new Variant(10));
				initArg.Add("clientY", new Variant(15));
				nsIDOMEvent aEvent = _eventHelper.Instance.CreateEvent2(new nsAString("MouseEvent"), new nsAString("mousemove"), initArg);
				Assert.NotNull(aEvent);
				Xpcom.FreeComObject(ref aEvent);
			}
		}

	}
}
