using System;
using Gecko;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.DOM.Abstractions;
using Gecko.DOM.HTML;
using System.Runtime.InteropServices;
using Gecko.DOM.XUL;
using Gecko.DOM.XML;

namespace Gecko.DOM
{
	public class GeckoDocument : GeckoNode<nsIDOMDocument>
	{
		public static GeckoDocument Create(nsIDOMDocument document)
		{
			GeckoDocument wrapper = TryCast<nsIDOMHTMLDocument, GeckoHTMLDocument>(document, GeckoHTMLDocument.Create);

			if (wrapper == null)
				wrapper = TryCast<nsIDOMXULDocument, GeckoXULDocument>(document, GeckoXULDocument.Create);

			if (wrapper == null)
				wrapper = TryCast<nsIDOMXMLDocument, GeckoXMLDocument>(document, GeckoXMLDocument.Create);

			if (wrapper == null)
				return new GeckoDocument(document);

			return wrapper;
		}

		private static GeckoDocument TryCast<TInterface, TWrapper>(nsIDOMDocument instance, Func<TInterface, TWrapper> create)
			where TInterface : class
			where TWrapper : GeckoDocument
		{
			var obj = Xpcom.QueryInterface<TInterface>(instance);
			if (obj != null)
			{
				return (GeckoDocument)create(obj);
			}
			return null;
		}

		protected GeckoDocument(nsIDOMDocument document)
			: base(document)
		{

		}

		public GeckoDocumentType Doctype
		{
			get { return Instance.GetDoctypeAttribute().Wrap(GeckoDocumentType.Create); }
		}

		/// <summary>
		/// Gets the top-level document element (for HTML documents, this is the html tag).
		/// </summary>
		public GeckoElement DocumentElement
		{
			get
			{
				return Instance.GetDocumentElementAttribute().Wrap(GeckoElement.Create);
			}
		}

		public TEvent CreateEvent<TEvent>(string eventType)
			where TEvent : GeckoEvent
		{
			if(eventType == null)
				throw new ArgumentNullException("eventType");
			using (var aEventType = new nsAString(eventType))
			{
				return (TEvent)Instance.CreateEvent(aEventType).Wrap(GeckoEvent.Create);
			}
		}

		/// <summary>
		/// In an HTML document creates the specified HTML element or HTMLUnknownElement if the element is not known.
		/// In a XUL document creates the specified XUL element.
		/// In other documents creates an element with a null namespaceURI.
		/// </summary>
		/// <param name="tagName">a string that specifies the type of element to be created.</param>
		/// <returns></returns>
		public GeckoElement CreateElement(string tagName)
		{
			if (tagName == null)
				throw new ArgumentNullException("tagName");

			if (tagName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("tagName");

			using (var aTagName = new nsAString(tagName))
			{
				return Instance.CreateElement(aTagName).Wrap(GeckoElement.Create);
			}
		}

		public GeckoDocumentFragment CreateDocumentFragment()
		{
			return Instance.CreateDocumentFragment().Wrap(GeckoDocumentFragment.Create);
		}

		public GeckoText CreateText(string data)
		{
			return nsString.Pass<nsIDOMText>(Instance.CreateTextNode, data).Wrap(GeckoText.Create);
		}

		public GeckoComment CreateComment(string data)
		{
			return nsString.Pass<nsIDOMComment>(Instance.CreateComment, data).Wrap(GeckoComment.Create);
		}

		public GeckoAttribute CreateAttribute(string name)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("name");

			return nsString.Pass<nsIDOMAttr>(Instance.CreateAttribute, name).Wrap(GeckoAttribute.Create);
		}

		/// <summary>
		/// Returns a collection containing all elements in the document with a given tag name.
		/// </summary>
		/// <param name="tagName"></param>
		/// <returns></returns>
		public GeckoCollection<GeckoElement> GetElementsByTagName(string tagName)
		{
			if (tagName == null)
				throw new ArgumentNullException("tagName");

			if (tagName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("tagName");

			return nsString.Pass<nsIDOMNodeList>(Instance.GetElementsByTagName, tagName)
				.Wrap(GeckoCollection<GeckoElement>.Create);
		}

		public GeckoNode ImportNode(GeckoNode node, bool deep)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			return Instance.ImportNode(node.Instance, deep, 1).Wrap(GeckoNode.Create);
		}


