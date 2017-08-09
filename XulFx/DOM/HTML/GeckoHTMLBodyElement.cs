using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLBodyElement : GeckoHTMLElement<nsIDOMHTMLBodyElement>
	{
		public static GeckoHTMLBodyElement Create(nsIDOMHTMLBodyElement element)
		{
			return new GeckoHTMLBodyElement(element);
		}

		private GeckoHTMLBodyElement(nsIDOMHTMLBodyElement element)
			: base(element)
		{

		}

		public string ALink
		{
			get { return nsString.Get(Instance.GetALinkAttribute); }
			set { nsString.Set(Instance.SetALinkAttribute, value); }
		}

		public string Background
		{
			get { return nsString.Get(Instance.GetBackgroundAttribute); }
			set { nsString.Set(Instance.SetBackgroundAttribute, value); }
		}

		public string BgColor
		{
			get { return nsString.Get(Instance.GetBgColorAttribute); }
			set { nsString.Set(Instance.SetBgColorAttribute, value); }
		}

		public string Link
		{
			get { return nsString.Get(Instance.GetLinkAttribute); }
			set { nsString.Set(Instance.SetLinkAttribute, value); }
		}

		public string Text
		{
			get { return nsString.Get(Instance.GetTextAttribute); }
			set { nsString.Set(Instance.SetTextAttribute, value); }
		}

		public string VLink
		{
			get { return nsString.Get(Instance.GetVLinkAttribute); }
			set { nsString.Set(Instance.SetVLinkAttribute, value); }
		}

	}
}

