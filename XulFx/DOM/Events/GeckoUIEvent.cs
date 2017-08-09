using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public class GeckoUIEvent : GeckoEvent
	{
		private new nsIDOMUIEvent _instance;

		public static GeckoUIEvent Create(nsIDOMUIEvent instance)
		{
			GeckoUIEvent result = null;
			result = Xpcom.QueryInterface<nsIDOMMouseEvent>(instance).Wrap(GeckoMouseEvent.Create);
			if(result == null)
			{
				result = Xpcom.QueryInterface<nsIDOMKeyEvent>(instance).Wrap(GeckoKeyboardEvent.Create);
			}
			if (result != null)
				Xpcom.FreeComObject(ref instance);
			else
				result = new GeckoUIEvent(instance);
			return result;
		}

		protected GeckoUIEvent(nsIDOMUIEvent instance)
			: base(Xpcom.QueryInterface<nsIDOMEvent>(instance))
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _instance);
			base.Dispose(disposing);
		}

		public new nsIDOMUIEvent Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);
				return _instance;
			}
		}

		public void InitUIEvent(string type, bool canBubble, bool cancelable, GeckoWindow view, int detail)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			if (view == null)
				throw new ArgumentNullException("view");

			using (var aType = new nsAString(type))
			{
				Instance.InitUIEvent(aType, canBubble, cancelable, view.MozInstance, detail);
			}

		}

		public GeckoWindow View
		{
			get { return Instance.GetViewAttribute().Wrap(GeckoWindow.Create); }
		}

		public int Detail
		{
			get { return Instance.GetDetailAttribute(); }
		}

		public int LayerX
		{
			get { return Instance.GetLayerXAttribute(); }
		}

		public int LayerY
		{
			get { return Instance.GetLayerYAttribute(); }
		}

		public int PageX
		{
			get { return Instance.GetPageXAttribute(); }
		}

		public int PageY
		{
			get { return Instance.GetPageYAttribute(); }
		}

		public int Which
		{
			get { return (int)Instance.GetWhichAttribute(); }
		}

		public GeckoNode RangeParent
		{
			get { return Instance.GetRangeParentAttribute().Wrap(GeckoNode.Create); }
		}

		public int RangeOffset
		{
			get { return Instance.GetRangeOffsetAttribute(); }
		}

		public bool IsChar
		{
			get { return Instance.GetIsCharAttribute(); }
		}

	}
}
