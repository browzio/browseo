using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLOptGroupElement : GeckoHTMLElement<nsIDOMHTMLOptGroupElement>
	{
		public static GeckoHTMLOptGroupElement Create(nsIDOMHTMLOptGroupElement element)
		{
			return new GeckoHTMLOptGroupElement(element);
		}

		private GeckoHTMLOptGroupElement(nsIDOMHTMLOptGroupElement element)
			: base(element)
		{

		}


		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public string Label
		{
			get { return nsString.Get(Instance.GetLabelAttribute); }
			set { nsString.Set(Instance.SetLabelAttribute, value); }
		}

	}
}
