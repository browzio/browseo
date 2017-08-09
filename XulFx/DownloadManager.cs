using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Services;
using System.IO;

namespace Gecko
{
	public sealed class DownloadRequestEvent : HandledEventArgs
	{
		/// <summary>
		/// Gets the nsIWebNavigation object. May be null for popup windows.
		/// </summary>
		public GeckoWebNavigation WebNavigation { get; private set; }

		/// <summary>
		/// A helper app launcher to be invoked when a file is selected.
		/// </summary>
		public GeckoHelperAppLauncher DownloadHelper { get; private set; }
		
		/// <summary>
		/// It indicates why the download request has been called.
		/// </summary>
		public GeckoDownloadReason Reason { get; set; }

		public DownloadRequestEvent(GeckoHelperAppLauncher downloadHelper, GeckoWebNavigation windowContext, GeckoDownloadReason aReason)
		{
			this.DownloadHelper = downloadHelper;
			this.WebNavigation = windowContext;
			this.Reason = aReason;
		}
	}

	public sealed class DownloadEventArgs : EventArgs
	{
		public GeckoDownload Downdload { get; private set; }

		public DownloadEventArgs(GeckoDownload download)
		{
			this.Downdload = download;
		}
	}

	/// <summary>
	/// The state of the download.
	/// </summary>
	public enum GeckoDownloadState
	{
		/// <summary>
		/// The download has not been started yet.
		/// </summary>
		NotStarted = nsIDownloadManagerConsts.DOWNLOAD_NOTSTARTED,
		/// <summary>
		/// The download is in the process of being downloaded.
		/// </summary>
		Downloading = nsIDownloadManagerConsts.DOWNLOAD_DOWNLOADING,
		/// <summary>
		/// Download completed including any processing of the target file.
		/// </summary>
		Finished = nsIDownloadManagerConsts.DOWNLOAD_FINISHED,
		/// <summary>
		/// The download failed due to error.
		/// </summary>
		Failed = nsIDownloadManagerConsts.DOWNLOAD_FAILED,
		/// <summary>
		/// The user canceled the download.
		/// </summary>
		Canceled = nsIDownloadManagerConsts.DOWNLOAD_CANCELED,
		/// <summary>
		/// The download is currently paused.
		/// </summary>
		Paused = nsIDownloadManagerConsts.DOWNLOAD_PAUSED,
		/// <summary>
		/// The download is in the queue but is not presently downloading.
		/// </summary>
		Queued = nsIDownloadManagerConsts.DOWNLOAD_QUEUED,
		/// <summary>
		/// The download has been blocked, either by parental controls or the virus scanner determining that a file is infected and cannot be cleaned.
		/// </summary>
		BlockedParental = nsIDownloadManagerConsts.DOWNLOAD_BLOCKED_PARENTAL,
		/// <summary>
		/// The download is being scanned by a virus checking utility.
		/// </summary>
		Scanning = nsIDownloadManagerConsts.DOWNLOAD_SCANNING,
		/// <summary>
		/// A virus was detected in the download. The target will most likely no longer exist.
		/// </summary>
		Dirty = nsIDownloadManagerConsts.DOWNLOAD_DIRTY,
		/// <summary>
		/// Request was blocked by zone policy settings.
		/// </summary>
		BlockedPolicy = nsIDownloadManagerConsts.DOWNLOAD_BLOCKED_POLICY,

	}

	/// <summary>
	/// It indicates why the download request has been called.
	/// </summary>
	public enum GeckoDownloadReason : uint
	{
		CantHandle = nsIHelperAppLauncherDialogConsts.REASON_CANTHANDLE,
		ServerRequest = nsIHelperAppLauncherDialogConsts.REASON_SERVERREQUEST,
		TypeSniffed = nsIHelperAppLauncherDialogConsts.REASON_TYPESNIFFED,

	}

	/// <summary>
	/// Represents a download object.
	/// </summary>
	public sealed class GeckoDownload : ComObject<nsIDownload>, IGeckoObjectWrapper
	{
		/// <summary>
		/// Creates a wrapper for a download object.
		/// </summary>
		public static GeckoDownload Create(nsIDownload instance)
		{
			return new GeckoDownload(instance);
		}

		private GeckoDownload(nsIDownload instance)
			: base(instance)
		{
			
		}

		/// <summary>
		/// The percentage of transfer completed.
		/// If the file size is unknown it&apos;ll be -1 here.
		/// </summary>
		public int PercentComplete
		{
			get { return Instance.GetPercentCompleteAttribute(); }
		}

		/// <summary>
		/// The amount of bytes downloaded so far.
		/// </summary>
		public long AmountTransferred
		{
			get { return Instance.GetAmountTransferredAttribute(); }
		}

