using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Gecko
{
	[ComImport]
	[Guid("3C12805F-43F9-45D0-82F7-938924D4DC9F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IContextMenuHandler
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void OnClick([MarshalAs(UnmanagedType.IUnknown)] object sender, [MarshalAs(UnmanagedType.IUnknown)] EventArgs e);
	}
}
