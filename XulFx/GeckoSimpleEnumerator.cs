using System;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Collections.Generic;

namespace Gecko
{
	public sealed class GeckoSimpleEnumerator<TInterface, TWrapper>
		: ComObject<nsISimpleEnumerator>, IEnumerator<TWrapper>
		where TWrapper : class, IGeckoObjectWrapper
		where TInterface : class
	{
		private bool _isDisposed;
		private TWrapper _currentItem;
		private Func<TInterface, TWrapper> _createItem;

		public GeckoSimpleEnumerator(nsISimpleEnumerator instance, Func<TInterface, TWrapper> createItem)
			: base(instance)
		{
			_createItem = createItem;
		}

		protected override void Dispose(bool disposing)
		{
			_isDisposed = true;
			_currentItem = null;
			base.Dispose(disposing);
		}

		public TWrapper Current
		{
			get
			{
				if (_isDisposed)
					throw new ObjectDisposedException(this.GetType().Name);

				return _currentItem;
			}
		}

		object System.Collections.IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		public bool MoveNext()
		{
			_currentItem = null;

			if (!Instance.HasMoreElements())
				return false;
			
			nsISupports item = Instance.GetNext();
			try
			{
				_currentItem = Xpcom.QueryInterface<TInterface>(item).Wrap<TInterface, TWrapper>(_createItem);
				if(item != null && _currentItem == null)
					throw new InvalidOperationException("TInterface is invalid?");
			}
			finally
			{
				Xpcom.FreeComObject(ref item);
			}
			return true;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}
}
