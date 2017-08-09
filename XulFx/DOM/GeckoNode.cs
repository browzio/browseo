using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Gecko.DOM;
using Gecko.Interop;
using Gecko.Interfaces;
using Gecko.DOM.Abstractions;

namespace Gecko
{
	/// <summary>
	/// Provides a base class for DOM nodes.
	/// </summary>
	public class GeckoNode : ComObject<nsIDOMNode>, IGeckoObjectWrapper
	{
		public static GeckoNode Create(nsIDOMNode node)
		{
			//bool found = true;
			GeckoNode wrapper;
			try
			{
				if (TryCast<nsIDOMElement, GeckoElement>(node, GeckoElement.Create, out wrapper))
					return wrapper;

				if (TryCast<nsIDOMAttr, GeckoAttribute>(node, GeckoAttribute.Create, out wrapper))
					return wrapper;

				if (TryCast<nsIDOMCharacterData, GeckoNode>(node, GeckoCharacterData<nsIDOMCharacterData>.Create, out wrapper))
					return wrapper;

				if (TryCast<nsIDOMDocument, GeckoDocument>(node, GeckoDocument.Create, out wrapper))
					return wrapper;

				if (TryCast<nsIDOMDocumentFragment, GeckoDocumentFragment>(node, GeckoDocumentFragment.Create, out wrapper))
					return wrapper;

				if (TryCast<nsIDOMDocumentType, GeckoDocumentType>(node, GeckoDocumentType.Create, out wrapper))
					return wrapper;

				//found = false;
			}
			finally
			{
				// if(found) Xpcom.FreeComObject(ref node);
			}
			return new GeckoNode(node);
		}

		private static bool TryCast<TInterface, TWrapper>(nsIDOMNode instance, Func<TInterface, TWrapper> create, out GeckoNode wrapper)
			where TInterface : class
			where TWrapper : GeckoNode
		{
			var obj = Xpcom.QueryInterface<TInterface>(instance);
			if (obj != null)
			{
				wrapper = create(obj);
				return true;
			}
			wrapper = null;
			return false;
		}

		protected GeckoNode(nsIDOMNode instance)
			: base(instance)
		{
			
		}

		/// <summary>
		/// Gets the text contents of the node.
		/// </summary>
		public string TextContent
		{
			get { return nsString.Get(Instance.GetTextContentAttribute); }
			set { nsString.Set(Instance.SetTextContentAttribute, value); }
		}

		/// <summary>
		/// Gets or sets the value of the node.
		/// </summary>
		public string NodeValue
		{
			get { return nsString.Get(Instance.GetNodeValueAttribute); }
			set { nsString.Set(Instance.SetNodeValueAttribute, value); }
		}

		public string NodeName
		{
			get { return nsString.Get(Instance.GetNodeNameAttribute); }
		}

		/// <summary>
		/// Gets a collection containing all child nodes of this node.
		/// </summary>
		public GeckoNodeCollection ChildNodes
		{
			get { return Instance.GetChildNodesAttribute().Wrap(GeckoNodeCollection.Create); }
		}

		public GeckoNode FirstChild { get { return Instance.GetFirstChildAttribute().Wrap(GeckoNode.Create); } }
		public GeckoNode LastChild { get { return Instance.GetLastChildAttribute().Wrap(GeckoNode.Create); } }
		public GeckoNode NextSibling { get { return Instance.GetNextSiblingAttribute().Wrap(GeckoNode.Create); } }
		public GeckoNode PreviousSibling { get { return Instance.GetPreviousSiblingAttribute().Wrap(GeckoNode.Create); } }
		public bool HasChildNodes { get { return Instance.HasChildNodes(); } }

		public GeckoDocument OwnerDocument
		{
			get { return Instance.GetOwnerDocumentAttribute().Wrap(GeckoDocument.Create); }
		}

		public GeckoNode AppendChild(GeckoNode node)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			Instance.AppendChild(node.Instance);
			return node;
		}

		public GeckoNode CloneNode(bool deep)
		{
			return Instance.CloneNode(deep, 1).Wrap(GeckoNode.Create);
		}

		public GeckoNode InsertBefore(GeckoNode newChild, GeckoNode before)
		{
			if (newChild == null)
				throw new ArgumentNullException("newChild");
			if (before == null)
				throw new ArgumentNullException("before");

			Instance.InsertBefore(newChild.Instance, before.Instance);
			return newChild;
		}

		public GeckoNode RemoveChild(GeckoNode node)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			Instance.RemoveChild(node.Instance);
			return node;
		}

		public GeckoNode ReplaceChild(GeckoNode newChild, GeckoNode oldChild)
		{
			if (newChild == null)
				throw new ArgumentNullException("newChild");
			if (oldChild == null)
				throw new ArgumentNullException("oldChild");

			Instance.ReplaceChild(newChild.Instance, oldChild.Instance);
			return newChild;
		}

		public string NamespaceURI
		{
			get { return nsString.Get(Instance.GetNamespaceURIAttribute); }
		}

		public string Prefix
		{
			get { return nsString.Get(Instance.GetPrefixAttribute); }
		}

		public string LocalName
		{
			get { return nsString.Get(Instance.GetLocalNameAttribute); }
		}

		private NodeType m_cachedType;

		public NodeType NodeType
		{
			get
			{
				if (m_cachedType != 0)
					return m_cachedType;

				return m_cachedType = (NodeType)Instance.GetNodeTypeAttribute();
			}
		}

		public GeckoNode ParentNode
		{
			get { return Instance.GetParentNodeAttribute().Wrap(GeckoNode.Create); }
		}

		/// <summary>
		/// Returns a value indicating whether a node is a descendant of a given node or not.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool Contains(GeckoNode node)
		{
			if (node == null)
				throw new ArgumentNullException("node");
		
			return Instance.Contains(node.Instance);
		}

		/// <summary>
		/// Compares the position of the current node against another node in any other document.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public int CompareDocumentPosition(GeckoNode node)
		{
			if (node == null)
				throw new ArgumentNullException("node");

			return Instance.CompareDocumentPosition(node.Instance);
		}

		public bool DispatchEvent(GeckoEvent @event)
		{
			GeckoDOMEventTarget eventTarget = Xpcom.QueryInterface<nsIDOMEventTarget>(Instance).Wrap(GeckoDOMEventTarget.Create);
			if (eventTarget == null)
				throw new NotSupportedException();
			return eventTarget.DispatchEvent(@event);
		}
	}
}
