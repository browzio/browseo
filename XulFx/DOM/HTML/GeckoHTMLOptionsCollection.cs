using System;
using System.Collections.Generic;
using System.Linq;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLOptionsCollection : ComObject<nsIDOMHTMLOptionsCollection>, IEnumerable<GeckoHTMLOptionElement>, IGeckoObjectWrapper
	{
		public static GeckoHTMLOptionsCollection Create(nsIDOMHTMLOptionsCollection options)
		{
			return new GeckoHTMLOptionsCollection(options);
		}

		private GeckoHTMLOptionsCollection(nsIDOMHTMLOptionsCollection options)
			: base(options)
		{

		}

		public int Length
		{
			get { return (int)Instance.GetLengthAttribute(); }
		}

		public GeckoHTMLOptionElement this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
					throw new ArgumentOutOfRangeException("index");

				return Instance.Item((uint)index).Wrap(GeckoHTMLOptionElement.Create);
			}
		}

		public GeckoHTMLOptionElement this[string name]
		{
			get
			{
				if (name == null)
					throw new ArgumentNullException("name");
				if (name.IsEmptyOrWhiteSpace())
					throw new ArgumentOutOfRangeException("name");

				using (var aName = new nsAString(name))
				{
					return Instance.NamedItem(aName).Wrap(GeckoHTMLOptionElement.Create);
				}
			}
		}

		#region IEnumerable's Members

		public IEnumerator<GeckoHTMLOptionElement> GetEnumerator()
		{
			uint length = (uint)Length;
			for (uint i = 0; i < length; i++)
			{
				yield return Instance.Item(i).Wrap(GeckoHTMLOptionElement.Create);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

	}
}
