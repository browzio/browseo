using System;
using Gecko;
using Gecko.Windows;

namespace TestApp
{
	class MyContextMenuHandler : ContextMenuHandler
	{
		protected override void ExecuteCommand(GeckoNode node, string command)
		{
			Console.WriteLine("click: {0}", command);
			base.ExecuteCommand(node, command);
		}
	}
}
