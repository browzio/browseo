using Gecko;
using NUnit.Framework;
using System;
using System.IO;

namespace UnitTests
{
	public class TestsBase
	{
		public static void Initialize()
		{
			if (!Xpcom.IsInitialized)
			{
				Xpcom.ProfilePath = Path.Combine(Path.GetDirectoryName(typeof(TestsBase).Assembly.Location), "profile");
				Xpcom.Initialize(TestsBase.XulRunnerLocation);
			}
		}

		public static string XulRunnerLocation
		{
			get
			{
				return XULRunnerLocator.GetXULRunnerLocation();
			}
		}

		[SetUp]
		public virtual void BeforeEachTestSetup()
		{
			TestsBase.Initialize();
		}

		[TearDown]
		public virtual void AfterEachTestTearDown()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
	}
}
