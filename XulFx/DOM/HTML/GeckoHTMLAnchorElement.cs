using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLAnchorElement : GeckoHTMLElement<nsIDOMHTMLAnchorElement>
	{
		public static GeckoHTMLAnchorElement Create(nsIDOMHTMLAnchorElement element)
		{
			return new GeckoHTMLAnchorElement(element);
		}

		private GeckoHTMLAnchorElement(nsIDOMHTMLAnchorElement element)
			: base(element)
		{
			 
		}

		public string Charset
		{
			get { return nsString.Get(Instance.GetCharsetAttribute); }
			set { nsString.Set(Instance.SetCharsetAttribute, value); }
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

		public string Hreflang
		{
			get { return nsString.Get(Instance.GetHreflangAttribute); }
			set { nsString.Set(Instance.SetHreflangAttribute, value); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public string Rel
		{
			get { return nsString.Get(Instance.GetRelAttribute); }
			set { nsString.Set(Instance.SetRelAttribute, value); }
		}

		public string Rev
		{
			get { return nsString.Get(Instance.GetRevAttribute); }
			set { nsString.Set(Instance.SetRevAttribute, value); }
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

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

		public override string ToString()
		{
			return nsString.Get(Instance.GetHrefAttribute);
		}
	}
}

