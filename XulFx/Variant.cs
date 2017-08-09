using System;
using System.Collections.Generic;
using System.Text;
using Gecko.Interop;
using Gecko.Interfaces;
using System.Runtime.InteropServices;
using Gecko.Javascript;
using System.Diagnostics;
using Gecko.CustomMarshalers;

namespace Gecko
{
	public sealed class Variant : ComObject<nsIVariant>
	{
		private static Variant _void;
		private static Variant _null;
		public static readonly Guid NS_ISUPPORTS_IID = typeof(nsISupports).GUID;

		public static Variant Void
		{
			get
			{
				if (_void == null)
				{
					_void = new Variant();
					_void.WritableInstance.SetAsVoid();
				}
				return _void;
			}
		}

		public static Variant Null
		{
			get
			{
				if (_null == null)
				{
					_null = new Variant();
					_null.WritableInstance.SetAsEmpty();
				}
				return _null;
			}
		}

		private new nsIWritableVariant _instance;

		public Variant()
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
		}

		public Variant(sbyte value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsInt8((byte)value);
		}

		public Variant(short value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsInt16(value);
		}

		public Variant(int value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsInt32(value);
		}

		public Variant(long value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsInt64(value);
		}

		public Variant(byte value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsUint8(value);
		}

		public Variant(ushort value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsUint16(value);
		}

		public Variant(uint value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsUint32(value);
		}

		public Variant(ulong value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsUint64(value);
		}

		public Variant(float value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsFloat(value);
		}

		public Variant(double value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsDouble(value);
		}

		public Variant(string value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			if (value != null)
				_instance.SetAsWString(value);
			else
				_instance.SetAsEmpty();
		}

		public Variant(bool value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.WritableVariant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			_instance.SetAsBool(value);
		}

		public Variant(string[] value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;

			if (value == null || value.Length == 0)
			{
				_instance.SetAsEmptyArray();
			}
			else
			{
				WStringMarshaler marshaler = WStringMarshaler.Instance;

				int count = value.Length;
				var values = new IntPtr[count];
				var array = Xpcom.Alloc(IntPtr.Size * count);
				try
				{
					for (int i = 0; i < count; i++)
					{
						values[i] = marshaler.MarshalManagedToNative(value[i]);
					}
					Marshal.Copy(values, 0, array, count);
					_instance.SetAsArray((ushort)VariantDataType.UnicodeString, new UUID(), (uint)count, array);
				}
				finally
				{
					for (int i = 0; i < count; i++)
					{
						marshaler.CleanUpNativeData(values[i]);
					}
					Xpcom.Free(array);
				}
			}
		}

		public Variant(int[] value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;

			if (value == null || value.Length == 0)
			{
				_instance.SetAsEmptyArray();
			}
			else
			{
				int count = value.Length;
				var array = Xpcom.Alloc(sizeof(int) * count);
				try
				{
					Marshal.Copy(value, 0, array, count);
					_instance.SetAsArray((ushort)VariantDataType.Integer, new UUID(), (uint)count, array);
				}
				finally
				{
					Xpcom.Free(array);
				}
			}
		}

		public Variant(object value)
			: base(Xpcom.CreateInstance<nsIWritableVariant>(Contracts.Variant))
		{
			_instance = (nsIWritableVariant)base.Instance;
			if (value != null)
			{
				Guid g = NS_ISUPPORTS_IID;
				IComObject comObj = value as IComObject;
				if (comObj != null)
				{
					_instance.SetAsInterface(ref g, comObj.NativeInstance);
				}
				else
				{
					_instance.SetAsInterface(ref g, value);
				}
			}
			else
			{
				_instance.SetAsVoid();
			}
		}

		public Variant(nsIWritableVariant instance)
			: base(instance)
		{
			_instance = instance;
		}

		public Variant(nsIVariant instance)
			: base(instance)
		{
			_instance = instance as nsIWritableVariant;
		}

		public nsIWritableVariant WritableInstance
		{
			get { return _instance; }
		}

