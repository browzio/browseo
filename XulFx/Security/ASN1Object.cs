using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.Security
{
	public sealed class ASN1Object : ComObject<nsIASN1Object>, IGeckoObjectWrapper
	{
		public static ASN1Object Create(nsIASN1Object instance)
		{
			return new ASN1Object(instance);
		}

		private ASN1Object(nsIASN1Object instance)
			: base(instance)
		{

		}


		public string DisplayName
		{
			get { return nsString.Get(Instance.GetDisplayNameAttribute); }
			set { nsString.Set(Instance.SetDisplayNameAttribute, value); }
		}

		public string DisplayValue
		{
			get { return nsString.Get(Instance.GetDisplayValueAttribute); }
			set { nsString.Set(Instance.SetDisplayValueAttribute, value); }
		}

		public uint Tag
		{
			get { return Instance.GetTagAttribute(); }
			set { Instance.SetTagAttribute(value); }
		}

		public uint Type
		{
			get { return Instance.GetTypeAttribute(); }
			set { Instance.SetTypeAttribute(value); }
		}
	}
}
