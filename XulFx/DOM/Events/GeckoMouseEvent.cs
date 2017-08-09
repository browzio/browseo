using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoMouseEvent : GeckoUIEvent
	{
		private new nsIDOMMouseEvent _instance;

		public static GeckoMouseEvent Create(nsIDOMMouseEvent instance)
		{
			return new GeckoMouseEvent(instance);
		}

		private GeckoMouseEvent(nsIDOMMouseEvent instance)
			: base(Xpcom.QueryInterface<nsIDOMUIEvent>(instance))
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _instance);
			base.Dispose(disposing);
		}

		public new nsIDOMMouseEvent Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);
				return _instance;
			}
		}

		public int ScreenX
		{
			get { return Instance.GetScreenXAttribute(); }
		}

		public int ScreenY
		{
			get { return Instance.GetScreenYAttribute(); }
		}

		public int MovementX
		{
			get { return Instance.GetMozMovementXAttribute(); }
		}

		public int MovementY
		{
			get { return Instance.GetMozMovementYAttribute(); }
		}

		public int ClientX
		{
			get { return Instance.GetClientXAttribute(); }
		}

		public int ClientY
		{
			get { return Instance.GetClientYAttribute(); }
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

		public GeckoMouseButton Button
		{
			get { return (GeckoMouseButton)Instance.GetButtonAttribute(); }
		}

		public MouseButtons Buttons
		{
			get { return (MouseButtons)Instance.GetButtonsAttribute(); }
		}

		public GeckoDOMEventTarget RelatedTarget
		{
			get { return Instance.GetRelatedTargetAttribute().Wrap(GeckoDOMEventTarget.Create); }
		}

		public void InitMouseEvent(string type, bool canBubble, bool cancelable, GeckoWindow view, int detail,
			int screenX, int screenY, int clientX, int clientY, bool ctrlKey, bool altKey, bool shiftkey,
			bool metaKey, ushort button, GeckoDOMEventTarget relatedTarget)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			if (view == null)
				throw new ArgumentNullException("view");

			using (var aType = new nsAString(type))
			{
				Instance.InitMouseEvent(aType, canBubble, cancelable, view.MozInstance, detail,
					screenX, screenY, clientX, clientY, ctrlKey, altKey, shiftkey, metaKey, button,
					relatedTarget != null ? relatedTarget.Instance : null);
			}
		}

		public float Pressure
		{
			get { return Instance.GetMozPressureAttribute(); }
		}

		public ushort InputSource
		{
			get { return Instance.GetMozInputSourceAttribute(); }
		}

		public bool GetModifierState(string key)
		{
			return nsString.Pass(Instance.GetModifierState, key);
		}

	}
}
