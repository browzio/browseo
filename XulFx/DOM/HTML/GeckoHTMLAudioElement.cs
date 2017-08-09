using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public class GeckoHTMLAudioElement : GeckoHTMLMediaElement
	{
		public static GeckoHTMLAudioElement Create(nsIDOMHTMLMediaElement instance)
		{
			return new GeckoHTMLAudioElement(instance);
		}

		private GeckoHTMLAudioElement(nsIDOMHTMLMediaElement instance)
			: base(instance)
		{

		}



	}
}
