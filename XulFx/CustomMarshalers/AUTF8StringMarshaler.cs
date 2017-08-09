using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Gecko.CustomMarshalers
{
	/// <summary>
	/// Custom Marshaler for xpcom/xulrunner native type nsAUTF8String
	/// 
	/// To use managed type nsAStringBase as parameter and apply attribute:
	/// [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AUTF8StringMarshaler))] nsAUTF8StringBase
	/// </summary>
	public class AUTF8StringMarshaler
		: ICustomMarshaler
	{
		public static readonly AUTF8StringMarshaler Instance = new AUTF8StringMarshaler();

		public void CleanUpManagedData(object ManagedObj)
		{

		}

		public void CleanUpNativeData(IntPtr pNativeData)
		{

		}

		public int GetNativeDataSize()
		{
			return -1;
		}

		/// <summary>
		/// Convert the passed string to Utf16 encoded string 
		/// written to memory allocated by xpcom/xulrunner itself.
		/// </summary>
		/// <param name="ManagedObj"></param>
		/// <returns>ptr to native memory containing utf16 encoded string + null term</returns>
		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			var s = ManagedObj as nsAUTF8StringBase;
			if (s == null)
				throw new MarshalDirectiveException();
			return s.Container;
		}

		/// <summary>
		/// Convert 
		/// </summary>
		/// <param name="pNativeData"></param>
		/// <returns>copy of native ut16 string as managed nsAString.</returns>
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			if (pNativeData == IntPtr.Zero)
				return null;
			return new nsAUTF8String(pNativeData);
		}

		static ICustomMarshaler GetInstance(string pstrCookie)
		{
			return AUTF8StringMarshaler.Instance;
		}
	}
}