using System;
using System.Collections.Generic;
using System.Linq;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoStyleSheetList : ComObject<nsIDOMStyleSheetList>, IEnumerable<GeckoStyleSheet>, IGeckoObjectWrapper
	{
		public static GeckoStyleSheetList Create(nsIDOMStyleSheetList instance)
		{
			return new GeckoStyleSheetList(instance);
		}

		private GeckoStyleSheetList(nsIDOMStyleSheetList instance)
			: base(instance)
		{

		}

		public GeckoStyleSheet this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
					throw new ArgumentOutOfRangeException("index");

				return Instance.Item((uint)index).Wrap(GeckoStyleSheet.Create);
			}
		}

		public int Length
		{
			get { return (int)Instance.GetLengthAttribute(); }
		}

		public IEnumerator<GeckoStyleSheet> GetEnumerator()
		{
			uint length = (uint)this.Length;
			for (uint i = 0; i < length; i++)
			{
				yield return Instance.Item(i).Wrap(GeckoStyleSheet.Create);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
