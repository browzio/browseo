using BrowseoFX_WPF.Core.Services;
using BrowseoFX_WPF.Core.Services.Browseo;
using Gecko;
using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Gecko.Windows;
using System.IO;
using Gecko.GUI;
using Gecko.Interop;
using Gecko.DOM;
using Gecko.CustomMarshalers;
using SpiderMonkey;
using BrowseoFX_WPF.Core.Services.BFXpcom;

namespace TestBrowseoFXApp
{
    class Program 
    {
        [STAThread]
        static void Main(string[] args)
        {
            string ffBrowserProfileName = "BrowseoFXAppDemo";

            var binDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin";
            //var binDirectory = @"C:\Program Files (x86)\Mozilla Firefox";
            Xpcom.SDKDirectory = binDirectory;

            //Xpcom.Initialize(binDirectory);

            //BrowserProfilesManagerService.Instance.ReadProfilesIni();
            BrowseoFXServicesManager.BrowserProfilesManagerService.SelectOrAddProfile(ffBrowserProfileName);
            if (BrowseoFXServicesManager.BrowserProfilesManagerService.SelectedProfile == null)
            {
                Console.WriteLine("no profile");
            }
            else
            {
                Environment.SetEnvironmentVariable("MOZ_SANDBOX", "");
                Environment.SetEnvironmentVariable("MOZ_DISABLE_CONTENT_SANDBOX", "1");
                Environment.SetEnvironmentVariable("MOZ_DISABLE_GMP_SANDBOX", "1");
                Environment.SetEnvironmentVariable("MOZ_DISABLE_NPAPI_SANDBOX", "1");
                Environment.SetEnvironmentVariable("MOZ_DISABLE_GPU_SANDBOX", "1");

                Xpcom.InitializeXPCOM(Xpcom.SDKDirectory, BrowseoFXServicesManager.DirectoryServiceProvider);

                Xpcom.RegisterInstance(typeof(nsIXULRuntime).GUID, "nsXULAppInfo", Gecko.Contracts.AppInfo, nsXULAppInfo.Instance);

                using (var directoryService = Xpcom.GetService2<nsIDirectoryService>(Gecko.Contracts.DirectoryService))
                {
                    if (directoryService != null)
                        directoryService.Instance.RegisterProvider(BrowseoFXServicesManager.DirectoryServiceProvider);
                }

                Xpcom.InitializeThreadManager();
                Xpcom.DoEvents();

                #region prefs
                using (var PreferencesService = Xpcom.GetService2<Gecko.Interfaces.nsIPrefService>(Gecko.Contracts.PreferencesService))
                {
                    //var userPrefsFile = Path.Combine(BrowseoFXServicesManager.DirectoryServiceProvider.ProfilePathRoaming, "prefs.js");

                    //if (File.Exists(userPrefsFile))
                    //    PreferencesService.Instance.ReadUserPrefs(BrowseoFXServicesManager.DirectoryServiceProvider.NewLocalFile(userPrefsFile));
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
                pref["browser.bookmarks.autoExportHTML"] = true;
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

                //var clh = new nsBrowserContentHandler();
                Xpcom.RegisterInstance(typeof(nsIBrowserHandler).GUID, "nsBrowserContentHandler", "@mozilla.org/browser/clh;1", nsBrowserHandler.Instance);
                var glue = new nsBrowserGlue();
                glue.RegisterObservers();
                Xpcom.RegisterInstance(typeof(nsIBrowserGlue).GUID, "nsBrowserGlue", "@mozilla.org/browser/browserglue;1", glue);

                Xpcom.LoadExtension(Path.Combine(binDirectory, "browser", "BFXComponents"), "components.manifest");

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



                //var xulWindow = new GeckoXULWindow();
                //xulWindow.WaitUntilChromeLoad();
                //xulWindow.Width = 1280;
                //xulWindow.Height = 800;
                //xulWindow.ShowDialog();
            }

            Console.WriteLine("shutdown");
            Console.ReadKey();
            Xpcom.Shutdown();
        }

        //test 1
        //static void Main(string[] args)
        //{
        //    string ffBrowserProfileName = "BrowseoFXAppDemo";

        //    var binDirectory = @"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\PutXulRunnerFolderHere\firefox-sdk\bin";
        //    //var binDirectory = @"C:\Program Files (x86)\Mozilla Firefox";
        //    Xpcom.SDKDirectory = binDirectory;

        //    //Xpcom.Initialize(binDirectory);

        //    //BrowserProfilesManagerService.Instance.ReadProfilesIni();
        //    BrowseoFXServicesManager.BrowserProfilesManagerService.SelectOrAddProfile(ffBrowserProfileName);
        //    if (BrowseoFXServicesManager.BrowserProfilesManagerService.SelectedProfile == null)
        //    {
        //        Console.WriteLine("no profile");
        //    }
        //    else
        //    {
        //        Environment.SetEnvironmentVariable("MOZ_SANDBOX", "");
        //        Environment.SetEnvironmentVariable("MOZ_DISABLE_CONTENT_SANDBOX", "1");
        //        Environment.SetEnvironmentVariable("MOZ_DISABLE_GMP_SANDBOX", "1");
        //        Environment.SetEnvironmentVariable("MOZ_DISABLE_NPAPI_SANDBOX", "1");
        //        Environment.SetEnvironmentVariable("MOZ_DISABLE_GPU_SANDBOX", "1");

        //        Xpcom.InitializeXPCOM(Xpcom.SDKDirectory, BrowseoFXServicesManager.DirectoryServiceProvider);

        //        Xpcom.RegisterInstance(typeof(nsIXULRuntime).GUID, "nsXULAppInfo", Gecko.Contracts.AppInfo, nsXULAppInfo.Instance);

        //        using (var directoryService = Xpcom.GetService2<nsIDirectoryService>(Gecko.Contracts.DirectoryService))
        //        {
        //            if (directoryService != null)
        //                directoryService.Instance.RegisterProvider(BrowseoFXServicesManager.DirectoryServiceProvider);
        //        }

        //        Xpcom.InitializeThreadManager();
        //        Xpcom.DoEvents();

        //        #region prefs
        //        using (var PreferencesService = Xpcom.GetService2<Gecko.Interfaces.nsIPrefService>(Gecko.Contracts.PreferencesService))
        //        {
        //            //var userPrefsFile = Path.Combine(BrowseoFXServicesManager.DirectoryServiceProvider.ProfilePathRoaming, "prefs.js");

        //            //if (File.Exists(userPrefsFile))
        //            //    PreferencesService.Instance.ReadUserPrefs(BrowseoFXServicesManager.DirectoryServiceProvider.NewLocalFile(userPrefsFile));
        //        }

        //        GeckoPreferences pref = GeckoPreferences.Default;
        //        pref["browser.startup.page"] = 3;
        //        pref["browser.tabs.closeWindowWithLastTab"] = false;
        //        pref["toolkit.telemetry.enabled"] = false;
        //        pref["toolkit.defaultChromeURI"] = "";
        //        pref["toolkit.defaultChromeFeatures"] = "";
        //        pref["toolkit.singletonWindowType"] = "";
        //        //pref["toolkit.defaultChromeURI"] = "chrome://browser/content/browser.xul";
        //        //pref["toolkit.defaultChromeFeatures"] = "chrome,dialog=no,all";
        //        //pref["toolkit.singletonWindowType"] = "navigator:browser";

        //        pref["browser.tabs.remote.autostart"] = false;
        //        pref["extensions.e10sBlockedByAddons"] = true;
        //        pref["extensions.e10sBlocksEnabling"] = false;

        //        //https://stackoverflow.com/questions/3729477/how-to-automatically-import-bookmarks-in-firefox
        //        pref["browser.places.importBookmarksHTML"] = true;
        //        pref["browser.bookmarks.restore_default_bookmarks"] = false;
        //        pref["browser.shell.checkDefaultBrowser"] = false;
        //        pref["browser.bookmarks.autoExportHTML"] = true;
        //        //pref["browser.bookmarks.file"] = "Bookmarks.html";

        //        //https://developer.mozilla.org/en-US/Firefox/Enterprise_deployment
        //        pref["app.update.enabled"] = false;
        //        pref["app.update.auto"] = false;
        //        pref["app.update.mode"] = 0;
        //        pref["app.update.service.enabled"] = false;
        //        pref["browser.rights.3.shown"] = true;
        //        pref["browser.startup.homepage_override.mstone"] = "ignore";
        //        pref["pdfjs.disabled"] = true;
        //        pref["shumway.disabled"] = true;
        //        pref["plugins.notifyMissingFlash"] = false;
        //        pref["plugins.hide_infobar_for_outdated_plugin"] = true;
        //        pref["datareporting.healthreport.service.enabled"] = false;
        //        pref["datareporting.policy.dataSubmissionEnabled"] = false;
        //        pref["toolkit.crashreporter.enabled"] = false;

        //        GeckoPreferences userPref = GeckoPreferences.User;
        //        userPref["datareporting.healthreport.uploadEnabled"] = false;
        //        userPref["datareporting.policy.currentPolicyVersion"] = 0;
        //        userPref["datareporting.policy.dataSubmissionEnabled"] = false;
        //        userPref["datareporting.policy.dataSubmissionPolicyBypassNotification"] = false;
        //        userPref["datareporting.policy.dataSubmissionPolicyAcceptedVersion"] = 0;
        //        //userPref["datareporting.policy.dataSubmissionPolicyNotifiedTime"] = 0;
        //        userPref["datareporting.policy.firstRunURL"] = "";
        //        userPref["datareporting.policy.minimumPolicyVersion"] = 0;
        //        userPref["datareporting.sessions.current.firstPaint"] = 0;
        //        userPref["datareporting.sessions.current.sessionRestored"] = 0;

        //        ///sandbox bullshit
        //        userPref["security.sandbox.content.level"] = 0;
        //        userPref["security.sandbox.gpu.level"] = 0;
        //        string tempdir = userPref["security.sandbox.content.tempDirSuffix"] as string;
        //        if (tempdir == null || tempdir == "")
        //        {
        //            userPref["security.sandbox.content.tempDirSuffix"] = Guid.NewGuid().ToString();
        //        }

        //        //userPref["full-screen-api.enabled"] = false;


        //        #endregion

        //        using (var TransportService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.TransportService)) { }
        //        using (var DnsService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.DnsService)) { }
        //        using (var NetworkIOService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.NetworkIOService)) { }
        //        using (var ChromeRegistry = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.ChromeRegistry)) { }
        //        //using (var SuppressorService = Xpcom.GetService2<Gecko.Interfaces.nsISupports>(Gecko.Contracts.SuppressorService)) { }


        //        using (var windowWatcher = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
        //        {
        //            using (var creator = Xpcom.GetService2<Gecko.Interfaces.nsIWindowCreator>(Gecko.Contracts.AppStartup))
        //            {
        //                windowWatcher.Instance.SetWindowCreator(creator.Instance);
        //            }
        //            //windowWatcher.Instance.SetWindowCreator(WindowCreator.Instance);
        //        }





        //        //var clh = new nsBrowserContentHandler();
        //        Xpcom.RegisterInstance(typeof(nsIBrowserHandler).GUID, "nsBrowserContentHandler", "@mozilla.org/browser/clh;1", nsBrowserHandler.Instance);
        //        var glue = new nsBrowserGlue();
        //        glue.RegisterObservers();
        //        Xpcom.RegisterInstance(typeof(nsIBrowserGlue).GUID, "nsBrowserGlue", "@mozilla.org/browser/browserglue;1", glue);

        //        Xpcom.LoadExtension(Path.Combine(binDirectory, "browser", "BFXComponents"), "components.manifest");

        //        using (var startupNotifier = Xpcom.GetService2<Gecko.Interfaces.nsIObserver>(Gecko.Contracts.AppStartupNotifier))
        //        {
        //            startupNotifier.Instance.Observe(null, "app-startup", null);
        //        }


        //        using (var appStartupSrv = Xpcom.GetService2<Gecko.Interfaces.nsIAppStartup>(Gecko.Contracts.AppStartup))
        //        {
        //            appStartupSrv.Instance.CreateHiddenWindow();

        //            using (var obsSvc = Xpcom.GetService2<Gecko.Interfaces.nsIObserverService>(Gecko.Contracts.ObserverService))
        //            {
        //                //obsSvc.Instance.AddObserver(new nsObserverAny(), "browser-delayed-startup-finished", false);

        //                obsSvc.Instance.NotifyObservers(null, "app-startup", null);
        //                obsSvc.Instance.NotifyObservers(null, "profile-do-change", "startup");

        //                var xulfxPath = System.IO.Path.Combine(@"C:\Users\eli\Documents\Visual Studio 2015\Projects\Firefox Builds\Builds\xulfx\Rebuilt\vmas-xulfx-a340969180cd\bin\Debug", "XulFx.xpi");
        //                Xpcom.XulfxPath = xulfxPath;
        //                Xpcom.LoadExtension(Xpcom.XulfxPath, "");


        //                Xpcom.DoEvents();
        //                //addons
        //                using (var em = Xpcom.GetService2<Gecko.Interfaces.nsIObserver>(Gecko.Contracts.AddonsIntegration))
        //                {
        //                    em.Instance.Observe(null, "addons-startup", null);
        //                }

        //                using (var SessionStartup = Xpcom.GetService2<Gecko.Interfaces.nsISessionStartup>(Gecko.Contracts.SessionStartup)) { }

        //                obsSvc.Instance.NotifyObservers(null, "load-extension-defaults", null);
        //                obsSvc.Instance.NotifyObservers(null, "profile-after-change", "startup");
        //                obsSvc.Instance.NotifyObservers(null, "profile-initial-state", null);


        //                //cmdline app
        //                using (var cmdLine = Xpcom.GetService2<Gecko.Interfaces.nsICommandLineRunner>(Gecko.Contracts.CommandLine))
        //                {
        //                    //using (var aFlag = new nsAString())
        //                    //{
        //                    //    aFlag.SetData("-chrome chrome://browser/content/browser.xul");
        //                    //    cmdLine.Instance.HandleFlag(aFlag, false);

        //                    //    aFlag.SetData("y-default");
        //                    //    cmdLine.Instance.HandleFlag(aFlag, false);
        //                    //}
        //                    //using (var cmdHandlerCLH = Xpcom.GetService2<Gecko.Interfaces.nsICommandLineHandler>("@mozilla.org/toolkit/default-clh;1"))
        //                    //{
        //                    //    //cmdHandlerCLH.Instance.Handle(cmdLine as nsICommandLine);
        //                    //}



        //                    //using (var cmdHandlerCLH = Xpcom.GetService2<Gecko.Interfaces.nsICommandLineValidator>("@mozilla.org/browser/clh;1"))
        //                    //{
        //                    //    cmdHandlerCLH.Instance.Handle(cmdLine as nsICommandLine);
        //                    //}

        //                    //obsSvc.Instance.NotifyObservers(cmdLine.Instance as Gecko.Interfaces.nsISupports, "command-line-startup", null);
        //                    //obsSvc.Instance.NotifyObservers(cmdLine.Instance as Gecko.Interfaces.nsISupports, "command-line-handler", "m-browser");
        //                    cmdLine.Instance.Run();

        //                    //using (var cmdHandlerCLH = Xpcom.GetService2<Gecko.Interfaces.nsIBrowserHandler>("@mozilla.org/browser/clh;1"))
        //                    //{
        //                    //    //cmdHandlerCLH.Instance.Handle(cmdLine as nsICommandLine);
        //                    //}


        //                    //using (var clh = Xpcom.GetService2<nsIBrowserHandler>("@mozilla.org/browser/clh;1"))
        //                    //{

        //                    //}
        //                }

        //                //cmdline nsDefaultCLH


        //                // clear out any environment variables which may have been set
        //                // during the relaunch process now that we know we won't be relaunching.
        //                Environment.SetEnvironmentVariable("XRE_PROFILE_PATH", "");
        //                Environment.SetEnvironmentVariable("XRE_PROFILE_LOCAL_PATH", "");
        //                Environment.SetEnvironmentVariable("XRE_PROFILE_NAME", "");
        //                Environment.SetEnvironmentVariable("XRE_START_OFFLINE", "");
        //                Environment.SetEnvironmentVariable("NO_EM_RESTART", "");
        //                Environment.SetEnvironmentVariable("XUL_APP_FILE", "");
        //                Environment.SetEnvironmentVariable("XRE_BINARY_PATH", "");



        //                obsSvc.Instance.NotifyObservers(null, "final-ui-startup", null);
        //            }

        //            //using (var ww = Xpcom.GetService2<nsIWindowWatcher>(Contracts.WindowWatcher))
        //            //{
        //            //    //  var argArray = Components.classes["@mozilla.org/array;1"]
        //            //    //.createInstance(Components.interfaces.nsIMutableArray);
        //            //    var argArray = Xpcom.CreateInstance<nsIMutableArray>("@mozilla.org/array;1");
        //            //    argArray.AppendElement(null, false);
        //            //    argArray.AppendElement(null, false);
        //            //    argArray.AppendElement(null, false);
        //            //    argArray.AppendElement(null, false);

        //            //    var window = ww.Instance.OpenWindow(null, "chrome://browser/content/browser.xul", "_blank", "chrome,dialog=no,all", argArray as nsISupports);
        //            //    using (var BrowserElementProxy = Xpcom.GetService2<Gecko.Interfaces.nsIDOMGlobalPropertyInitializer>("@mozilla.org/dom/browser-element-proxy;1"))
        //            //    {
        //            //        var thisWindow = Xpcom.QueryInterface<mozIDOMWindowProxy>(window);
        //            //        var helper = Xpcom.CreateInstance<nsIXulfxDOMWindowHelper>(Contracts.XulfxDOMWindow);
        //            //        helper.Init(window);

        //            //        BrowserElementProxy.Instance.Init(helper.GetMozWindowAttribute());
        //            //    }
        //            //}

        //            //const ss = Cc["@mozilla.org/browser/sessionstore;1"].getService(Ci.nsISessionStore);
        //            //nsISessionStore ss = Xpcom.GetService<nsISessionStore>("@mozilla.org/browser/sessionstore;1");
        //            //var canr =  ss.GetCanRestoreLastSessionAttribute();


        //            appStartupSrv.Instance.DoneStartingUp();

        //            //try
        //            //{


        //            //}
        //            //catch { }
        //            //appStartupSrv.Instance.Run();
        //        }

        //        //mMessageManager = BrowseoFXpcom.CreateInstance<nsIMessageBroadcaster>(Contracts.ChildProcessMessageManager);

        //        //using (var globalMM = Xpcom.GetService2<Gecko.Interfaces.nsIMessageListenerManager>("@mozilla.org/globalmessagemanager;1"))
        //        //{
        //        //    var pp = globalMM.QueryInterface<nsIFrameScriptLoader>();
        //        //    using (var aFlag = new nsAString())
        //        //    {
        //        //        aFlag.SetData("resource://gre/modules/commonjs/toolkit/loader.js");
        //        //        pp.LoadFrameScript(aFlag, false, true);
        //        //    }
        //        //}

        //        //public const string JsSubscriptLoader = "@mozilla.org/moz/jssubscript-loader;1";
        //        //using (var ppmm = Xpcom.GetService2<Gecko.Interfaces.nsIMessageBroadcaster>("@mozilla.org/parentprocessmessagemanager;1"))
        //        //{
        //        //    //var pp = ppmm.QueryInterface<nsIFrameScriptLoader>();
        //        //    //using (var aFlag = new nsAString())
        //        //    //{
        //        //    //    aFlag.SetData("resource://gre/modules/commonjs/toolkit/loader.js");
        //        //    //    pp.LoadFrameScript(aFlag,false,true);
        //        //    //}

        //        //}

        //        //nsIAboutNewTabService
        //        using (var AboutNewTabService = Xpcom.GetService2<nsIAboutNewTabService>("@mozilla.org/browser/aboutnewtab-service;1"))
        //        {
        //            using (var result = new nsACString())
        //            {
        //                AboutNewTabService.Instance.GetDefaultURLAttribute(result);
        //            }
        //        }




        //        var xulWindow = new GeckoXULWindow();
        //        //nsObserverService.ObserverService.AddObserver(xulWindow, "browser-delayed-startup-finished", false);
        //        xulWindow.WaitUntilChromeLoad();
        //        xulWindow.Width = 1280;
        //        xulWindow.Height = 800;

        //        //var tabbrowser = (Gecko.DOM.GeckoXULElement)xulWindow.View.Document.GetElementById("content");
        //        ////var tab = tabbrowser.GetProperty<nsIDOMNode>("selectedBrowser");//.Wrap(GeckoNode.Create);
        //        ////var SelectedContentDocument = tab.GetProperty<nsIDOMDocument>("contentDocument").Wrap(GeckoDocument.Create);

        //        //var broadcaster = tabbrowser.GetProperty<nsIMessageListenerManager>("messageManager");



        //        //public const string JsLoader = "@mozilla.org/moz/jsloader;1";
        //        //using (var JsLoader = Xpcom.GetService2<Gecko.Interfaces.xpcIJSModuleLoader>(Contracts.JsLoader))
        //        //{
        //        //    using (var aFlag = new nsAUTF8String())
        //        //    {
        //        //        aFlag.SetData("resource://gre/modules/PageThumbs.jsm");
        //        //        var loaded = JsLoader.Instance.IsModuleLoaded(aFlag);
        //        //        if (!loaded)
        //        //        {

        //        //        }
        //        //    }
        //        //}
        //        xulWindow.ShowDialog();
        //    }

        //    Console.WriteLine("shutdown");
        //    Console.ReadKey();
        //    Xpcom.Shutdown();
        //}
    }

