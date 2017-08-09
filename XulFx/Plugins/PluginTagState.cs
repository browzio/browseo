using Gecko.Interfaces;

namespace Gecko.Plugins
{
	public enum PluginTagState
	{
		Enabled = (int)nsIPluginTagConsts.STATE_ENABLED,
		Disabled = (int)nsIPluginTagConsts.STATE_DISABLED,
		ClickToPlay = (int)nsIPluginTagConsts.STATE_CLICKTOPLAY,

	}
}
