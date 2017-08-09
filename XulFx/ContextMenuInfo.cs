using System;
using Gecko.Interfaces;
using Gecko.DOM;
using Gecko.DOM.HTML;
using System.Runtime.InteropServices;

namespace Gecko.Windows
{
	public sealed class ContextMenuInfo : nsIContextMenuInfo
	{
		public static ContextMenuInfo Create(GeckoMouseEvent @event, GeckoElement element)
		{
			var result = new ContextMenuInfo();
			result.Event = @event;
			result.TargetElement = element;
			result.AssociatedLink = element.Closest("a") as GeckoHTMLAnchorElement;

			uint contextFlags = nsIContextMenuListenerConsts.CONTEXT_NONE;
			if (element is GeckoHTMLElement)
			{
				switch (element.TagName)
				{
					case "A":
						contextFlags = nsIContextMenuListenerConsts.CONTEXT_LINK;
						break;
					case "IMG":
						contextFlags = nsIContextMenuListenerConsts.CONTEXT_IMAGE;
						if (result.AssociatedLink != null)
							contextFlags |= nsIContextMenuListenerConsts.CONTEXT_LINK;
						break;
					case "INPUT":
						contextFlags = nsIContextMenuListenerConsts.CONTEXT_INPUT;
						var input = element as GeckoHTMLInputElement;
						if (input != null)
						{
							switch (input.Type)
							{
								case "image":
									contextFlags |= nsIContextMenuListenerConsts.CONTEXT_IMAGE;
									break;
								case "text":
								case "email":
								case "search":
								case "tel":
								case "url":
								case "date":
								case "time":
									contextFlags |= nsIContextMenuListenerConsts.CONTEXT_TEXT;
									break;
							}
						}
						break;
					case "TEXTAREA":
						contextFlags = nsIContextMenuListenerConsts.CONTEXT_TEXT;
						break;
					case "HTML":
						contextFlags = nsIContextMenuListenerConsts.CONTEXT_DOCUMENT;
						break;
				}
			}
			result.ContextFlags = contextFlags;
			return result;
		}

		private ContextMenuInfo()
		{

		}

		public GeckoMouseEvent Event { get; private set; }
		public GeckoElement TargetElement { get; private set; }
		public GeckoHTMLAnchorElement AssociatedLink { get; private set; }
		public uint ContextFlags { get; private set; }


		nsIDOMEvent nsIContextMenuInfo.GetMouseEventAttribute()
		{
			return Event.QueryInterface<nsIDOMEvent>();
		}

		nsIDOMNode nsIContextMenuInfo.GetTargetNodeAttribute()
		{
			return TargetElement.QueryInterface<nsIDOMNode>();
		}

		void nsIContextMenuInfo.GetAssociatedLinkAttribute(nsAStringBase result)
		{
			result.SetData(AssociatedLink != null ? AssociatedLink.Href : null);
		}

		imgIContainer nsIContextMenuInfo.GetImageContainerAttribute()
		{
			var content = TargetElement.QueryInterface<nsIImageLoadingContent>();
			if (content == null)
				Marshal.ThrowExceptionForHR(GeckoError.NS_ERROR_FAILURE);
			try
			{
				imgIRequest req = content.GetRequest(nsIImageLoadingContentConsts.CURRENT_REQUEST);
				if(req == null)
					Marshal.ThrowExceptionForHR(GeckoError.NS_ERROR_FAILURE);
				try
				{
					return req.GetImageAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref req);
				}
			}
			finally
			{
				Xpcom.FreeComObject(ref content);
			}
		}

		nsIURI nsIContextMenuInfo.GetImageSrcAttribute()
		{
			var content = TargetElement.QueryInterface<nsIImageLoadingContent>();
			if (content == null)
				Marshal.ThrowExceptionForHR(GeckoError.NS_ERROR_FAILURE);
			try
			{
				return content.GetCurrentURIAttribute();
			}
			finally
			{
				Xpcom.FreeComObject(ref content);
			}
		}

		imgIContainer nsIContextMenuInfo.GetBackgroundImageContainerAttribute()
		{
			throw new NotImplementedException();
		}

		nsIURI nsIContextMenuInfo.GetBackgroundImageSrcAttribute()
		{
			throw new NotImplementedException();
		}

	}
}
