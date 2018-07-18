using Gecko;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Runtime.CompilerServices;

namespace Gecko.Javascript
{
	public sealed class JSArray : ComObject<nsIVariant>, IGeckoObjectWrapper
	{
		//private GeckoJavascriptBridge _js;

		public static JSArray Create(nsIVariant instance)
		{
			return new JSArray(instance);
		}

		private JSArray(nsIVariant instance)
			: base(instance)
		{
			//_js = GeckoJavascriptBridge.GetService();
		}

		[IndexerName("Items")]
		public ComObject<nsIVariant> this[int index]
		{
			get
			{
				if (index < 0)
					throw new IndexOutOfRangeException();

                return null;
				//return new Variant(_js.Instance.GetItem(Instance, (uint)index)).AsEvalResult();
			}

			set
			{
				if (index < 0)
					throw new IndexOutOfRangeException();

				//((IDisposable)new Variant(_js.Instance.SetItem(Instance, (uint)index, value != null ? value.Instance : null)).AsEvalResult()).Dispose();
			}
		}

		public int Length
		{
			get
			{
                //using (Variant length = _js.GetProperty(this.Instance, "length"))
                //{
                return 0;
				//}
			}
		}
	}
}