		public VariantDataType DataType
		{
			get
			{
				return (VariantDataType)Instance.GetDataTypeAttribute();
			}
		}

		public bool IsObject
		{
			get { return this.DataType.GetIsObject(); }
		}

		public bool IsBoolean
		{
			get { return this.DataType == VariantDataType.Boolean; }
		}

		public bool IsInt
		{
			get { return this.DataType.GetIsInt(); }
		}

		public bool IsFloat
		{
			get { return this.DataType.GetIsFloat(); }
		}

		public bool IsDouble
		{
			get { return this.DataType.GetIsDouble(); }
		}

		public bool IsNumeric
		{
			get { return this.DataType.GetIsNumeric(); }
		}

		public bool IsPrimitive
		{
			get { return this.DataType.GetIsPrimitive(); }
		}

		public bool IsString
		{
			get { return this.DataType.GetIsString(); }
		}

		private void AssertWritable()
		{
			if (!WritableInstance.GetWritableAttribute())
				throw new InvalidOperationException();
		}

		public void SetValue(long value)
		{
			AssertWritable();
			WritableInstance.SetAsInt64(value);
		}

		public void SetValue(int value)
		{
			AssertWritable();
			WritableInstance.SetAsInt32(value);
		}

		public void SetValue(short value)
		{
			AssertWritable();
			WritableInstance.SetAsInt16(value);
		}

		public void SetValue(sbyte value)
		{
			AssertWritable();
			WritableInstance.SetAsInt8((byte)value);
		}

		public void SetValue(byte value)
		{
			AssertWritable();
			WritableInstance.SetAsUint8(value);
		}

		public void SetValue(ushort value)
		{
			AssertWritable();
			WritableInstance.SetAsUint16(value);
		}

		public void SetValue(uint value)
		{
			AssertWritable();
			WritableInstance.SetAsUint32(value);
		}

		public void SetValue(ulong value)
		{
			AssertWritable();
			WritableInstance.SetAsUint64(value);
		}

		public void SetValue(string value)
		{
			AssertWritable();
			WritableInstance.SetAsWString(value);
		}

		public void SetValue(float value)
		{
			AssertWritable();
			WritableInstance.SetAsFloat(value);
		}

		public void SetValue(double value)
		{
			AssertWritable();
			WritableInstance.SetAsDouble(value);
		}

		public void SetValue(bool value)
		{
			AssertWritable();
			WritableInstance.SetAsBool(value);
		}

		public void SetValue(char value)
		{
			AssertWritable();
			WritableInstance.SetAsWChar(value);
		}

		public void SetValue(object value)
		{
			AssertWritable();

			if (value == null)
			{
				WritableInstance.SetAsVoid();
			}
			else
			{
				Guid g = NS_ISUPPORTS_IID;
				WritableInstance.SetAsInterface(ref g, value);
			}
		}

		[DebuggerNonUserCodeAttribute, DebuggerStepThrough]
		public Variant AsEvalResult()
		{
			VariantDataType dataType = (VariantDataType)Instance.GetDataTypeAttribute();
			if (dataType == VariantDataType.Interface || dataType == VariantDataType.InterfaceIs)
			{
				object result = ToObject();
				try
				{
					GeckoJavaScriptException jsex = null; 
					var error = result as nsIScriptError;
					if (error != null)
					{
						jsex = result as GeckoJavaScriptException;
						if (jsex == null)
						{
							try
							{
								jsex = new GeckoJavaScriptException(
									nsString.Get(error.GetErrorMessageAttribute),
									nsString.Get(error.GetSourceNameAttribute),
									(int)error.GetLineNumberAttribute(),
									(int)error.GetColumnNumberAttribute()
								);
							}
							finally
							{
								Xpcom.FreeComObject(ref error);
							}
						}

						if (jsex != null)
							throw jsex;
					}
				}
				finally
				{
					Xpcom.FreeComObject(ref result);
				}
			}
			return this;
		}

