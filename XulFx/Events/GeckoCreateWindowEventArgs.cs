using Gecko.DOM;
using System;
using System.ComponentModel;

namespace Gecko
{
	public class GeckoCreateWindowEventArgs : CancelEventArgs
	{
		public GeckoWindow Window { get; set; }

		public GeckoCreateWindowEventArgs()
		{

		}
	}
}
