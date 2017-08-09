using Gecko.Interfaces;
using Gecko.Interop;
using System;


namespace Gecko.Security.Cryptography
{
	public sealed class CryptoHash : ComObject<nsICryptoHash>, IGeckoObjectWrapper
	{
		public static CryptoHash Create(HashAlgorithm algorithm)
		{
			nsICryptoHash hash = Xpcom.CreateInstance<nsICryptoHash>(Contracts.Hash);
			hash.Init((uint)algorithm);
			return hash.Wrap(CryptoHash.Create);
		}

		public static CryptoHash Create(nsICryptoHash instance)
		{
			return new CryptoHash(instance);
		}

		private CryptoHash(nsICryptoHash instance)
			: base(instance)
		{

		}


		public void Update(byte[] array)
		{
			Instance.Update(array, (uint)array.Length);
		}

		public void UpdateFromStream(Gecko.IO.InputStream stream, int count)
		{
			Instance.UpdateFromStream(stream.Instance, (uint)count);
		}

		public void UpdateFromStream(Gecko.IO.InputStream stream)
		{
			Instance.UpdateFromStream(stream.Instance, 0xFFFFFFFF);
		}


		/// <summary>
		/// Return hash in base64 form
		/// </summary>
		/// <returns></returns>
		public string FinishBase64()
		{
			return nsString.Get(Instance.Finish, true);
		}

		public byte[] Finish()
		{
			byte[] ret;
			using (var str = new nsACString())
			{
				Instance.Finish(false, str);
				ret = str.GetRawData();
			}
			return ret;
		}

	}
}
