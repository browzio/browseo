using Gecko.Interfaces;

namespace Gecko.DOM
{
	public enum KeyLocation : uint
	{
		Standard = nsIDOMKeyEventConsts.DOM_KEY_LOCATION_STANDARD,
		Left = nsIDOMKeyEventConsts.DOM_KEY_LOCATION_LEFT,
		Right = nsIDOMKeyEventConsts.DOM_KEY_LOCATION_RIGHT,
		NumPad = nsIDOMKeyEventConsts.DOM_KEY_LOCATION_NUMPAD,

	}
}
