using System;
using System.Collections.Generic;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Runtime.InteropServices;

namespace Gecko.Plugins
{
	public sealed class PluginHost : ComObject<nsIPluginHost>, IGeckoObjectWrapper
	{
		public static PluginHost GetService()
		{
			return Xpcom.GetService<nsIPluginHost>(Contracts.PluginHost).Wrap(PluginHost.Create);
		}

		private static PluginHost Create(nsIPluginHost instance)
		{
			return new PluginHost(instance);
		}

		private PluginHost(nsIPluginHost instance)
			: base(instance)
		{

		}

		public void ReloadPlugins()
		{
			Instance.ReloadPlugins();
		}

		public IEnumerable<PluginTag> GetPluginTags()
		{
			IntPtr pluginTagsArrayPtr = IntPtr.Zero;
			try
			{
				uint aCount = 0;
				Instance.GetPluginTags(out aCount, out pluginTagsArrayPtr);
				int count = (int)aCount;
				IntPtr[] tagPtrs = new IntPtr[count];
				Marshal.Copy(pluginTagsArrayPtr, tagPtrs, 0, tagPtrs.Length);
				try
				{
					for (int i = 0; i < count; i++)
					{
						yield return ((nsIPluginTag)Marshal.GetTypedObjectForIUnknown(tagPtrs[i], typeof(nsIPluginTag))).Wrap(PluginTag.Create);
					}
				}
				finally
				{
					for (int i = 0; i < count; i++)
						Marshal.Release(tagPtrs[i]);
				}
			}
			finally
			{
				if (pluginTagsArrayPtr != IntPtr.Zero)
					Xpcom.Free(pluginTagsArrayPtr);
			}
		}

		public bool SiteHasData(PluginTag tag, string domain)
		{
			return nsString.Pass<bool, nsIPluginTag>(Instance.SiteHasData, tag.Instance, domain);
		}

		public void ClearSiteData(PluginTag tag, string domain, ulong flags, long maxAge, nsIClearSiteDataCallback callback)
		{
			nsString.Set(x => Instance.ClearSiteData(tag.Instance, x, flags, maxAge, callback), domain);
		}
	}
}
