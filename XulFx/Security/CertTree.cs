using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.Security
{
	public sealed class CertTree : GeckoTreeView<nsICertTree>
	{
		public static CertTree Create(nsICertTree instance)
		{
			return new CertTree(instance);
		}

		private CertTree(nsICertTree instance)
			: base(instance)
		{

		}


		/// <summary>Member LoadCerts </summary>
		/// <param name='type'> </param>
		public void LoadCerts(uint type)
		{
			Instance.LoadCerts(type);
		}


		/// <summary>Member LoadCertsFromCache </summary>
		/// <param name='cache'> </param>
		/// <param name='type'> </param>
		public void LoadCertsFromCache(X509CertList cache, uint type)
		{
			Instance.LoadCertsFromCache(cache.Instance, type);
		}

		/// <summary>Member GetCert </summary>
		/// <param name='index'> </param>
		/// <returns>A nsIX509Cert</returns>
		public Certificate GetCert(uint index)
		{
			return Instance.GetCert(index).Wrap(Certificate.Create);
		}

		/// <summary>Member GetTreeItem </summary>
		/// <param name='index'> </param>
		/// <returns>A nsICertTreeItem</returns>
		public CertTreeItem GetTreeItem(int index)
		{
			return Instance.GetTreeItem((uint)index).Wrap(CertTreeItem.Create);
		}

		/// <summary>Member DeleteEntryObject </summary>
		/// <param name='index'> </param>
		public void DeleteEntryObject(uint index)
		{
			Instance.DeleteEntryObject(index);
		}
	}
}
