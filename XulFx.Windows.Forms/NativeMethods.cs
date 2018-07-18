using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Gecko.Windows
{
	static class NativeMethods
	{
		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll")]
		public static extern IntPtr GetAncestor(IntPtr hWnd, uint gaFlags);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EnableWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bEnable);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll")]
		public static extern uint GetMessagePos();

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

        public const int GCL_HBRBACKGROUND = -10;
        public const int COLOR_WINDOW = 5;

        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        public static extern uint SetClassLongPtr32(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
        public static extern IntPtr SetClassLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSysColorBrush(int nIndex);

        public static IntPtr SetClassLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            //check for x64
            if (IntPtr.Size > 4)
                return SetClassLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetClassLongPtr32(hWnd, nIndex, unchecked((uint)dwNewLong.ToInt32())));
        }

    }
}
