using System;
using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLCanvasElement : GeckoHTMLElement<nsIDOMHTMLCanvasElement>
	{
		public static GeckoHTMLCanvasElement Create(nsIDOMHTMLCanvasElement element)
		{
			return new GeckoHTMLCanvasElement(element);
		}

		private GeckoHTMLCanvasElement(nsIDOMHTMLCanvasElement element)
			: base(element)
		{

		}

		public uint Width
		{
			get { return Instance.GetWidthAttribute(); }
			set { Instance.SetWidthAttribute(value); }
		}

		public uint Height
		{
			get { return Instance.GetHeightAttribute(); }
			set { Instance.SetHeightAttribute(value); }
		}

		public string toDataURL(string type)
		{
			throw new NotImplementedException();

			//using (nsAString retval = new nsAString(), param = new nsAString(type))
			//{
			//	var instance = Xpcom.CreateInstance<nsIVariant>(Contracts.Variant);
			//	Instance.ToDataURL(param, instance, 2, retval);
			//	Marshal.ReleaseComObject(instance);
			//	return retval.ToString();
			//}
		}
	}
}

