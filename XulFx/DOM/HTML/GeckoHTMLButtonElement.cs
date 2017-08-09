using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLButtonElement : GeckoHTMLElement<nsIDOMHTMLButtonElement>
	{
		public static GeckoHTMLButtonElement Create(nsIDOMHTMLButtonElement element)
		{
			return new GeckoHTMLButtonElement(element);
		}

		private GeckoHTMLButtonElement(nsIDOMHTMLButtonElement element)
			: base(element)
		{

		}

		public GeckoHTMLFormElement Form
		{
			get { return Instance.GetFormAttribute().Wrap(GeckoHTMLFormElement.Create); }
		}

		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
		}

		public string Value
		{
			get { return nsString.Get(Instance.GetValueAttribute); }
			set { nsString.Set(Instance.SetValueAttribute, value); }
		}

	}
}

