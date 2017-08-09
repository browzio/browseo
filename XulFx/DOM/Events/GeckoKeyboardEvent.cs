using System;
using System.Linq;
using Gecko.Interfaces;

namespace Gecko.DOM
{
	public sealed class GeckoKeyboardEvent : GeckoUIEvent
	{
		private new nsIDOMKeyEvent _instance;

		public static GeckoKeyboardEvent Create(nsIDOMKeyEvent instance)
		{
			return new GeckoKeyboardEvent(instance);
		}

		private GeckoKeyboardEvent(nsIDOMKeyEvent instance)
			: base(Xpcom.QueryInterface<nsIDOMUIEvent>(instance))
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _instance);
			base.Dispose(disposing);
		}

		public new nsIDOMKeyEvent Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);
				return _instance;
			}
		}
		
		public int CharCode
		{
			get { return (int)Instance.GetCharCodeAttribute(); }
		}

		public int KeyCode
		{
			get { return (int)Instance.GetKeyCodeAttribute(); }
		}

		public bool AltKey
		{
			get { return Instance.GetAltKeyAttribute(); }
		}

		public bool CtrlKey
		{
			get { return Instance.GetCtrlKeyAttribute(); }
		}

		public bool ShiftKey
		{
			get { return Instance.GetShiftKeyAttribute(); }
		}

		public bool MetaKey
		{
			get { return Instance.GetShiftKeyAttribute(); }
		}
		
		public void InitKeyEvent(string type, bool canBubble, bool cancelable, GeckoWindow view,
			bool ctrlKey, bool altKey, bool shiftkey, bool metaKey, int keyCode, int charCode)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			if (view == null)
				throw new ArgumentNullException("view");

			using (var aType = new nsAString(type))
			{
				Instance.InitKeyEvent(aType, canBubble, cancelable, view.MozInstance, ctrlKey, altKey, shiftkey, metaKey, (uint)keyCode, (uint)charCode);
			}
		}

		public bool GetModifierState(string key)
		{
			return nsString.Pass(Instance.GetModifierState, key);
		}

		public KeyLocation Location
		{
			get { return (KeyLocation)Instance.GetLocationAttribute(); }
		}

		public bool Repeat
		{
			get { return Instance.GetRepeatAttribute(); }
		}

		public string Key
		{
			get { return nsString.Get(Instance.GetKeyAttribute); }
		}

	}
}
