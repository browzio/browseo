using System;
using Gecko.Interop;
using System.Runtime.InteropServices;
using Gecko.DOM;
using Gecko.Interfaces;

namespace Gecko
{
	public class GeckoElementWeakReference : GeckoNodeWeakReference
	{
		public GeckoElementWeakReference(GeckoElement element)
			: base(element)
		{

		}

		public new virtual GeckoElement Target
		{
			get
			{
				return base.Target as GeckoElement;
			}
		}

		public new virtual GeckoElement GetAliveElement()
		{
			return (GeckoElement)base.GetAliveElement();
		}
	}

	public class GeckoElementWeakReference<T> : GeckoElementWeakReference
		where T : GeckoElement
	{
		public GeckoElementWeakReference(T obj)
			: base(obj)
		{

		}

		public new virtual T Target
		{
			get
			{
				return base.Target as T;
			}
		}

		public new virtual T GetAliveElement()
		{
			T element = base.GetAliveElement() as T;
			if (element == null)
				throw new InvalidOperationException();

			return element;
		}
	}
}
