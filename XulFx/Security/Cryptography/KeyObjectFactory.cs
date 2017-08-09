using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko.Security.Cryptography
{
	/// <summary>
	/// DO NOT USE KeyFromString or expose it (according to xulrunner comments)
	/// </summary>
	public sealed class KeyObjectFactory : ComObject<nsIKeyObjectFactory>, IGeckoObjectWrapper
	{
		public static KeyObjectFactory GetService()
		{
			return Xpcom.GetService<nsIKeyObjectFactory>(Contracts.KeyObjectFactory).Wrap(KeyObjectFactory.Create);
		}

		private static KeyObjectFactory Create(nsIKeyObjectFactory instance)
		{
			return new KeyObjectFactory(instance);
		}

		private KeyObjectFactory(nsIKeyObjectFactory instance)
			: base(instance)
		{

		}

		public KeyObject KeyFromString(AlgorithmType algorithm, string key)
		{
			using (var aKey = new nsACString(key))
			{
				return Instance.KeyFromString((short)algorithm, aKey).Wrap(KeyObject.Create);
			}
		}
	}
}
