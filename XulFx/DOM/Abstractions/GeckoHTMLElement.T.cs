using System;
using Gecko.Interfaces;

namespace Gecko.DOM.Abstractions
{
	public abstract class GeckoHTMLElement<TInterface> : GeckoHTMLElement
		where TInterface : class//, nsIDOMHTMLElement
	{
		private new TInterface _instance;

		protected GeckoHTMLElement(TInterface instance)
			: base(Xpcom.QueryInterface<nsIDOMHTMLElement>(instance))
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

		public nsIDOMHTMLElement HTMLElement
		{
			get { return base.Instance; }
		}
	}
}
