using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko
{
	public class GeckoDOMMouseEventArgs
		: GeckoDOMUIEventArgs
	{
		private nsIDOMMouseEvent _instance;

		public static GeckoDOMMouseEventArgs Create(nsIDOMMouseEvent instance)
		{
			var dragEvent = Xpcom.QueryInterface<nsIDOMDragEvent>(instance);
			if (dragEvent != null)
			{
				Xpcom.FreeComObject(ref instance);
				return GeckoDOMDragEventArgs.Create(dragEvent);
			}

			return new GeckoDOMMouseEventArgs(instance);
		}

		protected GeckoDOMMouseEventArgs(nsIDOMMouseEvent instance)
			: base(instance)
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			_instance = null;
			base.Dispose(disposing);
		}

		public new nsIDOMMouseEvent Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ArgumentNullException();
				return _instance;
			}
		}

		public void InitMouseEvent(string type, bool canBubble, bool cancelable, GeckoWindow view, int detail,
								   int screenX, int screenY, int clientX, int clientY,
								   bool ctrlKey, bool altKey, bool shiftKey, bool metaKey,
			ushort button, GeckoDOMEventTarget target)
		{
			using (var typeArg = new nsAString(type))
			{
				Instance.InitMouseEvent(typeArg, canBubble, cancelable, view.MozInstance, detail, screenX, screenY,
					clientX, clientY, ctrlKey, altKey, shiftKey, metaKey, button, target.Instance);
			}
		}

		/// <summary>
		/// The X coordinate of the mouse pointer in local (DOM content) coordinates.
		/// </summary>
		public int ClientX
		{
			get { return Instance.GetClientXAttribute(); }
		}
		
		/// <summary>
		/// The Y coordinate of the mouse pointer in local (DOM content) coordinates.
		/// </summary>
		public int ClientY
		{
			get { return Instance.GetClientYAttribute(); }
		}
		
		/// <summary>
		/// The X coordinate of the mouse pointer in global (screen) coordinates.
		/// </summary>
		public int ScreenX
		{
			get { return Instance.GetScreenXAttribute(); }
		}
		
		/// <summary>
		/// The Y coordinate of the mouse pointer in global (screen) coordinates.
		/// </summary>
		public int ScreenY
		{
			get { return Instance.GetScreenYAttribute(); }
		}
		
		/// <summary>
		/// The button number that was pressed when the mouse event was fired.
		/// </summary>
		public GeckoMouseButton Button
		{
			get { return (GeckoMouseButton)Instance.GetButtonAttribute(); }
		}
		
		/// <summary>
		/// true if the alt key was down when the mouse event was fired.
		/// </summary>
		public bool AltKey
		{
			get { return Instance.GetAltKeyAttribute(); }
		}

		
		
		/// <summary>
		/// true if the control key was down when the mouse event was fired.
		/// </summary>
		public bool CtrlKey
		{
			get { return Instance.GetCtrlKeyAttribute(); }
		}
		
		/// <summary>
		/// true if the shift key was down when the mouse event was fired.
		/// </summary>
		public bool ShiftKey
		{
			get { return Instance.GetShiftKeyAttribute(); }
		}
	}
}
