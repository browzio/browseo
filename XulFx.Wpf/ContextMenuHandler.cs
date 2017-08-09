using Gecko.DOM;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Gecko.Windows
{
	public class ContextMenuHandler : ContextMenuHandlerBase
	{
		public override void OnClick(object sender, EventArgs e)
		{
			var ea = e as RoutedEventArgs;
			var menu = sender as ContextMenu;
			if (menu != null && ea != null)
			{
				var menuItem = ea.OriginalSource as MenuItem;
				if (menuItem == null)
					menuItem = ea.Source as MenuItem;
				if (menuItem == null)
					return;
			

				var weakRef = menu.Tag as GeckoNodeWeakReference;
				if (weakRef != null)
				{
					GeckoNode node = weakRef.Target;
					if (node != null)
					{
						ExecuteCommand(node, menuItem.Name);
					}
				}
			}
		}
	}
}
