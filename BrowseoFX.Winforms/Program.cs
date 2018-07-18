using Gecko;
using Gecko.GUI;
using Gecko.Javascript;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrowseoFX.Winforms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var xulfxPath = Path.Combine(@"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\bin\Debug", "XulFx.xpi");
            Xpcom.XulfxPath = xulfxPath;


            var profileDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\bin\Debug\profile";
           // Xpcom.ProfilePath = profileDirectory;

            var binDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin";
            Xpcom.Initialize(binDirectory);

            nsConsoleListener.Init();

            //var xulWindow = new GeckoXULWindow();
            //xulWindow.WaitUntilChromeLoad();
            //xulWindow.Width = 600;
            //xulWindow.Height = 400;
            //xulWindow.Show();
            
            Application.Run(new MainWindow());

            Xpcom.Shutdown();
        }






        public class nsConsoleListener : Gecko.Interfaces.nsIConsoleListener, Gecko.Interfaces.nsIObserver
        {
            public static void Init()
            {

                var cobs = new nsConsoleListener();
                var cc = Xpcom.GetService<Gecko.Interfaces.nsIConsoleService>(Gecko.Contracts.ConsoleService);
                cc.RegisterListener(cobs);
                var svc = Xpcom.GetService<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService);
                svc.AddObserver(cobs, "console-api-log-event", false);
            }

            public void Observe(Gecko.Interfaces.nsIConsoleMessage aMessage)
            {
                string message = aMessage.GetMessageAttribute();
                if (message.StartsWith("[JavaScript Error:"))
                {
                    Console.WriteLine("[{0}] jserror: {1}", DateTime.UtcNow.ToString("HH:mm:ss"), message);
                }
            }

            void Gecko.Interfaces.nsIObserver.Observe(Gecko.Interfaces.nsISupports aSubject, string aTopic, string aData)
            {
                try
                {
                    var js = GeckoJavascriptBridge.GetService();
                    string s = js.EvaluateToString(aSubject, GeckoPrincipal.SystemPrincipal, "this.wrappedJSObject.arguments + ' [level: ' + this.wrappedJSObject.level + ', file: \"' + this.wrappedJSObject.filename + '\", line: ' + this.wrappedJSObject.lineNumber + ']'");
                    Console.WriteLine("[{0}] console ({1}): {2}", DateTime.UtcNow.ToString("HH:mm:ss"), aData, s);
                }
                catch (Gecko.GeckoJavaScriptException e)
                {
                    Console.WriteLine("[{0}] {1}", DateTime.UtcNow.ToString("HH:mm:ss"), e.ToString());
                }
            }
        }
    }
}
