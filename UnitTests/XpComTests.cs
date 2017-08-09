using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Gecko;
using Gecko.Interfaces;

namespace UnitTests
{
	[TestFixture]
	public class XpComTests : TestsBase
	{
		[Test]
		public void Alloc_XpcomInitialize_ReturnsValidPointer()
		{			
			IntPtr memory = Xpcom.Alloc(100);
			Assert.IsFalse(memory == IntPtr.Zero);
			Xpcom.Free(memory);			
		}
		
		[Test]
		public void Realloc_XpcomInitialize_ReturnsValidPointer()
		{		
			IntPtr memory = Xpcom.Alloc(100);			
			memory = Xpcom.Realloc(memory, 200);
			Assert.IsFalse(memory == IntPtr.Zero);
			Xpcom.Free(memory);
		}

#region CreateInstance Unittests

		[Test]
		public void CreateInstance_CreatingGeckoJavaScriptBridge_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIGeckoScriptBridge>(Contracts.GeckoJavaScriptBridge);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingXulfxXPathEvaluator_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIXulfxDOMXPathEvaluator>(Contracts.XulfxXPathEvaluator);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingXulfxCookieManager_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIXulfxCookieManager>(Contracts.XulfxCookieManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingXulfxEventHelper_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIXulfxEventHelper>(Contracts.XulfxEventHelper);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingTextInputProcessor_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsITextInputProcessor>(Contracts.TextInputProcessor);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingInputStreamPump_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIInputStreamPump>(Contracts.InputStreamPump);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingX509CertList_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIX509CertList>(Contracts.X509CertList);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingnsWebBrowser_ReturnsValidInstance()
		{
			var webBrowser = Xpcom.CreateInstance<nsIWebBrowser>(Contracts.WebBrowser);
			Assert.IsNotNull(webBrowser);
			Marshal.ReleaseComObject(webBrowser);
		}			

		[Test]
		[Platform(Exclude="Linux")]
		public void CreateInstance_CreatingWindowsTaskbar_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIWinTaskbar>(Contracts.WindowsTaskbar);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingScriptSecurityManager_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIScriptSecurityManager>(Contracts.ScriptSecurityManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingNullPrincipal_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIPrincipal>(Contracts.NullPrincipal);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPrincipal_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIPrincipal>(Contracts.Principal);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSystemPrincipal_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIPrincipal>(Contracts.SystemPrincipal);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingChromeRegistry_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIChromeRegistry>(Contracts.ChromeRegistry);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingHtmlCopyEncoder_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.HtmlCopyEncoder);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}
		
		[Test]
		public void CreateInstance_CreatingEventListenerService_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIEventListenerService>(Contracts.EventListenerService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}		

		[Test]
		public void CreateInstance_CreatingContentPolicy_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIContentPolicy>(Contracts.ContentPolicy);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingFormData_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIDOMFormData>(Contracts.FormData);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingDomParser_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIDOMParser>(Contracts.DomParser);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingXmlSerializer_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIDOMSerializer>(Contracts.XmlSerializer);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingXmlHttpRequest_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIXMLHttpRequest>(Contracts.XmlHttpRequest);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCspService_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIContentPolicy>(Contracts.CspService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingDataDocumentContentPolicy_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIContentPolicy>(Contracts.DataDocumentContentPolicy);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingNoDataProtocolContentPolicy_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIContentPolicy>(Contracts.NoDataProtocolContentPolicy);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		[Ignore("I've seen this fail on 2 machines now (Win7 + Win8) - so ignoring")]
		public void CreateInstance_CreatingXmlHttpRequestBadCertHandler_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.XmlHttpRequestBadCertHandler);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingXPathNameSpace_ThrowsClassNotRegistered()
		{
			Assert.Throws<COMException>(() => Xpcom.CreateInstance<nsISupports>("@mozilla.org/transformiix/xpath-namespace;1"));			
		}

		[Test]
		public void CreateInstance_CreatingXsltDocumentTransformer_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIXSLTProcessor>(Contracts.DocumentTransformerXslt);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingMork_ThrowsClassNotRegistered()
		{
			Assert.Throws<COMException>(() => Xpcom.CreateInstance<nsISupports>("@mozilla.org/db/mork;1"));			
		}