		public string CastToString()
		{
			VariantDataType dataType = (VariantDataType)Instance.GetDataTypeAttribute();
			if (dataType == VariantDataType.Boolean)
				return Instance.GetAsBool() ? "True" : "False";
			if (dataType == VariantDataType.Empty || dataType == VariantDataType.Void)
				return null;
			if (dataType.GetIsObject())
			{
				return GeckoJavascriptBridge.GetService().ConvertToString(this);
			}
			return Instance.GetAsWString();
		}

		public string AsString()
		{
			var dataType = (VariantDataType)Instance.GetDataTypeAttribute();
			if (dataType.GetIsString())
				return Instance.GetAsWString();
			if (dataType == VariantDataType.Char)
				return ((char)Instance.GetAsChar()).ToString();
			if (dataType == VariantDataType.UnicodeChar)
				return Instance.GetAsWChar().ToString();
			return null;
		}

		public bool AsString(out string value)
		{
			return Instance.AsString(out value);
		}

		public bool AsBoolean(bool defaultValue)
		{
			var dataType = (VariantDataType)Instance.GetDataTypeAttribute();
			if (dataType == VariantDataType.Boolean)
				return Instance.GetAsBool();
			return defaultValue;
		}

		public bool AsBoolean(out bool value)
		{
			return Instance.AsBoolean(out value);
		}

		public int AsInt(int defaultValue)
		{
			var dataType = (VariantDataType)Instance.GetDataTypeAttribute();
			if (dataType.GetIsInt())
				return Instance.GetAsInt32();
			return defaultValue;
		}

		public bool AsInt(out int value)
		{
			return Instance.AsInt32(out value);
		}

		public bool AsInt64(out long value)
		{
			return Instance.AsInt64(out value);
		}

		public double AsDouble(double defaultValue)
		{
			var dataType = (VariantDataType)Instance.GetDataTypeAttribute();
			if (dataType.GetIsDouble())
				return Instance.GetAsDouble();
			return defaultValue;
		}

		public double AsNumeric(double defaultValue)
		{
			var dataType = (VariantDataType)Instance.GetDataTypeAttribute();
			if (dataType.GetIsNumeric())
				return Instance.GetAsDouble();
			return defaultValue;
		}

		public object ToObject()
		{
			return ToObject((VariantDataType)Instance.GetDataTypeAttribute());
		}

		public object AsObject()
		{
			return ToObject((VariantDataType)Instance.GetDataTypeAttribute());
		}

		public TInterface AsObject<TInterface>()
			where TInterface : class
		{
			var dataType = (VariantDataType)Instance.GetDataTypeAttribute();
			if(dataType == VariantDataType.InterfaceIs || dataType == VariantDataType.Interface)
			{
				UUID iid;
				object unknownObj = null;
				IntPtr pUnk = Instance.GetAsInterface(out iid);
				try
				{
					unknownObj = Marshal.GetObjectForIUnknown(pUnk);
					return Xpcom.QueryInterface<TInterface>(unknownObj);
				}
				finally
				{
					if (pUnk != IntPtr.Zero)
						Marshal.Release(pUnk);
					iid.Free();
					Xpcom.FreeComObject(ref unknownObj);
				}
			}
			return null;
		}

