using Gecko.Interop;
using System;
using System.Threading;

namespace Gecko
{
	public class GeckoSynchronizationContext : SynchronizationContext, IDisposable
	{
		protected class SyncData
		{
			private SendOrPostCallback _callback;
			private object _state;
			private bool _isSync;

			public SyncData(SendOrPostCallback d, object state, bool isSync)
			{
				_callback = d;
				_state = state;
				_isSync = isSync;
			}

			public void Run()
			{
				if (_isSync)
				{
					_callback(_state);
					return;
				}
				try
				{
					_callback(_state);
				}
				catch (Exception ex)
				{
					ThreadPool.QueueUserWorkItem(exceptionObj => { throw (Exception)exceptionObj; }, ex);
				}
			}
		}



		private bool _isDisposed;

		#region .ctor, .dtor, IDisposable

		public GeckoSynchronizationContext()
		{

		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				_isDisposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~GeckoSynchronizationContext()
		{
			Dispose(false);
		}

		#endregion .ctor, .dtor, IDisposable

		protected bool IsDisposed
		{
			get { return _isDisposed; }
		}

		protected static bool CanXpcomSync
		{
			get
			{
				return Xpcom.ComponentManager != null;
			}
		}

		public override void Send(SendOrPostCallback d, object state)
		{
			if (CanXpcomSync && Xpcom.InvokeRequired)
			{
				Xpcom.Invoke(new SyncData(d, state, true).Run);
			}
			else
			{
				d(state);
			}
		}

		public override void Post(SendOrPostCallback d, object state)
		{
			if (CanXpcomSync && Xpcom.InvokeRequired)
			{
				Xpcom.Post(new SyncData(d, state, false).Run);
			}
			else
			{
				d(state);
			}
		}

	}
}
