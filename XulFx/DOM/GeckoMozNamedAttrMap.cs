using System;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Collections.Generic;

namespace Gecko.DOM
{
	public sealed class GeckoMozNamedAttrMap : ComObject<nsIDOMMozNamedAttrMap>, IEnumerable<GeckoNode>, IGeckoObjectWrapper
	{
		public static GeckoMozNamedAttrMap Create(nsIDOMMozNamedAttrMap instance)
		{
			return new GeckoMozNamedAttrMap(instance);
		}

		private GeckoMozNamedAttrMap(nsIDOMMozNamedAttrMap instance)
			: base(instance)
		{

		}


		/// <summary>
		/// Gets the number of items in the map.
		/// </summary>
		public int Length
		{
			get { return (int)Instance.GetLengthAttribute(); }
		}

		public GeckoNode this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
					throw new ArgumentOutOfRangeException("index");

				return Instance.Item((uint)index).Wrap(GeckoNode.Create);
			}
		}

		public GeckoNode this[string name]
		{
			get { return nsString.Pass(Instance.GetNamedItem, name).Wrap(GeckoNode.Create); }
		}

		public GeckoNode this[string namespaceUri, string localName]
		{
			get { return nsString.Pass(Instance.GetNamedItemNS, namespaceUri, localName).Wrap(GeckoNode.Create); }
		}

		public GeckoNode RemoveNamedItem(string name)
		{
			return nsString.Pass(Instance.RemoveNamedItem, name).Wrap(GeckoNode.Create);
		}


		#region IEnumerable's

		public IEnumerator<GeckoNode> GetEnumerator()
		{
			uint length = (uint)Length;
			for (uint i = 0; i < length; i++)
			{
				yield return Instance.Item(i).Wrap(GeckoNode.Create);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}

}
