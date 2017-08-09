using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.DOM
{
	public class GeckoElement : GeckoNode<nsIDOMElement>
	{
		private string _tagName;

		public static GeckoElement Create(nsIDOMElement element)
		{
			bool found = true;
			GeckoElement wrapper;
			try
			{
				if (TryCast<nsIDOMHTMLElement, GeckoHTMLElement>(element, GeckoHTMLElement.Create, out wrapper))
					return wrapper;

				if (TryCast<nsIDOMSVGElement, GeckoSVGElement>(element, GeckoSVGElement.Create, out wrapper))
					return wrapper;

				if (TryCast<nsIDOMXULElement, GeckoXULElement>(element, GeckoXULElement.Create, out wrapper))
					return wrapper;

				found = false;
			}
			finally
			{
				if(found) Xpcom.FreeComObject(ref element);
			}
			return new GeckoElement(element);
		}

		private static bool TryCast<TInterface, TWrapper>(nsIDOMElement instance, Func<TInterface, TWrapper> create, out GeckoElement wrapper)
			where TInterface : class
			where TWrapper : GeckoElement
		{
			var obj = Xpcom.QueryInterface<TInterface>(instance);
			if (obj != null)
			{
				wrapper = create(obj);
				return true;
			}
			wrapper = null;
			return false;
		}

		protected GeckoElement(nsIDOMElement domElement)
			: base(domElement)
		{

		}

		/// <summary>
		/// Gets the name of the tag.
		/// </summary>
		public string TagName
		{
			get
			{
				if (_tagName == null)
					_tagName = nsString.Get(Instance.GetTagNameAttribute);
				return _tagName; 
			}
		}

		#region Attribute

		public GeckoMozNamedAttrMap Attributes
		{
			get { return Instance.GetAttributesAttribute().Wrap(GeckoMozNamedAttrMap.Create); }
		}

		/// <summary>
		/// Gets the value of an attribute on this element with the specified name.
		/// </summary>
		/// <param name="attributeName"></param>
		/// <returns></returns>
		public string GetAttribute(string attributeName)
		{
			if (attributeName == null)
				throw new ArgumentNullException("attributeName");
			if (attributeName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("attributeName");

			return nsString.Get(Instance.GetAttribute, attributeName);
		}

		/// <summary>
		/// Check if Element contains specified attribute.
		/// </summary>
		/// <param name="attributeName">The name of the attribute to look for</param>
		/// <returns>true if attribute exists false otherwise</returns>
		public bool HasAttribute(string attributeName)
		{
			if (attributeName == null)
				throw new ArgumentNullException("attributeName");
			if (attributeName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("attributeName");

			return nsString.Pass<bool>(Instance.HasAttribute, attributeName);
		}

		/// <summary>
		/// Sets the value of an attribute on this element with the specified name.
		/// </summary>
		/// <param name="attributeName"></param>
		/// <param name="value"></param>
		public void SetAttribute(string attributeName, string value)
		{
			if (attributeName == null)
				throw new ArgumentNullException("attributeName");
			if (attributeName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("attributeName");

			nsString.Set(Instance.SetAttribute, attributeName, value);
		}

		/// <summary>
		/// Removes an attribute from this element.
		/// </summary>
		/// <param name="attributeName"></param>
		public void RemoveAttribute(string attributeName)
		{
			if (attributeName == null)
				throw new ArgumentNullException("attributeName");
			if (attributeName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("attributeName");

			nsString.Set(Instance.RemoveAttribute, attributeName);
		}
		#endregion

		#region Attribute Nodes

		public GeckoAttribute GetAttributeNode(string name)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("name");

			return nsString.Pass<nsIDOMAttr>(Instance.GetAttributeNode, name).Wrap(GeckoAttribute.Create);
		}

		public GeckoAttribute SetAttributeNode(GeckoAttribute newAttr)
		{
			if (newAttr == null)
				throw new ArgumentNullException("newAttr");

			return Instance.SetAttributeNode(newAttr.Instance).Wrap(GeckoAttribute.Create);
		}

		public GeckoAttribute RemoveAttributeNode(GeckoAttribute newAttr)
		{
			if (newAttr == null)
				throw new ArgumentNullException("newAttr");

			return Instance.RemoveAttributeNode(newAttr.Instance).Wrap(GeckoAttribute.Create);
		}

		#endregion



		#region Attribute NS

		public bool HasAttributeNS(string namespaceUri, string attributeName)
		{
			if (namespaceUri == null)
				throw new ArgumentNullException("namespaceUri");
			if (namespaceUri.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("namespaceUri");

			if (attributeName == null)
				throw new ArgumentNullException("attributeName");
			if (attributeName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("attributeName");

			return nsString.Pass<bool>(Instance.HasAttributeNS, namespaceUri, attributeName);
		}

		/// <summary>
		/// Gets the value of an attribute on this element with the specified name and namespace.
		/// </summary>
		/// <param name="attributeName"></param>
		/// <returns></returns>
		public string GetAttributeNS(string namespaceUri, string attributeName)
		{
			if (namespaceUri == null)
				throw new ArgumentNullException("namespaceUri");
			if (namespaceUri.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("namespaceUri");

			if (attributeName == null)
				throw new ArgumentNullException("attributeName");
			if (attributeName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("attributeName");

			using (nsAString retval = new nsAString(), nsUri = new nsAString(namespaceUri), aName = new nsAString(attributeName))
			{
				Instance.GetAttributeNS(nsUri, aName, retval);
				return retval.ToString();
			}
		}

		/// <summary>
		/// Sets the value of an attribute on this element with the specified name and namespace.
		/// </summary>
		/// <param name="attributeName"></param>
		/// <param name="value"></param>
		public void SetAttributeNS(string namespaceUri, string attributeName, string value)
		{
			if (namespaceUri == null)
				throw new ArgumentNullException("namespaceUri");
			if (namespaceUri.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("namespaceUri");

			if (attributeName == null)
				throw new ArgumentNullException("attributeName");
			if (attributeName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("attributeName");

			using (nsAString nsUri = new nsAString(namespaceUri), aName = new nsAString(attributeName), aValue = new nsAString(value))
			{
				Instance.SetAttributeNS(nsUri, aName, aValue);
			}
		}
		
		#endregion

		#region Attribute Node NS

		public GeckoAttribute GetAttributeNodeNS(string namespaceUri, string localName)
		{
			if (namespaceUri == null)
				throw new ArgumentNullException("namespaceUri");
			if (namespaceUri.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("namespaceUri");

			if (localName == null)
				throw new ArgumentNullException("localName");
			if (localName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("localName");

			return nsString.Pass<nsIDOMAttr>(Instance.GetAttributeNodeNS, namespaceUri, localName).Wrap(GeckoAttribute.Create);
		}

		public GeckoAttribute SetAttributeNodeNS(GeckoAttribute attribute)
		{
			if (attribute == null)
				throw new ArgumentNullException("attribute");

			return Instance.SetAttributeNodeNS(attribute.Instance).Wrap(GeckoAttribute.Create);
		}

		#endregion


		public int ScrollLeft
		{
			get { return Instance.GetScrollLeftAttribute(); }
			set { Instance.SetScrollLeftAttribute(value); }
		}

		public int ScrollTop
		{
			get { return Instance.GetScrollTopAttribute(); }
			set { Instance.SetScrollTopAttribute(value); }
		}

		public int ScrollWidth
		{
			get { return Instance.GetScrollWidthAttribute(); }
		}

		public int ScrollHeight
		{
			get { return Instance.GetScrollHeightAttribute(); }
		}

		public int ClientLeft
		{
			get { return Instance.GetClientLeftAttribute(); }
		}

		public int ClientTop
		{
			get { return Instance.GetClientTopAttribute(); }
		}

		public int ClientWidth
		{
			get { return Instance.GetClientWidthAttribute(); }
		}

		public int ClientHeight
		{
			get { return Instance.GetClientHeightAttribute(); }
		}

		public GeckoElement ParentElement
		{
			get { return Instance.GetParentElementAttribute().Wrap(GeckoElement.Create); }
		}

		public GeckoNodeCollection Children
		{
			get { return Instance.GetChildrenAttribute().Wrap(GeckoNodeCollection.Create); }
		}

		public int ChildElementCount
		{
			get { return (int)Instance.GetChildElementCountAttribute(); }
		}

		public bool HasAttributes
		{
			get { return Instance.HasAttributes(); }
		}

		/// <summary>
		/// Gets style of the GeckoElement. 
		/// </summary>
		public GeckoCSSStyleDeclaration ComputedStyle
		{
			get
			{
				GeckoDocument document = this.OwnerDocument;
				if (document != null)
				{
					GeckoWindow window = document.DefaultView;
					if (window != null)
					{
						return window.GetComputedStyle(this, null);
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Returns a collection containing the child elements of this element with a given tag name.
		/// </summary>
		/// <param name="tagName"></param>
		/// <returns></returns>
		public GeckoHTMLCollection GetElementsByTagName(string tagName)
		{
			if (tagName == null)
				throw new ArgumentNullException("tagName");

			if (tagName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("tagName");

			return nsString.Pass<nsIDOMHTMLCollection>(Instance.GetElementsByTagName, tagName).Wrap(GeckoHTMLCollection.Create);
		}

		//public ICollection<GeckoElement> GetElementsByTagNameNS(string namespaceURI, string localName)
		//{
		//	if (string.IsNullOrEmpty(namespaceURI)) return GetElementsByTagName(localName);

		//	if (string.IsNullOrEmpty(localName))
		//		return null;


		//	//var ret = nsString.Pass<nsIDOMHTMLCollection>(_domElement.GetElementsByTagNameNS, namespaceURI, localName);
		//	//return ret == null ? null : new GeckoHtmlElementCollection(ret);
		//	return nsString.Pass<nsIDOMHTMLCollection>(Instance.GetElementsByTagNameNS, namespaceURI, localName)
		//				   .Wrap(x => new DomHtmlCollection<GeckoElement, nsIDOMHTMLElement>(x, CreateDomElementWrapper));
		//}

		/// <summary>
		/// Retrieve elements matching all classes listed in a space-separated string.
		/// </summary>
		/// <param name="tagName"></param>
		/// <returns></returns>
		public GeckoHTMLCollection GetElementsByClassName(string className)
		{
			if (className == null)
				throw new ArgumentNullException("className");

			if (className.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("className");

			return nsString.Pass<nsIDOMHTMLCollection>(Instance.GetElementsByClassName, className).Wrap(GeckoHTMLCollection.Create);
		}

		public GeckoElement QuerySelector(string selector)
		{
			if (selector == null)
				selector = "null";
			return nsString.Pass(Instance.QuerySelector, selector).Wrap(GeckoElement.Create);
		}

		public GeckoNodeCollection QuerySelectorAll(string selector)
		{
			if (selector == null)
				selector = "null";
			return nsString.Pass(Instance.QuerySelectorAll, selector).Wrap(GeckoNodeCollection.Create);
		}

		public bool Matches(string selector)
		{
			if (selector == null)
				selector = "null";
			return nsString.Pass(Instance.MozMatchesSelector, selector);
		}

	}
}
