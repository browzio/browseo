using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLQuoteElement : GeckoHTMLElement<nsIDOMHTMLQuoteElement>
	{
		public static GeckoHTMLQuoteElement Create(nsIDOMHTMLQuoteElement element)
		{
			return new GeckoHTMLQuoteElement(element);
		}

		private GeckoHTMLQuoteElement(nsIDOMHTMLQuoteElement element)
			: base(element)
		{

		}

		public string Cite
		{
			get { return nsString.Get(Instance.GetCiteAttribute); }
			set { nsString.Set(Instance.GetCiteAttribute, value); }
		}

	}
}
