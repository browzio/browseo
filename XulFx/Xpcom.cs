using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Services;
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
		private static XulfxAppInfo _appInfo;
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

				if (Xpcom.IsLinux) // Init xpcom glue & memory
					NativeMethods.XulFx_Init();

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

				using (var windowWatcher = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
				{
					windowWatcher.Instance.SetWindowCreator(WindowCreator.Instance);
				}

				_IsInitialized = true;

				XulfxAppInfo appInfo = Xpcom.AppInfo;
				appInfo.Init();
				Xpcom.RegisterInstance(typeof(nsIXULRuntime).GUID, "XulfxAppInfo", Contracts.AppInfo, appInfo);

				OnProfileStartup(libPath);

				DefaultPromptFactory.Init();

				ObserverService.GetService().AddObserver(new MemoryPressureObserver(), "memory-pressure", false);

				if (AfterInitalization != null)
					AfterInitalization(typeof(Xpcom), EventArgs.Empty);
			}
		}

		public static void Shutdown()
		{
			if (_serviceManager == null)
				return;

			if (BeforeShutdown != null)
				BeforeShutdown(typeof(Xpcom), EventArgs.Empty);

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
				ObserverService svc = ObserverService.GetService();
				svc.NotifyObservers(null, "profile-change-net-teardown", "shutdown-persist");
				svc.NotifyObservers(null, "profile-change-teardown", "shutdown-persist");
				svc.NotifyObservers(null, "profile-before-change", "shutdown-persist");
				svc.NotifyObservers(null, "profile-before-change2", "shutdown-persist");
				svc = null;

				Xpcom.AppInfo.Shutdown();

				// NS_ShutdownXPCOM calls Release on the ServiceManager COM objects.
				// However since the ServiceManager is a __ComObject its finaliser will also call release.
				IntPtr pServMan = Marshal.GetIUnknownForObject(_serviceManager.Instance);
				((IDisposable)_serviceManager).Dispose();
				_serviceManager = null;

				GC.Collect();
				GC.WaitForPendingFinalizers();

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


		private static void OnProfileStartup(string libPath)
		{
			nsIObserver addonsIntegration = null;

			ObserverService obsSvc = ObserverService.GetService();
			obsSvc.NotifyObservers(null, "profile-do-change", "startup");
			try
			{
				const string xpifile = "XulFx.xpi";
				string xulfxPath = Path.Combine(Path.GetDirectoryName(DirectoryServiceProvider.RuntimePath), xpifile);
				if (!File.Exists(xulfxPath))
					xulfxPath = Path.Combine(libPath, xpifile);

				if (!File.Exists(xulfxPath))
					throw new FileNotFoundException("File not found (" + xpifile + ").", xpifile);
				
				Xpcom.LoadExtension(xulfxPath);
				

				addonsIntegration = Xpcom.GetService<nsIObserver>(Contracts.AddonsIntegration);
				if (addonsIntegration != null)
				{
					addonsIntegration.Observe(null, "addons-startup", null);
				}

				obsSvc.NotifyObservers(null, "load-extension-defaults", null);
			}
			finally
			{
				Xpcom.FreeComObject(ref addonsIntegration);

				obsSvc.NotifyObservers(null, "profile-after-change", "startup");
				NS_CreateServicesFromCategory("profile-after-change", null, "profile-after-change");
				obsSvc.NotifyObservers(null, "profile-initial-state", null);
			}
		}

		private static void NS_CreateServicesFromCategory(string category, nsISupports origin, string observerTopic)
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
			if (Thread.CurrentThread.ManagedThreadId != _XpcomThreadId)
			{
				throw new InvalidOperationException("XulFx can only be called from the same thread on which it was initialized (normally the UI thread).");
			}
		}

		public static bool InvokeRequired
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId != _XpcomThreadId;
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

		public static XulfxAppInfo AppInfo
		{
			get
			{
				AssertCorrectThread();

				if(_appInfo == null)
				{
					_appInfo = new XulfxAppInfo();
				}
				return _appInfo;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (_appInfo != null)
					throw new InvalidOperationException();

				_appInfo = value;
			}
		}

		public static string RuntimeVersion
		{
			get { return _xulrunnerVersion; }
		}

		public static string RuntimeBuildID
		{
			get { return _xulrunnerTimestamp != null ? _xulrunnerTimestamp.Value.ToString("yyyyMMddHHmmss") : null; }
		}

		public static string ProfilePath
		{
			get
			{
				return Xpcom.DirectoryServiceProvider.ProfilePath;
			}
			set
			{
				Xpcom.DirectoryServiceProvider.ProfilePath = value;
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

		public static ComObject<TInterfaceType> GetService2<TInterfaceType>(string contractID)
			where TInterfaceType : class
		{
			return new ComObject<TInterfaceType>(GetService<TInterfaceType>(contractID));
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

		public static ComObject<TInterfaceType> CreateInstance2<TInterfaceType>(string contractID)
			where TInterfaceType : class
		{
			return new ComObject<TInterfaceType>(CreateInstance<TInterfaceType>(contractID));
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
		public static void LoadExtension(string extensionPath)
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

				string chromeFile = Path.Combine(extensionPath, "chrome.manifest");
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
}
