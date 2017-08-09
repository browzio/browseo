using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLHeadElement : GeckoHTMLElement<nsIDOMHTMLHeadElement>
	{
		public static GeckoHTMLHeadElement Create(nsIDOMHTMLHeadElement element)
		{
			return new GeckoHTMLHeadElement(element);
		}

		private GeckoHTMLHeadElement(nsIDOMHTMLHeadElement element)
			: base(element)
		{
			 
		}


	}
}
