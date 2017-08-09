using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLUListElement : GeckoHTMLElement<nsIDOMHTMLUListElement>
	{
		public static GeckoHTMLUListElement Create(nsIDOMHTMLUListElement element)
		{
			return new GeckoHTMLUListElement(element);
		}

		private GeckoHTMLUListElement(nsIDOMHTMLUListElement element)
			: base(element)
		{

		}


		public bool Compact
		{
			get { return Instance.GetCompactAttribute(); }
			set { Instance.SetCompactAttribute(value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

	}
}

