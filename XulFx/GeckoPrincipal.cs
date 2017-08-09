using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko
{
	public sealed class GeckoPrincipal : ComObject<nsIPrincipal>, IGeckoObjectWrapper
	{
		public static GeckoPrincipal SystemPrincipal
		{
			get { return Xpcom.GetService<nsIPrincipal>(Contracts.SystemPrincipal).Wrap(GeckoPrincipal.Create); }
		}

		public static GeckoPrincipal NullPrincipal
		{
			get { return Xpcom.GetService<nsIPrincipal>(Contracts.NullPrincipal).Wrap(GeckoPrincipal.Create); }
		}

		public static GeckoPrincipal Create(nsIPrincipal instance)
		{
			return new GeckoPrincipal(instance);
		}

		private GeckoPrincipal(nsIPrincipal instance)
			: base(instance)
		{

		}
	}
}