		private object ToObject(VariantDataType dataType)
		{
			switch (dataType)
			{
				case VariantDataType.UnicodeString:
				case VariantDataType.UnicodeStringFixedSize:
				case VariantDataType.UTF8String:
				case VariantDataType.AnsiString:
				case VariantDataType.AnsiStringFixedSize:
				case VariantDataType.AString:
				case VariantDataType.CString:
				case VariantDataType.DOMString:
					return Instance.GetAsWString();
				case VariantDataType.Boolean:
					return Instance.GetAsBool();
				case VariantDataType.Byte:
					return Instance.GetAsUint8();
				case VariantDataType.Char:
					return (char)Instance.GetAsChar();
				case VariantDataType.Double:
					return Instance.GetAsDouble();
				case VariantDataType.Float:
					return Instance.GetAsFloat();
				case VariantDataType.Guid:
					return 2;
				case VariantDataType.Integer:
					return Instance.GetAsInt32();
				case VariantDataType.Interface:
				case VariantDataType.InterfaceIs:
					UUID iid;
					IntPtr pUnk = Instance.GetAsInterface(out iid);
					try
					{
						return Marshal.GetObjectForIUnknown(pUnk);
					}
					finally
					{
						if (pUnk != IntPtr.Zero)
							Marshal.Release(pUnk);
						iid.Free();
					}
				case VariantDataType.Long:
					return Instance.GetAsInt64();
				case VariantDataType.SByte:
					return Instance.GetAsInt8();
				case VariantDataType.Short:
					return Instance.GetAsInt16();
				case VariantDataType.UnicodeChar:
					return Instance.GetAsWChar();
				case VariantDataType.UnsignedInteger:
					return Instance.GetAsUint32();
				case VariantDataType.UnsignedLong:
					return Instance.GetAsUint64();
				case VariantDataType.UnsignedShort:
					return Instance.GetAsUint16();
				case VariantDataType.Void:
					return null;
			}
			return null;
		}

