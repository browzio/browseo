using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLScriptElement : GeckoHTMLElement<nsIDOMHTMLScriptElement>
	{
		public static GeckoHTMLScriptElement Create(nsIDOMHTMLScriptElement element)
		{
			return new GeckoHTMLScriptElement(element);
		}

		private GeckoHTMLScriptElement(nsIDOMHTMLScriptElement element)
			: base(element)
		{

		}

		public string Text
		{
			get { return nsString.Get(Instance.GetTextAttribute); }
			set { nsString.Set(Instance.SetTextAttribute, value); }
		}

		public string HtmlFor
		{
			get { return nsString.Get(Instance.GetHtmlForAttribute); }
			set { nsString.Set(Instance.SetHtmlForAttribute, value); }
		}

		public string Event
		{
			get { return nsString.Get(Instance.GetEventAttribute); }
			set { nsString.Set(Instance.SetEventAttribute, value); }
		}

		public string Charset
		{
			get { return nsString.Get(Instance.GetCharsetAttribute); }
			set { nsString.Set(Instance.SetCharsetAttribute, value); }
		}

		public bool Defer
		{
			get { return Instance.GetDeferAttribute(); }
			set { Instance.SetDeferAttribute(value); }
		}

		public string Src
		{
			get { return nsString.Get(Instance.GetSrcAttribute); }
			set { nsString.Set(Instance.SetSrcAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

	}
}

