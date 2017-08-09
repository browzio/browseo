using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLFrameSetElement : GeckoHTMLElement<nsIDOMHTMLFrameSetElement>
	{
		public static GeckoHTMLFrameSetElement Create(nsIDOMHTMLFrameSetElement element)
		{
			return new GeckoHTMLFrameSetElement(element);
		}

		private GeckoHTMLFrameSetElement(nsIDOMHTMLFrameSetElement element)
			: base(element)
		{

		}

		public string Cols
		{
			get { return nsString.Get(Instance.GetColsAttribute); }
			set { nsString.Set(Instance.SetColsAttribute, value); }
		}

		public string Rows
		{
			get { return nsString.Get(Instance.GetRowsAttribute); }
			set { nsString.Set(Instance.SetRowsAttribute, value); }
		}

	}
}

