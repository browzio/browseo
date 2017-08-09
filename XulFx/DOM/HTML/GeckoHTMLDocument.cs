using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interfaces;
using Gecko.DOM.Abstractions;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLDocument : GeckoDocument<nsIDOMHTMLDocument>
	{
		public static GeckoHTMLDocument Create(nsIDOMHTMLDocument document)
		{
			return new GeckoHTMLDocument(document);
		}

		private GeckoHTMLDocument(nsIDOMHTMLDocument document)
			: base(document)
		{

		}

		/// <summary>
		/// Gets the HTML head element.
		/// </summary>
		public GeckoHTMLHeadElement Head
		{
			get { return Instance.GetHeadAttribute().Wrap(GeckoHTMLHeadElement.Create); }
		}

		/// <summary>
		/// Gets the HTML body element.
		/// </summary>
		public GeckoHTMLElement Body
		{
			get { return Instance.GetBodyAttribute().Wrap(GeckoHTMLElement.Create); }
		}

		///// <summary>
		///// Represents a collection of style sheets in a <see cref="GeckoDocument"/>.
		///// </summary>
		//public class StyleSheetCollection : IEnumerable<GeckoStyleSheet>
		//{
		//	internal StyleSheetCollection(GeckoDocument document)
		//	{
		//		this.List = document._domHtmlDocument.GetStyleSheetsAttribute();
		//	}
		//	nsIDOMStyleSheetList List;

		//	/// <summary>
		//	/// Gets the number of items in the collection.
		//	/// </summary>
		//	public int Count
		//	{
		//		get { return (List == null) ? 0 : (int)List.GetLengthAttribute(); }
		//	}

		//	/// <summary>
		//	/// Gets the item at the specified index in the collection.
		//	/// </summary>
		//	/// <param name="index"></param>
		//	/// <returns></returns>
		//	public GeckoStyleSheet this[int index]
		//	{
		//		get
		//		{
		//			if (index < 0 || index >= Count)
		//				throw new ArgumentOutOfRangeException("index");

		//			return GeckoStyleSheet.Create((nsIDOMCSSStyleSheet)List.Item((uint)index));
		//		}
		//	}

		//	#region IEnumerable<GeckoStyleSheet> Members

		//	/// <summary>
		//	/// Returns an <see cref="IEnumerator{GeckoStyleSheet}"/> which can enumerate through the collection.
		//	/// </summary>
		//	/// <returns></returns>
		//	public IEnumerator<GeckoStyleSheet> GetEnumerator()
		//	{
		//		int length = Count;
		//		for (int i = 0; i < length; i++)
		//		{
		//			yield return GeckoStyleSheet.Create((nsIDOMCSSStyleSheet)List.Item((uint)i));
		//		}
		//	}

		//	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		//	{
		//		foreach (GeckoStyleSheet element in this)
		//			yield return element;
		//	}

		//	#endregion
		//}

		///// <summary>
		///// Gets the collection of style sheets in the <see cref="GeckoDocument"/>.
		///// </summary>
		//public StyleSheetCollection StyleSheets
		//{
		//	get { return (_StyleSheets == null) ? (_StyleSheets = new StyleSheetCollection(this)) : _StyleSheets; }
		//}
		//StyleSheetCollection _StyleSheets;

		public GeckoHTMLCollection Forms
		{
			get { return Instance.GetFormsAttribute().Wrap(GeckoHTMLCollection.Create); }
		}

		public GeckoHTMLCollection Images
		{
			get { return Instance.GetImagesAttribute().Wrap(GeckoHTMLCollection.Create); }
		}

		public GeckoHTMLCollection Anchors
		{
			get { return Instance.GetAnchorsAttribute().Wrap(GeckoHTMLCollection.Create); }
		}

		public GeckoHTMLCollection Applets
		{
			get { return Instance.GetAppletsAttribute().Wrap(GeckoHTMLCollection.Create); }
		}

		public GeckoHTMLCollection Links
		{
			get { return Instance.GetLinksAttribute().Wrap(GeckoHTMLCollection.Create); }
		}

		public string Cookie
		{
			get { return nsString.Get(Instance.GetCookieAttribute); }
			set { nsString.Set(Instance.SetCookieAttribute, value); }
		}

		public string Domain
		{
			get { return nsString.Get(Instance.GetDomainAttribute); }
		}



		/// <summary>
		/// Returns a collection containing all elements in the document with a given name.		
		/// </summary>
		/// <param name="name">This is NOT the tagname but the name attribute.</param>
		/// <returns></returns>
		public GeckoNodeCollection GetElementsByName(string name)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("name");

			return Instance.GetElementsByName(new nsAString(name)).Wrap(GeckoNodeCollection.Create);
		}

		/// <summary>
		/// Creates the specified HTML element.
		/// </summary>
		/// <param name="tagName">a string that specifies the type of element to be created</param>
		public new GeckoHTMLElement CreateElement(string tagName)
		{
			return (GeckoHTMLElement)base.CreateElement(tagName);
		}

		/// <summary>
		/// Returns a collection containing all elements in the document with a given tag name.
		/// </summary>
		/// <param name="tagName"></param>
		/// <returns></returns>
		public new GeckoCollection<GeckoHTMLElement> GetElementsByTagName(string tagName)
		{
			if (tagName == null)
				throw new ArgumentNullException("tagName");

			if (tagName.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("tagName");

			return nsString.Pass<nsIDOMNodeList>(Instance.GetElementsByTagName, tagName)
				.Wrap(GeckoCollection<GeckoHTMLElement>.Create);
		}
	}
}
