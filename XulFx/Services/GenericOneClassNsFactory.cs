using Gecko.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Gecko.Services
{
	public class GenericOneClassNsFactory<TFactory, TType>
		: BaseNsFactory<TFactory>, nsIFactory
		where TFactory : nsIFactory, new()
		where TType : new()
	{

		protected virtual IntPtr Create(nsISupports aOuter, ref Guid iid)
		{
			IntPtr result = IntPtr.Zero;
			IntPtr iUnknownForObject = Marshal.GetIUnknownForObject(new TType());
			try
			{
				Marshal.ThrowExceptionForHR(Marshal.QueryInterface(iUnknownForObject, ref iid, out result));
			}
			finally
			{
				Marshal.Release(iUnknownForObject);
			}
			return result;
		}

		public IntPtr CreateInstance(nsISupports aOuter, ref Guid iid)
		{
			return Create(aOuter, ref iid);
		}

		public void LockFactory(bool @lock)
		{

		}
	}
}
