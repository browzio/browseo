using System;
using System.Collections.Generic;
using System.Linq;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoWindowCollection : ComObject<nsIDOMWindowCollection>, IEnumerable<GeckoWindow>, IGeckoObjectWrapper
	{
		public static GeckoWindowCollection Create(nsIDOMWindowCollection windows)
		{
			return new GeckoWindowCollection(windows);
		}

		private GeckoWindowCollection(nsIDOMWindowCollection windows)
			: base(windows)
		{

		}

		public int Length
		{
			get { return (int)Instance.GetLengthAttribute(); }
		}

		public GeckoWindow this[int index]
		{
			get
			{
				if (index < 0 || index >= Length)
					throw new ArgumentOutOfRangeException("index");

				return Instance.Item((uint)index).Wrap(GeckoWindow.Create);
			}
		}

		public GeckoWindow this[string name]
		{
			get
			{
				if (name == null)
					throw new ArgumentNullException("name");
				if (name.IsEmptyOrWhiteSpace())
					throw new ArgumentOutOfRangeException("name");

				using (var aName = new nsAString(name))
				{
					return Instance.NamedItem(aName).Wrap(GeckoWindow.Create);
				}
			}
		}

		#region IEnumerable<GeckoWindow> Members

		public IEnumerator<GeckoWindow> GetEnumerator()
		{
			uint length = (uint)Length;
			for (uint i = 0; i < length; i++)
			{
				yield return Instance.Item(i).Wrap(GeckoWindow.Create);
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
