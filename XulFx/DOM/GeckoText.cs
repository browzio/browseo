using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public class GeckoText : GeckoCharacterData<nsIDOMText>
	{
		public static GeckoText Create(nsIDOMText instance)
		{
			var cdata = Xpcom.QueryInterface<nsIDOMCDATASection>(instance);
			if (cdata != null)
				return GeckoCDATASection.Create(cdata);

			return new GeckoText(instance);
		}

		protected GeckoText(nsIDOMText instance)
			: base(instance)
		{
			
		}


		public GeckoText SplitText(uint offset)
		{
			return Instance.SplitText(offset).Wrap(GeckoText.Create);
		}


		public string WholeText
		{
			get { return nsString.Get(Instance.GetWholeTextAttribute); }
		}
	}
}
