using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLAppletElement : GeckoHTMLElement<nsIDOMHTMLAppletElement>
	{
		public static GeckoHTMLAppletElement Create(nsIDOMHTMLAppletElement element)
		{
			return new GeckoHTMLAppletElement(element);
		}

		private GeckoHTMLAppletElement(nsIDOMHTMLAppletElement element)
			: base(element)
		{

		}


		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		public string Alt
		{
			get { return nsString.Get(Instance.GetAltAttribute); }
			set { nsString.Set(Instance.SetAltAttribute, value); }
		}

		public string Archive
		{
			get { return nsString.Get(Instance.GetArchiveAttribute); }
			set { nsString.Set(Instance.SetArchiveAttribute, value); }
		}

		public string Code
		{
			get { return nsString.Get(Instance.GetCodeAttribute); }
			set { nsString.Set(Instance.SetCodeAttribute, value); }
		}

		public string CodeBase
		{
			get { return nsString.Get(Instance.GetCodeBaseAttribute); }
			set { nsString.Set(Instance.SetCodeBaseAttribute, value); }
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

		public string Object
		{
			get { return nsString.Get(Instance.GetObjectAttribute); }
			set { nsString.Set(Instance.SetObjectAttribute, value); }
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

	}
}

