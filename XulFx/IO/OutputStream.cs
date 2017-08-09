using Gecko.Interfaces;
using System;
using System.IO;

namespace Gecko.IO
{
	public sealed class OutputStream
		: GeckoStream<nsIOutputStream>
	{
		private bool _seekable;
		private nsISeekableStream _seekableStream;
		private nsIBinaryOutputStream _binaryOutputStream;

		public static OutputStream Create(nsIOutputStream stream)
		{
			return new OutputStream(stream);
		}

		private OutputStream(nsIOutputStream instance)
			: base(instance)
		{
			_binaryOutputStream = Xpcom.CreateInstance<nsIBinaryOutputStream>(Contracts.BinaryOutputStream);
			_binaryOutputStream.SetOutputStream(instance);
			
			_seekableStream = Xpcom.QueryInterface<nsISeekableStream>(instance);
			_seekable = _seekableStream != null;
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _seekableStream);
			Xpcom.FreeComObject(ref _binaryOutputStream);
			base.Dispose(disposing);
		}

		private nsIBinaryOutputStream BinaryOutputStream
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_binaryOutputStream == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _binaryOutputStream;
			}
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

		public override void Close()
		{
			BinaryOutputStream.Close();
			Instance.Close();
		}

		public override void Flush()
		{
			BinaryOutputStream.Flush();
			Instance.Flush();
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
			long current = SeekableStream.Tell();
			SeekableStream.Seek(0, value);
			SeekableStream.SetEOF();
			SeekableStream.Seek(0, current);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		public override void WriteByte(byte value)
		{
			BinaryOutputStream.Write8(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (offset == 0)
			{
				BinaryOutputStream.WriteByteArray(buffer, (uint)count);
				return;
			}
			byte[] newArray = new byte[count - offset];
			Array.Copy(buffer, offset, newArray, 0, newArray.Length);
			BinaryOutputStream.WriteByteArray(newArray, (uint)newArray.Length);
		}

		public override bool CanRead
		{
			get { return false; }
		}

		public override bool CanSeek
		{
			get { return _seekable; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override long Length
		{
			get { return SeekableStream.Tell(); }
		}

		public override long Position
		{
			get { return SeekableStream.Tell(); }
			set { SeekableStream.Seek(0, (int)Position); }
		}

	}
}
