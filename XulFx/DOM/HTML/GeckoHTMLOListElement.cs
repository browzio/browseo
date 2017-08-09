using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLOListElement : GeckoHTMLElement<nsIDOMHTMLOListElement>
	{
		public static GeckoHTMLOListElement Create(nsIDOMHTMLOListElement element)
		{
			return new GeckoHTMLOListElement(element);
		}

		private GeckoHTMLOListElement(nsIDOMHTMLOListElement element)
			: base(element)
		{

		}


		public bool Compact
		{
			get { return Instance.GetCompactAttribute(); }
			set { Instance.SetCompactAttribute(value); }
		}

		public int Start
		{
			get { return Instance.GetStartAttribute(); }
			set { Instance.SetStartAttribute(value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

	}
}

