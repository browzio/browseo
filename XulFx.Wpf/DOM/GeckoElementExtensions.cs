using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Gecko.DOM
{
	public static class GeckoElementExtensions
	{
		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static Int32Rect GetBoundingClientRect(this GeckoElement element)
		{
			nsIDOMClientRect domRect = element.Instance.GetBoundingClientRect();
			if (domRect == null)
				return Int32Rect.Empty;

			try
			{
				return new Int32Rect(
					(int)Math.Round(domRect.GetLeftAttribute()),
					(int)Math.Round(domRect.GetTopAttribute()),
					(int)Math.Round(domRect.GetWidthAttribute()),
					(int)Math.Round(domRect.GetHeightAttribute())
				);
			}
			finally
			{
				Xpcom.FreeComObject(ref domRect);
			}
		}

		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static Rect GetBoundingClientRectF(this GeckoElement element)
		{
			nsIDOMClientRect domRect = element.Instance.GetBoundingClientRect();
			if (domRect == null)
				return Rect.Empty;

			try
			{
				return new Rect(
					domRect.GetLeftAttribute(),
					domRect.GetTopAttribute(),
					domRect.GetWidthAttribute(),
					domRect.GetHeightAttribute()
				);
			}
			finally
			{
				Xpcom.FreeComObject(ref domRect);
			}
		}

		/// <summary>
		/// UI specific implementation extension method GetClientRects()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static Int32Rect[] GetClientRects(this GeckoElement element)
		{
			Int32Rect[] rects;
			nsIDOMClientRectList domRects = element.Instance.GetClientRects();
			if (domRects == null)
				return new Int32Rect[0];

			try
			{
				uint count = domRects.GetLengthAttribute();
				rects = new Int32Rect[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					try
					{
						rects[i] = new Int32Rect(
							(int)Math.Round(domRect.GetLeftAttribute()),
							(int)Math.Round(domRect.GetTopAttribute()),
							(int)Math.Round(domRect.GetWidthAttribute()),
							(int)Math.Round(domRect.GetHeightAttribute())
						);
					}
					finally
					{
						Xpcom.FreeComObject(ref domRect);
					}
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref domRects);
			}
			return rects;
		}

		/// <summary>
		/// UI specific implementation extension method GetClientRects()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static Rect[] GetClientRectsF(this GeckoElement element)
		{
			Rect[] rects;
			nsIDOMClientRectList domRects = element.Instance.GetClientRects();
			if (domRects == null)
				return new Rect[0];

			try
			{
				uint count = domRects.GetLengthAttribute();
				rects = new Rect[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					try
					{
						rects[i] = new Rect(
							domRect.GetLeftAttribute(),
							domRect.GetTopAttribute(),
							domRect.GetWidthAttribute(),
							domRect.GetHeightAttribute()
						);
					}
					finally
					{
						Xpcom.FreeComObject(ref domRect);
					}
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref domRects);
			}
			return rects;
		}

		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="range"></param>
		/// <returns></returns>
		public static Int32Rect GetBoundingClientRect(this GeckoRange range)
		{
			nsIDOMClientRect domRect = range.Instance.GetBoundingClientRect();
			if (domRect == null)
				return Int32Rect.Empty;

			try
			{
				return new Int32Rect(
					(int)Math.Round(domRect.GetLeftAttribute()),
					(int)Math.Round(domRect.GetTopAttribute()),
					(int)Math.Round(domRect.GetWidthAttribute()),
					(int)Math.Round(domRect.GetHeightAttribute())
				);
			}
			finally
			{
				Xpcom.FreeComObject(ref domRect);
			}
		}

		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="range"></param>
		/// <returns></returns>
		public static Rect GetBoundingClientRectF(this GeckoRange range)
		{
			nsIDOMClientRect domRect = range.Instance.GetBoundingClientRect();
			if (domRect == null)
				return Rect.Empty;

			try
			{
				return new Rect(
					domRect.GetLeftAttribute(),
					domRect.GetTopAttribute(),
					domRect.GetWidthAttribute(),
					domRect.GetHeightAttribute()
				);
			}
			finally
			{
				Xpcom.FreeComObject(ref domRect);
			}
		}

		/// <summary>
		/// UI specific implementation extension method GetClientRects()
		/// </summary>
		/// <param name="range"></param>
		/// <returns></returns>
		public static Int32Rect[] GetClientRects(this GeckoRange range)
		{
			Int32Rect[] rects;
			nsIDOMClientRectList domRects = range.Instance.GetClientRects();
			if (domRects == null)
				return new Int32Rect[0];

			try
			{
				uint count = domRects.GetLengthAttribute();
				rects = new Int32Rect[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					try
					{
						rects[i] = new Int32Rect(
							(int)Math.Round(domRect.GetLeftAttribute()),
							(int)Math.Round(domRect.GetTopAttribute()),
							(int)Math.Round(domRect.GetWidthAttribute()),
							(int)Math.Round(domRect.GetHeightAttribute())
						);
					}
					finally
					{
						Xpcom.FreeComObject(ref domRect);
					}
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref domRects);
			}
			return rects;
		}

		/// <summary>
		/// UI specific implementation extension method GetClientRects()
		/// </summary>
		/// <param name="range"></param>
		/// <returns></returns>
		public static Rect[] GetClientRectsF(this GeckoRange range)
		{
			Rect[] rects;
			nsIDOMClientRectList domRects = range.Instance.GetClientRects();
			if (domRects == null)
				return new Rect[0];

			try
			{
				uint count = domRects.GetLengthAttribute();
				rects = new Rect[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					try
					{
						rects[i] = new Rect(
							domRect.GetLeftAttribute(),
							domRect.GetTopAttribute(),
							domRect.GetWidthAttribute(),
							domRect.GetHeightAttribute()
						);
					}
					finally
					{
						Xpcom.FreeComObject(ref domRect);
					}
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref domRects);
			}
			return rects;
		}

		/// <summary>
		/// Returns the scrollbar width of the window&apos;s scroll frame.
		/// </summary>
		/// <param name="flushLayout">
		/// flushes layout if true. Otherwise, no flush occurs.
		/// </param>
		public static Size GetScrollbarSize(this GeckoWindow window, bool flushLayout)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			int width, height;
			window.WindowUtils.GetScrollbarSize(flushLayout, out width, out height);
			return new Size(width, height);
		}
	}
}
