using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLMapElement : GeckoHTMLElement<nsIDOMHTMLMapElement>
	{
		public static GeckoHTMLMapElement Create(nsIDOMHTMLMapElement element)
		{
			return new GeckoHTMLMapElement(element);
		}

		private GeckoHTMLMapElement(nsIDOMHTMLMapElement element)
			: base(element)
		{

		}

		public GeckoHTMLCollection Areas
		{
			get { return Instance.GetAreasAttribute().Wrap(GeckoHTMLCollection.Create); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

	}
}
