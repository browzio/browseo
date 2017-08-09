using System;
using Gecko.Interfaces;
using System.IO;
using System.Reflection;

namespace Gecko
{
	public class XulfxAppInfo : nsIXULAppInfo, nsIXULRuntime
	{
		private static nsIXULRuntime _instance;
		private string _buildID;
		private string _buildVersion;

		public XulfxAppInfo()
		{

		}

		internal void Init()
		{
			_instance = Xpcom.GetService<nsIXULRuntime>(Contracts.Runtime);
		}

		internal void Shutdown()
		{
			Xpcom.FreeComObject(ref _instance);
		}

		private nsIXULRuntime Instance
		{
			get
			{
				if (_instance == null)
					throw new InvalidOperationException();
				return _instance;
			}
		}

		protected virtual string BuildID
		{
			get
			{
				if (_buildID == null)
				{
					if (Xpcom.IsWindows)
					{
						try
						{

							_buildID = Utils.RetrieveLibraryLinkerTimestamp(DirectoryServiceProvider.RuntimePath).ToString("yyyyMMddHHmmss");
						}
						catch
						{
							_buildID = "20150508094354";
						}
					}
					else
					{
						_buildID = "20150508094354";
					}
				}
				return _buildID;
			}
		}

		protected virtual string BuildVersion
		{
			get
			{
				if (_buildVersion == null)
				{
					AssemblyName assemblyName = typeof(XulfxAppInfo).Assembly.GetName();
					if (assemblyName != null)
						_buildVersion = assemblyName.Version.ToString();
					else
						_buildVersion = "52";
				}
				return _buildVersion;
			}
		}

		#region nsIXULAppInfo

		public virtual void GetVendorAttribute(nsACStringBase aVendor)
		{
			aVendor.SetData("");
		}

		public virtual void GetNameAttribute(nsACStringBase aName)
		{
			aName.SetData("Xulfx");
		}

		public virtual void GetIDAttribute(nsACStringBase aID)
		{
			aID.SetData("{8547d29f-7ec3-40d9-ba80-3623382533fc}");
		}

		public virtual void GetVersionAttribute(nsACStringBase aVersion)
		{
			aVersion.SetData(this.BuildVersion);
		}

		public virtual void GetAppBuildIDAttribute(nsACStringBase aAppBuildID)
		{
			aAppBuildID.SetData(this.BuildID);
		}

		public virtual void GetPlatformVersionAttribute(nsACStringBase aPlatformVersion)
		{
			aPlatformVersion.SetData(Xpcom.RuntimeVersion ?? this.BuildVersion);
		}

		public virtual void GetPlatformBuildIDAttribute(nsACStringBase aPlatformBuildID)
		{
			aPlatformBuildID.SetData(Xpcom.RuntimeBuildID ?? this.BuildID);
		}

		public virtual void GetUANameAttribute(nsACStringBase aUAName)
		{
			aUAName.SetData("Xulfx");
		}

		#endregion nsIXULAppInfo

		#region nsIXULRuntime

		public virtual bool GetInSafeModeAttribute()
		{
			return Instance.GetInSafeModeAttribute();
		}

		public virtual bool GetLogConsoleErrorsAttribute()
		{
			return Instance.GetLogConsoleErrorsAttribute();
		}

		public virtual void SetLogConsoleErrorsAttribute(bool aLogConsoleErrors)
		{
			Instance.SetLogConsoleErrorsAttribute(aLogConsoleErrors);
		}

		public virtual void GetOSAttribute(nsAUTF8StringBase aOS)
		{
			Instance.GetOSAttribute(aOS);
		}

		public virtual void GetXPCOMABIAttribute(nsAUTF8StringBase aXPCOMABI)
		{
			Instance.GetXPCOMABIAttribute(aXPCOMABI);
		}

		public virtual void GetWidgetToolkitAttribute(nsAUTF8StringBase aWidgetToolkit)
		{
			Instance.GetWidgetToolkitAttribute(aWidgetToolkit);
		}

		public virtual uint GetProcessTypeAttribute()
		{
			return Instance.GetProcessTypeAttribute();
		}

		public virtual void InvalidateCachesOnRestart()
		{
			Instance.InvalidateCachesOnRestart();
		}

		public virtual void EnsureContentProcess()
		{
			Instance.EnsureContentProcess();
		}

		public virtual void GetLastRunCrashIDAttribute(nsAStringBase aLastRunCrashID)
		{
			Instance.GetLastRunCrashIDAttribute(aLastRunCrashID);
		}

		public virtual uint GetProcessIDAttribute()
		{
			return Instance.GetProcessIDAttribute();
		}

		public virtual bool GetBrowserTabsRemoteAutostartAttribute()
		{
			return Instance.GetBrowserTabsRemoteAutostartAttribute();
		}

		public virtual bool GetAccessibilityEnabledAttribute()
		{
			return Instance.GetAccessibilityEnabledAttribute();
		}

		public virtual ulong GetReplacedLockTimeAttribute()
		{
			return Instance.GetReplacedLockTimeAttribute();
		}

		public virtual bool GetIsReleaseOrBetaAttribute()
		{
			return Instance.GetIsReleaseOrBetaAttribute();
		}

		public virtual bool GetIsOfficialBrandingAttribute()
		{
			return Instance.GetIsOfficialBrandingAttribute();
		}

		public virtual void GetDefaultUpdateChannelAttribute(nsAUTF8StringBase result)
		{
			Instance.GetDefaultUpdateChannelAttribute(result);
		}

		public virtual void GetDistributionIDAttribute(nsAUTF8StringBase result)
		{
			Instance.GetDistributionIDAttribute(result);
		}

		public virtual bool GetIsOfficialAttribute()
		{
			return Instance.GetIsOfficialAttribute();
		}

		public bool GetIs64BitAttribute()
		{
			return IntPtr.Size == 8;
		}

		public virtual ulong GetUniqueProcessIDAttribute()
		{
			return Instance.GetUniqueProcessIDAttribute();
		}

		public virtual uint GetMultiprocessBlockPolicyAttribute()
		{
			return Instance.GetMultiprocessBlockPolicyAttribute();
		}

		public virtual bool GetWindowsDLLBlocklistStatusAttribute()
		{
			return Instance.GetWindowsDLLBlocklistStatusAttribute();
		}

		#endregion

	}

}
