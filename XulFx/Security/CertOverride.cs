using System;
using Gecko.Interfaces;

namespace Gecko.Security
{
	[Flags]
	public enum CertOverride
	{
		None = 0,
		Mismatch = nsICertOverrideServiceConsts.ERROR_MISMATCH,
		Time = nsICertOverrideServiceConsts.ERROR_TIME,
		Untrusted = nsICertOverrideServiceConsts.ERROR_UNTRUSTED,
	}
}
