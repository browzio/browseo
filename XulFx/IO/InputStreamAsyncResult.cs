using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Gecko.IO
{
	internal sealed class InputStreamAsyncResult : IAsyncResult, nsIStreamListener
	{
		private AsyncCallback _callback;
		private object _state;

		private bool _isCompleted;
		private byte[] _buffer;
		private int _bufferOffset;
		private uint _count;
		private nsIInputStream _inputStream;
		private EventWaitHandle _event;

		public InputStreamAsyncResult(AsyncCallback callback, object state)
		{
			_callback = callback;
			_state = state;
		}

		~InputStreamAsyncResult()
		{
			Xpcom.FreeComObject(ref _inputStream);
		}

		#region interface IAsyncResult

		public object AsyncState
		{
			get { return _state; }
		}

		public System.Threading.WaitHandle AsyncWaitHandle
		{
			get
			{
				if(_event == null)
					_event = new ManualResetEvent(false);
				return _event;
			}
		}

		public bool CompletedSynchronously
		{
			get { return _isCompleted; }
		}

		public bool IsCompleted
		{
			get { return _isCompleted; }
		}

		#endregion

		#region interface nsIStreamListener

		void nsIRequestObserver.OnStartRequest(nsIRequest aRequest, nsISupports aContext)
		{

		}

		void nsIRequestObserver.OnStopRequest(nsIRequest aRequest, nsISupports aContext, int aStatusCode)
		{

		}

		void nsIStreamListener.OnStartRequest(nsIRequest aRequest, nsISupports aContext)
		{

		}

		void nsIStreamListener.OnStopRequest(nsIRequest aRequest, nsISupports aContext, int aStatusCode)
		{

		}

		void nsIStreamListener.OnDataAvailable(nsIRequest aRequest, nsISupports aContext, nsIInputStream aInputStream, ulong aOffset, uint aCount)
		{
			_inputStream = Xpcom.QueryInterface<nsIInputStream>(aInputStream);
			_count = Math.Min(_count, aCount);
			_isCompleted = (aRequest.GetStatusAttribute() == GeckoError.NS_OK);

			EventWaitHandle waitHandle = _event;
			if (waitHandle != null)
				waitHandle.Set();

			if (_callback != null)
				_callback(this);
		}

		#endregion

		internal void BeginRead(nsIInputStream stream, byte[] buffer, int offset, int count)
		{
			if (buffer == null)
				throw new ArgumentNullException("buffer");
			if (offset < 0)
				throw new ArgumentOutOfRangeException("offset");
			if (count < 0 || offset + count > buffer.Length)
				throw new ArgumentOutOfRangeException("count");

			_buffer = buffer;
			_bufferOffset = offset;
			_count = (uint)count;
			nsIInputStreamPump inputStreamPump = null;
			try
			{
				inputStreamPump = Xpcom.CreateInstance<nsIInputStreamPump>(Contracts.InputStreamPump);
				inputStreamPump.Init(stream, -1, _count, _count, 1, false);
				inputStreamPump.AsyncRead(this, null);
			}
			finally
			{
				Xpcom.FreeComObject(ref inputStreamPump);
			}
		}

		internal unsafe int EndRead(ref bool canRead)
		{
			try
			{
				if (!_isCompleted || _buffer == null || _inputStream == null)
					throw new InvalidOperationException();

				ulong available = _inputStream.Available();

				uint count;
				fixed (byte* bufferPtr = _buffer)
				{
					byte* writePtr = bufferPtr + _bufferOffset;
					count = _inputStream.Read(new IntPtr(writePtr), _count);
				}
				canRead = (count < available);
				return (int)count;
			}
			finally
			{
				Xpcom.FreeComObject(ref _inputStream);
			}
		}





	}
}
