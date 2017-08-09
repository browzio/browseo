using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public class GeckoHTMLAreaElement : GeckoHTMLElement<nsIDOMHTMLAreaElement>
	{
		public static GeckoHTMLAreaElement Create(nsIDOMHTMLAreaElement element)
		{
			return new GeckoHTMLAreaElement(element);
		}

		private GeckoHTMLAreaElement(nsIDOMHTMLAreaElement element)
			: base(element)
		{

		}

		public string Alt
		{
			get { return nsString.Get(Instance.GetAltAttribute); }
			set { nsString.Set(Instance.SetAltAttribute, value); }
		}

		public string Coords
		{
			get { return nsString.Get(Instance.GetCoordsAttribute); }
			set { nsString.Set(Instance.SetCoordsAttribute, value); }
		}

		public string Href
		{
			get { return nsString.Get(Instance.GetHrefAttribute); }
			set { nsString.Set(Instance.SetHrefAttribute, value); }
		}

		public bool NoHref
		{
			get { return Instance.GetNoHrefAttribute(); }
			set { Instance.SetNoHrefAttribute(value); }
		}

		public string Shape
		{
			get { return nsString.Get(Instance.GetShapeAttribute); }
			set { nsString.Set(Instance.SetShapeAttribute, value); }
		}

		public string Target
		{
			get { return nsString.Get(Instance.GetTargetAttribute); }
			set { nsString.Set(Instance.SetTargetAttribute, value); }
		}

	}
}

