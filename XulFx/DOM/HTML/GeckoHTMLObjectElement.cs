using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLObjectElement : GeckoHTMLElement<nsIDOMHTMLObjectElement>
	{
		public static GeckoHTMLObjectElement Create(nsIDOMHTMLObjectElement element)
		{
			return new GeckoHTMLObjectElement(element);
		}

		private GeckoHTMLObjectElement(nsIDOMHTMLObjectElement element)
			: base(element)
		{

		}

		public GeckoHTMLFormElement Form
		{
			get { return Instance.GetFormAttribute().Wrap(GeckoHTMLFormElement.Create); }
		}

		public string Code
		{
			get { return nsString.Get(Instance.GetCodeAttribute); }
			set { nsString.Set(Instance.SetCodeAttribute, value); }
		}

		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		public string Archive
		{
			get { return nsString.Get(Instance.GetArchiveAttribute); }
			set { nsString.Set(Instance.SetArchiveAttribute, value); }
		}

		public string Border
		{
			get { return nsString.Get(Instance.GetBorderAttribute); }
			set { nsString.Set(Instance.SetBorderAttribute, value); }
		}

		public string CodeBase
		{
			get { return nsString.Get(Instance.GetCodeBaseAttribute); }
			set { nsString.Set(Instance.SetCodeBaseAttribute, value); }
		}

		public string CodeType
		{
			get { return nsString.Get(Instance.GetCodeTypeAttribute); }
			set { nsString.Set(Instance.SetCodeTypeAttribute, value); }
		}

		public string Data
		{
			get { return nsString.Get(Instance.GetDataAttribute); }
			set { nsString.Set(Instance.SetDataAttribute, value); }
		}

		public bool Declare
		{
			get { return Instance.GetDeclareAttribute(); }
			set { Instance.SetDeclareAttribute(value); }
		}

		public string Height
		{
			get { return nsString.Get(Instance.GetHeightAttribute); }
			set { nsString.Set(Instance.SetHeightAttribute, value); }
		}

		public int Hspace
		{
			get { return Instance.GetHspaceAttribute(); }
			set { Instance.SetHspaceAttribute(value); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public string Standby
		{
			get { return nsString.Get(Instance.GetStandbyAttribute); }
			set { nsString.Set(Instance.SetStandbyAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

		public string UseMap
		{
			get { return nsString.Get(Instance.GetUseMapAttribute); }
			set { nsString.Set(Instance.SetUseMapAttribute, value); }
		}

		public int Vspace
		{
			get { return Instance.GetVspaceAttribute(); }
			set { Instance.SetVspaceAttribute(value); }
		}

		public string Width
		{
			get { return nsString.Get(Instance.GetWidthAttribute); }
			set { nsString.Set(Instance.SetWidthAttribute, value); }
		}

		public GeckoDocument ContentDocument
		{
			get
			{
				return Instance.GetContentDocumentAttribute().Wrap(GeckoDocument.Create);
			}
		}

	}
}

