using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoDocumentFragment : GeckoNode<nsIDOMDocumentFragment>
	{
		public static GeckoDocumentFragment Create(nsIDOMDocumentFragment instance)
		{
			return new GeckoDocumentFragment(instance);
		}

		private GeckoDocumentFragment(nsIDOMDocumentFragment instance)
			: base(instance)
		{

		}


	}
}
