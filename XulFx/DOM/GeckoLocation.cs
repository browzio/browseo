using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoLocation : ComObject<nsIDOMLocation>, IGeckoObjectWrapper
	{
		public static GeckoLocation Create(nsIDOMLocation instance)
		{
			return new GeckoLocation(instance);
		}

		private GeckoLocation(nsIDOMLocation instance)
			: base(instance)
		{

		}


		public string Hash
		{
			get { return nsString.Get(Instance.GetHashAttribute); }
			set { nsString.Set(Instance.SetHashAttribute, value); }
		}

		public string Host
		{
			get { return nsString.Get(Instance.GetHostAttribute); }
			set { nsString.Set(Instance.SetHostAttribute, value); }
		}

		public string Hostname
		{
			get { return nsString.Get(Instance.GetHostnameAttribute); }
			set { nsString.Set(Instance.GetHostnameAttribute, value); }
		}

		public string Href
		{
			get { return nsString.Get(Instance.GetHrefAttribute); }
			set { nsString.Set(Instance.SetHrefAttribute, value); }
		}

		public string Pathname
		{
			get { return nsString.Get(Instance.GetPathnameAttribute); }
			set { nsString.Set(Instance.SetPathnameAttribute, value); }
		}

		public string Port
		{
			get { return nsString.Get(Instance.GetPortAttribute); }
			set { nsString.Set(Instance.SetPortAttribute, value); }
		}

		public string Protocol
		{
			get { return nsString.Get(Instance.GetProtocolAttribute); }
			set { nsString.Set(Instance.SetProtocolAttribute, value); }
		}

		public string Search
		{
			get { return nsString.Get(Instance.GetSearchAttribute); }
			set { nsString.Set(Instance.SetSearchAttribute, value); }
		}

		public void Reload(bool forceget)
		{
			Instance.Reload(forceget);
		}


		public void Replace(string url)
		{
			nsString.Set(Instance.Replace, url);
		}

		public void Assign(string url)
		{
			nsString.Set(Instance.Assign, url);
		}

		public override string ToString()
		{
			return nsString.Get(Instance.ToString);
		}
	}
}