		[Test]
		public void CreateInstance_CreatingUriFixup_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIURIFixup>(Contracts.UriFuxup);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingEnumeratorForwards_ThrowsClassNotRegistered()
		{
			Assert.Throws<COMException>(() => Xpcom.CreateInstance<nsIEnumerator>("@mozilla.org/docshell/enumerator-forwards;1"));			
		}

		[Test]
		public void CreateInstance_CreatingEnumeratorBackwards_ThrowsClassNotRegistered()
		{
			Assert.Throws<COMException>(() => Xpcom.CreateInstance<nsIEnumerator>("@mozilla.org/docshell/enumerator-backwards;1"));
		}
		

		[Test]
		public void CreateInstance_CreatingWebNavigationInfo_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIWebNavigationInfo>(Contracts.WebNavigationInfo);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingBrowserHistory_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.BrowserHistory);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSHistory_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISHistory>(Contracts.BrowserSHistory);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSHistoryInternal_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISHistory>(Contracts.BrowserSHistoryInternal);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSesionHistoryTransaction_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISHTransaction>(Contracts.BrowserSessionHistoryTransaction);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingXPathEvaluator_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIDOMXPathEvaluator>(Contracts.XPathEvaluator);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingFocusManager_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIFocusManager>(Contracts.FocusManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingWindowController_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIController>(Contracts.WindowController);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingContentPrefService_ThrowsFail()
		{
			var instance = Xpcom.CreateInstance<nsIContentPrefService>(Contracts.ContentPrefService);
			Assert.IsNotNull(instance);			
		}

		[Test]
		public void CreateInstance_CreatingHostnameGrouper_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.ContentPrefHostnameGrouper);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}				

		[Test]
		public void CreateInstance_CreatingTxtSrvFilter_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.EditorTxtSrvFilter);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingTxtSrvFilterMail_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.EditorTxtSrvFilterMail);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingTransactionManager_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsITransactionManager>(Contracts.TransactionManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSpellcheckerInline_ReturnsValidInstance()
		{				
			var instance = Xpcom.CreateInstance<nsIInlineSpellChecker>(Contracts.SpellCheckerInline);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSpellChecker_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.SpellChecker);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPromptService_ReturnsValidInstance()
		{
			var promptService = Xpcom.CreateInstance<nsIPromptService>(Contracts.PromptService);
			Assert.IsNotNull(promptService);
	
			// GeckoWebBrowser registers a global PromptService
			if (Marshal.IsComObject(promptService))
				Marshal.ReleaseComObject(promptService);
		}

		[Test]
		public void CreateInstance_CreatingTooltipTextProvider_ThrowsClassNotRegistered()
		{
			Assert.Throws<COMException>(() => Xpcom.CreateInstance<nsITooltipTextProvider>("@mozilla.org/embedcomp/tooltiptextprovider;1"));
		}
		
		[Test]
		public void CreateInstance_CreatingNsCommandHandler_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICommandHandler>(Contracts.NsCommandHandler);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);

		}

		[Test]
		public void CreateInstance_CreatingAppStartupNotifier_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.AppStartupNotifier);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCommandManager_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsICommandManager>(Contracts.CommandManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCommandParams_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsICommandParams>(Contracts.CommandParams);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingControllerCommandTable_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIControllerCommandTable>(Contracts.ControllerCommandTable);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingBaseCommandController_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsICommandController>(Contracts.BaseCommandController);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingControllerCommandGroup_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIControllerCommandGroup>(Contracts.ControllerCommandGroup);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingRangeFind_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIFind>(Contracts.RangeFind);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingFind_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIWebBrowserFind>(Contracts.Find);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPrintingPromptService_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIPrintingPromptService>(Contracts.PrintingPromptService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingWebBrowserPersist_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIWebBrowserPersist>(Contracts.NsWebBrowserPersist);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingDialogParam_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIDialogParamBlock>(Contracts.DialogParam);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingWindowWatcher_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIWindowWatcher>(Contracts.WindowWatcher);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCookiePrompt_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICookiePromptService>(Contracts.CookiePromptService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingContentBlocker_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.PermissionsContentBlocker);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingAutoConfiguration_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIAutoConfig>(Contracts.AutoConfiguration);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingReadConfig_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIReadConfig>(Contracts.ReadConfig);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}


		[Test]
		public void CreateInstance_CreatingSpellcheckerEngine_ReturnsValidInstance()
		{					
			var instance = Xpcom.CreateInstance<mozISpellCheckingEngine>(Contracts.SpellCheckerEngine);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPersonalDictionary_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<mozIPersonalDictionary>(Contracts.SpellCheckerPersonalDictionary);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingIl8nManager_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<mozISpellI18NManager>(Contracts.SpellCheckerI18nManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingLocaleService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsILocaleService>(Contracts.NsLocaleService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingScriptableDateFormat_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIScriptableDateFormat>(Contracts.ScriptableDateFormat);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCollation_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICollation>(Contracts.Collation);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCollationFactory_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICollationFactory>(Contracts.CollationFactory);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingDateTimeFormat_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.DateTimeFormat);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingLanguageAtomService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.NsLanguageAtomService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);		
		}

		[Test]
		public void CreateInstance_CreatingSemanticUnitsScanner_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISemanticUnitScanner>(Contracts.SemanticUnitScanner);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingLbrk_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.Lbrk);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingWbrk_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.Wbrk);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingStringBundle_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIStringBundleService>(Contracts.StringBundle);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingScriptableUnicodeConverter_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIScriptableUnicodeConverter>(Contracts.ScriptableUnicodeConverter);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingTextToSubUri_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsITextToSubURI>(Contracts.TextToSubURI);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPlatformCharset_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.PlatformCharset);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingUtf8ConverterService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.Utf8ConverterService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingConverterInputStream_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIConverterInputStream>(Contracts.ConverterInputStream);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingUnideDecoderWindows1252_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>("@mozilla.org/intl/unicode/decoder;1?charset=windows-1252");
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingUnicodeDecoderISO8859_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>("@mozilla.org/intl/unicode/decoder;1?charset=ISO-8859-1");
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingEntityConverter_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIEntityConverter>(Contracts.EntityConverter);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSaveAsCharset_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISaveAsCharset>(Contracts.SaveAsCharset);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingUnicodeNormalizer_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIUnicodeNormalizer>(Contracts.UnicodeNormalizer);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingUniCharUtil_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.UniCharUtil);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSciptError_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIScriptError>(Contracts.ScriptError);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}
		
		[Ignore("Creating multiple jsloaders causes multiple copies of mozJSComponentLoader (Singleton) to be inialized, This causes Xpcom.Shutdown to segfault")]		
		[Test]
		public void CreateInstance_CreatingJsLoader_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.JsLoader);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingStyleSheetService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIStyleSheetService>(Contracts.StyleSheetService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingDocumentLoaderFactory_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIDocumentLoaderFactory>(Contracts.DocumentLoaderFactory);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingTransformiixNodeSet_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<txINodeSet>(Contracts.TransformiixNodeSet);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingZipWriter_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIZipWriter>(Contracts.ZipWriter);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPrefLocalizedString_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIPrefLocalizedString>(Contracts.PrefLocalizedString);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPreferencesService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIPrefBranch>(Contracts.PreferencesService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPrefRelativeFile_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIRelativeFilePref>(Contracts.PrefRelativeFile);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPluginHost_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIPluginHost>(Contracts.PluginHost);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingThirdPartyUtil_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<mozIThirdPartyUtil>(Contracts.ThirdPartyUtil);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPermissionManager_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIPermissionManager>(Contracts.PermissionManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSecureBrowser_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISecureBrowserUI>(Contracts.SecureBrowserUI);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingMimeInpurtStream_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIMIMEInputStream>(Contracts.MimeInputStream);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCookiePermission_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICookiePermission>(Contracts.CookiePermission);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingTxtToHtmlConv_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<mozITXTToHTMLConv>(Contracts.TXTToHTMLConv);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingDirIndexParser_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIDirIndexParser>(Contracts.DirIndexParser);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingParserService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.ParserService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSaxParserAttributes_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISAXAttributes>(Contracts.SAXAttributes);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSaxParserXmlReader_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISAXXMLReader>(Contracts.SAXXXMLReader);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingASN1Tree_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIASN1Tree>(Contracts.NsASN1Tree);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCertificateDialogs_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICertificateDialogs>(Contracts.NsCertificateDialogs);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingAlertsService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIAlertsService>(Contracts.AlertsService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSystemInfo_ReturnsValidInstance()
		{	
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.SystemInfo);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingFindService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIFindService>(Contracts.FindService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingLoginInfo_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsILoginInfo>(Contracts.LoginInfo);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Ignore("This causes Xpcom.Shutdown to hang (Gecko 38 beta 7).")]
		[Test]
		public void CreateInstance_CreatingLoginManager_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsILoginManager>(Contracts.LoginManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingJSPerf_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.JsPerf);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingUserInfo_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIUserInfo>(Contracts.UserInfo);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingAppShellComponentBrowserStatusFilter_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.BrowserStatusFilter);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}		

		[Test]
		public void CreateInstance_CreatingCrashReporter_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICrashReporter>(Contracts.CrashReporter);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingUriLoader_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIURILoader>(Contracts.UriLoader);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingExternalHelperAppService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIExternalHelperAppService>(Contracts.UriLoaderExternalHelperAppService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingHelperAppLauncherDialog_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIHelperAppLauncherDialog>(Contracts.HelperAppLauncherDialog);
			Assert.IsNotNull(instance);
			// GeckoWebBrowser registers LauncherDialogFactory.
			if (Marshal.IsComObject(instance))
				Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPrefetchService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIPrefetchService>(Contracts.PrefetchService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingDebug_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupports>(Contracts.Debug);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingConsoleService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIConsoleService>(Contracts.ConsoleService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingErrorService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIErrorService>(Contracts.ErrorService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingUuidGenerator_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIUUIDGenerator>(Contracts.UUIDGenerator);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingVersionComparator_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIVersionComparator>(Contracts.VersionComparator);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingDirectoryService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIDirectoryService>(Contracts.DirectoryService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingFileLocal_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsILocalFile>(Contracts.LocalFile);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCategoryManager_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICategoryManager>(Contracts.CategoryManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingProperties_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIProperties>(Contracts.Properties);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingArray_ReturnsValidInstance()
		{			 
			var instance = Xpcom.CreateInstance<nsIArray>(Contracts.Array);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingObserverService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIObserverService>(Contracts.ObserverService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingIoUtil_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIIOUtil>(Contracts.IOUtil);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCycleCollector_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsICycleCollectorListener>(Contracts.CycleCollectorLogger);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsId_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupportsID>(Contracts.SupportsID);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsBool_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRBool>(Contracts.SupportsBool);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsByte_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRUint8>(Contracts.SupportsByte);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsUInt16_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRUint16>(Contracts.SupportsUInt16);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsUInt32_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRUint32>(Contracts.SupportsUInt32);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsUInt64_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRUint64>(Contracts.SupportsUInt64);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsInt16_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRInt16>(Contracts.SupportsInt16);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsInt32_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRInt32>(Contracts.SupportsInt32);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportInt64_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRInt64>(Contracts.SupportsInt64);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportTime_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsPRTime>(Contracts.SupportsTime);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportChar_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsChar>(Contracts.SupportsChar);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportFloat_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsFloat>(Contracts.SupportsFloat);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportDouble_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsDouble>(Contracts.SupportsDouble);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportVoid_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsVoid>(Contracts.SupportsVoid);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportInterfacePointer_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsInterfacePointer>(Contracts.SupportsInterfacePointer);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportArray_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISupportsArray>(Contracts.SupportsArray);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsCString_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupportsCString>(Contracts.SupportsCString);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsString_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupportsString>(Contracts.SupportsString);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingAtomService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIAtomService>(Contracts.AtomService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingIniParserFactory_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIINIParserFactory>(Contracts.IniParserFactory);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPersistentProperties_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIPersistentProperties>(Contracts.PersistentProperties);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSupportsArray_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsISupportsArray>(Contracts.SupportsArray);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingVariant_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIVariant>(Contracts.Variant);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingWritableVariant_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIWritableVariant>(Contracts.WritableVariant);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		[Platform(Exclude="Linux")]
		public void CreateInstance_CreatingWindowsRegistryKey_ReturnsValidInstance()
		{			 
			var instance = Xpcom.CreateInstance<nsIWindowsRegKey>(Contracts.WindowsRegistryKey);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingMemoryService_ReturnsValidInstance()
		{			 
			var instance = Xpcom.CreateInstance<nsIMemory>(Contracts.MemoryService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingBinaryOutputStream_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIBinaryOutputStream>(Contracts.BinaryOutputStream);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingBinaryInputStream_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIBinaryInputStream>(Contracts.BinaryInputStream);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}
	
		[Test]
		public void CreateInstance_CreatingMultiplexInputStream_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIMultiplexInputStream>(Contracts.MultiplexInputStream);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPipe_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIPipe>(Contracts.Pipe);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingScriptableInputStream_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIScriptableInputStream>(Contracts.ScriptableInputStream);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingStorageStream_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIStorageStream>(Contracts.StorageStream);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingGeolocationProvider_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIGeolocationProvider>(Contracts.GeolocationProvider);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingEnvironment_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIEnvironment>(Contracts.ProcessEnvironment);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingProcessUtil_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIProcess>(Contracts.ProcessUtil);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingTimer_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsITimer>(Contracts.Timer);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingAppShellService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIAppShellService>(Contracts.AppShellService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingPopupWindowManager_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIPopupWindowManager>(Contracts.PopupWindowManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingWindowMediator_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIWindowMediator>(Contracts.WindowMediator);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingHttpIndexService_ReturnsValidInstance()
		{			
			var instance = Xpcom.CreateInstance<nsIHTTPIndex>(Contracts.HttpIndexService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingTransferable_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsITransferable>(Contracts.WidgetTransferable);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSimpleUri_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIURI>(Contracts.SimpleUri);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingCryptoHash_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsICryptoHash>(Contracts.Hash);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingStringInputStream_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIStringInputStream>(Contracts.StringInputStream);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingZipReader_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIZipReader>(Contracts.ZipReader);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingSound_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsISound>(Contracts.Sound);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingEditingSession_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIEditingSession>(Contracts.EditingSession);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void CreateInstance_CreatingHtmlEditor_ReturnsValidInstance()
		{
			var instance = Xpcom.CreateInstance<nsIHTMLEditor>(Contracts.HtmlEditor);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		#endregion

		#region GetService Unittests

		[Test]
		public void GetXulfxMainService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsISupports>(Contracts.Xulfx);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetXulfxDownloadHelper_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsISupports>(Contracts.XulfxDownloadHelper);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetGeckoJavaScriptBridge_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIGeckoScriptBridge>(Contracts.GeckoJavaScriptBridge);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetMemoryInfoDumper_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIMemoryInfoDumper>(Contracts.MemoryInfoDumper);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetLoadContentInfoFactory_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsILoadContextInfoFactory>(Contracts.LoadContentInfoFactory);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetIoService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIIOService>(Contracts.NetworkIOService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetScriptSecurityManager_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIScriptSecurityManager>(Contracts.ScriptSecurityManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetPreferencesService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIPrefService>(Contracts.PreferencesService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetWindowWatcher_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIWindowWatcher>(Contracts.WindowWatcher);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetAppShellService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIAppShellService>(Contracts.AppShellService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetAppShell_CleanXpComInstance_ReturnsValidInstance()
		{
			System.IntPtr ptr = Xpcom.GetService(new Guid("2d96b3df-c051-11d1-a827-0040959a28c9"));
			var instance = (nsIAppShell)Marshal.GetObjectForIUnknown(ptr);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
			Marshal.Release(ptr);
		}

		[Test]
		public void GetAppInfo_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIXULRuntime>(Contracts.AppInfo);
			Assert.IsNotNull(instance);
			Xpcom.FreeComObject(ref instance);
		}

		[Test]
		public void GetXULRuntime_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIXULRuntime>(Contracts.Runtime);
			Assert.IsNotNull(instance);
			Xpcom.FreeComObject(ref instance);
		}

		[Test]
		public void GetAddonsIntegration_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIObserver>(Contracts.AddonsIntegration);
			Assert.IsNotNull(instance);
			Xpcom.FreeComObject(ref instance);
		}

		[Test]
		public void GetConsoleService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIConsoleService>(Contracts.ConsoleService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetDownloadManager_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIDownloadManager>(Contracts.DowndloadManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetHandlerService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIHandlerService>(Contracts.HandlerService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);			
		}

		[Test]
		public void GetProtocolProxyService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolProxyService2>(Contracts.ProtocolProxyService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetControllers_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIControllers>(Contracts.XulControllers);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetXPConnect_CleanXpComInstance_ReturnsValidInstance()
		{			
			var instance = Xpcom.GetService<nsIXPConnect>(Contracts.XPConnect);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetJsSubscriptLoader_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<mozIJSSubScriptLoader>(Contracts.JsSubscriptLoader);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetJsLoader_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<xpcIJSModuleLoader>(Contracts.JsLoader);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetThreadManager_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIThreadManager>(Contracts.ThreadManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetThreadPool_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIThreadPool>(Contracts.ThreadPool);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);			
		}

		[Test]
		public void GetMessageLoop_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIMessageLoop>(Contracts.MessageLoop);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);	
		}

		[Test]
		public void GetDnsService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIDNSService>(Contracts.DnsService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetStreamListenerTee_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIStreamListenerTee>(Contracts.StreamListenerTee);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetCacheService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsICacheService>(Contracts.CacheService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetCacheStorageService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsICacheStorageService>(Contracts.CacheStorageService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetHttpActivityDistributor_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIHttpActivityDistributor>(Contracts.HttpActivityDistributor);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetPrompter_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIPromptFactory>(Contracts.Prompter);
			Assert.IsNotNull(instance);
			if(Marshal.IsComObject(instance))
				Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetCertOverrideService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsICertOverrideService>(Contracts.CertOverride);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetX509CertDb_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIX509CertDB>(Contracts.X509CertDb);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetRandomGenerator_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIRandomGenerator>(Contracts.RandomGenerator);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetKeyObjectFactory_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIKeyObjectFactory>(Contracts.KeyObjectFactory);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetImageCache_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<imgICache>(Contracts.ImageCache);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetCookieManager_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsICookieManager2>(Contracts.CookieManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetBrowserSearchService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIBrowserSearchService>(Contracts.BrowserSearchService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetScreenManager_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIScreenManager>(Contracts.ScreenManager);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetWiFiMonitor_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIWifiMonitor>(Contracts.WiFiMonitor);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetNavHistoryService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIBrowserHistory>(Contracts.NavHistoryService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetNSSErrorsService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsINSSErrorsService>(Contracts.NSSErrorsService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetCrashService_CleanXpComInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsICrashService>(Contracts.CrashService);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetUpdateServiceStub_CleanXpComInstance_ReturnsValidInstance()
		{
			Assert.Throws<COMException>(() =>
			{
				var instance = Xpcom.GetService<nsISupports>(Contracts.UpdateServiceStub);
				Assert.IsNotNull(instance);
				Marshal.ReleaseComObject(instance);
			});
		}

		[Test]
		public void GetNetworkProtocolHandlerHttp_CleanProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIHttpProtocolHandler>(Contracts.NetworkProtocolHttp);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerHttps_CleanProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIHttpProtocolHandler>(Contracts.NetworkProtocolHttps);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerFtp_CleanProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolFtp);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerWyciwyg_CleanProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolWyciwyg);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetHandlerHandlerResource_CleanProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolResource);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerChrome_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolChrome);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerAbout_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolAbout);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}


		[Test]
		public void GetProtocolHandlerData_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolData);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}


		[Test]
		public void GetProtocolHandlerJar_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolJar);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerFile_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolFile);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerJavaScript_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolJavaScript);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerWebSocket_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolWebSocket);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerWebSocketSecure_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolWebSocketSecure);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerDefault_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolDefault);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		[Test]
		public void GetProtocolHandlerViewSource_CleankProtocolHandlerInstance_ReturnsValidInstance()
		{
			var instance = Xpcom.GetService<nsIProtocolHandler>(Contracts.NetworkProtocolViewSource);
			Assert.IsNotNull(instance);
			Marshal.ReleaseComObject(instance);
		}

		#endregion

	}
}
