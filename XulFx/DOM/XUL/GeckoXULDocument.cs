using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.DOM.Abstractions;

namespace Gecko.DOM.XUL
{
	public sealed class GeckoXULDocument : GeckoDocument<nsIDOMXULDocument>
	{
		public static GeckoXULDocument Create(nsIDOMXULDocument document)
		{
			return new GeckoXULDocument(document);
		}

		private GeckoXULDocument(nsIDOMXULDocument document)
			: base(document)
		{

		}

		public new GeckoXULElement DocumentElement
		{
			get
			{
				return (GeckoXULElement)Instance.GetDocumentElementAttribute().Wrap(GeckoElement.Create);
			}
		}

		public GeckoNode PopupNode
		{
			get { return Instance.GetPopupNodeAttribute().Wrap(GeckoNode.Create); }
			set { Instance.SetPopupNodeAttribute(value != null ? value.Instance : null); }
		}
	}
}
