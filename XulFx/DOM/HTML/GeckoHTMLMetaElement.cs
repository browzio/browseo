using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLMetaElement : GeckoHTMLElement<nsIDOMHTMLMetaElement>
	{
		public static GeckoHTMLMetaElement Create(nsIDOMHTMLMetaElement element)
		{
			return new GeckoHTMLMetaElement(element);
		}

		private GeckoHTMLMetaElement(nsIDOMHTMLMetaElement element)
			: base(element)
		{

		}


		public string Content
		{
			get { return nsString.Get(Instance.GetContentAttribute); }
			set { nsString.Set(Instance.SetContentAttribute, value); }
		}

		public string HttpEquiv
		{
			get { return nsString.Get(Instance.GetHttpEquivAttribute); }
			set { nsString.Set(Instance.SetHttpEquivAttribute, value); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public string Scheme
		{
			get { return nsString.Get(Instance.GetSchemeAttribute); }
			set { nsString.Set(Instance.SetSchemeAttribute, value); }
		}

	}
}