    ////nsISessionStartup
    //public class nsSessionStartup : nsISessionStartup,
    //    nsIObserver,
    //    nsISupportsWeakReference
    //{

    //    private GeckoWeakReference _weakRef;
    //    public GeckoWeakReference WeakReference
    //    {
    //        get { return _weakRef ?? (_weakRef = new GeckoWeakReference(this)); }
    //        protected set { _weakRef = value; }
    //    }
    //    [return: MarshalAs(UnmanagedType.Interface)]
    //    public nsIWeakReference GetWeakReference()
    //    {
    //        return WeakReference;
    //    }


    //    [return: MarshalAs(UnmanagedType.U1)]
    //    public bool DoRestore()
    //    {
    //        return File.Exists(Path.Combine(BrowseoFXServicesManager.BrowserProfilesManagerService.SelectedProfilePathRoaming, "sessionstrore.js"));
    //    }

    //    public JSVal GetOnceInitializedAttribute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [return: MarshalAs(UnmanagedType.U1)]
    //    public bool GetPreviousSessionCrashedAttribute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public uint GetSessionTypeAttribute()
    //    {
    //        return DoRestore() ? nsISessionStartupConsts.RESUME_SESSION : nsISessionStartupConsts.NO_SESSION;
    //    }

    //    public JSVal GetStateAttribute()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    [return: MarshalAs(UnmanagedType.U1)]
    //    public bool GetWillOverrideHomepageAttribute()
    //    {
    //        //TODO: redo for own homepage
    //        return false;
    //    }

