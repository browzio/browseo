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


namespace UnitTests
{
	[TestFixture]
	public sealed class CookieManagerTests : TestsBase
	{
		private CookieManager _cookieMan;

		public override void BeforeEachTestSetup()
		{
			_cookieMan = null;
			base.BeforeEachTestSetup();
			_cookieMan = CookieManager.GetService();
		}

		public override void AfterEachTestTearDown()
		{
			if (_cookieMan != null)
			{
				((IDisposable)_cookieMan).Dispose();
			}
			base.AfterEachTestTearDown();
		}

		[Test]
		public void CookieManager_Add()
		{
			Assert.DoesNotThrow(() => { _cookieMan.Add("example.com", "/", "demo", "value", false, false, true, DateTime.Now.AddDays(1)); });
			Assert.GreaterOrEqual(_cookieMan.CountCookiesFromHost("example.com"), 1);
		}

		[Test]
		public void CookieManager_Remove()
		{
			Assert.DoesNotThrow(() => { _cookieMan.Add("example.com", "/", "demo", "value", false, false, true, DateTime.Now.AddDays(1)); });
			Assert.DoesNotThrow(() => { _cookieMan.Remove("example.com", "demo", "/", true); });
			Assert.AreEqual(0, _cookieMan.CountCookiesFromHost("example.com"));
		}

		[Test]
		public void CookieManager_AddGetVerify()
		{
			Assert.DoesNotThrow(() => { _cookieMan.Add("example.com", "/", "demo", "value", false, false, true, DateTime.Now.AddDays(1)); });
			Cookie cookie = _cookieMan.GetCookiesFromHost("example.com").FirstOrDefault();
			Assert.IsNotNull(cookie);
			Assert.IsTrue(_cookieMan.Contains(cookie));
		}

	}
}
