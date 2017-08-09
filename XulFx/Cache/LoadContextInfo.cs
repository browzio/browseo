using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.Cache
{
	/// <summary>
	/// Helper class to carry informatin about the load context
	/// encapsulating an AppID, IsInBrowser and IsPrivite properties.
	/// </summary>
	public sealed class LoadContextInfo : ComObject<nsILoadContextInfo>, IGeckoObjectWrapper
	{
		public static LoadContextInfo Create(nsILoadContextInfo instance)
		{
			return new LoadContextInfo(instance);
		}

		private LoadContextInfo(nsILoadContextInfo instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Whether the context is in a Private Browsing mode
		/// </summary>
		public bool IsPrivate
		{
			get { return Instance.GetIsPrivateAttribute(); }
		}

		/// <summary>
		/// Whether the load is initiated as anonymous
		/// </summary>
		public bool IsAnonymous
		{
			get { return Instance.GetIsAnonymousAttribute(); }
		}

	}
}
