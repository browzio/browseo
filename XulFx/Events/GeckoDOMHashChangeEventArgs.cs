using Gecko.Interfaces;
using System;

namespace Gecko
{
	public sealed class GeckoDOMHashChangeEventArgs
		: GeckoDOMEventArgs
	{
		public static GeckoDOMHashChangeEventArgs Create(nsIDOMEvent instance)
		{
			return new GeckoDOMHashChangeEventArgs(instance);
		}

		private GeckoDOMHashChangeEventArgs(nsIDOMEvent instance)
			: base(instance)
		{

			
		}

		public Uri OldUrl
		{
			get { return new Uri(GetUri("newURL"), UriKind.RelativeOrAbsolute); }
		}

		public Uri NewUrl
		{
			get { return new Uri(GetUri("oldURL"), UriKind.RelativeOrAbsolute); }
		}

		private string GetUri(string propName)
		{
            return "about:home";
			//return Gecko.Javascript.GeckoJavascriptBridge.GetService().GetProperty(this.Instance, propName).ToString();
		}

	}
}
