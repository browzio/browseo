using System;
using Gecko.Interop;
using Gecko.Interfaces;
using System.Runtime.InteropServices;
using Gecko.CustomMarshalers;

namespace Gecko.DOM
{
	public class GeckoEvent : ComObject<nsIDOMEvent>, IGeckoObjectWrapper
	{
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void InitEventDelegate([MarshalAs(UnmanagedType.Interface)] nsIDOMEvent @this, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] nsAStringBase eventTypeArg, [MarshalAs(UnmanagedType.U1)] bool canBubbleArg, [MarshalAs(UnmanagedType.U1)] bool cancelableArg);

		public static GeckoEvent Create(nsIDOMEvent instance)
		{
			GeckoEvent result = null;
			result = Xpcom.QueryInterface<nsIDOMUIEvent>(instance).Wrap(GeckoUIEvent.Create);

			if (result != null)
				Xpcom.FreeComObject(ref instance);
			else
				result = new GeckoEvent(instance);
			return result;
		}

		protected GeckoEvent(nsIDOMEvent instance)
			: base(instance)
		{

		}

		public void InitEvent(string type, bool canBubble, bool cancelable)
		{
			if (type == null)
				throw new ArgumentNullException("type");

			InitEventDelegate initEvent = this.GetDelegateForComMethod<nsIDOMEvent, InitEventDelegate>(new Action<nsAStringBase, bool, bool>(Instance.InitEvent));

			using (var aType = new nsAString(type))
			{
				initEvent(Instance, aType, canBubble, cancelable);
			}

		}

		public bool CancelBubble
		{
			get { return Instance.GetCancelBubbleAttribute(); }
			set { Instance.SetCancelBubbleAttribute(true); }
		}

	}
}
