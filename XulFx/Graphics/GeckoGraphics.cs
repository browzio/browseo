using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Javascript;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Gecko
{
	public sealed class GeckoGraphics
	{
		/// <summary>
		/// Creates the copy of the content of the element as an image and returns its equivalent string representation that is encoded with base-64 digits.
		/// </summary>
		/// <param name="image">An element to copy into the base64-string; the specification permits any image element (that is, &lt;img&gt;, &lt;canvas&gt;, and &lt;video&gt;).</param>
		/// <param name="x">The X coordinate of the top left corner of the sub-rectangle of the source image.</param>
		/// <param name="y">The Y coordinate of the top left corner of the sub-rectangle of the source image.</param>
		/// <param name="width">The width of the sub-rectangle of the source image.</param>
		/// <param name="height">The height of the sub-rectangle of the source image.</param>
		/// <param name="format">The type for the image format to return.</param>
		/// <returns>A BASE64-encoded image string.</returns>
		public static string CopyGraphicsElementToBase64String(GeckoHTMLElement image, float x, float y, float width, float height, GeckoGraphicsImageFormat format)
		{
			return CopyGraphicsElementToBase64String(image, x, y, width, height, format, 0.92f);
		}

		/// <summary>
		/// Creates the copy of the content of the element as an image and returns its equivalent string representation that is encoded with base-64 digits.
		/// </summary>
		/// <param name="image">An element to copy into the base64-string; the specification permits any image element (that is, &lt;img&gt;, &lt;canvas&gt;, and &lt;video&gt;).</param>
		/// <param name="x">The X coordinate of the top left corner of the sub-rectangle of the source image.</param>
		/// <param name="y">The Y coordinate of the top left corner of the sub-rectangle of the source image.</param>
		/// <param name="width">The width of the sub-rectangle of the source image.</param>
		/// <param name="height">The height of the sub-rectangle of the source image.</param>
		/// <param name="format">The type for the image format to return.</param>
		/// <param name="imageQuality">A Number between 0 and 1 indicating image quality if the requested type is image/jpeg or image/webp.</param>
		/// <returns>A BASE64-encoded image string.</returns>
		public static string CopyGraphicsElementToBase64String(GeckoHTMLElement image, float x, float y, float width, float height, GeckoGraphicsImageFormat format, float imageQuality)
		{
			if (width <= 0)
				throw new ArgumentException("width");

			if (height <= 0)
				throw new ArgumentException("height");

			if (imageQuality <= 0.0f || imageQuality > 1.0f)
				throw new ArgumentOutOfRangeException("imageQuality");

			string code = string.Format(CultureInfo.InvariantCulture, @"
						(function(element, canvas, ctx, rect)
						{{
							canvas = element.ownerDocument.createElement('canvas');
							canvas.width = {2};
							canvas.height = {3};
							ctx = canvas.getContext('2d');
							ctx.drawImage(element.wrappedJSObject, -{0}, -{1});
							return canvas.toDataURL('{4}', {5});
						}}
						)(this)", x, y, width, height, GetImageMimeType(format), imageQuality.ToString(NumberFormatInfo.InvariantInfo));

			string data;
			if (TryEvalCopyingToBase64String(image.Instance, code, out data))
				return data;
			return null;
		}

		/// <summary>
		/// Creates the copy of the content of the element as an image and writes its binary representation into the output stream.
		/// </summary>
		/// <param name="image">An element to copy into the stream; the specification permits any image element (that is, &lt;img&gt;, &lt;canvas&gt;, and &lt;video&gt;).</param>
		/// <param name="outputStream"></param>
		/// <param name="x">The X coordinate of the top left corner of the sub-rectangle of the source image.</param>
		/// <param name="y">The Y coordinate of the top left corner of the sub-rectangle of the source image.</param>
		/// <param name="width">The width of the sub-rectangle of the source image.</param>
		/// <param name="height">The height of the sub-rectangle of the source image.</param>
		/// <param name="format">The type for the image format to return.</param>
		/// <returns>true if the operation is successful; otherwise, false.</returns>
		public static bool TryCopyGraphicsElementToStream(GeckoHTMLElement image, Stream outputStream, float x, float y, float width, float height, GeckoGraphicsImageFormat format)
		{
			return TryCopyGraphicsElementToStream(image, outputStream, x, y, width, height, format, 0.92f);
		}

		/// <summary>
		/// Creates the copy of the content of the element as an image and writes its binary representation into the output stream.
		/// </summary>
		/// <param name="image">An element to copy into the stream; the specification permits any image element (that is, &lt;img&gt;, &lt;canvas&gt;, and &lt;video&gt;).</param>
		/// <param name="outputStream"></param>
		/// <param name="x">The X coordinate of the top left corner of the sub-rectangle of the source image.</param>
		/// <param name="y">The Y coordinate of the top left corner of the sub-rectangle of the source image.</param>
		/// <param name="width">The width of the sub-rectangle of the source image.</param>
		/// <param name="height">The height of the sub-rectangle of the source image.</param>
		/// <param name="format">The type for the image format to return.</param>
		/// <param name="imageQuality">A Number between 0 and 1 indicating image quality if the requested type is image/jpeg or image/webp.</param>
		/// <returns>true if the operation is successful; otherwise, false.</returns>
		public static bool TryCopyGraphicsElementToStream(GeckoHTMLElement image, Stream outputStream, float x, float y, float width, float height, GeckoGraphicsImageFormat format, float imageQuality)
		{
			if (width <= 0)
				throw new ArgumentException("width");

			if (height <= 0)
				throw new ArgumentException("height");

			if (imageQuality <= 0.0f || imageQuality > 1.0f)
				throw new ArgumentOutOfRangeException("imageQuality");

			string code = string.Format(CultureInfo.InvariantCulture, @"
						(function(element, canvas, ctx, rect)
						{{
							canvas = element.ownerDocument.createElement('canvas');
							canvas.width = {2};
							canvas.height = {3};
							ctx = canvas.getContext('2d');
							ctx.drawImage(element.wrappedJSObject, -{0}, -{1});
							return canvas.toDataURL('{4}', {5});
						}}
						)(this)", x, y, width, height, GetImageMimeType(format), imageQuality.ToString(NumberFormatInfo.InvariantInfo));

			return TryEvalCopyingToStream(image.Instance, code, outputStream);
		}

		/// <summary>
		/// Creates the copy of the content of the window as an image and returns its equivalent string representation that is encoded with base-64 digits.
		/// </summary>
		/// <param name="window">An window to copy into the base64-string.</param>
		/// <param name="x">The X coordinate of the top left corner of the sub-rectangle of the source window.</param>
		/// <param name="y">The Y coordinate of the top left corner of the sub-rectangle of the source window.</param>
		/// <param name="width">The width of the sub-rectangle of the source window.</param>
		/// <param name="height">The height of the sub-rectangle of the source window.</param>
		/// <param name="format">The type for the image format to return.</param>
		public static string CopyWindowToBase64String(GeckoWindow window, float x, float y, float width, float height, GeckoGraphicsImageFormat format)
		{
			return CopyWindowToBase64String(window, x, y, width, height, format, 0.92f);
		}

		/// <summary>
		/// Creates the copy of the content of the window as an image and returns its equivalent string representation that is encoded with base-64 digits.
		/// </summary>
		/// <param name="window">An window to copy into the base64-string.</param>
		/// <param name="x">The X coordinate of the top left corner of the sub-rectangle of the source window.</param>
		/// <param name="y">The Y coordinate of the top left corner of the sub-rectangle of the source window.</param>
		/// <param name="width">The width of the sub-rectangle of the source window.</param>
		/// <param name="height">The height of the sub-rectangle of the source window.</param>
		/// <param name="imageQuality">A Number between 0 and 1 indicating image quality if the requested type is image/jpeg or image/webp.</param>
		/// <param name="format">The type for the image format to return.</param>
		public static string CopyWindowToBase64String(GeckoWindow window, float x, float y, float width, float height, GeckoGraphicsImageFormat format, float imageQuality)
		{
			if (width <= 0)
				throw new ArgumentException("width");

			if (height <= 0)
				throw new ArgumentException("height");

			if (imageQuality <= 0.0f || imageQuality > 1.0f)
				throw new ArgumentOutOfRangeException("imageQuality");

			uint flags = (uint)(nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_DO_NOT_FLUSH
							| nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_ASYNC_DECODE_IMAGES
							| nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_USE_WIDGET_LAYERS);

			string code = string.Format(CultureInfo.InvariantCulture, @"(function(canvas, ctx)
						{{
							canvas = window.document.createElement('canvas');
							canvas.width = {2};
							canvas.height = {3};
							ctx = canvas.getContext('2d');
							ctx.drawWindow(window, {0}, {1}, {2}, {3}, 'rgb(255,255,255)', {4});
							return canvas.toDataURL('{5}', {6});
						}}
						)()", x, y, width, height, flags, GetImageMimeType(format), imageQuality.ToString(NumberFormatInfo.InvariantInfo));


			string data;
			if (TryEvalCopyingToBase64String(window.Instance, code, out data))
				return data;
			return null;
		}

		/// <summary>
		/// Creates the copy of the content of the window as an image and writes its binary representation into the output stream.
		/// </summary>
		/// <param name="window">An window to copy into the output stream.</param>
		/// <param name="outputStream"></param>
		/// <param name="x">The X coordinate of the top left corner of the sub-rectangle of the source window.</param>
		/// <param name="y">The Y coordinate of the top left corner of the sub-rectangle of the source window.</param>
		/// <param name="width">The width of the sub-rectangle of the source window.</param>
		/// <param name="height">The height of the sub-rectangle of the source window.</param>
		/// <param name="format">The type for the image format to return.</param>
		/// <returns>true if the operation is successful; otherwise, false.</returns>
		public static bool TryCopyWindowToStream(GeckoWindow window, Stream outputStream, float x, float y, float width, float height, GeckoGraphicsImageFormat format)
		{
			return TryCopyWindowToStream(window, outputStream, x, y, width, height, format, 0.92f);
		}

		/// <summary>
		/// Creates the copy of the content of the window as an image and writes its binary representation into the output stream.
		/// </summary>
		/// <param name="window">An window to copy into the output stream.</param>
		/// <param name="outputStream"></param>
		/// <param name="x">The X coordinate of the top left corner of the sub-rectangle of the source window.</param>
		/// <param name="y">The Y coordinate of the top left corner of the sub-rectangle of the source window.</param>
		/// <param name="width">The width of the sub-rectangle of the source window.</param>
		/// <param name="height">The height of the sub-rectangle of the source window.</param>
		/// <param name="format">The type for the image format to return.</param>
		/// <param name="imageQuality">A Number between 0 and 1 indicating image quality if the requested type is image/jpeg or image/webp.</param>
		/// <returns>true if the operation is successful; otherwise, false.</returns>
		public static bool TryCopyWindowToStream(GeckoWindow window, Stream outputStream, float x, float y, float width, float height, GeckoGraphicsImageFormat format, float imageQuality)
		{
			if (width <= 0)
				throw new ArgumentException("width");

			if (height <= 0)
				throw new ArgumentException("height");

			if (imageQuality <= 0.0f || imageQuality > 1.0f)
				throw new ArgumentOutOfRangeException("imageQuality");

			uint flags = (uint)(nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_DO_NOT_FLUSH
				| nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_ASYNC_DECODE_IMAGES
				| nsIDOMCanvasRenderingContext2DConsts.DRAWWINDOW_USE_WIDGET_LAYERS);

			string code = string.Format(CultureInfo.InvariantCulture, @"(function(canvas, ctx)
						{{
							canvas = window.document.createElement('canvas');
							canvas.width = {2};
							canvas.height = {3};
							ctx = canvas.getContext('2d');
							ctx.drawWindow(window, {0}, {1}, {2}, {3}, 'rgb(255,255,255)', {4});
							return canvas.toDataURL('{5}', {6});
						}}
						)()", x, y, width, height, flags, GetImageMimeType(format), imageQuality.ToString(NumberFormatInfo.InvariantInfo));

			return TryEvalCopyingToStream(window.Instance, code, outputStream);
		}


		private static string GetImageMimeType(GeckoGraphicsImageFormat format)
		{
			switch (format)
			{
				case GeckoGraphicsImageFormat.Jpeg:
					return "image/jpeg";
				case GeckoGraphicsImageFormat.Gif:
					return "image/gif";
				case GeckoGraphicsImageFormat.Png:
					return "image/png";
				case GeckoGraphicsImageFormat.Bmp:
					return "image/x-bmp";
			}
			throw new ArgumentOutOfRangeException("format");
		}

		private static bool TryEvalCopyingToBase64String(object global, string code, out string data)
		{
			data = null;
			try
			{
				//data = GeckoJavascriptBridge.GetService().EvaluateToString(global, GeckoPrincipal.SystemPrincipal, code);
			}
			catch (GeckoException ex)
			{
				Debug.Print("{0}: {1}\r\n{2}", ex.GetType().Name, ex.Message, ex.StackTrace);
				return false;
			}

			if (data == null || !data.StartsWith("data:"))
				return false;

			int startIndex = data.IndexOf(";base64,") + 8;
			if (startIndex == 7 || data.Length <= startIndex)
				return false;

			data = data.Substring(startIndex);
			return true;
		}

		private static bool TryEvalCopyingToStream(object global, string code, Stream outputStream)
		{
			string data;
			if (!TryEvalCopyingToBase64String(global, code, out data))
				return false;

			using (var base64Stream = new MemoryStream(Encoding.ASCII.GetBytes(data)))
			{
				Utils.ConvertFromBase64ToStream(base64Stream, outputStream);
			}
			return true;
		}

	}
}
