using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using NUnit.Framework;
using Gecko;
using Gecko.DOM.HTML;
using Gecko.DOM;
using Gecko.Interop;
using Gecko.Interfaces;

namespace UnitTests
{
	[TestFixture]
	public class ComInteropTests : TestsBase
	{
		GeckoHTMLDocument htmlDocument;

		[SetUp]
		public override void BeforeEachTestSetup()
		{
			base.BeforeEachTestSetup();

			using(var parser = GeckoParser.Create())
			{
				htmlDocument = (GeckoHTMLDocument)parser.Parse("<!DOCTYPE html><html><head><title>hello page</title></head><body id='idhello'><!-- comment -->Hello, World!<iframe src='http://google.com'></iframe></body></html>");
			}
		}

		[Test]
		public void GeckoHTMLElement_CreateAndDisposeWrapper()
		{
			int count;
			IntPtr pUnk;
			GeckoHTMLElement element;
			element = htmlDocument.CreateElement("video");
#if DEBUG
			count = ComDebug.GetComRefCount(element.Instance);
			Assert.AreNotEqual(0, count);
#endif
			// AddRef (this will prevent the destruction of the XPCOM object)
			pUnk = Marshal.GetIUnknownForObject(element.Instance);
			
			((IDisposable)element).Dispose();
			
			count = Marshal.Release(pUnk);
			Assert.AreEqual(0, count);
		}

		[Test]
		public void GeckoHTMLElement_TestWrapperCaching()
		{
			IntPtr pUnk;
			GeckoHTMLElement wrapperA, wrapperB;
			wrapperA = htmlDocument.CreateElement("video");
			
			wrapperB = Xpcom.QueryInterface<nsIDOMHTMLElement>(wrapperA.Instance).Wrap(GeckoHTMLElement.Create); // RCW +1 & Wrap
			Assert.IsTrue(object.ReferenceEquals(wrapperA, wrapperB));

			pUnk = Marshal.GetIUnknownForObject(wrapperA.Instance);
			Marshal.Release(pUnk);

			wrapperB = ((nsIDOMHTMLElement)Marshal.GetObjectForIUnknown(pUnk)).Wrap(GeckoHTMLElement.Create);
			Assert.IsTrue(object.ReferenceEquals(wrapperA, wrapperB));
		}

	}
}
