using BrowseoFX.Base.Wpf.Browser;
using BrowseoFX_WPF.Core.DataAccess;
using BrowseoFX_WPF.Core.Services;
using BrowseoFX_WPF.Core.Services.Browser;
using Gecko;
using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestBrowseoFXWPFApp
{
    public class bOOTSTRAPtEST
    {
        public static WebViewHwnd CreateFFControl()
        {

            #region init_xpcoms
            var binDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin";
            Xpcom.SDKDirectory = binDirectory;
            Xpcom.BFXComponentsDirectory = Path.Combine(binDirectory, "browser", "BFXComponents");



            BrowseoFXServicesManager.BrowserProfilesManagerService.SelectOrAddProfile("WPF_TEST_2");
            if (BrowseoFXServicesManager.BrowserProfilesManagerService.SelectedProfile == null)
            {
                //TODO:NICE MESSAGEBOX
                MessageBox.Show("No profile for the project was able to be created.");
            }
            else
            {
                Environment.SetEnvironmentVariable("MOZ_SANDBOX", "");
                Environment.SetEnvironmentVariable("MOZ_DISABLE_CONTENT_SANDBOX", "1");
                Environment.SetEnvironmentVariable("MOZ_DISABLE_GMP_SANDBOX", "1");
                Environment.SetEnvironmentVariable("MOZ_DISABLE_NPAPI_SANDBOX", "1");
                Environment.SetEnvironmentVariable("MOZ_DISABLE_GPU_SANDBOX", "1");

                Xpcom.InitializeXPCOM(Xpcom.SDKDirectory, BrowseoFXServicesManager.DirectoryServiceProvider);

                using (var directoryService = Xpcom.GetService2<nsIDirectoryService>(Gecko.Contracts.DirectoryService))
                {
                    if (directoryService != null)
                        directoryService.Instance.RegisterProvider(BrowseoFXServicesManager.DirectoryServiceProvider);
                }

                Xpcom.RegisterInstance(typeof(nsIXULRuntime).GUID, "nsXULAppInfo", Gecko.Contracts.AppInfo, nsXULAppInfo.Instance);
                Xpcom.LoadExtension(Xpcom.BFXComponentsDirectory, "components.manifest");

                Xpcom.InitializeThreadManager();
                Xpcom.DoEvents();

                #region prefs
                using (var PreferencesService = Xpcom.GetService2<Gecko.Interfaces.nsIPrefService>(Gecko.Contracts.PreferencesService))
                {
                //    var browserPrefBranch = PreferencesService.Instance.GetBranch("browser.");
                    var userPrefsFile = Path.Combine(BrowseoFXServicesManager.DirectoryServiceProvider.ProfilePathRoaming, "prefs.js");

                    if (File.Exists(userPrefsFile))
                        PreferencesService.Instance.ReadUserPrefs(BrowseoFXServicesManager.DirectoryServiceProvider.NewLocalFile(userPrefsFile));
                }

                GeckoPreferences pref = GeckoPreferences.Default;
                pref["browser.startup.page"] = 3;
                pref["browser.tabs.closeWindowWithLastTab"] = false;
                pref["toolkit.telemetry.enabled"] = false;
                pref["toolkit.defaultChromeURI"] = "";
                pref["toolkit.defaultChromeFeatures"] = "";
                pref["toolkit.singletonWindowType"] = "";
                //pref["toolkit.defaultChromeURI"] = "chrome://browser/content/browser.xul";
                //pref["toolkit.defaultChromeFeatures"] = "chrome,dialog=no,all";
                //pref["toolkit.singletonWindowType"] = "navigator:browser";

                pref["browser.tabs.remote.autostart"] = false;
                pref["extensions.e10sBlockedByAddons"] = true;
                pref["extensions.e10sBlocksEnabling"] = false;

                //https://stackoverflow.com/questions/3729477/how-to-automatically-import-bookmarks-in-firefox
                pref["browser.places.importBookmarksHTML"] = true;
                pref["browser.bookmarks.restore_default_bookmarks"] = false;
                pref["browser.shell.checkDefaultBrowser"] = false;
                //pref["browser.bookmarks.autoExportHTML"] = true;
                //pref["browser.bookmarks.file"] = "Bookmarks.html";

                //https://developer.mozilla.org/en-US/Firefox/Enterprise_deployment
                pref["app.update.enabled"] = false;
                pref["app.update.auto"] = false;
                pref["app.update.mode"] = 0;
                pref["app.update.service.enabled"] = false;
                pref["browser.rights.3.shown"] = true;
                pref["browser.startup.homepage_override.mstone"] = "ignore";
                pref["pdfjs.disabled"] = true;
                pref["shumway.disabled"] = true;
                pref["plugins.notifyMissingFlash"] = false;
                pref["plugins.hide_infobar_for_outdated_plugin"] = true;
                pref["datareporting.healthreport.service.enabled"] = false;
                pref["datareporting.policy.dataSubmissionEnabled"] = false;
                pref["toolkit.crashreporter.enabled"] = false;

                GeckoPreferences userPref = GeckoPreferences.User;
                userPref["datareporting.healthreport.uploadEnabled"] = false;
                userPref["datareporting.policy.currentPolicyVersion"] = 0;
                userPref["datareporting.policy.dataSubmissionEnabled"] = false;
                userPref["datareporting.policy.dataSubmissionPolicyBypassNotification"] = false;
                userPref["datareporting.policy.dataSubmissionPolicyAcceptedVersion"] = 0;
                //userPref["datareporting.policy.dataSubmissionPolicyNotifiedTime"] = 0;
                userPref["datareporting.policy.firstRunURL"] = "";
                userPref["datareporting.policy.minimumPolicyVersion"] = 0;
                userPref["datareporting.sessions.current.firstPaint"] = 0;
                userPref["datareporting.sessions.current.sessionRestored"] = 0;

                ///sandbox bullshit
                userPref["security.sandbox.content.level"] = 0;
                userPref["security.sandbox.gpu.level"] = 0;
                string tempdir = userPref["security.sandbox.content.tempDirSuffix"] as string;
                if (tempdir == null || tempdir == "")
                {
                    userPref["security.sandbox.content.tempDirSuffix"] = Guid.NewGuid().ToString();
                }

                //userPref["full-screen-api.enabled"] = false;


                #endregion

                using (var TransportService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.TransportService)) { }
                using (var DnsService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.DnsService)) { }
                using (var NetworkIOService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.NetworkIOService)) { }
                using (var ChromeRegistry = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.ChromeRegistry)) { }
                //using (var SuppressorService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.SuppressorService)) { }

                using (var windowWatcher = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
                {
                    using (var creator = Xpcom.GetService2<Gecko.Interfaces.nsIWindowCreator>(Gecko.Contracts.AppStartup))
                    {
                        windowWatcher.Instance.SetWindowCreator(creator.Instance);
                    }
                    //windowWatcher.Instance.SetWindowCreator(WindowCreator.Instance);
                }


                //Xpcom.RegisterInstance(typeof(nsISessionStore).GUID, "SessionStore", "@mozilla.org/browser/sessionstore;1", BrowseoFXServicesManager.SessionStoreService);

                Xpcom.RegisterInstance(typeof(nsIBrowserHandler).GUID, "nsBrowserContentHandler", "@mozilla.org/browser/clh;1", nsBrowserHandler.Instance);
                Xpcom.RegisterInstance(typeof(nsIBrowserGlue).GUID, "nsBrowserGlue", "@mozilla.org/browser/browserglue;1", nsBrowserGlue.Instance);

                using (var startupNotifier = Xpcom.GetService2<Gecko.Interfaces.nsIObserver>(Gecko.Contracts.AppStartupNotifier))
                {
                    startupNotifier.Instance.Observe(null, "app-startup", null);
                }

                using (var appStartupSrv = Xpcom.GetService2<Gecko.Interfaces.nsIAppStartup>(Gecko.Contracts.AppStartup))
                {
                    appStartupSrv.Instance.CreateHiddenWindow();

                    using (var obsSvc = Xpcom.GetService2<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService))
                    {
                        //obsSvc.Instance.AddObserver(new nsObserverAny(), "browser-delayed-startup-finished", false);

                        obsSvc.Instance.NotifyObservers(null, "app-startup", null);
                        obsSvc.Instance.NotifyObservers(null, "profile-do-change", "startup");

                        var xulfxPath = System.IO.Path.Combine(@"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\bin\Debug", "XulFx.xpi");
                        Xpcom.XulfxPath = xulfxPath;
                        Xpcom.LoadExtension(Xpcom.XulfxPath, "");


                        Xpcom.DoEvents();
                        //addons
                        using (var em = Xpcom.GetService2<Gecko.Interfaces.nsIObserver>(Gecko.Contracts.AddonsIntegration))
                        {
                            em.Instance.Observe(null, "addons-startup", null);
                        }

                        using (var SessionStartup = Xpcom.GetService2<Gecko.Interfaces.nsISessionStartup>(Gecko.Contracts.SessionStartup)) { }

                        obsSvc.Instance.NotifyObservers(null, "load-extension-defaults", null);
                        obsSvc.Instance.NotifyObservers(null, "profile-after-change", "startup");
                        obsSvc.Instance.NotifyObservers(null, "profile-initial-state", null);


                        //cmdline app
                        //cmdline nsDefaultCLH
                        using (var cmdLine = Xpcom.GetService2<Gecko.Interfaces.nsICommandLineRunner>(Gecko.Contracts.CommandLine))
                        {
                            cmdLine.Instance.Run();
                        }


                        // clear out any environment variables which may have been set
                        // during the relaunch process now that we know we won't be relaunching.
                        Environment.SetEnvironmentVariable("XRE_PROFILE_PATH", "");
                        Environment.SetEnvironmentVariable("XRE_PROFILE_LOCAL_PATH", "");
                        Environment.SetEnvironmentVariable("XRE_PROFILE_NAME", "");
                        Environment.SetEnvironmentVariable("XRE_START_OFFLINE", "");
                        Environment.SetEnvironmentVariable("NO_EM_RESTART", "");
                        Environment.SetEnvironmentVariable("XUL_APP_FILE", "");
                        Environment.SetEnvironmentVariable("XRE_BINARY_PATH", "");



                        obsSvc.Instance.NotifyObservers(null, "final-ui-startup", null);
                    }

                    appStartupSrv.Instance.DoneStartingUp();

                }
            }

            Xpcom.DoEvents();
            #endregion

            return new WebViewHwnd();
        }
    }
}
