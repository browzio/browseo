using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Javascript;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLIFrameElement : GeckoHTMLElement<nsIDOMHTMLIFrameElement>
	{
		public static GeckoHTMLIFrameElement Create(nsIDOMHTMLIFrameElement element)
		{
			return new GeckoHTMLIFrameElement(element);
		}

		private GeckoHTMLIFrameElement(nsIDOMHTMLIFrameElement element)
			: base(element)
		{

		}

		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		public string FrameBorder
		{
			get { return nsString.Get(Instance.GetFrameBorderAttribute); }
			set { nsString.Set(Instance.SetFrameBorderAttribute, value); }
		}

		public string Height
		{
			get { return nsString.Get(Instance.GetHeightAttribute); }
			set { nsString.Set(Instance.SetHeightAttribute, value); }
		}

		public string LongDesc
		{
			get { return nsString.Get(Instance.GetLongDescAttribute); }
			set { nsString.Set(Instance.SetLongDescAttribute, value); }
		}

		public string MarginHeight
		{
			get { return nsString.Get(Instance.GetMarginHeightAttribute); }
			set { nsString.Set(Instance.SetMarginHeightAttribute, value); }
		}

		public string MarginWidth
		{
			get { return nsString.Get(Instance.GetMarginWidthAttribute); }
			set { nsString.Set(Instance.SetMarginWidthAttribute, value); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public string Scrolling
		{
			get { return nsString.Get(Instance.GetScrollingAttribute); }
			set { nsString.Set(Instance.SetScrollingAttribute, value); }
		}

		public string Src
		{
			get { return nsString.Get(Instance.GetSrcAttribute); }
			set { nsString.Set(Instance.SetSrcAttribute, value); }
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
				return GeckoJavascriptBridge.GetService().Call<GeckoDocument>(GetContentDocument);
			}
		}

		private GeckoDocument GetContentDocument()
		{
			return Instance.GetContentDocumentAttribute().Wrap(GeckoDocument.Create);
		}

		public GeckoWindow ContentWindow
		{
			get
			{
				GeckoDocument document = this.ContentDocument;
				return document != null ? document.DefaultView : null;
			}
		}
	}
}

