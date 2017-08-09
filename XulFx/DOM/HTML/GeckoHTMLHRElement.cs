using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLHRElement : GeckoHTMLElement<nsIDOMHTMLHRElement>
	{
		public static GeckoHTMLHRElement Create(nsIDOMHTMLHRElement element)
		{
			return new GeckoHTMLHRElement(element);
		}

		private GeckoHTMLHRElement(nsIDOMHTMLHRElement element)
			: base(element)
		{

		}

		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		public bool NoShade
		{
			get { return Instance.GetNoShadeAttribute(); }
			set { Instance.SetNoShadeAttribute(value); }
		}

		public string Size
		{
			get { return nsString.Get(Instance.GetSizeAttribute); }
			set { nsString.Set(Instance.SetSizeAttribute, value); }
		}

		public string Width
		{
			get { return nsString.Get(Instance.GetWidthAttribute); }
			set { nsString.Set(Instance.SetWidthAttribute, value); }
		}

	}
}
