using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Javascript;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.Interop
{
	public static partial class ExtensionMethods
	{
		#region ComObject

		/// <summary>
		/// Method for getting specific function in com object
		/// </summary>
		/// <typeparam name="TDelegate"></typeparam>
		/// <param name="slot"></param>
		/// <param name="method"></param>
		/// <returns></returns>
		public static bool GetDelegateForComMethod<TInterface, TDelegate>(this ComObject<TInterface> me, int slot, out TDelegate method)
			where TInterface : class
			where TDelegate : class
		{
			IntPtr comInterfaceForObject = Marshal.GetComInterfaceForObject(me._instance, typeof(TInterface));
			if (comInterfaceForObject == IntPtr.Zero)
			{
				method = null;
				return false;
			}
			bool flag = false;
			try
			{
				IntPtr ptr = Marshal.ReadIntPtr(Marshal.ReadIntPtr(comInterfaceForObject, 0), slot * IntPtr.Size);
				method = (TDelegate)(object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate));
				flag = true;
			}
			finally
			{
				Marshal.Release(comInterfaceForObject);
			}
			return flag;
		}

		public static TDelegate GetDelegateForComMethod<TInterface, TDelegate>(this ComObject<TInterface> me, Delegate method)
			where TInterface : class
			where TDelegate : class
		{
			int slot = GetSlotOfComMethod<TInterface>(method);
			Xpcom.DebugPrint(
				"Slot for {0}.{1} = {2}",
				typeof(TInterface).Name,
				typeof(TDelegate).Name.Replace(typeof(TInterface).Name, string.Empty).Replace("Delegate", string.Empty),
				slot);

			TDelegate @delegate;
			if (!GetDelegateForComMethod<TInterface, TDelegate>(me, slot, out @delegate))
				throw new MethodAccessException();
			return @delegate;
		}

		private static int GetSlotOfComMethod<TInterface>(Delegate method)
		{
			if (method == null)
				throw new ArgumentNullException("method");

			MethodInfo methodInfo = method.Method;
			if (methodInfo.DeclaringType != typeof(TInterface))
				throw new ArgumentOutOfRangeException("method");
			return Marshal.GetComSlotForMethodInfo(methodInfo);
		}

		#endregion


		/// <summary>
		/// Function that check if object is null -> then call wrapper creator
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <typeparam name="TWrapper"></typeparam>
		/// <param name="comObj"></param>
		/// <param name="wrapper"></param>
		/// <returns></returns>
		public static TWrapper Wrap<TInterface, TWrapper>(this TInterface comObj, Func<TInterface, TWrapper> wrapper)
			where TInterface : class
			where TWrapper : class, IGeckoObjectWrapper//, IComObject
		{
			if (!typeof(IComObject).IsAssignableFrom(typeof(TWrapper)))
				throw new ArgumentOutOfRangeException("TWrapper");
			
			TWrapper wrappedObject = null;
			if (comObj != null)
			{
				wrappedObject = (TWrapper)GeckoObjectCache.Get<TInterface, TWrapper>(comObj);
				if (wrappedObject == null)
				{
					wrappedObject = wrapper(comObj);
					GeckoObjectCache.Set(comObj, wrappedObject);
				}
				else
				{
					Xpcom.FreeComObject(ref comObj);
				}
			}
			return wrappedObject;
		}

		/// <summary>
		/// Function that check if object is null -> then call wrapper creator
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <typeparam name="TWrapper"></typeparam>
		/// <param name="comObj"></param>
		/// <param name="wrapper"></param>
		/// <returns></returns>
		internal static TWrapper Wrap<TInterface, TWrapper, TElementInterface, TElementWrapper>(
			this TInterface comObj,
			Func<TInterface, Func<TElementInterface, TElementWrapper>, TWrapper> wrapper,
			Func<TElementInterface, TElementWrapper> elementCreator
			)
			where TInterface : class
			where TElementInterface : class
			where TWrapper : class, IGeckoObjectWrapper, IComObject
			where TElementWrapper : class, IGeckoObjectWrapper, IComObject
		{
			//if (!typeof(IComObject).IsAssignableFrom(typeof(TWrapper)))
			//	throw new ArgumentOutOfRangeException("TWrapper");
			//if (!typeof(IComObject).IsAssignableFrom(typeof(TElementWrapper)))
			//	throw new ArgumentOutOfRangeException("TElementWrapper");

			TWrapper wrappedObject = null;
			if (comObj != null)
			{
				wrappedObject = (TWrapper)GeckoObjectCache.Get<TInterface, TWrapper>(comObj);
				if (wrappedObject == null)
				{
					wrappedObject = wrapper(comObj, elementCreator);
					GeckoObjectCache.Set(comObj, wrappedObject);
				}
				else
				{
					Xpcom.FreeComObject(ref comObj);
				}
			}
			return wrappedObject;
		}

		///// <summary>
		///// Function that check if object is null -> then call property getter,check returned object, and if it not null -> calls wrapper creator
		///// </summary>
		///// <typeparam name="TWrapper"></typeparam>
		///// <typeparam name="TGeckoObject2"></typeparam>
		///// <typeparam name="TGeckoObject1"></typeparam>
		///// <param name="obj"></param>
		///// <param name="wrapper"></param>
		///// <returns></returns>
		//public static TWrapper Wrap<TGeckoObject1, TGeckoObject2, TWrapper>(this TGeckoObject1 obj, Func<TGeckoObject1, TGeckoObject2> getter, Func<TGeckoObject2, TWrapper> wrapper)
		//	where TGeckoObject1 : class
		//	where TGeckoObject2 : class
		//	where TWrapper : class
		//{
		//	if (obj == null) return null;
		//	var obj1 = getter(obj);
		//	return obj1 == null ? null : wrapper(obj1);
		//}

		public static ComObject<TInterface> AsComObject<TInterface>(this TInterface instance)
			where TInterface : class
		{
			if (instance == null) return null;
			return new ComObject<TInterface>(instance);
		}

        public static TInterface GetProperty<TInterface>(this IGeckoObjectWrapper thisObj, string propertyName)
            where TInterface : class
        {
            using (Variant propertyValue = GeckoJavascriptBridge.GetService().GetProperty(thisObj, propertyName))
            {
                VariantDataType dataType = propertyValue.DataType;
                if (dataType == VariantDataType.Void || dataType == VariantDataType.Empty)
                    return null;
                if (dataType == VariantDataType.InterfaceIs || dataType == VariantDataType.Interface)
                {
                    return Xpcom.QueryInterface<TInterface>(propertyValue.ToObject());
                }
            }
            throw new InvalidCastException();
        }

        public static void SetProperty<TInterface>(this IGeckoObjectWrapper thisObj, string propertyName, TInterface value)
            where TInterface : class
        {
            using (Variant thisObjArg = new Variant(thisObj), valueObj = new Variant(value))
            {
                GeckoJavascriptBridge.GetService().SetProperty(thisObjArg, propertyName, valueObj);
            }
        }

        public static void SetProperty(this IGeckoObjectWrapper thisObj, string propertyName, string value)
        {
            using (Variant thisObjArg = new Variant(thisObj), valueObj = new Variant(value))
            {
                GeckoJavascriptBridge.GetService().SetProperty(thisObjArg, propertyName, valueObj);
            }
        }

        public static void SetProperty(this IGeckoObjectWrapper thisObj, string propertyName, bool value)
        {
            using (Variant thisObjArg = new Variant(thisObj), valueObj = new Variant(value))
            {
                GeckoJavascriptBridge.GetService().SetProperty(thisObjArg, propertyName, valueObj);
            }
        }

        public static bool GetBooleanProperty(this IGeckoObjectWrapper thisObj, string propertyName)
        {
            bool value;
            using (Variant propertyValue = GeckoJavascriptBridge.GetService().GetProperty(thisObj, propertyName))
            {
                if (propertyValue.AsBoolean(out value))
                    return value;
            }
            throw new InvalidCastException();
        }

        public static string GetStringProperty(this IGeckoObjectWrapper thisObj, string propertyName)
        {
            string value;
            using (Variant propertyValue = GeckoJavascriptBridge.GetService().GetProperty(thisObj, propertyName))
            {
                if (propertyValue.AsString(out value))
                    return value;
            }
            throw new InvalidCastException();
        }

        public static int GetInt32Property(this IGeckoObjectWrapper thisObj, string propertyName)
        {
            int value;
            using (Variant propertyValue = GeckoJavascriptBridge.GetService().GetProperty(thisObj, propertyName))
            {
                if (propertyValue.AsInt(out value))
                    return value;
            }
            throw new InvalidCastException();
        }

        public static void SetInt32Property(this IGeckoObjectWrapper thisObj, string propertyName, int value)
        {
            using (var thisObjArg = new Variant(thisObj))
            {
                GeckoJavascriptBridge.GetService().SetProperty(thisObjArg, propertyName, value);
            }
        }

        public static long GetInt64Property(this IGeckoObjectWrapper thisObj, string propertyName)
        {
            long value;
            using (Variant propertyValue = GeckoJavascriptBridge.GetService().GetProperty(thisObj, propertyName))
            {
                if (propertyValue.AsInt64(out value))
                    return value;
            }
            throw new InvalidCastException();
        }


        /// <summary>
        /// Returns HWND of window
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static IntPtr GetHandle(this nsIXULWindow window)
		{
			string nativeHandle;
			nsIBaseWindow baseWindow = null;
			try
			{
				baseWindow = Xpcom.QueryInterface<nsIBaseWindow>(window);
				nativeHandle = nsString.Get(baseWindow.GetNativeHandleAttribute);
			}
			finally
			{
				Xpcom.FreeComObject(ref baseWindow);
			}
			Debug.Assert(nativeHandle != null && nativeHandle.StartsWith("0x", StringComparison.OrdinalIgnoreCase), "Bad Handle");
			if (!nativeHandle.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
				throw new InvalidOperationException(string.Format("Native Handle = {0}", nativeHandle));
			
			if (IntPtr.Size == 4)
			{
				return new IntPtr(int.Parse(nativeHandle.Substring(2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture));
			}
			else
			{
				return new IntPtr(long.Parse(nativeHandle.Substring(2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture));
			}
		}

		internal static bool IsEmptyOrWhiteSpace(this string s)
		{
			return s.Length == 0 || s.Trim().Length == 0;
		}
	}
}
