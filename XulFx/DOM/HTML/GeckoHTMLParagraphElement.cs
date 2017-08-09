using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLParagraphElement : GeckoHTMLElement<nsIDOMHTMLParagraphElement>
	{
		public static GeckoHTMLParagraphElement Create(nsIDOMHTMLParagraphElement element)
		{
			return new GeckoHTMLParagraphElement(element);
		}

		private GeckoHTMLParagraphElement(nsIDOMHTMLParagraphElement element)
			: base(element)
		{

		}

		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

	}
}