    //    [return: MarshalAs(UnmanagedType.U1)]
    //    public bool IsAutomaticRestoreEnabled()
    //    {
    //        return true;
    //    }

    //    public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
    //    {
    //        switch (aTopic)
    //        {
    //            case "app-startup":
    //                nsObserverService.Instance.addObserver(this, "final-ui-startup", true);
    //                nsObserverService.Instance.addObserver(this, "quit-application", true);
    //                break;
    //            case "final-ui-startup":
    //                nsObserverService.ObserverService.RemoveObserver(this, "final-ui-startup");
    //                nsObserverService.ObserverService.RemoveObserver(this, "quit-application");
    //                this.init();
    //                break;
    //            case "quit-application":
    //                // no reason for initializing at this point (cf. bug 409115)
    //                nsObserverService.ObserverService.RemoveObserver(this, "final-ui-startup");
    //                nsObserverService.ObserverService.RemoveObserver(this, "quit-application");
    //                nsObserverService.ObserverService.RemoveObserver(this, "browser:purge-session-history");
    //                break;
    //            case "sessionstore-windows-restored":
    //                nsObserverService.ObserverService.RemoveObserver(this, "sessionstore-windows-restored");
    //                // free _initialState after nsSessionStore is done with it
    //                break;
    //            case "browser:purge-session-history":
    //                nsObserverService.ObserverService.RemoveObserver(this, "browser:purge-session-history");
    //                // reset all state on sanitization
    //                break;
    //        }
    //    }

