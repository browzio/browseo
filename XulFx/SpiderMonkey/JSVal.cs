using System.Runtime.InteropServices;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpiderMonkey
{
	[StructLayout(LayoutKind.Explicit)]
	public struct JSVal: IEquatable<JSVal>
	{
		public static readonly uint TagDouble;
		public static readonly uint TagInt32;
		public static readonly uint TagUndefined;
		public static readonly uint TagBoolean;
		public static readonly uint TagMagic;
		public static readonly uint TagString;
		public static readonly uint TagNull;
		public static readonly uint TagObject;
		public static readonly JSVal Undefined;
		public static readonly JSVal Null;

		private const int TagShift = 47;

		private static T SelectPlatformDependedValue<T> (T val_t4, T val_t8)
		{
			switch (IntPtr.Size)
			{
				case 4:
					return val_t4;
				case 8:
					return val_t8;
			}
			throw new NotSupportedException();
		}

		static JSVal()
		{
			uint clear = IntPtr.Size == 4 ? 0xFFFFFF80 : 0x1FFF0U;
			TagDouble = clear | (uint)JSValueType.Double;
			TagInt32 = clear | (uint)JSValueType.Int32;
			TagUndefined = clear | (uint)JSValueType.Undefined;
			TagBoolean = clear | (uint)JSValueType.Boolean;
			TagMagic = clear | (uint)JSValueType.Magic;
			TagString = clear | (uint)JSValueType.String;
			TagNull = clear | (uint)JSValueType.Null;
			TagObject = clear | (uint)JSValueType.Object;

			Undefined = new JSVal() { Tag = TagUndefined };
			Null = new JSVal() { Tag = TagNull };
		}

		[FieldOffset(0)]
		private ulong _raw;
		[FieldOffset(0)]
		private IntPtr _asPointer;
		[FieldOffset(0)]
		private int _asInt32;
		[FieldOffset(0)]
		private uint _asUint32;
		[FieldOffset(0)]
		private double _asDouble;
		[FieldOffset(4)]
		private uint _tag;

		public static JSVal FromRaw(byte[] raw)
		{
			if (raw == null)
				throw new ArgumentNullException();
			if (raw.Length != Marshal.SizeOf(typeof(JSVal)))
				throw new ArgumentOutOfRangeException();

			var value = new JSVal();
			value._raw = BitConverter.ToUInt64(raw, 0);
			return value;
		}

		public static JSVal FromRaw(ulong raw)
		{
			var value = new JSVal();
			value._raw = raw;
			return value;
		}

		internal static JSVal FromPointer(IntPtr ptr, uint tag)
		{
			if (ptr == IntPtr.Zero)
				throw new ArgumentOutOfRangeException("ptr");

			var value = new JSVal();
			if (IntPtr.Size == 8)
				Debug.Assert(((ulong)ptr >> TagShift) == 0UL);
			value._asPointer = ptr;
			value.Tag = tag;
			return value;
		}

		public static JSVal Create(IntPtr obj)
		{
			return JSVal.FromPointer(obj, TagObject);
		}

		public static JSVal Create(int value)
		{
			var val = new JSVal();
			val.Tag = TagInt32;
			val._asInt32 = value;
			return val;
		}

		public static JSVal Create(bool value)
		{
			var val = new JSVal();
			val.Tag = TagBoolean;
			val._asInt32 = value ? 1 : 0;
			return val;
		}

		public static JSVal Create(double value)
		{
			JSVal val = new JSVal();
			val._asDouble = value;
			return val;
		}

		public byte[] Raw
		{
			get
			{
				return BitConverter.GetBytes(this._raw);
			}
		}

		public ulong RawValue
		{
			get
			{
				return this._raw;
			}
		}

		public override string ToString()
		{
			if(this.IsBoolean)
				return ToBoolean().ToString();
 			if(this.IsInt32)
				return ToInt32().ToString();
			if (this.IsUndefined || this.IsNull)
				return null;
				
			return base.ToString();
		}

		public bool IsNull
		{
			get
			{
				return SelectPlatformDependedValue(this._tag == TagNull, this._raw == ((ulong)TagNull) << TagShift);
			}
		}
		public bool IsUndefined
		{
			get
			{
				return SelectPlatformDependedValue(this._tag == TagUndefined, this._raw == (((ulong)TagUndefined) << TagShift));
			}
		}
		public bool IsBoolean
		{
			get
			{
				return SelectPlatformDependedValue(this._tag, (uint)(this._raw >> TagShift)) == TagBoolean;
			}
		}
		public bool IsNumber
		{
			get
			{
				return SelectPlatformDependedValue(this._tag <= TagInt32, this._raw < (((ulong)TagUndefined) << TagShift));
			}
		}
		public bool IsInt32
		{
			get
			{
				return SelectPlatformDependedValue(this._tag, (uint)(this._raw >> TagShift)) == TagInt32;
			}
		}
		public bool IsDouble
		{
			get
			{
				return SelectPlatformDependedValue(this._tag <= TagDouble , this._raw <= (((ulong)TagDouble) << TagShift));
			}
		}
		public bool IsString
		{
			get
			{
				return SelectPlatformDependedValue(this._tag, (uint)(this._raw >> TagShift)) == TagString;
			}
		}

		public bool IsPrimitive
		{
			get
			{
				return SelectPlatformDependedValue(this._tag < TagObject, this._raw < (((ulong)TagObject) << TagShift));
			}
		}

		public bool IsObject
		{
			get
			{
				return !this.IsPrimitive;
			}
		}

		private IntPtr AsPointer
		{
			get { return new IntPtr(SelectPlatformDependedValue((long)this._asInt32, (long)this._raw & 0x00007FFFFFFFFFFFL)); }
		}

		private uint Tag
		{
			set
			{
				if(IntPtr.Size == 4)
				{
					this._tag = value;
				}
				else
				{
					this._raw = this._raw | (((ulong)value) << TagShift);
				}
			}
		}

		public static int Size
		{
			get
			{
				return Marshal.SizeOf(typeof(JSVal));
			}
		}

		#region Converting
		
		public bool ToBoolean()
		{
			if(this.IsBoolean)
				return (this._asUint32 & 0xFFFFFFFF) != 0;
			throw new InvalidCastException();
		}

		public bool AsBoolean(out bool value)
		{
			bool canCast = this.IsBoolean;
			if (canCast)
				value = this.ToBoolean();
			else
				value = false;
			return canCast;
		}

		public int ToInt32()
		{
			if(this.IsInt32) return this._asInt32;
			throw new InvalidCastException();
		}

		public bool AsInt32(out int value)
		{
			bool canCast = this.IsInt32;
			if (canCast)
				value = this.ToInt32();
			else
				value = 0;
			return canCast;
		}

		#endregion

		#region IEquatable

		public bool Equals (JSVal other)
		{
			return this._raw == other._raw;
		}

		public static bool operator ==(JSVal a, JSVal b)
		{
			return a._raw == b._raw;
		}

		public static bool operator !=(JSVal a, JSVal b)
		{
			return a._raw != b._raw;
		}

		#endregion

		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == typeof(JSVal) && this.Equals((JSVal)obj);
		}

		public override int GetHashCode()
		{
			return this._raw.GetHashCode();
		}
	}
}
