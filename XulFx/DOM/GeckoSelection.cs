using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	/// <summary>
	/// Manipulates and queries the current selected range of nodes within the document.
	/// </summary>
	public class GeckoSelection : ComObject<nsISelection>, IGeckoObjectWrapper
	{
		public static GeckoSelection Create(nsISelection selection)
		{
			return new GeckoSelection(selection);
		}

		protected GeckoSelection(nsISelection selection)
			: base(selection)
		{

		}
		
		/// <summary>
		/// Gets the node in which the selection begins.
		/// </summary>
		public GeckoNode AnchorNode
		{
			get { return Instance.GetAnchorNodeAttribute().Wrap(GeckoNode.Create); }
		}
		
		/// <summary>
		/// Gets the offset within the (text) node where the selection begins.
		/// </summary>
		public int AnchorOffset
		{
			get { return Instance.GetAnchorOffsetAttribute(); }
		}
		
		/// <summary>
		/// Gets the node in which the selection ends.
		/// </summary>
		public GeckoNode FocusNode
		{
			get { return Instance.GetFocusNodeAttribute().Wrap(GeckoNode.Create); }
		}
		
		/// <summary>
		/// Gets the offset within the (text) node where the selection ends.
		/// </summary>
		public int FocusOffset
		{
			get { return Instance.GetFocusOffsetAttribute(); }
		}
		
		/// <summary>
		/// Gets whether the selection is collapsed or not.
		/// </summary>
		public bool IsCollapsed
		{
			get { return Instance.GetIsCollapsedAttribute(); }
		}
		
		/// <summary>
		/// Gets the number of ranges in the Instance.
		/// </summary>
		public int RangeCount
		{
			get { return Instance.GetRangeCountAttribute(); }
		}
		
		/// <summary>
		/// Returns the range at the specified index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public GeckoRange GetRangeAt(int index)
		{
			return Instance.GetRangeAt(index).Wrap(GeckoRange.Create);
		}
		
		/// <summary>
		/// Collapses the selection to a single point, at the specified offset in the given DOM node. When the selection is collapsed, and the content is focused and editable, the caret will blink there.
		/// </summary>
		/// <param name="parentNode"></param>
		/// <param name="offset"></param>
		public void Collapse(GeckoNode parentNode, int offset)
		{
			Instance.Collapse(parentNode.Instance, offset);
		}
		
		/// <summary>
		/// Extends the selection by moving the selection end to the specified node and offset, preserving the selection begin position. The new selection end result will always be from the anchorNode to the new focusNode, regardless of direction.
		/// </summary>
		/// <param name="parentNode">The node where the selection will be extended to.</param>
		/// <param name="offset">Where in node to place the offset in the new selection end.</param>
		public void Extend(GeckoNode parentNode, int offset)
		{
			Instance.Extend(parentNode.Instance, offset);
		}
		
		/// <summary>
		/// Collapses the whole selection to a single point at the start of the current selection (irrespective of direction). If content is focused and editable, the caret will blink there.
		/// </summary>
		public void CollapseToStart()
		{
			Instance.CollapseToStart();
		}
		
		/// <summary>
		/// Collapses the whole selection to a single point at the end of the current selection (irrespective of direction). If content is focused and editable, the caret will blink there.
		/// </summary>
		public void CollapseToEnd()
		{
			Instance.CollapseToEnd();
		}
		
		/// <summary>
		/// Returns whether the specified node is part of the Instance.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="partlyContained">True if the function should return true when some part of the node is contained with the selection; when false, the function only returns true when the entire node is contained within the Instance.</param>
		/// <returns></returns>
		public bool ContainsNode(GeckoNode node, bool partlyContained)
		{
			return Instance.ContainsNode(node.Instance, partlyContained);
		}
		
		/// <summary>
		/// Adds all children of the specified node to the Instance.
		/// </summary>
		/// <param name="parentNode"></param>
		public void SelectAllChildren(GeckoNode parentNode)
		{
			Instance.SelectAllChildren(parentNode.Instance);
		}
		
		/// <summary>
		/// Adds a range to the current Instance.
		/// </summary>
		/// <param name="range"></param>
		public void AddRange(GeckoRange range)
		{
			Instance.AddRange(range.Instance);
		}
		
		/// <summary>
		/// Removes a range from the current Instance.
		/// </summary>
		/// <param name="range"></param>
		public void RemoveRange(GeckoRange range)
		{
			Instance.RemoveRange(range.Instance);
		}
		
		/// <summary>
		/// Removes all ranges from the current Instance.
		/// </summary>
		public void RemoveAllRanges()
		{
			Instance.RemoveAllRanges();
		}
		
		/// <summary>
		/// Deletes this selection from the document.
		/// </summary>
		public void DeleteFromDocument()
		{
			Instance.DeleteFromDocument();
		}

		/// <summary>
		/// Match this up with EndbatchChanges. Will stop ui updates while multiple selection methods are called.
		/// </summary>
		public void StartBatchChanges()
		{
			using (var selection = Xpcom.QueryInterface2<nsISelectionPrivate>(Instance))
			{
				selection.Instance.StartBatchChanges();
			}
		}

		/// <summary>
		/// Match this up with startBatchChanges
		/// </summary>
		public void EndBatchChanges()
		{
			using (var selection = Xpcom.QueryInterface2<nsISelectionPrivate>(Instance))
			{
				selection.Instance.EndBatchChanges();
			}
		}

		/// <summary>
		/// Scrolls a region of the selection, so that it is visible in
		/// the scrolled view.
		///
		/// @param aRegion - the region inside the selection to scroll into view
		/// (see selection region constants defined in
		/// nsISelectionController).
		/// @param aIsSynchronous - when true, scrolls the selection into view
		/// before returning. If false, posts a request which
		/// is processed at some point after the method returns.
		/// @param aVPercent - how to align the frame vertically. A value of 0
		/// means the frame's upper edge is aligned with the top edge
		/// of the visible area. A value of 100 means the frame's
		/// bottom edge is aligned with the bottom edge of
		/// the visible area. For values in between, the point
		/// "aVPercent" down the frame is placed at the point
		/// "aVPercent" down the visible area. A value of 50 centers
		/// the frame vertically. A value of -1 means move
		/// the frame the minimum amount necessary in order for
		/// the entire frame to be visible vertically (if possible).
		/// @param aHPercent - how to align the frame horizontally. A value of 0
		/// means the frame's left edge is aligned with the left
		/// edge of the visible area. A value of 100 means the
		/// frame's right edge is aligned with the right edge of
		/// the visible area. For values in between, the point
		/// "aHPercent" across the frame is placed at the point
		/// "aHPercent" across the visible area. A value of 50
		/// centers the frame horizontally . A value of -1 means
		/// move the frame the minimum amount necessary in order
		/// for the entire frame to be visible horizontally
		/// (if possible).
		/// </summary>
		public void ScrollIntoView(short aRegion, bool aIsSynchronous, short aVPercent, short aHPercent)
		{
			using (var selection = Xpcom.QueryInterface2<nsISelectionPrivate>(Instance))
			{
				selection.Instance.ScrollIntoView(aRegion, aIsSynchronous, aVPercent, aHPercent);
			}
		}

		/// <summary>
		/// Returns the whole selection as a plain text string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return nsString.Get(Instance.ToString);
		}
	}
}