    //    private void init()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

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
            // In the past, when an instance was not already running, the -remote
            // option returned an error code. Any script or application invoking the
            // -remote option is expected to be handling this case, otherwise they
            // wouldn't be doing anything when there is no Firefox already running.
            // Making the -remote option always return an error code makes those
            // scripts or applications handle the situation as if Firefox was not
            // already running.

            using (var aFlag = new nsAString())
            {
                //handleFlag
                //browser
                //remote
                //preferences
                //silent
                //private-window
                //private
                aFlag.SetData("browser");
                if (cmdLine.HandleFlag(aFlag, false))
                {

                }

                aFlag.SetData("remote");
                if (cmdLine.HandleFlag(aFlag, false))
                {

                }

                aFlag.SetData("preferences");
                if (cmdLine.HandleFlag(aFlag, false))
                {

                }

                aFlag.SetData("silent");
                if (cmdLine.HandleFlag(aFlag, false))
                {

                }

                aFlag.SetData("private-window");
                if (cmdLine.HandleFlag(aFlag, false))
                {

                }

                aFlag.SetData("private");
                if (cmdLine.HandleFlag(aFlag, false))
                {

                }

                //handleFlagWithParam
                //new-window
                //new-tab
                //chrome
                //search
                //file
                using (var result = new nsAString(""))
                {
                    string resString = "";

                    aFlag.SetData("new-window");
                    result.SetData("");
                    cmdLine.HandleFlagWithParam(aFlag, false, result);
                    resString = result.ToString();
                    if (resString != "" && resString != null)
                    {

                    }

                    aFlag.SetData("new-tab");
                    result.SetData("");
                    cmdLine.HandleFlagWithParam(aFlag, false, result);
                    resString = result.ToString();
                    if (resString != "" && resString != null)
                    {

                    }

                    aFlag.SetData("chrome");
                    result.SetData("");
                    cmdLine.HandleFlagWithParam(aFlag, false, result);
                    resString = result.ToString();
                    if (resString != "" && resString != null)
                    {

                    }

                    aFlag.SetData("search");
                    result.SetData("");
                    cmdLine.HandleFlagWithParam(aFlag, false, result);
                    resString = result.ToString();
                    if (resString != "" && resString != null)
                    {

                    }

                    aFlag.SetData("file");
                    result.SetData("");
                    cmdLine.HandleFlagWithParam(aFlag, false, result);
                    resString = result.ToString();
                    if (resString != "" && resString != null)
                    {

                    }
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

    //public class nsBrowserContentHandler :
    //    nsIBrowserHandler,
    //    nsIContentHandler
    //{
    //    public void GetDefaultArgsAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
    //    {
    //        result.SetData("about:home");
    //    }

    //    public void GetFeatures([MarshalAs(UnmanagedType.Interface)] nsICommandLine aCmdLine, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
    //    {

    //    }

    //    public void GetStartPageAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase result)
    //    {

    //    }

    //    public void SetDefaultArgsAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase value)
    //    {

    //    }

    //    public void SetStartPageAttribute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] nsAUTF8StringBase value)
    //    {

    //    }

    //    public void HandleContent([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aContentType, [MarshalAs(UnmanagedType.Interface)] nsIInterfaceRequestor aWindowContext, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest)
    //    {

    //    }
    //}

    public class nsBrowserGlue :
        nsIBrowserGlue,
        nsIObserver,
        nsISupportsWeakReference

    {

        private GeckoWeakReference _weakRef;
        public GeckoWeakReference WeakReference
        {
            get { return _weakRef ?? (_weakRef = new GeckoWeakReference(this)); }
            protected set { _weakRef = value; }
        }
        [return: MarshalAs(UnmanagedType.Interface)]
        public nsIWeakReference GetWeakReference()
        {
            return WeakReference;
        }


        public void RegisterObservers()
        {
            var os = nsObserverService.Instance;
            os.addObserver(this, "notifications-open-settings", false);
            os.addObserver(this, "prefservice:after-app-defaults", false);
            os.addObserver(this, "final-ui-startup", false);
            os.addObserver(this, "browser-delayed-startup-finished", false);
            os.addObserver(this, "sessionstore-windows-restored", false);
            os.addObserver(this, "browser:purge-session-history", false);
            os.addObserver(this, "quit-application-requested", false);
            os.addObserver(this, "quit-application-granted", false);
            //if (OBSERVE_LASTWINDOW_CLOSE_TOPICS)
            //{
            //    os.addObserver(this, "browser-lastwindow-close-requested", false);
            //    os.addObserver(this, "browser-lastwindow-close-granted", false);
            //}
            os.addObserver(this, "weave:service:ready", false);
            os.addObserver(this, "fxaccounts:onverified", false);
            os.addObserver(this, "fxaccounts:device_disconnected", false);
            os.addObserver(this, "weave:engine:clients:display-uris", false);
            os.addObserver(this, "session-save", false);
            os.addObserver(this, "places-init-complete", false);
            //this._isPlacesInitObserver = true;
            os.addObserver(this, "places-database-locked", false);
           // this._isPlacesLockedObserver = true;
            os.addObserver(this, "distribution-customization-complete", false);
            os.addObserver(this, "handle-xul-text-link", false);
            os.addObserver(this, "profile-before-change", false);
            //if (AppConstants.MOZ_TELEMETRY_REPORTING)
            //{
            //    os.addObserver(this, "keyword-search", false);
            //}
            os.addObserver(this, "browser-search-engine-modified", false);
            os.addObserver(this, "restart-in-safe-mode", false);
            os.addObserver(this, "flash-plugin-hang", false);
            os.addObserver(this, "xpi-signature-changed", false);
            os.addObserver(this, "autocomplete-did-enter-text", false);

            //if (AppConstants.NIGHTLY_BUILD)
            //{
            //    os.addObserver(this, AddonWatcher.TOPIC_SLOW_ADDON_DETECTED, false);
            //}

            //this._flashHangCount = 0;
            //this._firstWindowReady = new Promise(resolve => this._firstWindowLoaded = resolve);

            //if (AppConstants.platform == "win" ||
            //    AppConstants.platform == "macosx")
            //{
            //    // Handles prompting to inform about incompatibilites when accessibility
            //    // and e10s are active together.
            //    E10SAccessibilityCheck.init();
            //}
        }

        public void Observe([MarshalAs(UnmanagedType.Interface)] nsISupports aSubject, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringMarshaler))] string aTopic, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(WStringMarshaler))] string aData)
        {
            switch (aTopic)
            {
                case "places-init-complete":
                case "places-database-locked":
                case "distribution-customization-complete":
                case "browser-delayed-startup-finished":
                case "quit-application-granted":
                    nsObserverService.ObserverService.RemoveObserver(this, aTopic);
                    break;

                case "profile-before-change":
                    // Any component depending on Places should be finalized in
                    // _onPlacesShutdown.  Any component that doesn't need to act after
                    // the UI has gone should be finalized in _onQuitApplicationGranted.
                    dispose();
                    break;

                default:
                    break;
            }

            using (var browserClassInitializer = Xpcom.GetService2<Gecko.Interfaces.nsISupports>("@mozilla.browseo/browser/browserClassInitializer;1"))
            {
                var obs = browserClassInitializer.QueryInterface<nsIObserver>();
                obs.Observe(aSubject, aTopic, aData);
            }
        }

        private void dispose()
        { 
        }

        public void Sanitize([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aParentWindow)
        {

        }
    }
}
