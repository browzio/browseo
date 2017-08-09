using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gecko.Security
{
	public sealed class X509CertList : ComObject<nsIX509CertList>, IGeckoObjectWrapper, ICollection<Certificate>
	{
		internal static X509CertList Create(nsIX509CertList instance)
		{
			return new X509CertList(instance);
		}

		private X509CertList(nsIX509CertList instance)
			: base(instance)
		{

		}



		public IntPtr RawCertList
		{
			get { return Instance.GetRawCertList(); }
		}

		public void Add(Certificate item)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			Instance.AddCert(item.Instance);
		}

		public void Clear()
		{
			foreach (Certificate item in this.ToArray())
			{
				Instance.DeleteCert(item.Instance);
			}
		}

		public bool Contains(Certificate item)
		{
			using (var enumerator = this.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == item)
						return true;
				}
			}
			return false;
		}

		public void CopyTo(Certificate[] array, int destIndex)
		{
			if (array == null)
				throw new ArgumentNullException("array");

			Certificate[] certs = this.ToArray();
			if (destIndex + certs.Length > array.Length)
				throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");

			for (int i = certs.Length - 1; i >= 0; i--)
			{
				array[destIndex + i] = certs[i];
			}
		}

		public int Count
		{
			get
			{
				int count = 0;
				using (var enumerator = this.GetEnumerator())
				{
					while (enumerator.MoveNext())
						count++;
				}
				return count;
			}
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(Certificate item)
		{
			if (item == null)
				throw new ArgumentNullException("item");

			if (this.Contains(item))
			{
				Instance.DeleteCert(item.Instance);
				return true;
			}
			return false;
		}

		public IEnumerator<Certificate> GetEnumerator()
		{
			return new GeckoSimpleEnumerator<nsIX509Cert, Certificate>(Instance.GetEnumerator(), Certificate.Create);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public Certificate[] ToArray()
		{
			var list = new List<Certificate>();
			using (var enumerator = this.GetEnumerator())
			{
				while (enumerator.MoveNext())
					list.Add(enumerator.Current);
			}
			return list.ToArray();
		}
	}
}
