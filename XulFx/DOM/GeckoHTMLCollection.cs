using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace Gecko.DOM
{
	public class GeckoHTMLCollection : ComObject<nsIDOMHTMLCollection>, ICollection<GeckoElement>, IGeckoObjectWrapper
	{
		public static GeckoHTMLCollection Create(nsIDOMHTMLCollection instance)
		{
			return new GeckoHTMLCollection(instance);
		}

		protected GeckoHTMLCollection(nsIDOMHTMLCollection instance)
			: base(instance)
		{

		}

		#region ICollection<GeckoElement>

		public void Add(GeckoElement item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(GeckoElement item)
		{
			foreach (GeckoNode node in this)
			{
				if (node == item) return true;
			}
			return false;
		}

		public void CopyTo(GeckoElement[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException("array");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException("arrayIndex");

			int length = this.Count;
			if (arrayIndex + length >= array.Length)
				throw new ArgumentException();

			for (int i = 0; i < length; i++)
			{
				array[arrayIndex++] = (GeckoElement)Instance.Item((uint)i).Wrap(GeckoNode.Create);
			}
		}

		public int Count
		{
			get { return (int)Instance.GetLengthAttribute(); }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public bool Remove(GeckoElement item)
		{
			throw new NotSupportedException();
		}
		
		public virtual IEnumerator<GeckoElement> GetEnumerator()
		{
			int length = this.Count;
			for (int i = 0; i < length; i++)
			{
				yield return (GeckoElement)Instance.Item((uint)i).Wrap(GeckoNode.Create);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

	}
}
