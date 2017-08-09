using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLLinkElement : GeckoHTMLElement<nsIDOMHTMLLinkElement>
	{
		public static GeckoHTMLLinkElement Create(nsIDOMHTMLLinkElement element)
		{
			return new GeckoHTMLLinkElement(element);
		}

		private GeckoHTMLLinkElement(nsIDOMHTMLLinkElement element)
			: base(element)
		{

		}


		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public string Charset
		{
			get { return nsString.Get(Instance.GetCharsetAttribute); }
			set { nsString.Set(Instance.SetCharsetAttribute, value); }
		}

		public string Href
		{
			get { return nsString.Get(Instance.GetHrefAttribute); }
			set { nsString.Set(Instance.SetHrefAttribute, value); }
		}

		public string Hreflang
		{
			get { return nsString.Get(Instance.GetHreflangAttribute); }
			set { nsString.Set(Instance.SetHreflangAttribute, value); }
		}

		public string Media
		{
			get { return nsString.Get(Instance.GetMediaAttribute); }
			set { nsString.Set(Instance.SetMediaAttribute, value); }
		}

		public string Rel
		{
			get { return nsString.Get(Instance.GetRelAttribute); }
			set { nsString.Set(Instance.SetRelAttribute, value); }
		}

		public string Rev
		{
			get { return nsString.Get(Instance.GetRevAttribute); }
			set { nsString.Set(Instance.SetRevAttribute, value); }
		}

		public string Target
		{
			get { return nsString.Get(Instance.GetTargetAttribute); }
			set { nsString.Set(Instance.SetTargetAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

	}
}

