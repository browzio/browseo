using Gecko.Interfaces;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.IO
{
	public class InputStream
		: GeckoStream<nsIInputStream>
	{
		private bool _canRead;
		private bool _seekable;
		private nsISeekableStream _seekableStream;

		public static InputStream Create(nsIInputStream stream)
		{
			var mimeInputStream = Xpcom.QueryInterface<nsIMIMEInputStream>(stream);
			if (mimeInputStream != null)
			{
				Xpcom.FreeComObject(ref stream);
				return MimeInputStream.Create(mimeInputStream);
			}
			var stringInputStream = Xpcom.QueryInterface<nsIStringInputStream>(stream);
			if (stringInputStream != null)
			{
				Xpcom.FreeComObject(ref stream);
				return StringInputStream.Create(stringInputStream);
			}
			return new InputStream(stream);
		}

		internal InputStream(nsIInputStream instance)
			: base(instance)
		{
			_canRead = true;
			_seekableStream = Xpcom.QueryInterface<nsISeekableStream>(instance);
			_seekable = _seekableStream != null;
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _seekableStream);
			base.Dispose(disposing);
		}

		private nsISeekableStream SeekableStream
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_seekable && _seekableStream == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _seekableStream;
			}
		}

		public override void Flush()
		{

		}

		public override void Close()
		{
			Instance.Close();
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			//NS_SEEK_SET 	0 	Specifiesthat the offset is relative to the start of the stream.
			//NS_SEEK_CUR 	1 	Specifies that the offset is relative to the current position in the stream.
			//NS_SEEK_END 	2 	Specifies that the offset is relative to the end of the stream.
			SeekableStream.Seek((int)origin, (int)offset);
			return SeekableStream.Tell();
		}

		public override void SetLength(long value)
		{
			var position = SeekableStream.Tell();
			SeekableStream.Seek(0, (int)value);
			SeekableStream.SetEOF();
			if (position < value)
			{
				// Returning to old position
				SeekableStream.Seek(0, position);
			}

		}

		public unsafe override int Read(byte[] buffer, int offset, int count)
		{
			uint ret;
			// strict values & buffer size check before using pointers
			if ((offset < 0) || (count <= 0)) return 0;
			// offset >= 0 count>0
			if ((offset + count) > buffer.Length) return 0;
			fixed (byte* bufferPtr = buffer)
			{
				byte* writePtr = bufferPtr + offset;
				ret = Instance.Read(new IntPtr(writePtr), (uint)count);
			}
			return (int)ret;
		}

		public unsafe override int ReadByte()
		{
			byte ret;
			byte* ptr = &ret;
			uint count = Instance.Read(new IntPtr(ptr), 1);
			return count == 0 ? -1 : ret;
		}

		/// <summary>
		/// Begins an asynchronous read operation.
		/// </summary>
		/// <param name="buffer">The buffer to read the data into. </param>
		/// <param name="offset">The byte offset in buffer at which to begin writing data read from the stream. </param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="callback">An optional asynchronous callback, to be called when the read is complete.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <returns>An IAsyncResult that represents the asynchronous read, which could still be pending.</returns>
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			var asyncResult = new InputStreamAsyncResult(callback, state);
			asyncResult.BeginRead(Instance, buffer, offset, count);
			return asyncResult;
		}

		/// <summary>
		/// Waits for the pending asynchronous read to complete.
		/// </summary>
		/// <param name="asyncResult">The reference to the pending asynchronous request to finish.</param>
		/// <returns>The number of bytes read from the stream, between zero (0) and the number of bytes you requested. Streams return zero (0) only at the end of the stream, otherwise, they should block until at least one byte is available.</returns>
		public override int EndRead(IAsyncResult asyncResult)
		{
			return ((InputStreamAsyncResult)asyncResult).EndRead(ref _canRead);
		}


		/// <summary>
		/// Warning: .NET have another stream model. DON'T USE THIS FUNCTION
		/// InputStream can only read data
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("InputStream can only read data :)");
		}

		/// <summary>
		/// Gets a value indicating whether the current stream supports reading.
		/// </summary>
		public override bool CanRead
		{
			get { return _canRead; }
		}

		/// <summary>
		/// Gets a value indicating whether the current stream supports seeking.
		/// </summary>
		public override bool CanSeek
		{
			get { return _canRead && _seekable; }
		}

		/// <summary>
		/// Gets a value indicating whether the current stream supports writing.
		/// </summary>
		public override bool CanWrite
		{
			get { return false; }
		}

		/// <summary>
		/// Warning: .NET have another stream model. DON'T USE THIS PROPERTY
		/// </summary>
		public override long Length
		{
			get
			{
				return SeekableStream.Tell() + (long)Instance.Available();
			}
		}

		/// <summary>
		/// Determine number of bytes available in the stream.  A non-blocking
		/// stream that does not yet have any data to read should return 0 bytes
		/// from this method (i.e., it must not throw the NS_BASE_STREAM_WOULD_BLOCK
		/// exception).
		/// 
		/// In addition to the number of bytes available in the stream, this method
		/// also informs the caller of the current status of the stream.  A stream
		/// that is closed will throw an exception when this method is called.  That
		/// enables the caller to know the condition of the stream before attempting
		/// to read from it.  If a stream is at end-of-file, but not closed, then
		/// this method returns 0 bytes available.  (Note: some InputStream
		/// implementations automatically close when eof is reached; some do not).
		/// </summary>
		/// <returns>
		/// number of bytes currently available in the stream.
		/// </returns>
		/// <exception cref="System.Runtime.InteropServices.COMException">
		/// NS_BASE_STREAM_CLOSED: if the stream is closed normally.
		/// &lt;other-error&gt;: if the stream is closed due to some error
		/// condition
		/// </exception>
		public long Available
		{
			get { return (long)Instance.Available(); }
		}

		public override long Position
		{
			get { return SeekableStream.Tell(); }
			set { SeekableStream.Seek(0, (int)value); }
		}

		/// <summary>
		/// Method is useful when reading headers
		/// </summary>
		/// <returns></returns>
		public string ReadLine()
		{
			StringBuilder ret = new StringBuilder(64);
			var count = Instance.Available();
			for (uint i = 0; i < count; i++)
			{
				var character = ReadByte();
				if (character < 0) break;
				char test = (char)character;
				if (test == '\r')
				{
					// nothing
				}
				else
				{
					if (test == '\n')
					{
						break;
					}
					ret.Append(test);
				}
			}
			return ret.ToString();
		}
	}
}
