using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Gecko;
using System.Runtime.InteropServices;

namespace UnitTests
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Needed when single unittests are run
			// Xpcom.Initialize(XpComTests.XulRunnerLocation);
			//OverrideX11ErrorHandler();

			string prefix = Xpcom.IsLinux ? "--" : "/";

			var cargs = new List<string>();
			cargs.Add(Assembly.GetExecutingAssembly().Location);
			cargs.Add(prefix + "nothread");
			cargs.Add(prefix + "domain=None");

			if(args.Contains("verbose"))
				cargs.Add(prefix + "labels");

			int returnCode = NUnit.ConsoleRunner.Runner.Main(cargs.ToArray());

			if (returnCode != 0)
				Console.Beep();

			Xpcom.Shutdown();
		}

	}
}
