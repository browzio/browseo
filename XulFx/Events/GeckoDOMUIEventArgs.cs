using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko
{
	public class GeckoDOMUIEventArgs
		: GeckoDOMEventArgs
	{
		private nsIDOMUIEvent _instance;

		public static GeckoDOMUIEventArgs Create(nsIDOMUIEvent instance)
		{
			nsIDOMMouseEvent mouseEvent = Xpcom.QueryInterface<nsIDOMMouseEvent>(instance);
			if (mouseEvent != null)
			{
				Xpcom.FreeComObject(ref instance);
				return GeckoDOMMouseEventArgs.Create(mouseEvent);
			}
			nsIDOMKeyEvent keyEvent = Xpcom.QueryInterface<nsIDOMKeyEvent>(instance);
			if(keyEvent != null)
			{
				Xpcom.FreeComObject(ref instance);
				return GeckoDOMKeyEventArgs.Create(keyEvent);
			}
			return new GeckoDOMUIEventArgs(instance);
		}

		protected GeckoDOMUIEventArgs(nsIDOMUIEvent instance)
			: base(Xpcom.QueryInterface<nsIDOMEvent>(instance))
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			_instance = null;
			base.Dispose(disposing);
		}

		public new nsIDOMUIEvent Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ArgumentNullException();
				return _instance;
			}
		}


		public int Detail
		{
			get { return Instance.GetDetailAttribute(); }
		}
	}
}
