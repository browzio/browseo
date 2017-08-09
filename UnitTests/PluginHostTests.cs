using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Gecko;
using Gecko.Plugins;

namespace UnitTests
{
	[TestFixture]
	public class PluginHostTests : TestsBase
	{
		[Test]
		public void GetPluginTags()
		{
			foreach (PluginTag plugin in PluginHost.GetService().GetPluginTags())
			{
				Assert.NotNull(plugin.Fullpath);
				Assert.NotNull(plugin.Name);
			}
		}

	}

}
