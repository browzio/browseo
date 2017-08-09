using System;
using Gecko;
using Gecko.Windows;
using System.Windows.Forms;

namespace TestApp
{
	class MyContextMenuUpdater : ContextMenuUpdater
	{
		internal const string MyMenuItemKey = "my_menu_item";

		public override void Init(object contextMenu)
		{
			base.Init(contextMenu);

			var contextMenuStrip = contextMenu as ContextMenuStrip;
			if (contextMenuStrip == null)
				return;

			ToolStripItemCollection contextMenuItems = contextMenuStrip.Items;
			if (!contextMenuItems.ContainsKey(MyMenuItemKey))
			{
				var menuItem = new ToolStripMenuItem("My menu item");
				menuItem.Name = MyMenuItemKey;
				contextMenuItems.Add(menuItem);
			}
		}

		protected override void Activate(uint aContextFlags, Gecko.Interfaces.nsIContextMenuInfo menuInfo)
		{
			base.Activate(aContextFlags, menuInfo);
			SetEnable(MyMenuItemKey, true);
		}
	}
}
