using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLFieldSetElement : GeckoHTMLElement<nsIDOMHTMLFieldSetElement>
	{
		public static GeckoHTMLFieldSetElement Create(nsIDOMHTMLFieldSetElement element)
		{
			return new GeckoHTMLFieldSetElement(element);
		}

		private GeckoHTMLFieldSetElement(nsIDOMHTMLFieldSetElement element)
			: base(element)
		{

		}

		public GeckoHTMLFormElement Form
		{
			get { return Instance.GetFormAttribute().Wrap(GeckoHTMLFormElement.Create); }
		}

	}
}
