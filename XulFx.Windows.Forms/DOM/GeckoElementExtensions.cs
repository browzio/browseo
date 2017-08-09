using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	public static class GeckoElementExtensions
	{
		/// <summary>
		/// UI specific implementation extension method GetBoundingClientRect()
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static Rectangle GetBoundingClientRect(this GeckoElement element)
		{
			nsIDOMClientRect domRect = element.Instance.GetBoundingClientRect();
			if (domRect == null)
				return Rectangle.Empty;

			try
			{
				return new Rectangle(
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
		public static RectangleF GetBoundingClientRectF(this GeckoElement element)
		{
			nsIDOMClientRect domRect = element.Instance.GetBoundingClientRect();
			if (domRect == null)
				return Rectangle.Empty;

			try
			{
				return new RectangleF(
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
		public static Rectangle[] GetClientRects(this GeckoElement element)
		{
			Rectangle[] rects;
			nsIDOMClientRectList domRects = element.Instance.GetClientRects();
			if (domRects == null)
				return new Rectangle[0];

			try
			{
				uint count = domRects.GetLengthAttribute();
				rects = new Rectangle[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					try
					{
						rects[i] = new Rectangle(
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
		public static RectangleF[] GetClientRectsF(this GeckoElement element)
		{
			RectangleF[] rects;
			nsIDOMClientRectList domRects = element.Instance.GetClientRects();
			if (domRects == null)
				return new RectangleF[0];

			try
			{
				uint count = domRects.GetLengthAttribute();
				rects = new RectangleF[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					try
					{
						rects[i] = new RectangleF(
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
		public static Rectangle GetBoundingClientRect(this GeckoRange range)
		{
			nsIDOMClientRect domRect = range.Instance.GetBoundingClientRect();
			if (domRect == null)
				return Rectangle.Empty;

			try
			{
				return new Rectangle(
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
		public static RectangleF GetBoundingClientRectF(this GeckoRange range)
		{
			nsIDOMClientRect domRect = range.Instance.GetBoundingClientRect();
			if (domRect == null)
				return Rectangle.Empty;

			try
			{
				return new RectangleF(
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
		public static Rectangle[] GetClientRects(this GeckoRange range)
		{
			Rectangle[] rects;
			nsIDOMClientRectList domRects = range.Instance.GetClientRects();
			if (domRects == null)
				return new Rectangle[0];

			try
			{
				uint count = domRects.GetLengthAttribute();
				rects = new Rectangle[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					try
					{
						rects[i] = new Rectangle(
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
		public static RectangleF[] GetClientRectsF(this GeckoRange range)
		{
			RectangleF[] rects;
			nsIDOMClientRectList domRects = range.Instance.GetClientRects();
			if (domRects == null)
				return new RectangleF[0];

			try
			{
				uint count = domRects.GetLengthAttribute();
				rects = new RectangleF[count];
				for (uint i = 0; i < count; i++)
				{
					nsIDOMClientRect domRect = domRects.Item(i);
					try
					{
						rects[i] = new RectangleF(
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
			if(window == null)
				throw new ArgumentNullException("window");

			int width, height;
			window.WindowUtils.GetScrollbarSize(flushLayout, out width, out height);
			return new Size(width, height);
		}


	}
}
