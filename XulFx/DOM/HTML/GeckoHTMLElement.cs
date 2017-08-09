using Gecko.DOM.Abstractions;
using Gecko.DOM.HTML;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	public class GeckoHTMLElement : GeckoElement<nsIDOMHTMLElement>
	{
		public static GeckoHTMLElement Create(nsIDOMHTMLElement element)
		{
			GeckoHTMLElement wrapper = DOMHTMLElementFactory.TryCreate(element);
			if (wrapper != null)
			{
				return wrapper;
			}
			return new GeckoHTMLElement(element);
		}

		protected GeckoHTMLElement(nsIDOMHTMLElement element)
			: base(element)
		{

		}

		/// <summary>
		/// Gets the parent element of this one.
		/// </summary>
		public GeckoElement Parent
		{
			get
			{
				// note: the parent node could also be the document
				return Instance.GetParentElementAttribute().Wrap(GeckoElement.Create);
			}
		}



		/// <summary>
		/// Gets the value of the id attribute.
		/// </summary>
		public virtual string Id
		{
			get { return nsString.Get(Instance.GetIdAttribute); }
			set
			{
				if (string.IsNullOrEmpty(value))
					this.RemoveAttribute("id");
				else
					nsString.Set(Instance.SetIdAttribute, value);
			}
		}

		/// <summary>
		/// Gets the value of the class attribute.
		/// </summary>
		public virtual string ClassName
		{
			get { return nsString.Get(Instance.GetClassNameAttribute); }
			set
			{
				if (string.IsNullOrEmpty(value))
					this.RemoveAttribute("class");
				else
					nsString.Set(Instance.SetClassNameAttribute, value);
			}
		}

		public virtual void Blur()
		{
			Instance.Blur();
		}

		public virtual string AccessKey
		{
			get { return nsString.Get(Instance.GetAccessKeyAttribute); }
			set { nsString.Set(Instance.SetAccessKeyAttribute, value); }
		}

		public virtual void Focus()
		{
			Instance.Focus();
		}

		public virtual void Click()
		{
			Instance.Click();
		}

		public virtual bool Draggable
		{
			get { return Instance.GetDraggableAttribute(); }
			set { Instance.SetDraggableAttribute(value); }
		}

		/// <summary>
		/// Get the value of the ContentEditable Attribute
		/// </summary>
		public virtual string ContentEditable
		{
			get { return nsString.Get(Instance.GetContentEditableAttribute); }
			set { nsString.Set(Instance.GetContentEditableAttribute, value); }
		}

		//public System.Drawing.Rectangle[] ClientRects
		//{
		//	get
		//	{
		//		nsIDOMClientRectList domRects = DOMHtmlElement.GetClientRects();
		//		uint count = domRects.GetLengthAttribute();
		//		Rectangle[] rects = new Rectangle[count];
		//		for (uint i = 0; i < count; i++)
		//		{
		//			nsIDOMClientRect domRect = domRects.Item(i);
		//			rects[i] = new Rectangle(
		//				(int)Math.Round(domRect.GetLeftAttribute()),
		//				(int)Math.Round(domRect.GetTopAttribute()),
		//				(int)Math.Round(domRect.GetWidthAttribute()),
		//				(int)Math.Round(domRect.GetHeightAttribute()));
		//		}
		//		return rects;
		//	}
		//}

		public virtual int OffsetLeft { get { return Instance.GetOffsetLeftAttribute(); } }
		public virtual int OffsetTop { get { return Instance.GetOffsetTopAttribute(); } }
		public virtual int OffsetWidth { get { return Instance.GetOffsetWidthAttribute(); } }
		public virtual int OffsetHeight { get { return Instance.GetOffsetHeightAttribute(); } }


		public GeckoElement OffsetParent
		{
			get { return Instance.GetOffsetParentAttribute().Wrap(GeckoElement.Create); }
		}

		public virtual void ScrollIntoView(bool top)
		{
			Instance.ScrollIntoView(top, 1);
		}


		public virtual bool Spellcheck
		{
			get { return Instance.GetSpellcheckAttribute(); }
			set { Instance.SetSpellcheckAttribute(value); }
		}

		public virtual string InnerHtml
		{
			get { return nsString.Get(Instance.GetInnerHTMLAttribute); }
			set { nsString.Set(Instance.SetInnerHTMLAttribute, value); }
		}

		public virtual string OuterHtml
		{
			get { return nsString.Get(Instance.GetOuterHTMLAttribute); }
		}

		public virtual int TabIndex
		{
			get { return Instance.GetTabIndexAttribute(); }
			set { Instance.SetTabIndexAttribute(value); }
		}

		public virtual void InsertAdjacentHTML(string position, string text)
		{
			using (nsAString tempPos = new nsAString(position), tempText = new nsAString(text))
			{
				Instance.InsertAdjacentHTML(tempPos, tempText);
			}
		}

		public virtual void MozRequestFullScreen()
		{
			Instance.MozRequestFullScreen();
		}
	}
}
