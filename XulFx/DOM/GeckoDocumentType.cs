using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	public class GeckoDocumentType : GeckoNode<nsIDOMDocumentType>
	{
		public static GeckoDocumentType Create(nsIDOMDocumentType documentType)
		{
			return new GeckoDocumentType(documentType);
		}

		protected GeckoDocumentType(nsIDOMDocumentType documentType)
			: base(documentType)
		{

		}

		/// <summary>
		/// Each Document has a doctype attribute whose value is either null
		/// or a DocumentType object.
		/// The nsIDOMDocumentType interface in the DOM Core provides an
		/// interface to the list of entities that are defined for the document.
		///
		/// For more information on this interface please see
		/// http://www.w3.org/TR/DOM-Level-2-Core/
		/// </summary>
		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
		}

		public string PublicID
		{
			get { return nsString.Get(Instance.GetPublicIdAttribute); }
		}

		public string SystemID
		{
			get { return nsString.Get(Instance.GetSystemIdAttribute); }
		}

		public string InternalSubset
		{
			get { return nsString.Get(Instance.GetInternalSubsetAttribute); }
		}
	}
}
