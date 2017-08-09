using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoCDATASection : GeckoText
	{
		public static GeckoCDATASection Create(nsIDOMCDATASection instance)
		{
			return new GeckoCDATASection(instance);
		}

		private GeckoCDATASection(nsIDOMCDATASection instance)
			: base((nsIDOMText)instance)
		{
			
		}


	}
}
