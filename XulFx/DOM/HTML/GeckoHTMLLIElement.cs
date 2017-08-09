using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLLIElement : GeckoHTMLElement<nsIDOMHTMLLIElement>
	{
		public static GeckoHTMLLIElement Create(nsIDOMHTMLLIElement element)
		{
			return new GeckoHTMLLIElement(element);
		}

		private GeckoHTMLLIElement(nsIDOMHTMLLIElement element)
			: base(element)
		{

		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

		public int Value
		{
			get { return Instance.GetValueAttribute(); }
			set { Instance.SetValueAttribute(value); }
		}

	}
}