		public GeckoElement CreateElementNS(string namespaceUri, string qualifiedName)
		{
			if (namespaceUri == null)
				throw new ArgumentNullException("namespaceUri");
			if (namespaceUri.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("namespaceUri");

			if (qualifiedName == null)
				throw new ArgumentNullException("qualifiedName");
			if (qualifiedName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("qualifiedName");

			return nsString.Pass<nsIDOMElement>(Instance.CreateElementNS, namespaceUri, qualifiedName).Wrap(GeckoElement.Create);
		}

		public GeckoAttribute CreateAttributeNS(string namespaceUri, string qualifiedName)
		{
			if (namespaceUri == null)
				throw new ArgumentNullException("namespaceUri");
			if (namespaceUri.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("namespaceUri");

			if (qualifiedName == null)
				throw new ArgumentNullException("qualifiedName");
			if (qualifiedName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("qualifiedName");

			return nsString.Pass<nsIDOMAttr>(Instance.CreateAttributeNS, namespaceUri, qualifiedName).Wrap(GeckoAttribute.Create);
		}

		//public DomEventArgs CreateEvent(string name)
		//{
		//	var target = nsString.Pass(Instance.CreateEvent, name);
		//	return target.Wrap(DomEventArgs.Create);
		//}


		/// <summary>
		/// Returns a collection containing all elements in the document with a given namespaceUri & localName.
		/// </summary>
		/// <returns></returns>
		public GeckoCollection<GeckoElement> GetElementsByTagNameNS(string namespaceUri, string localName)
		{
			if (namespaceUri == null)
				throw new ArgumentNullException("namespaceUri");
			if (namespaceUri.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("namespaceUri");

			if (localName == null)
				throw new ArgumentNullException("localName");
			if (localName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("localName");

			return nsString.Pass<nsIDOMNodeList>(Instance.GetElementsByTagNameNS, namespaceUri, localName)
				.Wrap(GeckoCollection<GeckoElement>.Create);
		}


		/// <summary>
		/// Searches for and returns the element in the document with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Found element or null if element does not exist</returns>
		public GeckoElement GetElementById(string id)
		{
			if (id == null)
				throw new ArgumentNullException("id");
			if (id.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("id");

			return nsString.Pass<nsIDOMElement>(Instance.GetElementById, id).Wrap(GeckoElement.Create);
		}

		public string InputEncoding
		{
			get { return nsString.Get(Instance.GetInputEncodingAttribute); }
		}

		public Uri Url
		{
			get
			{
				Uri uri;
				return System.Uri.TryCreate(nsString.Get(Instance.GetDocumentURIAttribute), UriKind.Absolute, out uri) ? uri : new Uri("about:blank");
			}
		}

		public string Uri
		{
			get
			{
				string uri = nsString.Get(Instance.GetDocumentURIAttribute);
				if(string.IsNullOrEmpty(uri))
					return "about:blank";
				return uri;
			}
		}

		///// <summary>
		///// Introduced in DOM Level 3:
		///// </summary>
		//[return: MarshalAs(UnmanagedType.Interface)]
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//nsIDOMNode AdoptNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode source);

		/// <summary>
		/// <see cref="http://html5.org/specs/dom-range.html#dom-document-createrange"/>
		/// </summary>
		/// <returns></returns>
		public GeckoRange CreateRange()
		{
			return Instance.CreateRange().Wrap(GeckoRange.Create);
		}

		//[return: MarshalAs(UnmanagedType.Interface)]
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//nsIDOMNodeIterator CreateNodeIterator([MarshalAs(UnmanagedType.Interface)] nsIDOMNode root, uint whatToShow, [MarshalAs(UnmanagedType.Interface)] nsIDOMNodeFilter filter, [MarshalAs(UnmanagedType.U1)] bool entityReferenceExpansion);

		//[return: MarshalAs(UnmanagedType.Interface)]
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//nsIDOMTreeWalker CreateTreeWalker([MarshalAs(UnmanagedType.Interface)] nsIDOMNode root, uint whatToShow, [MarshalAs(UnmanagedType.Interface)] nsIDOMNodeFilter filter, [MarshalAs(UnmanagedType.U1)] bool entityReferenceExpansion);

		//[return: MarshalAs(UnmanagedType.Interface)]
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//nsIDOMEvent CreateEvent([MarshalAs(UnmanagedType.LPStruct)] nsAStringBase eventType);

		/// <summary>
		/// The window associated with this document.
		/// <see cref="http://www.whatwg.org/html/#dom-document-defaultview"/>
		/// </summary>
		public GeckoWindow DefaultView
		{
			get { return Instance.GetDefaultViewAttribute().Wrap(GeckoWindow.Create); }
		}

		/// <summary>
		/// <see cref="http://www.whatwg.org/html/#dom-document-characterset"/>
		/// </summary>
		public string CharacterSet
		{
			get { return nsString.Get(Instance.GetCharacterSetAttribute); }
		}

		/// <summary>
		/// <see cref="http://www.whatwg.org/html/#dom-document-dir"/>
		/// </summary>
		public string Dir
		{
			get { return nsString.Get(Instance.GetDirAttribute); }
			set { nsString.Set(Instance.SetDirAttribute, value); }
		}


		/// <summary>
		/// @see <http://www.whatwg.org/html/#dom-document-location>
		/// </summary>
		public GeckoLocation Location
		{
			get { return Instance.GetLocationAttribute().Wrap(GeckoLocation.Create); }
		}

		/// <summary>
		/// Gets the document title.
		/// </summary>
		public string Title
		{
			get { return nsString.Get(Instance.GetTitleAttribute); }
			set { nsString.Set(Instance.SetTitleAttribute, value); }
		}


		/// <summary>
		/// <see cref="http://www.whatwg.org/html/#dom-document-readystate"/>
		/// </summary>
		public string ReadyState
		{
			get { return nsString.Get(Instance.GetReadyStateAttribute); }
		}

		/// <summary>
		/// Returns a string containing the date and time on which the current document was last modified.
		/// <see cref="https://developer.mozilla.org/en-US/docs/Web/API/document.lastModified"/>
		/// </summary>
		public string LastModified
		{
			get { return nsString.Get(Instance.GetLastModifiedAttribute); }
		}

		/// <summary>
		/// Returns the URI of the page that linked to this page.
		/// <see cref="https://developer.mozilla.org/en-US/docs/Web/API/document.referrer"/>
		/// </summary>
		public string Referrer
		{
			get
			{
 				string uri = nsString.Get(Instance.GetReferrerAttribute);
				if (string.IsNullOrEmpty(uri))
					return "about:blank";
				return uri;
			}
		}

		/// <summary>
		/// <see cref="http://www.whatwg.org/html/#dom-document-hasfocus"/>
		/// </summary>
		public bool HasFocus()
		{
			return Instance.HasFocus();
		}

		/// <summary>
		/// Gets the currently focused element.
		/// </summary>
		public GeckoElement ActiveElement
		{
			get { return Instance.GetActiveElementAttribute().Wrap(GeckoElement.Create); }
		}

		/// <summary>
		/// Returns a set of elements with the given class name. When called on the document object, the complete document is searched, including the root node.
		/// </summary>
		/// <param name="classes"></param>
		/// <returns></returns>
		public GeckoCollection<GeckoElement> GetElementsByClassName(string className)
		{
			if (className == null)
				throw new ArgumentNullException("className");
			if (className.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("className");

			return nsString.Pass<nsIDOMNodeList>(Instance.GetElementsByClassName, className)
				.Wrap(GeckoCollection<GeckoElement>.Create);
		}

		///// <summary>
		///// @see <http://dev.w3.org/csswg/cssom/#dom-document-stylesheets>
		///// </summary>
		//[return: MarshalAs(UnmanagedType.Interface)]
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//nsIDOMStyleSheetList GetStyleSheetsAttribute();

		/// <summary>
		/// <see cref="http://dev.w3.org/csswg/cssom/#dom-document-preferredStyleSheetSet"/>
		/// </summary>
		public string PreferredStyleSheetSet
		{
			get { return nsString.Get(Instance.GetPreferredStyleSheetSetAttribute); }
		}

		/// <summary>
		/// <see cref="http://dev.w3.org/csswg/cssom/#dom-document-selectedStyleSheetSet"/>
		/// </summary>
		public string SelectedStyleSheetSet
		{
			get { return nsString.Get(Instance.GetSelectedStyleSheetSetAttribute); }
			set { nsString.Set(Instance.SetSelectedStyleSheetSetAttribute, value); }
		}

		/// <summary>
		/// <see cref="http://dev.w3.org/csswg/cssom/#dom-document-lastStyleSheetSet"/>
		/// </summary>
		public string LastStyleSheetSet
		{
			get { return nsString.Get(Instance.GetLastStyleSheetSetAttribute); }
		}

		/// <summary>
		/// Gets a list of stylesheets explicitly linked into or embedded in a document.
		/// </summary>
		public GeckoStyleSheetList StyleSheets
		{
			get { return Instance.GetStyleSheetsAttribute().Wrap(GeckoStyleSheetList.Create); }
		}

		///// <summary>
		///// This must return the live list of the currently available style sheet
		///// sets. This list is constructed by enumerating all the style sheets for
		///// this document available to the implementation, in the order they are
		///// listed in the styleSheets attribute, adding the title of each style sheet
		///// with a title to the list, avoiding duplicates by dropping titles that
		///// match (case-sensitively) titles that have already been added to the
		///// list.
		/////
		///// @see <http://dev.w3.org/csswg/cssom/#dom-document-styleSheetSets>
		///// </summary>
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//System.IntPtr GetStyleSheetSetsAttribute();

		/// <summary>
		/// <see cref="http://dev.w3.org/csswg/cssom/#dom-document-enableStyleSheetsForSet"/>
		/// </summary>
		/// <param name="name"></param>
		public void EnableStyleSheetsForSet(string name)
		{
			nsString.Set(Instance.EnableStyleSheetsForSet, name);
		}

		/// <summary>
		/// Returns the element visible at the given point, relative to the upper-left-most visible point in the document.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public GeckoElement ElementFromPoint(float x, float y)
		{
			return Instance.ElementFromPoint(x, y).Wrap(GeckoElement.Create);
		}

		public string ContentType
		{
			get { return nsString.Get(Instance.GetContentTypeAttribute); }
		}

		/// <summary>
		/// True if this document is synthetic : stand alone image, video, audio file,
		/// etc.
		/// </summary>
		public bool MozSyntheticDocument
		{
			get { return Instance.GetMozSyntheticDocumentAttribute(); }
		}

		/// <summary>
		/// Returns the script element whose script is currently being processed.
		/// <see cref="https://developer.mozilla.org/en/DOM/document.currentScript"/>
		/// </summary>
		public GeckoElement CurrentScript
		{
			get { return Instance.GetCurrentScriptAttribute().Wrap(GeckoElement.Create); }
		}

		/// <summary>
		/// <see cref="https://developer.mozilla.org/en/DOM/document.releaseCapture"/>
		/// </summary>
		public void ReleaseCapture()
		{
			Instance.ReleaseCapture();
		}

		/// <summary>
		/// Causes the document to leave DOM full-screen mode, if it's in
		/// full-screen mode, as per the DOM full-screen api.
		/// <see cref="https://wiki.mozilla.org/index.php?title=Gecko:FullScreenAPI"/>
		/// </summary>
		public void MozCancelFullScreen()
		{
			Instance.MozCancelFullScreen();
		}

		/// <summary>
		/// Denotes whether this document is in DOM full-screen mode, as per the DOM
		/// full-screen api.
		/// <see cref="https://wiki.mozilla.org/index.php?title=Gecko:FullScreenAPI"/>
		/// </summary>
		public bool MozFullScreen
		{
			get { return Instance.GetMozFullScreenAttribute(); }
		}

		/// <summary>
		/// Denotes whether the full-screen-api.enabled is true, no windowed
		/// plugins are present, and all ancestor documents have the
		/// mozallowfullscreen attribute set.
		/// <see cref="https://wiki.mozilla.org/index.php?title=Gecko:FullScreenAPI"/>
		/// </summary>
		public bool MozFullScreenEnabled
		{
			get { return Instance.GetMozFullScreenEnabledAttribute(); }
		}

		/// <summary>
		/// Visibility API implementation.
		/// </summary>
		public bool Hidden
		{
			get { return Instance.GetHiddenAttribute(); }
		}

		/// <summary>
		///  Gets the visibility of the document, that is in which context this element
		///  is now visible. It is useful to know if the document is in the background or
		///  an invisible tab, or only loaded for pre-rendering.
		///  <see cref="https://developer.mozilla.org/ru/docs/Web/API/Document/visibilityState"/>
		/// </summary>
		public string VisibilityState
		{
			get { return nsString.Get(Instance.GetVisibilityStateAttribute); }
		}

		/// <summary>
		/// Returns the first element within the document (using depth-first pre-order traversal of the document's nodes) that matches the specified group of selectors.
		/// </summary>
		/// <param name="selector">A string containing one or more CSS selectors separated by commas.</param>
		/// <returns></returns>
		public GeckoElement QuerySelector(string selector)
		{
			if (selector == null)
				selector = "null";
			return nsString.Pass(Instance.QuerySelector, selector).Wrap(GeckoElement.Create);
		}

		/// <summary>
		/// Returns a list of the elements within the document (using depth-first pre-order traversal of the document's nodes) that match the specified group of selectors.
		/// </summary>
		/// <param name="selector">A string containing one or more CSS selectors separated by commas.</param>
		/// <returns></returns>
		public GeckoNodeCollection QuerySelectorAll(string selector)
		{
			if (selector == null)
				selector = "null";
			return nsString.Pass(Instance.QuerySelectorAll, selector).Wrap(GeckoNodeCollection.Create);
		}

		/// <summary>
		/// Create the TreeWalker object.
		/// </summary>
		/// <param name="root">Is the root Node of this TreeWalker traversal. Typically this will be an element owned by the document.</param>
		/// <param name="whatToShow">The bitmask that describing way of filtering for certain types of node.</param>
		/// <param name="filter">Is an optional NodeFilter, that is an object with a method acceptNode, which is called by the TreeWalker to determine whether or not to accept a node that has passed the whatToShow check.</param>
		/// <returns>Returns a newly created TreeWalker object.</returns>
		public GeckoTreeWalker CreateTreeWalker(GeckoNode root, NodeFilterFlags whatToShow, nsIDOMNodeFilter filter)
		{
			if (root == null)
				throw new ArgumentNullException("root");

			return Instance.CreateTreeWalker(root.Instance, (uint)whatToShow, filter, 3).Wrap(GeckoTreeWalker.Create);
		}

		/// <summary>
		/// Returns an XPathResult based on an XPath expression and other given parameters.
		/// </summary>
		/// <param name="xpathExpression">A string representing the XPath to be evaluated.</param>
		/// <param name="contextNode">The contextNode specifies the context node for the query (see the [http://www.w3.org/TR/xpath XPath specification). It's common to pass document as the context node.</param>
		/// <param name="resultType">The type of result XPathResult to return.</param>
		/// <returns></returns>
		public GeckoXPathResult Evaluate(string xpathExpression, GeckoNode contextNode, GeckoXPathResultType resultType)
		{
			if (xpathExpression == null)
				throw new ArgumentNullException("xpathExpression");
			if (contextNode == null)
				throw new ArgumentNullException("contextNode");

			nsISupports resolver = null;
			nsIXulfxDOMXPathEvaluator evaluator = null;
			nsAString expression = new nsAString(xpathExpression);
			try
			{
				evaluator = Xpcom.CreateInstance<nsIXulfxDOMXPathEvaluator>("@xulfx/xpath/evaluator;1");
				evaluator.Init(this.Instance);
				//resolver = evaluator.CreateNSResolver(contextNode.Instance);
				resolver = contextNode.QueryInterface<nsISupports>();
				return new GeckoXPathResult(evaluator.Evaluate(expression, contextNode.Instance, resolver, (ushort)resultType));
			}
			finally
			{
				expression.Dispose();
				Xpcom.FreeComObject(ref resolver);
				Xpcom.FreeComObject(ref evaluator);
			}
		}

		/// <summary>
		/// Retrieves an anonymous descendant with a specified attribute value.
		/// Typically used with an (arbitrary) anonid attribute to retrieve a specific anonymous child in an XBL binding.
		/// </summary>
		/// <param name="element">The element to retrieve anonymous children for.</param>
		/// <param name="attrName">The attribute name to look up.</param>
		/// <param name="attrValue">The attribute value to match.</param>
		/// <returns>Returns an anonymous descendant of the given element with matching attribute name and value.</returns>
		public GeckoElement GetAnonymousElementByAttribute(GeckoElement element, string attrName, string attrValue)
		{
			if (element == null)
				throw new ArgumentNullException("element");

			nsIDOMDocumentXBL xbl = this.QueryInterface<nsIDOMDocumentXBL>();
			if (xbl == null)
				throw new InvalidCastException();

			nsAString aAttrName = null;
			nsAString aAttrValue = null;
			try
			{
				aAttrName = new nsAString(attrName);
				aAttrValue = new nsAString(attrValue);
				return xbl.GetAnonymousElementByAttribute(element.Instance, aAttrName, aAttrValue).Wrap(GeckoElement.Create);
			}
			finally
			{
				if(aAttrName != null) aAttrName.Dispose();
				if(aAttrValue != null) aAttrValue.Dispose();
				Xpcom.FreeComObject(ref xbl);
			}
		}

		/// <summary>
		/// Retrieves the anonymous children of the specified element.
		/// </summary>
		/// <param name="element">The element to retrieve anonymous children for.</param>
		/// <returns>
		/// Returns a NodeList that represents the children of an element after insertion points from its
		/// own binding have been applied. This means that, depending on the details regarding the insertion
		/// points of the binding, it&apos;s possible that some non-anonymous nodes appear in the list.
		/// </returns>
		public GeckoNodeCollection GetAnonymousNodes(GeckoElement element)
		{
			if (element == null)
				throw new ArgumentNullException("element");

			nsIDOMDocumentXBL xbl = this.QueryInterface<nsIDOMDocumentXBL>();
			if (xbl == null)
				throw new InvalidCastException();

			try
			{
				return xbl.GetAnonymousNodes(element.Instance).Wrap(GeckoNodeCollection.Create);
			}
			finally
			{
				Xpcom.FreeComObject(ref xbl);
			}
		}

		/// <summary>
		/// Obtains the bound element with the binding attached that is responsible for the generation
		/// of the specified anonymous node. This method enables an author to determine the scope of
		/// any content node. When content at the document-level scope is passed in as an argument,
		/// the property&apos;s value is null.
		/// </summary>
		/// <param name="anonymousNode">The node for which the bound element responsible for generation is desired.</param>
		/// <returns>Returns the element responsible for the given anonymous node.</returns>
		public GeckoElement GetBindingParent(GeckoNode anonymousNode)
		{
			if (anonymousNode == null)
				throw new ArgumentNullException("anonymousNode");

			nsIDOMDocumentXBL xbl = this.QueryInterface<nsIDOMDocumentXBL>();
			if (xbl == null)
				throw new InvalidCastException();

			try
			{
				return xbl.GetBindingParent(anonymousNode.Instance).Wrap(GeckoElement.Create);
			}
			finally
			{
				Xpcom.FreeComObject(ref xbl);
			}
		}


	}
}
