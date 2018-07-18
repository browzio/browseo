using Gecko.CustomMarshalers;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Old;
using Gecko.Services;
using SpiderMonkey;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Gecko
{
	public static partial class Xpcom
    {
		private static readonly object syncRoot = new object();
		private static ComObject<nsIComponentManager> _componentManager;
		private static ComObject<nsIComponentRegistrar> _componentRegistrar;
		private static ComObject<nsIServiceManager> _serviceManager;
        public static ComObject<nsIServiceManager> ServiceManager { get { return _serviceManager; } }
        //private static XulfxAppInfo _appInfo;
        private static DateTime? _xulrunnerTimestamp;
		private static string _xulrunnerVersion;

		private static bool _exitFlag;
		private static long _modalLoopCounter;
		private static IntPtr _mainThreadPtr;
		private static nsIThreadDispatchDelegate _raiseEventOnMainThread;
		private static volatile Exception _mainThreadException;

		#region Fields
		static readonly bool _isMono;
		static readonly bool _is64Bit;
		static readonly bool _is32Bit;
		static bool _IsInitialized;
		static int _XpcomThreadId;
		//private static COMGC _comGC;
		private static DirectoryServiceProvider _directoryServiceProvider;
		#endregion


		static Xpcom()
		{
			// get runtime information at first start
			//http://www.mono-project.com/Guide:_Porting_Winforms_Applications
			_isMono = Type.GetType("Mono.Runtime") != null;
			_is64Bit = IntPtr.Size == 8;
			_is32Bit = IntPtr.Size == 4;
		}

		#region Events

		/// <summary>
		/// Occurs once after XpCom has been initalized.
		/// </summary>
		public static event EventHandler BeforeInitalization;

		/// <summary>
		/// Occurs once after XpCom has been initalized.
		/// </summary>
		public static event EventHandler AfterInitalization;

		/// <summary>
		/// Occurs once after XpCom has been initalized.
		/// </summary>
		public static event EventHandler BeforeShutdown;

		/// <summary>
		/// Occurs once after XpCom has been initalized.
		/// </summary>
		public static event EventHandler AfterShutdown;

		public static event EventHandler Idle = (s, e) => { };

		#endregion

		#region CLR runtime
		/// <summary>
		/// GeckoFX is running on Linux
		/// </summary>
		public static bool IsLinux
		{
			get { return Environment.OSVersion.Platform == PlatformID.Unix; }
		}

		/// <summary>
		/// GeckoFX is running on Windows
		/// </summary>
		public static bool IsWindows
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT;
			}
		}

		/// <summary>
		/// GeckoFX is running on Mono CLR implementation
		/// </summary>
		public static bool IsMono
		{
			get { return _isMono; }
		}

		/// <summary>
		///  GeckoFX is running on Microsoft CLR implementation (.NET Framework)
		/// </summary>
		public static bool IsDotNet
		{
			get { return !_isMono; }
		}

		public static bool Is32Bit
		{
			get { return _is32Bit; }
		}

		public static bool Is64Bit
		{
			get { return _is64Bit; }
		}

		public static bool IsInitialized
		{
			get { return _IsInitialized; }
		}

		#endregion

		#region Init & shutdown
		/// <summary>
		/// Initializes XPCOM using the current directory as the XPCOM directory.
		/// </summary>
		public static void Initialize()
		{
			Initialize(null);
		}

		/// <summary>
		/// Initializes XPCOM using the specified directory.
		/// </summary>
		/// <param name="binDirectory">The directory which contains xul.dll.</param>
		public static void Initialize(string binDirectory)
		{
			int nsresult;

			if (!_IsInitialized)
			{
				if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
					throw new InvalidOperationException("The calling thread must be STA");

				if (BeforeInitalization != null)
					BeforeInitalization(typeof(Xpcom), EventArgs.Empty);

				Interlocked.Exchange(ref _XpcomThreadId, Thread.CurrentThread.ManagedThreadId);

				string oldCurrentDir = Directory.GetCurrentDirectory();
				string libPath = Path.GetFullPath(binDirectory ?? oldCurrentDir);
				LoadXpcom(libPath);

				if (binDirectory != null)
				{
					Environment.SetEnvironmentVariable("path", Environment.GetEnvironmentVariable("path") + ";" + binDirectory);
				}

				//if (Xpcom.IsLinux) // Init xpcom glue & memory
				//	NativeMethods.XulFx_Init();

				nsIFile mreAppDir = null;
				using (var str = new nsAString(libPath))
				{
					nsresult = NativeMethods.NS_NewLocalFile(str, true, out mreAppDir);
					if (nsresult != NativeMethods.NS_OK)
						throw new COMException("Failed on NS_NewLocalFile", nsresult);
				}

				try
				{
					// temporarily change the current directory so NS_InitEmbedding can find all the DLLs it needs
					Directory.SetCurrentDirectory(libPath);
					Xpcom.DirectoryServiceProvider.GeckoPath = libPath;


					//Note: the error box that this can generate can't be prevented with try/catch, and res is 0 even if it fails
					//REVIEW: how else can we determine what happened and give a more useful answer, to help new GeckoFX users,
					//Telling them that probably the version of firefox or xulrunner didn't match this library version?

					// calling NS_InitXPCOM2 invokes AddRef to the returned nsIServerManager, but as this gets assigned to the __ComObject serviceManager
					// Release will be called by the when the GC runs __ComObject finaliser.
					nsIServiceManager serviceManager;
					if(Xpcom.IsMono)
						nsresult = NativeMethods.NS_InitXPCOM2(out serviceManager, mreAppDir, null);
					else
						nsresult = NativeMethods.NS_InitXPCOM2(out serviceManager, mreAppDir, Xpcom.DirectoryServiceProvider);
					if (nsresult != NativeMethods.NS_OK)
						throw new COMException("Failed on NS_InitXPCOM2", nsresult);
					_serviceManager = new ComObject<nsIServiceManager>(serviceManager);
					serviceManager = null;

					// get some global objects we will need later
					nsIComponentManager componentManager;
					if (0 == NativeMethods.NS_GetComponentManager(out componentManager))
						_componentManager = new ComObject<nsIComponentManager>(componentManager);
					componentManager = null;

					nsIComponentRegistrar componentRegistrar;
					if (NativeMethods.NS_GetComponentRegistrar(out componentRegistrar) == NativeMethods.NS_OK)
						_componentRegistrar = new ComObject<nsIComponentRegistrar>(componentRegistrar);
					componentRegistrar = null;

				}
				finally
				{
					// change back
					Directory.SetCurrentDirectory(oldCurrentDir);
				}

             //  var profileservice = Xpcom.GetService2<nsIToolkitProfileService>("@mozilla.org/toolkit/profile-service;1");//.Instance.GetClassObjectByContractID["@mozilla.org/toolkit/profile-service;1"].getService(Ci.nsIToolkitProfileService);

                // RegisterProvider is necessary to get link styles etc.
                nsIDirectoryService directoryService = null;
				try
				{
					directoryService = Xpcom.GetService<nsIDirectoryService>(Contracts.DirectoryService);
					if (directoryService != null)
						directoryService.RegisterProvider(Xpcom.DirectoryServiceProvider);
				}
				finally
				{
					Xpcom.FreeComObject(ref directoryService);
				}

				nsIThreadManager threadMan = Xpcom.GetService<nsIThreadManager>(Contracts.ThreadManager);
				using (var mainThread = new ComObject<nsIThread>(threadMan.GetMainThreadAttribute()))
				{
					Xpcom.FreeComObject(ref threadMan);

					_raiseEventOnMainThread = mainThread.GetDelegateForComMethod<nsIThread, nsIThreadDispatchDelegate>(new Action<nsIRunnable, uint>(mainThread.Instance.Dispatch));
					_mainThreadPtr = Marshal.GetComInterfaceForObject(mainThread.Instance, typeof(nsIEventTarget));
				}

                using (var PreferencesService = Xpcom.GetService2<Gecko.Interfaces.nsIPrefService>(Gecko.Contracts.PreferencesService))
                {
                    var userPrefsFile = Path.Combine(Xpcom.DirectoryServiceProvider.ProfilePathRoaming, "prefs.js");

                    if (File.Exists(userPrefsFile))
                        PreferencesService.Instance.ReadUserPrefs(DirectoryServiceProvider.NewLocalFile(userPrefsFile));
                    else
                        PreferencesService.Instance.SavePrefFile(DirectoryServiceProvider.NewLocalFile(userPrefsFile));
                }


                //GeckoPreferences pref = GeckoPreferences.Default;
                //pref["toolkit.telemetry.enabled"] = false;
                //pref["app.update.enabled"] = false;
                //pref["browser.disableResetPrompt"] = true;
                //pref["general.skins.selectedSkin"] = "classic/1.0";
                //pref["browser.tabs.closeWindowWithLastTab"] = false;
                //pref["datareporting.sessions.current.sessionRestored"] = 0;

                //GeckoPreferences userPref = GeckoPreferences.User;
                //userPref["datareporting.sessions.current.sessionRestored"] = 0;

                //if (XpcomProgressListener != null) XpcomProgressListener.GeckoPreferencesServiceAvailable();

                Xpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIXULRuntime).GUID, "nsXULAppInfo", Gecko.Contracts.AppInfo, nsXULAppInfo.Instance);
               // Xpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIToolkitProfileService).GUID, "nsToolkitProfileService", "@mozilla.org/toolkit/profile-service;1", nsToolkitProfileService.Instance);
               // Xpcom.RegisterInstance(new Guid("5d0ce354-df01-421a-83fb-7ead0990c24e"), "nsBrowserHandler", "@mozilla.org/browser/clh;1", nsBrowserHandler.Instance);
                //Xpcom.RegisterInstance(typeof(nsIBrowserGlue).GUID, "nsBrowserGlue", "@mozilla.org/browser/browserglue;1", nsBrowserGlue.Instance);

                using (var TransportService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.TransportService)) { }
                using (var DnsService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.DnsService)) { }
                using (var NetworkIOService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.NetworkIOService)) { }
                using (var ChromeRegistry = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.ChromeRegistry)) { }

                
                //using (var FormHistoryStartup = Xpcom.GetService2<Gecko.Interfaces.nsISupports>("@mozilla.org/satchel/form-history-startup;1")) { }//FormHistoryStartup.js
                //class not registerd: using (var SuppressorService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.SuppressorService)) { }

                //Xpcom.LoadExtension(ComponentsPath, "components.manifest");
                //using (var browserClassInitializer = Xpcom.GetService2<Gecko.Interfaces.nsISupports>("@mozilla.browseo/browser/browserClassInitializer;1")) { }
                // using (var BrowseoFXBrowserHandler = Xpcom.GetService2<Gecko.Interfaces.nsISupports>("@mozilla.browseo/browser/BrowseoFXBrowserHandler;1")) { }
                //using (var BrowserContentHandler = Xpcom.GetService2<Gecko.Interfaces.nsIBrowserHandler>("@mozilla.org/browser/clh;1")) { }


                using (var windowWatcher = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
                {
                    using (var creator = Xpcom.GetService2<Gecko.Interfaces.nsIWindowCreator>(Gecko.Contracts.AppStartup))
                    {
                        windowWatcher.Instance.SetWindowCreator(creator.Instance);
                    }
                    //windowWatcher.Instance.SetWindowCreator(WindowCreator.Instance);
                }

                _IsInitialized = true;

                var startupNotifier = Xpcom.GetService<Gecko.Interfaces.nsIObserver>(Gecko.Contracts.AppStartupNotifier);
                startupNotifier.Observe(null, "app-startup", null);

                // var obsSvc = Xpcom.GetService<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService);
    //            nsObserverService.ObserverService.NotifyObservers(null, "app-startup", null);
    //            nsObserverService.ObserverService.NotifyObservers(null, "profile-do-change", "startup");


    //            Xpcom.LoadExtension(Xpcom.XulfxPath, "");

    //            var em = Xpcom.GetService<Gecko.Interfaces.nsIObserver>(Gecko.Contracts.AddonsIntegration);
    //            if (em != null) em.Observe(null, "addons-startup", null);


    //            nsObserverService.ObserverService.NotifyObservers(null, "load-extension-defaults", null);
    //            Xpcom.FreeComObject(ref em);

    //            nsObserverService.ObserverService.NotifyObservers(null, "profile-after-change", "startup");
    //            Xpcom.NS_CreateServicesFromCategory("profile-after-change", null, "profile-after-change");
    //            nsObserverService.ObserverService.NotifyObservers(null, "profile-initial-state", null);

    //            var cmdLine = Xpcom.GetService<Gecko.Interfaces.nsICommandLineRunner>(Gecko.Contracts.CommandLine);
    //            cmdLine.Init(0, IntPtr.Zero, DirectoryServiceProvider.NewLocalFile(binDirectory), Gecko.Interfaces.nsICommandLineConsts.STATE_INITIAL_LAUNCH);
    //            nsObserverService.ObserverService.NotifyObservers(cmdLine as Gecko.Interfaces.nsISupports, "command-line-startup", null);

    //            using (var appStartupSrv = Xpcom.GetService2<Gecko.Interfaces.nsIAppStartup>(Gecko.Contracts.AppStartup))
    //            {
    //                appStartupSrv.Instance.CreateHiddenWindow();
    //                nsObserverService.ObserverService.NotifyObservers(null, "final-ui-startup", null);
    //                appStartupSrv.Instance.DoneStartingUp();
    //            }
    //            cmdLine.Run();

    //            DefaultPromptFactory.Init();

				//ObserverService.GetService().AddObserver(new MemoryPressureObserver(), "memory-pressure", false);

                //Xpcom.FreeComObject(ref cmdLine);

                if (AfterInitalization != null)
					AfterInitalization(typeof(Xpcom), EventArgs.Empty);
            }
		}

        #region edited
        public static void InitializeXPCOM(string binDirectory, nsIDirectoryServiceProvider directoryProvider)
        {
            int nsresult;

            if (!_IsInitialized)
            {
                if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
                    throw new InvalidOperationException("The calling thread must be STA");

                if (BeforeInitalization != null)
                    BeforeInitalization(typeof(Xpcom), EventArgs.Empty);

                Interlocked.Exchange(ref _XpcomThreadId, Thread.CurrentThread.ManagedThreadId);

                string oldCurrentDir = Directory.GetCurrentDirectory();
                string libPath = Path.GetFullPath(binDirectory ?? oldCurrentDir);
                LoadXpcom(libPath);

                if (binDirectory != null)
                {
                    Environment.SetEnvironmentVariable("path", Environment.GetEnvironmentVariable("path") + ";" + binDirectory);
                }

                //if (Xpcom.IsLinux) // Init xpcom glue & memory
                //	NativeMethods.XulFx_Init();

                nsIFile mreAppDir = null;
                using (var str = new nsAString(libPath))
                {
                    nsresult = NativeMethods.NS_NewLocalFile(str, true, out mreAppDir);
                    if (nsresult != NativeMethods.NS_OK)
                        throw new COMException("Failed on NS_NewLocalFile", nsresult);
                }

                try
                {
                    // temporarily change the current directory so NS_InitEmbedding can find all the DLLs it needs
                    Directory.SetCurrentDirectory(libPath);


                    //Note: the error box that this can generate can't be prevented with try/catch, and res is 0 even if it fails
                    //REVIEW: how else can we determine what happened and give a more useful answer, to help new GeckoFX users,
                    //Telling them that probably the version of firefox or xulrunner didn't match this library version?

                    // calling NS_InitXPCOM2 invokes AddRef to the returned nsIServerManager, but as this gets assigned to the __ComObject serviceManager
                    // Release will be called by the when the GC runs __ComObject finaliser.
                    nsIServiceManager serviceManager;
                    //if (Xpcom.IsMono)
                    //    nsresult = NativeMethods.NS_InitXPCOM2(out serviceManager, mreAppDir, null);
                    //else
                        nsresult = NativeMethods.NS_InitXPCOM2(out serviceManager, mreAppDir, directoryProvider);
                    if (nsresult != NativeMethods.NS_OK)
                        throw new COMException("Failed on NS_InitXPCOM2", nsresult);
                    _serviceManager = new ComObject<nsIServiceManager>(serviceManager);
                    serviceManager = null;

                    // get some global objects we will need later
                    nsIComponentManager componentManager;
                    if (0 == NativeMethods.NS_GetComponentManager(out componentManager))
                        _componentManager = new ComObject<nsIComponentManager>(componentManager);
                    componentManager = null;

                    nsIComponentRegistrar componentRegistrar;
                    if (NativeMethods.NS_GetComponentRegistrar(out componentRegistrar) == NativeMethods.NS_OK)
                        _componentRegistrar = new ComObject<nsIComponentRegistrar>(componentRegistrar);
                    componentRegistrar = null;

                }
                finally
                {
                    // change back
                    Directory.SetCurrentDirectory(oldCurrentDir);
                }

                _IsInitialized = true;

                if (AfterInitalization != null)
                    AfterInitalization(typeof(Xpcom), EventArgs.Empty);
            }
        }

        public static void InitializeThreadManager()
        {
            nsIThreadManager threadMan = Xpcom.GetService<nsIThreadManager>(Contracts.ThreadManager);
            using (var mainThread = new ComObject<nsIThread>(threadMan.GetMainThreadAttribute()))
            {
                Xpcom.FreeComObject(ref threadMan);

                _raiseEventOnMainThread = mainThread.GetDelegateForComMethod<nsIThread, nsIThreadDispatchDelegate>(new Action<nsIRunnable, uint>(mainThread.Instance.Dispatch));
                _mainThreadPtr = Marshal.GetComInterfaceForObject(mainThread.Instance, typeof(nsIEventTarget));
            }
        }

        public static string SDKDirectory
        {
            get;
            set;
        }

        public static string BFXComponentsDirectory
        {
            get;
            set;
        }
        #endregion

        //public static string XulfxPath
        //{
        //    get;
        //    set;
        //}


        


        public static string ComponentsPath
        {
            get;
            set;
        }


        public static void Shutdown(bool ns_shutdown)
		{
			if (_serviceManager == null)
				return;

			if (BeforeShutdown != null)
				BeforeShutdown(typeof(Xpcom), EventArgs.Empty);

            using (var appStartupSrv = Xpcom.GetService2<Gecko.Interfaces.nsIAppStartup>(Gecko.Contracts.AppStartup))
            {
                appStartupSrv.Instance.DestroyHiddenWindow();
            }

            IntPtr mainThreadPtr = Interlocked.Exchange(ref _mainThreadPtr, IntPtr.Zero);
			if (mainThreadPtr != IntPtr.Zero)
			{
				Marshal.Release(mainThreadPtr);
				mainThreadPtr = IntPtr.Zero;
			}

			if (_componentRegistrar != null)
				((IDisposable)_componentRegistrar).Dispose();

			if (_componentManager != null)
				((IDisposable)_componentManager).Dispose();

			_componentRegistrar = null;
			_componentManager = null;

			if (_serviceManager != null)
			{
                // NotifyObservers of shutdown so that NS_ShutdownXPCOM doesn't cause access violation.
                // (This has only been seen to happen the first time xulfx is run - (or if the.sqlite files are deleted)
                nsIObserverService svc = Xpcom.GetService<nsIObserverService>(Contracts.ObserverService);
                svc.NotifyObservers(null, "profile-change-net-teardown", "shutdown-persist");
				svc.NotifyObservers(null, "profile-change-teardown", "shutdown-persist");
				svc.NotifyObservers(null, "profile-before-change", "shutdown-persist");
				svc.NotifyObservers(null, "profile-before-change2", "shutdown-persist");
				svc = null;

                nsXULAppInfo.Instance.Shutdown();
                

				// NS_ShutdownXPCOM calls Release on the ServiceManager COM objects.
				// However since the ServiceManager is a __ComObject its finaliser will also call release.
				IntPtr pServMan = Marshal.GetIUnknownForObject(_serviceManager.Instance);
				((IDisposable)_serviceManager).Dispose();
				_serviceManager = null;

				GC.Collect();
				GC.WaitForPendingFinalizers();
                if(ns_shutdown)
				    NativeMethods.NS_ShutdownXPCOM(pServMan);
			}

			if (AfterShutdown != null)
				AfterShutdown(typeof(Xpcom), EventArgs.Empty);
		}

		private static void LoadXpcom(string binDirectory)
		{
			if (IsWindows)
			{
				Gecko.NativeMethods.Windows.SetDllDirectory(binDirectory);
			}
			else
			{
				// TODO: load all libs
				//throw new NotImplementedException();
			}

			try
			{
				ReadXulrunnerVersion(Path.Combine(binDirectory, NativeMethods.XpcomLib));
			}
			catch { }
		}

		private static void ReadXulrunnerVersion(string xulrunnerLibraryPath)
		{
			if (IsWindows)
			{
				_xulrunnerTimestamp = Utils.RetrieveLibraryLinkerTimestamp(xulrunnerLibraryPath);
				FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(xulrunnerLibraryPath);
				if (fileVersionInfo != null)
					_xulrunnerVersion = fileVersionInfo.ProductVersion;
			}
		}
        

  //      private static void OnProfileStartup(string libPath)
		//{
		//	nsIObserver addonsIntegration = null;

		//	ObserverService obsSvc = ObserverService.GetService();
		//	obsSvc.NotifyObservers(null, "profile-do-change", "startup");
		//	try
		//	{
  // //             const string xpifile = "XulFx.xpi";
  // //             string xulfxPath = Path.Combine(Path.GetDirectoryName(DirectoryServiceProvider.RuntimePath), xpifile);
  // //             if (!File.Exists(xulfxPath))
  // //                 xulfxPath = Path.Combine(libPath, xpifile);

  // //             if (!File.Exists(xulfxPath))
  // //                 throw new FileNotFoundException("File not found (" + xpifile + ").", xpifile);

  // //             Xpcom.LoadExtension(xulfxPath);


  //              addonsIntegration = Xpcom.GetService<nsIObserver>(Contracts.AddonsIntegration);
  //              if (addonsIntegration != null)
  //              {
  //                  addonsIntegration.Observe(null, "addons-startup", null);
  //              }

  //              obsSvc.NotifyObservers(null, "load-extension-defaults", null);
		//	}
		//	finally
		//	{
		//		Xpcom.FreeComObject(ref addonsIntegration);

		//		obsSvc.NotifyObservers(null, "profile-after-change", "startup");
		//		NS_CreateServicesFromCategory("profile-after-change", null, "profile-after-change");
		//		obsSvc.NotifyObservers(null, "profile-initial-state", null);
		//	}
		//}

		public static void NS_CreateServicesFromCategory(string category, nsISupports origin, string observerTopic)
		{
			nsICategoryManager catMan = null;
			nsISimpleEnumerator enumerator = null;
			nsIUTF8StringEnumerator senumerator = null;
			try
			{
				catMan = Xpcom.GetService<nsICategoryManager>(Contracts.CategoryManager);
				if (catMan == null)
					return;

				enumerator = catMan.EnumerateCategory(category);
				if (enumerator == null)
					return;

				senumerator = Xpcom.QueryInterface<nsIUTF8StringEnumerator>(enumerator);
				if (senumerator == null)
					return;

				while (senumerator.HasMore())
				{
					nsISupports serviceInstance = null;
					nsIObserver observer = null;
					try
					{
						string entryString = nsString.Get(senumerator.GetNext);
						string contractID = catMan.GetCategoryEntry(category, entryString);
						serviceInstance = Xpcom.GetService<nsISupports>(contractID);
						if (serviceInstance == null || observerTopic == null)
							continue;

						observer = Xpcom.QueryInterface<nsIObserver>(serviceInstance);
						if (observer == null)
							continue;

						observer.Observe(origin, observerTopic, "");
					}
					catch (OutOfMemoryException) { }
					catch (COMException) { }
					finally
					{
						Xpcom.FreeComObject(ref serviceInstance);
						Xpcom.FreeComObject(ref observer);
					}
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref catMan);
				Xpcom.FreeComObject(ref enumerator);
				Xpcom.FreeComObject(ref senumerator);
			}
		}

		[Conditional("DEBUG")]
		public static void Assert(bool condition, string format, params object[] args)
		{
			if (!condition)
			{
				if (args != null)
					Console.WriteLine(format, args);
				else
					Console.WriteLine(format);
			}
		}

		[Conditional("DEBUG")]
		public static void DebugPrint(object obj)
		{
			DebugPrint(obj != null ? obj.ToString() : "<null>", null);
		}

		[Conditional("DEBUG")]
		public static void DebugPrint(string format, params object[] args)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				if (args != null)
					Debug.Print(format, args);
				else
					Debug.WriteLine(format);
			}
			else
			{
				if (args != null)
					Console.WriteLine(format, args);
				else
					Console.WriteLine(format);
			}
		}

        public static void AssertCorrectThread()
        {
            //if (Thread.CurrentThread.ManagedThreadId != _XpcomThreadId)
            //{
            //    throw new InvalidOperationException("XulFx can only be called from the same thread on which it was initialized (normally the UI thread).");
            //}
        }

        public static bool InvokeRequired
		{
			get
			{
                return false;// Thread.CurrentThread.ManagedThreadId != _XpcomThreadId;
			}
		}

		#endregion

		public static IntPtr Alloc(int size)
		{
			return NativeMethods.Alloc(new IntPtr(size));
		}

		public static IntPtr Realloc(IntPtr ptr, int size)
		{
			return NativeMethods.Realloc(ptr, new IntPtr(size));
		}

		public static void Free(IntPtr ptr)
		{
			NativeMethods.Free(ptr);
		}

		private static DirectoryServiceProvider DirectoryServiceProvider
		{
			get
			{
				if (_directoryServiceProvider == null)
				{
					lock (syncRoot)
					{
						if (_directoryServiceProvider == null)
							_directoryServiceProvider = new DirectoryServiceProvider();
					}
				}
				return _directoryServiceProvider;
			}
		}

		public static XPConnectService XPConnect
		{
			get { return XPConnectService.GetService(); }
		}

		//public static XulfxAppInfo AppInfo
		//{
		//	get
		//	{
		//		AssertCorrectThread();

		//		if(_appInfo == null)
		//		{
		//			_appInfo = new XulfxAppInfo();
		//		}
		//		return _appInfo;
		//	}
		//	set
		//	{
		//		if (value == null)
		//			throw new ArgumentNullException("value");
		//		if (_appInfo != null)
		//			throw new InvalidOperationException();

		//		_appInfo = value;
		//	}
		//}

		public static string RuntimeVersion
		{
			get { return _xulrunnerVersion; }
		}

		public static string RuntimeBuildID
		{
			get { return _xulrunnerTimestamp != null ? _xulrunnerTimestamp.Value.ToString("yyyyMMddHHmmss") : null; }
		}

		//public static string ProfilePath
		//{
		//	get
		//	{
		//		return Xpcom.DirectoryServiceProvider.ProfilePath;
		//	}
		//	set
		//	{
		//		Xpcom.DirectoryServiceProvider.ProfilePath = value;
		//	}
		//}
        public static string ProfileName
        {
            get
            {
                return Xpcom.DirectoryServiceProvider.ProfileName;
            }
            set
            {
                Xpcom.DirectoryServiceProvider.ProfileName = value;
            }
        }


        public static Action<string> MemoryPressureCallback { get; set; }

		public static ComObject<nsIFile> OpenFile(string filename)
		{
			IntPtr nsFilePtr = DirectoryServiceProvider.NewLocalFileAsNsPtr(filename);
			try
			{
				return new ComObject<nsIFile>((nsIFile)Marshal.GetTypedObjectForIUnknown(nsFilePtr, typeof(nsIFile)));
			}
			finally
			{
				Marshal.Release(nsFilePtr);
			}
		}

		public static ComObject<nsIComponentRegistrar> ComponentRegistrar
		{
			get { return _componentRegistrar; }
		}

		public static ComObject<nsIComponentManager> ComponentManager
		{
			get { return _componentManager; }
		}

     

        public static void Run()
		{
			if (Interlocked.Read(ref _modalLoopCounter) != 0)
				throw new InvalidOperationException("Starting a second message loop on a single thread is not a valid operation. Use Xpcom.ModalEventLoop instead.");
			
			_exitFlag = false;
			ModalEventLoop(() => !_exitFlag && _modalLoopCounter > 0);
		}

		public static void Exit()
		{
			_exitFlag = true;
		}

		internal static void SetException(Exception ex)
		{
			_mainThreadException = ex;
		}

		public static void ModalEventLoop(Func<bool> condition)
		{
			ModalEventLoop(thread => condition(), true);
		}

		public static void ModalEventLoop(Func<nsIThread, bool> condition, bool mayWait)
		{
			nsIThreadManager threadMan = Xpcom.GetService<nsIThreadManager>(Contracts.ThreadManager);
			nsIThread thread = threadMan.GetCurrentThreadAttribute();
			Xpcom.FreeComObject(ref threadMan);

			Interlocked.Increment(ref _modalLoopCounter);
			try
			{
				while (condition(thread))
				{
					_mainThreadException = null;
					bool isProcessed = thread.ProcessNextEvent(mayWait);
					Exception ex = _mainThreadException;
					_mainThreadException = null;
					if (ex != null)
						throw ex;
					if (!isProcessed)
						break;

				}
			}
			finally
			{
				Interlocked.Decrement(ref _modalLoopCounter);
				Xpcom.FreeComObject(ref thread);
			}
		}

		public static void RaiseIdle(EventArgs e)
		{
			IntPtr mainThreadPtr = _mainThreadPtr;
			if (mainThreadPtr == IntPtr.Zero)
				throw new InvalidOperationException();

			var syncTask = new XpcomIdleEvent(ea => Idle(Thread.CurrentThread, ea), e);
			int errorCode = _raiseEventOnMainThread(_mainThreadPtr, syncTask, nsIEventTargetConsts.DISPATCH_SYNC);
			if (errorCode != 0)
			{
				Exception exception = syncTask.Exception;
				if (exception != null)
					throw new TargetInvocationException(exception);
				Marshal.ThrowExceptionForHR(errorCode);
			}
		}

		/// <summary>
		/// Processes all events currently in the event queue.
		/// </summary>
		public static void DoEvents()
		{
			DoEvents(true);
		}

		/// <summary>
		/// Processes pending events.
		/// </summary>
		/// <param name="mayWait">
		/// A boolean parameter that if &quot;true&quot; indicates that the method may block
		/// the calling thread to wait for a pending event.
		/// </param>
		public static void DoEvents(bool mayWait)
		{
			nsIThreadManager threadMan = Xpcom.GetService<nsIThreadManager>(Contracts.ThreadManager);
			nsIThread thread = threadMan.GetCurrentThreadAttribute();
			Xpcom.FreeComObject(ref threadMan);
			if (thread == null)
				throw new InvalidOperationException();

			try
			{
				bool processed = true;
				while (processed && thread.HasPendingEvents())
				{
					_mainThreadException = null;
					processed = thread.ProcessNextEvent(mayWait);
					Exception ex = _mainThreadException;
					_mainThreadException = null;
					if (ex != null)
						throw ex;
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref thread);
			}
		}

		/// <summary>
		/// Executes the specified method on the main thread.
		/// </summary>
		/// <param name="func">A delegate that contains a method to be called in the main thread context.</param>
		[DebuggerNonUserCode]
		public static void Invoke(Action action)
		{
			if (Xpcom.InvokeRequired)
			{
				IntPtr mainThreadPtr = _mainThreadPtr;
				if (mainThreadPtr == IntPtr.Zero)
					throw new InvalidAsynchronousStateException("The destination thread no longer exists.");

				var syncTask = new SyncTask((t) => action(), false);
				int errorCode = _raiseEventOnMainThread(_mainThreadPtr, syncTask, nsIEventTargetConsts.DISPATCH_SYNC);
				if (errorCode != 0)
				{
					Exception exception = syncTask.Exception;
					if (exception != null)
						throw new TargetInvocationException(exception);
					Marshal.ThrowExceptionForHR(errorCode);
				}

			}
			else
			{
				action();
			}
		}

		/// <summary>
		/// Executes the specified method on the main thread.
		/// </summary>
		/// <param name="func">A delegate that contains a method to be called in the main thread context.</param>
		[DebuggerNonUserCode]
		public static T Invoke<T>(Func<T> func)
		{
			if (!Xpcom.InvokeRequired) return func();

			IntPtr mainThreadPtr = _mainThreadPtr;
			if (mainThreadPtr == IntPtr.Zero)
				throw new InvalidAsynchronousStateException("The destination thread no longer exists.");

			var syncTask = new SyncTask((t) => t.State = func(), false);
			int errorCode = _raiseEventOnMainThread(_mainThreadPtr, syncTask, nsIEventTargetConsts.DISPATCH_SYNC);
			if (errorCode != 0)
			{
				Exception exception = syncTask.Exception;
				if (exception != null)
					throw new TargetInvocationException(exception);
				Marshal.ThrowExceptionForHR(errorCode);
			}
			return (T)syncTask.State;
		}

		/// <summary>
		/// Dispatches an asynchronous message to a synchronization context.
		/// </summary>
		/// <param name="func">A delegate that contains a method to be called in the main thread context.</param>
		internal static void Post(Action action)
		{
			IntPtr mainThreadPtr = _mainThreadPtr;
			if (mainThreadPtr == IntPtr.Zero)
				throw new InvalidAsynchronousStateException("The destination thread no longer exists.");
				
			Marshal.ThrowExceptionForHR(_raiseEventOnMainThread(_mainThreadPtr, new SyncTask((t) => action(), true), nsIEventTargetConsts.DISPATCH_NORMAL));
		}

		#region QueryInterface

		/// <summary>
		/// A special declaration of nsIInterfaceRequestor used only for QueryInterface, using PreserveSig
		/// to prevent .NET from throwing an exception when the interface doesn't exist.
		/// </summary>
		[Guid("033a1470-8b2a-11d3-af88-00a024ffc08c"), ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		interface QI_nsIInterfaceRequestor
		{

			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[PreserveSig]
			int GetInterface(ref Guid uuid, out IntPtr pUnk);
		}

		public static IntPtr QueryInterfaceForObject(object obj, Guid iid)
		{
			AssertCorrectThread();

			if (obj == null)
				return IntPtr.Zero;

			// get an nsISupports (aka IUnknown) pointer from the object
			IntPtr pUnk = Marshal.GetIUnknownForObject(obj);
			if (pUnk == IntPtr.Zero)
				return IntPtr.Zero;

			// query interface
			IntPtr ppv;
			Marshal.QueryInterface(pUnk, ref iid, out ppv);

			// if QueryInterface didn't work, try using nsIInterfaceRequestor instead
			if (ppv == IntPtr.Zero)
			{
				// QueryInterface the object for nsIInterfaceRequestor
				Guid interfaceRequestorIID = typeof(nsIInterfaceRequestor).GUID;
				IntPtr pInterfaceRequestor;
				Marshal.QueryInterface(pUnk, ref interfaceRequestorIID, out pInterfaceRequestor);

				// if we got a pointer to nsIInterfaceRequestor
				if (pInterfaceRequestor != IntPtr.Zero)
				{
					// convert it to a managed interface
					object comObject = Marshal.GetObjectForIUnknown(pInterfaceRequestor);
					try
					{
						QI_nsIInterfaceRequestor req = comObject as QI_nsIInterfaceRequestor;
						if (req != null)
						{

							try
							{
								req.GetInterface(ref iid, out ppv);
							}
							catch (NullReferenceException ex)
							{
								Debug.WriteLine("NullRefException from native code.\n" + ex.ToString());
							}
						}
						else if (comObject != null)
						{
							nsIInterfaceRequestor req2 = (nsIInterfaceRequestor)comObject;
							try
							{
								ppv = req2.GetInterface(ref iid);
							}
							catch (InvalidCastException)
							{
								ppv = IntPtr.Zero;
							}
							catch (NullReferenceException ex)
							{
								Debug.WriteLine("NullRefException from native code.\n" + ex.ToString());
							}
						}
					}
					finally
					{
						if (Marshal.IsComObject(comObject))
							Xpcom.ReleaseComObject(comObject);
						Marshal.Release(pInterfaceRequestor);
					}
				}
			}

			Marshal.Release(pUnk);
			return ppv;
		}

		public static object QueryInterface(object obj, Guid iid)
		{
			IntPtr ppv = Xpcom.QueryInterfaceForObject(obj, iid);
			if (ppv == IntPtr.Zero)
				return null;

			object result = Marshal.GetObjectForIUnknown(ppv);
			Marshal.Release(ppv);
			return result;
		}

		public static TInterface QueryInterface<TInterface>(object obj)
			where TInterface : class
		{
			Type interfaceType = typeof(TInterface);
			IntPtr ppv = Xpcom.QueryInterfaceForObject(obj, interfaceType.GUID);
			if (ppv == IntPtr.Zero)
				return null;

			TInterface result = (TInterface)Marshal.GetTypedObjectForIUnknown(ppv, interfaceType);
			Marshal.Release(ppv);
			return result;
		}

		public static ComObject<TInterface> QueryInterface2<TInterface>(object obj)
			where TInterface : class
		{
			return QueryInterface<TInterface>(obj).AsComObject<TInterface>();
		}

		#endregion

		///	<summary>
		/// Helper method for GeckoWeakReference
		///	</summary>
		internal static IntPtr QueryReferent(object obj, ref Guid uuid)
		{
			Xpcom.AssertCorrectThread();

			IntPtr ppv, pUnk = Marshal.GetIUnknownForObject(obj);
			Marshal.QueryInterface(pUnk, ref uuid, out ppv);
			Marshal.Release(pUnk);
			return ppv;

		}

		public static IntPtr GetService(Guid classIID)
		{
			AssertCorrectThread();

			Guid iid = typeof(nsISupports).GUID;
			return _serviceManager.Instance.GetService(ref classIID, ref iid);
		}

		public static TInterfaceType GetService<TInterfaceType>(string contractID)
		{
			AssertCorrectThread();

			Guid iid = typeof(TInterfaceType).GUID;
			IntPtr pUnk = _serviceManager.Instance.GetServiceByContractID(contractID, ref iid);
			TInterfaceType result = (TInterfaceType)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(TInterfaceType));
			Marshal.Release(pUnk);
			return result;
		}

        public static TInterfaceType GetService<TInterfaceType>(Guid classIID)
        {
            AssertCorrectThread();

            Guid iid = typeof(nsISupports).GUID;
            IntPtr pUnk = _serviceManager.Instance.GetService(ref classIID, ref iid);
            TInterfaceType result = (TInterfaceType)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(TInterfaceType));
            Marshal.Release(pUnk);
            return result;
        }

        public static ComObject<TInterfaceType> GetService2<TInterfaceType>(string contractID)
			where TInterfaceType : class
		{
			return new ComObject<TInterfaceType>(GetService<TInterfaceType>(contractID));
		}

        public static ComObject<TInterfaceType> GetService2<TInterfaceType>(Guid classIID)
        where TInterfaceType : class
        {
            return new ComObject<TInterfaceType>(GetService<TInterfaceType>(classIID));
        }

        public static TInterfaceType CreateInstance<TInterfaceType>(string contractID)
		{
			AssertCorrectThread();

			Guid iid = typeof(TInterfaceType).GUID;
			IntPtr pUnk = _componentManager.Instance.CreateInstanceByContractID(contractID, null, ref iid);
			TInterfaceType instance = (TInterfaceType)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(TInterfaceType));
			Marshal.Release(pUnk);
			return instance;
		}

        public static TInterfaceType CreateInstance<TInterfaceType>(Guid classIID)
        {
            AssertCorrectThread();

            Guid iid = typeof(TInterfaceType).GUID;
            IntPtr pUnk = _componentManager.Instance.CreateInstance(classIID, null, ref iid);
            TInterfaceType instance = (TInterfaceType)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(TInterfaceType));
            Marshal.Release(pUnk);
            return instance;
        }

        public static ComObject<TInterfaceType> CreateInstance2<TInterfaceType>(string contractID)
			where TInterfaceType : class
		{
			return new ComObject<TInterfaceType>(CreateInstance<TInterfaceType>(contractID));
		}

        public static ComObject<TInterfaceType> CreateInstance2<TInterfaceType>(Guid classIID)
            where TInterfaceType : class
        {
            return new ComObject<TInterfaceType>(CreateInstance<TInterfaceType>(classIID));
        }

        /// <summary>
        /// Registers a factory to be used to instantiate a particular class identified by ClassID, and creates an association of class name and ContractID with the class.
        /// </summary>
        /// <param name="classID">The ClassID of the class being registered.</param>
        /// <param name="className">The name of the class being registered. This value is intended as a human-readable name for the class and need not be globally unique.</param>
        /// <param name="contractID">The ContractID of the class being registered.</param>
        /// <param name="factory">The nsIFactory instance of the class being registered.</param>
        public static void RegisterFactory(Guid classID, string className, string contractID, nsIFactory factory)
		{
			Xpcom.AssertCorrectThread();
			ComponentRegistrar.Instance.RegisterFactory(ref classID, className, contractID, factory);
		}

		public static void UnregisterFactory(Guid classID, nsIFactory factory)
		{
			Xpcom.AssertCorrectThread();
			ComponentRegistrar.Instance.UnregisterFactory(ref classID, factory);
		}

		public static bool IsContractIDRegistered(string contractID)
		{
			Xpcom.AssertCorrectThread();
			return ComponentRegistrar.Instance.IsContractIDRegistered(contractID);
		}

		public static void RegisterInstance(Guid classID, string className, string contractID, object instance)
		{
			Xpcom.RegisterFactory(classID, className, contractID, new XulfxSingletonFactory(instance));
		}

		/// <summary>
		/// Loads extension from the specified directory or XPI file.
		/// </summary>
		/// <param name="extensionPath">Path to specified directory or XPI file</param>
		public static void LoadExtension(string extensionPath, string chromeFilename)
        {
			if (extensionPath == null)
				throw new ArgumentNullException("extensionPath");

			extensionPath = Path.GetFullPath(extensionPath);

			if (extensionPath.EndsWith(".xpi", StringComparison.OrdinalIgnoreCase))
			{
				if (!File.Exists(extensionPath))
					throw new FileNotFoundException(string.Format("Could not find file '{0}'.", extensionPath));
				using (var xpiFile = OpenFile(extensionPath))
				{
					Xpcom.ComponentManager.Instance.AddBootstrappedManifestLocation(xpiFile.Instance);
					int result = NativeMethods.XRE_AddJarManifestLocation(0, xpiFile.Instance);
					Marshal.ThrowExceptionForHR(result);
				}
			}
			else
			{
				if (!Directory.Exists(extensionPath))
					throw new DirectoryNotFoundException(string.Format("Could not find a part of the path '{0}'", extensionPath));

                string chromeFile = Path.Combine(extensionPath, chromeFilename);
                if (!File.Exists(chromeFile))
                    throw new FileNotFoundException(string.Format("Could not find file '{0}'.", chromeFile));

                using (var extensionDirectory = OpenFile(extensionPath))
				{
					Xpcom.ComponentManager.Instance.AddBootstrappedManifestLocation(extensionDirectory.Instance);
				}
				using (var manifestFile = OpenFile(chromeFile))
				{
					Xpcom.ComponentRegistrar.Instance.AutoRegister(manifestFile.Instance);
				}
			}
		}


        public static void FreeComObject<T>(ref T obj) where T : class
		{
			// take it to local variable
			var localObj = Interlocked.Exchange(ref obj, null);
			// if it is already null -> return
			if (localObj == null) return;

			if (Marshal.IsComObject(localObj))
			{
				if(Xpcom.IsMono)
				{
					if(Xpcom.InvokeRequired)
						Xpcom.InvokeFreeComObject(ref localObj);
					else
						Xpcom.ReleaseComObject(localObj);
				}
				else
				{
					Marshal.ReleaseComObject(localObj);
				}
			}
		}

		private static void InvokeFreeComObject<T>(ref T obj)
		{

		}

		public static void ReleaseComObject(object comObject)
		{
			if (!Xpcom.IsMono)
				Marshal.ReleaseComObject(comObject);

		}

    }


    //public class nsBrowserHandler :
    //   nsICommandLineHandler,
    //   nsIBrowserHandler,
    //   nsIContentHandler,
    //   nsICommandLineValidator
    //{
    //    private static nsBrowserHandler _instancs;
    //    public static nsBrowserHandler Instance
    //    {
    //        get
    //        {
    //            if (_instancs == null) _instancs = new nsBrowserHandler();
    //            return _instancs;
    //        }
    //    }

    //    public void GetHelpInfoAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
    //    {

    //    }


    //    public void Handle([MarshalAs(UnmanagedType.Interface)] nsICommandLine cmdLine)
    //    {
    //        // In the past, when an instance was not already running, the -remote
    //        // option returned an error code. Any script or application invoking the
    //        // -remote option is expected to be handling this case, otherwise they
    //        // wouldn't be doing anything when there is no Firefox already running.
    //        // Making the -remote option always return an error code makes those
    //        // scripts or applications handle the situation as if Firefox was not
    //        // already running.

    //        using (var aFlag = new nsAString())
    //        {
    //            //handleFlag
    //            //browser
    //            //remote
    //            //preferences
    //            //silent
    //            //private-window
    //            //private
    //            aFlag.SetData("browser");
    //            if (cmdLine.HandleFlag(aFlag, false))
    //            {

    //            }

    //            aFlag.SetData("remote");
    //            if (cmdLine.HandleFlag(aFlag, false))
    //            {

    //            }

    //            aFlag.SetData("preferences");
    //            if (cmdLine.HandleFlag(aFlag, false))
    //            {

    //            }

    //            aFlag.SetData("silent");
    //            if (cmdLine.HandleFlag(aFlag, false))
    //            {

    //            }

    //            aFlag.SetData("private-window");
    //            if (cmdLine.HandleFlag(aFlag, false))
    //            {

    //            }

    //            aFlag.SetData("private");
    //            if (cmdLine.HandleFlag(aFlag, false))
    //            {

    //            }

    //            //handleFlagWithParam
    //            //new-window
    //            //new-tab
    //            //chrome
    //            //search
    //            //file
    //            using (var result = new nsAString(""))
    //            {
    //                string resString = "";

    //                aFlag.SetData("new-window");
    //                result.SetData("");
    //                cmdLine.HandleFlagWithParam(aFlag, false, result);
    //                resString = result.ToString();
    //                if (resString != "" && resString != null)
    //                {

    //                }
                    
    //                aFlag.SetData("new-tab");
    //                result.SetData("");
    //                cmdLine.HandleFlagWithParam(aFlag, false, result);
    //                resString = result.ToString();
    //                if (resString != "" && resString != null)
    //                {

    //                }

    //                aFlag.SetData("chrome");
    //                result.SetData("");
    //                cmdLine.HandleFlagWithParam(aFlag, false, result);
    //                resString = result.ToString();
    //                if (resString != "" && resString != null)
    //                {

    //                }

    //                aFlag.SetData("search");
    //                result.SetData("");
    //                cmdLine.HandleFlagWithParam(aFlag, false, result);
    //                resString = result.ToString();
    //                if (resString != "" && resString != null)
    //                {

    //                }

    //                aFlag.SetData("file");
    //                result.SetData("");
    //                cmdLine.HandleFlagWithParam(aFlag, false, result);
    //                resString = result.ToString();
    //                if (resString != "" && resString != null)
    //                {

    //                }
    //            }
    //        }
    //    }



    //    public void GetDefaultArgsAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
    //    {
    //        result.SetData("about:home");
    //    }

    //    public void GetFeatures([MarshalAs(UnmanagedType.Interface)] nsICommandLine aCmdLine, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
    //    {
    //    }

    //    public void GetStartPageAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
    //    {
    //    }

    //    public void SetDefaultArgsAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase value)
    //    {
    //    }

    //    public void SetStartPageAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase value)
    //    {
    //    }











    //    public void HandleContent([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aContentType, [MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor aWindowContext, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest)
    //    {

    //    }









    //    public void Validate([MarshalAs(UnmanagedType.Interface)] nsICommandLine aCommandLine)
    //    {
    //        using (var aFlag = new nsAString())
    //        {
    //            // Other handlers may use osint so only handle the osint flag if the url
    //            // flag is also present and the command line is valid.
    //            aFlag.SetData("osint");
    //            var osintFlagIdx = aCommandLine.FindFlag(aFlag, false);

    //            aFlag.SetData("url");
    //            var urlFlagIdx = aCommandLine.FindFlag(aFlag, false);

    //            if (urlFlagIdx > -1 && (osintFlagIdx > -1 || aCommandLine.GetStateAttribute() == nsICommandLineConsts.STATE_REMOTE_EXPLICIT))
    //            {
    //                //var urlParam = aCommandLine.getArgument(urlFlagIdx + 1);
    //                //if (aCommandLine.length != urlFlagIdx + 2 || / firefoxurl:/.test(urlParam))
    //                //        throw NS_ERROR_ABORT;
    //                //var isDefault = false;
    //                //try
    //                //{
    //                //    var url = Services.urlFormatter.formatURLPref("app.support.baseURL") +
    //                //              "win10-default-browser";
    //                //    if (urlParam == url)
    //                //    {
    //                //        isDefault = ShellService.isDefaultBrowser(false, false);
    //                //    }
    //                //}
    //                //catch (ex) { }
    //                //if (isDefault)
    //                //{
    //                //    // Firefox is already the default HTTP handler.
    //                //    // We don't have to show the instruction page.
    //                //    throw NS_ERROR_ABORT;
    //                //}
    //                //cmdLine.handleFlag("osint", false)
    //            }
    //        }
    //    }
    //}



    //public class nsSessionStartup : nsISessionStartup
    //{
    //    private static nsSessionStartup _instancs;
    //    public static nsSessionStartup Instance
    //    {
    //        get
    //        {
    //            if (_instancs == null)
    //            {
    //                _instancs = new nsSessionStartup();
    //                _instancs.Init();
    //            }
    //            return _instancs;
    //        }
    //    }
    //    public void Init()
    //    {
    //        nsObserverService.ObserverService.NotifyObservers(null, "sessionstore-init-started", null);
    //        nsObserverService.ObserverService.NotifyObservers(null, "sessionstore-state-finalized", null);
    //    }
    //    //@mozilla.org/browser/sessionstartup;1
    //    [return: MarshalAs(UnmanagedType.U1)]
    //    public bool DoRestore()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public JSVal GetOnceInitializedAttribute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [return: MarshalAs(UnmanagedType.U1)]
    //    public bool GetPreviousSessionCrashedAttribute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public uint GetSessionTypeAttribute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public JSVal GetStateAttribute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [return: MarshalAs(UnmanagedType.U1)]
    //    public bool GetWillOverrideHomepageAttribute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [return: MarshalAs(UnmanagedType.U1)]
    //    public bool IsAutomaticRestoreEnabled()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class nsObserverService2
    {
        private static nsObserverService2 _instancs;
        public static nsObserverService2 Instance
        {
            get
            {
                if (_instancs == null) _instancs = new nsObserverService2();
                return _instancs;
            }
        }

        private static nsIObserverService observerService;
        public static nsIObserverService ObserverService
        {
            get
            {
                if (observerService == null)
                {
                    observerService = Xpcom.GetService<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService);
                }
                return observerService;
            }
        }

        public void addObserver(nsIObserver anObserver, string aTopic)
        {
            ObserverService.AddObserver(anObserver, aTopic, false);
        }

        public void addObserver(nsIObserver oba, string aTopic, bool ownsWeak)
        {
            ObserverService.AddObserver(oba, aTopic, ownsWeak);
        }
    }

    //  public class nsBrowserGlue : nsIBrowserGlue, nsIObserver, nsISupportsWeakReference
    //  {
    //      private static nsBrowserGlue _instancs;
    //      public static nsBrowserGlue Instance
    //      {
    //          get
    //          {
    //              if (_instancs == null) _instancs = new nsBrowserGlue();
    //              return _instancs;
    //          }
    //      }

    //      private GeckoWeakReference _weakRef;
    //public GeckoWeakReference WeakReference
    //{
    //	get { return _weakRef ?? (_weakRef = new GeckoWeakReference(this)); }
    //	protected set { _weakRef = value; }
    //}

    //      nsIWeakReference nsISupportsWeakReference.GetWeakReference()
    //      {
    //          return this.WeakReference;
    //      }

    //      public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
    //      {

    //      }

    //      public void Sanitize([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aParentWindow)
    //      {

    //      }
    //  }





}
