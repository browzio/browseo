using Gecko.Interfaces;
using System;

namespace Gecko
{
	public sealed class GeckoDOMKeyEventArgs
		: GeckoDOMUIEventArgs
	{
		private nsIDOMKeyEvent _instance;

		public static GeckoDOMKeyEventArgs Create(nsIDOMKeyEvent ev)
		{
			return new GeckoDOMKeyEventArgs(ev);
		}

		private GeckoDOMKeyEventArgs(nsIDOMKeyEvent instance)
			: base(instance)
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			_instance = null;
			base.Dispose(disposing);
		}

		public new nsIDOMKeyEvent Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ArgumentNullException();
				return _instance;
			}
		}


		public int KeyChar
		{
			get { return (int)Instance.GetCharCodeAttribute(); }
		}
		
		public int KeyCode
		{
			get { return (int)Instance.GetKeyCodeAttribute(); }
		}
		
		public bool Alt
		{
			get { return Instance.GetAltKeyAttribute(); }
		}
		
		public bool Control
		{
			get { return Instance.GetCtrlKeyAttribute(); }
		}
		
		public bool Shift
		{
			get { return Instance.GetShiftKeyAttribute(); }
		}
	}


}
