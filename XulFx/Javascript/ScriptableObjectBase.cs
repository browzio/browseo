using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Gecko.Interfaces;

namespace Gecko.Javascript
{
	public abstract class ScriptableObjectBase : nsIClassInfo
	{

		public ScriptableObjectBase()
		{

		}


		protected virtual IEnumerable<Guid> GetInterfaces()
		{
			Type classInfo = typeof(nsIClassInfo);
			Type unknown = typeof(nsISupports);
			foreach (Type t in this.GetType().GetInterfaces())
			{
				if (t != classInfo && t != unknown)
					yield return t.GUID;
			}
		}

		#region nsIClassInfo

		void nsIClassInfo.GetInterfaces(out uint count, out IntPtr array)
		{
			count = 0;
			array = IntPtr.Zero;

			var iids = new List<IntPtr>();
			try
			{
				foreach (Guid guid in GetInterfaces())
				{
					IntPtr iid = Xpcom.Alloc(16);
					if (iid == IntPtr.Zero)
						throw new OutOfMemoryException();
					Marshal.Copy(guid.ToByteArray(), 0, iid, 16);
					iids.Add(iid);
				}

				int aCount = iids.Count;
				array = Xpcom.Alloc(IntPtr.Size * aCount);
				for (var i = 0; i < aCount; i++)
				{
					Marshal.WriteIntPtr(array, IntPtr.Size * i, iids[i]);
				}
				count = (uint)aCount;
			}
			catch (Exception)
			{
				if (array != IntPtr.Zero)
					Xpcom.Free(array);

				count = 0;
				array = IntPtr.Zero;

				for (var i = iids.Count - 1; i >= 0; i--)
				{
					Xpcom.Free(iids[i]);
				}

				throw;
			}
		}

		nsIXPCScriptable nsIClassInfo.GetScriptableHelper()
		{
			return null;
		}

		public virtual string GetContractIDAttribute()
		{
			return null;
		}

		public virtual string GetClassDescriptionAttribute()
		{
			return null;
		}

		public virtual UUID GetClassIDAttribute()
		{
			throw Marshal.GetExceptionForHR(GeckoError.NS_ERROR_NOT_AVAILABLE);
		}

		public virtual uint GetFlagsAttribute()
		{
			return nsIClassInfoConsts.MAIN_THREAD_ONLY | nsIClassInfoConsts.DOM_OBJECT;
		}

		public virtual UUID GetClassIDNoAllocAttribute()
		{
			throw Marshal.GetExceptionForHR(GeckoError.NS_ERROR_NOT_AVAILABLE);
		}

		#endregion

	}
}
