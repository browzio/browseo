using Gecko.Interfaces;

namespace Gecko
{
	public enum EventPhase : ushort
	{
		None = (ushort)nsIDOMEventConsts.NONE,
		CapturingPhase = (ushort)nsIDOMEventConsts.CAPTURING_PHASE,
		AtTarget = (ushort)nsIDOMEventConsts.AT_TARGET,
		BubblingPhase = (ushort)nsIDOMEventConsts.BUBBLING_PHASE
	}
}
