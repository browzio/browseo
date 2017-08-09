using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko.Security
{
	public sealed class CertTreeItem : ComObject<nsICertTreeItem>, IGeckoObjectWrapper
	{
		public static CertTreeItem Create(nsICertTreeItem instance)
		{
			return new CertTreeItem(instance);
		}

		private CertTreeItem(nsICertTreeItem instance)
			: base(instance)
		{

		}


		public Certificate Certificate
		{
			get { return Instance.GetCertAttribute().Wrap(Certificate.Create); }
		}

		public string HostPort
		{
			get { return nsString.Get(Instance.GetHostPortAttribute); }
		}

	}
}
