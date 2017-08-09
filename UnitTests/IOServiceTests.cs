using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Gecko;
using Gecko.Interfaces;
using Gecko.IO;

namespace UnitTests
{
	[TestFixture]
	public class IOServiceTests : TestsBase
	{

		[Test]
		public void AllowPort_TelnetPort_ReturnsUnsafe()
		{
			Assert.IsFalse(IOService.GetService().Instance.AllowPort(23, String.Empty));
		}

		[Test]
		public void GetOfflineAttribute_NotOffline_ReturnsFalse()
		{
			Assert.IsFalse(IOService.GetService().Offline);
		}

		[Test]
		public void GetProtocalFlags_CompareValidAndInvalidProtocoals_ProtocolFlagsDiffer()
		{
			IOService ioSvc = IOService.GetService();

			// Expect http to be non zero
			Assert.AreNotEqual(0, ioSvc.Instance.GetProtocolFlags("http"));
			// Expect http flag values to be different from invalid protocol
			Assert.AreNotEqual(ioSvc.Instance.GetProtocolFlags("http"), ioSvc.Instance.GetProtocolFlags("nonexistantprotocaol"));
		}

		[Test]
		public void GetProtocolHandler_HandlerForHttp_ReturnsValidHandlerInstance()
		{
			nsIProtocolHandler handler = IOService.GetService().Instance.GetProtocolHandler("http");
			Assert.IsNotNull(handler);
		}

		[Test]
		public void NewURI_BadUri_ThrowsMalformedUriException()
		{
			string referrer = "somejunk";
			Assert.Throws<COMException>(() => IOService.GetService().CreateNsIUri(referrer));
		}

		[Test]
		public void NewURI_ValidUri_ReturnsValidUriInstance()
		{
			string referrer = "http://www.google.co.uk";
			nsIURI result = IOService.GetService().CreateNsIUri(referrer);
		}

		[Test]
		//[Ignore("SetOfflineAttribute(true) hangs")]
		public void SetOfflineAttribute_SwitchingAttributeOnAndOff_ShouldChangeState()
		{
			IOService ioSvc = IOService.GetService();
			Assert.IsFalse(ioSvc.Offline);
			ioSvc.Offline = true;
			Assert.IsTrue(ioSvc.Offline);
			ioSvc.Offline = false;
			Assert.IsFalse(ioSvc.Offline);
		}

	}
}
