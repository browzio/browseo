using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoComment : GeckoCharacterData<nsIDOMComment>
	{
		public static GeckoComment Create(nsIDOMComment instance)
		{
			return new GeckoComment(instance);
		}

		private GeckoComment(nsIDOMComment instance)
			: base(instance)
		{
			
		}


	}
}
