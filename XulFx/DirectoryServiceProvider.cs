using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Gecko.Interfaces;
using Gecko.CustomMarshalers;
using System.Collections.Generic;
using System.Text;

namespace Gecko.Old
{
    public class IniFile1
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <param name="INIPath"></param>
        public IniFile1(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="Section"></param>
        /// Section name
        /// <param name="Key"></param>
        /// Key Name
        /// <param name="Value"></param>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();

        }
    }

    // Appends the distribution-specific search engine directories to the
    // array.  The directory structure is as follows:

    // appdir/
    // \- distribution/
    //    \- searchplugins/
    //       |- common/
    //       \- locale/
    //          |- <locale 1>/
    //          ...
    //          \- <locale N>/

    // common engines are loaded for all locales.  If there is no locale
    // directory for the current locale, there is a pref:
    // "distribution.searchplugins.defaultLocale"
    // which specifies a default locale to use.
    //internal class DirectoryProvider : nsIDirectoryServiceProvider, nsIDirectoryServiceProvider2
    //{
    //    const string NS_APP_DISTRIBUTION_SEARCH_DIR_LIST = "SrchPluginsDistDL";
    //    const string NS_DIRECTORY_SERVICE_CONTRACTID = "@mozilla.org/file/directory_service;1";

    //    const string XRE_APP_DISTRIBUTION_DIR = "XREAppDist";

    //    public IntPtr GetFile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string prop, [MarshalAs(UnmanagedType.U1)] out bool persistent)
    //    {
    //        persistent = false;
    //        return IntPtr.Zero;
    //    }

    //    [return: MarshalAs(UnmanagedType.Interface)]
    //    public nsISimpleEnumerator GetFiles([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string prop)
    //    {
    //        if (prop == NS_APP_DISTRIBUTION_SEARCH_DIR_LIST) return null;
    //        else
    //        {
    //            using(var dirSvc = Xpcom.GetService2<nsIProperties>(NS_DIRECTORY_SERVICE_CONTRACTID))
    //            {
    //                if (dirSvc == null) return null;
    //                else
    //                {
    //                    List<nsIFile> distroFiles;

    //                    Guid nsiFileGuid = new Guid("2fa6884a-ae65-412a-9d4c-ce6e34544ba1");
    //                    dirSvc.Instance.Get(XRE_APP_DISTRIBUTION_DIR, ref nsiFileGuid);

    //                }
    //            }
    //        }
    //    }
    //}
    /// <summary>
    /// firefox.exe -no-remote -CreateProfile "JoelUser"
    /// A simple nsIDirectoryServiceProvider which provides the profile directory, etc.
    /// This is still an incomplete implementation and can cause issues -- if this happens, add the missing item.
    /// For lists of items in DirectoryService, see
    /// https://developer.mozilla.org/en-US/Add-ons/Code_snippets/File_I_O#Getting_files_in_special_directories
    /// </summary>
    internal class DirectoryServiceProvider : nsIDirectoryServiceProvider
	{
        private string _profileName;

        public DirectoryServiceProvider()
		{
			//this.ProfilePath = null;
		}

		public string GeckoPath { get; internal set; }

		//public string ProfilePath
		//{
		//	get { return _profilePath; }
		//	set
		//	{
		//		if (value == null)
		//			_profilePath = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine("XulFx", "DefaultProfile")));
		//		else
		//			_profilePath = Path.GetFullPath(value);

		//		CreateDirectory(_profilePath);
  //              CreateDirectory(Path.Combine(ProfilePath, "Local"));
  //              CreateDirectory(Path.Combine(ProfilePath, "Roaming"));
  //          }
		//}

        public string ProfileName
        {
            get
            {
                return _profileName;
            }
            set
            {
                _profileName = value;
                CreateDirectory(ProfilePathRoaming);
                CreateDirectory(ProfilePathLocal);
                if (File.Exists(ProfilesFile))
                {
                    var filedata = new IniFile1(ProfilesFile);
                    int i = 0;
                    try
                    {
                        while (filedata.IniReadValue("Profile" + i + "", "Name") != "")
                        {
                            i++;
                        }
                    }
                    catch { }

                    var profnameValSection = "Profile" + i + "";
                    filedata.IniWriteValue(profnameValSection, "Name", ProfileName);
                    filedata.IniWriteValue(profnameValSection, "IsRelative", "1");
                    filedata.IniWriteValue(profnameValSection, "Path", "Profiles/"+ ProfileName);
                }
            }
        }

        public string ProfilePathRoaming
        {
            get
            {
                return  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "Profiles", ProfileName);
            }
        }
        public string ProfilePathLocal
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mozilla", "Firefox", "Profiles", ProfileName);
            }
        }
        /// <summary>
        /// [General]
        /// StartWithLastProfile=1
        /// 
        /// [Profile0]
        /// Name=default
        /// IsRelative=1
        /// Path=Profiles/3kw8mj5i.default
        /// Default=1
        /// </summary>
        public string ProfilesFile
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox", "profiles.ini");
            }
        }


        public nsIFile GetFileInterface(string prop, out bool persistent)
		{
			IntPtr nsFilePtr = GetFile(prop, out persistent);
			try
			{
				return (nsIFile)Marshal.GetTypedObjectForIUnknown(nsFilePtr, typeof(nsIFile));
			}
			finally
			{
				Marshal.Release(nsFilePtr);
			}
		}

        //public IntPtr GetFile(string prop, out bool persistent)
        //{
        //	persistent = false;
        //	switch(prop)
        //	{
        //		case "ProfD":
        //		case "ProfLD":
        //		case "ProfDS":
        //		case "ProfLDS":
        //		case "cachePDir":
        //		case "permissionDBPDir":
        //			return NewLocalFileAsNsPtr(this.ProfilePath);
        //		case "UChrm":
        //			return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "chrome"));
        //		case "TmpD":
        //			return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "tmp"));
        //		case "UMimTyp": // required to handle mailto protocol, etc.
        //			return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "mimeTypes.rdf"));
        //		case "LclSt":
        //			return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "localstore.rdf"));
        //		case "WinD":
        //			return NewLocalFileAsNsPtr(Path.GetDirectoryName(Environment.SystemDirectory));
        //		case "XCurProcD":
        //			return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser"));
        //	}
        //	Debug.WriteLine("Gecko.Xpcom.DirectoryServiceProvider.GetFile: not implemented: {0}", prop);
        //	return IntPtr.Zero;
        //}

        #region actual paths
        ////Services.dirsvc.get('cachePDir', Ci.nsIFile).path;
        ///*
        //Exception: [Exception... "Component returned failure code: 0x80004005 (NS_ERROR_FAILURE) [nsIProperties.get]"  nsresult: "0x80004005 (NS_ERROR_FAILURE)"  location: "JS frame :: Scratchpad/1 :: <TOP_LEVEL> :: line 39"  data: no]
        //*/
        ////Services.dirsvc.get('XRESysSExtPD', Ci.nsIFile).path;
        ///*
        //Exception: [Exception... "Component returned failure code: 0x80004005 (NS_ERROR_FAILURE) [nsIProperties.get]"  nsresult: "0x80004005 (NS_ERROR_FAILURE)"  location: "JS frame :: Scratchpad/1 :: <TOP_LEVEL> :: line 91"  data: no]
        //*/
        ////Services.dirsvc.get('XRESysLExtPD', Ci.nsIFile).path;
        ///*
        //Exception: [Exception... "Component returned failure code: 0x80004005 (NS_ERROR_FAILURE) [nsIProperties.get]"  nsresult: "0x80004005 (NS_ERROR_FAILURE)"  location: "JS frame :: Scratchpad/1 :: <TOP_LEVEL> :: line 95"  data: no]
        //*/
        ////Services.dirsvc.get('permissionDBPDir', Ci.nsIFile).path;
        ///*
        //Exception: [Exception... "Component returned failure code: 0x80004005 (NS_ERROR_FAILURE) [nsIProperties.get]"  nsresult: "0x80004005 (NS_ERROR_FAILURE)"  location: "JS frame :: Scratchpad/1 :: <TOP_LEVEL> :: line 103"  data: no]
        //*/
        ////Services.dirsvc.get('DictD', Ci.nsIFile).path;
        ///*
        //Exception: [Exception... "Component returned failure code: 0x80004005 (NS_ERROR_FAILURE) [nsIProperties.get]"  nsresult: "0x80004005 (NS_ERROR_FAILURE)"  location: "JS frame :: Scratchpad/1 :: <TOP_LEVEL> :: line 131"  data: no]
        //*/GreBinD
        #endregion
        public IntPtr GetFile(string prop, out bool persistent)
        {
            Console.WriteLine(prop);
            persistent = false;
            switch (prop)
            {                    
                //C:\Users\eli\AppData\Local\Mozilla\Firefox\Profiles\3kw8mj5i.default
                case "ProfLDS":
                case "ProfLD":
                case "cachePDir":
                case "permissionDBPDir":
                    return NewLocalFileAsNsPtr(Path.Combine(ProfilePathLocal));
                    
                //C:\Users\eli\AppData\Roaming\Mozilla\Firefox\Profiles\3kw8mj5i.default
                case "ProfDS":
                case "ProfD":
                    var file = NewLocalFileAsNsPtr(Path.Combine(ProfilePathRoaming));
                    return file;
                case "UMimTyp":
                    return NewLocalFileAsNsPtr(Path.Combine(ProfilePathRoaming, "mimeTypes.rdf"));

                //C:\Users\eli\AppData\Roaming\Mozilla\Firefox\Profiles\3kw8mj5i.default\searchplugins
                case "UsrSrchPlugns":
                    return NewLocalFileAsNsPtr(Path.Combine(ProfilePathRoaming));

                //C:\Users\eli\AppData\Roaming\Mozilla\Firefox\Profiles\3kw8mj5i.default\chrome
                case "UChrm":
                    return NewLocalFileAsNsPtr(Path.Combine(ProfilePathRoaming, "chrome"));

                //C:\Program Files (x86)\Mozilla Firefox\browser
                case "XREAddonAppDir":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));

                //C:\Program Files (x86)\Mozilla Firefox\distribution
                case "XREAppDist":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));

                //C:\Program Files (x86)\Mozilla Firefox\browser\features
                case "XREAppFeat":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser"));

                case "XREExeF":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));

                //C:\Program Files (x86)\Mozilla Firefox
                case "GreBinD":
                case "GreD":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));

                //C:\Program Files (x86)\Mozilla Firefox\browser
                case "XCurProcD":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser"));

                //C:\Program Files (x86)\Mozilla Firefox\defaults\pref
                case "PrfDef":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "defaults", "pref"));

                //C:\Program Files (x86)\Mozilla Firefox\browser\chrome
                case "AChrom":
                    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser", "chrome"));

                //C:\Users\eli\AppData\Local\Temp
                case "TmpD":
                    return NewLocalFileAsNsPtr(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp"));

                //C:\Users\eli
                case "Home":
                    return NewLocalFileAsNsPtr(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

                //C:\Users\eli\Desktop
                case "Desk":
                    return NewLocalFileAsNsPtr(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

                //C:\Users\eli\AppData\Roaming\Mozilla\Firefox
                case "UAppData":
                    return NewLocalFileAsNsPtr(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Firefox"));

                //C:\Users\eli\AppData\Roaming
                case "AppData":
                    return NewLocalFileAsNsPtr(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)));

                //C:\Users\eli\AppData\Roaming\Microsoft\Windows\Start Menu\Programs
                case "Progs":
                    return NewLocalFileAsNsPtr(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs)));

                //C:\WINDOWS
                case "WinD":
                    return NewLocalFileAsNsPtr(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows)));

                //C:\Users\eli\AppData\Roaming\Mozilla\Extensions
                case "XREUSysExt":
                    return NewLocalFileAsNsPtr(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "Extensions"));

                //C: \Users\eli\AppData\Local
                case "LocalAppData":
                    return NewLocalFileAsNsPtr(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)));

                //C: \Users\eli\AppData\Local\Mozilla\updates\
                case "UpdRootD":
                    return NewLocalFileAsNsPtr(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mozilla", "updates"));

                //case "XRESysSExtPD":
                //case "XRESysLExtPD":

                default:
                    break;
            }
             //switch (prop)
             //{
             //    //case "PrfDef":
             //    //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath));

            //    //case "AChrom":
            //    //case "ProfD":
            //    //case "permissionDBPDir":
            //    //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Roaming"));
            //    //case "UMimTyp": // required to handle mailto protocol, etc.
            //    //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Roaming", "mimeTypes.rdf"));

            //    //case "PrefD":
            //    //case "ProfLDS":
            //    //case "PrefDL":
            //    //case "ExtPrefDL":
            //    //case "PrefDOverride":
            //    //case "UsrSrchPlugns":
            //    //case "indexedDBPDir":
            //    //case "ProfLD":
            //    //case "cachePDir":
            //    //case "ProfDS":
            //    //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Local"));


            //    case "ProfD":
            //    case "permissionDBPDir":
            //    case "PrefD":
            //    case "ProfLDS":
            //    case "PrefDL":
            //    case "ExtPrefDL":
            //    case "PrefDOverride":
            //    case "UsrSrchPlugns":
            //    case "indexedDBPDir":
            //    case "ProfLD":
            //    case "cachePDir":
            //    case "ProfDS":
            //        return NewLocalFileAsNsPtr(this.ProfilePath);

            //    case "UChrm":
            //    	return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "chrome"));

            //    //case "DictD":
            //    //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "AppData\\Mozilla\\Cache\\Dict"));

            //    case "UMimTyp": // required to handle mailto protocol, etc.
            //        return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "mimeTypes.rdf"));

            //    case "XREAddonAppDir":
            //    case "XREAppFeat":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Addons"));

            //    case "UAppData":
            //    case "AppData":
            //    case "XRESysSExtPD":
            //    case "XREUSysExt":
            //    case "LocalAppData":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "AppData"));

            //    case "Home":
            //        return NewLocalFileAsNsPtr(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            //    // return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "Home"));

            //    case "Desk":
            //        return NewLocalFileAsNsPtr(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            //    case "Progs":
            //        return NewLocalFileAsNsPtr(Environment.GetFolderPath(Environment.SpecialFolder.Programs));

            //    case "XREExtDL":
            //    case "XRESysLExtPD":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "xre"));


            //    //case nsAppDirectoryServiceDefs.NS_APP_PREFS_50_FILE:
            //    //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "prefs.js"));

            //    //case nsAppDirectoryServiceDefs.NS_APP_USER_SEARCH_DIR:
            //    //    return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "plugins"));

            //    case "LStoreS":
            //    case "LclSt":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "localstore.rdf"));

            //    case "TmpD":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "tmp"));

            //    case "DictD":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "DictD"));

            //    case "DfltDwnld":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "DfltDwnld"));

            //    //case nsDirectoryServiceDefs.NS_WIN_WINDOWS_DIR:
            //    //    return NewLocalFileAsNsPtr(Path.GetDirectoryName(Environment.SystemDirectory));

            //    //case nsDirectoryServiceDefs.NS_XPCOM_CURRENT_PROCESS_DIR:
            //    ////    //return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser", "content", "preferences"));//content/preferences/
            //    //    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser"));//content/preferences/
            //    //   return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));//content/preferences/

            //    //case NS_XPCOM_INIT_CURRENT_PROCESS_DIR:
            //    //    return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath));

            //    //case nsDirectoryServiceDefs.NS_OS_CURRENT_PROCESS_DIR:

            //    case "XCurProcD":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser"));

            //    case "AChrom":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "chrome"));

            //    case "PrfDef":
            //        return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "defaults", "pref"));

            //    //case "GreD":
            //    //case "GreBinD":
            //    //    return NewLocalFileAsNsPtr(this.GeckoPath);



            //    case "XREExeF":
            //    case "XREAppDist":
            //    case "UpdRootD":
            //        //case "DictD":
            //        return NewLocalFileAsNsPtr(this.GeckoPath);

            //    case "WinD":
            //        return NewLocalFileAsNsPtr(Path.GetDirectoryName(Environment.SystemDirectory));

            //    case "UpdArchD":
            //    case "OSUpdApplyToD":
            //        return IntPtr.Zero;



            //    default:
            //        //if (BrowseoFXpcom.ServiceManager == null) break;
            //        //try
            //        //{
            //        //    nsIDirectoryServiceProvider dirsvc = BrowseoFXpcom.GetService<nsIDirectoryServiceProvider>("@mozilla.org/file/directory_service;1");
            //        //    return dirsvc.GetFile(prop, out persistent);
            //        //}
            //        //catch (System.Runtime.InteropServices.COMException e)
            //        //{
            //        //    Debug.WriteLine("Gecko.Xpcom.DirectoryServiceProvider.GetFile: not implemented: {0}", prop);
            //        //    return IntPtr.Zero;
            //        //}
            //        break;
            //}


            Debug.WriteLine("Gecko.Xpcom.DirectoryServiceProvider.GetFile: not implemented: {0}", prop);
            return IntPtr.Zero;
        }

        public static string RuntimePath
		{
			get
			{
				return typeof(Xpcom).Assembly.Location;
			}
		}

        

        public static IntPtr NewLocalFileAsNsPtr(string filename)
		{
			IntPtr result;
			using (var fileName = new nsAString(filename))
			{
				int error = NativeMethods.NS_NewLocalFile(fileName, true, out result);
				Marshal.ThrowExceptionForHR(error);
			}
			return result;
		}

		public static nsIFile NewLocalFile(string filename)
		{
			nsIFile result;
			using (var fileName = new nsAString(filename))
			{
				int error = NativeMethods.NS_NewLocalFile(fileName, true, out result);
				Marshal.ThrowExceptionForHR(error);
			}
			return result;
		}

		public static void CreateDirectory(string directory)
		{
			if (!Directory.Exists(directory))
			{
				CreateDirectory(Path.GetDirectoryName(directory));
				Directory.CreateDirectory(directory);
			}
		}
	}
}
