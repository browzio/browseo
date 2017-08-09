using System.Runtime.InteropServices;

namespace Gecko
{
	public enum WhenToScroll: byte
	{
		SCROLL_ALWAYS = 0 ,
		SCROLL_IF_NOT_VISIBLE = 1,
		SCROLL_IF_NOT_FULLY_VISIBLE = 2
	}

	[StructLayout(LayoutKind.Explicit, Size = 4)]
	public struct ScrollAxis
	{
		[FieldOffset(0)]
		short mWhereToScroll;
		[FieldOffset(2)]
		WhenToScroll mWhenToScroll; // : 8
		[FieldOffset(3)]
		byte mOnlyIfPerceivedScrollableDirection; // : 1

		public bool OnlyIfPerceivedScrollableDirection
		{
			get { return (mOnlyIfPerceivedScrollableDirection & 0x1) == 0x1; }
			set { mOnlyIfPerceivedScrollableDirection = (byte)(value ? 1 : 0); }
		}
	}
}
