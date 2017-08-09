using System;
using System.Collections.Generic;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	/// <summary>
	/// Represents a collection of <see cref="GeckoHtmlElement"/> objects.
	/// </summary>
	public class GeckoNodeCollection : GeckoCollection<GeckoNode>
	{
		public static new GeckoNodeCollection Create(nsIDOMNodeList list)
		{
			return new GeckoNodeCollection(list);
		}

		protected GeckoNodeCollection(nsIDOMNodeList instance)
			: base(instance)
		{

		}

	}
}
