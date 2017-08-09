using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	public class GeckoCollection<T> : ComObject<nsIDOMNodeList>, IEnumerable<T>, IGeckoObjectWrapper
		where T : GeckoNode
	{
		public static GeckoCollection<T> Create(nsIDOMNodeList list)
		{
			return new GeckoCollection<T>(list);
		}

		protected GeckoCollection(nsIDOMNodeList instance)
			: base(instance)
		{

		}

		public virtual int Length
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

		#region IEnumerable<GeckoNode> Members

		public virtual IEnumerator<T> GetEnumerator()
		{
			uint length = (uint)Length;
			for (uint i = 0; i < length; i++)
			{
				yield return (T)Instance.Item(i).Wrap(GeckoNode.Create);
			}
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion


	}
}
