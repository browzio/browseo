using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko
{
	public sealed class GeckoTreeSelection : ComObject<nsITreeSelection>, IGeckoObjectWrapper
	{
		public static GeckoTreeSelection Create(nsITreeSelection instance)
		{
			return new GeckoTreeSelection(instance);
		}

		private GeckoTreeSelection(nsITreeSelection instance)
			: base(instance)
		{

		}


		/// <summary>
		/// The tree widget for this selection.
		/// </summary>
		public GeckoTreeBoxObject Tree
		{
			get { return Instance.GetTreeAttribute().Wrap(GeckoTreeBoxObject.Create); }
			set { Instance.SetTreeAttribute(value.Instance); }
		}

		/// <summary>
		/// Boolean indicating single selection.
		/// </summary>
		public bool Single
		{
			get { return Instance.GetSingleAttribute(); }
		}


		/// <summary>
		/// The number of rows currently selected in this tree.
		/// </summary>
		public int Count
		{
			get { return Instance.GetCountAttribute(); }
		}

		/// <summary>
		/// Indicates whether or not the row at the specified index is
		/// part of the selection.
		/// </summary>
		public bool IsSelected(int index)
		{
			return Instance.IsSelected(index);
		}

		/// <summary>
		/// Deselect all rows and select the row at the specified index.
		/// </summary>
		public void Select(int index)
		{
			Instance.Select(index);
		}

		/// <summary>
		/// Perform a timed select.
		/// </summary>
		public void TimedSelect(int index, int delay)
		{
			Instance.TimedSelect(index, delay);
		}

		/// <summary>
		/// Toggle the selection state of the row at the specified index.
		/// </summary>
		public void ToggleSelect(int index)
		{
			Instance.ToggleSelect(index);
		}

		/// <summary>
		/// Select the range specified by the indices.  If augment is true,
		/// then we add the range to the selection without clearing out anything
		/// else.  If augment is false, everything is cleared except for the specified range.
		/// </summary>
		public void RangedSelect(int startIndex, int endIndex, bool augment)
		{
			Instance.RangedSelect(startIndex, endIndex, augment);
		}

		/// <summary>
		/// Clears the range.
		/// </summary>
		public void ClearRange(int startIndex, int endIndex)
		{
			Instance.ClearRange(startIndex, endIndex);
		}

		/// <summary>
		/// Clears the selection.
		/// </summary>
		public void ClearSelection()
		{
			Instance.ClearSelection();
		}

		/// <summary>
		/// Inverts the selection.
		/// </summary>
		public void InvertSelection()
		{
			Instance.InvertSelection();
		}

		/// <summary>
		/// Selects all rows.
		/// </summary>
		public void SelectAll()
		{
			Instance.SelectAll();
		}

		/// <summary>
		/// Iterate the selection using these methods.
		/// </summary>
		public int RangeCount
		{
			get { return Instance.GetRangeCount(); }
		}

		/// <summary>Member GetRangeAt </summary>
		/// <param name='i'> </param>
		/// <param name='min'> </param>
		/// <param name='max'> </param>
		public void GetRangeAt(int i, out int min, out int max)
		{
			Instance.GetRangeAt(i, out min, out max);
		}

		/// <summary>
		/// Can be used to invalidate the selection.
		/// </summary>
		public void InvalidateSelection()
		{
			Instance.InvalidateSelection();
		}

		/// <summary>
		/// Called when the row count changes to adjust selection indices.
		/// </summary>
		public void AdjustSelection(int index, int count)
		{
			Instance.AdjustSelection(index, count);
		}

		/// <summary>
		/// This attribute is a boolean indicating whether or not the
		/// "select" event should fire when the selection is changed using
		/// one of our methods.  A view can use this to temporarily suppress
		/// the selection while manipulating all of the indices, e.g., on
		/// a sort.
		/// Note: setting this attribute to false will fire a select event.
		/// </summary>
		public bool SelectEventsSuppressed
		{
			get { return Instance.GetSelectEventsSuppressedAttribute(); }
			set { Instance.SetSelectEventsSuppressedAttribute(value); }
		}

		/// <summary>
		/// The current item (the one that gets a focus rect in addition to being
		/// selected).
		/// </summary>
		public int CurrentIndex
		{
			get { return Instance.GetCurrentIndexAttribute(); }
			set { Instance.SetCurrentIndexAttribute(value); }
		}

		/// <summary>
		/// The current column.
		/// </summary>
		public GeckoTreeColumn CureentColumn
		{
			get { return Instance.GetCurrentColumnAttribute().Wrap(GeckoTreeColumn.Create); }
			set { Instance.SetCurrentColumnAttribute(value.Instance); }
		}

		/// <summary>
		/// The selection "pivot".  This is the first item the user selected as
		/// part of a ranged select.
		/// </summary>
		public int ShiftSelectPivot
		{
			get { return Instance.GetShiftSelectPivotAttribute(); }
		}
	}
}
