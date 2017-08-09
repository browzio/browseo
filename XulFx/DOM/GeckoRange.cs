using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	public sealed class GeckoRange : ComObject<nsIDOMRange>, ICloneable, IGeckoObjectWrapper
	{
		public static GeckoRange Create(nsIDOMRange range)
		{
			return new GeckoRange(range);
		}

		private GeckoRange(nsIDOMRange range)
			: base(range)
		{

		}

		#region ICloneable

		public object Clone()
		{
			return CloneRange();
		}

		#endregion

		public GeckoNode StartContainer
		{
			get { return Instance.GetStartContainerAttribute().Wrap(GeckoNode.Create); }
		}

		public int StartOffset { get { return Instance.GetStartOffsetAttribute(); } }

		public GeckoNode EndContainer
		{
			get { return Instance.GetEndContainerAttribute().Wrap(GeckoNode.Create); }
		}

		public int EndOffset { get { return Instance.GetEndOffsetAttribute(); } }

		public bool Collapsed { get { return Instance.GetCollapsedAttribute(); } }

		public GeckoNode CommonAncestorContainer
		{
			get { return Instance.GetCommonAncestorContainerAttribute().Wrap(GeckoNode.Create); }
		}

		public void SetStart(GeckoNode node, int offset)
		{
			Instance.SetStart(node.Instance, offset);
		}

		public void SetEnd(GeckoNode node, int offset)
		{
			Instance.SetEnd(node.Instance, offset);
		}

		public void SetStartBefore(GeckoNode node)
		{
			Instance.SetStartBefore(node.Instance);
		}

		public void SetStartAfter(GeckoNode node)
		{
			Instance.SetStartAfter(node.Instance);
		}

		public void SetEndBefore(GeckoNode node)
		{
			Instance.SetEndBefore(node.Instance);
		}

		public void SetEndAfter(GeckoNode node)
		{
			Instance.SetEndAfter(node.Instance);
		}

		public void Collapse(bool toStart)
		{
			Instance.Collapse(toStart);
		}

		public void SelectNode(GeckoNode node)
		{
			Instance.SelectNode(node.Instance);
		}

		public void SelectNodeContents(GeckoNode node)
		{
			Instance.SelectNodeContents(node.Instance);
		}

		public short CompareBoundaryPoints(ushort how, GeckoRange sourceRange)
		{
			return Instance.CompareBoundaryPoints(how, sourceRange.Instance);
		}

		public void DeleteContents()
		{
			Instance.DeleteContents();
		}

		public GeckoNode ExtractContents()
		{
			return Instance.ExtractContents().Wrap(GeckoNode.Create);
		}

		public GeckoNode CloneContents()
		{
			return Instance.CloneContents().Wrap(GeckoNode.Create);
		}

		public void InsertNode(GeckoNode newNode)
		{
			Instance.InsertNode(newNode.Instance);
		}

		public void SurroundContents(GeckoNode newParent)
		{
			Instance.SurroundContents(newParent.Instance);
		}

		public GeckoRange CloneRange()
		{
			return Instance.CloneRange().Wrap(GeckoRange.Create);
		}

		public override string ToString()
		{
			return nsString.Get(Instance.ToString);
		}

		public void Detach()
		{
			Instance.Detach();
		}

		//// Get Rectange which surrounds entire selection.
		//public Rectangle BoundingClientRect
		//{
		//	get
		//	{
		//		nsIDOMClientRect domRect = Instance.GetBoundingClientRect();
		//		var r = new Rectangle((int)domRect.GetLeftAttribute(), (int)domRect.GetTopAttribute(), (int)domRect.GetWidthAttribute(), (int)domRect.GetHeightAttribute());
		//		return r;
		//	}
		//}

		///// <summary>
		///// Get the Individual rectangles that make up a selection.
		///// </summary>
		//public IEnumerable<Rectangle> ClientRects
		//{
		//	get
		//	{
		//		//List<Rectangle> retVal = new List<Rectangle>();
		//		nsIDOMClientRectList domRectangles = Instance.GetClientRects();
		//		for (uint i = 0; i < domRectangles.GetLengthAttribute(); i++)
		//		{
		//			nsIDOMClientRect domRect = domRectangles.Item(i);
		//			//retVal.Add(new Rectangle((int)domRect.GetLeftAttribute(), (int)domRect.GetTopAttribute(), (int)domRect.GetWidthAttribute(), (int)domRect.GetHeightAttribute());
		//			yield return new Rectangle((int)domRect.GetLeftAttribute(), (int)domRect.GetTopAttribute(), (int)domRect.GetWidthAttribute(), (int)domRect.GetHeightAttribute());
		//		}

		//		yield break;
		//	}
		//}

		public bool IsPointInRange(GeckoNode node, int offset)
		{
			return Instance.IsPointInRange(node.Instance, offset);
		}
	}
}
