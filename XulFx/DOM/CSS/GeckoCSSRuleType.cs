using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	/// <summary>
	/// Specifies the various types of rules for a <see cref="GeckoStyleRule"/>.
	/// </summary>
	public enum GeckoCSSRuleType
	{
		Unknown = 0,
		Style = 1,
		CharSet = 2,
		Import = 3,
		Media = 4,
		FontFace = 5,
		Page = 6,
	}
}
