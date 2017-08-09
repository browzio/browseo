using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLFormElement : GeckoHTMLElement<nsIDOMHTMLFormElement>
	{
		public static GeckoHTMLFormElement Create(nsIDOMHTMLFormElement element)
		{
			return new GeckoHTMLFormElement(element);
		}

		private GeckoHTMLFormElement(nsIDOMHTMLFormElement element)
			: base(element)
		{

		}

		public GeckoHTMLCollection Elements
		{
			get { return Instance.GetElementsAttribute().Wrap(GeckoHTMLCollection.Create); }
		}

		public int Length
		{
			get { return Instance.GetLengthAttribute(); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public string AcceptCharset
		{
			get { return nsString.Get(Instance.GetAcceptCharsetAttribute); }
			set { nsString.Set(Instance.SetAcceptCharsetAttribute, value); }
		}

		public string Action
		{
			get { return nsString.Get(Instance.GetActionAttribute); }
			set { nsString.Set(Instance.SetActionAttribute, value); }
		}

		public string Enctype
		{
			get { return nsString.Get(Instance.GetEnctypeAttribute); }
			set { nsString.Set(Instance.SetEnctypeAttribute, value); }
		}

		public string Method
		{
			get { return nsString.Get(Instance.GetMethodAttribute); }
			set { nsString.Set(Instance.SetMethodAttribute, value); }
		}

		public string Target
		{
			get { return nsString.Get(Instance.GetTargetAttribute); }
			set { nsString.Set(Instance.SetTargetAttribute, value); }
		}

		public void Submit()
		{
			Instance.Submit();
		}

		public void Reset()
		{
			Instance.Reset();
		}

	}
}

