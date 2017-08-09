using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLPreElement : GeckoHTMLElement<nsIDOMHTMLPreElement>
	{
		public static GeckoHTMLPreElement Create(nsIDOMHTMLPreElement element)
		{
			return new GeckoHTMLPreElement(element);
		}

		private GeckoHTMLPreElement(nsIDOMHTMLPreElement element)
			: base(element)
		{

		}

		public int Width
		{
			get { return Instance.GetWidthAttribute(); }
			set { Instance.SetWidthAttribute(value); }
		}

	}
}
