using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLDirectoryElement : GeckoHTMLElement<nsIDOMHTMLDirectoryElement>
	{
		public static GeckoHTMLDirectoryElement Create(nsIDOMHTMLDirectoryElement element)
		{
			return new GeckoHTMLDirectoryElement(element);
		}

		private GeckoHTMLDirectoryElement(nsIDOMHTMLDirectoryElement element)
			: base(element)
		{

		}

	}
}

