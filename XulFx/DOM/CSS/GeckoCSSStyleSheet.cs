using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko.DOM
{
	public sealed class GeckoCSSStyleSheet : GeckoStyleSheet
	{
		private new nsIDOMCSSStyleSheet _instance;

		public static GeckoCSSStyleSheet Create(nsIDOMCSSStyleSheet instance)
		{
			return new GeckoCSSStyleSheet(instance);
		}

		private GeckoCSSStyleSheet(nsIDOMCSSStyleSheet instance)
			: base(instance)
		{

		}

		protected override void Dispose(bool disposing)
		{
			_instance = null;
			base.Dispose(disposing);
		}



		public new nsIDOMCSSStyleSheet Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _instance;
			}
		}

		/// <summary>
		/// Gets the <see cref="GeckoStyleRule"/> which imported this style sheet.
		/// </summary>
		public GeckoCSSRule OwnerRule
		{
			get { return Instance.GetOwnerRuleAttribute().Wrap(GeckoCSSRule.Create); }
		}

		/// <summary>
		/// Gets the collection of rules in the style sheet.
		/// </summary>
		public GeckoCSSRuleList CssRules
		{
			get { return Instance.GetCssRulesAttribute().Wrap(GeckoCSSRuleList.Create); }
		}
	}
}
