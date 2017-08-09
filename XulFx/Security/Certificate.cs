using System;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gecko.Security
{
	public sealed class Certificate : ComObject<nsIX509Cert>, IGeckoObjectWrapper
	{
		public static Certificate Create(nsIX509Cert instance)
		{
			return new Certificate(instance);
		}

		private Certificate(nsIX509Cert instance)
			: base(instance)
		{

		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}




		public string Nickname
		{
			get { return nsString.Get(Instance.GetNicknameAttribute); }
		}

		public string EmailAddress
		{
			get { return nsString.Get(Instance.GetEmailAddressAttribute); }
		}

		public void GetEmailAddresses(out uint length, out IntPtr addresses)
		{
			Instance.GetEmailAddresses(out length, out addresses);
		}

		public bool ContainsEmailAddress(string email)
		{
			return nsString.Pass<bool>(Instance.ContainsEmailAddress, email);
		}

		public string SubjectName
		{
			get { return nsString.Get(Instance.GetSubjectNameAttribute); }
		}

		public string CommonName
		{
			get { return nsString.Get(Instance.GetCommonNameAttribute); }
		}

		public string Organization
		{
			get { return nsString.Get(Instance.GetOrganizationAttribute); }
		}

		public string OrganizationalUnit
		{
			get { return nsString.Get(Instance.GetOrganizationalUnitAttribute); }
		}

		public string Sha1Fingerprint
		{
			get { return nsString.Get(Instance.GetSha1FingerprintAttribute); }
		}

		public string TokenName
		{
			get { return nsString.Get(Instance.GetTokenNameAttribute); }
		}

		public string IssuerName
		{
			get { return nsString.Get(Instance.GetIssuerNameAttribute); }
		}

		public string SerialNumber
		{
			get { return nsString.Get(Instance.GetSerialNumberAttribute); }
		}

		public string IssuerCommonName
		{
			get { return nsString.Get(Instance.GetIssuerCommonNameAttribute); }
		}

		public string IssuerOrganization
		{
			get { return nsString.Get(Instance.GetIssuerOrganizationAttribute); }
		}

		public string IssuerOrganizationUnit
		{
			get { return nsString.Get(Instance.GetIssuerOrganizationUnitAttribute); }
		}

		public Certificate Issuer
		{
			get { return Create(Instance.GetIssuerAttribute()); }
		}

		public X509CertificateValidity Validity
		{
			get { return Instance.GetValidityAttribute().Wrap(X509CertificateValidity.Create); }
		}

		public string DbKey
		{
			get { return nsString.Get(Instance.GetDbKeyAttribute); }
		}

		/// <summary>
		/// Gets a human readable identifier to label this certificate.
		/// </summary>
		public string WindowTitle
		{
			get { return nsString.Get(Instance.GetWindowTitleAttribute); }
		}

		public ReadOnlyCollection<Certificate> Chain
		{
			get
			{
				return Instance.GetChain()
					.Wrap<nsIArray, GeckoArray<Certificate, nsIX509Cert>, nsIX509Cert, Certificate>(
						GeckoArray<Certificate, nsIX509Cert>.Create, Certificate.Create
					).AsReadOnlyCollection();
			}
		}

		public ASN1Object ASN1Structure
		{
			get { return Instance.GetASN1StructureAttribute().Wrap(ASN1Object.Create); }
		}

		//public void GetRawDER(ref uint length, ref byte[] data)
		//{
		//	Instance.GetRawDER(ref length, ref data);
		//}

		public uint CertType
		{
			get { return Instance.GetCertTypeAttribute(); }
		}

		public void MarkForPermDeletion()
		{
			Instance.MarkForPermDeletion();
		}

		public IntPtr GetCert()
		{
			return Instance.GetCert();
		}

		//void RequestUsagesArrayAsync([MarshalAs(UnmanagedType.Interface)] nsICertVerificationListener cvl);


		//public void ExportAsCMS(uint chainMode, ref uint length, ref byte[] data)
		//{
		//	Instance.ExportAsCMS(chainMode, ref length, ref data);
		//}

		public bool IsSelfSigned
		{
			get { return Instance.GetIsSelfSignedAttribute(); }
		}

		//public void GetAllTokenNames(ref uint length, ref System.IntPtr[] tokenNames)
		//{
		//	Instance.GetAllTokenNames(ref length, ref tokenNames);
		//}

	}
}
