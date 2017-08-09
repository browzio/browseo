using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System;


namespace Gecko
{
	public sealed class GeckoDOMEventTarget : ComObject<nsIDOMEventTarget>, IGeckoObjectWrapper
	{
		public static GeckoDOMEventTarget Create(nsIDOMEventTarget instance)
		{
			return new GeckoDOMEventTarget(instance);
		}

		private GeckoDOMEventTarget(nsIDOMEventTarget instance)
			: base(instance)
		{

		}


		public GeckoElement CastToGeckoElement()
		{
			return Xpcom.QueryInterface<nsIDOMElement>(Instance).Wrap(GeckoElement.Create);
		}



		public bool DispatchEvent(GeckoEvent e)
		{
			return Instance.DispatchEvent(e.Instance);
		}

		public void AddEventListener(string type, nsIDOMEventListener listener, bool useCapture, bool wantUntrusted, byte argc)
		{
			using (var nType = new nsAString(type))
			{
				Instance.AddEventListener(nType, listener, useCapture, wantUntrusted, argc);
			}
		}

		public void RemoveEventListener(string type, nsIDOMEventListener listener, bool useCapture)
		{
			using (var nType = new nsAString(type))
			{
				Instance.RemoveEventListener(nType, listener, useCapture);
			}
		}
		
	}
}
