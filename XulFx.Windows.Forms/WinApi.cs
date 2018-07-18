using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.Windows
{
	public static class WinApi
	{
		public const int WS_OVERLAPPED = 0;
		public const int WS_POPUP = unchecked((int)0x80000000);
		public const int WS_CHILD = 0x40000000;
		public const int WS_MINIMIZE = 0x20000000;
		public const int WS_VISIBLE = 0x10000000;
		public const int WS_DISABLED = 0x8000000;
		public const int WS_CLIPSIBLINGS = 0x4000000;
		public const int WS_CLIPCHILDREN = 0x2000000;
		public const int WS_MAXIMIZE = 0x1000000;
		public const int WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
		public const int WS_BORDER = 0x800000;
		public const int WS_DLGFRAME = 0x400000;
		public const int WS_VSCROLL = 0x200000;
		public const int WS_HSCROLL = 0x100000;
		public const int WS_SYSMENU = 0x80000;
		public const int WS_THICKFRAME = 0x40000;
		public const int WS_GROUP = 0x20000;
		public const int WS_TABSTOP = 0x10000;
		public const int WS_MINIMIZEBOX = 0x20000;
		public const int WS_MAXIMIZEBOX = 0x10000;
		public const int WS_TILED = WS_OVERLAPPED;
		public const int WS_ICONIC = WS_MINIMIZE;
		public const int WS_SIZEBOX = WS_THICKFRAME;

		public const int WS_EX_CONTROLPARENT = 0x00010000;

		public const int GA_ROOT = 2;
		public const int GWL_EXSTYLE = -20;
		public const int GWL_STYLE = -16;
		public const int GWL_HWNDPARENT = -8;
		public const int WM_MOUSEACTIVATE = 0x21;
		public const int WM_ACTIVATE = 0x0006;
		public const int MA_ACTIVATE = 0x1;

		public const int WM_SETFOCUS = 0x0007;
		public const int WM_KILLFOCUS = 0x0008;

		public const int WM_KEYDOWN = 0x100;
		public const int WM_KEYUP = 0x101;
		public const int WM_CHAR = 0x102;
		public const int WM_CONTEXTMENU = 0x007B;

		public const int WM_ENABLE = 0x000A;
		public const int WM_CANCELMODE = 0x001F;

		public const int SW_SHOWNA = 8;

		public const int WM_WINDOWPOSCHANGING = 0x0046;
		public const int SWP_NOMOVE = 0x0002;
		public const int SWP_NOSIZE = 0x0001;


		public static int GetXLParam(IntPtr lparam) { return LowWord((int)lparam); }
		public static int GetYLParam(IntPtr lparam) { return HighWord((int)lparam); }
		public static int LowWord(int word) { return word & 0xFFFF; }
		public static int HighWord(int word) { return word >> 16; }

		public static IntPtr LParam(int lowWord, int highWord)
		{
			return new IntPtr((int)((((uint)highWord << 16) & 0xFFFF0000) | ((uint)lowWord & 0xFFFF)));
		}

		public static int WParam(int lowWord, int highWord)
		{
			return (int)((((uint)highWord << 16) & 0xFFFF0000) | ((uint)lowWord & 0xFFFF));
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	class WINDOWPOS
	{
		public IntPtr hwnd;
		public IntPtr hwndInsertAfter;
		public int x;
		public int y;
		public int cx;
		public int cy;
		public uint flags;
	}
}
