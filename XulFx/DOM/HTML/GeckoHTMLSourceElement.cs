using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLSourceElement : GeckoHTMLElement<nsIDOMHTMLSourceElement>
	{
		public static GeckoHTMLSourceElement Create(nsIDOMHTMLSourceElement element)
		{
			return new GeckoHTMLSourceElement(element);
		}

		private GeckoHTMLSourceElement(nsIDOMHTMLSourceElement element)
			: base(element)
		{

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

		public string Media
		{
			get { return nsString.Get(Instance.GetMediaAttribute); }
			set { nsString.Set(Instance.SetMediaAttribute, value); }
		}

	}
}
