using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko
{
	public abstract class GeckoTreeView<T> : ComObject<nsITreeView>, IGeckoObjectWrapper
		where T : class, nsITreeView
	{
		private new T _instance;

		internal protected GeckoTreeView(nsITreeView instance)
			: base(instance)
		{

		}

		protected override void Dispose(bool disposing)
		{
			_instance = null;
			base.Dispose(disposing);
		}

		public new T Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _instance;
			}
		}


		/// <summary>
		/// The total number of rows in the tree (including the offscreen rows).
		/// </summary>
		public int RowCount
		{
			get { return Instance.GetRowCountAttribute(); }
		}

		/// <summary>
		/// The selection for this view.
		/// </summary>
		public GeckoTreeSelection Selection
		{
			get { return Instance.GetSelectionAttribute().Wrap(GeckoTreeSelection.Create); }
			set { Instance.SetSelectionAttribute(value.Instance); }
		}

		/// <summary>
		/// An atomized list of properties for a given row.  Each property, x, that
		/// the view gives back will cause the pseudoclass :moz-tree-row-x
		/// to be matched on the pseudoelement ::moz-tree-row.
		/// </summary>
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//void GetRowProperties(int index, [MarshalAs(UnmanagedType.Interface)] nsISupportsArray properties);

		/// <summary>
		/// An atomized list of properties for a given cell.  Each property, x, that
		/// the view gives back will cause the pseudoclass :moz-tree-cell-x
		/// to be matched on the ::moz-tree-cell pseudoelement.
		/// </summary>
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//void GetCellProperties(int row, [MarshalAs(UnmanagedType.Interface)] nsITreeColumn col, [MarshalAs(UnmanagedType.Interface)] nsISupportsArray properties);

		/// <summary>
		/// Called to get properties to paint a column background.  For shading the sort
		/// column, etc.
		/// </summary>
		//MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//void GetColumnProperties([MarshalAs(UnmanagedType.Interface)] nsITreeColumn col, [MarshalAs(UnmanagedType.Interface)] nsISupportsArray properties);

		/// <summary>
		/// Methods that can be used to test whether or not a twisty should be drawn,
		/// and if so, whether an open or closed twisty should be used.
		/// </summary>
		public bool IsContainer(int index)
		{
			return Instance.IsContainer(index);
		}

		/// <summary>Member IsContainerOpen </summary>
		/// <param name='index'> </param>
		/// <returns>A System.Boolean</returns>
		public bool IsContainerOpen(int index)
		{
			return Instance.IsContainerOpen(index);
		}

		/// <summary>Member IsContainerEmpty </summary>
		/// <param name='index'> </param>
		/// <returns>A System.Boolean</returns>
		public bool IsContainerEmpty(int index)
		{
			return Instance.IsContainerEmpty(index);
		}

		/// <summary>
		/// isSeparator is used to determine if the row at index is a separator.
		/// A value of true will result in the tree drawing a horizontal separator.
		/// The tree uses the ::moz-tree-separator pseudoclass to draw the separator.
		/// </summary>
		public bool IsSeparator(int index)
		{
			return Instance.IsSeparator(index);
		}

		/// <summary>
		/// Specifies if there is currently a sort on any column. Used mostly by dragdrop
		/// to affect drop feedback.
		/// </summary>
		public bool IsSorted
		{
			get { return Instance.IsSorted(); }
		}

		/// <summary>
		/// Methods used by the drag feedback code to determine if a drag is allowable at
		/// the current location. To get the behavior where drops are only allowed on
		/// items, such as the mailNews folder pane, always return false when
		/// the orientation is not DROP_ON.
		/// </summary>
		//bool CanDrop(int index, int orientation, [MarshalAs(UnmanagedType.Interface)] nsIDOMDataTransfer dataTransfer);

		/// <summary>
		/// Called when the user drops something on this view. The |orientation| param
		/// specifies before/on/after the given |row|.
		/// </summary>
		//void Drop(int row, int orientation, [MarshalAs(UnmanagedType.Interface)] nsIDOMDataTransfer dataTransfer);

		/// <summary>
		/// Methods used by the tree to draw thread lines in the tree.
		/// getParentIndex is used to obtain the index of a parent row.
		/// If there is no parent row, getParentIndex returns -1.
		/// </summary>
		public int GetParentIndex(int rowIndex)
		{
			return Instance.GetParentIndex(rowIndex);
		}

		/// <summary>
		/// hasNextSibling is used to determine if the row at rowIndex has a nextSibling
		/// that occurs *after* the index specified by afterIndex.  Code that is forced
		/// to march down the view looking at levels can optimize the march by starting
		/// at afterIndex+1.
		/// </summary>
		public bool HasNextSibling(int rowIndex, int afterIndex)
		{
			return Instance.HasNextSibling(rowIndex, afterIndex);
		}

		/// <summary>
		/// The level is an integer value that represents
		/// the level of indentation.  It is multiplied by the width specified in the
		/// :moz-tree-indentation pseudoelement to compute the exact indendation.
		/// </summary>
		public int GetLevel(int index)
		{
			return Instance.GetLevel(index);
		}

		/// <summary>
		/// The image path for a given cell. For defining an icon for a cell.
		/// If the empty string is returned, the :moz-tree-image pseudoelement
		/// will be used.
		/// </summary>
		public string GetImageSrc(int row, GeckoTreeColumn col)
		{
			return nsString.Get(Instance.GetImageSrc, row, col.Instance);
		}

		/// <summary>
		/// The progress mode for a given cell. This method is only called for
		/// columns of type |progressmeter|.
		/// </summary>
		public int GetProgressMode(int row, GeckoTreeColumn col)
		{
			return Instance.GetProgressMode(row, col.Instance);
		}

		/// <summary>
		/// The value for a given cell. This method is only called for columns
		/// of type other than |text|.
		/// </summary>
		public string GetCellValue(int row, GeckoTreeColumn col)
		{
			return nsString.Get(Instance.GetCellValue, row, col.Instance);
		}

		/// <summary>
		/// The text for a given cell.  If a column consists only of an image, then
		/// the empty string is returned.
		/// </summary>
		public string GetCellText(int row, GeckoTreeColumn col)
		{
			return nsString.Get(Instance.GetCellText, row, col.Instance);
		}

		/// <summary>
		/// Called during initialization to link the view to the front end box object.
		/// </summary>
		public void SetTree(GeckoTreeBoxObject tree)
		{
			Instance.SetTree(tree.Instance);
		}

		/// <summary>
		/// Called on the view when an item is opened or closed.
		/// </summary>
		public void ToggleOpenState(int index)
		{
			Instance.ToggleOpenState(index);
		}

		/// <summary>
		/// Called on the view when a header is clicked.
		/// </summary>
		public void CycleHeader(GeckoTreeColumn col)
		{
			Instance.CycleHeader(col.Instance);
		}

		/// <summary>
		/// Should be called from a XUL onselect handler whenever the selection changes.
		/// </summary>
		public void SelectionChanged()
		{
			Instance.SelectionChanged();
		}

		/// <summary>
		/// Called on the view when a cell in a non-selectable cycling column (e.g., unread/flag/etc.) is clicked.
		/// </summary>
		public void CycleCell(int row, GeckoTreeColumn col)
		{
			Instance.CycleCell(row, col.Instance);
		}

		/// <summary>
		/// isEditable is called to ask the view if the cell contents are editable.
		/// A value of true will result in the tree popping up a text field when
		/// the user tries to inline edit the cell.
		/// </summary>
		public bool IsEditable(int row, GeckoTreeColumn col)
		{
			return Instance.IsEditable(row, col.Instance);
		}

		/// <summary>
		/// isSelectable is called to ask the view if the cell is selectable.
		/// This method is only called if the selection style is |cell| or |text|.
		/// XXXvarga shouldn't this be called isCellSelectable?
		/// </summary>
		public bool IsSelectable(int row, GeckoTreeColumn col)
		{
			return Instance.IsSelectable(row, col.Instance);
		}

		/// <summary>
		/// setCellValue is called when the value of the cell has been set by the user.
		/// This method is only called for columns of type other than |text|.
		/// </summary>
		public void SetCellValue(int row, GeckoTreeColumn col, string value)
		{
			nsString.Set(Instance.SetCellValue, row, col.Instance, value);
		}

		/// <summary>
		/// setCellText is called when the contents of the cell have been edited by the user.
		/// </summary>
		public void SetCellText(int row, GeckoTreeColumn col, string value)
		{
			nsString.Set(Instance.SetCellText, row, col.Instance, value);
		}

		/// <summary>
		/// A command API that can be used to invoke commands on the selection.  The tree
		/// will automatically invoke this method when certain keys are pressed.  For example,
		/// when the DEL key is pressed, performAction will be called with the "delete" string.
		/// </summary>
		public void PerformAction(string action)
		{
			Instance.PerformAction(action);
		}

		/// <summary>
		/// A command API that can be used to invoke commands on a specific row.
		/// </summary>
		public void PerformActionOnRow(string action, int row)
		{
			Instance.PerformActionOnRow(action, row);
		}

		/// <summary>
		/// A command API that can be used to invoke commands on a specific cell.
		/// </summary>
		public void PerformActionOnCell(string action, int row, GeckoTreeColumn col)
		{
			Instance.PerformActionOnCell(action, row, col.Instance);
		}
	}
}
