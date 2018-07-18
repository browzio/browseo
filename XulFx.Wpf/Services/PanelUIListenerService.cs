using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Net;
using Gecko.Security;
using System;
using System.Runtime.InteropServices;

namespace Gecko.Windows.Services
{
    public class PanelUIListenerService : nsIDOMEventListener
    {
        private GeckoDocument xulDocument;
        private GeckoWindow view;

        public PanelUIListenerService(GeckoWindow view, GeckoDocument document)
        {
            this.xulDocument = document;
            this.view = view;
        }

        public void HandleEvent([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent @event)
        {
            //var PanelUIpopup = xulDocument.GetElementById("PanelUI-menu-button") as GeckoXULElement;
            //using (var result = new nsAString(""))
            //{
            //    using (var name = new nsAString("-moz-image-region"))
            //    {
            //        PanelUIpopup.ComputedStyle.Instance.GetPropertyValue(name, result);
            //    }
            //}

            //var paneluiIMacroButton = xulDocument.GetElementById("paneluiIMacroButton") as GeckoXULElement;
            //paneluiIMacroButton.SetAttribute("style", "list-style-image: url(\"chrome://xulfx/skin/browseo.png\"); -moz-image-region: auto;");

            var args = Xpcom.QueryInterface<nsIDOMEvent>(@event).Wrap(GeckoDOMEventArgs.Create);

            switch (args.Type)
            {
                case "popupshown":
                    var fullscreenBtn = xulDocument.GetElementById("fullscreen-button") as GeckoXULElement;
                    fullscreenBtn.Hidden = true;
                    //var e = args.CurrentTarget.CastToGeckoElement();



                    ////PanelUI-contents
                    //var contents = xulDocument.GetElementById("PanelUI-contents") as GeckoXULElement;
                    //foreach (var node in contents.ChildNodes)
                    //{
                    //   var id = node.GetStringProperty("id");
                    //    if(id == "fullscreen-button")
                    //    {

                    //    }
                    //}


                    //var helper1 = Xpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
                    //mozIDOMWindowProxy window = view.QueryInterface<mozIDOMWindowProxy>();
                    //helper1.Init(window);

                    //var PanelUIopup = xulDocument.GetElementById("PanelUI-popup") as GeckoXULElement;
                    //PanelUIopup.Hidden = false;

                    //nsIDocShell docShell = view.QueryInterface<nsIDocShell>();
                    //if (docShell != null)
                    //{
                    //    nsICommandManager commandMan = null;
                    //    try
                    //    {
                    //        commandMan = Xpcom.QueryInterface<nsICommandManager>(docShell);
                    //        if (commandMan != null)
                    //        {
                    //            commandMan.DoCommand("", null, null);
                    //        }
                    //    }
                    //    catch (COMException e)
                    //    {
                    //        if (e.ErrorCode != GeckoError.NS_ERROR_FAILURE)
                    //            throw;
                    //    }
                    //    finally
                    //    {
                    //        Xpcom.FreeComObject(ref commandMan);
                    //        Xpcom.FreeComObject(ref docShell);
                    //    }
                    //}


                    // var openPopupCommand = xulDocument.GetElementById("View:FullScreen") as GeckoXULElement;
                    //  openPopupCommand.DoCommand();

                    break;

                case "command":
                    //var width = webView.Width;
                    //var height = webView.Height;
                    // webView.Dock = DockStyle.Top;


                    // webView.Width = width - 400;
                    //  webView.Height = height;


                    //var browserObj = xulDocument.GetElementById("browser") as GeckoXULElement;
                    //if (browserObj != null)
                    //{
                    //    browserObj.Width = "" + (browserObj.ClientWidth - 100);
                    //}

                    break;

                default:
                    break;
            }
        }
    }
}
