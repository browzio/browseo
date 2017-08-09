using Gecko.Interfaces;
using Gecko.Interop;
using System;


namespace Gecko.DOM
{
	/// <summary>
	/// The GeckoTreeWalker object represents the nodes of a document subtree and a position within them.
	/// </summary>
	public sealed class GeckoTreeWalker : ComObject<nsIDOMTreeWalker>, IGeckoObjectWrapper
	{
		/// <summary>
		/// Creates a wrapper of TreeWalker
		/// </summary>
		/// <param name="instance">Instance of TreeWalker</param>
		/// <returns></returns>
		public static GeckoTreeWalker Create(nsIDOMTreeWalker instance)
		{
			return new GeckoTreeWalker(instance);
		}

		private GeckoTreeWalker(nsIDOMTreeWalker instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Gets a Node representing the root node as specified when the TreeWalker was created.
		/// </summary>
		public GeckoNode Root
		{
			get { return Instance.GetRootAttribute().Wrap(GeckoNode.Create); }
		}

		/// <summary>
		/// Gets a value being a bitmask made of constants describing the types of Node that must to be presented. Non-matching nodes are skipped, but their children may be included, if relevant.
		/// </summary>
		public NodeFilterFlags WhatToShow
		{
			get { return (NodeFilterFlags)Instance.GetWhatToShowAttribute(); }
		}

		/// <summary>
		/// Returns a NodeFilter used to select the relevant nodes.
		/// </summary>
		public nsIDOMNodeFilter GetFilter()
		{
			return Instance.GetFilterAttribute();
		}

		/// <summary>
		/// Gets and sets the Node on which the GeckoTreeWalker is currently pointing at.
		/// </summary>
		public GeckoNode CurrentNode
		{
			get
			{
				return Instance.GetCurrentNodeAttribute().Wrap(GeckoNode.Create);
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				Instance.SetCurrentNodeAttribute(value.Instance);
			}
		}

		/// <summary>
		/// Moves the current Node to the first visible ancestor node in the document order, and returns the found node. It also moves the current node to this one.
		/// </summary>
		/// <returns>If no such node exists, or if it is before that the root node defined at the object construction, returns null and the current node is not changed.</returns>
		public GeckoNode MoveToParentNode()
		{
			return Instance.ParentNode().Wrap(GeckoNode.Create);
		}

		/// <summary>
		/// Moves the current Node to the first visible child of the current node, and returns the found child. It also moves the current node to this child.
		/// </summary>
		/// <returns>If no such child exists, returns null and the current node is not changed.</returns>
		public GeckoNode MoveToFirstChild()
		{
			return Instance.FirstChild().Wrap(GeckoNode.Create);
		}

		/// <summary>
		/// Moves the current Node to the last visible child of the current node, and returns the found child. It also moves the current node to this child.
		/// </summary>
		/// <returns>If no such child exists, returns null and the current node is not changed.</returns>
		public GeckoNode MoveToLastChild()
		{
			return Instance.LastChild().Wrap(GeckoNode.Create);
		}

		/// <summary>
		/// Moves the current Node to its previous sibling, if any, and returns the found sibling.
		/// </summary>
		/// <returns>If no such child exists, returns null and the current node is not changed.</returns>
		public GeckoNode MoveToPreviousSibling()
		{
			return Instance.PreviousSibling().Wrap(GeckoNode.Create);
		}

		/// <summary>
		/// Moves the current Node to its next sibling, if any, and returns the found sibling.
		/// </summary>
		/// <returns>If no such child exists, returns null and the current node is not changed.</returns>
		public GeckoNode MoveToNextSibling()
		{
			return Instance.NextSibling().Wrap(GeckoNode.Create);
		}

		/// <summary>
		/// Moves the current Node to the previous visible node in the document order, and returns the found node. It also moves the current node to this one.
		/// </summary>
		/// <returns>If no such node exists,or if it is before that the root node defined at the object construction, returns null and the current node is not changed.</returns>
		public GeckoNode MoveToPreviousNode()
		{
			return Instance.PreviousSibling().Wrap(GeckoNode.Create);
		}

		/// <summary>
		/// Moves the current Node to the next visible node in the document order, and returns the found node. It also moves the current node to this one.
		/// </summary>
		/// <returns>If no such child exists, returns null and the current node is not changed.</returns>
		public GeckoNode MoveToNextNode()
		{
			return Instance.NextNode().Wrap(GeckoNode.Create);
		}

	}
}
