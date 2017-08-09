using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Gecko.CustomMarshalers
{
	/// <summary>
	/// Custom Marshaler for xpcom/xulrunner native type nsACString
	/// 
	/// To use managed type nsAStringBase as parameter and apply attribute:
	/// [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.ACStringMarshaler))] nsACStringBase
	/// </summary>
	public class ACStringMarshaler
		: ICustomMarshaler
	{
		public static readonly ACStringMarshaler Instance = new ACStringMarshaler();

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
			var s = ManagedObj as nsACStringBase;
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
			return new nsACString(pNativeData);
		}

		static ICustomMarshaler GetInstance(string pstrCookie)
		{
			return ACStringMarshaler.Instance;
		}
	}
}