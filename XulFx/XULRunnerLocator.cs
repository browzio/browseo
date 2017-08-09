using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;


namespace Gecko
{
	/// <summary>
	/// Search /usr/lib directory for the latest xulrunner.
	/// Currently looks for xulrunner-24 variants in /usr/lib.
	/// Returns null if no xulrunner-24 directory is found.
	/// xulrunner doesn't come with a .pc file so I can't use pkg-config to find location of xulrunner.
	/// </summary>
	public static class XULRunnerLocator
	{
		private static string FindSpecialXulRunnerDirectory(string applicationPath)
		{
			string rootPath = Path.GetPathRoot(applicationPath);
			string path = applicationPath;

			while (!rootPath.Equals(path, StringComparison.CurrentCultureIgnoreCase))
			{
				if(Directory.GetDirectories(path).FirstOrDefault(x => Path.GetFileName(x) == "PutXulRunnerFolderHere") != null)
				{
					// in solition directory
					if (path != null)
					{
						string xulrunnerPath = Path.GetFullPath(Path.Combine(Path.Combine(Path.Combine(path, "PutXulRunnerFolderHere"), "xulrunner-sdk"), "bin"));
						if (Directory.Exists(xulrunnerPath))
							return xulrunnerPath;

						xulrunnerPath = Path.GetFullPath(Path.Combine(Path.Combine(Path.Combine(path, "PutXulRunnerFolderHere"), "firefox-sdk"), "bin"));
						if (Directory.Exists(xulrunnerPath))
							return xulrunnerPath;

						xulrunnerPath = Path.GetFullPath(Path.Combine(Path.Combine(path, "PutXulRunnerFolderHere"), "xulrunner"));
						return Directory.Exists(xulrunnerPath) ? xulrunnerPath : null;
					}
				}
				path = Path.GetDirectoryName(path);				
			}
			return null;
		}

		private static string GetXULRunnerLocationLinux()
		{
			string solutionXulRunnerFolder = FindSpecialXulRunnerDirectory(DirectoryOfTheApplicationExecutable);
			if (solutionXulRunnerFolder != null)
				return solutionXulRunnerFolder;

			string version = typeof(XULRunnerLocator).Assembly.GetName().Version.Major.ToString();
			string xulrunnerPath = "/usr/lib/xulrunner-" + version;
			if (Directory.Exists(xulrunnerPath))
				return xulrunnerPath;
			for (int i = 0; i <= 12; i++)
			{
				if (Directory.Exists(xulrunnerPath + "." + i.ToString()))
					return xulrunnerPath + "." + i.ToString();
			}
			return null;
		}

		private static string GetXULRunnerLocationWindows()
		{
			//NB for shipping apps, we don't have a way to find their xulrunner, so they won't be running this code 
			//unless they depend on the customer installing a certain verion of Firefox and keeping it from auto-updating.
			//So this is more for unit tests and geckofx sample apps.

			string solutionXulRunnerFolder = FindSpecialXulRunnerDirectory(DirectoryOfTheApplicationExecutable);
			if (solutionXulRunnerFolder != null) 
				return solutionXulRunnerFolder;

			//look for firefox itself

			string version = typeof(XULRunnerLocator).Assembly.GetName().Version.Major.ToString();

			string[] folderSearch = new string[] { "Mozilla Firefox " + version + ".0", "Mozilla Firefox " + version, "Mozilla Firefox" };

			var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			return folderSearch.Select(t => Path.Combine(programFiles, t)).FirstOrDefault(Directory.Exists);
		}

		private static string DirectoryOfTheApplicationExecutable
		{
			get
			{
				string path;
				bool unitTesting = Assembly.GetEntryAssembly() == null;
				if (unitTesting)
				{
					path = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;
					path = Uri.UnescapeDataString(path);
				}
				else
				{
					path = AppDomain.CurrentDomain.BaseDirectory;					
				}
				return Directory.GetParent(path).FullName;
			}
		}

		public static string GetXULRunnerLocation()
		{
			return Xpcom.IsLinux
				? GetXULRunnerLocationLinux()
				: GetXULRunnerLocationWindows();
		}
	}
}
