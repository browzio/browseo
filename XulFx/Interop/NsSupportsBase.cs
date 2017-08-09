using System;
using Gecko.Interfaces;
using System.Runtime.InteropServices;

namespace Gecko.Interop
{
	[ComDefaultInterface(typeof(nsISupports))]
	public abstract class NsSupportsBase : nsISupports
	{
		private IntPtr _unknown;

		protected NsSupportsBase()
		{
			_unknown = Marshal.GetIUnknownForObject(this);
			Marshal.Release(_unknown);
		}

		public virtual IntPtr QueryInterface(ref Guid uuid)
		{
			IntPtr ppv;
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(_unknown, ref uuid, out ppv));
			return ppv;
		}

		public virtual uint AddRef()
		{
			return (uint)Marshal.AddRef(_unknown);
		}

		public virtual uint Release()
		{
			return (uint)Marshal.Release(_unknown);
		}
	}
}
