using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko.Security.Cryptography
{
	public sealed class KeyObject : ComObject<nsIKeyObject>, IGeckoObjectWrapper
	{
		public static KeyObject Create(nsIKeyObject instance)
		{
			return new KeyObject(instance);
		}

		public KeyObject(nsIKeyObject instance)
			: base(instance)
		{

		}


	}
}
