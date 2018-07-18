using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gecko;
using Gecko.CustomMarshalers;
using SpiderMonkey;
using Gecko.Security;
using System.Reflection;
using System.Windows.Forms;

namespace BrowseoFX
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate int nsIThreadDispatchDelegate(IntPtr @this, [MarshalAs(UnmanagedType.Interface)] nsIRunnable @event, uint flags);

    public static class BrowseoFXpcomExtensions
    {
        /// <summary>
        /// Function that check if object is null -> then call wrapper creator
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TWrapper"></typeparam>
        /// <param name="comObj"></param>
        /// <param name="wrapper"></param>
        /// <returns></returns>
        public static TWrapper Wrap<TInterface, TWrapper>(this TInterface comObj, Func<TInterface, TWrapper> wrapper)
            where TInterface : class
            where TWrapper : class, IGeckoObjectWrapper//, IComObject
        {
            if (!typeof(IComObject).IsAssignableFrom(typeof(TWrapper)))
                throw new ArgumentOutOfRangeException("TWrapper");

            TWrapper wrappedObject = null;
            if (comObj != null)
            {
                wrappedObject = (TWrapper)GeckoObjectCache.Get<TInterface, TWrapper>(comObj);
                if (wrappedObject == null)
                {
                    wrappedObject = wrapper(comObj);
                    GeckoObjectCache.Set(comObj, wrappedObject);
                }
                else
                {
                    BrowseoFXpcom.FreeComObject(ref comObj);
                }
            }
            return wrappedObject;
        }

        public static ComObject<TInterface> AsComObject<TInterface>(this TInterface instance) where TInterface : class
        {
            if (instance == null) return null;
            return new ComObject<TInterface>(instance);
        }

        #region ComObject

        /// <summary>
        /// Method for getting specific function in com object
        /// </summary>
        /// <typeparam name="TDelegate"></typeparam>
        /// <param name="slot"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool GetDelegateForComMethod<TInterface, TDelegate>(this ComObject<TInterface> me, int slot, out TDelegate method)
            where TInterface : class
            where TDelegate : class
        {
            IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(me._instance, typeof(TInterface));
            if (comInterfaceForObject == IntPtr.Zero)
            {
                method = null;
                return false;
            }
            bool flag = false;
            try
            {
                IntPtr ptr = Marshal.ReadIntPtr(Marshal.ReadIntPtr(comInterfaceForObject, 0), slot * IntPtr.Size);
                method = (TDelegate)(object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate));
                flag = true;
            }
            finally
            {
                Marshal.Release(comInterfaceForObject);
            }
            return flag;
        }

        public static TDelegate GetDelegateForComMethod<TInterface, TDelegate>(this ComObject<TInterface> me, Delegate method)
            where TInterface : class
            where TDelegate : class
        {
            int slot = GetSlotOfComMethod<TInterface>(method);
            //BrowseoFXpcom.DebugPrint(
            //    "Slot for {0}.{1} = {2}",
            //    typeof(TInterface).Name,
            //    typeof(TDelegate).Name.Replace(typeof(TInterface).Name, string.Empty).Replace("Delegate", string.Empty),
            //    slot);

            TDelegate @delegate;
            if (!GetDelegateForComMethod<TInterface, TDelegate>(me, slot, out @delegate))
                throw new MethodAccessException();
            return @delegate;
        }

        private static int GetSlotOfComMethod<TInterface>(Delegate method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            MethodInfo methodInfo = method.Method;
            if (methodInfo.DeclaringType != typeof(TInterface))
                throw new ArgumentOutOfRangeException("method");
            return Marshal.GetComSlotForMethodInfo(methodInfo);
        }

        #endregion
    }

    public class NativeMethods
    {
        public const int NS_OK = 0;

        public class windows
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetDllDirectory(string lpPathName);

            //[DllImport("kernel32.dll", SetLastError = true)]
            //public unsafe static extern uint HeapSetInformation(IntPtr HeapHandle,int HeapInformationClass,void* HeapInformation,uint HeapInformationLength);
        }

        public class xpcom
        {
            public const string XPCOM = "xul.dll";

            /// <summary>
            /// Declaration in nsXPCOM.h
            /// XPCOM_API(nsresult) NS_NewLocalFile(const nsAString &amp;path, bool followLinks, nsIFile* *result);
            /// </summary>
            /// <param name="path"></param>
            /// <param name="followLinks"></param>
            /// <param name="result"></param>
            /// <returns></returns>
            [DllImport(XPCOM, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int NS_NewLocalFile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AStringMarshaler))] Gecko.nsAString path, [MarshalAs(UnmanagedType.U1)] bool followLinks, [MarshalAs(UnmanagedType.Interface)] out Gecko.Interfaces.nsIFile result);
            [DllImport(XPCOM, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int NS_NewLocalFile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AStringMarshaler))] Gecko.nsAString path, [MarshalAs(UnmanagedType.U1)] bool followLinks, out IntPtr result);

            /// <summary>
            /// Declaration in nsXPCOM.h
            /// XPCOM_API(nsresult) NS_InitXPCOM2(nsIServiceManager* *result, nsIFile* binDirectory, nsIDirectoryServiceProvider* appFileLocationProvider);
            /// </summary>
            /// <param name="serviceManager"></param>
            /// <param name="binDirectory"></param>
            /// <param name="appFileLocationProvider"></param>
            /// <returns></returns>
            [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int NS_InitXPCOM2([MarshalAs(UnmanagedType.Interface)] out Gecko.Interfaces.nsIServiceManager serviceManager, [MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIFile binDirectory, Gecko.Interfaces.nsIDirectoryServiceProvider appFileLocationProvider);

            /// <summary>
            /// Declaration in nsXPCOM.h
            /// XPCOM_API(nsresult) NS_GetComponentManager(nsIComponentManager* *result);
            /// </summary>
            /// <param name="componentManager"></param>
            /// <returns></returns>
            [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int NS_GetComponentManager([MarshalAs(UnmanagedType.Interface)] out Gecko.Interfaces.nsIComponentManager componentManager);


            /// <summary>
            /// Declaration in nsXPCOM.h
            /// XPCOM_API(nsresult) NS_GetComponentRegistrar(nsIComponentRegistrar* *result);
            /// </summary>
            /// <param name="componentRegistrar"></param>
            /// <returns></returns>
            [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int NS_GetComponentRegistrar([MarshalAs(UnmanagedType.Interface)] out Gecko.Interfaces.nsIComponentRegistrar componentRegistrar);

            [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int NS_GetServiceManager([MarshalAs(UnmanagedType.Interface)] out Gecko.Interfaces.nsIServiceManager servicemanager);

            [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int PREF_ClearAllUserPrefs();

            /// <summary>
            /// Declaration in nsXULAppAPI.h
            /// nsresult XRE_AddJarManifestLocation(NSLocationType aType, nsIFile* aLocation)
            /// </summary>
            /// <param name="aType"></param>
            /// <param name="aLocation"></param>
            /// <returns></returns>
            [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            internal static extern int XRE_AddJarManifestLocation(int aType, [MarshalAs(UnmanagedType.Interface)] nsIFile aLocation);

            //[DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //internal static extern int XRE_InitCommandLine(int aArgc, out char[] aArgv);
        }

    }

    public interface IGeckoObjectWrapper
    {
    }

    internal interface IComObject
    {
        object NativeInstance { get; }
        Type GetComObjectType();
    }

    internal static class GeckoObjectCache
    {
        internal struct CacheKey
        {
            private IntPtr _iUnknown;
            private Type _comObjectType;

            public CacheKey(IntPtr iUnknown, Type comObjectType)
            {
                _iUnknown = iUnknown;
                _comObjectType = comObjectType;
            }

            public IntPtr IUnknown
            {
                get { return _iUnknown; }
            }

            public override int GetHashCode()
            {
                return _iUnknown.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is CacheKey)
                {
                    CacheKey key = (CacheKey)obj;
                    return _iUnknown == key._iUnknown && (_comObjectType.IsAssignableFrom(key._comObjectType) || key._comObjectType.IsAssignableFrom(_comObjectType));
                }
                return false;
            }
        }

        private static readonly Dictionary<CacheKey, WeakReference> _cache = new Dictionary<CacheKey, WeakReference>();
        private static readonly ReaderWriterLockSlim syncRoot = new ReaderWriterLockSlim();

        internal static CacheKey GetKey(object comObject, Type typeOfWrapper)
        {
            IntPtr pUnk = Marshal.GetIUnknownForObject(comObject);
            Marshal.Release(pUnk);
            return new CacheKey(pUnk, typeOfWrapper);
        }

        public static object Get<TInterface, TWrapper>(TInterface comObject)
            where TInterface : class
            where TWrapper : IGeckoObjectWrapper
        {
            WeakReference weakRef;

            CacheKey key = GetKey(comObject, typeof(TWrapper));

            syncRoot.EnterReadLock();
            try
            {
                if (!_cache.TryGetValue(key, out weakRef))
                    return null;
            }
            finally
            {
                syncRoot.ExitReadLock();
            }

            return weakRef.Target;
        }

        public static CacheKey Set<TInterface>(TInterface comObject, IGeckoObjectWrapper wrapper)
            where TInterface : class
        {
            WeakReference weakRef = new WeakReference(wrapper);

            CacheKey key = GetKey(comObject, ((IComObject)wrapper).GetComObjectType());

            syncRoot.EnterWriteLock();
            try
            {
                _cache[key] = weakRef;
            }
            finally
            {
                syncRoot.ExitWriteLock();
            }
            return key;
        }

        public static void Remove(CacheKey key)
        {
            syncRoot.EnterWriteLock();
            try
            {
                _cache.Remove(key);
            }
            finally
            {
                syncRoot.ExitWriteLock();
            }

        }
    }

    public class ComObject<TInterface> : IDisposable, IEquatable<ComObject<TInterface>>, IEquatable<TInterface>, IComObject
        where TInterface : class
    {

        internal TInterface _instance;
        private GeckoObjectCache.CacheKey _cacheKey;

        #region ctor & dtor

        public ComObject(TInterface instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            _instance = instance;


            IGeckoObjectWrapper wrapper = this as IGeckoObjectWrapper;
            if (wrapper != null)
                _cacheKey = GeckoObjectCache.Set(instance, wrapper);
        }

        ~ComObject()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this is IGeckoObjectWrapper)
                GeckoObjectCache.Remove(_cacheKey);

            BrowseoFXpcom.FreeComObject(ref _instance);
            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        void IDisposable.Dispose()
        {
            if (_instance != null)
            {
                Dispose(true);
            }
        }

        #endregion

        public TInterface Instance
        {
            get
            {
                BrowseoFXpcom.AssertCorrectThread();
                if (_instance == null)
                    throw new ObjectDisposedException(this.GetType().Name);

                return _instance;
            }
        }

        protected IntPtr Unknown
        {
            get
            {
                if (_instance == null)
                    throw new ObjectDisposedException(this.GetType().Name);

                if (_cacheKey.IUnknown != IntPtr.Zero)
                    return _cacheKey.IUnknown;

                if (this is IGeckoObjectWrapper)
                    throw new InvalidOperationException(); // the key must be created in the constructor

                _cacheKey = GeckoObjectCache.GetKey(_instance, this.GetType());
                return _cacheKey.IUnknown;
            }
        }

        public T QueryInterface<T>()
            where T : class
        {
            return BrowseoFXpcom.QueryInterface<T>(this.Instance);
        }

        object IComObject.NativeInstance
        {
            get
            {
                return this.Instance;
            }
        }

        Type IComObject.GetComObjectType()
        {
            return typeof(ComObject<TInterface>);
        }

        #region Equality

        public bool Equals(ComObject<TInterface> other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return this.Unknown == other.Unknown;
        }

        public bool Equals(TInterface other)
        {
            if (ReferenceEquals(null, other)) return false;
            IntPtr pUnk = Marshal.GetIUnknownForObject(other);
            Marshal.Release(pUnk);
            return this.Unknown == pUnk;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (!(obj is ComObject<TInterface>)) return false;
            return this.Unknown == ((ComObject<TInterface>)obj).Unknown;
        }

        public override int GetHashCode()
        {
            return this.Unknown.GetHashCode();
        }


        #endregion
    }

    public static class BrowseoFXpcom
    {
        public static ComObject<Gecko.Interfaces.nsIServiceManager> ServiceManager;
        public static ComObject<Gecko.Interfaces.nsIComponentManager> ComponentManager;
        public static ComObject<Gecko.Interfaces.nsIComponentRegistrar> ComponentRegistrar;

        public static int _XpcomThreadId;

        private static long _modalLoopCounter;
        private static volatile Exception _mainThreadException;







        //get services
        public static IntPtr GetService(Guid classIID)
        {
            AssertCorrectThread();

            Guid iid = typeof(Gecko.Interfaces.nsISupports).GUID;
            return ServiceManager.Instance.GetService(ref classIID, ref iid);
        }

        public static TInterfaceType GetService<TInterfaceType>(string contractID)
        {
            AssertCorrectThread();

            Guid iid = typeof(TInterfaceType).GUID;
            IntPtr pUnk = ServiceManager.Instance.GetServiceByContractID(contractID, ref iid);
            TInterfaceType result = (TInterfaceType)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(TInterfaceType));
            Marshal.Release(pUnk);
            return result;
        }

        public static ComObject<TInterfaceType> GetService2<TInterfaceType>(string contractID) where TInterfaceType : class
        {
            return new ComObject<TInterfaceType>(GetService<TInterfaceType>(contractID));
        }





        //Create
        public static TInterfaceType CreateInstance<TInterfaceType>(string contractID)
        {
            AssertCorrectThread();

            Guid iid = typeof(TInterfaceType).GUID;
            IntPtr pUnk = ComponentManager.Instance.CreateInstanceByContractID(contractID, null, ref iid);
            TInterfaceType instance = (TInterfaceType)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(TInterfaceType));
            Marshal.Release(pUnk);

            //todo IntPtr cUnk = ComponentManager.Instance.GetClassObjectByContractID(contractID, ref iid);
            return instance;
        }

        public static ComObject<TInterfaceType> CreateInstance2<TInterfaceType>(string contractID)
            where TInterfaceType : class
        {
            return new ComObject<TInterfaceType>(CreateInstance<TInterfaceType>(contractID));
        }

        public static void NS_CreateServicesFromCategory(string category, Gecko.Interfaces.nsISupports origin, string observerTopic)
        {
            Gecko.Interfaces.nsICategoryManager catMan = null;
            Gecko.Interfaces.nsISimpleEnumerator enumerator = null;
            Gecko.Interfaces.nsIUTF8StringEnumerator senumerator = null;
            try
            {
                catMan = BrowseoFXpcom.GetService<Gecko.Interfaces.nsICategoryManager>(Gecko.Contracts.CategoryManager);
                if (catMan == null)
                    return;

                enumerator = catMan.EnumerateCategory(category);
                if (enumerator == null)
                    return;

                senumerator = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsIUTF8StringEnumerator>(enumerator);
                if (senumerator == null)
                    return;

                while (senumerator.HasMore())
                {
                    Gecko.Interfaces.nsISupports serviceInstance = null;
                    Gecko.Interfaces.nsIObserver observer = null;
                    try
                    {
                        string entryString = Gecko.nsString.Get(senumerator.GetNext);
                        string contractID = catMan.GetCategoryEntry(category, entryString);
                        serviceInstance = BrowseoFXpcom.GetService<Gecko.Interfaces.nsISupports>(contractID);
                        if (serviceInstance == null || observerTopic == null)
                            continue;

                        observer = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsIObserver>(serviceInstance);
                        if (observer == null)
                            continue;

                        observer.Observe(origin, observerTopic, "");
                    }
                    catch (OutOfMemoryException) { }
                    catch (COMException) { }
                    finally
                    {
                        BrowseoFXpcom.FreeComObject(ref serviceInstance);
                        BrowseoFXpcom.FreeComObject(ref observer);
                    }
                }
            }
            finally
            {
                BrowseoFXpcom.FreeComObject(ref catMan);
                BrowseoFXpcom.FreeComObject(ref enumerator);
                BrowseoFXpcom.FreeComObject(ref senumerator);
            }
        }





        //Register
        /// <summary>
		/// Registers a factory to be used to instantiate a particular class identified by ClassID, and creates an association of class name and ContractID with the class.
		/// </summary>
		/// <param name="classID">The ClassID of the class being registered.</param>
		/// <param name="className">The name of the class being registered. This value is intended as a human-readable name for the class and need not be globally unique.</param>
		/// <param name="contractID">The ContractID of the class being registered.</param>
		/// <param name="factory">The nsIFactory instance of the class being registered.</param>
		public static void RegisterFactory(Guid classID, string className, string contractID, Gecko.Interfaces.nsIFactory factory)
        {
            BrowseoFXpcom.AssertCorrectThread();
            ComponentRegistrar.Instance.RegisterFactory(ref classID, className, contractID, factory);
        }

        public static void RegisterInstance(Guid classID, string className, string contractID, object instance)
        {
            BrowseoFXpcom.RegisterFactory(classID, className, contractID, new nsSingletonFactory(instance));
        }





        //queryInterface
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
                Guid interfaceRequestorIID = typeof(Gecko.Interfaces.nsIInterfaceRequestor).GUID;
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
                            Gecko.Interfaces.nsIInterfaceRequestor req2 = (Gecko.Interfaces.nsIInterfaceRequestor)comObject;
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
                            BrowseoFXpcom.ReleaseComObject(comObject);
                        Marshal.Release(pInterfaceRequestor);
                    }
                }
            }

            Marshal.Release(pUnk);
            return ppv;
        }

        public static TInterface QueryInterface<TInterface>(object obj)
            where TInterface : class
        {
            Type interfaceType = typeof(TInterface);
            IntPtr ppv = BrowseoFXpcom.QueryInterfaceForObject(obj, interfaceType.GUID);
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

        ///	<summary>
        /// Helper method for GeckoWeakReference
        ///	</summary>
        internal static IntPtr QueryReferent(object obj, ref Guid uuid)
        {
            BrowseoFXpcom.AssertCorrectThread();

            IntPtr ppv, pUnk = Marshal.GetIUnknownForObject(obj);
            Marshal.QueryInterface(pUnk, ref uuid, out ppv);
            Marshal.Release(pUnk);
            return ppv;

        }





        //free objects
        public static void FreeComObject<T>(ref T obj) where T : class
        {
            // take it to local variable
            var localObj = Interlocked.Exchange(ref obj, null);
            // if it is already null -> return
            if (localObj == null) return;

            if (Marshal.IsComObject(localObj))
            {
                Marshal.ReleaseComObject(localObj);
            }
        }

        public static void ReleaseComObject(object comObject)
        {
            Marshal.ReleaseComObject(comObject);
        }





        public static void AssertCorrectThread()
        {
            if (Thread.CurrentThread.ManagedThreadId != _XpcomThreadId)
            {
                throw new InvalidOperationException("XulFx can only be called from the same thread on which it was initialized (normally the UI thread).");
            }
        }

        public static void ModalEventLoop(Func<bool> condition)
        {
            ModalEventLoop(thread => condition(), true);
        }

        public static void ModalEventLoop(Func<Gecko.Interfaces.nsIThread, bool> condition, bool mayWait)
        {
            Gecko.Interfaces.nsIThreadManager threadMan = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIThreadManager>(Gecko.Contracts.ThreadManager);
            Gecko.Interfaces.nsIThread thread = threadMan.GetCurrentThreadAttribute();
            BrowseoFXpcom.FreeComObject(ref threadMan);

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
                BrowseoFXpcom.FreeComObject(ref thread);
            }
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
                using (var xpiFile = nsXREDirProvider.Instance.OpenFile(extensionPath))
                {
                    ComponentManager.Instance.AddBootstrappedManifestLocation(xpiFile.Instance);
                    int result = NativeMethods.xpcom.XRE_AddJarManifestLocation(0, xpiFile.Instance);
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
                using (var extensionDirectory = nsXREDirProvider.Instance.OpenFile(extensionPath))
                {
                    ComponentManager.Instance.AddBootstrappedManifestLocation(extensionDirectory.Instance);
                }
                using (var manifestFile = nsXREDirProvider.Instance.OpenFile(chromeFile))
                {
                    ComponentRegistrar.Instance.AutoRegister(manifestFile.Instance);
                }
            }
        }

    }

    public class nsXULAppInfo : Gecko.Interfaces.nsIXULAppInfo,
                               Gecko.Interfaces.nsIObserver,
                               Gecko.Interfaces.nsIWinAppHelper,
                               Gecko.Interfaces.nsICrashReporter,
                               Gecko.Interfaces.nsIFinishDumpingCallback,
                               Gecko.Interfaces.nsIXULRuntime
    {
        private static nsXULAppInfo _instancs;
        public static nsXULAppInfo Instance
        {
            get
            {
                if (_instancs == null) _instancs = new nsXULAppInfo();
                return _instancs;
            }
        }
        static ulong gBrowserTabsRemoteStatus = 1;

        #region nsIXULAppInfo
        public void GetVendorAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase result)
        {
            result.SetData("Mozilla");
        }

        public void GetNameAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase result)
        {
            result.SetData("Firefox");
        }

        public void GetIDAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase result)
        {
            result.SetData("{ec8030f7-c20a-464f-9b0e-13a3a9e97384}");
        }

        public void GetVersionAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase result)
        {
            result.SetData("52.0");
        }

        public void GetPlatformVersionAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase result)
        {
            result.SetData("52.0");
        }

        public void GetAppBuildIDAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase result)
        {
            result.SetData("20170302120751");
        }

        public void GetPlatformBuildIDAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase result)
        {
            result.SetData("20170302120751");
        }

        public void GetUANameAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase result)
        {
            result.SetData("Firefox");
        }
        #endregion




        #region nsIObserver
        public void Observe([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.WStringMarshaler))] string aData)
        {
            Gecko.Interfaces.nsISupportsPRUint64 ret = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsISupportsPRUint64>(aSubject);
            ret.SetDataAttribute(gBrowserTabsRemoteStatus);
        }
        #endregion




        #region nsIWinAppHelper
        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetUserCanElevateAttribute()
        {
            return true;
        }
        #endregion




        #region nsIFinishDumpingCallback
        public void Callback([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsISupports data)
        {

        }
        #endregion




        #region nsIXULRuntime
        private static Gecko.Interfaces.nsIXULRuntime _instance;
        private Gecko.Interfaces.nsIXULRuntime RuntimeInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIXULRuntime>(Gecko.Contracts.Runtime);
                }
                return _instance;
            }
        }

        bool logerrors = false;

        public virtual bool GetInSafeModeAttribute()
        {
            //return RuntimeInstance.GetInSafeModeAttribute();
            return false;
        }

        public virtual bool GetLogConsoleErrorsAttribute()
        {
            //return RuntimeInstance.GetLogConsoleErrorsAttribute();
            return logerrors;
        }

        public virtual void SetLogConsoleErrorsAttribute(bool aLogConsoleErrors)
        {
            //  RuntimeInstance.SetLogConsoleErrorsAttribute(aLogConsoleErrors);
            logerrors = aLogConsoleErrors;
        }

        public virtual void GetOSAttribute(Gecko.nsAUTF8StringBase aOS)
        {
            aOS.SetData("WINNT");
           // RuntimeInstance.GetOSAttribute(aOS);
        }

        public virtual void GetXPCOMABIAttribute(Gecko.nsAUTF8StringBase aXPCOMABI)
        {
            aXPCOMABI.SetData("");
           // RuntimeInstance.GetXPCOMABIAttribute(aXPCOMABI);
        }

        public virtual void GetWidgetToolkitAttribute(Gecko.nsAUTF8StringBase aWidgetToolkit)
        {
            aWidgetToolkit.SetData("windows");
           // RuntimeInstance.GetWidgetToolkitAttribute(aWidgetToolkit);
        }

        //const unsigned long PROCESS_TYPE_DEFAULT = 0;
        //const unsigned long PROCESS_TYPE_PLUGIN = 1;
        //const unsigned long PROCESS_TYPE_CONTENT = 2;
        //const unsigned long PROCESS_TYPE_IPDLUNITTEST = 3;
        //const unsigned long PROCESS_TYPE_GMPLUGIN = 4;
        //const unsigned long PROCESS_TYPE_GPU = 5;
        public virtual uint GetProcessTypeAttribute()
        {
            // return RuntimeInstance.GetProcessTypeAttribute();
            return 0;
        }

        public virtual void InvalidateCachesOnRestart()
        {
            RuntimeInstance.InvalidateCachesOnRestart();
        }

        public virtual void EnsureContentProcess()
        {
            RuntimeInstance.EnsureContentProcess();
        }

        public virtual void GetLastRunCrashIDAttribute(Gecko.nsAStringBase aLastRunCrashID)
        {
            RuntimeInstance.GetLastRunCrashIDAttribute(aLastRunCrashID);
        }

        public virtual uint GetProcessIDAttribute()
        {
            return RuntimeInstance.GetProcessIDAttribute();
        }

        public virtual bool GetBrowserTabsRemoteAutostartAttribute()
        {
            // return RuntimeInstance.GetBrowserTabsRemoteAutostartAttribute();
            return false;
        }

        public virtual bool GetAccessibilityEnabledAttribute()
        {
            // return RuntimeInstance.GetAccessibilityEnabledAttribute();
            return false;
        }

        public virtual ulong GetReplacedLockTimeAttribute()
        {
            try
            {
               return RuntimeInstance.GetReplacedLockTimeAttribute();
            }
            catch
            {
                return 0x80040111;
            }
        }

        public virtual bool GetIsReleaseOrBetaAttribute()
        {
            return false;
        }

        public virtual bool GetIsOfficialBrandingAttribute()
        {
            return true;
        }

        public virtual void GetDefaultUpdateChannelAttribute(Gecko.nsAUTF8StringBase result)
        {
            result.SetData("");
        //    RuntimeInstance.GetDefaultUpdateChannelAttribute(result);
        }

        public virtual void GetDistributionIDAttribute(Gecko.nsAUTF8StringBase result)
        {
            result.SetData("org.mozilla");
          // var d = result.ToString();
           // RuntimeInstance.GetDistributionIDAttribute(result);
        }

        public virtual bool GetIsOfficialAttribute()
        {
            //return RuntimeInstance.GetIsOfficialAttribute();
            return true;
        }

        public bool GetIs64BitAttribute()
        {
            return false;
        }

        public virtual ulong GetUniqueProcessIDAttribute()
        {
             return RuntimeInstance.GetUniqueProcessIDAttribute();
            //return 0;
        }

        public virtual uint GetMultiprocessBlockPolicyAttribute()
        {
            return RuntimeInstance.GetMultiprocessBlockPolicyAttribute();
        }

        public virtual bool GetWindowsDLLBlocklistStatusAttribute()
        {
            return RuntimeInstance.GetWindowsDLLBlocklistStatusAttribute();
        }
        #endregion





        #region nsICrashReporter
        private static Gecko.Interfaces.nsICrashReporter _nsICrashReporterinstance;
        private Gecko.Interfaces.nsICrashReporter CrashReporterInstance
        {
            get
            {
                if (_nsICrashReporterinstance == null)
                {
                    _nsICrashReporterinstance = BrowseoFXpcom.GetService<Gecko.Interfaces.nsICrashReporter>(Gecko.Contracts.CrashReporter);
                }
                return _nsICrashReporterinstance;
            }
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetEnabledAttribute()
        {
            return false;
        }

        public void SetEnabled([MarshalAs(UnmanagedType.U1)] bool enabled)
        {
            CrashReporterInstance.SetEnabled(false);
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public Gecko.Interfaces.nsIURL GetServerURLAttribute()
        {
            return CrashReporterInstance.GetServerURLAttribute();
        }

        public void SetServerURLAttribute([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIURL value)
        {
            CrashReporterInstance.SetServerURLAttribute(value);
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public Gecko.Interfaces.nsIFile GetMinidumpPathAttribute()
        {
            return CrashReporterInstance.GetMinidumpPathAttribute();
        }

        public void SetMinidumpPathAttribute([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIFile value)
        {
            CrashReporterInstance.SetMinidumpPathAttribute(value);
        }

        public void AnnotateCrashReport([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AUTF8StringMarshaler))] Gecko.nsAUTF8StringBase key, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AUTF8StringMarshaler))] Gecko.nsAUTF8StringBase data)
        {
            // CrashReporterInstance.AnnotateCrashReport(key, data);
        }

        public void AppendAppNotesToCrashReport([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase data)
        {
            CrashReporterInstance.AppendAppNotesToCrashReport(data);
        }

        public void RegisterAppMemory(ulong ptr, ulong size)
        {
            CrashReporterInstance.RegisterAppMemory(ptr, size);
        }

        public void WriteMinidumpForException(IntPtr aExceptionInfo)
        {
            CrashReporterInstance.WriteMinidumpForException(aExceptionInfo);
        }

        public void AppendObjCExceptionInfoToAppNotes(IntPtr aException)
        {
            CrashReporterInstance.AppendObjCExceptionInfoToAppNotes(aException);
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetSubmitReportsAttribute()
        {
            return CrashReporterInstance.GetSubmitReportsAttribute();
        }

        public void SetSubmitReportsAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            CrashReporterInstance.SetSubmitReportsAttribute(value);
        }

        public void UpdateCrashEventsDir()
        {
            CrashReporterInstance.UpdateCrashEventsDir();
        }

        public void SaveMemoryReport()
        {
            CrashReporterInstance.SaveMemoryReport();
        }

        public void SetTelemetrySessionId([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AUTF8StringMarshaler))] Gecko.nsAUTF8StringBase id)
        {
            CrashReporterInstance.SetTelemetrySessionId(id);
        }
        #endregion

    }

    //public class nsBrowserGlue : nsIBrowserGlue,
    //    nsIObserver,
    //    nsIMessageListener
    //{
    //    private static nsBrowserGlue _instancs;
    //    public static nsBrowserGlue Instance
    //    {
    //        get
    //        {
    //            if (_instancs == null) _instancs = new nsBrowserGlue();
    //            return _instancs;
    //        }
    //    }

    //    private nsBrowserGlue()
    //    {
    //        var os = nsObserverService.Instance;

    //        #region listeners
    //        var observerMessages = new string[] {
    //            "update-staged",
    //            "update-downloaded",
    //            "update-available",
    //            "update-error",
    //        };
    //        foreach (var message in observerMessages)
    //        {
    //            os.addObserver(this, message);
    //        }


    //        var mmMessages = new string[] {
    //                    "AboutHome:MaybeShowMigrateMessage",
    //                    "AboutHome:RequestUpdate",
    //                    "Content:Click",
    //                    "ContentSearch",
    //                    "FormValidation:ShowPopup",
    //                    "FormValidation:HidePopup",
    //                    "Prompt:Open",
    //                    "Reader:ArticleGet",
    //                    "Reader:FaviconRequest",
    //                    "Reader:UpdateReaderButton",
    //                    // PLEASE KEEP THIS LIST IN SYNC WITH THE LISTENERS ADDED IN LoginManagerParent.init
    //                    "RemoteLogins:findLogins",
    //                    "RemoteLogins:findRecipes",
    //                    "RemoteLogins:onFormSubmit",
    //                    "RemoteLogins:autoCompleteLogins",
    //                    "RemoteLogins:removeLogin",
    //                    "RemoteLogins:insecureLoginFormPresent",
    //                    // PLEASE KEEP THIS LIST IN SYNC WITH THE LISTENERS ADDED IN LoginManagerParent.init
    //                    "WCCR:registerProtocolHandler",
    //                    "WCCR:registerContentHandler",
    //                    "rtcpeer:CancelRequest",
    //                    "rtcpeer:Request",
    //                    "webrtc:CancelRequest",
    //                    "webrtc:Request",
    //                    "webrtc:StopRecording",
    //                    "webrtc:UpdateBrowserIndicators"
    //        };
    //        nsIMessageBroadcaster mm = BrowseoFXpcom.GetService<nsIMessageBroadcaster>(Contracts.GlobalMessageManager);
    //        //var FrameScriptLoader = BrowseoFXpcom.QueryInterface<nsIFrameScriptLoader>(mm); 
    //        foreach (var message in mmMessages)
    //        {
    //            using (var messageName = new nsAString(message))
    //            {
    //                mm.AddMessageListener(messageName, listener: this, listenWhenClosed: false);
    //            }
    //        }


    //        var ppmmMessages = new string[] {
    //            // PLEASE KEEP THIS LIST IN SYNC WITH THE LISTENERS ADDED IN ContentPrefServiceParent.init
    //            "ContentPrefs:FunctionCall",
    //            "ContentPrefs:AddObserverForName",
    //            "ContentPrefs:RemoveObserverForName",
    //            // PLEASE KEEP THIS LIST IN SYNC WITH THE LISTENERS ADDED IN ContentPrefServiceParent.init

    //            // PLEASE KEEP THIS LIST IN SYNC WITH THE LISTENERS ADDED IN AsyncPrefs.init
    //            "AsyncPrefs:SetPref",
    //            "AsyncPrefs:ResetPref",
    //            // PLEASE KEEP THIS LIST IN SYNC WITH THE LISTENERS ADDED IN AsyncPrefs.init

    //            "FeedConverter:addLiveBookmark",
    //            "WCCR:setAutoHandler",
    //            "webrtc:UpdateGlobalIndicators",
    //            "webrtc:UpdatingIndicators"
    //        };
    //        nsIMessageBroadcaster ppmm = BrowseoFXpcom.GetService<nsIMessageBroadcaster>("@mozilla.org/parentprocessmessagemanager;1");
    //        //var ProcessScriptLoader = BrowseoFXpcom.QueryInterface<nsIProcessScriptLoader>(ppmm); 
    //        foreach (var message in ppmmMessages)
    //        {
    //            using (var messageName = new nsAString(message))
    //            {
    //                ppmm.AddMessageListener(messageName, listener: this, listenWhenClosed: false);
    //            }
    //        }
    //        #endregion




    //        os.addObserver(this, "notifications-open-settings");
    //        os.addObserver(this, "prefservice:after-app-defaults");
    //        os.addObserver(this, "final-ui-startup");
    //        os.addObserver(this, "browser-delayed-startup-finished");
    //        os.addObserver(this, "sessionstore-windows-restored");
    //        os.addObserver(this, "browser:purge-session-history");
    //        os.addObserver(this, "quit-application-requested");
    //        os.addObserver(this, "quit-application-granted");
    //        os.addObserver(this, "browser-lastwindow-close-requested");
    //        os.addObserver(this, "browser-lastwindow-close-granted");
    //        os.addObserver(this, "weave:service:ready");
    //        os.addObserver(this, "fxaccounts:onverified");
    //        os.addObserver(this, "fxaccounts:device_connected");
    //        os.addObserver(this, "fxaccounts:verify_login");
    //        os.addObserver(this, "fxaccounts:device_disconnected");
    //        os.addObserver(this, "weave:engine:clients:display-uris");
    //        os.addObserver(this, "session-save");
    //        os.addObserver(this, "places-init-complete");
    //        os.addObserver(this, "distribution-customization-complete");
    //        os.addObserver(this, "handle-xul-text-link");
    //        os.addObserver(this, "profile-before-change");
    //        os.addObserver(this, "keyword-search");
    //        os.addObserver(this, "browser-search-engine-modified");
    //        os.addObserver(this, "restart-in-safe-mode");
    //        os.addObserver(this, "flash-plugin-hang");
    //        os.addObserver(this, "xpi-signature-changed");
    //        os.addObserver(this, "sync-ui-state:update");
    //    }

    //    public void Sanitize([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aParentWindow)
    //    {
    //    }

    //    public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
    //    {

    //    }

    //    public void ReceiveMessage()
    //    {

    //    }
    //}

    public class nsGlobalChromeWindow :
        nsIEventTarget,
        nsIDOMChromeWindow,
        nsISupportsWeakReference,
        nsIObserver, 
        nsIInterfaceRequestor,
        mozIDOMWindowProxy,
        nsPIDOMWindowOuter,
        nsIDOMWindow
    {
        #region singletone
        private static nsGlobalChromeWindow instance;
        public static nsGlobalChromeWindow Instance
        {
            get
            {
                if (instance == null) instance = new nsGlobalChromeWindow();
                return instance;
            }
        }
        private nsGlobalChromeWindow() { }
        #endregion

        public ushort WindowState { get; set; }
        public BrowseoFXpcomBareWindow Widget { get; set; }

        private nsIBrowserDOMWindow mBrowserDOMWindow;
        private mozIDOMWindowProxy mOpenerForInitialContentBrowser;

        //todo: nsIFrameScriptLoader.cpp
        private nsIMessageBroadcaster mMessageManager;
        //nsInterfaceHashtable<nsStringHashKey, nsIMessageBroadcaster> mGroupMessageManagers;
        private Dictionary<string, nsIMessageBroadcaster> mGroupMessageManagers;

        public void Init()
        {
            mGroupMessageManagers = new Dictionary<string, nsIMessageBroadcaster>();

            nsObserverService.ObserverService.AddObserver(this, "memory-pressure", false);
            nsObserverService.ObserverService.AddObserver(this, "network:offline-status-changed", false);

            Widget = new BrowseoFXpcomBareWindow(this);
            Widget.WaitUntilChromeLoad();
            Widget.Instance.SetXULBrowserWindowAttribute(new WebBrowserGlue());

            nsIWindowMediator wm = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIWindowMediator>(Contracts.WindowMediator);
          //  wm.UpdateWindowTitle(Widget.Instance, "yoyo");
           // wm.RegisterWindow(Widget.Instance);

            var win = wm.GetMostRecentWindow("navigator:browser");
            WindowState = nsIDOMChromeWindowConsts.STATE_NORMAL;
        }


        public void BeginWindowMove([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent mouseDownEvent, [MarshalAs(UnmanagedType.Interface)] nsIDOMElement panel)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void GetAttention()
        {
            GetAttentionWithCycleCount(-1);
        }

        public void GetAttentionWithCycleCount(int aCycleCount)
        {
            Widget.GetAttentionWithCycleCount(aCycleCount);
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIBrowserDOMWindow GetBrowserDOMWindowAttribute()
        {
            return mBrowserDOMWindow;
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIMessageBroadcaster GetMessageManagerAttribute()
        {
            //var globalMM = BrowseoFXpcom.GetService<nsIMessageBroadcaster>(Contracts.GlobalMessageManager);
            if (mMessageManager == null)
            {
                //nsIContentFrameMessageManager
                mMessageManager = BrowseoFXpcom.CreateInstance<nsIMessageBroadcaster>(Contracts.ChildProcessMessageManager);
            }

            return mMessageManager;
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIMessageBroadcaster GetGroupMessageManager([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase group)
        {
            var groupString = group.ToString();
            if (mGroupMessageManagers.ContainsKey(groupString))
            {
                return mGroupMessageManagers[groupString];
            }
            else
            {
                var groupManager = BrowseoFXpcom.CreateInstance<nsIMessageBroadcaster>(Contracts.ChildProcessMessageManager);
                mGroupMessageManagers.Add(groupString, groupManager);

                return groupManager;
            }
        }


        //public sealed class nsIDOMChromeWindowConsts
        //{
        //    public const ushort STATE_MAXIMIZED = 1;
        //    public const ushort STATE_MINIMIZED = 2;
        //    public const ushort STATE_NORMAL = 3;
        //    public const ushort STATE_FULLSCREEN = 4;
        //}
        public ushort GetWindowStateAttribute()
        {
            return WindowState;
        }

        public void Maximize()
        {
            WindowState = nsIDOMChromeWindowConsts.STATE_MAXIMIZED;
            //TODO rest
        }

        public void Minimize()
        {
            WindowState = nsIDOMChromeWindowConsts.STATE_MINIMIZED;
            //TODO rest
        }

        public void Restore()
        {
            //TODO 
            throw new NotImplementedException();
        }


        public void NotifyDefaultButtonLoaded([MarshalAs(UnmanagedType.Interface)] nsIDOMElement defaultButton)
        {
            //TODO
            //nsIDOMXULControlElement xulControl = BrowseoFXpcom.QueryInterface<nsIDOMXULControlElement>(defaultButton);
            //if (xulControl == null) return;

            //if (xulControl.GetDisabledAttribute()) return;

            //nsIFrame frame = defaultButton.getPr
        }

        public void SetBrowserDOMWindowAttribute([MarshalAs(UnmanagedType.Interface)] nsIBrowserDOMWindow value)
        {
            mBrowserDOMWindow = value;
        }

        public void SetCursor([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase cursor)
        {
            //TODO 
            throw new NotImplementedException();
        }

        public void SetOpenerForInitialContentBrowser([MarshalAs(UnmanagedType.Interface)] mozIDOMWindowProxy aOpener)
        {
            if (mOpenerForInitialContentBrowser == null)
                mOpenerForInitialContentBrowser = aOpener;
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public mozIDOMWindowProxy TakeOpenerForInitialContentBrowser()
        {
            if (mOpenerForInitialContentBrowser == null)
            {
                mOpenerForInitialContentBrowser = BrowseoFXpcom.QueryInterface<mozIDOMWindowProxy>(Widget.Instance);
                return mOpenerForInitialContentBrowser;
            }
            else
            {
                BrowseoFXpcom.FreeComObject(ref mOpenerForInitialContentBrowser);
                mOpenerForInitialContentBrowser = null;
                return TakeOpenerForInitialContentBrowser();
            }
        }




        public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
        {
            if (Widget.window != null)
            {
                var observer = BrowseoFXpcom.QueryInterface<nsIObserver>(Widget.window);
                if(observer != null) observer.Observe(aSubject, aTopic, aData);
            }
        }



        public IntPtr GetInterface(ref Guid uuid)
        {
            if (Widget.window != null)
            {
                if (uuid == typeof(nsIDOMWindow).GUID || uuid == typeof(nsPIDOMWindowOuter).GUID)
                {
                    IntPtr pType = BrowseoFXpcom.QueryInterfaceForObject(Widget.window.Instance, uuid);
                    if (pType != IntPtr.Zero)
                        return pType;
                }
            }

            return IntPtr.Zero;
        }



        #region nsISupportsWeakReference

        private nsWeakReference _weakRef;
        public Gecko.Interfaces.nsIWeakReference GetWeakReference()
        {
            return _weakRef ?? (_weakRef = new nsWeakReference(this));
        }


        #endregion

        [return: MarshalAs(UnmanagedType.U1)]
        public bool IsOnCurrentThread()
        {
            return true;
        }

        public void DispatchFromC(IntPtr @event, uint flags)
        {
        }

        public void Dispatch([MarshalAs(UnmanagedType.Interface)] nsIRunnable @event, uint flags)
        {
        }

        public void DelayedDispatch(IntPtr @event, uint delay)
        {
        }
    }

    
    public class BrowseoFXpcomBareWindow : ComObject<Gecko.Interfaces.nsIXULWindow>, Gecko.Interfaces.nsIWebProgressListener, Gecko.Interfaces.nsISupportsWeakReference, nsIDOMEventListener
    {
        public static string xulWindowUrl = @"chrome://browser/content/browser.xul";
        public ComObject<Gecko.Interfaces.nsIBaseWindow> _baseWindow;
        public ComObject<Gecko.Interfaces.nsIDocShell> _docshell;
        private ComObject<Gecko.Interfaces.nsIWebProgress> _progress;


        public GeckoWindow window;

        private nsWeakReference _weakRef;

        private bool _isChromeLoaded;

        private mozIDOMWindowProxy win;

        private static Gecko.Interfaces.nsIXULWindow CreateTopLevelWindow(mozIDOMWindowProxy win)
        {
            Gecko.Interfaces.nsIURI uri = null;
            Gecko.Interfaces.nsIIOService2 io = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIIOService2>(Gecko.Contracts.NetworkIOService);
            using (var str = new Gecko.nsAUTF8String(xulWindowUrl))//chrome://browser/content/preferences/preferences.xul chrome://browser/content/browser.xul //: 0x80520012: 0x80520012
            {
                uri = io.NewURI(str, null, null);
            }
            Gecko.Interfaces.nsIAppShellService appShell = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIAppShellService>(Gecko.Contracts.AppShellService);
            Gecko.Interfaces.nsIXULWindow mWindow = appShell.CreateTopLevelWindow(null, uri, (uint)Gecko.Interfaces.nsIWebBrowserChromeConsts.CHROME_ALL, Gecko.Interfaces.nsIAppShellServiceConsts.SIZE_TO_CONTENT, Gecko.Interfaces.nsIAppShellServiceConsts.SIZE_TO_CONTENT, null, null);
            

            BrowseoFXpcom.FreeComObject(ref appShell);
            BrowseoFXpcom.FreeComObject(ref io);
            BrowseoFXpcom.FreeComObject(ref uri);

            return mWindow;
        }

        public BrowseoFXpcomBareWindow()
            : base(CreateTopLevelWindow(null))
        {
            _baseWindow = BrowseoFXpcom.QueryInterface2<Gecko.Interfaces.nsIBaseWindow>(Instance);
            _baseWindow.Instance.SetFocus();

            //mozIDOMWindowProxy 
            mozIDOMWindowProxy thisWindow = BrowseoFXpcom.QueryInterface<mozIDOMWindowProxy>(Instance);
            var helper = BrowseoFXpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
            helper.Init(thisWindow);

            //mozIDOMWindowProxy GetRealInstanceAttribute
            mozIDOMWindowProxy domWindowProxy = helper.GetRealInstanceAttribute();
            //mozIDOMWindow
            mozIDOMWindow domWindow = helper.GetMozWindowAttribute();
            //nsPIDOMWindowOuter
            nsPIDOMWindowOuter outer = BrowseoFXpcom.QueryInterface<nsPIDOMWindowOuter>(domWindow);
            //nsIDOMDocument
            nsIDOMDocument domDoc = helper.GetDocumentAttribute();
            //nsIDOMEventTarget
            nsIDOMEventTarget domEventTarget = helper.GetWindowRootAttribute();
            //using (var nType = new nsAString("TabOpen"))
            //{
            //    domEventTarget.AddEventListener(nType, this, true, true, 2);
            //}
            //using (var nType = new nsAString("AppCommand"))
            //{
            //    domEventTarget.AddEventListener(nType, this, true, true, 2);
            //}
            //   < command id = "cmd_newNavigatorTab" oncommand = "BrowserOpenTab(event);" reserved = "true" />

            // < command id = "cmd_newNavigatorTabNoEvent" oncommand = "BrowserOpenTab();" reserved = "true" />



            //var chrome = creator.Instance.CreateChromeWindow(null, nsIWebBrowserChromeConsts.CHROME_ALL);
            //if (chrome != null)
            //{
            //    var window = BrowseoFXpcom.QueryInterface<nsIDOMWindow>(nsGlobalChromeWindow.Instance.Widget.Instance).Wrap(GeckoWindow.Create);
            //    var webnav = BrowseoFXpcom.QueryInterface<nsIWebNavigation>(window.Instance).Wrap(GeckoWebNavigation.Create);

            //    mozIDOMWindowProxy thisWindow = BrowseoFXpcom.QueryInterface<mozIDOMWindowProxy>(nsGlobalChromeWindow.Instance.Widget.Instance);
            //    var helper = BrowseoFXpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
            //    helper.Init(thisWindow);

            //    nsISupports comObj = helper.GetNavigatorAttribute();
            //    var navigator = Xpcom.QueryInterface<nsIDOMNavigator>(comObj);
            //    var browser = BrowseoFXpcom.QueryInterface<nsIWebBrowser>(navigator);

            //    chrome.SetWebBrowserAttribute(new nsWebBrowser());

            //    chrome.ShowAsModal();
            //}

           var requester = BrowseoFXpcom.QueryInterface<nsIInterfaceRequestor>(thisWindow);











            window = BrowseoFXpcom.QueryInterface<nsIDOMWindow>(Instance).Wrap(GeckoWindow.Create);
            var webnav = BrowseoFXpcom.QueryInterface<nsIWebNavigation>(window);

            var browser = BrowseoFXpcom.QueryInterface<nsIWebBrowser>(webnav);
            // var _chrome = browser.GetContainerWindowAttribute();

            _docshell = Instance.GetDocShellAttribute().AsComObject();

            var systemPrincipal = BrowseoFXpcom.GetService<nsIPrincipal>(Contracts.NullPrincipal);
            //_docshell.Instance.CreateAboutBlankContentViewer(systemPrincipal);

            _progress = _docshell.QueryInterface<Gecko.Interfaces.nsIWebProgress>().AsComObject();
            _progress.Instance.AddProgressListener(this, (uint)(Gecko.Interfaces.nsIWebProgressConsts.NOTIFY_ALL) | (uint)(Gecko.Interfaces.nsIWebProgressConsts.NOTIFY_STATE_ALL));


            // var _tabchild = _docshell.Instance.GetTabChildAttribute();
            //var tabchild = Marshal.GetTypedObjectForIUnknown(_tabchild, typeof(nsITabChild)) as nsITabChild; 


            //var window = BrowseoFXpcom.QueryInterface<nsIDOMWindow>(Instance);
            // var gWindow = GeckoWindow.Create(window);

            // var outer = BrowseoFXpcom.QueryInterface<nsPIDOMWindowOuter>(gWindow.Instance);
            // var eventTarget = BrowseoFXpcom.QueryInterface<nsIDOMEventTarget>(outer);
            // using (var nType = new nsAString("load"))
            // {
            //     eventTarget.AddEventListener(nType, this, true, true, 2);
            // }


            // var outer = _docshell.Instance.GetWindow();
            // gWindow.QueryInterface<nsIDocShell>().SetChromeEventHandlerAttribute()
            //var _eventTarget = gWindow.QueryInterface<nsIDocShell>().GetChromeEventHandlerAttribute().Wrap(GeckoDOMEventTarget.Create);

            //var _eventTarget = _docshell.Instance.GetChromeEventHandlerAttribute().Wrap(GeckoDOMEventTarget.Create);

            //   var domWindow = _docshell.Instance.GetWindow();
        }

        public BrowseoFXpcomBareWindow(mozIDOMWindowProxy win)
            : base(CreateTopLevelWindow(win))
        {
            this.win = win;
            window = BrowseoFXpcom.QueryInterface<nsIDOMWindow>(Instance).Wrap(GeckoWindow.Create);

            _baseWindow = BrowseoFXpcom.QueryInterface2<Gecko.Interfaces.nsIBaseWindow>(Instance);
            _baseWindow.Instance.SetFocus();


            //mozIDOMWindowProxy 
            mozIDOMWindowProxy thisWindow = BrowseoFXpcom.QueryInterface<mozIDOMWindowProxy>(Instance);
            var helper = BrowseoFXpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
            helper.Init(thisWindow);

            //mozIDOMWindowProxy GetRealInstanceAttribute
            mozIDOMWindowProxy domWindowProxy = helper.GetRealInstanceAttribute();
            //mozIDOMWindow
            mozIDOMWindow domWindow = helper.GetMozWindowAttribute();
            //nsPIDOMWindowOuter
            nsPIDOMWindowOuter outer = BrowseoFXpcom.QueryInterface<nsPIDOMWindowOuter>(domWindow);
            //nsIDOMDocument
            nsIDOMDocument domDoc = helper.GetDocumentAttribute();
            //nsIDOMEventTarget
            nsIDOMEventTarget domEventTarget = helper.GetWindowRootAttribute();
            //using (var nType = new nsAString("TabOpen"))
            //{
            //    domEventTarget.AddEventListener(nType, this, true, true, 2);
            //}
            //using (var nType = new nsAString("AppCommand"))
            //{
            //    domEventTarget.AddEventListener(nType, this, true, true, 2);
            //}
            //   < command id = "cmd_newNavigatorTab" oncommand = "BrowserOpenTab(event);" reserved = "true" />

            // < command id = "cmd_newNavigatorTabNoEvent" oncommand = "BrowserOpenTab();" reserved = "true" />


            var requester = BrowseoFXpcom.QueryInterface<nsIInterfaceRequestor>(thisWindow);
            var webNavigation = BrowseoFXpcom.QueryInterface<nsIWebNavigation>(requester);
            var loadContext = BrowseoFXpcom.QueryInterface<nsILoadContext>(webNavigation);
            var gmulty = loadContext.GetUseRemoteTabsAttribute();
            //    requester.GetInterface()








            var webnav = BrowseoFXpcom.QueryInterface<nsIWebNavigation>(window);
            var browser = BrowseoFXpcom.QueryInterface<nsIWebBrowser>(webnav);

            _docshell = Instance.GetDocShellAttribute().AsComObject();

            var systemPrincipal = BrowseoFXpcom.GetService<nsIPrincipal>(Contracts.NullPrincipal);
            //_docshell.Instance.CreateAboutBlankContentViewer(systemPrincipal);

            _progress = _docshell.QueryInterface<Gecko.Interfaces.nsIWebProgress>().AsComObject();
            _progress.Instance.AddProgressListener(this, (uint)(Gecko.Interfaces.nsIWebProgressConsts.NOTIFY_ALL) | (uint)(Gecko.Interfaces.nsIWebProgressConsts.NOTIFY_STATE_ALL));
        }

        public void WaitUntilChromeLoad()
        {
            BrowseoFXpcom.ModalEventLoop(() => !_isChromeLoaded);
        }

        public void ShowDialog()
        {
            _baseWindow.Instance.SetVisibilityAttribute(true);
            Instance.ShowModal();
        }


        #region nsIWebProgressListener
        private bool IsThisDomWindow(Gecko.Interfaces.nsIWebProgress aWebProgress)
        {
            bool result = false;
            if (aWebProgress.GetIsTopLevelAttribute())
            {
                Gecko.Interfaces.mozIDOMWindowProxy thisWindow = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.mozIDOMWindowProxy>(Instance);
                try
                {
                    Gecko.Interfaces.mozIDOMWindowProxy otherWindow = null;
                    try
                    {
                        otherWindow = aWebProgress.GetDOMWindowAttribute();
                        result = thisWindow == otherWindow;
                    }
                    finally
                    {
                        BrowseoFXpcom.FreeComObject(ref otherWindow);
                    }
                }
                finally
                {
                    BrowseoFXpcom.FreeComObject(ref thisWindow);
                }
            }
            return result;
        }

        public void OnLocationChange([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIRequest aRequest, [MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIURI aLocation, uint aFlags)
        {
            if (aWebProgress != null && IsThisDomWindow(aWebProgress))
                _isChromeLoaded = false;
        }

        public void OnProgressChange([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress)
        {
        }

        public void OnSecurityChange([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIRequest aRequest, uint aState)
        {
        }

        public void OnStateChange([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIRequest aRequest, uint aStateFlags, int aStatus)
        {
            // if the notification is not about a document finishing, then just ignore it...
            if ((aStateFlags & Gecko.Interfaces.nsIWebProgressListenerConsts.STATE_STOP) == 0 ||
                (aStateFlags & Gecko.Interfaces.nsIWebProgressListenerConsts.STATE_IS_NETWORK) == 0)
            {
                return;
            }

            // if this notification is not for this window then ignore it...
            if (aWebProgress != null && IsThisDomWindow(aWebProgress))
            {
                _isChromeLoaded = true;
            }
        }

        public void OnStatusChange([MarshalAs(UnmanagedType.Interface)]Gecko.Interfaces.nsIWebProgress aWebProgress, [MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIRequest aRequest, int aStatus, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.WStringMarshaler))] string aMessage)
        {
        }
        #endregion

        #region nsISupportsWeakReference

        public Gecko.Interfaces.nsIWeakReference GetWeakReference()
        {
            return _weakRef ?? (_weakRef = new nsWeakReference(this));
        }


        #endregion nsISupportsWeakReference

        public void HandleEvent([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent @event)
        {
        }

        internal void GetAttentionWithCycleCount(int aCycleCount)
        {
            //TODO
            throw new NotImplementedException();
        }
    }

    public class nsObserverService 
    {
        private static nsObserverService _instancs;
        public static nsObserverService Instance
        {
            get
            {
                if (_instancs == null) _instancs = new nsObserverService();
                return _instancs;
            }
        }

        private static nsIObserverService observerService;
        public static nsIObserverService ObserverService
        {
            get
            {
                if(observerService == null)
                {
                    observerService = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService);
                }
                return observerService;
            }
        }

        public void addObserver(nsIObserver anObserver, string aTopic)
        {
            ObserverService.AddObserver(anObserver, aTopic, false);
        }
    }

    public class nsBrowserGlue : nsIBrowserGlue, nsIObserver, nsISupportsWeakReference
    {
        private static nsBrowserGlue _instancs;
        public static nsBrowserGlue Instance
        {
            get
            {
                if (_instancs == null) _instancs = new nsBrowserGlue();
                return _instancs;
            }
        }

        private nsWeakReference _weakRef;
        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIWeakReference GetWeakReference()
        {
            return _weakRef ?? (_weakRef = new nsWeakReference(this));
        }

        public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
        {

        }

        public void Sanitize([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aParentWindow)
        {

        }
    }

    public class nsBrowserHandler :
        nsICommandLineHandler,
        nsIBrowserHandler,
        nsIContentHandler,
        nsICommandLineValidator
    {
        private static nsBrowserHandler _instancs;
        public static nsBrowserHandler Instance
        {
            get
            {
                if (_instancs == null) _instancs = new nsBrowserHandler();
                return _instancs;
            }
        }

        public void GetHelpInfoAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
        {

        }

        public void Handle([MarshalAs(UnmanagedType.Interface)] nsICommandLine cmdLine)
        {
            using (var aFlag = new nsAString())
            {
                aFlag.SetData("browser");
                if (cmdLine.HandleFlag(aFlag, false))
                {

                }

                // In the past, when an instance was not already running, the -remote
                // option returned an error code. Any script or application invoking the
                // -remote option is expected to be handling this case, otherwise they
                // wouldn't be doing anything when there is no Firefox already running.
                // Making the -remote option always return an error code makes those
                // scripts or applications handle the situation as if Firefox was not
                // already running.
                aFlag.SetData("remote");
                if (cmdLine.HandleFlag(aFlag, false))
                {

                }

                aFlag.SetData("preferences");
                if (cmdLine.HandleFlag(aFlag, false))
                {
                    //openPreferences();
                    //cmdLine.preventDefault = true;
                }

                aFlag.SetData("silent");
                if (cmdLine.HandleFlag(aFlag, false))
                {
                    //cmdLine.preventDefault = true;
                }

                using (var result = new nsAString("")) {

                        do
                        {
                        aFlag.SetData("new-window");
                        result.SetData("");
                        cmdLine.HandleFlagWithParam(aFlag, false, result);
                            if (result.ToString() == "" || result.ToString() == null) break;

                            //let uri = resolveURIInternal(cmdLine, uriparam);
                            //if (!shouldLoadURI(uri))
                            //    continue;

                            //openWindow(null, this.chromeURL, "_blank",
                            //           "chrome,dialog=no,all" + this.getFeatures(cmdLine),
                            //           uri.spec);
                            //cmdLine.preventDefault = true;

                        } while (true);



                        do
                        {

                        aFlag.SetData("new-tab");
                        result.SetData("");
                        cmdLine.HandleFlagWithParam(aFlag, false, result);
                        if (result.ToString() == "" || result.ToString() == null) break;

                        //let uri = resolveURIInternal(cmdLine, uriparam);
                        //handURIToExistingBrowser(uri, nsIBrowserDOMWindow.OPEN_NEWTAB, cmdLine);
                        //cmdLine.preventDefault = true;

                    } while (true);

                    do
                    {

                        aFlag.SetData("chrome");
                        result.SetData("");
                        cmdLine.HandleFlagWithParam(aFlag, false, result);
                        if (result.ToString() == "" || result.ToString() == null) break;


                        //// Handle old preference dialog URLs.
                        //if (chromeParam == "chrome://browser/content/pref/pref.xul" ||
                        //    chromeParam == "chrome://browser/content/preferences/preferences.xul")
                        //{
                        //    openPreferences();
                        //    cmdLine.preventDefault = true;
                        //}
                        //else try
                        //    {
                        //        let resolvedURI = resolveURIInternal(cmdLine, chromeParam);
                        //        let isLocal = uri => {
                        //        let localSchemes = new Set(["chrome", "file", "resource"]);
                        //        if (uri instanceof Components.interfaces.nsINestedURI) {
                        //            uri = uri.QueryInterface(Components.interfaces.nsINestedURI).innerMostURI;
                        //        }
                        //        return localSchemes.has(uri.scheme);
                        //    };
                        //if (isLocal(resolvedURI))
                        //{
                        //    // If the URI is local, we are sure it won't wrongly inherit chrome privs
                        //    var features = "chrome,dialog=no,all" + this.getFeatures(cmdLine);
                        //    openWindow(null, resolvedURI.spec, "_blank", features);
                        //    cmdLine.preventDefault = true;
                        //}
                        //else
                        //{
                        //    dump("*** Preventing load of web URI as chrome\n");
                        //    dump("    If you're trying to load a webpage, do not pass --chrome.\n");
                        //}

                    } while (true);







                //try
                //{
                //    var privateWindowParam = cmdLine.handleFlagWithParam("private-window", false);
                //    if (privateWindowParam)
                //    {
                //        let resolvedURI = resolveURIInternal(cmdLine, privateWindowParam);
                //        handURIToExistingBrowser(resolvedURI, nsIBrowserDOMWindow.OPEN_NEWTAB, cmdLine, true);
                //        cmdLine.preventDefault = true;
                //    }
                //}
                //catch (e)
                //{
                //    if (e.result != Components.results.NS_ERROR_INVALID_ARG)
                //    {
                //        throw e;
                //    }
                //    // NS_ERROR_INVALID_ARG is thrown when flag exists, but has no param.
                //    if (cmdLine.handleFlag("private-window", false))
                //    {
                //        openWindow(null, this.chromeURL, "_blank",
                //          "chrome,dialog=no,private,all" + this.getFeatures(cmdLine),
                //          "about:privatebrowsing");
                //        cmdLine.preventDefault = true;
                //    }
                //}

                //var searchParam = cmdLine.handleFlagWithParam("search", false);
                //if (searchParam)
                //{
                //    doSearch(searchParam, cmdLine);
                //    cmdLine.preventDefault = true;
                //}

                //// The global PB Service consumes this flag, so only eat it in per-window
                //// PB builds.
                //if (cmdLine.handleFlag("private", false))
                //{
                //    PrivateBrowsingUtils.enterTemporaryAutoStartMode();
                //}

                //var fileParam = cmdLine.handleFlagWithParam("file", false);
                //if (fileParam)
                //{
                //    var file = cmdLine.resolveFile(fileParam);
                //    var fileURI = Services.io.newFileURI(file);
                //    openWindow(null, this.chromeURL, "_blank",
                //               "chrome,dialog=no,all" + this.getFeatures(cmdLine),
                //               fileURI.spec);
                //    cmdLine.preventDefault = true;
                //}

                //if (AppConstants.platform == "win")
                //{
                //    // Handle "? searchterm" for Windows Vista start menu integration
                //    for (var i = cmdLine.length - 1; i >= 0; --i)
                //    {
                //        var param = cmdLine.getArgument(i);
                //        if (param.match(/^\? /))
                //        {
                //            cmdLine.removeArguments(i, i);
                //            cmdLine.preventDefault = true;

                //            searchParam = param.substr(2);
                //            doSearch(searchParam, cmdLine);
                //        }
                //    }
                //}

            }
        }
        }



        public void GetDefaultArgsAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
        {
            result.SetData("about:home");
        }

        public void GetFeatures([MarshalAs(UnmanagedType.Interface)] nsICommandLine aCmdLine, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
        {
        }

        public void GetStartPageAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
        {
        }

        public void SetDefaultArgsAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase value)
        {
        }

        public void SetStartPageAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase value)
        {
        }











        public void HandleContent([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aContentType, [MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor aWindowContext, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest)
        {
            
        }









        public void Validate([MarshalAs(UnmanagedType.Interface)] nsICommandLine aCommandLine)
        {
            using (var aFlag = new nsAString())
            {
                // Other handlers may use osint so only handle the osint flag if the url
                // flag is also present and the command line is valid.
                aFlag.SetData("osint");
                var osintFlagIdx = aCommandLine.FindFlag(aFlag, false);

                aFlag.SetData("url");
                var urlFlagIdx = aCommandLine.FindFlag(aFlag, false);

                if (urlFlagIdx > -1 && (osintFlagIdx > -1 || aCommandLine.GetStateAttribute() == nsICommandLineConsts.STATE_REMOTE_EXPLICIT))
                {
                    //var urlParam = aCommandLine.getArgument(urlFlagIdx + 1);
                    //if (aCommandLine.length != urlFlagIdx + 2 || / firefoxurl:/.test(urlParam))
                    //        throw NS_ERROR_ABORT;
                    //var isDefault = false;
                    //try
                    //{
                    //    var url = Services.urlFormatter.formatURLPref("app.support.baseURL") +
                    //              "win10-default-browser";
                    //    if (urlParam == url)
                    //    {
                    //        isDefault = ShellService.isDefaultBrowser(false, false);
                    //    }
                    //}
                    //catch (ex) { }
                    //if (isDefault)
                    //{
                    //    // Firefox is already the default HTTP handler.
                    //    // We don't have to show the instruction page.
                    //    throw NS_ERROR_ABORT;
                    //}
                    //cmdLine.handleFlag("osint", false)
                }
            }
        }
    }

    public class NativeView : NativeWindow, IDisposable
    {
        private Control _view;

        public NativeView(Control view, IntPtr hView)
        {
            _view = view;
            this.AssignHandle(hView);
        }

        private void Dispose(bool disposing)
        {
            if (_view != null)
            {
                this.ReleaseHandle();
                _view = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~NativeView()
        {
            Dispose(false);
        }

        protected override void WndProc(ref Message m)
        {
            //if (_view.MozWndProc(ref m))
            //    return;

            base.WndProc(ref m);
        }

    }


    // @mozilla.org/browser/aboutnewtab-service;1 
    public class aboutNewTabService : ComObject<nsIAboutNewTabService>
    {
        public aboutNewTabService() : base(CreateNewTabService())
        {
        }

        private static nsIAboutNewTabService CreateNewTabService()
        {
            return BrowseoFXpcom.CreateInstance<Gecko.Interfaces.nsIAboutNewTabService>("@mozilla.org/browser/aboutnewtab-service;1");
        }

        public void GenerateRemoteURL([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase result)
        {
        }

        public void GetDefaultURLAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase result)
        {
        }

        public void GetNewTabURLAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase result)
        {
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetOverriddenAttribute()
        {
            return false;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetRemoteEnabledAttribute()
        {
            return false;
        }

        public void GetRemoteReleaseNameAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase result)
        {
            //result.SetData("Mozilla");
        }

        public void GetRemoteVersionAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase result)
        {

        }

        public void ReleaseFromUpdateChannel([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase channelName, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase result)
        {

        }

        public void ResetNewTabURL()
        {

        }

        public void SetDefaultURLAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase value)
        {

        }

        public void SetNewTabURLAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] nsACStringBase value)
        {

        }
    }

    //@mozilla.org/browser/sessionstore;1
    public class nsSessionStore : nsISessionStore
    {
        private static nsSessionStore _instancs;
        public static nsSessionStore Instance
        {
            get
            {
                if (_instancs == null)
                {
                    _instancs = new nsSessionStore();
                    //_instancs.Init();
                }
                return _instancs;
            }
        }
        public void DeleteGlobalValue([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey)
        {
            //result.SetData("Mozilla");
        }

        public void DeleteTabValue([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aTab, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey)
        {

        }

        public void DeleteWindowValue([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey)
        {
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDOMNode DuplicateTab([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode aTab, int aDelta)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDOMNode ForgetClosedTab([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, uint aIndex)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDOMNode ForgetClosedWindow(uint aIndex)
        {
            throw new NotImplementedException();
        }

        public void GetBrowserState([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetCanRestoreLastSessionAttribute()
        {
            throw new NotImplementedException();
        }

        public uint GetClosedTabCount([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow)
        {
            throw new NotImplementedException();
        }

        public void GetClosedTabData([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        public uint GetClosedWindowCount()
        {
            throw new NotImplementedException();
        }

        public void GetClosedWindowData([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        public void GetGlobalValue([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        public void GetTabState([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aTab, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        public void GetTabValue([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aTab, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        public void GetWindowState([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        public void GetWindowValue([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        public void PersistTabAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aName)
        {
            throw new NotImplementedException();
        }

        public void RestoreLastSession()
        {
            throw new NotImplementedException();
        }

        public void SetBrowserState([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aState)
        {
            throw new NotImplementedException();
        }

        public void SetCanRestoreLastSessionAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            throw new NotImplementedException();
        }

        public void SetGlobalValue([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey, ref JSVal aStringValue)
        {
            throw new NotImplementedException();
        }

        public void SetTabState([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aTab, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aState)
        {
            throw new NotImplementedException();
        }

        public void SetTabValue([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aTab, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey, ref JSVal aStringValue)
        {
            throw new NotImplementedException();
        }

        public void SetWindowState([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aState, [MarshalAs(UnmanagedType.U1)] bool aOverwrite)
        {
            throw new NotImplementedException();
        }

        public void SetWindowValue([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase aKey, ref JSVal aStringValue)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDOMNode UndoCloseTab([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, uint aIndex)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDOMWindow UndoCloseWindow(uint aIndex)
        {
            throw new NotImplementedException();
        }
    }

    public class nsSessionStartup : nsISessionStartup
    {
        private static nsSessionStartup _instancs;
        public static nsSessionStartup Instance
        {
            get
            {
                if (_instancs == null)
                {
                    _instancs = new nsSessionStartup();
                    _instancs.Init();
                }
                return _instancs;
            }
        }
        public void Init()
        {
            nsObserverService.ObserverService.NotifyObservers(null, "sessionstore-init-started", null);
            nsObserverService.ObserverService.NotifyObservers(null, "sessionstore-state-finalized",null);
        }
        //@mozilla.org/browser/sessionstartup;1
        [return: MarshalAs(UnmanagedType.U1)]
        public bool DoRestore()
        {
            throw new NotImplementedException();
        }

        public JSVal GetOnceInitializedAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetPreviousSessionCrashedAttribute()
        {
            throw new NotImplementedException();
        }

        public uint GetSessionTypeAttribute()
        {
            throw new NotImplementedException();
        }

        public JSVal GetStateAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetWillOverrideHomepageAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool IsAutomaticRestoreEnabled()
        {
            throw new NotImplementedException();
        }
    }

    /**
 * A callback passed to nsISessionStoreUtils.forEachNonDynamicChildFrame().
 */
    [ComImport]
    [Guid("8199ebf7-76c0-43d6-bcbe-913dd3de3ebf")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface nsISessionStoreUtilsFrameCallback 
    {
        /**
         * handleFrame() will be called once for each non-dynamic child frame of the
         * given parent |frame|. The second argument is the |index| of the frame in
         * the list of all child frames.
         */

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void handleFrame([MarshalAs(UnmanagedType.Interface)] mozIDOMWindowProxy frame, uint index);
    };
    [ComImport]
    [Guid("2be448ef-c783-45de-a0df-442bccbb4532")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface nsISessionStoreUtils 
    {
        /**
         * Calls the given |callback| once for each non-dynamic child frame of the
         * given |window|.
         */
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void forEachNonDynamicChildFrame([MarshalAs(UnmanagedType.Interface)] mozIDOMWindowProxy window,
                                         [MarshalAs(UnmanagedType.Interface)] nsISessionStoreUtilsFrameCallback callback);

        /**
         * Creates and returns an event listener that filters events from dynamic
         * docShells. It forwards those from non-dynamic docShells to the given
         * |listener|.
         *
         * This is implemented as a native filter, rather than a JS-based one, for
         * performance reasons.
         */
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        [return: System.Runtime.InteropServices.MarshalAs(UnmanagedType.Interface)]
        nsIDOMEventListener createDynamicFrameEventFilter([MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener listener);
    };

    //"@mozilla.org/browser/sessionstore/utils;1", nsISessionStoreUtils
    public class nsSessionStoreUtils : nsISessionStoreUtils
    {
        private static nsSessionStoreUtils _instancs;
        public static nsSessionStoreUtils Instance
        {
            get
            {
                if (_instancs == null)
                {
                    _instancs = new nsSessionStoreUtils();
                }
                return _instancs;
            }
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDOMEventListener createDynamicFrameEventFilter([MarshalAs(UnmanagedType.Interface)] nsIDOMEventListener listener)
        {
            throw new NotImplementedException();
        }

        public void forEachNonDynamicChildFrame([MarshalAs(UnmanagedType.Interface)] mozIDOMWindowProxy window, [MarshalAs(UnmanagedType.Interface)] nsISessionStoreUtilsFrameCallback callback)
        {
        }
    }

       


        //component {0636a680-45cb-11e4-916c-0800200c9a66}
        //MainProcessSingleton.js process = main
        //contract @mozilla.org/main-process-singleton;1 {0636a680-45cb-11e4-916c-0800200c9a66} process=main
        //category app-startup MainProcessSingleton service,@mozilla.org/main-process-singleton;1 process=main

        class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var nsresult = -1;
            var binDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin";
            var profileDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\bin\Debug\profile";

            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
                throw new InvalidOperationException("The calling thread must be STA");

            #region before Initialize
            Interlocked.Exchange(ref BrowseoFXpcom._XpcomThreadId, Thread.CurrentThread.ManagedThreadId);

            #region initialize nsIServiceManager nsIComponentManager nsIComponentRegistrar
            string oldCurrentDir = Directory.GetCurrentDirectory();
            string libPath = Path.GetFullPath(binDirectory);

            NativeMethods.windows.SetDllDirectory(libPath);
            Environment.SetEnvironmentVariable("path", Environment.GetEnvironmentVariable("path") + ";" + binDirectory);
            //Environment.SetEnvironmentVariable("MOZ_NEW_INSTANCE", "1");

            nsXREDirProvider.Instance.GeckoPath = libPath;
            nsXREDirProvider.Instance.ProfilePath = profileDirectory;


            Gecko.Interfaces.nsIFile mreAppDir = null;
            using (var str = new Gecko.nsAString(libPath))
            {
                nsresult = NativeMethods.xpcom.NS_NewLocalFile(str, true, out mreAppDir);
                if (nsresult != NativeMethods.NS_OK)
                    throw new COMException("Failed on NS_NewLocalFile", nsresult);
            }
            
            Directory.SetCurrentDirectory(libPath);

            Gecko.Interfaces.nsIServiceManager serviceManager;
            nsresult = NativeMethods.xpcom.NS_InitXPCOM2(out serviceManager, mreAppDir, nsXREDirProvider.Instance);
            if (nsresult != NativeMethods.NS_OK)
                throw new COMException("Failed on NS_InitXPCOM2", nsresult);
            BrowseoFXpcom.ServiceManager = new ComObject<Gecko.Interfaces.nsIServiceManager>(serviceManager);
            serviceManager = null;

            // get some global objects we will need later
            Gecko.Interfaces.nsIComponentManager componentManager;
            if (0 == NativeMethods.xpcom.NS_GetComponentManager(out componentManager))
                BrowseoFXpcom.ComponentManager = new ComObject<Gecko.Interfaces.nsIComponentManager>(componentManager);
            componentManager = null;

            Gecko.Interfaces.nsIComponentRegistrar componentRegistrar;
            if (NativeMethods.xpcom.NS_GetComponentRegistrar(out componentRegistrar) == NativeMethods.NS_OK)
                BrowseoFXpcom.ComponentRegistrar = new ComObject<Gecko.Interfaces.nsIComponentRegistrar>(componentRegistrar);
            componentRegistrar = null;

            Directory.SetCurrentDirectory(oldCurrentDir);
            #endregion


            for (int i = 0; i < 100; i++)
            {
                nsObserverService.Instance.addObserver(new nsObserverAny(), "browserClassInitializer_"+i);
                nsObserverService.Instance.addObserver(new nsObserverAny(), "browserClassInitializer_handleResult_" + i);
                nsObserverService.Instance.addObserver(new nsObserverAny(), "browserClassInitializer_handleCompletion_" + i);
            }
            nsObserverService.Instance.addObserver(new nsObserverAny(), "handleError");


            var PreferencesService = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.PreferencesService);
            nsPrefBranch pref = nsPrefBranch.Default;
            //pref["extensions.e10sBlocksEnabling"] = false;
            //pref["extensions.e10sBlockedByAddons"] = false;
            pref["toolkit.telemetry.enabled"] = false;
            pref["app.update.enabled"] = false;
            pref["browser.disableResetPrompt"] = true;



            BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIXULRuntime).GUID, "nsXULAppInfo", Gecko.Contracts.AppInfo, nsXULAppInfo.Instance);
            BrowseoFXpcom.RegisterInstance(new Guid("5d0ce354-df01-421a-83fb-7ead0990c24e"), "nsBrowserHandler", "@mozilla.org/browser/clh;1", nsBrowserHandler.Instance);
            BrowseoFXpcom.RegisterInstance(typeof(nsIBrowserGlue).GUID, "nsBrowserGlue", "@mozilla.org/browser/browserglue;1", nsBrowserGlue.Instance);


            //var nsXULAppInfo = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.AppInfo);
            var TransportService = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.TransportService);
            var DnsService = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.DnsService);
            var NetworkIOService = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.NetworkIOService);
            var ChromeRegistry = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.ChromeRegistry);
            //var AddonsManager = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.AddonsIntegration);


            BrowseoFXpcom.LoadExtension(@"C:\Users\eli\Desktop\temp\omni1\omni\components - Copy", "components.manifest");
            //var browserGlue = BrowseoFXpcom.GetService<Gecko.Interfaces.nsISupports>("@mozilla.org/browser/browserglue;1");
            var browserClassInitializer = BrowseoFXpcom.GetService<Gecko.Interfaces.nsISupports>("@mozilla.browseo/browser/browserClassInitializer;1");
            var BrowserContentHandler = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIBrowserHandler>("@mozilla.org/browser/clh;1");

            //var def = new nsAUTF8String();
            //BrowserContentHandler.GetDefaultArgsAttribute(def);
            //def.Dispose();















            // RegisterProvider is necessary to get link styles etc.
            Gecko.Interfaces.nsIDirectoryService directoryService = null;
            try
            {
                directoryService = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIDirectoryService>(Gecko.Contracts.DirectoryService);
                if (directoryService != null)
                    directoryService.RegisterProvider(nsXREDirProvider.Instance);
            }
            finally
            {
                BrowseoFXpcom.FreeComObject(ref directoryService);
            }

            nsIThreadManager threadMan = BrowseoFXpcom.GetService<nsIThreadManager>(Contracts.ThreadManager);
            using (var mainThread = new ComObject<nsIThread>(threadMan.GetMainThreadAttribute()))
            {
                BrowseoFXpcom.FreeComObject(ref threadMan);

                var _raiseEventOnMainThread = mainThread.GetDelegateForComMethod<nsIThread, nsIThreadDispatchDelegate>(new Action<nsIRunnable, uint>(mainThread.Instance.Dispatch));
                var _mainThreadPtr = Marshal.GetComInterfaceForObject(mainThread.Instance, typeof(nsIEventTarget));
            }




            //var SuppressorService = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.SuppressorService);


            //BrowseoFXpcom.RegisterInstance(typeof(nsISessionStoreUtils).GUID, "nsSessionStoreUtils", "@mozilla.org/browser/sessionstore/utils;1", nsSessionStoreUtils.Instance);
            //BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsISessionStore).GUID, "nsSessionStore", "@mozilla.org/browser/sessionstore;1", nsSessionStore.Instance);
            //var sessionStartup = BrowseoFXpcom.CreateInstance<nsISessionStartup>("@mozilla.org/browser/sessionstartup;1");
            //  BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIBrowserHandler).GUID, "nsBrowserHandler", "@mozilla.org/browser/clh;1", new nsBrowserHandler());
            //   BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIBrowserHandler).GUID, "nsBrowserHandler", "@mozilla.org/browser/final-clh;1", new nsBrowserHandler());


            //BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIAppShell).GUID, "nsIAppShell", Gecko.Contracts.AppStartup, nsAppShell.Instance);
            //BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIAppStartup).GUID, "nsAppStartup", Gecko.Contracts.AppStartup, nsAppStartup.Instance);
            // BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIWindowMediator).GUID, "nsWindowMediator", Gecko.Contracts.WindowMediator, nsWindowMediator.Instance);
            //BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIAboutNewTabService).GUID, "aboutNewTabService", "@mozilla.org/browser/aboutnewtab-service;1", new aboutNewTabService().Instance);
            //BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIBrowserGlue).GUID, "nsBrowserGlue", "@mozilla.org/browser/browserglue;1", new nsBrowserGlue());


            //var mm1 = BrowseoFXpcom.GetService2<nsIMessageBroadcaster>(Contracts.GlobalMessageManager);
            //var mmscriptLoader = mm1.QueryInterface<nsIFrameScriptLoader>();
            //using (var str = new Gecko.nsAString(@"chrome://browser/nsSessionStartup.js"))
            //{
            //    mmscriptLoader.LoadFrameScript(str,/* allowDelayedLoad = */ true,/* aRunInGlobalScope */ true);
            //}

            

            var creator = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsIWindowCreator>(Gecko.Contracts.AppStartup); // BrowseoFXpcom.GetService<Gecko.Interfaces.nsIWindowCreator>(Gecko.Contracts.AppStartup); //creator (do_GetService(NS_APPSTARTUP_CONTRACTID));
            var wwatch = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsIWindowWatcher>(Gecko.Contracts.WindowWatcher);
            wwatch.Instance.SetWindowCreator(creator.Instance);

            var ioParamBlock = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIDialogParamBlock>(Gecko.Contracts.DialogParam);
            var dlgArray = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIMutableArray>(Gecko.Contracts.Array);
            ioParamBlock.SetObjectsAttribute(dlgArray);

            //AppStartupNotifier
            var startupNotifier = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIObserver>(Gecko.Contracts.AppStartupNotifier);
            startupNotifier.Observe(null, "app-startup", null);


            //var sessionStartup = BrowseoFXpcom.GetService<Gecko.Interfaces.nsISessionStartup>(Gecko.Contracts.SessionStartup);
            // sessionStartup.Observe(null, "app-startup", null);

            //nsINativeAppSupport
            //var mNativeApp = BrowseoFXpcom.GetService<Gecko.Interfaces.nsINativeAppSupport>(Gecko.Contracts.NS_NATIVEAPPSUPPORT_CONTRACTID);
            //var canRun = mNativeApp.Start();

            //var xpifilePath = Path.Combine(@"C:\Users\eli\Desktop\temp\omni");
            //BrowseoFXpcom.LoadExtension(xpifilePath, "chrome.manifest");

            //var xpibrowserfilePath = Path.Combine(@"C:\Users\eli\Desktop\temp\omni1\omni");
            //BrowseoFXpcom.LoadExtension(xpibrowserfilePath, "chrome.manifest");

            //var browsercomponents = Path.Combine(@"C:\mozilla-source\mozilla-central\browser\components");
            //BrowseoFXpcom.LoadExtension(browsercomponents, "BrowserComponents.manifest");

            //var browsercomponents_newtab = Path.Combine(@"C:\mozilla-source\mozilla-central\browser\components\newtab");
            //BrowseoFXpcom.LoadExtension(browsercomponents_newtab, "NewTabComponents.manifest");

            //var browsercomponents_selfsupport = Path.Combine(@"C:\mozilla-source\mozilla-central\browser\components\selfsupport");
            //BrowseoFXpcom.LoadExtension(browsercomponents_selfsupport, "SelfSupportService.manifest");

            //var browsercomponents_sessionstore = Path.Combine(@"C:\mozilla-source\mozilla-central\browser\components\sessionstore");
            //BrowseoFXpcom.LoadExtension(browsercomponents_sessionstore, "nsSessionStore.manifest");

            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\components\processsingleton", "ProcessSingleton.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\pluginproblem", "pluginGlue.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\components\crashmonitor", "crashmonitor.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\components\timermanager", "nsUpdateTimerManager.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\mozapps\update", "nsUpdateService.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\components", "nsDefaultCLH.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\components\extensions", "extensions-toolkit.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\components\crashes", "CrashService.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\mozapps\extensions", "extensions.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\toolkit\components\places", "toolkitplaces.manifest");

            //BrowseoFXpcom.LoadExtension(@"", "");

            //BrowseoFXpcomBareWindow.xulWindowUrl = @"chrome://gBrowser/content/browser.xul";

            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\browser\base", "jar.manifest");
            //BrowseoFXpcom.LoadExtension(@"C:\mozilla-source\mozilla-central\browser\components", "BrowserComponents.manifest");



            var obsSvc = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService);
            obsSvc.NotifyObservers(null, "app-startup", null);

            
            //var prefsService = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIPrefService>(Gecko.Contracts.PreferencesService);
            ////prefsService.ResetPrefs();
            ////prefsService.ResetUserPrefs();

            //var prefsFile = Path.Combine(nsXREDirProvider.Instance.ProfilePath, "prefs.js");
            //if (File.Exists(prefsFile))
            //{
            //    var userprefsFile = nsXREDirProvider.Instance.NewLocalFile(prefsFile);
            //    prefsService.ReadUserPrefs(userprefsFile);
            //}
            //obsSvc.NotifyObservers(prefsService as nsISupports, "before-read-userprefs", null);
            //BrowseoFXpcom.FreeComObject(ref prefsService);

            obsSvc.NotifyObservers(null, "profile-do-change", "startup");

            nsXREDirProvider.Instance.ChangeToBrowserDir = true;

            const string xpifile = "XulFx.xpi";
            var xulfxPath = Path.Combine(@"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\bin\Debug", xpifile);
            BrowseoFXpcom.LoadExtension(xulfxPath,"");


            var em = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIObserver>(Gecko.Contracts.AddonsIntegration);
            if (em != null) em.Observe(null, "addons-startup", null);

            obsSvc.NotifyObservers(null, "load-extension-defaults", null);
            obsSvc.NotifyObservers(null, "profile-after-change", "startup");
            BrowseoFXpcom.NS_CreateServicesFromCategory("profile-after-change", null, "profile-after-change");
            obsSvc.NotifyObservers(null, "profile-initial-state", null);


            var cmdLine = BrowseoFXpcom.GetService<Gecko.Interfaces.nsICommandLineRunner>(Gecko.Contracts.CommandLine);
            cmdLine.Init(0, IntPtr.Zero, nsXREDirProvider.Instance.NewLocalFile(binDirectory), Gecko.Interfaces.nsICommandLineConsts.STATE_INITIAL_LAUNCH);
            obsSvc.NotifyObservers(cmdLine as Gecko.Interfaces.nsISupports, "command-line-startup", null);

            var appStartupSrv = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIAppStartup>(Gecko.Contracts.AppStartup);
            appStartupSrv.CreateHiddenWindow();
            obsSvc.NotifyObservers(null, "final-ui-startup", null);
            appStartupSrv.DoneStartingUp();
            
            Gecko.Interfaces.nsIAppShellService appShell = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIAppShellService>(Gecko.Contracts.AppShellService);
            //appShell.CreateHiddenWindow();
            BrowseoFXpcom.FreeComObject(ref appShell);

            //obsSvc.NotifyObservers(null, "command-line-handler", "x-default");

            cmdLine.Run();

            #endregion

            // var win = wwatch.Instance.OpenWindow(null, "chrome://webide/content/", "webide", "chrome,centerscreen,resizable,dialog=no", null);
         //   var browserService = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIDirectoryServiceProvider2>(Gecko.Contracts.NS_BROWSERDIRECTORYPROVIDER_CONTRACTID);

            nsConsoleListener.Init();


           // obsSvc.NotifyObservers(null, "browser-ui-startup-complete",null);


            //XPCOMUtils.defineLazyGetter(Services, "mm", () => {
            //    return Cc["@mozilla.org/globalmessagemanager;1"]
            //             .getService(Ci.nsIMessageBroadcaster)
            //             .QueryInterface(Ci.nsIFrameScriptLoader);
            //});

            //XPCOMUtils.defineLazyGetter(Services, "ppmm", () => {
            //    return Cc["@mozilla.org/parentprocessmessagemanager;1"]
            //             .getService(Ci.nsIMessageBroadcaster)
            //             .QueryInterface(Ci.nsIProcessScriptLoader);
            //});

            

            
            ////Services.mm.loadFrameScript("chrome://global/content/browser-content.js", true);
            //var mm = BrowseoFXpcom.GetService2<nsIMessageBroadcaster>(Contracts.GlobalMessageManager);
            //using (var str = new Gecko.nsAString(@"chrome://global/content/browser-content.js"))
            //{
            //    mm.QueryInterface<nsIFrameScriptLoader>().LoadFrameScript(str,/* allowDelayedLoad = */ true,/* aRunInGlobalScope */ true);
            //}
            //var mmlm = mm.QueryInterface<nsIMessageListenerManager>();
            //using (var str = new Gecko.nsAString(@"AboutHome:Downloads"))
            //{
            //   if(mmlm!=null) mmlm.AddMessageListener(str, new AboutHomeDownloads(), true);
            //}

            ////Services.ppmm.loadProcessScript("chrome://global/content/process-content.js", true);
            //var ppmm = BrowseoFXpcom.GetService2<nsIMessageBroadcaster>("@mozilla.org/parentprocessmessagemanager;1").QueryInterface<nsIProcessScriptLoader>();
            //using (var str = new Gecko.nsAString(@"chrome://global/content/process-content.js"))
            //{
            //    ppmm.LoadProcessScript(str,/* allowDelayedLoad = */ true);
            //}

            //
            //var messageBroadcaster = BrowseoFXpcom.GetService2<nsIMessageBroadcaster>(Contracts.GlobalMessageManager);
            //works - BrowseoFXpcom.RegisterInstance(typeof(Gecko.Interfaces.nsIMessageBroadcaster).GUID, "nsMessageSender", Gecko.Contracts.GlobalMessageManager, nsMessageSender.Instance);



            //const ss = Cc["@mozilla.org/browser/sessionstore;1"].getService(Ci.nsISessionStore);
            //nsISessionStore ss = BrowseoFXpcom.GetService<nsISessionStore>("@mozilla.org/browser/sessionstore;1");
            //nsIObserver ssObs = BrowseoFXpcom.QueryInterface<nsIObserver>(ss);
            //obsSvc.AddObserver(ssObs, "browser-window-before-show", false);

            //





            //var basicWindow = new BrowseoFXpcomBareWindow();
            //basicWindow.WaitUntilChromeLoad();

            ////nsIXULBrowserWindow
            //nsIWindowMediator mediator = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIWindowMediator>(Contracts.WindowMediator);
            //nsISyncMessageSender cpmm = BrowseoFXpcom.GetService<Gecko.Interfaces.nsISyncMessageSender>("@mozilla.org/childprocessmessagemanager;1");

            ////nsIDOMChromeWindow
            //mozIDOMWindowProxy thisWindow = BrowseoFXpcom.QueryInterface<mozIDOMWindowProxy>(basicWindow.Instance);
            //nsIDOMChromeWindow domchromeWindow = BrowseoFXpcom.QueryInterface<nsIDOMChromeWindow>(thisWindow);
            //var messageManager = domchromeWindow.GetMessageManagerAttribute();

            ////nsIXULBrowserWindow
            //nsIXULBrowserWindow browserWindow = basicWindow.Instance.GetXULBrowserWindowAttribute();
            //var tabCount = browserWindow.GetTabCount();
            //nsIObserver browserWindowObs = BrowseoFXpcom.QueryInterface<nsIObserver>(browserWindow);
            //obsSvc.AddObserver(browserWindowObs, "AboutHome:Downloads", false);

            //obsSvc.NotifyObservers(browserWindow as nsISupports, "browser-window-before-show", null);
            //basicWindow.ShowDialog();

          



            nsGlobalChromeWindow.Instance.Init();
            //   
            //var chrome = creator.Instance.CreateChromeWindow(null, nsIWebBrowserChromeConsts.CHROME_ALL);
            //if (chrome != null)
            //{
            //    var window = BrowseoFXpcom.QueryInterface<nsIDOMWindow>(nsGlobalChromeWindow.Instance.Widget.Instance).Wrap(GeckoWindow.Create);
            //    var webnav = BrowseoFXpcom.QueryInterface<nsIWebNavigation>(window.Instance).Wrap(GeckoWebNavigation.Create);

            //    mozIDOMWindowProxy thisWindow = BrowseoFXpcom.QueryInterface<mozIDOMWindowProxy>(nsGlobalChromeWindow.Instance.Widget.Instance);
            //    var helper = BrowseoFXpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
            //    helper.Init(thisWindow);

            //    nsISupports comObj = helper.GetNavigatorAttribute();
            //    var navigator = Xpcom.QueryInterface<nsIDOMNavigator>(comObj);
            //    var browser = BrowseoFXpcom.QueryInterface<nsIWebBrowser>(navigator);

            //    chrome.SetWebBrowserAttribute(new nsWebBrowser());

            //    chrome.ShowAsModal();
            //}
            nsGlobalChromeWindow.Instance.Widget.ShowDialog();


















            //var xulWindow = basicWindow.Instance.GetXULBrowserWindowAttribute();
            //var parent = basicWindow.Instance.GetPrimaryTabParentAttribute();

            //nsISyncMessageSender msgs = BrowseoFXpcom.QueryInterface<nsISyncMessageSender>(Gecko.Contracts.MessageSender);

            ////do_QueryInterface(OwnerDoc()->GetWindow());
            ////var outer = basicWindow._docshell.Instance.GetWindow();

            //nsIWindowMediator mediator = BrowseoFXpcom.CreateInstance<Gecko.Interfaces.nsIWindowMediator>(Contracts.WindowMediator);
            //mediator.RegisterWindow(basicWindow.Instance);


            //nsIWebBrowserChrome chromeWindow = BrowseoFXpcom.QueryInterface<nsIWebBrowserChrome>(basicWindow.Instance);

            //    var window = BrowseoFXpcom.QueryInterface<nsIDOMWindow>(basicWindow.Instance);
            //var gWindow = GeckoWindow.Create(window);
            //var outer = BrowseoFXpcom.QueryInterface<nsPIDOMWindowOuter>(gWindow.Instance);

            //var shell = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsIDocShell>(basicWindow.Instance.GetDocShellAttribute());

            ////var root = shell.GetRootTreeItemAttribute();
            ////var doc1 = root.GetDocument();



            //nsIDOMChromeWindow domchromeWindow = BrowseoFXpcom.QueryInterface<nsIDOMChromeWindow>(thisWindow);
            //var initialOpener = domchromeWindow.TakeOpenerForInitialContentBrowser();

            //IntPtr frameLoader = BrowseoFXpcom.GetService(typeof(nsIFrameLoader).GUID);
            //var pwwatch = BrowseoFXpcom.GetService<Gecko.Interfaces.nsPIWindowWatcher>(Gecko.Contracts.WindowWatcher);
            //pwwatch.AddWindow(thisWindow, chromeWindow);

            //obsSvc.NotifyObservers(basicWindow.Instance as nsISupports, "xul-window-registered", null);


        }
    }
    //AboutHome:Downloads
    public class AboutHomeDownloads : nsIMessageListener
    {
        public void ReceiveMessage()
        {

        }
    }

    public class nsObserverAny :  nsIObserver
    {
        public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
        {
            Console.WriteLine(aTopic);
        }
    }

    public class nsWebBrowser : nsIWebBrowser, nsIInterfaceRequestor
    {
        public void AddWebBrowserListener([MarshalAs(UnmanagedType.Interface)] nsIWeakReference aListener, ref Guid aIID)
        {
            throw new NotImplementedException();
        }

        public void BinarySetOriginAttributes(IntPtr aOriginAttrs)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIWebBrowserChrome GetContainerWindowAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public mozIDOMWindowProxy GetContentDOMWindowAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetIsActiveAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIURIContentListener GetParentURIContentListenerAttribute()
        {
            throw new NotImplementedException();
        }

        public void RemoveWebBrowserListener([MarshalAs(UnmanagedType.Interface)] nsIWeakReference aListener, ref Guid aIID)
        {
            throw new NotImplementedException();
        }

        public void SetContainerWindowAttribute([MarshalAs(UnmanagedType.Interface)] nsIWebBrowserChrome value)
        {
            throw new NotImplementedException();
        }

        public void SetIsActiveAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            throw new NotImplementedException();
        }

        public void SetParentURIContentListenerAttribute([MarshalAs(UnmanagedType.Interface)] nsIURIContentListener value)
        {
            throw new NotImplementedException();
        }


        public IntPtr GetInterface(ref Guid uuid)
        {
            throw new NotImplementedException();
        }
    }


    public class nsXREDirProvider : Gecko.Interfaces.nsIDirectoryServiceProvider,
        nsIObserver
    {
        public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
        {

        }
        /**
        * A directory service key which specifies the profile
        * directory. Unlike the NS_APP_USER_PROFILE_LOCAL_50_DIR key, this key may
        * be available when the profile hasn't been "started", or after is
        * has been shut down. If the application is running without a
        * profile, such as when showing the profile manager UI, this key will
        * not be available. This key is provided by the XUL apprunner or by
        * the aAppDirProvider object passed to XRE_InitEmbedding.
        */
        public const string NS_APP_PROFILE_LOCAL_DIR_STARTUP = "ProfLDS";
                /**
         * A directory service key which specifies the profile
         * directory. Unlike the NS_APP_USER_PROFILE_50_DIR key, this key may
         * be available when the profile hasn't been "started", or after is
         * has been shut down. If the application is running without a
         * profile, such as when showing the profile manager UI, this key will
         * not be available. This key is provided by the XUL apprunner or by
         * the aAppDirProvider object passed to XRE_InitEmbedding.
         */
        public const string NS_APP_PROFILE_DIR_STARTUP = "ProfDS";

        public const string NS_XPCOM_INIT_CURRENT_PROCESS_DIR = "MozBinD";

        public string GeckoPath { get; set; }
        public string ProfilePath { get; set; }
        public static string RuntimePath
        {
            get
            {
                return typeof(BrowseoFXpcom).Assembly.Location;
            }
        }

        private static nsXREDirProvider instance;
        public static nsXREDirProvider Instance
        {
            get
            {
                if (instance == null) instance = new nsXREDirProvider();
                return instance;
            }
        }

        public bool ChangeToBrowserDir { get; internal set; }

        private nsXREDirProvider() { }

        //XRE_APP_FEATURES_DIR
        //XRE_ADDON_APP_DIR
        public IntPtr GetFile(string prop, out bool persistent)
        {
            persistent = false;
            switch (prop)
            {
                //case "PrfDef":
                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath));

                //case "AChrom":
                //case "ProfD":
                //case "permissionDBPDir":
                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Roaming"));
                //case "UMimTyp": // required to handle mailto protocol, etc.
                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Roaming", "mimeTypes.rdf"));

                //case "PrefD":
                //case "ProfLDS":
                //case "PrefDL":
                //case "ExtPrefDL":
                //case "PrefDOverride":
                //case "UsrSrchPlugns":
                //case "indexedDBPDir":
                //case "ProfLD":
                //case "cachePDir":
                //case "ProfDS":
                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Local"));

                case "AChrom":
                case "ProfD":
                case "permissionDBPDir":
                case "PrefD":
                case "ProfLDS":
                case "PrefDL":
                case "ExtPrefDL":
                case "PrefDOverride":
                case "UsrSrchPlugns":
                case "indexedDBPDir":
                case "ProfLD":
                case "cachePDir":
                case "ProfDS":
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "AppData", "Mozilla", "Cache"));

                case "UMimTyp": // required to handle mailto protocol, etc.
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "AppData", "Mozilla", "Cache", "mimeTypes.rdf"));



                case "UAppData":
                case "AppData":
                case "XRESysSExtPD":
                case "XREUSysExt":
                case "LocalAppData":
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "AppData"));

                case "Home":
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Home"));

                case "Desk":
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Desk"));

                case "Progs":
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Progs"));

                case "WinD":
                    return NewLocalFileAsNsPtr(Path.GetDirectoryName(Environment.SystemDirectory));

                case "XREExtDL":
                case "XRESysLExtPD":
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "AppData", "xre"));

                case "XREAddonAppDir":
                case "XREAppFeat":
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "AppData", "Addons"));

                //case nsAppDirectoryServiceDefs.NS_APP_PREFS_50_FILE:
                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "prefs.js"));

                case nsAppDirectoryServiceDefs.NS_APP_USER_CHROME_DIR:
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "chrome"));

                //case nsAppDirectoryServiceDefs.NS_APP_USER_SEARCH_DIR:
                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "plugins"));

                case nsAppDirectoryServiceDefs.NS_LOCALSTORE_UNSAFE_FILE:
                case nsAppDirectoryServiceDefs.NS_APP_LOCALSTORE_50_FILE:
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "localstore.rdf"));

                case nsAppDirectoryServiceDefs.NS_APP_CONTENT_PROCESS_TEMP_DIR:
                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "tmp"));


                //case nsDirectoryServiceDefs.NS_WIN_WINDOWS_DIR:
                //    return NewLocalFileAsNsPtr(Path.GetDirectoryName(Environment.SystemDirectory));

                //case nsDirectoryServiceDefs.NS_XPCOM_CURRENT_PROCESS_DIR:
                ////    //return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser", "content", "preferences"));//content/preferences/
                //    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser"));//content/preferences/
                //   return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));//content/preferences/

                //case NS_XPCOM_INIT_CURRENT_PROCESS_DIR:
                //    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));

                //case nsDirectoryServiceDefs.NS_OS_CURRENT_PROCESS_DIR:

                case "XCurProcD":
                  return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath,"browser"));
                // else return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));

                case nsDirectoryServiceDefs.NS_GRE_DIR:
                case nsDirectoryServiceDefs.NS_GRE_BIN_DIR:
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));


                case "XREExeF":
                case "XREAppDist":
                case "UpdRootD":
                //case "DictD":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));

                case "UpdArchD":
                case "OSUpdApplyToD":
                    return IntPtr.Zero;



                default:
                    //if (BrowseoFXpcom.ServiceManager == null) break;
                    //try
                    //{
                    //    nsIDirectoryServiceProvider dirsvc = BrowseoFXpcom.GetService<nsIDirectoryServiceProvider>("@mozilla.org/file/directory_service;1");
                    //    return dirsvc.GetFile(prop, out persistent);
                    //}
                    //catch (System.Runtime.InteropServices.COMException e)
                    //{
                    //    Debug.WriteLine("Gecko.Xpcom.DirectoryServiceProvider.GetFile: not implemented: {0}", prop);
                    //    return IntPtr.Zero;
                    //}
                    break;
            }


            Debug.WriteLine("Gecko.Xpcom.DirectoryServiceProvider.GetFile: not implemented: {0}", prop);
            return IntPtr.Zero;
        }



        public IntPtr NewLocalFileAsNsPtr(string filename)
        {
            CreateDirectory(filename);
            IntPtr result;
            using (var fileName = new Gecko.nsAString(filename))
            {
                int error = NativeMethods.xpcom.NS_NewLocalFile(fileName, true, out result);
                Marshal.ThrowExceptionForHR(error);
            }
            return result;
        }

        public Gecko.Interfaces.nsIFile NewLocalFile(string filename)
        {
            Gecko.Interfaces.nsIFile result;
            using (var fileName = new Gecko.nsAString(filename))
            {
                int error = NativeMethods.xpcom.NS_NewLocalFile(fileName, true, out result);
                Marshal.ThrowExceptionForHR(error);
            }
            return result;
        }

        public Gecko.Interfaces.nsIFile GetFileInterface(string prop, out bool persistent)
        {
            IntPtr nsFilePtr = GetFile(prop, out persistent);
            try
            {
                return (Gecko.Interfaces.nsIFile)Marshal.GetTypedObjectForIUnknown(nsFilePtr, typeof(Gecko.Interfaces.nsIFile));
            }
            finally
            {
                Marshal.Release(nsFilePtr);
            }
        }

        public ComObject<Gecko.Interfaces.nsIFile> OpenFile(string filename)
        {
            IntPtr nsFilePtr = NewLocalFileAsNsPtr(filename);
            try
            {
                return new ComObject<Gecko.Interfaces.nsIFile>((Gecko.Interfaces.nsIFile)Marshal.GetTypedObjectForIUnknown(nsFilePtr, typeof(Gecko.Interfaces.nsIFile)));
            }
            finally
            {
                Marshal.Release(nsFilePtr);
            }
        }

        public void CreateDirectory(string dir)
        {
            var directory = Path.GetDirectoryName(dir);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }

    public class bDirectoryProvider : Gecko.Interfaces.nsIDirectoryServiceProvider
    {
        public IntPtr GetFile(string prop, out bool persistent)
        {
            persistent = false;
            switch (prop)
            {
                case "XCurProcD":
                    return nsXREDirProvider.Instance.NewLocalFileAsNsPtr(Path.Combine(nsXREDirProvider.Instance.GeckoPath, "browser"));

                default:break;
            }

            return nsXREDirProvider.Instance.GetFile(prop,out persistent);
        }
    }

    public abstract class WebBrowserGlueBase :
        nsIXULBrowserWindow,
        nsIWebProgressListener2,
        nsISupportsWeakReference,
        nsIDOMEventListener,
        nsIInterfaceRequestor
    {
        private IntPtr _pUnknown;
        private GeckoWeakReference _weakRef;
        private long _maxProgressValue;
        private bool _inActivate;

        protected WebBrowserGlueBase()
        {
            _pUnknown = Marshal.GetIUnknownForObject(this);
            Marshal.Release(_pUnknown);
           // this.FocusMan = Gecko.Services.FocusService.GetService();
        }

        //protected abstract GeckoWindow GlobalWindow { get; }
        //protected abstract IWebView WebViewControl { get; }
        protected abstract void SetStatusText(string statusText);
        protected abstract void SetBusy(bool state);
        protected abstract void RaiseDOMEvent(GeckoDOMEventArgs e);
        protected abstract void RaiseNavigating(GeckoNavigatingEventArgs e);
        protected abstract void RaiseFrameNavigating(GeckoNavigatingEventArgs e);
        protected abstract void RaiseFrameNavigated(GeckoNavigatedEventArgs e);
        protected abstract void RaiseRedirecting(GeckoNavigatingEventArgs e);
        protected abstract void RaiseNavigated(GeckoNavigatedEventArgs e);
        protected abstract void RaiseNSSError(GeckoNSSErrorEventArgs e);
        protected abstract void RaiseDocumentCompleted(GeckoDocumentCompletedEventArgs e);
        protected abstract void RaiseProgressChanged(GeckoProgressEventArgs e);
        protected abstract void RaiseCreateWindow(GeckoCreateWindowEventArgs e);

        //public Gecko.Services.FocusService FocusMan { get; private set; }

        public virtual bool IsActive { get; protected set; }

        public virtual void Activate()
        {
            if (_inActivate)
                return;

            //todo GeckoWindow globalView = this.GlobalWindow;
            //if (globalView != null)
            //{
            //    try
            //    {
            //        _inActivate = true;
            //        //todo FocusMan.WindowRaised(globalView);
            //        this.IsActive = true;
            //    }
            //    finally
            //    {
            //        _inActivate = false;
            //    }
            //}
        }

        public virtual void Deactivate()
        {
            //todo GeckoWindow globalView = this.GlobalWindow;
            //if (globalView != null)
            //{
            //    //todo  FocusMan.WindowLowered(globalView);
            //    this.IsActive = false;
            //}
        }

        public virtual nsIWebBrowserChrome CreateWindow(out bool cancel)
        {
            var ea = new GeckoCreateWindowEventArgs();
            RaiseCreateWindow(ea);
            cancel = ea.Cancel;
            if (cancel)
                return null;

            if (ea.Window == null)
                return null;

            return null;

            //todo return new WebBrowserChromeStub(ea.Window);
        }

        #region nsISupportsWeakReference

        public virtual nsIWeakReference GetWeakReference()
        {
            if (_weakRef == null)
                _weakRef = new GeckoWeakReference(this);
            return _weakRef;
        }

        #endregion nsISupportsWeakReference

        #region nsIInterfaceRequestor

        public virtual IntPtr GetInterface(ref Guid uuid)
        {
            Type TWebView = typeof(IWebView);
            IntPtr ppv;
            if (uuid == TWebView.GUID)
            {
                //todo IWebView webView = this.WebViewControl;
                //if (webView == null)
                //    throw new InvalidCastException();

                //return Marshal.GetComInterfaceForObject(webView, TWebView);
            }
            Marshal.ThrowExceptionForHR(Marshal.QueryInterface(_pUnknown, ref uuid, out ppv));
            return ppv;
        }

        #endregion nsIInterfaceRequestor

        #region nsIWebProgressListener2

        public virtual void OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus)
        {
            #region validity checks
            // The request parametere may be null
            if (aRequest == null)
                return;

            // Ignore ViewSource requests, they don't provide the URL
            // see: http://mxr.mozilla.org/mozilla-central/source/netwerk/protocol/viewsource/nsViewSourceChannel.cpp#114
            {
                var viewSource = BrowseoFXpcom.QueryInterface<nsIViewSourceChannel>(aRequest);
                if (viewSource != null)
                {
                    BrowseoFXpcom.ReleaseComObject(viewSource);
                    return;
                }
            }

            #endregion validity checks

            #region request parameters

            GeckoWindow domWindow = null;
            try
            {
                // In some cases a nsIWebProgress instance may not have an associated DOM window, then an exception is thrown.
                //todo domWindow = aWebProgress.GetDOMWindowAttribute().Wrap(GeckoWindow.Create);
            }
            catch (InvalidCastException)
            {
                return;
            }
            catch (COMException e)
            {
                if (e.ErrorCode == GeckoError.NS_ERROR_FAILURE)
                    return;
                throw;
            }


            Uri destUri = null;
            Uri.TryCreate(nsString.Get(aRequest.GetNameAttribute), UriKind.Absolute, out destUri);

            /* This flag indicates that the state transition is for a request, which includes but is not limited to document requests.
			 * Other types of requests, such as requests for inline content (for example images and stylesheets) are considered normal requests.
			 */
            bool stateIsRequest = ((aStateFlags & nsIWebProgressListenerConsts.STATE_IS_REQUEST) != 0);

            /* This flag indicates that the state transition is for a document request. This flag is set in addition to STATE_IS_REQUEST.
			 * A document request supports the nsIChannel interface and its loadFlags attribute includes the nsIChannel ::LOAD_DOCUMENT_URI flag.
			 * A document request does not complete until all requests associated with the loading of its corresponding document have completed.
			 * This includes other document requests (for example corresponding to HTML <iframe> elements).
			 * The document corresponding to a document request is available via the DOMWindow attribute of onStateChange()'s aWebProgress parameter.
			 */
            bool stateIsDocument = ((aStateFlags & nsIWebProgressListenerConsts.STATE_IS_DOCUMENT) != 0);

            /* This flag indicates that the state transition corresponds to the start or stop of activity in the indicated nsIWebProgress instance.
			 * This flag is accompanied by either STATE_START or STATE_STOP, and it may be combined with other State Type Flags.
			 * 
			 * Unlike STATE_IS_WINDOW, this flag is only set when activity within the nsIWebProgress instance being observed starts or stops.
			 * If activity only occurs in a child nsIWebProgress instance, then this flag will be set to indicate the start and stop of that activity.
			 * For example, in the case of navigation within a single frame of a HTML frameset, a nsIWebProgressListener instance attached to the
			 * nsIWebProgress of the frameset window will receive onStateChange() calls with the STATE_IS_NETWORK flag set to indicate the start and
			 * stop of said navigation. In other words, an observer of an outer window can determine when activity, that may be constrained to a
			 * child window or set of child windows, starts and stops.
			 */
            bool stateIsNetwork = ((aStateFlags & nsIWebProgressListenerConsts.STATE_IS_NETWORK) != 0);

            /* This flag indicates that the state transition corresponds to the start or stop of activity in the indicated nsIWebProgress instance.
			 * This flag is accompanied by either STATE_START or STATE_STOP, and it may be combined with other State Type Flags.
			 * This flag is similar to STATE_IS_DOCUMENT. However, when a document request completes, two onStateChange() calls with STATE_STOP are generated.
			 * The document request is passed as aRequest to both calls. The first has STATE_IS_REQUEST and STATE_IS_DOCUMENT set, and the second has
			 * the STATE_IS_WINDOW flag set (and possibly the STATE_IS_NETWORK flag set as well -- see above for a description of when the STATE_IS_NETWORK
			 * flag may be set). This second STATE_STOP event may be useful as a way to partition the work that occurs when a document request completes.
			 */
            bool stateIsWindow = ((aStateFlags & nsIWebProgressListenerConsts.STATE_IS_WINDOW) != 0);
            #endregion request parameters

            #region STATE_START
            /* This flag indicates the start of a request.
			 * This flag is set when a request is initiated.
			 * The request is complete when onStateChange() is called for the same request with the STATE_STOP flag set.
			 */
             if ((aStateFlags & nsIWebProgressListenerConsts.STATE_START) != 0)
            {
                //todo bool isTopLevelWindow = aWebProgress.GetIsTopLevelAttribute();
                //if (stateIsNetwork && isTopLevelWindow)
                //{
                //    _maxProgressValue = 100;
                //    SetBusy(true);

                //    GeckoRequest request = Xpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create);
                //    GeckoNavigatingEventArgs ea = new GeckoNavigatingEventArgs(destUri, request, domWindow);
                //    RaiseNavigating(ea);

                //    if (ea.Cancel)
                //    {
                //        aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
                //        //TODO: change the following handling of cancelled request

                //        // clear busy state
                //        SetBusy(false);

                //        // kill any cached document and raise DocumentCompleted event

                //        RaiseDocumentCompleted(new GeckoDocumentCompletedEventArgs(destUri, request, domWindow));

                //        // clear progress bar
                //        RaiseProgressChanged(new GeckoProgressEventArgs(_maxProgressValue, _maxProgressValue));

                //        // clear status bar
                //        SetStatusText(string.Empty);
                //    }
                //}
                //else if (stateIsDocument && !isTopLevelWindow)
                //{
                //    GeckoNavigatingEventArgs ea = new GeckoNavigatingEventArgs(destUri, Xpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create), domWindow);
                //    RaiseFrameNavigating(ea);

                //    if (ea.Cancel)
                //    {
                //        // TODO: test it on Linux
                //        if (!Xpcom.IsLinux)
                //            aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
                //    }
                //}
            }
            #endregion STATE_START

            #region STATE_REDIRECTING
            /* This flag indicates that a request is being redirected.
			 * The request passed to onStateChange() is the request that is being redirected.
			 * When a redirect occurs, a new request is generated automatically to process the new request.
			 * Expect a corresponding STATE_START event for the new request, and a STATE_STOP for the redirected request.
			 */
            else if ((aStateFlags & nsIWebProgressListenerConsts.STATE_REDIRECTING) != 0)
            {

                //// make sure we're loading the top-level window
                //todo var ea = new GeckoNavigatingEventArgs(destUri, BrowseoFXpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create), domWindow);
                //RaiseRedirecting(ea);

                //if (ea.Cancel)
                //{
                //    aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
                //}
            }
            #endregion STATE_REDIRECTING

            #region STATE_TRANSFERRING
            /* This flag indicates that data for a request is being transferred to an end consumer.
			 * This flag indicates that the request has been targeted, and that the user may start seeing content corresponding to the request.
			 */
            //else if ((aStateFlags & nsIWebProgressListenerConstants.STATE_TRANSFERRING) != 0)
            //{
            //}
            #endregion STATE_TRANSFERRING

            #region STATE_STOP
            /* This flag indicates the completion of a request.
			 * The aStatus parameter to onStateChange() indicates the final status of the request.
			 */
            else if ((aStateFlags & nsIWebProgressListenerConsts.STATE_STOP) != 0)
            {
                /* aStatus
				 * Error status code associated with the state change.
				 * This parameter should be ignored unless aStateFlags includes the STATE_STOP bit.
				 * The status code indicates success or failure of the request associated with the state change.
				 * 
				 * Note: aStatus may be a success code even for server generated errors, such as the HTTP 404 File Not Found error.
				 * In such cases, the request itself should be queried for extended error information (for example for HTTP requests see nsIHttpChannel).
				 */

                if (stateIsNetwork)
                {
                    // clear busy state
                    SetBusy(false);

                    // kill any cached document and raise DocumentCompleted event
                   //todo RaiseDocumentCompleted(new GeckoDocumentCompletedEventArgs(destUri, Xpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create), domWindow));

                    // clear progress bar
                    RaiseProgressChanged(new GeckoProgressEventArgs(_maxProgressValue, _maxProgressValue));

                    // clear status bar
                    SetStatusText(string.Empty);
                }

                if (stateIsRequest)
                {
                    if ((aStatus & 0xff0000) == ((GeckoError.NS_ERROR_MODULE_SECURITY + GeckoError.NS_ERROR_MODULE_BASE_OFFSET) << 16))
                    {
                        SSLStatus sslStatus = null;
                        nsIChannel aChannel = null;
                        nsISupports aSecInfo = null;
                        nsISSLStatusProvider aSslStatusProv = null;
                        try
                        {
                            aChannel = BrowseoFXpcom.QueryInterface<nsIChannel>(aRequest);
                            if (aChannel != null)
                            {
                                aSecInfo = aChannel.GetSecurityInfoAttribute();
                                if (aSecInfo != null)
                                {
                                    aSslStatusProv = BrowseoFXpcom.QueryInterface<nsISSLStatusProvider>(aSecInfo);
                                    if (aSslStatusProv != null)
                                    {
                                       //todo sslStatus = aSslStatusProv.GetSSLStatusAttribute().Wrap(SSLStatus.Create);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            BrowseoFXpcom.FreeComObject(ref aChannel);
                            BrowseoFXpcom.FreeComObject(ref aSecInfo);
                            BrowseoFXpcom.FreeComObject(ref aSslStatusProv);
                        }

                        var ea = new GeckoNSSErrorEventArgs(destUri, aStatus, sslStatus);
                        RaiseNSSError(ea);
                        if (ea.Handled)
                        {
                            aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
                        }
                    }
                }
            }
            #endregion STATE_STOP

        }

        public virtual void OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress)
        {
            OnProgressChange64(aWebProgress, aRequest, aCurSelfProgress, aMaxSelfProgress, aCurTotalProgress, aMaxTotalProgress);
        }

        public virtual void OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint flags)
        {
            Uri uri;
            Uri.TryCreate(nsString.Get(aLocation.GetAsciiSpecAttribute), UriKind.Absolute, out uri);
            var domWindow = aWebProgress.GetDOMWindowAttribute();

            bool sameDocument = (flags & nsIWebProgressListenerConsts.LOCATION_CHANGE_SAME_DOCUMENT) == nsIWebProgressListenerConsts.LOCATION_CHANGE_SAME_DOCUMENT;
            bool errorPage = (flags & nsIWebProgressListenerConsts.LOCATION_CHANGE_ERROR_PAGE) == nsIWebProgressListenerConsts.LOCATION_CHANGE_ERROR_PAGE;
            bool isTopLevel = aWebProgress.GetIsTopLevelAttribute();
            //var ea = new GeckoNavigatedEventArgs(uri, BrowseoFXpcom.QueryInterface<nsIRequest>(aRequest).Wrap(GeckoRequest.Create), domWindow, isTopLevel, sameDocument, errorPage);

            //if (isTopLevel)
            //    this.RaiseNavigated(ea);
            //else
            //    this.RaiseFrameNavigated(ea);
        }

        public virtual void OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage)
        {
            SetStatusText(aMessage ?? string.Empty);
            //if (aWebProgress.GetIsLoadingDocumentAttribute())
            //{
            //	SetStatusText(aMessage ?? string.Empty);
            //}
        }

        public virtual void OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState)
        {

        }

        public virtual void OnProgressChange64(nsIWebProgress aWebProgress, nsIRequest aRequest, long aCurSelfProgress, long aMaxSelfProgress, long aCurTotalProgress, long aMaxTotalProgress)
        {
            if (aMaxTotalProgress < 0)
                return;

            while (aMaxTotalProgress > _maxProgressValue)
            {
                if (aMaxTotalProgress < 5000)
                    _maxProgressValue = 10000;
                else if (aMaxTotalProgress < 50000)
                    _maxProgressValue = 100000;
                else if (aMaxTotalProgress < 500000)
                    _maxProgressValue = 1000000;
                else
                    _maxProgressValue = (_maxProgressValue << 1);
            }

            RaiseProgressChanged(new GeckoProgressEventArgs(aCurTotalProgress, _maxProgressValue, aMaxTotalProgress));

        }

        public virtual bool OnRefreshAttempted(nsIWebProgress aWebProgress, nsIURI aRefreshURI, int aMillis, bool aSameURI)
        {
            return true;
        }

        #endregion nsIWebProgressListener2

        #region nsIXULBrowserWindow

        public virtual void SetJSStatus(nsAStringBase status)
        {
            var sstatus = status.ToString();
        }

        public virtual void SetOverLink(nsAStringBase link, nsIDOMElement element)
        {

        }

        public virtual void OnBeforeLinkTraversal(nsAStringBase originalTarget, nsIURI linkURI, nsIDOMNode linkNode, bool isAppTab, nsAStringBase retval)
        {
            retval.SetData(originalTarget.ToString());
        }

        public virtual void ShowTooltip(int x, int y, nsAStringBase tooltip, nsAStringBase direction)
        {

        }

        public virtual void HideTooltip()
        {

        }

        public nsITabParent ForceInitialBrowserRemote()
        {
            throw new NotImplementedException();
        }

        public bool ShouldLoadURI(nsIDocShell aDocShell, nsIURI aURI, nsIURI aReferrer)
        {
            return true;
        }

        public void ForceInitialBrowserNonRemote(mozIDOMWindowProxy openerWindow)
        {

        }

        public uint GetTabCount()
        {
            return 1;
        }

        #endregion nsIXULBrowserWindow

        #region nsIDOMEventListener

        public virtual void HandleEvent(nsIDOMEvent @event)
        {
            //RaiseDOMEvent(BrowseoFXpcom.QueryInterface<nsIDOMEvent>(@event).Wrap(GeckoDOMEventArgs.Create));
        }

        #endregion nsIDOMEventListener

    }

    public class WebBrowserGlue : WebBrowserGlueBase
    {
        //protected override GeckoWindow GlobalWindow
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //protected override IWebView WebViewControl
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        protected override void RaiseCreateWindow(GeckoCreateWindowEventArgs e)
        {
        }

        protected override void RaiseDocumentCompleted(GeckoDocumentCompletedEventArgs e)
        {
        }

        protected override void RaiseDOMEvent(GeckoDOMEventArgs e)
        {
        }

        protected override void RaiseFrameNavigated(GeckoNavigatedEventArgs e)
        {
        }

        protected override void RaiseFrameNavigating(GeckoNavigatingEventArgs e)
        {
        }

        protected override void RaiseNavigated(GeckoNavigatedEventArgs e)
        {
        }

        protected override void RaiseNavigating(GeckoNavigatingEventArgs e)
        {
        }

        protected override void RaiseNSSError(GeckoNSSErrorEventArgs e)
        {
        }

        protected override void RaiseProgressChanged(GeckoProgressEventArgs e)
        {
        }

        protected override void RaiseRedirecting(GeckoNavigatingEventArgs e)
        {
        }

        protected override void SetBusy(bool state)
        {
        }

        protected override void SetStatusText(string statusText)
        {
        }
    }

    public class nsWindowMediator : nsIWindowMediator
    {
        private static nsWindowMediator _instancs;
        public static nsWindowMediator Instance
        {
            get
            {
                if (_instancs == null) _instancs = new nsWindowMediator();
                return _instancs;
            }
        }

        private static ComObject<nsIWindowMediator> windowMediator;
        public static ComObject<nsIWindowMediator> WindowMediator
        {
            get
            {
                if (windowMediator == null) windowMediator = BrowseoFXpcom.CreateInstance2<nsIWindowMediator>(Contracts.WindowMediator);
                return windowMediator;
            }
        }

        public void AddListener([MarshalAs(UnmanagedType.Interface)] nsIWindowMediatorListener aListener)
        {

        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool CalculateZPosition([MarshalAs(UnmanagedType.Interface)] nsIXULWindow inWindow, uint inPosition, IntPtr inBelow, out uint outPosition, out IntPtr outBelow)
        {
            outPosition = 0;
            outBelow = IntPtr.Zero;
            return false;
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public mozIDOMWindow GetCurrentInnerWindowWithId(ulong aInnerWindowID)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsISimpleEnumerator GetEnumerator([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aWindowType)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public mozIDOMWindowProxy GetMostRecentWindow([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aWindowType)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public mozIDOMWindowProxy GetOuterWindowWithId(ulong aOuterWindowID)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsISimpleEnumerator GetXULWindowEnumerator([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aWindowType)
        {
           return WindowMediator.Instance.GetXULWindowEnumerator(aWindowType);
        }

        public uint GetZLevel([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aWindow)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsISimpleEnumerator GetZOrderDOMWindowEnumerator([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aWindowType, [MarshalAs(UnmanagedType.U1)] bool aFrontToBack)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsISimpleEnumerator GetZOrderXULWindowEnumerator([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aWindowType, [MarshalAs(UnmanagedType.U1)] bool aFrontToBack)
        {
            throw new NotImplementedException();
        }

        public void RegisterWindow([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aWindow)
        {
        }

        public void RemoveListener([MarshalAs(UnmanagedType.Interface)] nsIWindowMediatorListener aListener)
        {
        }

        public void SetZLevel([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aWindow, uint aZLevel)
        {
        }

        public void SetZPosition([MarshalAs(UnmanagedType.Interface)] nsIXULWindow inWindow, uint inPosition, [MarshalAs(UnmanagedType.Interface)] nsIXULWindow inBelow)
        {
        }

        public void UnregisterWindow([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aWindow)
        {
        }

        public void UpdateWindowTimeStamp([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aWindow)
        {
        }

        public void UpdateWindowTitle([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aWindow, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string inTitle)
        {
        }
    }

    public class nsXPCComponents_Utils : nsIXPCComponents_Utils, nsIXPCScriptable
    {
        #region nsIXPCComponents_Utils
        void nsIXPCComponents_Utils.AllowCPOWsInAddon(nsACStringBase addonId, bool allow, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.BlockScriptForGlobal(ref JSVal global, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.CallFunctionWithAsyncStack(ref JSVal function, nsIStackFrame stack, nsAStringBase asyncCause, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.CcSlice(long budget)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.ClearMaxCCTime()
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.CloneInto(ref JSVal value, ref JSVal scope, ref JSVal options, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.CreateObjectIn(ref JSVal vobj, ref JSVal voptions, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.Dispatch(ref JSVal runnable, ref JSVal scope, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.EvalInSandbox(nsAStringBase source, ref JSVal sandbox, ref JSVal version, nsAUTF8StringBase filename, int lineNo, IntPtr cx, byte argc)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.ExportFunction(ref JSVal vfunction, ref JSVal vscope, ref JSVal voptions, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.FinishCC()
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.ForceCC(nsICycleCollectorListener aListener)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.ForceGC()
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.ForcePermissiveCOWs(IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.ForcePrivilegedComponentsForScope(ref JSVal vscope, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.ForceShrinkingGC()
        {
            throw new NotImplementedException();
        }

        nsISupports nsIXPCComponents_Utils.GenerateXPCWrappedJS(ref JSVal obj, ref JSVal scope, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        string nsIXPCComponents_Utils.GetClassName(ref JSVal aObj, bool aUnwrap, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.GetCompartmentLocation(ref JSVal obj, IntPtr cx, nsACStringBase result)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.GetComponentsForScope(ref JSVal vscope, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.GetCrossProcessWrapperTag(ref JSVal obj, nsACStringBase result)
        {
            throw new NotImplementedException();
        }

        nsIClassInfo nsIXPCComponents_Utils.GetDOMClassInfo(nsAStringBase aClassName)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.GetGlobalForObject(ref JSVal obj, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.GetIncumbentGlobal(ref JSVal callback, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.GetIonAttribute(IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.GetJSEngineTelemetryValue(IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.GetJSTestingFunctions(IntPtr cx)
        {
            throw new NotImplementedException();
        }

        int nsIXPCComponents_Utils.GetMaxCCSliceTimeSinceClear()
        {
            throw new NotImplementedException();
        }

        nsIPrincipal nsIXPCComponents_Utils.GetObjectPrincipal(ref JSVal obj, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.GetSandboxAddonId(ref JSVal sandbox, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        nsIXPCComponents_utils_Sandbox nsIXPCComponents_Utils.GetSandboxAttribute()
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.GetSandboxMetadata(ref JSVal sandbox, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.GetStrictAttribute(IntPtr cx)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.GetStrict_modeAttribute(IntPtr cx)
        {
            throw new NotImplementedException();
        }

        ulong nsIXPCComponents_Utils.GetWatchdogTimestamp(nsAStringBase aCategory)
        {
            throw new NotImplementedException();
        }

        xpcIJSWeakReference nsIXPCComponents_Utils.GetWeakReference(ref JSVal obj, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        nsIPrincipal nsIXPCComponents_Utils.GetWebIDLCallerPrincipal()
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.GetWerrorAttribute(IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.Import(nsAUTF8StringBase aResourceURI, ref JSVal targetObj, IntPtr cx, byte argc)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.ImportGlobalProperties(ref JSVal aPropertyList, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.IsCrossProcessWrapper(ref JSVal obj)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.IsDeadWrapper(ref JSVal obj)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.IsModuleLoaded(nsAUTF8StringBase aResourceURI)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.IsProxy(ref JSVal vobject, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCComponents_Utils.IsXrayWrapper(ref JSVal obj)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.MakeObjectPropsNormal(ref JSVal vobj, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        double nsIXPCComponents_Utils.Now()
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.NukeSandbox(ref JSVal obj, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.PermitCPOWsInScope(ref JSVal obj)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.RecomputeWrappers(ref JSVal vobj, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.ReportError(ref JSVal error, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SchedulePreciseGC(ScheduledGCCallback callback)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SchedulePreciseShrinkingGC(ScheduledGCCallback callback)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetAddonCallInterposition(ref JSVal target, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetAddonInterposition(nsACStringBase addonId, nsIAddonInterposition interposition, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetGCZeal(int zeal, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetIonAttribute(IntPtr cx, bool value)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetSandboxMetadata(ref JSVal sandbox, ref JSVal metadata, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetStrictAttribute(IntPtr cx, bool value)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetStrict_modeAttribute(IntPtr cx, bool value)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetWantXrays(ref JSVal vscope, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.SetWerrorAttribute(IntPtr cx, bool value)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.UnblockScriptForGlobal(ref JSVal global, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.UnlinkGhostWindows()
        {
            throw new NotImplementedException();
        }

        void nsIXPCComponents_Utils.Unload(nsAUTF8StringBase registryLocation)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.UnwaiveXrays(ref JSVal aVal, IntPtr cx)
        {
            throw new NotImplementedException();
        }

        JSVal nsIXPCComponents_Utils.WaiveXrays(ref JSVal aVal, IntPtr cx)
        {
            throw new NotImplementedException();
        }
        #endregion



        #region nsIXPCScriptable
        bool nsIXPCScriptable.AddProperty(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj, IntPtr id, ref JSVal val)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCScriptable.Call(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj, IntPtr args)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCScriptable.Construct(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj, IntPtr args)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCScriptable.Enumerate(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj)
        {
            throw new NotImplementedException();
        }

        void nsIXPCScriptable.FinalizeNative(nsIXPConnectWrappedNative wrapper, IntPtr fop, IntPtr obj)
        {
            throw new NotImplementedException();
        }

        IntPtr nsIXPCScriptable.GetClass()
        {
            throw new NotImplementedException();
        }

        string nsIXPCScriptable.GetClassNameAttribute()
        {
            throw new NotImplementedException();
        }

        bool nsIXPCScriptable.GetProperty(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj, IntPtr id, IntPtr vp)
        {
            throw new NotImplementedException();
        }

        uint nsIXPCScriptable.GetScriptableFlags()
        {
            throw new NotImplementedException();
        }

        bool nsIXPCScriptable.HasInstance(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj, ref JSVal val, out bool bp)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCScriptable.NewEnumerate(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj, IntPtr properties)
        {
            throw new NotImplementedException();
        }

        void nsIXPCScriptable.PostCreatePrototype(IntPtr cx, IntPtr proto)
        {
            throw new NotImplementedException();
        }

        void nsIXPCScriptable.PreCreate(nsISupports nativeObj, IntPtr cx, IntPtr globalObj, out IntPtr parentObj)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCScriptable.Resolve(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj, IntPtr id, out bool resolvedp)
        {
            throw new NotImplementedException();
        }

        bool nsIXPCScriptable.SetProperty(nsIXPConnectWrappedNative wrapper, IntPtr cx, IntPtr obj, IntPtr id, IntPtr vp)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class nsAppShell : nsIAppShell
    {
        private static nsAppShell _instance;
        public static nsAppShell Instance
        {
            get
            {
                return _instance ?? (_instance = new nsAppShell());
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _instance = value;
            }
        }

        public void Run()
        {
        }

        public void Exit()
        {
        }

        public void FavorPerformanceHint([MarshalAs(UnmanagedType.U1)] bool favorPerfOverStarvation, uint starvationDelay)
        {
        }

        public uint GetEventloopNestingLevelAttribute()
        {
            throw new NotImplementedException();
        }

        public void ResumeNative()
        {
        }

        public void SuspendNative()
        {
        }
    }

    public class nsAppStartup : nsIWindowCreator2, nsIAppStartup
    {
        private static nsAppStartup _instance;
        public static nsAppStartup Instance
        {
            get
            {
                return _instance ?? (_instance = new nsAppStartup());
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _instance = value;
            }
        }

        int mConsiderQuitStopper;

        #region nsIWindowCreator2
        public Gecko.Interfaces.nsIWebBrowserChrome CreateChromeWindow(Gecko.Interfaces.nsIWebBrowserChrome parent, uint chromeFlags)
        {
            return OnCreateChromeWindow(parent, (int)chromeFlags);
        }

        public Gecko.Interfaces.nsIWebBrowserChrome CreateChromeWindow2(Gecko.Interfaces.nsIWebBrowserChrome parent, uint chromeFlags, uint contextFlags, Gecko.Interfaces.nsITabParent openingTab, Gecko.Interfaces.mozIDOMWindowProxy aOpener, out bool cancel)
        {
            return OnCreateChromeWindow2(parent, (int)chromeFlags, (int)contextFlags, openingTab, aOpener, out cancel);
        }

        protected virtual Gecko.Interfaces.nsIWebBrowserChrome OnCreateChromeWindow(Gecko.Interfaces.nsIWebBrowserChrome parent, int chromeFlags)
        {
            bool cancel = false;
            return CreateChromeWindow2Internal(parent, chromeFlags, 0, null, null, out cancel);
        }

        protected virtual Gecko.Interfaces.nsIWebBrowserChrome OnCreateChromeWindow2(Gecko.Interfaces.nsIWebBrowserChrome parent, int chromeFlags, int contextFlags, Gecko.Interfaces.nsITabParent openingTab, Gecko.Interfaces.mozIDOMWindowProxy aOpener, out bool cancel)
        {
            return CreateChromeWindow2Internal(parent, chromeFlags, 0, openingTab, aOpener, out cancel);
        }

        private Gecko.Interfaces.nsIWebBrowserChrome CreateChromeWindow2Internal(Gecko.Interfaces.nsIWebBrowserChrome parent, int chromeFlags, int contextFlags, Gecko.Interfaces.nsITabParent openingTab, Gecko.Interfaces.mozIDOMWindowProxy opener, out bool cancel)
        {
            cancel = false;

            Gecko.Interfaces.nsIXULWindow childXulWindow = null;
            if (parent != null)
            {
                Gecko.Interfaces.nsIXULWindow parentXulWindow = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsIXULWindow>(parent);
                try
                {
                    if (parentXulWindow != null)
                        childXulWindow = parentXulWindow.CreateNewWindow(chromeFlags, openingTab, opener);
                }
                finally
                {
                    BrowseoFXpcom.FreeComObject(ref parentXulWindow);
                }
            }
            else
            {
                using (var appShell = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsIAppShellService>(Gecko.Contracts.AppShellService))
                {
                    if (appShell == null)
                        throw Marshal.GetExceptionForHR(Gecko.GeckoError.NS_ERROR_FAILURE);

                    childXulWindow = appShell.Instance.CreateTopLevelWindow(null, null, (uint)chromeFlags, Gecko.Interfaces.nsIAppShellServiceConsts.SIZE_TO_CONTENT, Gecko.Interfaces.nsIAppShellServiceConsts.SIZE_TO_CONTENT, openingTab, opener);
                }
            }

            if (childXulWindow != null)
            {
                childXulWindow.SetContextFlagsAttribute((uint)contextFlags);
                Gecko.Interfaces.nsIWebBrowserChrome chromeWindow = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsIWebBrowserChrome>(childXulWindow);
                BrowseoFXpcom.FreeComObject(ref childXulWindow);
                if (chromeWindow != null) return chromeWindow;
            }

            throw Marshal.GetExceptionForHR(Gecko.GeckoError.NS_ERROR_FAILURE);
        }

        public void SetScreenId(uint aScreenId)
        {
            System.Diagnostics.Debug.Print("Not implemented: Gecko.WindowCreator.SetScreenId()");
        }
        #endregion

        public void CreateHiddenWindow()
        {
            using (var appShell = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsIAppShellService>(Gecko.Contracts.AppShellService))
            {
                if (appShell == null)
                    throw Marshal.GetExceptionForHR(Gecko.GeckoError.NS_ERROR_FAILURE);

                appShell.Instance.CreateHiddenWindow();
            }
        }

        public void DestroyHiddenWindow()
        {
            using (var appShell = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsIAppShellService>(Gecko.Contracts.AppShellService))
            {
                if (appShell == null)
                    throw Marshal.GetExceptionForHR(Gecko.GeckoError.NS_ERROR_FAILURE);

                appShell.Instance.DestroyHiddenWindow();
            }
        }

        public void Run()
        {
            using (var appShell = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsIAppShell>(Gecko.Contracts.AppShell))
            {
                if (appShell == null)
                    throw Marshal.GetExceptionForHR(Gecko.GeckoError.NS_ERROR_FAILURE);

                appShell.Instance.Run();
            }
        }

        public void Quit(uint aMode)
        {
        }

        public void CreateInstanceWithProfile([MarshalAs(UnmanagedType.Interface)] nsIToolkitProfile aProfile)
        {
        }

        public void DoneStartingUp()
        {
        }

        public void EnterLastWindowClosingSurvivalArea()
        {
            ++mConsiderQuitStopper;
        }

        public void ExitLastWindowClosingSurvivalArea()
        {
            --mConsiderQuitStopper;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetAutomaticSafeModeNecessaryAttribute()
        {
            return false;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetInterruptedAttribute()
        {
            return false;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetRestartingAttribute()
        {
            return false;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetShuttingDownAttribute()
        {
            return false;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetStartingUpAttribute()
        {
            return true;
        }

        public JSVal GetStartupInfo(IntPtr cx)
        {
            var val = JSVal.Create(cx);

            return val;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetWasRestartedAttribute()
        {
            return false;
        }

        public void RestartInSafeMode(uint aQuitMode)
        {

        }

        public void SetInterruptedAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool TrackStartupCrashBegin()
        {
            return false;
        }

        public void TrackStartupCrashEnd()
        {

        }
    }

    public class nsMessageSender : nsIMessageBroadcaster
    {
        private static nsMessageSender _instancs;
        public static nsMessageSender Instance
        {
            get
            {
                if (_instancs == null) _instancs = new nsMessageSender();
                return _instancs;
            }
        }

        private static ComObject<nsIMessageBroadcaster> nsIMessageBroadcaster_instancs;
        public static ComObject<nsIMessageBroadcaster> MessageBroadcaster
        {
            get
            {
                if (nsIMessageBroadcaster_instancs == null) nsIMessageBroadcaster_instancs = BrowseoFXpcom.GetService2<nsIMessageBroadcaster>(Contracts.GlobalMessageManager);
                return nsIMessageBroadcaster_instancs;
            }
        }



        public void AddMessageListener([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase messageName, [MarshalAs(UnmanagedType.Interface)] nsIMessageListener listener, [MarshalAs(UnmanagedType.U1)] bool listenWhenClosed)
        {
        }

        public void AddWeakMessageListener([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase messageName, [MarshalAs(UnmanagedType.Interface)] nsIMessageListener listener)
        {
        }

        public void BroadcastAsyncMessage([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase messageName, ref JSVal obj, ref JSVal objects, IntPtr cx, byte argc)
        {
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIMessageListenerManager GetChildAt(uint aIndex)
        {
            return MessageBroadcaster.Instance.GetChildAt(aIndex);
        }

        public uint GetChildCountAttribute()
        {
            return MessageBroadcaster.Instance.GetChildCountAttribute();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool MarkForCC()
        {
            return MessageBroadcaster.Instance.MarkForCC();
        }

        public void RemoveMessageListener([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase messageName, [MarshalAs(UnmanagedType.Interface)] nsIMessageListener listener)
        {
        }

        public void RemoveWeakMessageListener([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase messageName, [MarshalAs(UnmanagedType.Interface)] nsIMessageListener listener)
        {
        }
    }

   

    public struct nsAppDirectoryServiceDefs
    {
        //========================================================================================
        //
        // Defines property names for directories available from standard nsIDirectoryServiceProviders.
        // These keys are not guaranteed to exist because the nsIDirectoryServiceProviders which
        // provide them are optional.
        //
        // Keys whose definition ends in "DIR" or "FILE" return a single nsIFile (or subclass).
        // Keys whose definition ends in "LIST" return an nsISimpleEnumerator which enumerates a
        // list of file objects.
        //
        // System and XPCOM level properties are defined in nsDirectoryServiceDefs.h.
        //
        //========================================================================================

        // --------------------------------------------------------------------------------------
        // Files and directories which exist on a per-product basis
        // --------------------------------------------------------------------------------------

        public const string NS_APP_APPLICATION_REGISTRY_FILE    = "AppRegF"          ;
        public const string NS_APP_APPLICATION_REGISTRY_DIR     = "AppRegD"          ;
                                                                                     
        public const string NS_APP_DEFAULTS_50_DIR              = "DefRt"            ;   // The root dir of all defaults dirs
        public const string NS_APP_PREF_DEFAULTS_50_DIR         = "PrfDef"           ;
                                                                                     
        public const string NS_APP_USER_PROFILES_ROOT_DIR       = "DefProfRt"        ;   // The dir where user profile dirs live.
        public const string NS_APP_USER_PROFILES_LOCAL_ROOT_DIR = "DefProfLRt"       ;   // The dir where user profile temp dirs live.
                                                                                     
        public const string NS_APP_RES_DIR                      = "ARes"             ;
        public const string NS_APP_CHROME_DIR                   = "AChrom"           ;
        public const string NS_APP_PLUGINS_DIR                  = "APlugns"          ;   // Deprecated - use NS_APP_PLUGINS_DIR_LIST
        public const string NS_APP_SEARCH_DIR                   = "SrchPlugns"       ;
                                                                                     
        public const string NS_APP_CHROME_DIR_LIST              = "AChromDL"         ;
        public const string NS_APP_PLUGINS_DIR_LIST             = "APluginsDL"       ;
        public const string NS_APP_SEARCH_DIR_LIST              = "SrchPluginsDL"    ;
        public const string NS_APP_DISTRIBUTION_SEARCH_DIR_LIST = "SrchPluginsDistDL";


        // --------------------------------------------------------------------------------------
        // Files and directories which exist on a per-profile basis
        // These locations are typically provided by the profile mgr
        // --------------------------------------------------------------------------------------

        // In a shared profile environment, prefixing a profile-relative
        // key with NS_SHARED returns a location that is shared by
        // other users of the profile. Without this prefix, the consumer
        // has exclusive access to this location.

        public const string NS_SHARED                         = "SHARED"          ;

        public const string NS_APP_PREFS_50_DIR               = "PrefD"           ;// Directory which contains user prefs
        public const string NS_APP_PREFS_50_FILE              = "PrefF"           ;
        public const string NS_APP_PREFS_DEFAULTS_DIR_LIST    = "PrefDL"          ;
        public const string NS_EXT_PREFS_DEFAULTS_DIR_LIST    = "ExtPrefDL"       ;
        public const string NS_APP_PREFS_OVERRIDE_DIR         = "PrefDOverride"   ;// Directory for per-profile defaults

        public const string NS_APP_USER_PROFILE_50_DIR        = "ProfD"           ;
        public const string NS_APP_USER_PROFILE_LOCAL_50_DIR  = "ProfLD"          ;

        public const string NS_APP_USER_CHROME_DIR            = "UChrm"           ;
        public const string NS_APP_USER_SEARCH_DIR            = "UsrSrchPlugns"   ;

        public const string NS_APP_LOCALSTORE_50_FILE         = "LclSt"           ;
        public const string NS_APP_USER_PANELS_50_FILE        = "UPnls"           ;
        public const string NS_APP_CACHE_PARENT_DIR           = "cachePDir"       ;
        public const string NS_LOCALSTORE_UNSAFE_FILE         = "LStoreS";

        public const string NS_APP_SEARCH_50_FILE             = "SrchF"           ;

        public const string NS_APP_INSTALL_CLEANUP_DIR        = "XPIClnupD"       ;//location of xpicleanup.dat xpicleanup.exe

        public const string NS_APP_INDEXEDDB_PARENT_DIR       = "indexedDBPDir"   ;

        public const string NS_APP_PERMISSION_PARENT_DIR      = "permissionDBPDir";


        //
        // NS_APP_CONTENT_PROCESS_TEMP_DIR refers to a directory that is read and
        // write accessible from a sandboxed content process. The key may be used in
        // either process, but the directory is intended to be used for short-lived
        // files that need to be saved to the filesystem by the content process and
        // don't need to survive browser restarts. The directory is reset on startup.
        // The key is only valid when MOZ_CONTENT_SANDBOX is defined. When
        // MOZ_CONTENT_SANDBOX is defined, the directory the key refers to differs
        // depending on whether or not content sandboxing is enabled.
        //
        // When MOZ_CONTENT_SANDBOX is defined and sandboxing is enabled (versus
        // manually disabled via prefs), the content process replaces NS_OS_TEMP_DIR
        // with NS_APP_CONTENT_PROCESS_TEMP_DIR so that legacy code in content
        // attempting to write to NS_OS_TEMP_DIR will write to
        // NS_APP_CONTENT_PROCESS_TEMP_DIR instead. When MOZ_CONTENT_SANDBOX is
        // defined but sandboxing is disabled, NS_APP_CONTENT_PROCESS_TEMP_DIR
        // falls back to NS_OS_TEMP_DIR in both content and chrome processes.
        //
        // New code should avoid writing to the filesystem from the content process
        // and should instead proxy through the parent process whenever possible.
        //
        // At present, all sandboxed content processes use the same directory for
        // NS_APP_CONTENT_PROCESS_TEMP_DIR, but that should not be relied upon.
        //
        //public const string NS_APP_CONTENT_PROCESS_TEMP_DIR =  "ContentTmpD";
        // Otherwise NS_APP_CONTENT_PROCESS_TEMP_DIR must match NS_OS_TEMP_DIR.
        public const string NS_APP_CONTENT_PROCESS_TEMP_DIR = "TmpD";
    }

    public struct nsDirectoryServiceDefs
    {
        /**
        * Defines the property names for directories available from
        * nsIDirectoryService. These dirs are always available even if no
        * nsIDirectoryServiceProviders have been registered with the service.
        * Application level keys are defined in nsAppDirectoryServiceDefs.h.
        *
        * Keys whose definition ends in "DIR" or "FILE" return a single nsIFile (or
        * subclass). Keys whose definition ends in "LIST" return an nsISimpleEnumerator
        * which enumerates a list of file objects.
        *
        * Defines listed in this file are FROZEN.  This list may grow.
        */

        public const string NS_OS_HOME_DIR                          = "Home";
        public const string NS_OS_TEMP_DIR                          = "TmpD";
        public const string NS_OS_CURRENT_WORKING_DIR               = "CurWorkD";
        /* Files stored in this directory will appear on the user's desktop,
         * if there is one, otherwise it's just the same as "Home"
         */
        public const string NS_OS_DESKTOP_DIR                       = "Desk";

        /* Property returns the directory in which the procces was started from.
         * On Unix this will be the path in the MOZILLA_FIVE_HOME env var and if
         * unset will be the current working directory.
         */
        public const string NS_OS_CURRENT_PROCESS_DIR               = "CurProcD";

        /* This location is similar to NS_OS_CURRENT_PROCESS_DIR, however,
         * NS_XPCOM_CURRENT_PROCESS_DIR can be overriden by passing a "bin
         * directory" to NS_InitXPCOM2().
         */
        public const string NS_XPCOM_CURRENT_PROCESS_DIR            = "XCurProcD";

        /* Property will return the location of the the XPCOM Shared Library.
         */
        public const string NS_XPCOM_LIBRARY_FILE                   = "XpcomLib";

        /* Property will return the current location of the GRE directory.
         * On OSX, this typically points to Contents/Resources in the app bundle.
         * If no GRE is used, this propery will behave like
         * NS_XPCOM_CURRENT_PROCESS_DIR.
         */
        public const string NS_GRE_DIR                              = "GreD";

        /* Property will return the current location of the GRE-binaries directory.
         * On OSX, this typically points to Contents/MacOS in the app bundle. On
         * all other platforms, this will be identical to NS_GRE_DIR.
         * Since this property is based on the NS_GRE_DIR, if no GRE is used, this
         * propery will behave like NS_XPCOM_CURRENT_PROCESS_DIR.
         */
        public const string NS_GRE_BIN_DIR                          = "GreBinD";

        /* Platform Specific Locations */
        public const string NS_OS_SYSTEM_DIR                        = "SysD";
                                                                    
        public const string NS_WIN_WINDOWS_DIR                      = "WinD"        ; 
        public const string NS_WIN_PROGRAM_FILES_DIR                = "ProgF"       ; 
        public const string NS_WIN_HOME_DIR                         = NS_OS_HOME_DIR; 
        public const string NS_WIN_DESKTOP_DIR                      = "DeskV"       ;    // virtual folder at the root of the namespace
        public const string NS_WIN_PROGRAMS_DIR                     = "Progs"       ;    // User start menu programs directory!
        public const string NS_WIN_CONTROLS_DIR                     = "Cntls"       ;
        public const string NS_WIN_PRINTERS_DIR                     = "Prnts"       ;
        public const string NS_WIN_PERSONAL_DIR                     = "Pers"        ;
        public const string NS_WIN_FAVORITES_DIR                    = "Favs"        ;
        public const string NS_WIN_STARTUP_DIR                      = "Strt"        ;
        public const string NS_WIN_RECENT_DIR                       = "Rcnt"        ;
        public const string NS_WIN_SEND_TO_DIR                      = "SndTo"       ;
        public const string NS_WIN_BITBUCKET_DIR                    = "Buckt"       ;
        public const string NS_WIN_STARTMENU_DIR                    = "Strt"        ;
        // This gives the same thing as NS_OS_DESKTOP_DIR           
        public const string NS_WIN_DESKTOP_DIRECTORY                = "DeskP"        ; // file sys dir which physically stores objects on desktop
        public const string NS_WIN_DRIVES_DIR                       = "Drivs"        ;
        public const string NS_WIN_NETWORK_DIR                      = "NetW"         ;
        public const string NS_WIN_NETHOOD_DIR                      = "netH"         ;
        public const string NS_WIN_FONTS_DIR                        = "Fnts"         ;
        public const string NS_WIN_TEMPLATES_DIR                    = "Tmpls"        ;
        public const string NS_WIN_COMMON_STARTMENU_DIR             = "CmStrt"       ;
        public const string NS_WIN_COMMON_PROGRAMS_DIR              = "CmPrgs"       ;
        public const string NS_WIN_COMMON_STARTUP_DIR               = "CmStrt"       ;
        public const string NS_WIN_COMMON_DESKTOP_DIRECTORY         = "CmDeskP"      ;
        public const string NS_WIN_COMMON_APPDATA_DIR               = "CmAppData"    ;
        public const string NS_WIN_APPDATA_DIR                      = "AppData"      ;
        public const string NS_WIN_LOCAL_APPDATA_DIR                = "LocalAppData" ;
        public const string NS_WIN_PRINTHOOD                        = "PrntHd"       ;
        public const string NS_WIN_COOKIES_DIR                      = "CookD"        ;
        public const string NS_WIN_DEFAULT_DOWNLOAD_DIR             = "DfltDwnld"    ;
        // On Win7 and up these ids will return the default save-to location for
        // Windows Libraries associated with the specific content type. For other
        // os they return the local user folder. Note these can return network file
        // paths which can jank the ui thread so be careful how you access them.
        public const string NS_WIN_DOCUMENTS_DIR                    = "Docs"         ;
        public const string NS_WIN_PICTURES_DIR                     = "Pict"         ;
        public const string NS_WIN_MUSIC_DIR                        = "Music"        ; 
        public const string NS_WIN_VIDEOS_DIR                       = "Vids"         ;


    }

    public class XULAppAPI
    {
        /**
* A directory service key which provides the platform-correct "application
* data" directory as follows, where $name and $vendor are as defined above and
* $vendor is optional:
*
* Windows:
*   HOME = Documents and Settings\$USER\Application Data
*   UAppData = $HOME[\$vendor]\$name
*
* Unix:
*   HOME = ~
*   UAppData = $HOME/.[$vendor/]$name
*
* Mac:
*   HOME = ~
*   UAppData = $HOME/Library/Application Support/$name
*
* Note that the "profile" member above will change the value of UAppData as
* follows:
*
* Windows:
*   UAppData = $HOME\$profile
*
* Unix:
*   UAppData = $HOME/.$profile
*
* Mac:
*   UAppData = $HOME/Library/Application Support/$profile
*/
        public const string XRE_USER_APP_DATA_DIR = "UAppData";
        /**
        * A directory service key which provides the update directory. Callers should
        * fall back to appDir.
        * Windows:    If vendor name exists:
        *             Documents and Settings\<User>\Local Settings\Application Data\
        *             <vendor name>\updates\
        *             <hash of the path to XRE_EXECUTABLE_FILE’s parent directory>
        *
        *             If vendor name doesn't exist, but product name exists:
        *             Documents and Settings\<User>\Local Settings\Application Data\
        *             <product name>\updates\
        *             <hash of the path to XRE_EXECUTABLE_FILE’s parent directory>
        *
        *             If neither vendor nor product name exists:
        *               If app dir is under Program Files:
        *               Documents and Settings\<User>\Local Settings\Application Data\
        *               <relative path to app dir from Program Files>
        *
        *               If app dir isn’t under Program Files:
        *               Documents and Settings\<User>\Local Settings\Application Data\
        *               <MOZ_APP_NAME>
        *
        * Mac:        ~/Library/Caches/Mozilla/updates/<absolute path to app dir>
        *
        * Gonk:       /data/local
        *
        * All others: Parent directory of XRE_EXECUTABLE_FILE.
        */
        public const string XRE_UPDATE_ROOT_DIR = "UpdRootD";
        /**
        * A directory service key which provides the executable file used to
        * launch the current process.  This is the same value returned by the
        * XRE_GetBinaryPath function defined below.
        */
        public const string XRE_EXECUTABLE_FILE = "XREExeF";

        /**
         * A directory service key which specifies the profile
         * directory. Unlike the NS_APP_USER_PROFILE_50_DIR key, this key may
         * be available when the profile hasn't been "started", or after is
         * has been shut down. If the application is running without a
         * profile, such as when showing the profile manager UI, this key will
         * not be available. This key is provided by the XUL apprunner or by
         * the aAppDirProvider object passed to XRE_InitEmbedding.
         */
        public const string NS_APP_PROFILE_DIR_STARTUP = "ProfDS";

        /**
        * A directory service key which specifies the profile
        * directory. Unlike the NS_APP_USER_PROFILE_LOCAL_50_DIR key, this key may
        * be available when the profile hasn't been "started", or after is
        * has been shut down. If the application is running without a
        * profile, such as when showing the profile manager UI, this key will
        * not be available. This key is provided by the XUL apprunner or by
        * the aAppDirProvider object passed to XRE_InitEmbedding.
        */
        public const string NS_APP_PROFILE_LOCAL_DIR_STARTUP = "ProfLDS";

        /**
        * A directory service key which specifies the user system extension
        * parent directory.
        */
        public const string XRE_USER_SYS_EXTENSION_DIR = "XREUSysExt";
        /**
        * A directory service key which specifies the distribution specific files for
        * the application.
        */
        public const string XRE_APP_DISTRIBUTION_DIR = "XREAppDist";

        /**
         * A directory service key which specifies the location for app dir add-ons.
         * Should be a synonym for XCurProcD everywhere except in tests.
         */
        public const string XRE_ADDON_APP_DIR = "XREAddonAppDir";

        public const string PREF_OVERRIDE_DIRNAME = "preferences";
        public const string APP_REGISTRY_NAME = "appreg";
    }

    public class nsSingletonFactory : Gecko.Interfaces.nsIFactory
    {
        private object _instance;

        public nsSingletonFactory(object instance)
        {
            _instance = instance;
        }

        public IntPtr CreateInstance(Gecko.Interfaces.nsISupports aOuter, ref Guid iid)
        {
            if (aOuter != null)
                Marshal.ThrowExceptionForHR(Gecko.GeckoError.NS_ERROR_NO_AGGREGATION);

            IntPtr pvv;
            IntPtr pUnk = Marshal.GetIUnknownForObject(_instance);
            try
            {
                Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pUnk, ref iid, out pvv));
            }
            finally
            {
                Marshal.Release(pUnk);
            }
            return pvv;
        }

        public void LockFactory(bool @lock)
        {
            throw new NotImplementedException();
        }

    }

    public class nsConsoleListener : Gecko.Interfaces.nsIConsoleListener, Gecko.Interfaces.nsIObserver
    {
        public static void Init()
        {

            var cobs = new nsConsoleListener();
            var cc = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIConsoleService>(Gecko.Contracts.ConsoleService);
            cc.RegisterListener(cobs);
            var svc = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService);
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
                //	var js = GeckoJavascriptBridge.GetService();
                //	string s = js.EvaluateToString(aSubject, GeckoPrincipal.SystemPrincipal, "this.wrappedJSObject.arguments + ' [level: ' + this.wrappedJSObject.level + ', file: \"' + this.wrappedJSObject.filename + '\", line: ' + this.wrappedJSObject.lineNumber + ']'");
                Console.WriteLine("[{0}] console ({1}): {2}", DateTime.UtcNow.ToString("HH:mm:ss"), aData, aTopic);
            }
            catch (Gecko.GeckoJavaScriptException e)
            {
                Console.WriteLine("[{0}] {1}", DateTime.UtcNow.ToString("HH:mm:ss"), e.ToString());
            }
        }
    }

    public class nsWeakReference : Gecko.Interfaces.nsIWeakReference
    {
        protected WeakReference _weakReference;

        public nsWeakReference(object obj)
        {
            _weakReference = new WeakReference(obj, false);
        }

        IntPtr Gecko.Interfaces.nsIWeakReference.QueryReferent(ref Guid uuid)
        {
            // If object is alive we take it to QueryReferentImplementation
            // else return IntPtr.Zero
            return _weakReference.IsAlive
                       ? QueryReferentImplementation(_weakReference.Target, ref uuid)
                       : IntPtr.Zero;
        }

        protected virtual IntPtr QueryReferentImplementation(object obj, ref Guid uuid)
        {
            // by default we make QueryReferent
            return BrowseoFXpcom.QueryReferent(obj, ref uuid);
        }
    }

	public class nsPrefBranch : ComObject<Gecko.Interfaces.nsIPrefBranch>
    {
        private static ComObject<Gecko.Interfaces.nsIPrefService> _prefService;
        private static nsPrefBranch _userBranch;
        private static nsPrefBranch _defaultBranch;

        #region static members

        public static ComObject<Gecko.Interfaces.nsIPrefService> PrefService
        {
            get { return _prefService ?? (_prefService = new ComObject<Gecko.Interfaces.nsIPrefService>(BrowseoFXpcom.GetService<Gecko.Interfaces.nsIPrefService>(Gecko.Contracts.PreferencesService))); }
        }

        /// <summary>
        /// Gets the preferences defined for the current user.
        /// </summary>
        public static nsPrefBranch User
        {
            get { return _userBranch ?? (_userBranch = new nsPrefBranch(PrefService.Instance.GetBranch(""))); }
        }

        /// <summary>
        /// Gets the set of preferences used as defaults when no user preference is set.
        /// </summary>
        public static nsPrefBranch Default
        {
            get { return _defaultBranch ?? (_defaultBranch = new nsPrefBranch(PrefService.Instance.GetDefaultBranch(""))); }
        }

        #endregion static members




        private nsPrefBranch(Gecko.Interfaces.nsIPrefBranch prefBranch)
            : base(prefBranch)
        {

        }

        /// <summary>
        /// Reads all User preferences from the specified file.
        /// </summary>
        /// <param name="filename">Required. The name of the file from which preferences are read.  May not be null.</param>
        public static void Load(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("filename");

            else if (!File.Exists(filename))
                throw new FileNotFoundException();
            using (var file = nsXREDirProvider.Instance.OpenFile(filename))
            {
                PrefService.Instance.ReadUserPrefs(file.Instance);
            }
        }

        /// <summary>
        /// Saves all User preferences to the specified file.
        /// </summary>
        /// <param name="filename">Required. The name of the file to which preferences are saved.  May not be null.</param>
        public static void Save(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("filename");
            using (var file = nsXREDirProvider.Instance.OpenFile(filename))
            {
                PrefService.Instance.SavePrefFile(file.Instance);
            }
        }

        /// <summary>
        /// Resets all preferences to their default values.
        /// </summary>
        public void Reset()
        {
            if (this == _defaultBranch)
                PrefService.Instance.ResetPrefs();
            else
                PrefService.Instance.ResetUserPrefs();
        }

        const int PREF_INVALID = 0;
        const int PREF_STRING = 32;
        const int PREF_INT = 64;
        const int PREF_BOOL = 128;

        /// <summary>
        /// Gets or sets the preference with the given name.
        /// </summary>
        /// <param name="prefName">Required. The name of the preference to get or set.</param>
        /// <returns></returns>
        public object this[string prefName]
        {
            get
            {
                int type = Instance.GetPrefType(prefName);
                switch (type)
                {
                    case PREF_INVALID:
                        return null;
                    case PREF_STRING:
                        object comObject = null;
                        Gecko.Interfaces.nsISupportsString aString = null;
                        Guid guid = typeof(Gecko.Interfaces.nsISupportsString).GUID;
                        IntPtr pUnk = Instance.GetComplexValue(prefName, ref guid);
                        try
                        {
                            comObject = Marshal.GetObjectForIUnknown(pUnk);
                            aString = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsISupportsString>(comObject);
                            return Gecko.nsString.Get(aString.GetDataAttribute);
                        }
                        finally
                        {
                            BrowseoFXpcom.FreeComObject(ref comObject);
                            BrowseoFXpcom.FreeComObject(ref aString);
                        }
                    case PREF_INT:
                        return Instance.GetIntPref(prefName);
                    case PREF_BOOL:
                        return Instance.GetBoolPref(prefName);
                }
                throw new ArgumentException("prefName");
            }
            set
            {
                if (string.IsNullOrEmpty(prefName))
                    throw new ArgumentException("prefName");
                else if (value == null)
                    throw new ArgumentNullException("value");

                int existingType = Instance.GetPrefType(prefName);
                int assignedType = GetValueType(value);

                if (existingType != 0 && existingType != assignedType)
                {
                    throw new InvalidCastException(string.Format(
                        "A {0} value may not be assigned to '{1}' because it is already defined as {2}.",
                        value.GetType().Name,
                        prefName,
                        GetPreferenceType(prefName).Name));
                }
                switch (assignedType)
                {
                    case PREF_STRING:
                        Guid guid = typeof(Gecko.Interfaces.nsISupportsString).GUID;
                        Gecko.Interfaces.nsISupports aValueAsIUnknown = null;
                        var aValue = BrowseoFXpcom.CreateInstance<Gecko.Interfaces.nsISupportsString>(Gecko.Contracts.SupportsString);
                        try
                        {
                            Gecko.nsString.Set(aValue.SetDataAttribute, (string)value);
                            aValueAsIUnknown = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsISupports>(aValue);
                            Instance.SetComplexValue(prefName, ref guid, aValueAsIUnknown);
                        }
                        finally
                        {
                            BrowseoFXpcom.FreeComObject(ref aValueAsIUnknown);
                            BrowseoFXpcom.FreeComObject(ref aValue);
                        }
                        break;
                    case PREF_INT:
                        Instance.SetIntPref(prefName, (int)value);
                        break;
                    case PREF_BOOL:
                        Instance.SetBoolPref(prefName, (bool)value);
                        break;
                }
            }
        }

        public void SetLocalizedString(string prefName, string value)
        {
            if (string.IsNullOrEmpty(prefName))
                throw new ArgumentException("prefName");
            else if (value == null)
                throw new ArgumentNullException("value");

            Guid guid = typeof(Gecko.Interfaces.nsISupportsString).GUID;
            Gecko.Interfaces.nsISupports aValueAsIUnknown = null;
            var aValue = BrowseoFXpcom.CreateInstance<Gecko.Interfaces.nsIPrefLocalizedString>(Gecko.Contracts.PrefLocalizedString);
            try
            {
                aValue.SetDataAttribute(value);
                aValueAsIUnknown = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsISupports>(aValue);
                Instance.SetComplexValue(prefName, ref guid, aValueAsIUnknown);
            }
            finally
            {
                BrowseoFXpcom.FreeComObject(ref aValueAsIUnknown);
                BrowseoFXpcom.FreeComObject(ref aValue);
            }
        }

        public string GetLocalizedString(string prefName)
        {
            if (string.IsNullOrEmpty(prefName))
                throw new ArgumentException("prefName");

            try
            {
                object comObject = null;
                Gecko.Interfaces.nsIPrefLocalizedString aString = null;
                Guid guid = typeof(Gecko.Interfaces.nsIPrefLocalizedString).GUID;
                IntPtr pUnk = Instance.GetComplexValue(prefName, ref guid);
                try
                {
                    comObject = Marshal.GetObjectForIUnknown(pUnk);
                    aString = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsIPrefLocalizedString>(comObject);
                    return aString.GetDataAttribute();
                }
                finally
                {
                    BrowseoFXpcom.FreeComObject(ref comObject);
                    BrowseoFXpcom.FreeComObject(ref aString);
                }
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode != Gecko.GeckoError.NS_ERROR_MALFORMED_URI)
                    throw;
                return this[prefName] as string;
            }
        }

        int GetValueType(object value)
        {
            if (value is int)
                return PREF_INT;
            else if (value is string)
                return PREF_STRING;
            else if (value is bool)
                return PREF_BOOL;

            throw new ArgumentException("Gecko preferences must be either a String, Int32, or Boolean value.", "prefName");
        }

        /// <summary>
        /// Gets the <see cref="Type"/> of the specified preference.
        /// </summary>
        /// <param name="name">Required. The name of the preference whose type is returned.</param>
        /// <returns></returns>
        public Type GetPreferenceType(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            switch (Instance.GetPrefType(name))
            {
                case PREF_STRING: return typeof(string);
                case PREF_INT: return typeof(int);
                case PREF_BOOL: return typeof(bool);
            }
            return null;
        }

        /// <summary>
        /// Sets whether the specified preference is locked. Locking a preference will cause the preference service to always return the default value regardless of whether there is a user set value or not.
        /// </summary>
        /// <param name="name">Required. The preference to lock or unlock.</param>
        /// <param name="locked">True if the preference should be locked; otherwise, false, and the preference is unlocked.</param>
        public void SetLocked(string name, bool locked)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            if (locked)
                Instance.LockPref(name);
            else
                Instance.UnlockPref(name);
        }

        /// <summary>
        /// Gets whether the specified preference is locked.
        /// </summary>
        /// <param name="name">Required. The preference whose lock status is returned.</param>
        /// <returns></returns>
        public bool GetLocked(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            return Instance.PrefIsLocked(name);
        }

        /// <summary>
        /// Clear user preferences
        /// </summary>
        /// <param name="name">Required. The preference to lock or unlock.</param>
        public void Clear(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name");

            Instance.ClearUserPref(name);
        }
    }

   

    public class BrowseoFXpcomStartupObserver : Gecko.Interfaces.nsIObserver
    {
        public BrowseoFXpcomStartupObserver()
        {
            Gecko.Interfaces.nsIObserverService os = BrowseoFXpcom.GetService<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService);

            os.AddObserver(this, "quit-application", false);
            os.AddObserver(this, "quit-application", false);
            os.AddObserver(this, "quit-application-forced", false);
            os.AddObserver(this, "sessionstore-init-started", false);
            os.AddObserver(this, "sessionstore-windows-restored", false);
            os.AddObserver(this, "profile-change-teardown", false);
            os.AddObserver(this, "xul-window-registered", false);
            os.AddObserver(this, "xul-window-destroyed", false);
            os.AddObserver(this, "profile-before-change", false);
            os.AddObserver(this, "xpcom-shutdown", false);

        }
        public void Observe([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.WStringMarshaler))] string aData)
        {
            switch (aTopic)
            {
                default:
                    break;
            }
        }
    }

    //public class BrowseoFXpcomWindowCreator : Gecko.Interfaces.nsIWindowCreator
    //{
    //    [return: MarshalAs(UnmanagedType.Interface)]
    //    public Gecko.Interfaces.nsIWebBrowserChrome CreateChromeWindow([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIWebBrowserChrome parent, uint chromeFlags)

    //    {
    //        Gecko.Interfaces.nsIXULWindow childXulWindow = null;

    //        using (var appShell = BrowseoFXpcom.GetService2<Gecko.Interfaces.nsIAppShellService>(Gecko.Contracts.AppShellService))
    //        {
    //            if (appShell == null)
    //                throw Marshal.GetExceptionForHR(Gecko.GeckoError.NS_ERROR_FAILURE);

    //            childXulWindow = appShell.Instance.CreateTopLevelWindow(null, null, (uint)chromeFlags, Gecko.Interfaces.nsIAppShellServiceConsts.SIZE_TO_CONTENT, Gecko.Interfaces.nsIAppShellServiceConsts.SIZE_TO_CONTENT, null, null);
    //        }

    //        if (childXulWindow != null)
    //        {
    //            Gecko.Interfaces.nsIWebBrowserChrome chromeWindow = BrowseoFXpcom.QueryInterface<Gecko.Interfaces.nsIWebBrowserChrome>(childXulWindow);
    //            BrowseoFXpcom.FreeComObject(ref childXulWindow);
    //            if (chromeWindow != null) return chromeWindow;
    //        }

    //        return null;
    //    }
    //}

    public sealed class GeckoDOMEventTarget : ComObject<nsIDOMEventTarget>, IGeckoObjectWrapper
    {
        public static GeckoDOMEventTarget Create(nsIDOMEventTarget instance)
        {
            return new GeckoDOMEventTarget(instance);
        }

        private GeckoDOMEventTarget(nsIDOMEventTarget instance)
            : base(instance)
        {

        }


        //public GeckoElement CastToGeckoElement()
        //{
        //    return Xpcom.QueryInterface<nsIDOMElement>(Instance).Wrap(GeckoElement.Create);
        //}



        //public bool DispatchEvent(GeckoEvent e)
        //{
        //    return Instance.DispatchEvent(e.Instance);
        //}

        public void AddEventListener(string type, nsIDOMEventListener listener, bool useCapture, bool wantUntrusted, byte argc)
        {
            using (var nType = new nsAString(type))
            {
                Instance.AddEventListener(nType, listener, useCapture, wantUntrusted, argc);
            }
        }

        public void RemoveEventListener(string type, nsIDOMEventListener listener, bool useCapture)
        {
            using (var nType = new nsAString(type))
            {
                Instance.RemoveEventListener(nType, listener, useCapture);
            }
        }

    }

    public sealed class GeckoWindow : ComObject<nsIDOMWindow>, IGeckoObjectWrapper
    {
        public static GeckoWindow Create(nsIDOMWindow instance)
        {
            return new GeckoWindow(instance);
        }

        private GeckoWindow(nsIDOMWindow window)  : base(window)
        {

        }
    }

    public sealed class GeckoWebNavigation : ComObject<nsIWebNavigation>, IGeckoObjectWrapper
    {
        public static GeckoWebNavigation Create(nsIWebNavigation instance)
        {
            return new GeckoWebNavigation(instance);
        }

        private GeckoWebNavigation(nsIWebNavigation instance)
            : base(instance)
        {

        }
    }

    public sealed class GeckoDocument : ComObject<nsIDOMDocument>, IGeckoObjectWrapper
    {
        public static GeckoDocument Create(nsIDOMDocument instance)
        {
            return new GeckoDocument(instance);
        }

        private GeckoDocument(nsIDOMDocument window) : base(window)
        {

        }
    }

    public class nsXULWindow : nsIBaseWindow, nsIInterfaceRequestor, nsIXULWindow, nsISupportsWeakReference
    {
        #region nsIBaseWindow
        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public double GetDevicePixelsPerDesktopPixelAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetEnabledAttribute()
        {
            throw new NotImplementedException();
        }

        public IntPtr GetMainWidgetAttribute()
        {
            throw new NotImplementedException();
        }

        public void GetNativeHandleAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase result)
        {
            throw new NotImplementedException();
        }

        public IntPtr GetParentNativeWindowAttribute()
        {
            throw new NotImplementedException();
        }

        public IntPtr GetParentWidgetAttribute()
        {
            throw new NotImplementedException();
        }

        public void GetPosition(out int x, out int y)
        {
            throw new NotImplementedException();
        }

        public void GetPositionAndSize(out int x, out int y, out int cx, out int cy)
        {
            throw new NotImplementedException();
        }

        public void GetSize(out int cx, out int cy)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))]
        public string GetTitleAttribute()
        {
            throw new NotImplementedException();
        }

        public double GetUnscaledDevicePixelsPerCSSPixelAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetVisibilityAttribute()
        {
            throw new NotImplementedException();
        }

        public void InitWindow(IntPtr parentNativeWindow, IntPtr parentWidget, int x, int y, int cx, int cy)
        {
            throw new NotImplementedException();
        }

        public void Repaint([MarshalAs(UnmanagedType.U1)] bool force)
        {
            throw new NotImplementedException();
        }

        public void SetEnabledAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            throw new NotImplementedException();
        }

        public void SetFocus()
        {
            throw new NotImplementedException();
        }

        public void SetParentNativeWindowAttribute(IntPtr value)
        {
            throw new NotImplementedException();
        }

        public void SetParentWidgetAttribute(IntPtr value)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void SetPositionAndSize(int x, int y, int cx, int cy, uint flags)
        {
            throw new NotImplementedException();
        }

        public void SetPositionDesktopPix(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void SetSize(int cx, int cy, [MarshalAs(UnmanagedType.U1)] bool fRepaint)
        {
            throw new NotImplementedException();
        }

        public void SetTitleAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string value)
        {
            throw new NotImplementedException();
        }

        public void SetVisibilityAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region nsIInterfaceRequestor
        public IntPtr GetInterface(ref Guid uuid)
        {
            return BrowseoFXpcom.QueryInterfaceForObject(this, uuid);
        }
        #endregion

        #region nsIXULWindow
        public void AddChildWindow([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aChild)
        {
            throw new NotImplementedException();
        }

        public void ApplyChromeFlags()
        {
            throw new NotImplementedException();
        }

        public void AssumeChromeFlagsAreFrozen()
        {
            throw new NotImplementedException();
        }

        public void Center([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aRelative, [MarshalAs(UnmanagedType.U1)] bool aScreen, [MarshalAs(UnmanagedType.U1)] bool aAlert)
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIXULWindow CreateNewWindow(int aChromeFlags, [MarshalAs(UnmanagedType.Interface)] nsITabParent aOpeningTab, [MarshalAs(UnmanagedType.Interface)] mozIDOMWindowProxy aOpener)
        {
            throw new NotImplementedException();
        }

        public uint GetChromeFlagsAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDocShellTreeItem GetContentShellById([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string ID)
        {
            throw new NotImplementedException();
        }

        public uint GetContextFlagsAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDocShell GetDocShellAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetIntrinsicallySizedAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIDocShellTreeItem GetPrimaryContentShellAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsITabParent GetPrimaryTabParentAttribute()
        {
            throw new NotImplementedException();
        }

        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIXULBrowserWindow GetXULBrowserWindowAttribute()
        {
            throw new NotImplementedException();
        }

        public uint GetZLevelAttribute()
        {
            throw new NotImplementedException();
        }

        public void RemoveChildWindow([MarshalAs(UnmanagedType.Interface)] nsIXULWindow aChild)
        {
            throw new NotImplementedException();
        }

        public void SetChromeFlagsAttribute(uint value)
        {
            throw new NotImplementedException();
        }

        public void SetContextFlagsAttribute(uint value)
        {
            throw new NotImplementedException();
        }

        public void SetIntrinsicallySizedAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            throw new NotImplementedException();
        }

        public void SetXULBrowserWindowAttribute([MarshalAs(UnmanagedType.Interface)] nsIXULBrowserWindow value)
        {
            throw new NotImplementedException();
        }

        public void SetZLevelAttribute(uint value)
        {
            throw new NotImplementedException();
        }

        public void ShowModal()
        {
            throw new NotImplementedException();
        }

        public void SizeShellToWithLimit(int aDesiredWidth, int aDesiredHeight, int shellItemWidth, int shellItemHeight)
        {
            throw new NotImplementedException();
        }

        public void TabParentAdded([MarshalAs(UnmanagedType.Interface)] nsITabParent aTab, [MarshalAs(UnmanagedType.U1)] bool aPrimary)
        {
            throw new NotImplementedException();
        }

        public void TabParentRemoved([MarshalAs(UnmanagedType.Interface)] nsITabParent aTab)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region nsISupportsWeakReference
        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIWeakReference GetWeakReference()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
