using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko.Net
{
	public sealed class Cookie : ComObject<nsICookie2>, IGeckoObjectWrapper
	{
		public static Cookie Create(nsICookie2 instance)
		{
			return new Cookie(instance);
		}

		private Cookie(nsICookie2 instance)
			: base(instance)
		{

		}

		public DateTime CreationTime
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetCreationTimeAttribute() / 1000000.0); }
		}

		/// <summary>
		/// Gets the expiration date and time for the Cookie as a DateTime.
		/// </summary>
		public DateTime Expiry
		{
			get
			{
				if (Instance.GetIsSessionAttribute())
					return DateTime.MinValue;

				long expiry = Instance.GetExpiryAttribute();
				return expiry >= 0 ? Utils.FromSecondsSinceEpoch(expiry) : DateTime.MinValue;
			}
		}

		public string Host
		{
			get { return nsString.Get(Instance.GetHostAttribute); }
		}

		public bool IsDomain
		{
			get { return Instance.GetIsDomainAttribute(); }
		}

		public bool IsHttpOnly
		{
			get { return Instance.GetIsHttpOnlyAttribute(); }
		}

		public bool IsSecure
		{
			get { return Instance.GetIsSecureAttribute(); }
		}

		public bool IsSession
		{
			get { return Instance.GetIsSessionAttribute(); }
		}

		public DateTime LastAccessed
		{
			get { return Utils.FromSecondsSinceEpoch(Instance.GetLastAccessedAttribute() / 1000000.0); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
		}

		public string Path
		{
			get { return nsString.Get(Instance.GetPathAttribute); }
		}

		public string RawHost
		{
			get { return nsString.Get(Instance.GetRawHostAttribute); }
		}

		public string Value
		{
			get { return nsString.Get(Instance.GetValueAttribute); }
		}

		public int GetExpiryAsUnixTime()
		{
			if (!Instance.GetIsSessionAttribute())
			{
				long expiry = Instance.GetExpiryAttribute();
				if (expiry >= 0)
					return (int)expiry;
			}
			return 0;
		}

	}
}
