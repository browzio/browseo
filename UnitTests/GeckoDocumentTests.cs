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
	public class GeckoDocumentTests : TestsBase
	{
		GeckoHTMLDocument htmlDocument;

		[SetUp]
		public override void BeforeEachTestSetup()
		{
			base.BeforeEachTestSetup();

			using (var parser = GeckoParser.Create())
			{
				htmlDocument = (GeckoHTMLDocument)parser.Parse(
@"<!DOCTYPE html>
<html>
	<head>
		<title>title</title>
	</head>
	<body>
		<h2>heading<h2>
	</body>
</html>"
);
			}
		}


		[Test]
		public void GeckoDocument_TestGetUrl()
		{
			Assert.DoesNotThrow(() => { 
				var url = htmlDocument.Url;
			});
		}

		#region XPath

		[Test]
		public void GeckoDocument_Evaluate()
		{
			GeckoXPathResult xpathResult = htmlDocument.Evaluate("/html/body//h2", htmlDocument, GeckoXPathResultType.Any);
			Assert.IsNotNull(xpathResult);
			Assert.IsNotNull(xpathResult.IterateNext());
		}

		[Test]
		public void GeckoDocument_GetNodes()
		{
			IEnumerable<GeckoNode> nodes = null;
			Assert.DoesNotThrow(() => { nodes = htmlDocument.GetNodes("/html/body//h2"); });
			Assert.IsNotNull(nodes);
			Assert.IsNotNull(nodes.FirstOrDefault());
		}

		[Test]
		public void GeckoDocument_GetSingleNode()
		{
			GeckoNode node = null;
			Assert.DoesNotThrow(() => { node = htmlDocument.GetSingleNode("/html/body//h2"); });
			Assert.IsNotNull(node);
		}

		#endregion

	}
}
