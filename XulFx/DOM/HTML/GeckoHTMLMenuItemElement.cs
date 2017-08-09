using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLMenuItemElement : GeckoHTMLElement<nsIDOMHTMLMenuItemElement>
	{
		public static GeckoHTMLMenuItemElement Create(nsIDOMHTMLMenuItemElement element)
		{
			return new GeckoHTMLMenuItemElement(element);
		}

		private GeckoHTMLMenuItemElement(nsIDOMHTMLMenuItemElement element)
			: base(element)
		{

		}


		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

		public string Label
		{
			get { return nsString.Get(Instance.GetLabelAttribute); }
			set { nsString.Set(Instance.SetLabelAttribute, value); }
		}

		public string Icon
		{
			get { return nsString.Get(Instance.GetIconAttribute); }
			set { nsString.Set(Instance.SetIconAttribute, value); }
		}

		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public bool DefaultChecked
		{
			get { return Instance.GetDefaultCheckedAttribute(); }
			set { Instance.SetDefaultCheckedAttribute(value); }
		}

		public bool Checked
		{
			get { return Instance.GetCheckedAttribute(); }
			set { Instance.SetCheckedAttribute(value); }
		}

		public string Radiogroup
		{
			get { return nsString.Get(Instance.GetRadiogroupAttribute); }
			set { nsString.Set(Instance.SetRadiogroupAttribute, value); }
		}
	}
}
