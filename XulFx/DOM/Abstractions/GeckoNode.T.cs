using System;
using Gecko.Interfaces;

namespace Gecko.DOM.Abstractions
{
	public abstract class GeckoNode<TInterface> : GeckoNode
		where TInterface : class, nsIDOMNode
	{
		private new TInterface _instance;

		protected GeckoNode(TInterface instance)
			: base(Xpcom.QueryInterface<nsIDOMNode>(instance))
		{
			_instance = instance;
		}

		public new TInterface Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _instance;
			}
		}

		public nsIDOMNode Node
		{
			get { return base.Instance; }
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _instance);
			base.Dispose(disposing);
		}
	}
}
