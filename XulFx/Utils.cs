using System;
using Gecko.Interfaces;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Gecko
{
	public static class Utils
	{
		private static readonly DateTime _utcLinuxStartEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static DateTime EpochStart
		{
			get { return _utcLinuxStartEpoch; }
		}

		public static DateTime FromSecondsSinceEpoch(double time)
		{
			return _utcLinuxStartEpoch.AddSeconds(time).ToLocalTime();
		}

		public static DateTime FromSecondsSinceEpoch(ulong time)
		{
			return _utcLinuxStartEpoch.AddSeconds(time).ToLocalTime();
		}

		public static double ToSecondsSinceEpoch(DateTime time)
		{
			return (time.ToUniversalTime() - _utcLinuxStartEpoch).TotalSeconds;
		}

		public static Uri ToUri(this nsIURI value)
		{
			if (value == null)
				return null;

			Uri url;
			return Uri.TryCreate(nsString.Get(value.GetAsciiSpecAttribute), UriKind.Absolute, out url) ? url : null;
		}

		public static void ConvertFromBase64ToStream(Stream inputStream, Stream outputStream)
		{
			if (inputStream == null)
				throw new ArgumentNullException("inputStream");
			if(outputStream == null)
				throw new ArgumentNullException("outputStream");

			using (var base64 = new FromBase64Transform())
			{
				int inputBlockSize = base64.InputBlockSize;
				byte[] inputBuffer = new byte[inputBlockSize];
				byte[] outputBuffer = new byte[base64.OutputBlockSize];
				
				//Transform the data in chunks the size of InputBlockSize.  
				int count;
				while ((count = inputStream.Read(inputBuffer, 0, inputBlockSize)) == inputBlockSize)
				{
					count = base64.TransformBlock(inputBuffer, 0, count, outputBuffer, 0);
					outputStream.Write(outputBuffer, 0, count);
				}

				// Transform the final block of data.
				outputBuffer = base64.TransformFinalBlock(inputBuffer, 0, count);
				outputStream.Write(outputBuffer, 0, outputBuffer.Length);
			}
		}

		public static DateTime RetrieveLibraryLinkerTimestamp(string path)
		{
			const int c_PeHeaderOffset = 60;
			const int c_LinkerTimestampOffset = 8;

			var buffer = new byte[2048];

			using (var file = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				file.Read(buffer, 0, 2048);
			}

			return _utcLinuxStartEpoch.AddSeconds(BitConverter.ToInt32(buffer, BitConverter.ToInt32(buffer, c_PeHeaderOffset) + c_LinkerTimestampOffset));
		}

	}
}
