using Gecko.DOM;
using Gecko.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	public abstract class ContextMenuHandlerBase : IContextMenuHandler
	{
		public abstract void OnClick(object sender, EventArgs e);

		protected virtual void ExecuteCommand(GeckoNode node, string command)
		{
			GeckoDocument document = node.OwnerDocument;
			if (document == null)
				return;

			GeckoWindow window = document.DefaultView;
			if (window == null)
				return;

			if (window != null)
			{
				switch (command)
				{
					case "cmd_undo":
					case "cmd_redo":
					case "cmd_cut":
					case "cmd_copy":
					case "cmd_paste":
					case "cmd_delete":
					case "cmd_copyLink":
					case "cmd_copyImageLocation":
					case "cmd_copyImageContents":
					case "cmd_selectAll":
					case "cmd_selectNone":
						ExecuteGeckoCommand(window, command);
						return;
					case "cmd_viewSource":
						window.Open("about:blank", null, null).Location.Assign("view-source:" + window.Document.Uri);
						return;
				}
				nsIWebNavigation webNav = window.QueryInterface<nsIWebNavigation>();
				if (webNav != null)
				{
					try
					{
						switch (command)
						{
							case "cmd_back":
								webNav.GoBack();
								return;
							case "cmd_forward":
								webNav.GoForward();
								return;
							case "cmd_reload":
								webNav.Reload((uint)GeckoLoadFlags.None);
								return;
						}
					}
					finally
					{
						Xpcom.FreeComObject(ref webNav);
					}
				}
			}
		}

		protected virtual void ExecuteGeckoCommand(GeckoWindow window, string command)
		{
			nsIDocShell docShell = window.QueryInterface<nsIDocShell>();
			if (docShell != null)
			{
				nsICommandManager commandMan = null;
				try
				{
					commandMan = Xpcom.QueryInterface<nsICommandManager>(docShell);
					if (commandMan != null)
					{
						commandMan.DoCommand(command, null, null);
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
					Xpcom.FreeComObject(ref docShell);
				}
			}
		}
	}
}
