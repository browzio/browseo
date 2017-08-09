using Gecko.Interfaces;

namespace Gecko
{
	public enum TextBoundaryType : short
	{
		CharBoundary = (short)nsIAccessiblePivotConsts.CHAR_BOUNDARY,
		WordBoundary = (short)nsIAccessiblePivotConsts.WORD_BOUNDARY,
		LineBoundary = (short)nsIAccessiblePivotConsts.LINE_BOUNDARY,
		AttributeRangeBoundary = (short)nsIAccessiblePivotConsts.ATTRIBUTE_RANGE_BOUNDARY,
	}
}