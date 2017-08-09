using Gecko.Interfaces;

namespace Gecko
{
	/// <summary>
	/// The Referrer Policy
	/// </summary>
	public enum ReferrerPolicy : uint
	{
		/// <summary>
		/// This is a user agent&apos;s default behavior. Same as the NoReferrerWhenDowngrade value.
		/// </summary>
		Default = nsIHttpChannelConsts.REFERRER_POLICY_DEFAULT,
		/// <summary>
		/// The No Referrer When Downgrade policy sends a full URL along with requests from
		/// TLS-protected settings object to a non-a priori insecure origin, and requests
		/// from settings objects which are not TLS-protected to any origin.
		/// Requests from TLS-protected settings objects to a priori insecure origins,
		/// on the other hand, will contain no referrer information. A Referer HTTP header
		/// will not be sent.
		/// </summary>
		NoReferrerWhenDowngrade = nsIHttpChannelConsts.REFERRER_POLICY_NO_REFERRER_WHEN_DOWNGRADE,
		/// <summary>
		/// The simplest policy is No Referrer, which specifies that no referrer information is
		/// to be sent along with requests made from a particular settings object to any origin.
		/// </summary>
		NoReferrer = nsIHttpChannelConsts.REFERRER_POLICY_NO_REFERRER,
		/// <summary>
		/// The Origin Only policy specifies that only the ASCII serialization of the origin
		/// of the settings object from which a request is made is sent as referrer information
		/// when making both same-origin requests and cross-origin requests from a particular
		/// settings object.
		/// </summary>
		Origin = nsIHttpChannelConsts.REFERRER_POLICY_ORIGIN,
		/// <summary>
		/// The Origin When Cross-Origin policy specifies that a full URL, stripped for use
		/// as a referrer, is sent as referrer information when making same-origin requests
		/// from a particular settings object, and only the ASCII serialization of the origin
		/// of the settings object from which a request is made is sent as referrer information
		/// when making cross-origin requests from a particular settings object.
		/// </summary>
		OriginWhenCrossOrigin = nsIHttpChannelConsts.REFERRER_POLICY_ORIGIN_WHEN_XORIGIN,
		/// <summary>
		/// The Unsafe URL policy specifies that a full URL, stripped for use as a referrer,
		/// is sent along with both cross-origin requests and same-origin requests made from
		/// a particular settings object.
		/// </summary>
		UnsafeUrl = nsIHttpChannelConsts.REFERRER_POLICY_UNSAFE_URL,

	}
}
