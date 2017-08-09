using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLStyleElement : GeckoHTMLElement<nsIDOMHTMLStyleElement>
	{
		public static GeckoHTMLStyleElement Create(nsIDOMHTMLStyleElement element)
		{
			return new GeckoHTMLStyleElement(element);
		}

		private GeckoHTMLStyleElement(nsIDOMHTMLStyleElement element)
			: base(element)
		{

		}


		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public string Media
		{
			get { return nsString.Get(Instance.GetMediaAttribute); }
			set { nsString.Set(Instance.SetMediaAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

	}
}
