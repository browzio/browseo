using Gecko.Interfaces;
using System;
using System.Reflection;
using System.Threading;

namespace Gecko
{
	sealed class SyncTask : nsIRunnable
	{
		private volatile Action<SyncTask> _action;
		private volatile object _state;
		private volatile Exception _exception;
		private bool _isAsync;

		internal SyncTask(Action<SyncTask> action, bool isAsync)
		{
			_action = action;
			_isAsync = isAsync;
		}

		public object State
		{
			get { return _state; }
			set { _state = value; }
		}

		public Exception Exception
		{
			get { return _exception; }
		}

		public void Run()
		{
			try
			{
				if (_action != null)
					_action(this);
			}
			catch(Exception e)
			{
				if (_isAsync)
				{
					Xpcom.SetException(e);
				}
				else
				{
					_exception = e;
				}
				throw;
			}
		}

	}
}
