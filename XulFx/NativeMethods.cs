using Gecko.Interfaces;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Gecko
{
	[SuppressUnmanagedCodeSecurity]
	internal static partial class NativeMethods
	{
		internal const string XPCOM = "xul.dll";
		internal const string MOZGLUE = "mozglue.dll";
		internal static readonly string XpcomLib = Xpcom.IsLinux ? "libxul.so" : "xul.dll";

		public const int NS_OK = 0;

		[DllImport("libxulfx.so", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int XulFx_Init();

		#region XpCom Methods
		/// <summary>
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_InitXPCOM2(nsIServiceManager* *result, nsIFile* binDirectory, nsIDirectoryServiceProvider* appFileLocationProvider);
		/// </summary>
		/// <param name="serviceManager"></param>
		/// <param name="binDirectory"></param>
		/// <param name="appFileLocationProvider"></param>
		/// <returns></returns>
		[DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int NS_InitXPCOM2([MarshalAs(UnmanagedType.Interface)] out nsIServiceManager serviceManager, [MarshalAs(UnmanagedType.Interface)] nsIFile binDirectory, nsIDirectoryServiceProvider appFileLocationProvider);

		/// <summary>
		/// Shutdown XPCOM. You must call this method after you are finished
		/// using xpcom.
		/// 
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_ShutdownXPCOM(nsIServiceManager* servMgr);
		/// </summary>
		/// <param name="serviceManager"></param>
		/// <returns></returns>
		[DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int NS_ShutdownXPCOM(IntPtr serviceManager);

		/// <summary>
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_NewLocalFile(const nsAString &amp;path, bool followLinks, nsIFile* *result);
		/// </summary>
		/// <param name="path"></param>
		/// <param name="followLinks"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		[DllImport(XPCOM, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int NS_NewLocalFile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AStringMarshaler))] nsAString path, [MarshalAs(UnmanagedType.U1)] bool followLinks, [MarshalAs(UnmanagedType.Interface)] out nsIFile result);
		[DllImport(XPCOM, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int NS_NewLocalFile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AStringMarshaler))] nsAString path, [MarshalAs(UnmanagedType.U1)] bool followLinks, out IntPtr result);

		/// <summary>
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_GetComponentManager(nsIComponentManager* *result);
		/// </summary>
		/// <param name="componentManager"></param>
		/// <returns></returns>
		[DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int NS_GetComponentManager([MarshalAs(UnmanagedType.Interface)] out nsIComponentManager componentManager);

		/// <summary>
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_GetComponentRegistrar(nsIComponentRegistrar* *result);
		/// </summary>
		/// <param name="componentRegistrar"></param>
		/// <returns></returns>
		[DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int NS_GetComponentRegistrar([MarshalAs(UnmanagedType.Interface)] out nsIComponentRegistrar componentRegistrar);

		/// <summary>
		/// XPCOM_API(void*) NS_Alloc(size_t size);
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		[DllImport(MOZGLUE, EntryPoint = "moz_xmalloc", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Alloc(IntPtr size);

		/// <summary>
		/// XPCOM_API(void*) NS_Realloc(void* ptr, size_t size);
		/// </summary>
		/// <param name="ptr"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		[DllImport(MOZGLUE, EntryPoint = "moz_xrealloc", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr Realloc(IntPtr ptr, IntPtr size);

		/// <summary>
		/// XPCOM_API(void) NS_Free(void* ptr)
		/// </summary>
		/// <param name="ptr"></param>
		[DllImport(MOZGLUE, EntryPoint = "free", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void Free(IntPtr ptr);

		/// <summary>
		/// Declaration in nsXULAppAPI.h
		/// nsresult XRE_AddJarManifestLocation(NSLocationType aType, nsIFile* aLocation)
		/// </summary>
		/// <param name="aType"></param>
		/// <param name="aLocation"></param>
		/// <returns></returns>
		[DllImport(XPCOM, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		internal static extern int XRE_AddJarManifestLocation(int aType, [MarshalAs(UnmanagedType.Interface)] nsIFile aLocation);

		#endregion


		#region Strings

		[DllImport(NativeMethods.XPCOM, CallingConvention = CallingConvention.Cdecl)]
		public static extern int NS_StringContainerInit(IntPtr container);

		[DllImport(NativeMethods.XPCOM, CallingConvention = CallingConvention.Cdecl)]
		public static extern int NS_StringGetData(IntPtr str, out IntPtr data, [MarshalAs(UnmanagedType.U1)] out bool nullTerm);

		[DllImport(NativeMethods.XPCOM, CallingConvention = CallingConvention.Cdecl)]
		public static extern int NS_StringContainerFinish(IntPtr container);

		[DllImport(NativeMethods.XPCOM, CallingConvention = CallingConvention.Cdecl)]
		[return: MarshalAs(UnmanagedType.U1)]
		public static extern bool NS_StringGetIsVoid(IntPtr str);

		[DllImport(NativeMethods.XPCOM, CallingConvention = CallingConvention.Cdecl)]
		public static extern void NS_StringSetIsVoid(IntPtr str, [MarshalAs(UnmanagedType.U1)] bool isVoid);

		[DllImport(NativeMethods.XPCOM, EntryPoint = "NS_StringSetData", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
		public static extern int NS_StringSetAStringData(IntPtr aAString, string data, int length);

		#endregion
	}

}
