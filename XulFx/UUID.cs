using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Gecko
{
	/// <summary>
	/// Contains pointer to Guid
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct UUID : IDisposable
	{
		[FieldOffset(0)]
		private IntPtr _this;
		[FieldOffset(0)]
		private int _32bit; // not used (fix MarshalDirectiveException on 32-bit .NET)

		public UUID(IntPtr nsCIDPtr)
		{
			if (nsCIDPtr == IntPtr.Zero)
				throw new ArgumentOutOfRangeException("nsCIDPtr");
			_32bit = 0;
			_this = nsCIDPtr;
		}

		public UUID(Guid guid)
		{
			_32bit = 0;
			_this = Xpcom.Alloc(16);
			Marshal.Copy(guid.ToByteArray(), 0, _this, 16);
		}

		void IDisposable.Dispose()
		{
			this.Free();
		}

		public bool IsEmpty
		{
			get
			{
				if (this.IsNull)
					return false;
				return this.ToGuid() == Guid.Empty;
			}
		}

		public bool IsNull
		{
			get { return _this == IntPtr.Zero; }
		}

		public void Init(Guid guid)
		{
			if (this.IsNull)
				_this = Xpcom.Alloc(16);
			Marshal.Copy(guid.ToByteArray(), 0, _this, 16);
		}

		public void Free()
		{
			if (_this != IntPtr.Zero)
			{
				Xpcom.Free(_this);
				_this = IntPtr.Zero;
			}
		}

		public Guid ToGuid()
		{
			if (_this == IntPtr.Zero)
				throw new InvalidCastException();
			var data = new byte[16];
			Marshal.Copy(_this, data, 0, 16);
			return new Guid(data);
		}
		
		public override string ToString()
		{
			if (this.IsNull)
				return base.ToString();
			return this.ToGuid().ToString();
		}

		public static implicit operator Guid(UUID uuid)
		{
			return uuid.ToGuid();
		}

		public static implicit operator IntPtr(UUID uuid)
		{
			return uuid._this;
		}
	}

}
