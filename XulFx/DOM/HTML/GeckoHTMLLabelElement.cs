using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLLabelElement : GeckoHTMLElement<nsIDOMHTMLLabelElement>
	{
		public static GeckoHTMLLabelElement Create(nsIDOMHTMLLabelElement element)
		{
			return new GeckoHTMLLabelElement(element);
		}

		private GeckoHTMLLabelElement(nsIDOMHTMLLabelElement element)
			: base(element)
		{

		}

		public GeckoHTMLFormElement Form
		{
			get { return Instance.GetFormAttribute().Wrap(GeckoHTMLFormElement.Create); }
		}

		public string HtmlFor
		{
			get { return nsString.Get(Instance.GetHtmlForAttribute); }
			set { nsString.Set(Instance.SetHtmlForAttribute, value); }
		}

	}
}

