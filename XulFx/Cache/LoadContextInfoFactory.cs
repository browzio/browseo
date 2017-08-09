using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.Cache
{
	public sealed class LoadContextInfoFactory : ComObject<nsILoadContextInfoFactory>, IGeckoObjectWrapper
	{
		public static LoadContextInfoFactory GetService()
		{
			return Xpcom.GetService<nsILoadContextInfoFactory>(Contracts.LoadContentInfoFactory).Wrap(LoadContextInfoFactory.Create);
		}

		private static LoadContextInfoFactory Create(nsILoadContextInfoFactory instance)
		{
			return new LoadContextInfoFactory(instance);
		}

		private LoadContextInfoFactory(nsILoadContextInfoFactory instance)
			: base(instance)
		{

		}

		public LoadContextInfo Default
		{
			get
			{
				return Instance.GetDefaultAttribute().Wrap(LoadContextInfo.Create);
			}
		}

		public LoadContextInfo Private
		{
			get
			{
				return Instance.GetPrivateAttribute().Wrap(LoadContextInfo.Create);
			}
		}

		public LoadContextInfo Anonymous
		{
			get
			{
				return Instance.GetAnonymousAttribute().Wrap(LoadContextInfo.Create);
			}
		}

		public LoadContextInfo FromWindow(GeckoWindow window, bool anonymous)
		{
			return Instance.FromWindow(window.Instance, anonymous).Wrap(LoadContextInfo.Create);
		}

	}
}
