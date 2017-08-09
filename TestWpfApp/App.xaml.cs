using Gecko;
using Gecko.Services;
using System;
using System.IO;
using System.Windows;

namespace TestWpfApp
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			Xpcom.ProfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "profile");
			Xpcom.Initialize(XULRunnerLocator.GetXULRunnerLocation());
			//ManagedPromptFactory.Init();
			base.OnStartup(e);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			Xpcom.Shutdown();
		}
	}
}
