using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;

namespace Gecko.Security
{
	public sealed class CertificateDatabase : ComObject<nsIX509CertDB>, IGeckoObjectWrapper
	{
		public static CertificateDatabase GetService()
		{
			return Xpcom.GetService<nsIX509CertDB>(Contracts.X509CertDb).Wrap(CertificateDatabase.Create);
		}

		private static CertificateDatabase Create(nsIX509CertDB instance)
		{
			return new CertificateDatabase(instance);
		}

		private CertificateDatabase(nsIX509CertDB instance)
			: base(instance)
		{

		}


		public Certificate ConstructX509FromBase64(string base64)
		{
			return nsString.Pass<nsIX509Cert>(Instance.ConstructX509FromBase64, base64).Wrap(Certificate.Create);
		}

		public void DeleteCertificate(Certificate certificate)
		{
			if (certificate == null)
				throw new ArgumentNullException("certificate");

			Instance.DeleteCertificate(certificate.Instance);
		}

		public ICollection<Certificate> GetCerts()
		{
			return Instance.GetCerts().Wrap(X509CertList.Create);
		}

		public void AddCertFromBase64(string base64, string trust, string name)
		{
			if (base64 == null)
				throw new ArgumentNullException("base64");
			if (trust == null)
				throw new ArgumentNullException("trust");
			if (name == null)
				throw new ArgumentNullException("name");

			if (base64.IsEmptyOrWhiteSpace())
				throw new ArgumentException("base64");
			if (trust.IsEmptyOrWhiteSpace())
				throw new ArgumentException("trust");
			if (name.IsEmptyOrWhiteSpace())
				throw new ArgumentException("name");

			using (var aName = new nsAUTF8String(name))
			using (nsACString aBase64 = new nsACString(), aTrust = new nsACString())
			{
				Instance.AddCertFromBase64(aBase64, aTrust, aName);
			}
		}

	}
}
