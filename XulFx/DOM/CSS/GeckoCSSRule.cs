using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public class GeckoCSSRule : ComObject<nsIDOMCSSRule>, IGeckoObjectWrapper
	{
		public static GeckoCSSRule Create(nsIDOMCSSRule instance)
		{
			// TODO: add wrappers

			return new GeckoCSSRule(instance);
		}

		protected GeckoCSSRule(nsIDOMCSSRule instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Gets this rule formatted as CSS text.
		/// </summary>
		public string CssText
		{
			get { return nsString.Get(Instance.GetCssTextAttribute); }
		}

		/// <summary>
		/// Gets the <see cref="GeckoCSSStyleSheet"/> which contains this rule.
		/// </summary>
		public GeckoCSSStyleSheet ParentStyleSheet
		{
			get { return Instance.GetParentStyleSheetAttribute().Wrap(GeckoCSSStyleSheet.Create); }
		}

		/// <summary>
		/// Gets the <see cref="GeckoCSSRuleType"/> of this rule.
		/// </summary>
		public GeckoCSSRuleType Type
		{
			get { return (GeckoCSSRuleType)Instance.GetTypeAttribute(); }
		}

		public override string ToString()
		{
			return this.CssText;
		}
	}


}
