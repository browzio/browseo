using System;
using Gecko.Interfaces;
using Gecko.DOM.Abstractions;

namespace Gecko.DOM.XML
{
	public sealed class GeckoXMLDocument : GeckoDocument
	{
		private nsIDOMXMLDocument _instance;

		public static GeckoXMLDocument Create(nsIDOMXMLDocument instance)
		{
			return new GeckoXMLDocument(instance);
		}

		private GeckoXMLDocument(nsIDOMXMLDocument instance)
			: base(Xpcom.QueryInterface<nsIDOMDocument>(instance))
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _instance);
			base.Dispose(disposing);
		}

		public new nsIDOMXMLDocument Instance
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
