using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Net;
using Gecko.Security;
using System;
using System.Runtime.InteropServices;

namespace Gecko.GUI
{
    public enum PanelUIListenerStateOld
    {
        PopupWindow,
        IAMacro,
        AnonymitySettings,
        menuitem_Plugins,
        menuitem_Flash,
        menuitem_WebGL,
        menuitem_Javascript,
        menuitem_Java,
        menuitem_WebRtc,
        menuitem_DNT,
    }
    public class PanelUIListenerOld : nsIDOMEventListener
    {
        private GeckoDocument xulDocument;
        private GeckoWindow view;
        private PanelUIListenerStateOld state;

        public PanelUIListenerOld(GeckoWindow view, GeckoDocument document, PanelUIListenerStateOld state)
        {
            this.xulDocument = document;
            this.view = view;
            this.state = state;
        }

        public void HandleEvent([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent @event)
        {
            //var toolbars = xulDocument.GetElementsByTagName("toolbar");
            //int times = 0;
            //foreach (var tBar in toolbars)
            //{
            //    tBar.SetAttribute("style", "background-color: rgb(0,0,0) !important;");
            //    times++;
            //    if (times == 2) break;
            //}
            //return;
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
                case "dblclick":
                    args.StopPropagation();
                    args.StopImmediatePropagation();
                    args.PreventDefault();
                    break;
                case "popupshown":
                    switch (state)
                    {
                        case PanelUIListenerStateOld.PopupWindow:
                            var fullscreenBtn = xulDocument.GetElementById("fullscreen-button") as GeckoXULElement;
                            fullscreenBtn.Hidden = true;
                            break;

                        default:
                            break;
                    }
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
                    //PanelUI-developerItems
                  //  view.RaisOnClickedUIListener(state);

                    var panelviewDeveloperItems = xulDocument.GetElementById("PanelUI-developerItems") as GeckoXULElement;
                    foreach (var n in panelviewDeveloperItems.Children)
                    {

                    }

                    var menuitemsecurity_webrtc = xulDocument.GetElementById("menuitemsecurity_webrtc") as GeckoXULElement;
                   
                    var isChecked = menuitemsecurity_webrtc.GetAttribute("checked");
                    if(isChecked == "true")
                    {

                    }

                    foreach (var n in menuitemsecurity_webrtc.Attributes)
                    {
                      
                    }

                    //var paneluiISecurityButton = xulDocument.GetElementById("paneluiISecurityButton") as GeckoXULElement;

                    //class="subviewbutton subviewbutton-nav"

                    return;

                    //switch (state)
                    //{
                    //    case PanelUIListenerState.IAMacro:
                    //        view.RaisOnClickedUIListener(state);
                    //        break;

                    //    case PanelUIListenerState.AnonymitySettings:
                    //        break;

                    //    default:
                    //        break;
                    //}

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

                case "TabClose":
                    args.StopPropagation();
                    args.StopImmediatePropagation();
                    args.PreventDefault();
                    break;

                default:
                    break;
            }
        }
    }
}
