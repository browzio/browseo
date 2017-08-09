using System;
using Gecko.Interfaces;

namespace Gecko.Javascript
{
	sealed class Runnable : nsIRunnable
	{
		private Action _action;
		private Exception _exception;

		public Runnable(Action action)
		{
			_action = action;
		}

		public Exception Exception
		{
			get { return _exception; }
		}

		void nsIRunnable.Run()
		{
			try
			{
				_action();
			}
			catch (Exception e)
			{
				_exception = e;
			}
		}
	}

	sealed class Runnable<T> : nsIRunnable
	{
		private Func<T> _func;
		private Exception _exception;
		private T _result;

		public Runnable(Func<T> func)
		{
			_func = func;
		}

		public Exception Exception
		{
			get { return _exception; }
		}

		public T Result
		{
			get { return _result; }
		}

		void nsIRunnable.Run()
		{
			try
			{
				_result = _func();
			}
			catch (Exception e)
			{
				_exception = e;
			}
		}
	}

}
