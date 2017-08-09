using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.U1)]
	public delegate bool xpcContextCallback(IntPtr cx, uint operation);
}
