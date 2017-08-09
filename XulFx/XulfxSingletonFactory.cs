using System;
using System.Collections.Generic;
using Gecko.Interfaces;
using System.Runtime.InteropServices;

namespace Gecko
{

	sealed class XulfxSingletonFactory : nsIFactory
	{
		private object _instance;

		public XulfxSingletonFactory(object instance)
		{
			_instance = instance;
		}

		public IntPtr CreateInstance(nsISupports aOuter, ref Guid iid)
		{
			if (aOuter != null)
				Marshal.ThrowExceptionForHR(GeckoError.NS_ERROR_NO_AGGREGATION);

			IntPtr pvv;
			IntPtr pUnk = Marshal.GetIUnknownForObject(_instance);
			try
			{
				Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pUnk, ref iid, out pvv));
			}
			finally
			{
				Marshal.Release(pUnk);
			}
			return pvv;
		}

		public void LockFactory(bool @lock)
		{
			throw new NotImplementedException();
		}

	}
}
