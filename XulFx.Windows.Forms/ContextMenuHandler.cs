using System;
using System.Windows.Forms;
using Gecko.DOM;

namespace Gecko.Windows
{
	public class ContextMenuHandler : ContextMenuHandlerBase
	{
		public override void OnClick(object sender, EventArgs e)
		{
			var ea = e as ToolStripItemClickedEventArgs;
			var menu = sender as ContextMenuStrip;
			if (menu != null && ea != null)
			{
				var weakRef = menu.Tag as GeckoNodeWeakReference;
				if (weakRef != null)
				{
					GeckoNode node = weakRef.Target;
					if (node != null)
					{
						ExecuteCommand(node, ea.ClickedItem.Name);
					}
				}
			}
		}
	}
}
