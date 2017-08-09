using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.Security
{
	public sealed class X509CertificateValidity : ComObject<nsIX509CertValidity>, IGeckoObjectWrapper
	{
		public static X509CertificateValidity Create(nsIX509CertValidity instance)
		{
			return new X509CertificateValidity(instance);
		}

		private X509CertificateValidity(nsIX509CertValidity instance)
			: base(instance)
		{

		}

		public ulong NotBefore
		{
			get { return Instance.GetNotBeforeAttribute(); }
		}

		public string NotBeforeLocalTime
		{
			get { return nsString.Get(Instance.GetNotBeforeLocalTimeAttribute); }
		}

		public string NotBeforeLocalDay
		{
			get { return nsString.Get(Instance.GetNotBeforeLocalDayAttribute); }
		}

		public string NotBeforeGMT
		{
			get { return nsString.Get(Instance.GetNotBeforeGMTAttribute); }
		}
	
		public ulong NotAfter
		{
			get { return Instance.GetNotAfterAttribute(); }
		}

		public string NotAfterLocalTime
		{
			get { return nsString.Get(Instance.GetNotAfterLocalTimeAttribute); }
		}

		public string NotAfterLocalDay
		{
			get { return nsString.Get(Instance.GetNotAfterLocalDayAttribute); }
		}

		public string NotAfterGMT
		{
			get { return nsString.Get(Instance.GetNotAfterGMTAttribute); }
		}

	}
}
