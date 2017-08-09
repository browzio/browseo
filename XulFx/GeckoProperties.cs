using System;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Runtime.InteropServices;
using Gecko.CustomMarshalers;

namespace Gecko
{
	public class GeckoProperties : ComObject<nsIProperties>, IGeckoObjectWrapper
	{
		public static GeckoProperties Create(nsIProperties instance)
		{
			nsIPersistentProperties persistent = Xpcom.QueryInterface<nsIPersistentProperties>(instance);
			if (persistent != null)
			{
				Xpcom.FreeComObject(ref instance);
				return GeckoPersistentProperties.Create(persistent);
			}
			return new GeckoProperties(instance);
		}

		protected GeckoProperties(nsIProperties instance)
			: base(instance)
		{

		}

		public bool Has(string property)
		{
			return Instance.Has(property);
		}

		public bool Undefine(string property)
		{
			// If property is not exist it will throw exception
			bool flag = Instance.Has(property);
			if (flag)
			{
				Instance.Undefine(property);
			}
			return flag;
		}

		public string[] Keys
		{
			get
			{
				uint count = 0;
				IntPtr ret;
#warning test it
				Instance.GetKeys(out count, out ret);
				string[] keys = new string[count];
				if (ret != IntPtr.Zero)
				{
					var marshaler = StringMarshaler.Instance;
					try
					{
						for (int i = 0; i < count; i++)
						{
							IntPtr sPtr = Marshal.ReadIntPtr(ret, i * IntPtr.Size);
							if (sPtr != IntPtr.Zero)
							{
								keys[i] = marshaler.MarshalNativeToManaged(sPtr) as string;
								marshaler.CleanUpNativeData(sPtr);
							}
						}
					}
					finally
					{
						Xpcom.Free(ret);
					}
				}
				return keys;
			}
		}

	}

}