		public override string ToString()
		{
			try
			{
				AsEvalResult();
			}
			catch (GeckoException e)
			{
				return e.Message;
			}
			return base.ToString();
		}
	}

	public enum VariantDataType
	{
		SByte = nsIDataTypeConsts.VTYPE_INT8,
		Short = nsIDataTypeConsts.VTYPE_INT16,
		Integer = nsIDataTypeConsts.VTYPE_INT32,
		Long = nsIDataTypeConsts.VTYPE_INT64,
		Byte = nsIDataTypeConsts.VTYPE_UINT8,
		UnsignedShort = nsIDataTypeConsts.VTYPE_UINT16,
		UnsignedInteger = nsIDataTypeConsts.VTYPE_UINT32,
		UnsignedLong = nsIDataTypeConsts.VTYPE_UINT64,
		Float = nsIDataTypeConsts.VTYPE_FLOAT,
		Double = nsIDataTypeConsts.VTYPE_DOUBLE,
		Boolean = nsIDataTypeConsts.VTYPE_BOOL,
		Char = nsIDataTypeConsts.VTYPE_CHAR,
		UnicodeChar = nsIDataTypeConsts.VTYPE_WCHAR,
		Void = nsIDataTypeConsts.VTYPE_VOID,
		Guid = nsIDataTypeConsts.VTYPE_ID,
		DOMString = nsIDataTypeConsts.VTYPE_DOMSTRING,
		AnsiString = nsIDataTypeConsts.VTYPE_CHAR_STR,
		UnicodeString = nsIDataTypeConsts.VTYPE_WCHAR_STR,
		Interface = nsIDataTypeConsts.VTYPE_INTERFACE,
		InterfaceIs = nsIDataTypeConsts.VTYPE_INTERFACE_IS,
		Array = nsIDataTypeConsts.VTYPE_ARRAY,
		AnsiStringFixedSize = nsIDataTypeConsts.VTYPE_STRING_SIZE_IS,
		UnicodeStringFixedSize = nsIDataTypeConsts.VTYPE_WSTRING_SIZE_IS,
		UTF8String = nsIDataTypeConsts.VTYPE_UTF8STRING,
		CString = nsIDataTypeConsts.VTYPE_CSTRING,
		AString = nsIDataTypeConsts.VTYPE_ASTRING,
		EmptyArray = nsIDataTypeConsts.VTYPE_EMPTY_ARRAY,
		Empty = nsIDataTypeConsts.VTYPE_EMPTY,
	}

	public static class VariantExtension
	{
		public static object GetAsObject(this nsIVariant Instance)
		{
			UUID iid;
			IntPtr pUnk = Instance.GetAsInterface(out iid);
			try
			{
				return Marshal.GetObjectForIUnknown(pUnk);
			}
			finally
			{
				if (pUnk != IntPtr.Zero)
					Marshal.Release(pUnk);
				iid.Free();
			}
		}
	
		public static bool GetIsFloat(this VariantDataType dataType)
		{
			return dataType == VariantDataType.Float;
		}

		public static bool GetIsDouble(this VariantDataType dataType)
		{
			return dataType == VariantDataType.Float || dataType == VariantDataType.Double;
		}

		public static bool GetIsInt(this VariantDataType dataType)
		{
			return dataType < VariantDataType.UnsignedLong && dataType != VariantDataType.Long;
		}

		public static bool GetIsInt64(this VariantDataType dataType)
		{
			return dataType <= VariantDataType.UnsignedLong;
		}

		public static bool GetIsNumeric(this VariantDataType dataType)
		{
			return dataType <= VariantDataType.Double;
		}

		public static bool GetIsObject(this VariantDataType dataType)
		{
			return dataType == VariantDataType.InterfaceIs || dataType == VariantDataType.Interface;
		}

		public static bool GetIsString(this VariantDataType dataType)
		{
			return (
				dataType == VariantDataType.UnicodeString
				|| dataType == VariantDataType.UnicodeStringFixedSize
				|| dataType == VariantDataType.UTF8String
				|| dataType == VariantDataType.AnsiString
				|| dataType == VariantDataType.AnsiStringFixedSize
				|| dataType == VariantDataType.AString
				|| dataType == VariantDataType.CString
				|| dataType == VariantDataType.DOMString
			);
		}

		public static bool GetIsPrimitive(this VariantDataType dataType)
		{
			return dataType != VariantDataType.Array
				&& dataType != VariantDataType.EmptyArray
				&& dataType != VariantDataType.Guid
				&& dataType != VariantDataType.Interface
				&& dataType != VariantDataType.InterfaceIs;
		}

		public static bool AsInt32(this nsIVariant instance, out int value)
		{
			VariantDataType dataType = (VariantDataType)instance.GetDataTypeAttribute();
			if (dataType.GetIsInt())
			{
				value = instance.GetAsInt32();
				return true;
			}
			value = 0;
			return false;
		}

		public static bool AsInt64(this nsIVariant instance, out long value)
		{
			VariantDataType dataType = (VariantDataType)instance.GetDataTypeAttribute();
			if (dataType.GetIsInt64())
			{
				value = instance.GetAsInt64();
				return true;
			}
			value = 0L;
			return false;
		}

		public static bool AsBoolean(this nsIVariant instance, out bool value)
		{
			if ((VariantDataType)instance.GetDataTypeAttribute() == VariantDataType.Boolean)
			{
				value = instance.GetAsBool();
				return true;
			}
			value = false;
			return false;
		}

		public static bool AsString(this nsIVariant instance, out string value)
		{
			if (GetIsString((VariantDataType)instance.GetDataTypeAttribute()))
			{
				value = instance.GetAsWString();
				return true;
			}
			value = null;
			return false;
		}

		public static bool AsObject(this nsIVariant instance, out object value)
		{
			if (GetIsObject((VariantDataType)instance.GetDataTypeAttribute()))
			{
				value = GetAsObject(instance);
				return true;
			}
			value = null;
			return false;
		}

		public static bool AsFloat(this nsIVariant instance, out float value)
		{
			if (GetIsFloat((VariantDataType)instance.GetDataTypeAttribute()))
			{
				value = instance.GetAsFloat();
				return true;
			}
			value = 0;
			return false;
		}

		public static bool AsDouble(this nsIVariant instance, out double value)
		{
			if (GetIsDouble((VariantDataType)instance.GetDataTypeAttribute()))
			{
				value = instance.GetAsDouble();
				return true;
			}
			value = 0;
			return false;
		}

		public static bool AsNumeric(this nsIVariant instance, out double value)
		{
			if (GetIsNumeric((VariantDataType)instance.GetDataTypeAttribute()))
			{
				value = instance.GetAsDouble();
				return true;
			}
			value = 0;
			return false;
		}
	}
}