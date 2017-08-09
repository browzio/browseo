using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko
{
	public static partial class Contracts
	{
		public const string GeckoJavaScriptBridge = "@gecko/scriptbridge;1";
		public const string XulfxContextMenuUpdater = "@xulfx/view/contextmenu/updater;1";
		public const string XulfxContextMenuHandler = "@xulfx/view/contextmenu/handler;1";
		public const string XulfxDownloadHelper = "@xulfx/download/helper;1";
		public const string Xulfx = "@xulfx/xulfx;1";
		public const string XulfxXPathEvaluator = "@xulfx/xpath/evaluator;1";
		public const string XulfxCookieManager = "@xulfx/cookie/helper;1";
		public const string XulfxEventHelper = "@xulfx/event/helper;1";


		public const string WebBrowser = "@mozilla.org/embedding/browser/nsWebBrowser;1";

		#region Network
		public const string MimeInputStream = "@mozilla.org/network/mime-input-stream;1";
		public const string DnsService = "@mozilla.org/network/dns-service;1";
		public const string StreamListenerTee = "@mozilla.org/network/stream-listener-tee;1";
		public const string CacheService = "@mozilla.org/network/cache-service;1";
		public const string CacheStorageService = "@mozilla.org/netwerk/cache-storage-service;1";
		public const string NetworkIOService = "@mozilla.org/network/io-service;1";
		public const string HttpActivityDistributor = "@mozilla.org/network/http-activity-distributor;1";
		public const string ProtocolProxyService = "@mozilla.org/network/protocol-proxy-service;1";
		public const string HandlerService = "@mozilla.org/uriloader/handler-service;1";
		public const string LoadContentInfoFactory = "@mozilla.org/load-context-info-factory;1";

		public const string NetworkProtocolHttp = "@mozilla.org/network/protocol;1?name=http";
		public const string NetworkProtocolHttps = "@mozilla.org/network/protocol;1?name=https";
		public const string NetworkProtocolFtp = "@mozilla.org/network/protocol;1?name=ftp";
		public const string NetworkProtocolResource = "@mozilla.org/network/protocol;1?name=resource";
		public const string NetworkProtocolWyciwyg = "@mozilla.org/network/protocol;1?name=wyciwyg";
		public const string NetworkProtocolChrome = "@mozilla.org/network/protocol;1?name=chrome";
		public const string NetworkProtocolAbout = "@mozilla.org/network/protocol;1?name=about";
		public const string NetworkProtocolData = "@mozilla.org/network/protocol;1?name=data";
		public const string NetworkProtocolJar = "@mozilla.org/network/protocol;1?name=jar";
		public const string NetworkProtocolFile = "@mozilla.org/network/protocol;1?name=file";
		public const string NetworkProtocolJavaScript = "@mozilla.org/network/protocol;1?name=javascript";
		public const string NetworkProtocolWebSocket = "@mozilla.org/network/protocol;1?name=ws";
		public const string NetworkProtocolWebSocketSecure = "@mozilla.org/network/protocol;1?name=wss";
		public const string NetworkProtocolDefault = "@mozilla.org/network/protocol;1?name=default";
		public const string NetworkProtocolViewSource = "@mozilla.org/network/protocol;1?name=view-source";

		public static string NetworkProtocol(string protocol)
		{
			if (protocol == null)
				throw new ArgumentNullException("protocol");

			return "@mozilla.org/network/protocol;1?name=" + protocol.ToLowerInvariant();
		}

		#endregion
		#region Security
		public const string CertOverride = "@mozilla.org/security/certoverride;1";
		public const string X509CertDb = "@mozilla.org/security/x509certdb;1";
		public const string RandomGenerator = "@mozilla.org/security/random-generator;1";
		public const string KeyObjectFactory = "@mozilla.org/security/keyobjectfactory;1";
		public const string Hash = "@mozilla.org/security/hash;1";
		public const string X509CertList = "@mozilla.org/security/x509certlist;1";
		public const string ScriptSecurityManager = "@mozilla.org/scriptsecuritymanager;1";
		public const string NullPrincipal = "@mozilla.org/nullprincipal;1";
		public const string Principal = "@mozilla.org/principal;1";
		public const string SystemPrincipal = "@mozilla.org/systemprincipal;1";
		public const string ContentPolicy = "@mozilla.org/layout/content-policy;1";
		#endregion
		#region AppShell
		public const string AppShellService = "@mozilla.org/appshell/appShellService;1";
		public const string WindowMediator = "@mozilla.org/appshell/window-mediator;1";
		public const string FocusManager = "@mozilla.org/focus-manager;1";
		#endregion
		#region Image
		public const string ImageCache = "@mozilla.org/image/cache;1";
		#endregion
		#region IO
		public const string PromptService = "@mozilla.org/embedcomp/prompt-service;1";
		public const string Prompter = "@mozilla.org/prompter;1";
		public const string SimpleUri = "@mozilla.org/network/simple-uri;1";
		public const string StringInputStream = "@mozilla.org/io/string-input-stream;1";
		public const string DirectoryService = "@mozilla.org/file/directory_service;1";
		public const string LocalFile = "@mozilla.org/file/local;1";
		public const string BinaryInputStream = "@mozilla.org/binaryinputstream;1";
		public const string BinaryOutputStream = "@mozilla.org/binaryoutputstream;1";
		public const string MessageLoop = "@mozilla.org/message-loop;1";
		public const string ThreadManager = "@mozilla.org/thread-manager;1";
		public const string ThreadPool = "@mozilla.org/thread-pool;1";
		public const string MultiplexInputStream = "@mozilla.org/io/multiplex-input-stream;1";
		public const string IOUtil = "@mozilla.org/io-util;1";
		public const string InputStreamPump = "@mozilla.org/network/input-stream-pump;1";
		#endregion

		public const string XPConnect = "@mozilla.org/js/xpc/XPConnect;1";
		public const string DomParser = "@mozilla.org/xmlextras/domparser;1";
		public const string FormData = "@mozilla.org/files/formdata;1";
		public const string EventListenerService = "@mozilla.org/eventlistenerservice;1";
		public const string HtmlCopyEncoder = "@mozilla.org/layout/htmlCopyEncoder;1";
		public const string ChromeRegistry = "@mozilla.org/chrome/chrome-registry;1";
		public const string DOMParser = "@mozilla.org/xmlextras/domparser;1";
		public const string PreferencesService = "@mozilla.org/preferences-service;1";
		public const string Array = "@mozilla.org/array;1";
		public const string WindowWatcher = "@mozilla.org/embedcomp/window-watcher;1";
		public const string WritableVariant = "@mozilla.org/variant;1";
		public const string ZipReader = "@mozilla.org/libjar/zip-reader;1";
		public const string Sound = "@mozilla.org/sound;1";
		public const string Variant = "@mozilla.org/variant;1";
		public const string CategoryManager = "@mozilla.org/categorymanager;1";
		public const string CookieManager = "@mozilla.org/cookiemanager;1";
		public const string BrowserSearchService = "@mozilla.org/browser/search-service;1";
		public const string ScriptableInputStream = "@mozilla.org/scriptableinputstream;1";
		public const string Pipe = "@mozilla.org/pipe;1";
		public const string ObserverService = "@mozilla.org/observer-service;1";
		public const string PluginHost = "@mozilla.org/plugin/host;1";
		public const string VersionComparator = "@mozilla.org/xpcom/version-comparator;1";
		public const string StorageStream = "@mozilla.org/storagestream;1";
		public const string ConsoleService = "@mozilla.org/consoleservice;1";
		public const string ScreenManager = "@mozilla.org/gfx/screenmanager;1";
		public const string WiFiMonitor = "@mozilla.org/wifi/monitor;1";
		public const string WindowsTaskbar = "@mozilla.org/windows-taskbar;1";
		public const string AddonsIntegration = "@mozilla.org/addons/integration;1";

		#region nsISupportsPrimitive's
		public const string SupportsID = "@mozilla.org/supports-id;1";
		public const string SupportsString = "@mozilla.org/supports-string;1";
		public const string SupportsCString = "@mozilla.org/supports-cstring;1";
		public const string SupportsBool = "@mozilla.org/supports-PRBool;1";
		public const string SupportsByte = "@mozilla.org/supports-PRUint8;1";
		public const string SupportsUInt16 = "@mozilla.org/supports-PRUint16;1";
		public const string SupportsUInt32 = "@mozilla.org/supports-PRUint32;1";
		public const string SupportsUInt64 = "@mozilla.org/supports-PRUint64;1";
		public const string SupportsTime = "@mozilla.org/supports-PRTime;1";
		public const string SupportsChar = "@mozilla.org/supports-char;1";
		public const string SupportsInt16 = "@mozilla.org/supports-PRInt16;1";
		public const string SupportsInt32 = "@mozilla.org/supports-PRInt32;1";
		public const string SupportsInt64 = "@mozilla.org/supports-PRInt64;1";
		public const string SupportsFloat = "@mozilla.org/supports-float;1";
		public const string SupportsDouble = "@mozilla.org/supports-double;1";
		public const string SupportsVoid = "@mozilla.org/supports-void;1";
		public const string SupportsInterfacePointer = "@mozilla.org/supports-interface-pointer;1";
		public const string SupportsArray = "@mozilla.org/supports-array;1";
		#endregion

		public const string NavHistoryService = "@mozilla.org/browser/nav-history-service;1";
		public const string XPathEvaluator = "@mozilla.org/dom/xpath-evaluator;1";
		public const string NSSErrorsService = "@mozilla.org/nss_errors_service;1";
		public const string MemoryService = "@mozilla.org/xpcom/memory-service;1";
		public const string MemoryInfoDumper = "@mozilla.org/memory-info-dumper;1";
		public const string XmlSerializer = "@mozilla.org/xmlextras/xmlserializer;1";
		public const string XmlHttpRequest = "@mozilla.org/xmlextras/xmlhttprequest;1";
		public const string CspService = "@mozilla.org/cspservice;1";
		public const string DataDocumentContentPolicy = "@mozilla.org/data-document-content-policy;1";
		public const string NoDataProtocolContentPolicy = "@mozilla.org/no-data-protocol-content-policy;1";
		public const string XmlHttpRequestBadCertHandler = "@mozilla.org/content/xmlhttprequest-bad-cert-handler;1";
		public const string JsSubscriptLoader = "@mozilla.org/moz/jssubscript-loader;1";
		public const string JsLoader = "@mozilla.org/moz/jsloader;1";
		public const string XulControllers = "@mozilla.org/xul/xul-controllers;1";
		public const string DowndloadManager = "@mozilla.org/download-manager;1";
		public const string Runtime = "@mozilla.org/xre/runtime;1";
		public const string AppInfo = "@mozilla.org/xre/app-info;1";
		public const string WidgetTransferable = "@mozilla.org/widget/transferable;1";
		public const string HttpIndexService = "@mozilla.org/browser/httpindex-service;1";
		public const string PopupWindowManager = "@mozilla.org/PopupWindowManager;1";
		public const string Timer = "@mozilla.org/timer;1";
		public const string ProcessUtil = "@mozilla.org/process/util;1";
		public const string ProcessEnvironment = "@mozilla.org/process/environment;1";
		public const string GeolocationProvider = "@mozilla.org/geolocation/provider;1";
		public const string WindowsRegistryKey = "@mozilla.org/windows-registry-key;1";
		public const string PersistentProperties = "@mozilla.org/persistent-properties;1";
		public const string IniParserFactory = "@mozilla.org/xpcom/ini-parser-factory;1";
		public const string AtomService = "@mozilla.org/atom-service;1";
		public const string CycleCollectorLogger = "@mozilla.org/cycle-collector-logger;1";
		public const string Properties = "@mozilla.org/properties;1";
		public const string UUIDGenerator = "@mozilla.org/uuid-generator;1";
		public const string ErrorService = "@mozilla.org/xpcom/error-service;1";
		public const string Debug = "@mozilla.org/xpcom/debug;1";
		public const string PrefetchService = "@mozilla.org/prefetch-service;1";
		public const string HelperAppLauncherDialog = "@mozilla.org/helperapplauncherdialog;1";
		public const string UriLoaderExternalHelperAppService = "@mozilla.org/uriloader/external-helper-app-service;1";
		public const string UriLoader = "@mozilla.org/uriloader;1";
		public const string CrashReporter = "@mozilla.org/toolkit/crash-reporter;1";
		public const string CrashService = "@mozilla.org/crashservice;1";
		public const string BrowserStatusFilter = "@mozilla.org/appshell/component/browser-status-filter;1";
		public const string UserInfo = "@mozilla.org/userinfo;1";
		public const string JsPerf = "@mozilla.org/jsperf;1";
		public const string LoginManager = "@mozilla.org/login-manager;1";
		public const string LoginInfo = "@mozilla.org/login-manager/loginInfo;1";
		public const string FindService = "@mozilla.org/find/find_service;1";
		public const string SystemInfo = "@mozilla.org/system-info;1";
		public const string AlertsService = "@mozilla.org/alerts-service;1";
		public const string NsCertPickDialogs = "@mozilla.org/nsCertPickDialogs;1";
		public const string NsCertificateDialogs = "@mozilla.org/nsCertificateDialogs;1";
		public const string NsASN1Tree = "@mozilla.org/security/nsASN1Tree;1";
		public const string SAXXXMLReader = "@mozilla.org/saxparser/xmlreader;1";
		public const string SAXAttributes = "@mozilla.org/saxparser/attributes;1";
		public const string ParserService = "@mozilla.org/parser/parser-service;1";
		public const string DirIndexParser = "@mozilla.org/dirIndexParser;1";
		public const string TXTToHTMLConv = "@mozilla.org/txttohtmlconv;1";
		public const string CookiePermission = "@mozilla.org/cookie/permission;1";
		public const string SecureBrowserUI = "@mozilla.org/secure_browser_ui;1";
		public const string PermissionManager = "@mozilla.org/permissionmanager;1";
		public const string ThirdPartyUtil = "@mozilla.org/thirdpartyutil;1";
		public const string PrefRelativeFile = "@mozilla.org/pref-relativefile;1";
		public const string PrefLocalizedString = "@mozilla.org/pref-localizedstring;1";
		public const string ZipWriter = "@mozilla.org/zipwriter;1";
		public const string TransformiixNodeSet = "@mozilla.org/transformiix-nodeset;1";
		public const string DocumentLoaderFactory = "@mozilla.org/content/document-loader-factory;1";
		public const string StyleSheetService = "@mozilla.org/content/style-sheet-service;1";
		public const string ScriptError = "@mozilla.org/scripterror;1";

		public const string UniCharUtil = "@mozilla.org/intl/unicharutil;1";
		public const string UnicodeNormalizer = "@mozilla.org/intl/unicodenormalizer;1";
		public const string SaveAsCharset = "@mozilla.org/intl/saveascharset;1";
		public const string EntityConverter = "@mozilla.org/intl/entityconverter;1";
		public const string ConverterInputStream = "@mozilla.org/intl/converter-input-stream;1";
		public const string Utf8ConverterService = "@mozilla.org/intl/utf8converterservice;1";
		public const string PlatformCharset = "@mozilla.org/intl/platformcharset;1";
		public const string TextToSubURI = "@mozilla.org/intl/texttosuburi;1";
		public const string ScriptableUnicodeConverter = "@mozilla.org/intl/scriptableunicodeconverter";
		public const string StringBundle = "@mozilla.org/intl/stringbundle;1";
		public const string SemanticUnitScanner = "@mozilla.org/intl/semanticunitscanner;1";
		public const string UriFuxup = "@mozilla.org/docshell/urifixup;1";
		public const string WebNavigationInfo = "@mozilla.org/webnavigation-info;1";
		public const string WindowController = "@mozilla.org/dom/window-controller;1";
		public const string CookiePromptService = "@mozilla.org/embedcomp/cookieprompt-service;1";
		public const string PermissionsContentBlocker = "@mozilla.org/permissions/contentblocker;1";
		public const string AutoConfiguration = "@mozilla.org/autoconfiguration;1";
		public const string ReadConfig = "@mozilla.org/readconfig;1";
		public const string SpellCheckerEngine = "@mozilla.org/spellchecker/engine;1";
		public const string SpellCheckerPersonalDictionary = "@mozilla.org/spellchecker/personaldictionary;1";
		public const string SpellCheckerI18nManager = "@mozilla.org/spellchecker/i18nmanager;1";
		public const string DocumentTransformerXslt = "@mozilla.org/document-transformer;1?type=xslt";
		public const string BrowserHistory = "@mozilla.org/browser/history;1";
		public const string BrowserSHistory = "@mozilla.org/browser/shistory;1";
		public const string BrowserSHistoryInternal = "@mozilla.org/browser/shistory-internal;1";
		public const string BrowserSessionHistoryTransaction = "@mozilla.org/browser/session-history-transaction;1";
		public const string SecurityEntropy = "@mozilla.org/security/entropy;1";
		public const string ContentPrefService = "@mozilla.org/content-pref/service;1";
		public const string ContentPrefHostnameGrouper = "@mozilla.org/content-pref/hostname-grouper;1";
		public const string EditorTxtSrvFilter = "@mozilla.org/editor/txtsrvfilter;1";
		public const string EditorTxtSrvFilterMail = "@mozilla.org/editor/txtsrvfiltermail;1";
		public const string TransactionManager = "@mozilla.org/transactionmanager;1";
		public const string SpellCheckerInline = "@mozilla.org/spellchecker-inline;1";
		public const string SpellChecker = "@mozilla.org/spellchecker;1";
		public const string NsCommandHandler = "@mozilla.org/embedding/browser/nsCommandHandler;1";
		public const string AppStartupNotifier = "@mozilla.org/embedcomp/appstartup-notifier;1";
		public const string CommandManager = "@mozilla.org/embedcomp/command-manager;1";
		public const string CommandParams = "@mozilla.org/embedcomp/command-params;1";
		public const string ControllerCommandTable = "@mozilla.org/embedcomp/controller-command-table;1";
		public const string BaseCommandController = "@mozilla.org/embedcomp/base-command-controller;1";
		public const string ControllerCommandGroup = "@mozilla.org/embedcomp/controller-command-group;1";
		public const string RangeFind = "@mozilla.org/embedcomp/rangefind;1";
		public const string Find = "@mozilla.org/embedcomp/find;1";
		public const string PrintingPromptService = "@mozilla.org/embedcomp/printingprompt-service;1";
		public const string NsWebBrowserPersist = "@mozilla.org/embedding/browser/nsWebBrowserPersist;1";
		public const string DialogParam = "@mozilla.org/embedcomp/dialogparam;1";
		public const string NsLocaleService = "@mozilla.org/intl/nslocaleservice;1";
		public const string ScriptableDateFormat = "@mozilla.org/intl/scriptabledateformat;1";
		public const string Collation = "@mozilla.org/intl/collation;1";
		public const string CollationFactory = "@mozilla.org/intl/collation-factory;1";
		public const string DateTimeFormat = "@mozilla.org/intl/datetimeformat;1";
		public const string NsLanguageAtomService = "@mozilla.org/intl/nslanguageatomservice;1";
		public const string Lbrk = "@mozilla.org/intl/lbrk;1";
		public const string Wbrk = "@mozilla.org/intl/wbrk;1";
		public const string EditingSession = "@mozilla.org/editor/editingsession;1";
		public const string HtmlEditor = "@mozilla.org/editor/htmleditor;1";
		public const string TextInputProcessor = "@mozilla.org/text-input-processor;1";

		public const string UpdateServiceStub = "@mozilla.org/updates/update-service-stub;1";

	}
}
