using System;

namespace Gecko.Services
{
	public sealed class ContractIDAttribute
		: Attribute
	{
		public ContractIDAttribute(string contractID)
		{
			ContractID = contractID;
		}

		public string ContractID { get; private set; }
	}
}
