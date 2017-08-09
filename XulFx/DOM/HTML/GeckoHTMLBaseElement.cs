using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLBaseElement : GeckoHTMLElement<nsIDOMHTMLBaseElement>
	{
		public static GeckoHTMLBaseElement Create(nsIDOMHTMLBaseElement element)
		{
			return new GeckoHTMLBaseElement(element);
		}

		private GeckoHTMLBaseElement(nsIDOMHTMLBaseElement element)
			: base(element)
		{

		}

		public string Href
		{
			get { return nsString.Get(Instance.GetHrefAttribute); }
			set { nsString.Set(Instance.SetHrefAttribute, value); }
		}

		public string Target
		{
			get { return nsString.Get(Instance.GetTargetAttribute); }
			set { nsString.Set(Instance.SetTargetAttribute, value); }
		}

	}
}