		/// <summary>
		/// The size of file in bytes.
		/// Unknown size is represented by -1.
		/// </summary>
		public long Size
		{
			get { return Instance.GetSizeAttribute(); }
		}

		/// <summary>
		/// The speed of the transfer in bytes/sec.
		/// </summary>
		public double Speed
		{
			get { return Instance.GetSpeedAttribute(); }
		}

		/// <summary>
		/// The time a transfer was started.
		/// </summary>
		public DateTime StartTime
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetStartTimeAttribute() / 1000000.0); }
		}

		/// <summary>
		/// The user-readable description of the transfer.
		/// </summary>
		public string DisplayName
		{
			get { return nsString.Get(Instance.GetDisplayNameAttribute); }
		}

		/// <summary>
		/// The state of the download.
		/// </summary>
		public GeckoDownloadState State
		{
			get { return (GeckoDownloadState)Instance.GetStateAttribute(); }
		}

		/// <summary>
		/// The source of the transfer.
		/// </summary>
		public Uri Source
		{
			get
			{
				nsIURI nsUri = Instance.GetSourceAttribute();
				try
				{
					return nsUri.ToUri();	
				}
				finally
				{
					Xpcom.FreeComObject(ref nsUri);
				}
			}
		}

		/// <summary>
		/// Gets a target file name
		/// </summary>
		public string Filename
		{
			get
			{
				nsIURI nsUri = Instance.GetTargetAttribute();
				try
				{
					Uri uri = nsUri.ToUri();
					if (uri == null || !uri.IsFile)
						return null;
					return uri.LocalPath;
				}
				finally
				{
					Xpcom.FreeComObject(ref nsUri);
				}
			}
		}

		/// <summary>
		/// Indicates if the download can be resumed after being paused or not.  This
		/// is only the case if the download is over HTTP/1.1 or FTP and if the
		/// server supports it.
		/// </summary>
		public bool IsResumable
		{
			get { return Instance.GetResumableAttribute(); }
		}

		/// <summary>
		/// Indicates if the download was initiated from a context marked as private,
		/// controlling whether it should be stored in a permanent manner or not.
		/// </summary>
		public bool IsPrivate
		{
			get { return Instance.GetIsPrivateAttribute(); }
		}
		
		/// <summary>
		/// Cancel this download if it&apos;s currently in progress.
		/// </summary>
		public void Cancel()
		{
			Instance.Cancel();
		}

		/// <summary>
		/// Pause this download if it is in progress.
		/// </summary>
		/// <exception cref="System.Runtime.InteropServices.COMException">
		/// NS_ERROR_UNEXPECTED: if it cannot be paused.
		/// </exception>
		public void Pause()
		{
			Instance.Pause();
		}

		/// <summary>
		/// Resume this download if it is paused.
		/// </summary>
		/// <exception cref="System.Runtime.InteropServices.COMException">
		/// NS_ERROR_UNEXPECTED: if it cannot be resumed or is not paused.
		/// </exception>
		public void Resume()
		{
			Instance.Resume();
		}

		/// <summary>
		/// Instruct the download manager to retry this failed download
		/// </summary>
		/// <exception cref="System.Runtime.InteropServices.COMException">
		/// NS_ERROR_NOT_AVAILABLE: if the download is not known.
		/// NS_ERROR_FAILURE: if the download is not in the following states:
		/// nsIDownloadManager::DOWNLOAD_CANCELED
		/// nsIDownloadManager::DOWNLOAD_FAILED
		/// </exception>
		public void Retry()
		{
			Instance.Retry();
		}

		/// <summary>
		/// Instruct the download manager to remove this download. Whereas
		/// cancel simply cancels the transfer, but retains information about it,
		/// remove removes all knowledge of it.
		/// </summary>
		/// <exception cref="System.Runtime.InteropServices.COMException">
		/// NS_ERROR_FAILURE: if the download is active.
		/// </exception>
		/// <remarks>
		/// See nsIDownloadManager.removeDownload for more detail
		/// </remarks>
		public void Remove()
		{
			Instance.Remove();
		}
	}

	/// <summary>
	/// A helper app launcher is a small object created to handle the launching
	/// of an external application.
	/// </summary>
	public sealed class GeckoHelperAppLauncher : ComObject<nsIHelperAppLauncher>, IGeckoObjectWrapper
	{
		/// <summary>
		/// Creates a wrapper for a helper app launcher
		/// </summary>
		/// <param name="instance">A helper app launcher</param>
		/// <returns></returns>
		public static GeckoHelperAppLauncher Create(nsIHelperAppLauncher instance)
		{
			return new GeckoHelperAppLauncher(instance);
		}

		private GeckoHelperAppLauncher(nsIHelperAppLauncher instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Gets the source uri
		/// </summary>
		public Uri Source
		{
			get
			{
				nsIURI uri = Instance.GetSourceAttribute();
				try
				{
					return uri.ToUri();
				}
				finally
				{
					Xpcom.FreeComObject(ref uri);
				}
			}
		}

		/// <summary>
		/// Gets the suggested name for a file
		/// </summary>
		public string SuggestedFileName
		{
			get { return nsString.Get(Instance.GetSuggestedFileNameAttribute); }
		}

		/// <summary>
		/// The name of the file we are saving to
		/// </summary>
		public string TargetFileName
		{
			get
			{
				nsIFile file = Instance.GetTargetFileAttribute();
				if (file == null)
					return null;
				try
				{
					return nsString.Get(file.GetPathAttribute);
				}
				finally
				{
					Xpcom.FreeComObject(ref file);
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether the target file is executable.
		/// </summary>
		/// <exception cref="COMException">Throws NS_ERROR_FILE_NOT_FOUND.</exception>
		public bool TargetFileIsExecutable
		{
			get { return Instance.GetTargetFileIsExecutableAttribute(); }
		}

		/// <summary>
		/// Get a time when the download started
		/// </summary>
		public DateTime StartTime
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetTimeDownloadStartedAttribute() / 1000000.0); }
		}

		/// <summary>
		/// The download content length, or -1 if the length is not available.
		/// </summary>
		public long ContentLength
		{
			get { return Instance.GetContentLengthAttribute(); }
		}

		/// <summary>
		/// Call this method to request that this object abort whatever operation it
		/// may be performing.
		/// </summary>
		public void Cancel()
		{
			Instance.Cancel(GeckoError.NS_BINDING_ABORTED);
		}

		/// <summary>
		/// Call this method to request that this object abort whatever operation it
		/// may be performing.
		/// </summary>
		/// <param name="aReason">
		/// Pass a failure code to indicate the reason why this operation is
		/// being canceled.  It is an error to pass a success code.
		/// </param>
		public void Cancel(int aReason)
		{
			Instance.Cancel(aReason);
		}

		public void SaveToDisk(string path, bool rememberThisPreference)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			string filename = Path.GetFileName(path);
			if (filename.Length == 0
				|| filename.IndexOfAny(Path.GetInvalidFileNameChars()) != -1
				|| path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
			{
				throw new ArgumentOutOfRangeException("path");
			}

			path = Path.GetFullPath(path);
			using (var file = Xpcom.OpenFile(path))
			{
				Instance.SaveToDisk(file.Instance, rememberThisPreference);
			}
		}

		public void LaunchWithApplication(string appPath, bool rememberThisPreference)
		{
			if (appPath == null)
			{
				Instance.LaunchWithApplication(null, rememberThisPreference);
			}
			else
			{
				using (var path = Xpcom.OpenFile(appPath))
				{
					Instance.LaunchWithApplication(path.Instance, rememberThisPreference);
				}
			}
		}

	}

	public sealed class DownloadManager : nsIHelperAppLauncherDialog, nsIObserver
	{
		public static event EventHandler<DownloadRequestEvent> DownloadRequest;
		public static event EventHandler<DownloadEventArgs> DownloadStarted;
		public static event EventHandler<DownloadEventArgs> DownloadCompleted;
		public static event EventHandler<DownloadEventArgs> DownloadCancelled;
		public static event EventHandler<DownloadEventArgs> DownloadFailed;

		private DownloadManager()
		{
			var obsSvc = Xpcom.GetService<nsIObserverService>(Contracts.ObserverService);
			obsSvc.AddObserver(this, "dl-start", false);
			obsSvc.AddObserver(this, "dl-done", false);
			obsSvc.AddObserver(this, "dl-cancel", false);
			obsSvc.AddObserver(this, "dl-failed", false);
			obsSvc.AddObserver(this, "dl-scanning", false);
			obsSvc.AddObserver(this, "dl-blocked", false);
			obsSvc.AddObserver(this, "dl-dirty", false);
		}


		/// <summary>
		/// Initialize the DownloadManager.
		/// </summary>
		/// <remarks>
		/// Uses lazy initialization. Can cause hangs on Xpcom.Shutdown() if the event loop never existed. 
		/// </remarks>
		public static void Init()
		{
			Xpcom.RegisterInstance(typeof(nsIHelperAppLauncherDialog).GUID, typeof(DownloadManager).Name, Contracts.HelperAppLauncherDialog, new DownloadManager());
			
			var dlh = Xpcom.GetService<nsIRunnable>(Contracts.XulfxDownloadHelper);
			try
			{
				dlh.Run();
			}
			finally
			{
				Xpcom.FreeComObject(ref dlh);
			}
		}

		public static void Shutdown()
		{
			nsICancelable dlh = Xpcom.GetService<nsICancelable>(Contracts.XulfxDownloadHelper);
			try
			{
				dlh.Cancel(GeckoError.NS_ERROR_ABORT);
			}
			finally
			{
				Xpcom.FreeComObject(ref dlh);
			}
		}

		public static void AddDownload(Uri url, string filename)
		{
			nsIURI sourceUri = null;
			nsIURI targetUri = null;
			nsIDownload download = null;
			nsAString displayName = null;
			nsIDownloadManager downloadMan = null;
			nsIWebBrowserPersist persist = Xpcom.CreateInstance<nsIWebBrowserPersist>(Contracts.NsWebBrowserPersist);
			try
			{
				IOService ioSvc = IOService.GetService();
				sourceUri = ioSvc.CreateNsIUri(url.AbsoluteUri);
				targetUri = ioSvc.CreateNsIUri(new Uri(filename, UriKind.Absolute).AbsoluteUri);
				displayName = new nsAString(System.IO.Path.GetFileName(filename));

				downloadMan = Xpcom.GetService<nsIDownloadManager>(Contracts.DowndloadManager);
				download = downloadMan.AddDownload(nsIDownloadManagerConsts.DOWNLOAD_TYPE_DOWNLOAD, sourceUri, targetUri, displayName, null, (ulong)(Utils.ToSecondsSinceEpoch(DateTime.Now) * 1000000.0), null, persist, false);

				if (download != null)
				{
					persist.SetPersistFlagsAttribute(
						nsIWebBrowserPersistConsts.PERSIST_FLAGS_BYPASS_CACHE
						| nsIWebBrowserPersistConsts.PERSIST_FLAGS_REPLACE_EXISTING_FILES
						| nsIWebBrowserPersistConsts.PERSIST_FLAGS_AUTODETECT_APPLY_CONVERSION
					);
					persist.SetProgressListenerAttribute(download);
					persist.SaveURI(sourceUri, null, null, nsIHttpChannelConsts.REFERRER_POLICY_DEFAULT, null, null, (nsISupports)targetUri, null);
				}
			}
			finally
			{
				if (displayName != null)
					displayName.Dispose();
				Xpcom.FreeComObject(ref download);
				Xpcom.FreeComObject(ref downloadMan);
				Xpcom.FreeComObject(ref sourceUri);
				Xpcom.FreeComObject(ref targetUri);
				Xpcom.FreeComObject(ref persist);
			}
		}

		void nsIHelperAppLauncherDialog.PromptForSaveToFileAsync(nsIHelperAppLauncher aLauncher, nsISupports aWindowContext, string aDefaultFileName,
			string aSuggestedFileExtension, bool aForcePrompt)
		{
			throw new NotImplementedException();
		}

		void nsIHelperAppLauncherDialog.Show(nsIHelperAppLauncher aLauncher, nsISupports aWindowContext, uint aReason)
		{
			if (DownloadRequest != null)
			{
				try
				{
					var ea = new DownloadRequestEvent(
						Xpcom.QueryInterface<nsIHelperAppLauncher>(aLauncher).Wrap(GeckoHelperAppLauncher.Create),
						Xpcom.QueryInterface<nsIWebNavigation>(aWindowContext).Wrap(GeckoWebNavigation.Create),
						(GeckoDownloadReason)aReason);

					DownloadRequest(this, ea);
					if (ea.Handled)
						return;
				}
				catch
				{
					aLauncher.Cancel(GeckoError.NS_BINDING_ABORTED);
					throw;
				}
			}
			aLauncher.Cancel(GeckoError.NS_BINDING_ABORTED);
		}

		void nsIObserver.Observe(nsISupports aSubject, string aTopic, string aData)
		{
			switch (aTopic)
			{
				case "dl-done":
					if (DownloadCompleted != null)
						DownloadCompleted(this, new DownloadEventArgs(Xpcom.QueryInterface<nsIDownload>(aSubject).Wrap(GeckoDownload.Create)));
					break;
				case "dl-start":
					if (DownloadStarted != null)
						DownloadStarted(this, new DownloadEventArgs(Xpcom.QueryInterface<nsIDownload>(aSubject).Wrap(GeckoDownload.Create)));
					break;
				case "dl-cancel":
					if (DownloadCancelled != null)
						DownloadCancelled(this, new DownloadEventArgs(Xpcom.QueryInterface<nsIDownload>(aSubject).Wrap(GeckoDownload.Create)));
					break;
				case "dl-failed":
					if (DownloadFailed != null)
						DownloadFailed(this, new DownloadEventArgs(Xpcom.QueryInterface<nsIDownload>(aSubject).Wrap(GeckoDownload.Create)));
					break;
			}
		}
	}

}