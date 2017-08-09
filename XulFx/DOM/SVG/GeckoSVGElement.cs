using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using System;

namespace Gecko.DOM
{
	public class GeckoSVGElement : GeckoElement<nsIDOMSVGElement>
	{
		public static GeckoSVGElement Create(nsIDOMSVGElement element)
		{
			// TODO: create correct wrappers
			return new GeckoSVGElement(element);
		}

		protected GeckoSVGElement(nsIDOMSVGElement element)
			: base(element)
		{

		}


	}
}
