using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gecko.DOM.HTML
{
    internal static class DOMHTMLElementFactory
    {
		private delegate GeckoHTMLElement CreationDelegate<T>(T comObject)
			where T : class; // nsIDOMHTMLElement;

		private interface IWrapperDesc
		{
			string TagName { get; }
			GeckoHTMLElement Create(nsIDOMHTMLElement element);
		}

		private struct WrapperDesc<T> : IWrapperDesc
			where T : class // nsIDOMHTMLElement
		{
			private string _tagName;
			public CreationDelegate<T> _creationMethod;

			public WrapperDesc(string tagName, CreationDelegate<T> creationMethod)
			{
				_tagName = tagName;
				_creationMethod = creationMethod;
			}

			public string TagName
			{
				get { return _tagName; }
			}

			public GeckoHTMLElement Create(nsIDOMHTMLElement element)
			{
				return _creationMethod((T)element);
			}
		}

		private static readonly Dictionary<string, IWrapperDesc> _descriptors;

		static DOMHTMLElementFactory()
		{
			_descriptors = new Dictionary<string, IWrapperDesc>(new TagNameComparer());

			Add(new WrapperDesc<nsIDOMHTMLAnchorElement>("A", GeckoHTMLAnchorElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLAppletElement>("APPLET", GeckoHTMLAppletElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLAreaElement>("AREA", GeckoHTMLAreaElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLMediaElement>("AUDIO", GeckoHTMLAudioElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLBaseElement>("BASE", GeckoHTMLBaseElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLBodyElement>("BODY", GeckoHTMLBodyElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLButtonElement>("BUTTON", GeckoHTMLButtonElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLCanvasElement>("CANVAS", GeckoHTMLCanvasElement.Create));			
			Add(new WrapperDesc<nsIDOMHTMLDirectoryElement>("DIRECTORY", GeckoHTMLDirectoryElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLEmbedElement>("EMBED", GeckoHTMLEmbedElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLFieldSetElement>("FIELDSET", GeckoHTMLFieldSetElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLFormElement>("FORM", GeckoHTMLFormElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLFrameElement>("FRAME", GeckoHTMLFrameElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLFrameSetElement>("FRAMESET", GeckoHTMLFrameSetElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLHeadElement>("HEAD", GeckoHTMLHeadElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLHRElement>("HR", GeckoHTMLHRElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLHtmlElement>("HTML", GeckoHTMLHtmlElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLIFrameElement>("IFRAME", GeckoHTMLIFrameElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLImageElement>("IMG", GeckoHTMLImageElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLInputElement>("INPUT", GeckoHTMLInputElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLLabelElement>("LABEL", GeckoHTMLLabelElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLLIElement>("LI", GeckoHTMLLIElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLLinkElement>("LINK", GeckoHTMLLinkElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLMapElement>("MAP", GeckoHTMLMapElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLMenuElement>("MENU", GeckoHTMLMenuElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLMenuItemElement>("MENUITEM", GeckoHTMLMenuItemElement.Create));			
			Add(new WrapperDesc<nsIDOMHTMLMetaElement>("META", GeckoHTMLMetaElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLObjectElement>("OBJECT", GeckoHTMLObjectElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLOListElement>("OL", GeckoHTMLOListElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLOptGroupElement>("OPTGROUP", GeckoHTMLOptGroupElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLOptionElement>("OPTION", GeckoHTMLOptionElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLParagraphElement>("P", GeckoHTMLParagraphElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLPreElement>("PRE", GeckoHTMLPreElement.Create));			
			Add(new WrapperDesc<nsIDOMHTMLQuoteElement>("Q", GeckoHTMLQuoteElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLScriptElement>("SCRIPT", GeckoHTMLScriptElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLSelectElement>("SELECT", GeckoHTMLSelectElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLSourceElement>("SOURCE", GeckoHTMLSourceElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLStyleElement>("STYLE", GeckoHTMLStyleElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLTableCellElement>("TD", GeckoHTMLTableCellElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLTextAreaElement>("TEXTAREA", GeckoHTMLTextAreaElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLUListElement>("UL", GeckoHTMLUListElement.Create));
			Add(new WrapperDesc<nsIDOMHTMLMediaElement>("VIDEO", GeckoHTMLVideoElement.Create));


			//Add(new WrapperDesc<nsIDOMHTMLElement>("", GeckoHTMLElement.Create));
			
		}

		private static void Add(IWrapperDesc descriptor)
		{
			_descriptors.Add(descriptor.TagName, descriptor);
		}

		public static GeckoHTMLElement TryCreate(nsIDOMHTMLElement element)
		{
			using (var tagName = new nsAString())
			{
				element.GetTagNameAttribute(tagName);

				IWrapperDesc descriptor;
				if (_descriptors.TryGetValue(tagName.ToString(), out descriptor))
				{
					return descriptor.Create(element);
				}
			}
			return null;
		}
    }
}

