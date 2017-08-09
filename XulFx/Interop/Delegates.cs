using Gecko.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Gecko.Interop
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate int nsIThreadDispatchDelegate(IntPtr @this, [MarshalAs(UnmanagedType.Interface)] nsIRunnable @event, uint flags);
}
