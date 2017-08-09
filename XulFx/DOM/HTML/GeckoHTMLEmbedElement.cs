using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLEmbedElement : GeckoHTMLElement<nsIDOMHTMLEmbedElement>
	{
		public static GeckoHTMLEmbedElement Create(nsIDOMHTMLEmbedElement element)
		{
			return new GeckoHTMLEmbedElement(element);
		}

		private GeckoHTMLEmbedElement(nsIDOMHTMLEmbedElement element)
			: base(element)
		{

		}

		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		public string Height
		{
			get { return nsString.Get(Instance.GetHeightAttribute); }
			set { nsString.Set(Instance.SetHeightAttribute, value); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public string Src
		{
			get { return nsString.Get(Instance.GetSrcAttribute); }
			set { nsString.Set(Instance.SetSrcAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

		public string Width
		{
			get { return nsString.Get(Instance.GetWidthAttribute); }
			set { nsString.Set(Instance.SetWidthAttribute, value); }
		}

	}
}

