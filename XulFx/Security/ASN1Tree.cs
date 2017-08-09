using Gecko.Interfaces;
using System;

namespace Gecko.Security
{
	public sealed class ASN1Tree : GeckoTreeView<nsIASN1Tree>
	{
		public static ASN1Tree Create(nsIASN1Tree instance)
		{
			return new ASN1Tree(instance);
		}

		private ASN1Tree(nsIASN1Tree instance)
			: base(instance)
		{
			_instance = instance;
		}

		public void LoadASN1Structure(ASN1Object asn1Object)
		{
			Instance.LoadASN1Structure(asn1Object.Instance);
		}

		public string GetDisplayData(uint index)
		{
			return nsString.Get(Instance.GetDisplayData, index);
		}
	}
}
