using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void xpcGCCallback(JSGCStatus status);

	public enum JSGCStatus
	{
		JSGC_BEGIN,
		JSGC_END
	}
}