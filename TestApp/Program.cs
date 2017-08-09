using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Gecko;
using System.IO;
using Gecko.Security;

namespace TestApp
{
	static class Program
	{

		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Xpcom.ProfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "profile");
			Xpcom.MemoryPressureCallback += OnMemoryPressure;
			
			Xpcom.Initialize(XULRunnerLocator.GetXULRunnerLocation());

			GeckoPreferences pref = GeckoPreferences.Default;
			pref["extensions.getAddons.cache.enabled"] = false;
			pref["general.useragent.override"] = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:52.0) Gecko/20100101 Firefox/52.0";

			// enable WebRTC
			pref["media.navigator.enabled"] = true;
			pref["media.navigator.permission.disabled"] = true;
			pref["media.peerconnection.enabled"] = true;

			if (false) // fiddler
			{
				pref["network.proxy.type"] = 1;
				pref["network.proxy.http"] = "127.0.0.1";
				pref["network.proxy.http_port"] = 8888;
				pref["network.proxy.ssl"] = "127.0.0.1";
				pref["network.proxy.ssl_port"] = 8888;
			}

			// add a custom item to context menu
			Xpcom.RegisterInstance(typeof(IContextMenuUpdater).GUID, "MyContextMenuUpdater", Contracts.XulfxContextMenuUpdater, new MyContextMenuUpdater());
			Xpcom.RegisterInstance(typeof(IContextMenuHandler).GUID, "MyContextMenuHandler", Contracts.XulfxContextMenuHandler, new MyContextMenuHandler());
			// initialize a console logging
			ConsoleObserver.Init();

			DownloadManager.Init();
			Application.ApplicationExit += (s, e) => { DownloadManager.Shutdown(); };
			DownloadManager.DownloadRequest += DownloadManager_DownloadRequest;
			DownloadManager.DownloadCompleted += DownloadManager_DownloadCompleted;
			CertOverrideService.GetService().ValidityOverride += Certificate_ValidityOverride;

			Application.Run(new MainForm());
			
			Xpcom.Shutdown();
		}

		private static void OnMemoryPressure(string reason)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
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
			using(var dlg = new SaveFileDialog())
			{
				dlg.FileName = e.DownloadHelper.SuggestedFileName;
				string ext = Path.GetExtension(dlg.FileName);
				dlg.Filter = (ext.Length > 0 ? string.Format("Files (*{0})|*{0}|", ext) : string.Empty) + "All files (*.*)|*.*";
				if(dlg.ShowDialog() == DialogResult.OK)
				{
					e.DownloadHelper.SaveToDisk(dlg.FileName, false);
					e.Handled = true;
				}
			}
		}

		private static void DownloadManager_DownloadCompleted(object sender, DownloadEventArgs e)
		{
			MessageBox.Show(string.Format("Download completed [{0}]", e.Downdload.Filename));
		}

	}

}
