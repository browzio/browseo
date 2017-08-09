using System;

namespace Gecko
{
	public enum GeckoMouseButton
	{
		None = -1,
		Left = 0,
		Middle = 1,
		Right = 2,
		Back = 3,
		Forward = 4,

	}

	[Flags]
	public enum MouseButtons
	{
		None = 0,
		Left = 1,
		Right = 2,
		Middle = 4,
		Back = 8,
		Forward = 16,

	}
}
