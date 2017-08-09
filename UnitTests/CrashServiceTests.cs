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
	public class CrashServiceTests : TestsBase
	{
		[Test]
		public void CrashService_AddCrash()
		{
			var crashService = Xpcom.GetService<nsICrashService>(Contracts.CrashService);
			Assert.Throws<COMException>(() => crashService.AddCrash(nsICrashServiceConsts.PROCESS_TYPE_MAIN, nsICrashServiceConsts.CRASH_TYPE_CRASH, new nsAString("7ABD18DC-B361-4A47-84D9-596A8C4DC861")));
			Xpcom.FreeComObject(ref crashService);
		}
	}
}
