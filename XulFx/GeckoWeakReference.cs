using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko
{
	public class GeckoWeakReference : nsIWeakReference
	{
		protected WeakReference _weakReference;

		public GeckoWeakReference(object obj)
		{
			_weakReference = new WeakReference(obj, false);
		}

		IntPtr nsIWeakReference.QueryReferent(ref Guid uuid)
		{
			// If object is alive we take it to QueryReferentImplementation
			// else return IntPtr.Zero
			return _weakReference.IsAlive
				       ? QueryReferentImplementation(_weakReference.Target, ref uuid)
				       : IntPtr.Zero;
		}

		protected virtual IntPtr QueryReferentImplementation(object obj, ref Guid uuid)
		{
			// by default we make QueryReferent
			return Xpcom.QueryReferent(obj, ref uuid);
		}
	}
}
