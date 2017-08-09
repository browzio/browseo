using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using System;

namespace Gecko.DOM.HTML
{
	public class GeckoHTMLVideoElement : GeckoHTMLMediaElement
	{
		public static GeckoHTMLVideoElement Create(nsIDOMHTMLMediaElement instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");
	
			if (!instance.IsVideo())
				throw new ArgumentOutOfRangeException("instance");

			return new GeckoHTMLVideoElement(instance);
		}

		private GeckoHTMLVideoElement(nsIDOMHTMLMediaElement instance)
			: base(instance)
		{

		}

	}
}
