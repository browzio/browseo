using System;
using Gecko.Interfaces;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Gecko
{
	[ComImport]
	[Guid("2AD11D92-EB79-44A2-A58C-28382082DBE1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IContextMenuUpdater
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Init([MarshalAs(UnmanagedType.IUnknown)] object contextMenu);

		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Update(uint aContextFlags, nsIContextMenuInfo menuInfo);
	}
}
