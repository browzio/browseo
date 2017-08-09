using System;
using System.Linq;
using System.Windows.Forms;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.DOM;
using Gecko.DOM.HTML;
using System.Runtime.InteropServices;

namespace Gecko.Windows
{
	public class ContextMenuUpdater : ContextMenuUpdaterBase
	{
		private ToolStripItemCollection _contextMenuItems;

		public ContextMenuUpdater()
		{

		}

		public override void Init(object contextMenu)
		{
			if (contextMenu == null)
				throw new ArgumentNullException("contextMenu");

			var menu = contextMenu as ContextMenuStrip;
			if (menu == null)
				throw new ArgumentOutOfRangeException("contextMenu");

			_contextMenuItems = menu.Items;
		}

		protected override void HideAll()
		{
			foreach(ToolStripItem item in _contextMenuItems)
			{
				item.Available = (item is ToolStripSeparator);
				item.Enabled = false;
			}
		}

		protected override void HideUnnecessarySeparators()
		{
			bool prevItemIsSeparator = true;
			foreach (ToolStripItem item in _contextMenuItems)
			{
				if (item.Available)
				{
					bool isSeparator = item is ToolStripSeparator;
					if (isSeparator && prevItemIsSeparator)
					{
						item.Available = false;
					}
					prevItemIsSeparator = isSeparator;
				}
			}
			if (prevItemIsSeparator)
			{
				ToolStripItem lastItem = _contextMenuItems.Cast<ToolStripItem>()
					.LastOrDefault(m => m.Available);
				if (lastItem is ToolStripSeparator)
					lastItem.Available = false;
			}
		}

		protected override void SetEnable(string key, bool enable)
		{
			foreach (ToolStripItem item in _contextMenuItems.Find(key, true))
			{
				item.Enabled = enable;
				item.Available = true;
			}
		}

	}
}
