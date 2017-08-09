using System;
using System.Linq;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.DOM;
using Gecko.DOM.HTML;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows;

namespace Gecko.Windows
{
	public class ContextMenuUpdater : ContextMenuUpdaterBase
	{
		private ItemCollection _contextMenuItems;

		public ContextMenuUpdater()
		{

		}

		public override void Init(object contextMenu)
		{
			if (contextMenu == null)
				throw new ArgumentNullException("contextMenu");

			var menu = contextMenu as ContextMenu;
			if (menu == null)
				throw new ArgumentOutOfRangeException("contextMenu");

			_contextMenuItems = menu.Items;
		}

		protected override void HideAll()
		{
			foreach (Control item in _contextMenuItems)
			{
				var menuItem = item as MenuItem;
				if(menuItem != null)
				{
					menuItem.Visibility = Visibility.Collapsed;
					menuItem.IsEnabled = false;
				}
				else if(item is Separator)
				{
					item.Visibility = Visibility.Visible;
				}
			}
		}

		protected override void HideUnnecessarySeparators()
		{
			bool prevItemIsSeparator = true;
			foreach (Control item in _contextMenuItems)
			{
				if (item.Visibility == Visibility.Visible)
				{
					Separator sepa = item as Separator;
					bool isSeparator = sepa != null;
					if (isSeparator && prevItemIsSeparator)
					{
						sepa.Visibility = Visibility.Collapsed;
					}
					prevItemIsSeparator = isSeparator;
				}
			}
			if (prevItemIsSeparator)
			{
				Control lastItem = _contextMenuItems.Cast<Control>()
					.LastOrDefault(m => m.Visibility == Visibility.Visible);
				Separator sepa = lastItem as Separator;
				if (sepa != null)
					sepa.Visibility = Visibility.Collapsed;
			}
		}

		protected override void SetEnable(string key, bool enable)
		{
			foreach (Control item in _contextMenuItems.Cast<Control>().Where(c => c.Name == key))
			{
				var menuItem = item as MenuItem;
				if (menuItem != null)
					menuItem.IsEnabled = enable;
				item.Visibility = Visibility.Visible;
			}
		}

	}
}
