using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Gecko.Interfaces;

namespace Gecko
{
	/// <summary>
	/// A simple nsIDirectoryServiceProvider which provides the profile directory, etc.
	/// This is still an incomplete implementation and can cause issues -- if this happens, add the missing item.
	/// For lists of items in DirectoryService, see
	/// https://developer.mozilla.org/en-US/Add-ons/Code_snippets/File_I_O#Getting_files_in_special_directories
	/// </summary>
	internal class DirectoryServiceProvider : nsIDirectoryServiceProvider
	{
		private string _profilePath;

		public DirectoryServiceProvider()
		{
			this.ProfilePath = null;
		}

		public string GeckoPath { get; internal set; }

		public string ProfilePath
		{
			get { return _profilePath; }
			set
			{
				if (value == null)
					_profilePath = Path.GetFullPath(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine("XulFx", "DefaultProfile")));
				else
					_profilePath = Path.GetFullPath(value);
				CreateDirectory(_profilePath);
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

		public IntPtr GetFile(string prop, out bool persistent)
		{
			persistent = false;
			switch(prop)
			{
				case "ProfD":
				case "ProfLD":
				case "ProfDS":
				case "ProfLDS":
				case "cachePDir":
				case "permissionDBPDir":
					return NewLocalFileAsNsPtr(this.ProfilePath);
				case "UChrm":
					return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "chrome"));
				case "TmpD":
					return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "tmp"));
				case "UMimTyp": // required to handle mailto protocol, etc.
					return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "mimeTypes.rdf"));
				case "LclSt":
					return NewLocalFileAsNsPtr(Path.Combine(this.ProfilePath, "localstore.rdf"));
				case "WinD":
					return NewLocalFileAsNsPtr(Path.GetDirectoryName(Environment.SystemDirectory));
				case "XCurProcD":
					return NewLocalFileAsNsPtr(Path.Combine(this.GeckoPath, "browser"));
			}
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
