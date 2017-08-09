using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.Javascript;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLFrameElement : GeckoHTMLElement<nsIDOMHTMLFrameElement>
	{
		public static GeckoHTMLFrameElement Create(nsIDOMHTMLFrameElement element)
		{
			return new GeckoHTMLFrameElement(element);
		}

		private GeckoHTMLFrameElement(nsIDOMHTMLFrameElement element)
			: base(element)
		{

		}


		public string FrameBorder
		{
			get { return nsString.Get(Instance.GetFrameBorderAttribute); }
			set { nsString.Set(Instance.SetFrameBorderAttribute, value); }
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

		public bool NoResize
		{
			get { return Instance.GetNoResizeAttribute(); }
			set { Instance.SetNoResizeAttribute(value); }
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

