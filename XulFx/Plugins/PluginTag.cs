using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.Plugins
{
	public sealed class PluginTag : ComObject<nsIPluginTag>, IGeckoObjectWrapper
	{
		public static PluginTag Create(nsIPluginTag instance)
		{
			return new PluginTag(instance);
		}

		private PluginTag(nsIPluginTag instance)
			: base(instance)
		{
			
		}

		public bool Blocklisted
		{
			get { return Instance.GetBlocklistedAttribute(); }
		}

		public string Description
		{
			get { return nsString.Get(Instance.GetDescriptionAttribute); }
		}

		public PluginTagState Enabled
		{
			get { return (PluginTagState)Instance.GetEnabledStateAttribute(); }
			set { Instance.SetEnabledStateAttribute((uint)value); }
		}

		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
		}

		public string FileName
		{
			get { return nsString.Get(Instance.GetFilenameAttribute); }
		}

		public string Fullpath
		{
			get { return nsString.Get(Instance.GetFullpathAttribute); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
		}

		public string Version
		{
			get { return nsString.Get(Instance.GetVersionAttribute); }
		}
	}
}
