using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLOptionElement : GeckoHTMLElement<nsIDOMHTMLOptionElement>
	{
		public static GeckoHTMLOptionElement Create(nsIDOMHTMLOptionElement element)
		{
			return new GeckoHTMLOptionElement(element);
		}

		internal static new GeckoHTMLOptionElement Create(nsIDOMNode element)
		{
			return new GeckoHTMLOptionElement((nsIDOMHTMLOptionElement)element);
		}

		private GeckoHTMLOptionElement(nsIDOMHTMLOptionElement element)
			: base(element)
		{

		}


		public GeckoHTMLFormElement Form
		{
			get { return Instance.GetFormAttribute().Wrap(GeckoHTMLFormElement.Create); }
		}

		public bool DefaultSelected
		{
			get { return Instance.GetDefaultSelectedAttribute(); }
			set { Instance.SetDefaultSelectedAttribute(value); }
		}

		public string Text
		{
			get { return nsString.Get(Instance.GetTextAttribute); }
		}

		public int Index
		{
			get { return Instance.GetIndexAttribute(); }
		}

		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public string Label
		{
			get { return nsString.Get(Instance.GetLabelAttribute); }
			set { nsString.Set(Instance.SetLabelAttribute, value); }
		}

		public bool Selected
		{
			get { return Instance.GetSelectedAttribute(); }
			set { Instance.SetSelectedAttribute(value); }
		}

		public string Value
		{
			get { return nsString.Get(Instance.GetValueAttribute); }
			set { nsString.Set(Instance.SetValueAttribute, value); }
		}

	}
}
