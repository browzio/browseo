using System;
using Gecko.DOM.HTML;
using Gecko.Interfaces;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gecko.DOM
{
	public static class GeckoDOMExtensions
	{
		/// <summary>
		/// Find an element from the given coordinates. This method descends through
		/// frames to find the element the user clicked inside frames.
		/// </summary>
		/// <param name="document">The document to look into.</param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>The element found at the given coordinates or null.</returns>
		public static GeckoElement GetRealElementFromPoint(this GeckoDocument document, float x, float y)
		{
			float cx,cy;
			return GetRealElementFromPoint(document, x, y, out cx, out cy);
		}
		/// <summary>
		/// Find an element from the given coordinates. This method descends through
		/// frames to find the element the user clicked inside frames.
		/// </summary>
		/// <param name="document">The document to look into.</param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="cx">The x-coordinate of specify the point to check, in CSS pixels, relative to the left-most point in the window or frame containing the element that has been found.</param>
		/// <param name="cy">The y-coordinate of specify the point to check, in CSS pixels, relative to the top-most point in the window or frame containing the element that has been found.</param>
		/// <returns>The element found at the given coordinates or null.</returns>
		public static GeckoElement GetRealElementFromPoint(this GeckoDocument document, float x, float y, out float cx, out float cy)
		{
			cx = x;
			cy = y;
			GeckoElement element = document.ElementFromPoint(x, y);
			var iframe = element as GeckoHTMLIFrameElement;
			var frame = element as GeckoHTMLFrameElement;

			if (iframe == null && frame == null)
				return element;

			if (iframe != null)
			{
				float offsetLeft, offsetTop;
				GetIframeContentOffset(iframe, out offsetLeft, out offsetTop);

				nsIDOMClientRect rect = element.Instance.GetBoundingClientRect();
				if (rect == null)
					return element;

				try
				{
					x -= rect.GetLeftAttribute() + offsetLeft;
					y -= rect.GetTopAttribute() + offsetTop;
				}
				finally
				{
					Xpcom.FreeComObject(ref rect);
				}
				if (x < 0 || y < 0)
					return element;

				float outX, outY;
				GeckoElement elem = GetRealElementFromPoint(iframe.ContentDocument, x, y, out outX, out outY);
				if (elem == null)
					return element;
				cx = outX;
				cy = outY;
				return elem;

			}
			return GetRealElementFromPoint(frame.ContentDocument, x, y, out cx, out cy) ?? element;
		}

		/// <summary>
		/// Returns iframe content offset (iframe border + padding).
		/// Note: this function shouldn't need to exist, had the platform provided a
		/// suitable API for determining the offset between the iframe's content and
		/// its bounding client rect. Bug 626359 should provide us with such an API.
		/// </summary>
		/// <param name="iframe">The iframe.</param>
		/// <param name="offsetleft">The distance from the top of the iframe and the top of the content document.</param>
		/// <param name="offsetTop">The distance from the left of the iframe and the left of the content document.</param>
		public static void GetIframeContentOffset(this GeckoHTMLIFrameElement iframe, out float offsetleft, out float offsetTop)
		{
			offsetleft = 0;
			offsetTop = 0;
			GeckoCSSStyleDeclaration style = iframe.ContentWindow.GetComputedStyle(iframe, null);
			if (style == null)
				return;

			string value = style.GetPropertyValue("padding-top");
			offsetTop = float.Parse(value.Remove(value.Length - 2), NumberFormatInfo.InvariantInfo) + iframe.ClientTop;

			value = style.GetPropertyValue("padding-left");
			offsetleft = float.Parse(value.Remove(value.Length - 2), NumberFormatInfo.InvariantInfo) + iframe.ClientLeft;
		}

		public static GeckoElement Closest(this GeckoElement element, string selector)
		{
			while(element != null && !element.Matches(selector))
				element = element.ParentElement;
			return element;
		}

		/// <summary>
		/// Get nodes from give xpath expression.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="xpathExpression">The xpath expression.</param>
		/// <returns></returns>
		public static IEnumerable<GeckoNode> GetNodes(this GeckoNode node, string xpathExpression)
		{
			GeckoDocument document = (node as GeckoDocument) ?? node.OwnerDocument;
			if (document == null)
				throw new InvalidOperationException();

			using(GeckoXPathResult xpathResult = document.Evaluate(xpathExpression, node, GeckoXPathResultType.Any))
			{
				return xpathResult.AsEnumerable();
			}
		}

		/// <summary>
		/// Get a node from give xpath expression.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="xpathExpression">The xpath expression.</param>
		/// <returns></returns>
		public static GeckoNode GetSingleNode(this GeckoNode node, string xpathExpression)
		{
			GeckoDocument document = (node as GeckoDocument) ?? node.OwnerDocument;
			if (document == null)
				throw new InvalidOperationException();

			using(GeckoXPathResult xpathResult = document.Evaluate(xpathExpression, node, GeckoXPathResultType.AnyUnorderedNode))
			{
				return xpathResult.SingleNodeValue;
			}
		}

		/// <summary>
		/// Get a WebBrowserGlueBase from give nsIWebBrowserChrome.
		/// </summary>
		/// <param name="chrome"></param>
		/// <returns></returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static WebBrowserGlueBase GetWebBrowserGlue(this nsIWebBrowserChrome chrome)
		{
			if (chrome == null)
				return null;

			var xulWindow = Xpcom.QueryInterface<nsIXULWindow>(chrome);
			WebBrowserGlueBase wbg = null;
			if (xulWindow != null)
			{
				nsIXULBrowserWindow xbw = xulWindow.GetXULBrowserWindowAttribute();
				if (xbw != null)
				{
					wbg = xbw as WebBrowserGlueBase;
					Xpcom.FreeComObject(ref xbw);
				}
				Xpcom.FreeComObject(ref xulWindow);
			}
			else
			{
				wbg = WindowlessWebView.GetGlue(chrome);
			}
			return wbg;
		}

		/// <summary>
		/// Get a IWebView from give GeckoWebNavigation.
		/// </summary>
		/// <param name="navigation"></param>
		/// <returns></returns>
		public static IWebView GetWebView(this GeckoWebNavigation navigation)
		{
			if (navigation == null)
				throw new ArgumentNullException("navigation");
			
			nsIDocShellTreeItem treeItem = navigation.QueryInterface<nsIDocShellTreeItem>();
			if (treeItem == null)
				return null;

			nsIDocShellTreeOwner treeOwner = treeItem.GetTreeOwnerAttribute();
			Xpcom.FreeComObject(ref treeItem);
			if (treeOwner == null)
				return null;

			nsIWebBrowserChrome chrome = Xpcom.QueryInterface<nsIWebBrowserChrome>(treeOwner);
			Xpcom.FreeComObject(ref treeOwner);
			if (chrome == null)
				return null;

			WebBrowserGlueBase glue = GetWebBrowserGlue(chrome);
			Xpcom.FreeComObject(ref chrome);
			return Xpcom.QueryInterface<IWebView>(glue);
		}

		/// <summary>
		/// Get a IWebView from give GeckoWindow.
		/// </summary>
		/// <param name="window"></param>
		/// <returns></returns>
		public static IWebView GetWebView(this GeckoWindow window)
		{
			return Xpcom.QueryInterface<IWebView>(GetWebBrowserGlue(window));
		}

		/// <summary>
		/// Get a IWebView from give GeckoWindow.
		/// </summary>
		/// <param name="window"></param>
		/// <returns></returns>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static WebBrowserGlueBase GetWebBrowserGlue(this GeckoWindow window)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			nsIDocShell docShell = window.QueryInterface<nsIDocShell>();
			if (docShell == null)
				return null;

			nsIDocShellTreeOwner treeOwner = docShell.GetTreeOwnerAttribute();
			Xpcom.FreeComObject(ref docShell);
			if (treeOwner == null)
				return null;

			nsIWebBrowserChrome chrome = Xpcom.QueryInterface<nsIWebBrowserChrome>(treeOwner);
			Xpcom.FreeComObject(ref treeOwner);
			if (chrome == null)
				return null;

			WebBrowserGlueBase glue = GetWebBrowserGlue(chrome);
			Xpcom.FreeComObject(ref chrome);
			return glue;
		}

	}
}
