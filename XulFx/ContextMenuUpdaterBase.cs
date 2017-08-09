using System;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.DOM;
using Gecko.DOM.HTML;
using System.Runtime.InteropServices;

namespace Gecko
{
	public abstract class ContextMenuUpdaterBase : IContextMenuUpdater
	{
		public virtual void Update(uint aContextFlags, nsIContextMenuInfo menuInfo)
		{
			HideAll();
			Activate(aContextFlags, menuInfo);
			HideUnnecessarySeparators();
		}

		public abstract void Init(object contextMenu);

		protected virtual void Activate(uint aContextFlags, nsIContextMenuInfo menuInfo)
		{
			bool disableNavigation = false;
			var node = menuInfo.GetTargetNodeAttribute().Wrap(GeckoNode.Create) as GeckoNode;
			if (node == null)
				return;

			GeckoDocument document = node.OwnerDocument;
			if (document == null)
				return;

			GeckoWindow window = document.DefaultView;
			if (window == null)
				return;

			nsIDocShell docShell = window.QueryInterface<nsIDocShell>();
			if (docShell != null)
			{
				nsICommandManager commandMan = Xpcom.QueryInterface<nsICommandManager>(docShell);
				if (commandMan != null)
				{
					try
					{
						if ((aContextFlags & nsIContextMenuListener2Consts.CONTEXT_TEXT) == nsIContextMenuListener2Consts.CONTEXT_TEXT)
						{
							disableNavigation = true;
							SetEnable("cmd_undo", commandMan.IsCommandEnabled("cmd_undo", null));
							SetEnable("cmd_redo", commandMan.IsCommandEnabled("cmd_redo", null));
							SetEnable("cmd_delete", commandMan.IsCommandEnabled("cmd_delete", null));
							SetEnable("cmd_cut", commandMan.IsCommandEnabled("cmd_cut", null));
							SetEnable("cmd_copy", commandMan.IsCommandEnabled("cmd_copy", null));
							SetEnable("cmd_paste", commandMan.IsCommandEnabled("cmd_paste", null));
							var input = node as GeckoHTMLInputElement;
							var textarea = node as GeckoHTMLTextAreaElement;
							SetEnable("cmd_selectAll", (input != null && input.TextLength > 0) || (textarea != null && textarea.TextLength > 0));
						}
						if ((aContextFlags & nsIContextMenuListener2Consts.CONTEXT_IMAGE) == nsIContextMenuListener2Consts.CONTEXT_IMAGE)
						{
							disableNavigation = true;
							SetEnable("cmd_copyImageContents", commandMan.IsCommandEnabled("cmd_copyImageContents", null));
							SetEnable("cmd_copyImageLocation", commandMan.IsCommandEnabled("cmd_copyImageLocation", null));
						}
						if ((aContextFlags & nsIContextMenuListener2Consts.CONTEXT_LINK) == nsIContextMenuListener2Consts.CONTEXT_LINK)
						{
							disableNavigation = true;
							SetEnable("cmd_copyLink", commandMan.IsCommandEnabled("cmd_copyLink", null));
						}
						GeckoSelection selection = window.Selection;
						if (selection != null && (selection.RangeCount > 1 || (selection.RangeCount == 1 && !string.IsNullOrEmpty(selection.GetRangeAt(0).ToString()))))
						{
							SetEnable("cmd_copy", commandMan.IsCommandEnabled("cmd_copy", null));
						}
					}
					catch (COMException e)
					{
						if (e.ErrorCode != GeckoError.NS_ERROR_FAILURE)
							throw;
					}
					finally
					{
						Xpcom.FreeComObject(ref commandMan);
					}
				}
			}
			if (!disableNavigation)
			{
				nsIWebNavigation webNav = window.QueryInterface<nsIWebNavigation>();
				if(webNav != null)
				{
					try
					{
						SetEnable("cmd_back", webNav.GetCanGoBackAttribute());
						SetEnable("cmd_forward", webNav.GetCanGoForwardAttribute());
					}
					finally
					{
						Xpcom.FreeComObject(ref webNav);
					}
				}
				SetEnable("cmd_reload", true);
				SetEnable("cmd_viewSource", true);
				SetEnable("cmd_pageProperties", true);
			}
		}

		protected abstract void HideAll();

		protected abstract void HideUnnecessarySeparators();

		protected abstract void SetEnable(string key, bool enable);
	}
}
