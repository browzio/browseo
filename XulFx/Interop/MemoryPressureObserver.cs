using System;
using Gecko.Interfaces;

namespace Gecko.Interop
{
	internal sealed class MemoryPressureObserver : NsSupportsBase, nsIObserver
	{
		public void Observe(nsISupports aSubject, string aTopic, string aData)
		{
			Action<string> pressureCallback = Xpcom.MemoryPressureCallback;
			if (pressureCallback != null)
				pressureCallback(aData);
		}
	}
}
