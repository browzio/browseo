using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko.DOM
{
	/// <summary>
	/// Represents the result of the evaluation of an XPath 1.0 expression within the context of a particular node.
	/// Since evaluation of an XPath expression can result in various result types, this object makes it possible
	/// to discover and manipulate the type and value of the result.
	/// </summary>
	public sealed class GeckoXPathResult: ComObject<nsIXulfxDOMXPathResult>
	{
		private bool _isIterated;
		private GeckoXPathResultType _cachedResultType;
		private IEnumerable<GeckoNode> _enumerable;

		/// <summary>
		/// Creates a GeckoXPathResult wrapper 
		/// </summary>
		/// <param name="instance"></param>
		internal GeckoXPathResult(nsIXulfxDOMXPathResult instance)
			: base(instance)
		{
			_cachedResultType = GeckoXPathResultType.Any;
		}

		/// <summary>
		/// A value representing the type of this result.
		/// </summary>
		public GeckoXPathResultType ResultType
		{
			get
			{
				if (_cachedResultType == GeckoXPathResultType.Any)
				{
					_cachedResultType = (GeckoXPathResultType)Instance.GetResultTypeAttribute();
				}
				return _cachedResultType;
			}
		}

		/// <summary>
		/// The value of this number result. If the native double type of the DOM binding does not directly
		/// support the exact IEEE 754 result of the XPath expression, then it is up to the definition of
		/// the binding to specify how the XPath number is converted to the native binding number.
		/// </summary>
		public double NumberValue
		{
			get
			{
				if (this.ResultType != GeckoXPathResultType.Number)
					throw new InvalidCastException();

				return Instance.GetNumberValueAttribute();
			}
		}

		/// <summary>
		/// The value of this string result.
		/// </summary>
		public string StringValue
		{
			get
			{
				if (this.ResultType != GeckoXPathResultType.String)
					throw new InvalidCastException();

				using(var s = new nsAString())
				{
					Instance.GetStringValueAttribute(s);
					return s.ToString();
				}
			}
		}

		/// <summary>
		/// The value of this boolean result.
		/// </summary>
		public bool BooleanValue
		{
			get
			{
				if (this.ResultType != GeckoXPathResultType.Boolean)
					throw new InvalidCastException();

				return Instance.GetBooleanValueAttribute();
			}
		}

		/// <summary>
		/// The value of this single node result, which may be null.
		/// </summary>
		public GeckoNode SingleNodeValue
		{
			get
			{
				GeckoXPathResultType resType = this.ResultType;
				if (resType != GeckoXPathResultType.AnyUnorderedNode
					&& resType != GeckoXPathResultType.FirstOrderedNode)
				{
					throw new InvalidCastException();
				}
				return Instance.GetSingleNodeValueAttribute().Wrap(GeckoNode.Create);
			}
		}

		/// <summary>
		/// Signifies that the iterator has become invalid.
		/// </summary>
		public bool InvalidIteratorState
		{
			get { return Instance.GetInvalidIteratorStateAttribute(); }
		}

		/// <summary>
		/// The number of nodes in the result snapshot.
		/// </summary>
		public int SnapshotLength
		{
			get 
			{
				GeckoXPathResultType resType = this.ResultType;
				if (resType != GeckoXPathResultType.OrderedNodeSnapshot
					&& resType != GeckoXPathResultType.UnorderedNodeSnapshot)
				{
					throw new InvalidCastException();
				}

				return (int)Instance.GetSnapshotLengthAttribute();
			}
		}

		/// <summary>
		/// Iterates and returns the next node from the node set or nullif there are no more nodes.
		/// </summary>
		/// <returns>Returns the next node.</returns>
		public GeckoNode IterateNext()
		{
			_isIterated = true;
			return Instance.IterateNext().Wrap(GeckoNode.Create);
		}

		/// <summary>
		/// Returns the indexth item in the snapshot collection. If index is greater than or equal
		/// to the number of nodes in the list, this method returns null. Unlike the iterator result,
		/// the snapshot does not become invalid, but may not correspond to the current document if it is mutated.
		/// </summary>
		/// <param name="index">Index into the snapshot collection.</param>
		/// <returns>The node at the indexth position in the NodeList, or null if that is not a valid index.</returns>
		public GeckoNode SnapshotItem(uint index)
		{
			return Instance.SnapshotItem(index).Wrap(GeckoNode.Create);
		}
		
		/// <summary>
		/// Converts GeckoXPathResult to IEnumerable&lt;GeckoNode&gt;.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<GeckoNode> AsEnumerable()
		{
			if (_enumerable != null)
				return _enumerable;

			if (_isIterated || Instance.GetInvalidIteratorStateAttribute())
				throw new InvalidOperationException();

			GeckoXPathResultType resType = this.ResultType;
			if (resType != GeckoXPathResultType.UnorderedNodeIterator && resType != GeckoXPathResultType.OrderedNodeIterator)
				throw new InvalidOperationException();

			_isIterated = true;

			GeckoNode node;
			var nodes = new List<GeckoNode>();
			while((node = Instance.IterateNext().Wrap(GeckoNode.Create)) != null)
			{
				nodes.Add(node);
			}
			_enumerable = nodes.ToArray();
			return _enumerable;
		}
	}

	public enum GeckoXPathResultType : ushort
	{
		Any = nsIDOMXPathResultConsts.ANY_TYPE,
		Number = nsIDOMXPathResultConsts.NUMBER_TYPE,
		String = nsIDOMXPathResultConsts.STRING_TYPE,
		Boolean = nsIDOMXPathResultConsts.BOOLEAN_TYPE,
		UnorderedNodeIterator = nsIDOMXPathResultConsts.UNORDERED_NODE_ITERATOR_TYPE,
		OrderedNodeIterator = nsIDOMXPathResultConsts.ORDERED_NODE_ITERATOR_TYPE,
		UnorderedNodeSnapshot = nsIDOMXPathResultConsts.UNORDERED_NODE_SNAPSHOT_TYPE,
		OrderedNodeSnapshot = nsIDOMXPathResultConsts.ORDERED_NODE_SNAPSHOT_TYPE,
		AnyUnorderedNode = nsIDOMXPathResultConsts.ANY_UNORDERED_NODE_TYPE,
		FirstOrderedNode = nsIDOMXPathResultConsts.FIRST_ORDERED_NODE_TYPE,

	}
}
