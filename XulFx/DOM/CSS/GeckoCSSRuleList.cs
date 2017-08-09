using Gecko.Interfaces;
using Gecko.Interop;
using System.Collections.Generic;

namespace Gecko.DOM
{
	public sealed class GeckoCSSRuleList : ComObject<nsIDOMCSSRuleList>, IEnumerable<GeckoCSSRule>, IGeckoObjectWrapper
	{
		public static GeckoCSSRuleList Create(nsIDOMCSSRuleList instance)
		{
			return new GeckoCSSRuleList(instance);
		}

		private GeckoCSSRuleList(nsIDOMCSSRuleList instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Gets the number of items in the collection.
		/// </summary>
		public int Count
		{
			get { return (int)Instance.GetLengthAttribute(); }
		}


		#region IEnumerable<GeckoCSSRule>

		/// <summary>
		/// Returns an IEnumerator which can enumerate through the rules in the collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<GeckoCSSRule> GetEnumerator()
		{
			int length = Count;
			for (uint i = 0; i < length; i++)
			{
				yield return Instance.Item(i).Wrap(GeckoCSSRule.Create);
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}
