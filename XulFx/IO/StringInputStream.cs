using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Runtime.InteropServices;

namespace Gecko.IO
{
	public sealed class StringInputStream
		: InputStream
	{
		private new nsIStringInputStream _instance;

		public static StringInputStream Create(nsIStringInputStream instance)
		{
			return new StringInputStream(instance);
		}

		public static StringInputStream Create(string data)
		{
			var stream = Xpcom.CreateInstance<nsIStringInputStream>(Contracts.StringInputStream)
				.Wrap(StringInputStream.Create);
			if(!string.IsNullOrEmpty(data))
				stream.SetData(data, data.Length);

			return stream;
		}

		private StringInputStream(nsIStringInputStream instance)
			: base(instance)
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			_instance = null;
			base.Dispose(disposing);
		}

		public new nsIStringInputStream Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);
				return _instance;
			}
		}


		public void SetData(string data, int dataLen)
		{
			if(!string.IsNullOrEmpty(data))
				Instance.SetData(data, dataLen);
		}

		public void AdoptData(byte[] data)
		{
			AdoptData(data, data.Length);
		}

		public void AdoptData(byte[] data, int dataLen)
		{
			// DON'T FREE THIS BUFFER
			IntPtr buffer = Xpcom.Alloc(dataLen);
			if(buffer == IntPtr.Zero)
				throw new OutOfMemoryException();
			
			try
			{
				Marshal.Copy(data, 0, buffer, dataLen);
				// the input stream takes
				// ownership of the given data buffer and will nsMemory::Free it when
				// the input stream is destroyed.
				Instance.AdoptData(buffer, dataLen);
			}
			catch
			{
				Xpcom.Free(buffer);
				throw;
			}
		}

		public void ShareData(string data, int dataLen)
		{
			Instance.ShareData(data, dataLen);
		}
	}
}
