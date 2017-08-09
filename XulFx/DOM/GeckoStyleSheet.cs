using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public class GeckoStyleSheet : ComObject<nsIDOMStyleSheet>, IGeckoObjectWrapper
	{
		public static GeckoStyleSheet Create(nsIDOMStyleSheet instance)
		{
			nsIDOMCSSStyleSheet cssStyleSheet = Xpcom.QueryInterface<nsIDOMCSSStyleSheet>(instance);
			if (cssStyleSheet != null)
				return GeckoCSSStyleSheet.Create(cssStyleSheet);
			return new GeckoStyleSheet(instance);
		}

		protected GeckoStyleSheet(nsIDOMStyleSheet instance)
			: base(instance)
		{

		}


		public virtual string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
		}

		public virtual bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public GeckoNode OwnerNode
		{
			get { return Instance.GetOwnerNodeAttribute().Wrap(GeckoNode.Create); }
		}

		public virtual GeckoStyleSheet ParentStyleSheet
		{
			get { return Instance.GetParentStyleSheetAttribute().Wrap(GeckoStyleSheet.Create); }
		}

		public virtual string Href
		{
			get { return nsString.Get(Instance.GetHrefAttribute); }
		}

		public virtual string Title
		{
			get { return nsString.Get(Instance.GetTitleAttribute); }
		}

		public GeckoMediaList Media
		{
			get { return Instance.GetMediaAttribute().Wrap(GeckoMediaList.Create); }
		}

		public override string ToString()
		{
			return "Href=" + this.Href;
		}
	}
}
