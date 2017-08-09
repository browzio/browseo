using System;

namespace Gecko.Security.Cryptography
{
	/// <summary>
	/// Hash Algorith type
	/// </summary>
	public enum HashAlgorithm
	{
		/// <summary>
		/// Message Digest Algorithm 2
		/// </summary>
		MD2 = 1,
		/// <summary>
		/// Message-Digest algorithm 5
		/// </summary>
		MD5 = 2,
		/// <summary>
		/// Secure Hash Algorithm 1
		/// </summary>
		Sha1 = 3,
		/// <summary>
		/// Secure Hash Algorithm 256
		/// </summary>
		Sha256 = 4,
		/// <summary>
		/// Secure Hash Algorithm 384
		/// </summary>
		Sha384 = 5,
		/// <summary>
		/// Secure Hash Algorithm 512
		/// </summary>
		Sha512 = 6
	}
}
