using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoCSSStyleDeclaration : ComObject<nsIDOMCSSStyleDeclaration>, IGeckoObjectWrapper
	{
		public static GeckoCSSStyleDeclaration Create(nsIDOMCSSStyleDeclaration instance)
		{
			return new GeckoCSSStyleDeclaration(instance);
		}

		private GeckoCSSStyleDeclaration(nsIDOMCSSStyleDeclaration instance)
			: base(instance)
		{

		}


		/// <summary>
		/// Get and sets the CssText.
		/// For example: "background-color: green; color: red;"
		/// </summary>
		public string CssText
		{
			get { return nsString.Get(Instance.GetCssTextAttribute); }
			set { nsString.Set(Instance.SetCssTextAttribute, value); }
		}

		/// <summary>
		/// Get the number of CSS properties. 
		/// </summary>
		public uint Length
		{
			get { return Instance.GetLengthAttribute(); }
		}

		/// <summary>
		/// Get property name by index
		/// </summary>		
		public string this[int index]
		{
			get
			{
				using (var retval = new nsAString())
				{
					Instance.Item((uint)index, retval);
					return retval.ToString();
				}
			}
		}

		/// <summary>
		/// Get the value of a specfic Css Property.
		/// </summary>		
		public string GetPropertyValue(string propertyName)
		{
			using (nsAString retval = new nsAString(), aName = new nsAString(propertyName))
			{
				Instance.GetPropertyValue(aName, retval);
				return retval.ToString();
			}
		}

		/// <summary>
		/// Set the value of a specfic Css Property.
		/// </summary>		
		public void SetPropertyValue(string propertyName, string value)
		{
			using(nsAString aName = new nsAString(propertyName), aValue = new nsAString(value), aPriority = new nsAString())
			{
				Instance.SetProperty(aName, aValue, aPriority);
			}
		}

		/// <summary>
		/// Set the value of a specfic Css Property.
		/// </summary>          
		public void SetPropertyValue(string propertyName, string value, string priority)
		{
			using (nsAString aName = new nsAString(propertyName), aValue = new nsAString(value), aPriority = new nsAString(priority))
			{
				Instance.SetProperty(aName, aValue, aPriority);
			}
		}
	}
}
