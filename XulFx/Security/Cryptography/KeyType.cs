using System;

namespace Gecko.Security.Cryptography
{
	public enum KeyType : short
	{
		/// <summary>
		/// SYM_KEY
		/// </summary>
		Symmetric = 1,
		/// <summary>
		/// PRIVATE_KEY
		/// </summary>
		Private = 2,
		/// <summary>
		/// PUBLIC_KEY
		/// </summary>
		Public = 3,
	}
}
