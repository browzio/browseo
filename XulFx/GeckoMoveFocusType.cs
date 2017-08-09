using Gecko.Interfaces;
namespace Gecko
{
	/// <summary>
	/// Move focus type.
	/// </summary>
	public enum GeckoMoveFocusType : uint
	{
		/// <summary>
		/// Move focus forward one element, used when pressing TAB.
		/// </summary>
		Forward = nsIFocusManagerConsts.MOVEFOCUS_FORWARD,
		/// <summary>
		/// Move focus backward one element, used when pressing Shift+TAB.
		/// </summary>
		Backward = nsIFocusManagerConsts.MOVEFOCUS_BACKWARD,
		/// <summary>
		/// Move focus forward to the next frame document, used when pressing F6.
		/// </summary>
		ForwardDocument = nsIFocusManagerConsts.MOVEFOCUS_FORWARDDOC,
		/// <summary>
		/// Move focus backward to the previous frame document, used when pressing Shift+F6.
		/// </summary>
		BackwardDocument = nsIFocusManagerConsts.MOVEFOCUS_BACKWARDDOC,
		/// <summary>
		/// Move focus to the first focusable element.
		/// </summary>
		First = nsIFocusManagerConsts.MOVEFOCUS_FIRST,
		/// <summary>
		/// Move focus to the last focusable element.
		/// </summary>
		Last = nsIFocusManagerConsts.MOVEFOCUS_LAST,
		/// <summary>
		/// Move focus to the root element in the document.
		/// </summary>
		Root = nsIFocusManagerConsts.MOVEFOCUS_ROOT,
		/// <summary>
		/// Move focus to a link at the position of the caret. This is a special value
		/// used to focus links as the caret moves over them in caret browsing mode.
		/// </summary>
		Caret = nsIFocusManagerConsts.MOVEFOCUS_CARET,


	}
}
