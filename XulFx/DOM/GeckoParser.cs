using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;
using Gecko.Interfaces;

namespace Gecko.DOM
{
	public sealed class GeckoParser : ComObject<nsIDOMParser>, IGeckoObjectWrapper
	{
		public static GeckoParser Create()
		{
			return new GeckoParser(Xpcom.CreateInstance<nsIDOMParser>(Contracts.DOMParser));
		}

		public static GeckoParser Create(nsIDOMParser instance)
		{
			return new GeckoParser(instance);
		}

		private GeckoParser(nsIDOMParser instance)
			: base(instance)
		{

		}



		/// <summary>
		/// It is working!
		/// </summary>
		/// <param name="str"></param>
		/// <param name="contentType"></param>
		/// <returns></returns>
		public GeckoDocument Parse(string content, string contentType = "text/html")
		{
			return Instance.ParseFromString(content, contentType).Wrap(GeckoDocument.Create);
		}

		///// <summary>
		///// throws not implemented exception
		///// </summary>
		///// <param name="buffer"></param>
		///// <param name="count"></param>
		///// <param name="contentType"></param>
		///// <returns></returns>
		//public GeckoDomDocument ParseFromBuffer(byte[] buffer, int count, string contentType = "text/html")
		//{
		//	return _domParser.Instance.ParseFromBuffer(buffer, (uint)count, contentType).Wrap(GeckoDomDocument.CreateDomDocumentWraper);
		//}

		///// <summary>
		///// throws not implemented exception
		///// </summary>
		///// <param name="buffer"></param>
		///// <param name="contentType"></param>
		///// <returns></returns>
		//public GeckoDomDocument ParseFromBuffer(byte[] buffer, string contentType = "text/html")
		//{
		//	return _domParser.Instance.ParseFromBuffer(buffer, (uint)buffer.Length, contentType)
		//		.Wrap(GeckoDomDocument.CreateDomDocumentWraper);
		//}

		// not needed in .NET
		//public GeckoDomDocument ParseFromStream(ByteArrayInputStream buffer, string contentType)
		//{
		//    return _domParser.Instance.ParseFromStream()
		//}
	}
}