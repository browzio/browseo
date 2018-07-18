using System;
using Gecko.Interfaces;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Gecko
{
	//public class XulfxAppInfo : nsIXULAppInfo, nsIXULRuntime
	//{
	//	private static nsIXULRuntime _instance;
	//	private string _buildID;
	//	private string _buildVersion;

	//	public XulfxAppInfo()
	//	{

	//	}

	//	internal void Init()
	//	{
	//		_instance = Xpcom.GetService<nsIXULRuntime>(Contracts.Runtime);
	//	}

	//	internal void Shutdown()
	//	{
	//		Xpcom.FreeComObject(ref _instance);
	//	}

	//	private nsIXULRuntime Instance
	//	{
	//		get
	//		{
	//			if (_instance == null)
	//				throw new InvalidOperationException();
	//			return _instance;
	//		}
	//	}

	//	protected virtual string BuildID
	//	{
	//		get
	//		{
 //               return "20170302120751";

 //   //            if (_buildID == null)
	//			//{
	//			//	if (Xpcom.IsWindows)
	//			//	{
	//			//		try
	//			//		{

	//			//			_buildID = Utils.RetrieveLibraryLinkerTimestamp(DirectoryServiceProvider.RuntimePath).ToString("yyyyMMddHHmmss");
	//			//		}
	//			//		catch
	//			//		{
	//			//			_buildID = "20150508094354";
	//			//		}
	//			//	}
	//			//	else
	//			//	{
	//			//		_buildID = "20150508094354";
	//			//	}
	//			//}
	//			//return _buildID;
	//		}
	//	}

	//	protected virtual string BuildVersion
	//	{
	//		get
	//		{
	//			if (_buildVersion == null)
	//			{
	//				AssemblyName assemblyName = typeof(XulfxAppInfo).Assembly.GetName();
	//				if (assemblyName != null)
	//					_buildVersion = assemblyName.Version.ToString();
	//				else
	//					_buildVersion = "52";
	//			}
	//			return _buildVersion;
	//		}
	//	}

	//	#region nsIXULAppInfo

	//	public virtual void GetVendorAttribute(nsACStringBase aVendor)
	//	{
	//		aVendor.SetData("Mozilla");
	//	}

	//	public virtual void GetNameAttribute(nsACStringBase aName)
	//	{
	//		aName.SetData("Firefox");
	//	}

	//	public virtual void GetIDAttribute(nsACStringBase aID)
	//	{
	//		//aID.SetData("{8547d29f-7ec3-40d9-ba80-3623382533fc}");
	//	}

	//	public virtual void GetVersionAttribute(nsACStringBase aVersion)
	//	{
	//		//aVersion.SetData(this.BuildVersion);
	//	}

	//	public virtual void GetAppBuildIDAttribute(nsACStringBase aAppBuildID)
	//	{
	//		aAppBuildID.SetData(this.BuildID);
	//	}

	//	public virtual void GetPlatformVersionAttribute(nsACStringBase aPlatformVersion)
	//	{
	//		aPlatformVersion.SetData(Xpcom.RuntimeVersion ?? this.BuildVersion);
	//	}

	//	public virtual void GetPlatformBuildIDAttribute(nsACStringBase aPlatformBuildID)
	//	{
	//		//aPlatformBuildID.SetData(Xpcom.RuntimeBuildID ?? this.BuildID);
	//	}

	//	public virtual void GetUANameAttribute(nsACStringBase aUAName)
	//	{
	//		aUAName.SetData("Firefox");
	//	}

	//	#endregion nsIXULAppInfo

	//	#region nsIXULRuntime

	//	public virtual bool GetInSafeModeAttribute()
	//	{
	//		return Instance.GetInSafeModeAttribute();
	//	}

	//	public virtual bool GetLogConsoleErrorsAttribute()
	//	{
	//		return Instance.GetLogConsoleErrorsAttribute();
	//	}

	//	public virtual void SetLogConsoleErrorsAttribute(bool aLogConsoleErrors)
	//	{
	//		Instance.SetLogConsoleErrorsAttribute(aLogConsoleErrors);
	//	}

	//	public virtual void GetOSAttribute(nsAUTF8StringBase aOS)
	//	{
	//		Instance.GetOSAttribute(aOS);
	//	}

	//	public virtual void GetXPCOMABIAttribute(nsAUTF8StringBase aXPCOMABI)
	//	{
	//		Instance.GetXPCOMABIAttribute(aXPCOMABI);
	//	}

	//	public virtual void GetWidgetToolkitAttribute(nsAUTF8StringBase aWidgetToolkit)
	//	{
	//		Instance.GetWidgetToolkitAttribute(aWidgetToolkit);
	//	}

	//	public virtual uint GetProcessTypeAttribute()
	//	{
	//		return Instance.GetProcessTypeAttribute();
	//	}

	//	public virtual void InvalidateCachesOnRestart()
	//	{
	//		Instance.InvalidateCachesOnRestart();
	//	}

	//	public virtual void EnsureContentProcess()
	//	{
	//		Instance.EnsureContentProcess();
	//	}

	//	public virtual void GetLastRunCrashIDAttribute(nsAStringBase aLastRunCrashID)
	//	{
	//		Instance.GetLastRunCrashIDAttribute(aLastRunCrashID);
	//	}

	//	public virtual uint GetProcessIDAttribute()
	//	{
	//		return Instance.GetProcessIDAttribute();
	//	}

	//	public virtual bool GetBrowserTabsRemoteAutostartAttribute()
	//	{
	//		return Instance.GetBrowserTabsRemoteAutostartAttribute();
	//	}

	//	public virtual bool GetAccessibilityEnabledAttribute()
	//	{
	//		return Instance.GetAccessibilityEnabledAttribute();
	//	}

	//	public virtual ulong GetReplacedLockTimeAttribute()
	//	{
	//		return Instance.GetReplacedLockTimeAttribute();
	//	}

	//	public virtual bool GetIsReleaseOrBetaAttribute()
	//	{
	//		return Instance.GetIsReleaseOrBetaAttribute();
	//	}

	//	public virtual bool GetIsOfficialBrandingAttribute()
	//	{
	//		return Instance.GetIsOfficialBrandingAttribute();
	//	}

	//	public virtual void GetDefaultUpdateChannelAttribute(nsAUTF8StringBase result)
	//	{
	//		Instance.GetDefaultUpdateChannelAttribute(result);
	//	}

	//	public virtual void GetDistributionIDAttribute(nsAUTF8StringBase result)
	//	{
	//		Instance.GetDistributionIDAttribute(result);
	//	}

	//	public virtual bool GetIsOfficialAttribute()
	//	{
	//		return Instance.GetIsOfficialAttribute();
	//	}

	//	public bool GetIs64BitAttribute()
	//	{
	//		return IntPtr.Size == 8;
	//	}

	//	public virtual ulong GetUniqueProcessIDAttribute()
	//	{
	//		return Instance.GetUniqueProcessIDAttribute();
	//	}

	//	public virtual uint GetMultiprocessBlockPolicyAttribute()
	//	{
	//		return Instance.GetMultiprocessBlockPolicyAttribute();
	//	}

	//	public virtual bool GetWindowsDLLBlocklistStatusAttribute()
	//	{
	//		return Instance.GetWindowsDLLBlocklistStatusAttribute();
	//	}

	//	#endregion

	//}


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
        static ulong gBrowserTabsRemoteStatus = 0;

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
            result.SetData("");
        }
        #endregion




        #region nsIObserver
        public void Observe([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.WStringMarshaler))] string aData)
        {
            if (aTopic != "getE10SBlocked") return;
            Gecko.Interfaces.nsISupportsPRUint64 ret = Xpcom.QueryInterface<Gecko.Interfaces.nsISupportsPRUint64>(aSubject);
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
                    _instance = Xpcom.GetService<Gecko.Interfaces.nsIXULRuntime>(Gecko.Contracts.Runtime);
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
            aXPCOMABI.SetData("x86-msvc");
            // RuntimeInstance.GetXPCOMABIAttribute(aXPCOMABI);
        }

        public virtual void GetWidgetToolkitAttribute(Gecko.nsAUTF8StringBase aWidgetToolkit)
        {
            aWidgetToolkit.SetData("windows");
            // RuntimeInstance.GetWidgetToolkitAttribute(aWidgetToolkit);
        }

        public bool GetIs64BitAttribute()
        {
            return false;
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

        public virtual uint GetProcessIDAttribute()
        {
            return (uint)System.Diagnostics.Process.GetCurrentProcess().Id;
        }

        public virtual bool GetBrowserTabsRemoteAutostartAttribute()
        {
            // return RuntimeInstance.GetBrowserTabsRemoteAutostartAttribute();
            return true;
        }
        public virtual bool GetAccessibilityEnabledAttribute()
        {
            // return RuntimeInstance.GetAccessibilityEnabledAttribute();
            return false;
        }

        public virtual ulong GetReplacedLockTimeAttribute()
        {
            return 0;
            //try
            //{
            //    return RuntimeInstance.GetReplacedLockTimeAttribute();
            //}
            //catch
            //{
            //    return 0x80040111;
            //}
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
            result.SetData("release");
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

        public virtual ulong GetUniqueProcessIDAttribute()
        {
            //return RuntimeInstance.GetUniqueProcessIDAttribute();
            //var gu = Guid.NewGuid();
            //gu.GetHashCode();
            //return (ulong)gu.GetHashCode();
            return 0;
        }

        public virtual uint GetMultiprocessBlockPolicyAttribute()
        {
            return 0;
        }

        public virtual bool GetWindowsDLLBlocklistStatusAttribute()
        {
            return false;
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
            aLastRunCrashID.SetData("");
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
                    _nsICrashReporterinstance = Xpcom.GetService<Gecko.Interfaces.nsICrashReporter>(Gecko.Contracts.CrashReporter);
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
            return null;
        }

        public void SetMinidumpPathAttribute([MarshalAs(UnmanagedType.Interface)] Gecko.Interfaces.nsIFile value)
        {
            //CrashReporterInstance.SetMinidumpPathAttribute(value);
        }

        public void AnnotateCrashReport([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AUTF8StringMarshaler))] Gecko.nsAUTF8StringBase key, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AUTF8StringMarshaler))] Gecko.nsAUTF8StringBase data)
        {
           //  CrashReporterInstance.AnnotateCrashReport(key, data);
        }

        public void AppendAppNotesToCrashReport([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] Gecko.nsACStringBase data)
        {
          //  CrashReporterInstance.AppendAppNotesToCrashReport(data);
        }

        public void RegisterAppMemory(ulong ptr, ulong size)
        {
           // CrashReporterInstance.RegisterAppMemory(ptr, size);
        }

        public void WriteMinidumpForException(IntPtr aExceptionInfo)
        {
          //  CrashReporterInstance.WriteMinidumpForException(aExceptionInfo);
        }

        public void AppendObjCExceptionInfoToAppNotes(IntPtr aException)
        {
           // CrashReporterInstance.AppendObjCExceptionInfoToAppNotes(aException);
        }

        [return: MarshalAs(UnmanagedType.U1)]
        public bool GetSubmitReportsAttribute()
        {
            return false;
            //return CrashReporterInstance.GetSubmitReportsAttribute();
        }

        public void SetSubmitReportsAttribute([MarshalAs(UnmanagedType.U1)] bool value)
        {
            CrashReporterInstance.SetSubmitReportsAttribute(value);
        }//

        public void UpdateCrashEventsDir()
        {
           // CrashReporterInstance.UpdateCrashEventsDir();
        }

        public void SaveMemoryReport()
        {
           // CrashReporterInstance.SaveMemoryReport();
        }

        public void SetTelemetrySessionId([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AUTF8StringMarshaler))] Gecko.nsAUTF8StringBase id)
        {
            //CrashReporterInstance.SetTelemetrySessionId(id);
        }
        #endregion


        internal void Shutdown()
        {
            Xpcom.FreeComObject(ref _instance);
        }
    }

}
