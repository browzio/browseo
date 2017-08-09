using System;
using Gecko.Interfaces;

namespace Gecko.DOM
{
	public enum NodeFilterFlags : uint
	{
		All = nsIDOMNodeFilterConsts.SHOW_ALL,
		Attribute = nsIDOMNodeFilterConsts.SHOW_ATTRIBUTE,
		CDATASection = nsIDOMNodeFilterConsts.SHOW_CDATA_SECTION,
		Comment = nsIDOMNodeFilterConsts.SHOW_COMMENT,
		Document = nsIDOMNodeFilterConsts.SHOW_DOCUMENT,
		DocumentFragment = nsIDOMNodeFilterConsts.SHOW_DOCUMENT_FRAGMENT,
		DocumentType = nsIDOMNodeFilterConsts.SHOW_DOCUMENT_TYPE,
		Element = nsIDOMNodeFilterConsts.SHOW_ELEMENT,
		Entity = nsIDOMNodeFilterConsts.SHOW_ENTITY,
		EntityReference = nsIDOMNodeFilterConsts.SHOW_ENTITY_REFERENCE,
		Notation = nsIDOMNodeFilterConsts.SHOW_NOTATION,
		ProcessingInstruction = nsIDOMNodeFilterConsts.SHOW_PROCESSING_INSTRUCTION,
		Text = nsIDOMNodeFilterConsts.SHOW_TEXT,

	}
}
