using Gecko;
using Gecko.DOM;
using Gecko.GUI;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace TestXulApp
{
    //    public interface IGeckoObjectWrapper
    //    {
    //    }

    //    internal interface IComObject
    //    {
    //        object NativeInstance { get; }
    //        Type GetComObjectType();
    //    }

    //    internal static class GeckoObjectCache
    //    {
    //        internal struct CacheKey
    //        {
    //            private IntPtr _iUnknown;
    //            private Type _comObjectType;

    //            public CacheKey(IntPtr iUnknown, Type comObjectType)
    //            {
    //                _iUnknown = iUnknown;
    //                _comObjectType = comObjectType;
    //            }

    //            public IntPtr IUnknown
    //            {
    //                get { return _iUnknown; }
    //            }

    //            public override int GetHashCode()
    //            {
    //                return _iUnknown.GetHashCode();
    //            }

    //            public override bool Equals(object obj)
    //            {
    //                if (obj is CacheKey)
    //                {
    //                    CacheKey key = (CacheKey)obj;
    //                    return _iUnknown == key._iUnknown && (_comObjectType.IsAssignableFrom(key._comObjectType) || key._comObjectType.IsAssignableFrom(_comObjectType));
    //                }
    //                return false;
    //            }
    //        }

    //        private static readonly Dictionary<CacheKey, WeakReference> _cache = new Dictionary<CacheKey, WeakReference>();
    //        private static readonly ReaderWriterLockSlim syncRoot = new ReaderWriterLockSlim();

    //        internal static CacheKey GetKey(object comObject, Type typeOfWrapper)
    //        {
    //            IntPtr pUnk = Marshal.GetIUnknownForObject(comObject);
    //            Marshal.Release(pUnk);
    //            return new CacheKey(pUnk, typeOfWrapper);
    //        }

    //        public static object Get<TInterface, TWrapper>(TInterface comObject)
    //            where TInterface : class
    //            where TWrapper : IGeckoObjectWrapper
    //        {
    //            WeakReference weakRef;

    //            CacheKey key = GetKey(comObject, typeof(TWrapper));

    //            syncRoot.EnterReadLock();
    //            try
    //            {
    //                if (!_cache.TryGetValue(key, out weakRef))
    //                    return null;
    //            }
    //            finally
    //            {
    //                syncRoot.ExitReadLock();
    //            }

    //            return weakRef.Target;
    //        }

    //        public static CacheKey Set<TInterface>(TInterface comObject, IGeckoObjectWrapper wrapper)
    //            where TInterface : class
    //        {
    //            WeakReference weakRef = new WeakReference(wrapper);

    //            CacheKey key = GetKey(comObject, ((IComObject)wrapper).GetComObjectType());

    //            syncRoot.EnterWriteLock();
    //            try
    //            {
    //                _cache[key] = weakRef;
    //            }
    //            finally
    //            {
    //                syncRoot.ExitWriteLock();
    //            }
    //            return key;
    //        }

    //        public static void Remove(CacheKey key)
    //        {
    //            syncRoot.EnterWriteLock();
    //            try
    //            {
    //                _cache.Remove(key);
    //            }
    //            finally
    //            {
    //                syncRoot.ExitWriteLock();
    //            }

    //        }
    //    }


    //    public class ComObject1<TInterface> : IDisposable, IEquatable<ComObject1<TInterface>>, IEquatable<TInterface>, IComObject
    //        where TInterface : class
    //    {

    //        internal TInterface _instance;
    //        private GeckoObjectCache.CacheKey _cacheKey;

    //        #region ctor & dtor

    //        public ComObject1(TInterface instance)
    //        {
    //            if (instance == null)
    //                throw new ArgumentNullException("instance");

    //            _instance = instance;


    //            IGeckoObjectWrapper wrapper = this as IGeckoObjectWrapper;
    //            if (wrapper != null)
    //                _cacheKey = GeckoObjectCache.Set(instance, wrapper);
    //        }

    //        ~ComObject1()
    //        {
    //            Dispose(false);
    //        }

    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (this is IGeckoObjectWrapper)
    //                GeckoObjectCache.Remove(_cacheKey);

    //           // Xpcom.FreeComObject(ref _instance);
    //            if (disposing)
    //            {
    //                GC.SuppressFinalize(this);
    //            }
    //        }

    //        void IDisposable.Dispose()
    //        {
    //            if (_instance != null)
    //            {
    //                Dispose(true);
    //            }
    //        }

    //        #endregion

    //        public TInterface Instance
    //        {
    //            get
    //            {
    //               // Xpcom.AssertCorrectThread();
    //                if (_instance == null)
    //                    throw new ObjectDisposedException(this.GetType().Name);

    //                return _instance;
    //            }
    //        }

    //        protected IntPtr Unknown
    //        {
    //            get
    //            {
    //                if (_instance == null)
    //                    throw new ObjectDisposedException(this.GetType().Name);

    //                if (_cacheKey.IUnknown != IntPtr.Zero)
    //                    return _cacheKey.IUnknown;

    //                if (this is IGeckoObjectWrapper)
    //                    throw new InvalidOperationException(); // the key must be created in the constructor

    //                _cacheKey = GeckoObjectCache.GetKey(_instance, this.GetType());
    //                return _cacheKey.IUnknown;
    //            }
    //        }

    //        public T QueryInterface<T>()
    //            where T : class
    //        {
    //            return Program.QueryInterface<T>(this.Instance);
    //        }

    //        object IComObject.NativeInstance
    //        {
    //            get
    //            {
    //                return this.Instance;
    //            }
    //        }

    //        Type IComObject.GetComObjectType()
    //        {
    //            return typeof(ComObject1<TInterface>);
    //        }

    //        #region Equality

    //        public bool Equals(ComObject1<TInterface> other)
    //        {
    //            if (ReferenceEquals(this, other)) return true;
    //            if (ReferenceEquals(null, other)) return false;
    //            return this.Unknown == other.Unknown;
    //        }

    //        public bool Equals(TInterface other)
    //        {
    //            if (ReferenceEquals(null, other)) return false;
    //            IntPtr pUnk = Marshal.GetIUnknownForObject(other);
    //            Marshal.Release(pUnk);
    //            return this.Unknown == pUnk;
    //        }

    //        public override bool Equals(object obj)
    //        {
    //            if (ReferenceEquals(this, obj)) return true;
    //            if (ReferenceEquals(null, obj)) return false;
    //            if (!(obj is ComObject1<TInterface>)) return false;
    //            return this.Unknown == ((ComObject1<TInterface>)obj).Unknown;
    //        }

    //        public override int GetHashCode()
    //        {
    //            return this.Unknown.GetHashCode();
    //        }


    //        #endregion


    //    }

    //    public class GeckoXULWindow1 : ComObject1<nsIXULWindow>, nsIWebProgressListener, nsISupportsWeakReference
    //    {
    //        private ComObject1<nsIBaseWindow> _baseWindow;
    //        private ComObject1<nsIDocShell> _docshell;
    //        private ComObject1<nsIWebProgress> _progress;
    //        private bool _isChromeLoaded;
    //        private GeckoWeakReference _weakRef;

    //        private static nsIXULWindow CreateTopLevelWindow()
    //        {
    //            nsIURI uri = null;
    //            nsIXULWindow window = null;
    //            nsIAppShellService appShellSvc = null;
    //            try
    //            {
    //                var io = Program.GetService<nsIIOService2>(Contracts.NetworkIOService);
    //                using (var str = new nsAUTF8String("chrome://browser/content/browser.xul"))
    //                {
    //                    uri = io.NewURI(str, null, null);
    //                }

    //                appShellSvc = Program.GetService<nsIAppShellService>(Contracts.AppShellService);
    //                if (appShellSvc != null)
    //                {
    //                    window = appShellSvc.CreateTopLevelWindow(null, uri, (uint)nsIWebBrowserChromeConsts.CHROME_ALL, nsIAppShellServiceConsts.SIZE_TO_CONTENT, nsIAppShellServiceConsts.SIZE_TO_CONTENT, null, null);
    //                }
    //            }
    //            finally
    //            {
    //                //Xpcom.FreeComObject(ref uri);
    //                //Xpcom.FreeComObject(ref appShellSvc);
    //            }

    //            if (window != null)
    //                return window;

    //            throw Marshal.GetExceptionForHR(GeckoError.NS_ERROR_FAILURE);
    //        }

    //        public GeckoXULWindow1()
    //            : base(CreateTopLevelWindow())
    //        {
    //            _baseWindow = Program.QueryInterface2<nsIBaseWindow>(Instance);

    //            _docshell = Program.AsComObject1(Instance.GetDocShellAttribute());
    //            _progress = Program.AsComObject1(_docshell.QueryInterface<nsIWebProgress>()); 
    //            if (_progress != null)
    //            {
    //                _progress.Instance.AddProgressListener(this, (uint)(nsIWebProgressConsts.NOTIFY_ALL) | (uint)(nsIWebProgressConsts.NOTIFY_STATE_ALL));
    //            }
    //            // TODO: wait until the chrome has loaded
    //            //Xpcom.DoEvents(); throws InvalidOperationException on WPF
    //        }

    //        protected override void Dispose(bool disposing)
    //        {
    //            if (disposing && _progress != null)
    //            {
    //                Close();
    //                try
    //                {
    //                    _progress.Instance.RemoveProgressListener(this);
    //                }
    //                catch (COMException e)
    //                {
    //                    if (e.ErrorCode != GeckoError.NS_ERROR_FAILURE)
    //                        throw;
    //                }
    //                finally
    //                {
    //                    ((IDisposable)_progress).Dispose();
    //                    _progress = null;
    //                }
    //            }
    //            if (_baseWindow != null)
    //            {
    //                ((IDisposable)_baseWindow).Dispose();
    //                _baseWindow = null;
    //            }
    //            if (_docshell != null)
    //            {
    //                ((IDisposable)_docshell).Dispose();
    //                _docshell = null;
    //            }
    //            base.Dispose(disposing);
    //        }

    //        public void Dispose()
    //        {
    //            ((IDisposable)this).Dispose();
    //        }

    //        private ComObject1<nsIBaseWindow> BaseWindow
    //        {
    //            get
    //            {
    //               // Xpcom.AssertCorrectThread();
    //                if (_baseWindow == null)
    //                    throw new ObjectDisposedException(this.GetType().Name);

    //                return _baseWindow;
    //            }
    //        }

    //        public string NativeHandle
    //        {
    //            get
    //            {
    //                if (_baseWindow == null) return null;
    //                return nsString.Get(_baseWindow.Instance.GetNativeHandleAttribute);
    //            }
    //        }

    //        public int Left
    //        {
    //            get
    //            {
    //                int x, y;
    //                BaseWindow.Instance.GetPosition(out x, out y);
    //                return x;
    //            }
    //            set
    //            {
    //                int x, y;
    //                BaseWindow.Instance.GetPosition(out x, out y);
    //                BaseWindow.Instance.SetPosition(value, y);
    //            }
    //        }

    //        public int Top
    //        {
    //            get
    //            {
    //                int x, y;
    //                BaseWindow.Instance.GetPosition(out x, out y);
    //                return y;
    //            }
    //            set
    //            {
    //                int x, y;
    //                BaseWindow.Instance.GetPosition(out x, out y);
    //                BaseWindow.Instance.SetPosition(x, value);
    //            }
    //        }

    //        public int Width
    //        {
    //            get
    //            {
    //                int w, h;
    //                BaseWindow.Instance.GetSize(out w, out h);
    //                return w;
    //            }
    //            set
    //            {
    //                if (value < 0)
    //                    throw new ArgumentOutOfRangeException("width");

    //                int w, h;
    //                BaseWindow.Instance.GetSize(out w, out h);
    //                BaseWindow.Instance.SetSize(value, h, true);
    //            }
    //        }

    //        public int Height
    //        {
    //            get
    //            {
    //                int w, h;
    //                BaseWindow.Instance.GetSize(out w, out h);
    //                return h;
    //            }
    //            set
    //            {
    //                if (value < 0)
    //                    throw new ArgumentOutOfRangeException("height");

    //                int w, h;
    //                BaseWindow.Instance.GetSize(out w, out h);
    //                BaseWindow.Instance.SetSize(w, value, true);
    //            }
    //        }

    //        public bool Visible
    //        {
    //            get
    //            {
    //                return BaseWindow.Instance.GetVisibilityAttribute();
    //            }
    //            set
    //            {
    //                BaseWindow.Instance.SetVisibilityAttribute(value);
    //            }
    //        }

    //        public GeckoWindow View
    //        {
    //            get
    //            {
    //                return Program.QueryInterface<nsIDOMWindow>(Instance).Wrap(GeckoWindow.Create);
    //            }
    //        }

    //        public void WaitUntilChromeLoad()
    //        {
    //            Program.ModalEventLoop(() => !_isChromeLoaded);
    //        }

    //        public void SetPosition(int x, int y)
    //        {
    //            BaseWindow.Instance.SetPosition(x, y);
    //        }

    //        public void SetSize(int width, int height, bool invalidate)
    //        {
    //            if (width < 0)
    //                throw new ArgumentOutOfRangeException("width");
    //            if (height < 0)
    //                throw new ArgumentOutOfRangeException("height");
    //            BaseWindow.Instance.SetSize(width, height, invalidate);
    //        }

    //        public void SetPositionAndSize(int x, int y, int width, int height, bool invalidate, bool delayResize)
    //        {
    //            if (width < 0)
    //                throw new ArgumentOutOfRangeException("width");
    //            if (height < 0)
    //                throw new ArgumentOutOfRangeException("height");

    //            uint flags = 0;
    //            if (invalidate) flags = flags | nsIBaseWindowConsts.eRepaint;
    //            if (delayResize) flags = flags | nsIBaseWindowConsts.eDelayResize;
    //            BaseWindow.Instance.SetPositionAndSize(x, y, width, height, flags);
    //        }

    //        public void Show()
    //        {
    //            this.Visible = true;
    //        }

    //        public void ShowDialog()
    //        {
    //            this.Visible = true;
    //            Instance.ShowModal();
    //        }

    //        public void Hide()
    //        {
    //            this.Visible = false;
    //        }

    //        public void Close()
    //        {
    //            if (_docshell != null)
    //            {
    //                GeckoWindow window = _docshell.QueryInterface<nsIDOMWindow>().Wrap(GeckoWindow.Create);
    //                if (window != null && !window.Closed)
    //                    window.Close();
    //            }
    //        }

    //        #region nsIWebProgressListener

    //        private bool IsThisDomWindow(nsIWebProgress aWebProgress)
    //        {
    //            bool result = false;
    //            if (aWebProgress.GetIsTopLevelAttribute())
    //            {
    //                mozIDOMWindowProxy thisWindow = Program.QueryInterface<mozIDOMWindowProxy>(Instance);
    //                try
    //                {
    //                    mozIDOMWindowProxy otherWindow = null;
    //                    try
    //                    {
    //                        otherWindow = aWebProgress.GetDOMWindowAttribute();
    //                        result = thisWindow == otherWindow;
    //                    }
    //                    finally
    //                    {
    //                       // Xpcom.FreeComObject(ref otherWindow);
    //                    }
    //                }
    //                finally
    //                {
    //                   // Xpcom.FreeComObject(ref thisWindow);
    //                }
    //            }
    //            return result;
    //        }

    //        void nsIWebProgressListener.OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus)
    //        {
    //            // if the notification is not about a document finishing, then just ignore it...
    //            if ((aStateFlags & nsIWebProgressListenerConsts.STATE_STOP) == 0 ||
    //                (aStateFlags & nsIWebProgressListenerConsts.STATE_IS_NETWORK) == 0)
    //            {
    //                return;
    //            }

    //            // if this notification is not for this window then ignore it...
    //            if (aWebProgress != null && IsThisDomWindow(aWebProgress))
    //            {
    //                _isChromeLoaded = true;
    //            }
    //        }

    //        void nsIWebProgressListener.OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress)
    //        {

    //        }

    //        void nsIWebProgressListener.OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint aFlags)
    //        {
    //            if (aWebProgress != null && IsThisDomWindow(aWebProgress))
    //                _isChromeLoaded = false;
    //        }

    //        void nsIWebProgressListener.OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage)
    //        {

    //        }

    //        void nsIWebProgressListener.OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState)
    //        {

    //        }

    //        #endregion

    //        #region nsISupportsWeakReference

    //        public nsIWeakReference GetWeakReference()
    //        {
    //            return _weakRef ?? (_weakRef = new GeckoWeakReference(this));
    //        }

    //        #endregion nsISupportsWeakReference
    //    }

    //    internal class DirectoryServiceProvider : nsIDirectoryServiceProvider
    //    {
    //        private string _profilePath;

    //        public DirectoryServiceProvider()
    //        {

    //        }

    //        public string GeckoPath { get; internal set; }

    //        public string ProfilePath
    //        {
    //            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "profile"); }
    //            //set
    //            //{
    //            //    if (value == null)
    //            //        _profilePath = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine("XulFx", "DefaultProfile")));
    //            //    else
    //            //        _profilePath = Path.GetFullPath(value);
    //            //    CreateDirectory(_profilePath);
    //            //}
    //        }

    //        public nsIFile GetFileInterface(string prop, out bool persistent)
    //        {
    //            IntPtr nsFilePtr = GetFile(prop, out persistent);
    //            try
    //            {
    //                return (nsIFile)Marshal.GetTypedObjectForIUnknown(nsFilePtr, typeof(nsIFile));
    //            }
    //            finally
    //            {
    //                Marshal.Release(nsFilePtr);
    //            }
    //        }

    //        public IntPtr GetFile(string prop, out bool persistent)
    //        {
    //            persistent = false;
    //            switch (prop)
    //            {
    //                case "ProfD":
    //                case "ProfLD":
    //                case "ProfDS":
    //                case "ProfLDS":
    //                case "cachePDir":
    //                case "PrefD":
    //                case "permissionDBPDir":
    //                    return NewLocalFileAsNsPtr(this.ProfilePath);
    //                //case "UAppData":
    //                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "UAppData"));
    //                //case "AppData":
    //                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "AppData"));
    //                //case "XREAppFeat":
    //                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "XREAppFeat"));
    //                //case "XREUSysExt":
    //                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "XREUSysExt"));
    //                //case "XREAddonAppDir":
    //                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "XREAddonAppDir"));
    //                //case "LocalAppData":
    //                //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "LocalAppData"));

    //                //case "XREAppDist":
    //                //    return NewLocalFileAsNsPtr(this.GeckoPath);

    //                case "UChrm":
    //                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "chrome"));
    //                case "TmpD":
    //                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "tmp"));
    //                case "UMimTyp": // required to handle mailto protocol, etc.
    //                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "mimeTypes.rdf"));
    //                case "LclSt":
    //                    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "localstore.rdf"));
    //                case "WinD":
    //                    return NewLocalFileAsNsPtr(Path.GetDirectoryName(Environment.SystemDirectory));
    //                case "XCurProcD":
    //                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser"));
    //                    //case "PrfDef":
    //                    //    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "defaults\\pref"));

    //            }
    //            Debug.WriteLine("Gecko.Xpcom.DirectoryServiceProvider.GetFile: not implemented: {0}", prop);
    //            return IntPtr.Zero;
    //        }

    //        //public static string RuntimePath
    //        //{
    //        //    get
    //        //    {
    //        //        return typeof(Xpcom).Assembly.Location;
    //        //    }
    //        //}

    //        public static IntPtr NewLocalFileAsNsPtr(string filename)
    //        {
    //            IntPtr result;
    //            using (var fileName = new nsAString(filename))
    //            {
    //                int error = Program.NS_NewLocalFile(fileName, true, out result);
    //                Marshal.ThrowExceptionForHR(error);
    //            }
    //            return result;
    //        }

    //        public static nsIFile NewLocalFile(string filename)
    //        {
    //            nsIFile result;
    //            using (var fileName = new nsAString(filename))
    //            {
    //                int error = Program.NS_NewLocalFile(fileName, true, out result);
    //                Marshal.ThrowExceptionForHR(error);
    //            }
    //            return result;
    //        }

    //        public static void CreateDirectory(string directory)
    //        {
    //            if (!Directory.Exists(directory))
    //            {
    //                CreateDirectory(Path.GetDirectoryName(directory));
    //                Directory.CreateDirectory(directory);
    //            }
    //        }
    //    }

    //    class Program
    //	{
    //        internal const string XPCOM = "xul.dll";

    //        public const int NS_OK = 0;

    //        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    //        [return: MarshalAs(UnmanagedType.Bool)]
    //        public static extern bool SetDllDirectory(string lpPathName);

    //        /// <summary>
    //		/// Declaration in nsXPCOM.h
    //		/// XPCOM_API(nsresult) NS_NewLocalFile(const nsAString &amp;path, bool followLinks, nsIFile* *result);
    //		/// </summary>
    //		/// <param name="path"></param>
    //		/// <param name="followLinks"></param>
    //		/// <param name="result"></param>
    //		/// <returns></returns>
    //		[DllImport(XPCOM, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    //        internal static extern int NS_NewLocalFile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AStringMarshaler))] nsAString path, [MarshalAs(UnmanagedType.U1)] bool followLinks, [MarshalAs(UnmanagedType.Interface)] out nsIFile result);
    //        [DllImport(XPCOM, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    //        internal static extern int NS_NewLocalFile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AStringMarshaler))] nsAString path, [MarshalAs(UnmanagedType.U1)] bool followLinks, out IntPtr result);

    //        /// <summary>
    //        /// Declaration in nsXPCOM.h
    //        /// XPCOM_API(nsresult) NS_InitXPCOM2(nsIServiceManager* *result, nsIFile* binDirectory, nsIDirectoryServiceProvider* appFileLocationProvider);
    //        /// </summary>
    //        /// <param name="serviceManager"></param>
    //        /// <param name="binDirectory"></param>
    //        /// <param name="appFileLocationProvider"></param>
    //        /// <returns></returns>
    //        [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    //        internal static extern int NS_InitXPCOM2([MarshalAs(UnmanagedType.Interface)] out nsIServiceManager serviceManager, [MarshalAs(UnmanagedType.Interface)] nsIFile binDirectory, nsIDirectoryServiceProvider appFileLocationProvider);

    //        /// <summary>
    //        /// Declaration in nsXPCOM.h
    //        /// XPCOM_API(nsresult) NS_GetComponentManager(nsIComponentManager* *result);
    //        /// </summary>
    //        /// <param name="componentManager"></param>
    //        /// <returns></returns>
    //        [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    //        internal static extern int NS_GetComponentManager([MarshalAs(UnmanagedType.Interface)] out nsIComponentManager componentManager);


    //        /// <summary>
    //        /// Declaration in nsXPCOM.h
    //        /// XPCOM_API(nsresult) NS_GetComponentRegistrar(nsIComponentRegistrar* *result);
    //        /// </summary>
    //        /// <param name="componentRegistrar"></param>
    //        /// <returns></returns>
    //        [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    //        internal static extern int NS_GetComponentRegistrar([MarshalAs(UnmanagedType.Interface)] out nsIComponentRegistrar componentRegistrar);

    //        [DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    //        internal static extern int NS_GetServiceManager([MarshalAs(UnmanagedType.Interface)] out nsIServiceManager servicemanager);


    //        private static ComObject1<nsIComponentManager> _componentManager;
    //        private static ComObject1<nsIComponentRegistrar> _componentRegistrar;
    //        private static ComObject1<nsIServiceManager> _serviceManager;

    //        [STAThread]
    //		static void Main(string[] args)
    //		{
    //            int nsresult;

    //            var libPath = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin";
    //            FXAppStartup.Init(libPath);

    //            SetDllDirectory(libPath);

    //            Environment.SetEnvironmentVariable("path", Environment.GetEnvironmentVariable("path") + ";" + libPath);

    //            string oldCurrentDir = Directory.GetCurrentDirectory();

    //            nsIFile mreAppDir = null;
    //            using (var str = new nsAString(libPath))
    //            {
    //                nsresult = NS_NewLocalFile(str, true, out mreAppDir);
    //                if (nsresult != NS_OK)
    //                    throw new COMException("Failed on NS_NewLocalFile", nsresult);
    //            }

    //            Directory.SetCurrentDirectory(libPath);


    //            nsIServiceManager serviceManager;
    //            nsresult = NS_InitXPCOM2(out serviceManager, mreAppDir, new DirectoryServiceProvider());
    //            _serviceManager = new ComObject1<nsIServiceManager>(serviceManager);
    //            serviceManager = null;

    //            // get some global objects we will need later
    //            nsIComponentManager componentManager;
    //            NS_GetComponentManager(out componentManager);
    //            _componentManager = new ComObject1<nsIComponentManager>(componentManager);
    //            componentManager = null;

    //            nsIComponentRegistrar componentRegistrar;
    //            NS_GetComponentRegistrar(out componentRegistrar);
    //            _componentRegistrar = new ComObject1<nsIComponentRegistrar>(componentRegistrar);
    //            componentRegistrar = null;


    //            Directory.SetCurrentDirectory(oldCurrentDir);

    //            using (var windowWatcher = Program.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
    //            {
    //                windowWatcher.Instance.SetWindowCreator(WindowCreator.Instance);
    //            }

    //            var xulWindow1 = new GeckoXULWindow1();
    //            xulWindow1.WaitUntilChromeLoad();
    //            xulWindow1.Width = 600;
    //            xulWindow1.Height = 400;
    //            xulWindow1.ShowDialog();





    //            Xpcom.ProfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "profile");

    //			string xulrunnerPath = XULRunnerLocator.GetXULRunnerLocation();
    //			//Xpcom.DebugPrint("xulrunner path = {0}", xulrunnerPath);
    //			//if (Xpcom.IsLinux)
    //			//{
    //			//	if (!(Environment.GetEnvironmentVariable("LD_LIBRARY_PATH") ?? string.Empty).Contains(xulrunnerPath))
    //			//		throw new ApplicationException(String.Format("LD_LIBRARY_PATH must contain {0}", xulrunnerPath));
    //			//}

    //			//if(Xpcom.IsMono)
    //			//	SynchronizationContext.SetSynchronizationContext(new GeckoSynchronizationContext());

    //			Xpcom.Initialize(xulrunnerPath);

    //            GeckoPreferences pref = GeckoPreferences.Default;
    //            pref["extensions.getAddons.cache.enabled"] = false;

    //            ConsoleObserver.Init();

    //			//DownloadManager.Init();
    //			//Xpcom.BeforeShutdown += (s, e) =>
    //			//{
    //			//	DownloadManager.Shutdown();
    //			//};
    //			//DownloadManager.DownloadRequest += DownloadManager_DownloadRequest;
    //			//DownloadManager.DownloadCompleted += DownloadManager_DownloadCompleted;
    //			//CertOverrideService.GetService().ValidityOverride += Certificate_ValidityOverride;

    //#if GTK
    //			Gtk.Application.Init();
    //#endif
    //			//GeckoPreferences.Default["browser.startup.homepage"] = "about:home";
    //			//GeckoPreferences.User["browser.startup.homepage"] = "about:home";

    //            var xulWindow = new GeckoXULWindow();
    //			xulWindow.WaitUntilChromeLoad();
    //			xulWindow.Width = 600;
    //			xulWindow.Height = 400;

    //			//var xulDocument = xulWindow.View.Document;

    //			//Xpcom.DebugPrint(xulDocument);
    //			//GeckoElement window = xulDocument.DocumentElement;//.GetElementById("rootWindow");
    //			//window.SetAttribute("title", "Hello, World!");

    //			//GeckoElement browser = xulDocument.CreateElement("browser");
    //			//browser.SetAttribute("id", "tab1");
    //			//browser.SetAttribute("flex", "1");
    //			//browser.SetAttribute("type", "content-targetable");
    //			//browser.SetAttribute("src", "about:memory");
    //			//window.AppendChild(browser);


    //			//GeckoElement button = xulDocument.CreateElement("button");
    //			//button.SetAttribute("label", "Go to Google");
    //			//button.SetAttribute("oncommand", "alert(document.getElementById('tab1').contentWindow.location.href = 'http://localhost:12111/geckofx/index.php')");
    //			//window.AppendChild(button);


    //			xulWindow.ShowDialog();

    //			Console.WriteLine("shutdown");
    //			Xpcom.Shutdown();
    //		}

    //        #region QueryInterface

    //        /// <summary>
    //        /// A special declaration of nsIInterfaceRequestor used only for QueryInterface, using PreserveSig
    //        /// to prevent .NET from throwing an exception when the interface doesn't exist.
    //        /// </summary>
    //        [Guid("033a1470-8b2a-11d3-af88-00a024ffc08c"), ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    //        interface QI_nsIInterfaceRequestor
    //        {

    //            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    //            [PreserveSig]
    //            int GetInterface(ref Guid uuid, out IntPtr pUnk);
    //        }

    //        public static IntPtr QueryInterfaceForObject(object obj, Guid iid)
    //        {
    //            if (obj == null)
    //                return IntPtr.Zero;

    //            // get an nsISupports (aka IUnknown) pointer from the object
    //            IntPtr pUnk = Marshal.GetIUnknownForObject(obj);
    //            if (pUnk == IntPtr.Zero)
    //                return IntPtr.Zero;

    //            // query interface
    //            IntPtr ppv;
    //            Marshal.QueryInterface(pUnk, ref iid, out ppv);

    //            // if QueryInterface didn't work, try using nsIInterfaceRequestor instead
    //            if (ppv == IntPtr.Zero)
    //            {
    //                // QueryInterface the object for nsIInterfaceRequestor
    //                Guid interfaceRequestorIID = typeof(nsIInterfaceRequestor).GUID;
    //                IntPtr pInterfaceRequestor;
    //                Marshal.QueryInterface(pUnk, ref interfaceRequestorIID, out pInterfaceRequestor);

    //                // if we got a pointer to nsIInterfaceRequestor
    //                if (pInterfaceRequestor != IntPtr.Zero)
    //                {
    //                    // convert it to a managed interface
    //                    object comObject = Marshal.GetObjectForIUnknown(pInterfaceRequestor);
    //                    try
    //                    {
    //                        QI_nsIInterfaceRequestor req = comObject as QI_nsIInterfaceRequestor;
    //                        if (req != null)
    //                        {

    //                            try
    //                            {
    //                                req.GetInterface(ref iid, out ppv);
    //                            }
    //                            catch (NullReferenceException ex)
    //                            {
    //                                Debug.WriteLine("NullRefException from native code.\n" + ex.ToString());
    //                            }
    //                        }
    //                        else if (comObject != null)
    //                        {
    //                            nsIInterfaceRequestor req2 = (nsIInterfaceRequestor)comObject;
    //                            try
    //                            {
    //                                ppv = req2.GetInterface(ref iid);
    //                            }
    //                            catch (InvalidCastException)
    //                            {
    //                                ppv = IntPtr.Zero;
    //                            }
    //                            catch (NullReferenceException ex)
    //                            {
    //                                Debug.WriteLine("NullRefException from native code.\n" + ex.ToString());
    //                            }
    //                        }
    //                    }
    //                    finally
    //                    {
    //                        //if (Marshal.IsComObject(comObject))
    //                        //    Xpcom.ReleaseComObject(comObject);
    //                        Marshal.Release(pInterfaceRequestor);
    //                    }
    //                }
    //            }

    //            Marshal.Release(pUnk);
    //            return ppv;
    //        }

    //        public static object QueryInterface(object obj, Guid iid)
    //        {
    //            IntPtr ppv = QueryInterfaceForObject(obj, iid);
    //            if (ppv == IntPtr.Zero)
    //                return null;

    //            object result = Marshal.GetObjectForIUnknown(ppv);
    //            Marshal.Release(ppv);
    //            return result;
    //        }

    //        public static TInterface QueryInterface<TInterface>(object obj)
    //            where TInterface : class
    //        {
    //            Type interfaceType = typeof(TInterface);
    //            IntPtr ppv = QueryInterfaceForObject(obj, interfaceType.GUID);
    //            if (ppv == IntPtr.Zero)
    //                return null;

    //            TInterface result = (TInterface)Marshal.GetTypedObjectForIUnknown(ppv, interfaceType);
    //            Marshal.Release(ppv);
    //            return result;
    //        }

    //        public static ComObject1<TInterface> QueryInterface2<TInterface>(object obj) where TInterface : class
    //        {
    //            return new ComObject1<TInterface>(QueryInterface<TInterface>(obj));

    //            //return QueryInterface<TInterface>(obj).AsComObject1<TInterface>();
    //        }

    //        public static ComObject1<TInterface> AsComObject1<TInterface>(TInterface instance) where TInterface : class
    //        {
    //            if (instance == null) return null;
    //            return new ComObject1<TInterface>(instance);
    //        }

    //        #endregion

    //        public static TInterfaceType GetService<TInterfaceType>(string contractID)
    //        {
    //            Guid iid = typeof(TInterfaceType).GUID;
    //            IntPtr pUnk = _serviceManager.Instance.GetServiceByContractID(contractID, ref iid);
    //            TInterfaceType result = (TInterfaceType)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(TInterfaceType));
    //            Marshal.Release(pUnk);
    //            return result;
    //        }

    //        public static ComObject1<TInterfaceType> GetService2<TInterfaceType>(string contractID) where TInterfaceType : class
    //        {
    //            return new ComObject1<TInterfaceType>(GetService<TInterfaceType>(contractID));
    //        }

    //        private static long _modalLoopCounter;
    //        private static volatile Exception _mainThreadException;
    //        public static void ModalEventLoop(Func<bool> condition)
    //        {
    //            ModalEventLoop(thread => condition(), true);
    //        }
    //        public static void ModalEventLoop(Func<nsIThread, bool> condition, bool mayWait)
    //        {
    //            nsIThreadManager threadMan = GetService<nsIThreadManager>(Contracts.ThreadManager);
    //            nsIThread thread = threadMan.GetCurrentThreadAttribute();
    //           // Xpcom.FreeComObject(ref threadMan);

    //            Interlocked.Increment(ref _modalLoopCounter);
    //            try
    //            {
    //                while (condition(thread))
    //                {
    //                    _mainThreadException = null;
    //                    bool isProcessed = thread.ProcessNextEvent(mayWait);
    //                    Exception ex = _mainThreadException;
    //                    _mainThreadException = null;
    //                    if (ex != null)
    //                        throw ex;
    //                    if (!isProcessed)
    //                        break;

    //                }
    //            }
    //            finally
    //            {
    //                Interlocked.Decrement(ref _modalLoopCounter);
    //                //Xpcom.FreeComObject(ref thread);
    //            }
    //        }
    //    }


    //    class FXAppStartup
    //    {
    //        private static ComObject1<nsIAppShell> mAppShell;
    //        private static ComObject1<nsIServiceManager> _serviceManager;

    //        public static void Init(string libPath)
    //        {
    //            int nsresult;

    //            Program.SetDllDirectory(libPath);
    //            Environment.SetEnvironmentVariable("path", Environment.GetEnvironmentVariable("path") + ";" + libPath);

    //            nsIFile mreAppDir = null;
    //            using (var str = new nsAString(libPath))
    //            {
    //                nsresult = Program.NS_NewLocalFile(str, true, out mreAppDir);
    //                if (nsresult != Program.NS_OK)
    //                    throw new COMException("Failed on NS_NewLocalFile", nsresult);
    //            }

    //            string oldCurrentDir = Directory.GetCurrentDirectory();
    //            Directory.SetCurrentDirectory(libPath);

    //            nsIServiceManager serviceManager;
    //            nsresult = Program.NS_InitXPCOM2(out serviceManager, mreAppDir, null);
    //            _serviceManager = new ComObject1<nsIServiceManager>(serviceManager);
    //            serviceManager = null;

    //            Guid iid = typeof(nsISupports).GUID;
    //            var classid = new Guid("7cd5c71d-223b-4afe-931d-5eedb1f2b01f");
    //            var appshellpointer = _serviceManager.Instance.GetService(ref classid, ref iid);

    //            mAppShell = GetService2<nsIAppShell>(Contracts.AppShellService);
    //        }

    //        public static TInterfaceType GetService<TInterfaceType>(string contractID)
    //        {
    //            Guid iid = typeof(TInterfaceType).GUID;
    //            IntPtr pUnk = _serviceManager.Instance.GetServiceByContractID(contractID, ref iid);
    //            TInterfaceType result = (TInterfaceType)Marshal.GetTypedObjectForIUnknown(pUnk, typeof(TInterfaceType));
    //            Marshal.Release(pUnk);
    //            return result;
    //        }

    //        public static ComObject1<TInterfaceType> GetService2<TInterfaceType>(string contractID) where TInterfaceType : class
    //        {
    //            return new ComObject1<TInterfaceType>(GetService<TInterfaceType>(contractID));
    //        }
    //    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var XulComponents = @"C:\Users\eli\Desktop\temp\omni1\omni\components - Copy";
            XulComponents = XulComponents.Replace("\\\\", "\\");
            Xpcom.ComponentsPath = XulComponents;
            
            var xulfxPath = System.IO.Path.Combine(@"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\bin\Debug", "XulFx.xpi");
            Xpcom.XulfxPath = xulfxPath;

            //var profileDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\bin\Debug\profile";
            //var profileDirectory = @"C:\Users\eli\AppData\Roaming\Mozilla\Firefox\Profiles\3kw8mj5i.default";
            //Xpcom.ProfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BrowserFX");
            Xpcom.ProfileName = "BrowserFX.Demo";

            var binDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin";
            //var binDirectory = @"C:\Program Files (x86)\Mozilla Firefox";
            Xpcom.Initialize(binDirectory);
            
            ConsoleObserver.Init();

            //DownloadManager.Init();
            //Xpcom.BeforeShutdown += (s, e) =>
            //{
            //    DownloadManager.Shutdown();
            //};
            //DownloadManager.DownloadRequest += DownloadManager_DownloadRequest;
            //DownloadManager.DownloadCompleted += DownloadManager_DownloadCompleted;
            CertOverrideService.GetService().ValidityOverride += Certificate_ValidityOverride;


            var xulWindow = new GeckoXULWindow();
            xulWindow.WaitUntilChromeLoad();
            xulWindow.Width = 1280;
            xulWindow.Height = 800;

            //var xulDocument = xulWindow.View.Document;

            //Xpcom.DebugPrint(xulDocument);
            //GeckoElement window = xulDocument.DocumentElement;//.GetElementById("rootWindow");
            //window.SetAttribute("title", "Hello, World!");

            //GeckoElement browser = xulDocument.CreateElement("browser");
            //browser.SetAttribute("id", "tab1");
            //browser.SetAttribute("flex", "1");
            //browser.SetAttribute("type", "content-targetable");
            //browser.SetAttribute("src", "about:memory");
            //window.AppendChild(browser);


            //GeckoElement button = xulDocument.CreateElement("button");
            //button.SetAttribute("label", "Go to Google");
            //button.SetAttribute("oncommand", "alert(document.getElementById('tab1').contentWindow.location.href = 'http://localhost:12111/geckofx/index.php')");
            //window.AppendChild(button);


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
            string path = Path.Combine(Path.Combine("download"), filename);
            e.DownloadHelper.SaveToDisk(path, false);
            e.Handled = true;
        }

        private static void DownloadManager_DownloadCompleted(object sender, DownloadEventArgs e)
        {
            Console.WriteLine(string.Format("Download completed [{0}]", e.Downdload.Filename));
        }
    }
}
