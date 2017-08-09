using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	/// <summary>
	/// Represents a DOM attribute.
	/// </summary>
	public class GeckoAttribute : GeckoNode<nsIDOMAttr>
	{
		internal static GeckoAttribute Create(nsIDOMAttr instance)
		{
			return new GeckoAttribute(instance);
		}

		public GeckoAttribute(nsIDOMAttr instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Gets the name of the attribute.
		/// </summary>
		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
		}

		/// <summary>
		/// Gets the <see cref="GeckoElement"/> which contains this attribute.
		/// </summary>
		public GeckoElement OwnerElement
		{
			get { return Instance.GetOwnerElementAttribute().Wrap(GeckoElement.Create); }
		}

		/// <summary>
		/// Gets a value indicating whether the attribute is specified.
		/// </summary>
		public bool Specified
		{
			get { return Instance.GetSpecifiedAttribute(); }
		}

		/// <summary>
		/// Gets the value of the attribute.
		/// </summary>
		public string Value
		{
			get { return nsString.Get(Instance.GetValueAttribute); }
			set { nsString.Set(Instance.SetValueAttribute, value); }
		}
	}
}
