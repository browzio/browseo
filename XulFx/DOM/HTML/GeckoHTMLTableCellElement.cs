using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLTableCellElement : GeckoHTMLElement<nsIDOMHTMLTableCellElement>
	{
		public static GeckoHTMLTableCellElement Create(nsIDOMHTMLTableCellElement element)
		{
			return new GeckoHTMLTableCellElement(element);
		}

		private GeckoHTMLTableCellElement(nsIDOMHTMLTableCellElement element)
			: base(element)
		{

		}

		public int CellIndex
		{
			get { return Instance.GetCellIndexAttribute(); }
		}

		public string Abbr
		{
			get { return nsString.Get(Instance.GetAbbrAttribute); }
			set { nsString.Set(Instance.SetAbbrAttribute, value); }
		}

		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		public string Axis
		{
			get { return nsString.Get(Instance.GetAxisAttribute); }
			set { nsString.Set(Instance.SetAxisAttribute, value); }
		}

		public string BgColor
		{
			get { return nsString.Get(Instance.GetBgColorAttribute); }
			set { nsString.Set(Instance.SetBgColorAttribute, value); }
		}

		public string Ch
		{
			get { return nsString.Get(Instance.GetChAttribute); }
			set { nsString.Set(Instance.SetChAttribute, value); }
		}

		public string ChOff
		{
			get { return nsString.Get(Instance.GetChOffAttribute); }
			set { nsString.Set(Instance.SetChOffAttribute, value); }
		}

		public int ColSpan
		{
			get { return Instance.GetColSpanAttribute(); }
			set { Instance.SetColSpanAttribute(value); }
		}

		public string Headers
		{
			get { return nsString.Get(Instance.GetHeadersAttribute); }
			set { nsString.Set(Instance.SetHeadersAttribute, value); }
		}

		public string Height
		{
			get { return nsString.Get(Instance.GetHeightAttribute); }
			set { nsString.Set(Instance.SetHeightAttribute, value); }
		}

		public bool NoWrap
		{
			get { return Instance.GetNoWrapAttribute(); }
			set { Instance.SetNoWrapAttribute(value); }
		}

		public int RowSpan
		{
			get { return Instance.GetRowSpanAttribute(); }
			set { Instance.SetRowSpanAttribute(value); }
		}

		public string Scope
		{
			get { return nsString.Get(Instance.GetScopeAttribute); }
			set { nsString.Set(Instance.SetScopeAttribute, value); }
		}

		public string VAlign
		{
			get { return nsString.Get(Instance.GetVAlignAttribute); }
			set { nsString.Set(Instance.SetVAlignAttribute, value); }
		}

		public string Width
		{
			get { return nsString.Get(Instance.GetWidthAttribute); }
			set { nsString.Set(Instance.SetWidthAttribute, value); }
		}

	}
}
