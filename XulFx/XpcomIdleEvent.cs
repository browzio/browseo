using Gecko.Interfaces;
using System;

namespace Gecko
{
	sealed class XpcomIdleEvent : nsIRunnable
	{
		private volatile Action<EventArgs> _action;
		private volatile EventArgs _arg;
		private volatile Exception _exception;

		public XpcomIdleEvent(Action<EventArgs> action, EventArgs arg)
		{
			_action = action;
			_arg = arg;
		}

		public Exception Exception
		{
			get { return _exception; }
		}

		public void Run()
		{
			try
			{
				if (_action != null) _action(_arg);
			}
			catch(Exception ex)
			{
				_exception = ex;
				throw;
			}
		}

	}
}
