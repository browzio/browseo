using System;
using Gecko.Interfaces;

namespace Gecko.DOM.Abstractions
{
	public abstract class GeckoDocument<TInterface> : GeckoDocument
		where TInterface : class, nsIDOMDocument
	{
		private new TInterface _instance;

		protected GeckoDocument(TInterface instance)
			: base(Xpcom.QueryInterface<nsIDOMDocument>(instance))
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _instance);
			base.Dispose(disposing);
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

		public nsIDOMDocument Document
		{
			get { return base.Instance; }
		}
	}
}
