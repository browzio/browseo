using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.Events
{
	public sealed class XulfxEventInitDictionary : Dictionary<string, Variant>, nsIXulfxDictionary, IDisposable
	{
		private class KeysEnumerator : nsIStringEnumerator
		{
			private IEnumerator<string> _enumerator;

			public KeysEnumerator(IEnumerator<string> enumerator)
			{
				_enumerator = enumerator;
			}

			public bool HasMore()
			{
				return _enumerator.MoveNext();
			}

			public void GetNext(nsAStringBase result)
			{
				result.SetData(_enumerator.Current);
			}
		}

		public void Dispose()
		{
			foreach (var v in this.Values)
			{
				((IDisposable)v).Dispose();
			}
		}

		#region nsIXulfxDictionary

		nsIStringEnumerator nsIXulfxDictionary.Keys()
		{
			return new KeysEnumerator(base.Keys.GetEnumerator());
		}

		nsIVariant nsIXulfxDictionary.Get(nsAStringBase aKey)
		{
			Variant value;
			if (this.TryGetValue(aKey.ToString(), out value))
				return value.QueryInterface<nsIVariant>();
			return Variant.Void.QueryInterface<nsIVariant>();
		}

		void nsIXulfxDictionary.Set(nsAStringBase aKey, nsIVariant aValue)
		{
			this[aKey.ToString()] = new Variant(Xpcom.QueryInterface<nsIVariant>(aValue));
		}

		void nsIXulfxDictionary.Remove(nsAStringBase aKey)
		{
			this.Remove(aKey.ToString());
		}

		#endregion nsIXulfxDictionary

	}
}
