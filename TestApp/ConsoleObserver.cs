using System;
using Gecko.Interfaces;
using Gecko.Javascript;

namespace Gecko
{
	class ConsoleObserver : nsIConsoleListener, nsIObserver
	{
		public static void Init()
		{

			var cobs = new ConsoleObserver();
			var cc = Xpcom.GetService<nsIConsoleService>(Contracts.ConsoleService);
			cc.RegisterListener(cobs);
			var svc = Xpcom.GetService<nsIObserverService>(Contracts.ObserverService);
			svc.AddObserver(cobs, "console-api-log-event", false);
		}

		public void Observe(nsIConsoleMessage aMessage)
		{
			string message = aMessage.GetMessageAttribute();
			if (message.StartsWith("[JavaScript Error:"))
			{
				Console.WriteLine("[{0}] jserror: {1}", DateTime.UtcNow.ToString("HH:mm:ss"), message);
			}
		}

		void nsIObserver.Observe(nsISupports aSubject, string aTopic, string aData)
		{
			try
			{
				var js = GeckoJavascriptBridge.GetService();
				string s = js.EvaluateToString(aSubject, GeckoPrincipal.SystemPrincipal, "this.wrappedJSObject.arguments + ' [level: ' + this.wrappedJSObject.level + ', file: \"' + this.wrappedJSObject.filename + '\", line: ' + this.wrappedJSObject.lineNumber + ']'");
				Console.WriteLine("[{0}] console ({1}): {2}", DateTime.UtcNow.ToString("HH:mm:ss"), aData, s);
			}
			catch (GeckoJavaScriptException e)
			{
				Console.WriteLine("[{0}] {1}", DateTime.UtcNow.ToString("HH:mm:ss"), e.ToString());
			}
		}
	}
}
