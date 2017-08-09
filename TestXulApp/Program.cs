using Gecko;
using Gecko.DOM;
using Gecko.GUI;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace TestXulApp
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Xpcom.ProfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "profile");

			string xulrunnerPath = XULRunnerLocator.GetXULRunnerLocation();
			Xpcom.DebugPrint("xulrunner path = {0}", xulrunnerPath);
			if (Xpcom.IsLinux)
			{
				if (!(Environment.GetEnvironmentVariable("LD_LIBRARY_PATH") ?? string.Empty).Contains(xulrunnerPath))
					throw new ApplicationException(String.Format("LD_LIBRARY_PATH must contain {0}", xulrunnerPath));
			}

			if(Xpcom.IsMono)
				SynchronizationContext.SetSynchronizationContext(new GeckoSynchronizationContext());

			Xpcom.Initialize(xulrunnerPath);
			
			ConsoleObserver.Init();

			DownloadManager.Init();
			Xpcom.BeforeShutdown += (s, e) =>
			{
				DownloadManager.Shutdown();
			};
			DownloadManager.DownloadRequest += DownloadManager_DownloadRequest;
			DownloadManager.DownloadCompleted += DownloadManager_DownloadCompleted;
			CertOverrideService.GetService().ValidityOverride += Certificate_ValidityOverride;

#if GTK
			Gtk.Application.Init();
#endif
			//GeckoPreferences.Default["browser.chromeURL"] = "chrome://xulfx/content/window.xul";

			var xulWindow = new GeckoXULWindow();
			xulWindow.WaitUntilChromeLoad();
			xulWindow.Width = 600;
			xulWindow.Height = 400;

			var xulDocument = xulWindow.View.Document;

			Xpcom.DebugPrint(xulDocument);
			GeckoElement window = xulDocument.DocumentElement;//.GetElementById("rootWindow");
			window.SetAttribute("title", "Hello, World!");

			GeckoElement browser = xulDocument.CreateElement("browser");
			browser.SetAttribute("id", "tab1");
			browser.SetAttribute("flex", "1");
			browser.SetAttribute("type", "content-targetable");
			browser.SetAttribute("src", "about:memory");
			window.AppendChild(browser);


			GeckoElement button = xulDocument.CreateElement("button");
			button.SetAttribute("label", "Go to Google");
			button.SetAttribute("oncommand", "alert(document.getElementById('tab1').contentWindow.location.href = 'http://localhost:12111/geckofx/index.php')");
			window.AppendChild(button);


			xulWindow.ShowDialog();

			Console.WriteLine("shutdown");
			Xpcom.Shutdown();
		}

		private static void Certificate_ValidityOverride(object sender, CertOverrideEventArgs e)
		{
			// Ignore certificate errors
			e.OverrideResult = CertOverride.Mismatch | CertOverride.Time | CertOverride.Untrusted;
			e.Temporary = true;
			e.Handled = true;
		}

		private static void DownloadManager_DownloadRequest(object sender, DownloadRequestEvent e)
		{
			string filename = e.DownloadHelper.SuggestedFileName;
			foreach (char ch in Path.GetInvalidFileNameChars())
			{
				filename = filename.Replace(ch, '-');
			}
			string path = Path.Combine(Path.Combine(Xpcom.ProfilePath, "download"), filename);
			e.DownloadHelper.SaveToDisk(path, false);
			e.Handled = true;
		}

		private static void DownloadManager_DownloadCompleted(object sender, DownloadEventArgs e)
		{
			Console.WriteLine(string.Format("Download completed [{0}]", e.Downdload.Filename));
		}

	}

}
