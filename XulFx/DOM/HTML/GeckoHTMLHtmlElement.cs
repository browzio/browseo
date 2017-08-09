using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLHtmlElement : GeckoHTMLElement<nsIDOMHTMLHtmlElement>
	{
		public static GeckoHTMLHtmlElement Create(nsIDOMHTMLHtmlElement element)
		{
			return new GeckoHTMLHtmlElement(element);
		}

		private GeckoHTMLHtmlElement(nsIDOMHTMLHtmlElement element)
			: base(element)
		{

		}

		public string Version
		{
			get { return nsString.Get(Instance.GetVersionAttribute); }
			set { nsString.Set(Instance.SetVersionAttribute, value); }
		}

	}
}

