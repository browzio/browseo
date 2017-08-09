using Gecko.Interfaces;

namespace Gecko
{
	/// <summary>
	/// 
	/// </summary>
	public enum GeckoFocusFlags : uint
	{
		/// <summary>
		/// No flags.
		/// </summary>
		None = 0,
		/// <summary>
		/// Raise the window when switching focus
		/// </summary>
		Raise = nsIFocusManagerConsts.FLAG_RAISE,
		/// <summary>
		/// Do not scroll the element to focus into view.
		/// </summary>
		NoScroll = nsIFocusManagerConsts.FLAG_NOSCROLL,
		/// <summary>
		/// If attempting to change focus in a window that is not focused, do not
		/// switch focus to that window. Instead, just update the focus within
		/// that window and leave the application focus as is. This flag will have
		/// no effect if a child window is focused and an attempt is made to adjust
		/// the focus in an ancestor, as the frame must be switched in this case.
		/// </summary>
		NoSwitchFrame = nsIFocusManagerConsts.FLAG_NOSWITCHFRAME,
		/// <summary>
		/// This flag is only used when passed to moveFocus. If set, focus is never
		/// moved to the parent frame of the starting element&apos;s document, instead
		/// iterating around to the beginning of that document again. Child frames
		/// are navigated as normal.
		/// </summary>
		NoParentFrame = nsIFocusManagerConsts.FLAG_NOPARENTFRAME,
		/// <summary>
		/// Focus is changing due to a mouse operation, for instance the mouse was clicked on an element.
		/// </summary>
		ByMouse = nsIFocusManagerConsts.FLAG_BYMOUSE,
		/// <summary>
		/// Focus is changing due to a key operation, for instance pressing the tab key.
		/// This flag would normally be passed when move type forward or backward is used.
		/// </summary>
		ByKey = nsIFocusManagerConsts.FLAG_BYKEY,
		/// <summary>
		/// Focus is changing due to a call to MoveFocus(). This flag will be implied
		/// when MoveFocus() is called except when one of the other mechanisms
		/// (mouse or key) is specified, or when the move type is root or caret.
		/// </summary>
		ByMoveFocus = nsIFocusManagerConsts.FLAG_BYMOVEFOCUS,
		/// <summary>
		/// Always show the focus ring or other indicator of focus, regardless of other state.
		/// </summary>
		ShowRing = nsIFocusManagerConsts.FLAG_SHOWRING,

	}
}
