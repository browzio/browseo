using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLMenuElement : GeckoHTMLElement<nsIDOMHTMLMenuElement>
	{
		public static GeckoHTMLMenuElement Create(nsIDOMHTMLMenuElement element)
		{
			return new GeckoHTMLMenuElement(element);
		}

		private GeckoHTMLMenuElement(nsIDOMHTMLMenuElement element)
			: base(element)
		{

		}


		public bool Compact
		{
			get { return Instance.GetCompactAttribute(); }
			set { Instance.SetCompactAttribute(value); }
		}

	}
}
