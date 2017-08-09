using System;
using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLSelectElement : GeckoHTMLElement<nsIDOMHTMLSelectElement>
	{
		public static GeckoHTMLSelectElement Create(nsIDOMHTMLSelectElement element)
		{
			return new GeckoHTMLSelectElement(element);
		}

		private GeckoHTMLSelectElement(nsIDOMHTMLSelectElement element)
			: base(element)
		{

		}


		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
		}

		public int SelectedIndex
		{
			get { return Instance.GetSelectedIndexAttribute(); }
			set { Instance.SetSelectedIndexAttribute(value); }
		}

		public string Value
		{
			get { return nsString.Get(Instance.GetValueAttribute); }
			set { nsString.Set(Instance.SetValueAttribute, value); }
		}

		public uint Length
		{
			get { return Instance.GetLengthAttribute(); }
			set { Instance.SetLengthAttribute(value); }
		}

		public GeckoHTMLFormElement Form
		{
			get
			{
				return Instance.GetFormAttribute().Wrap(GeckoHTMLFormElement.Create);
			}
		}

		public GeckoHTMLOptionsCollection Options
		{
			get
			{
				return Instance.GetOptionsAttribute().Wrap(GeckoHTMLOptionsCollection.Create);
			}
		}

		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public bool Multiple
		{
			get { return Instance.GetMultipleAttribute(); }
			set { Instance.SetMultipleAttribute(value); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public uint Size
		{
			get { return Instance.GetSizeAttribute(); }
			set { Instance.SetSizeAttribute(value); }
		}

		public void Add(GeckoHTMLElement element, GeckoHTMLElement before)
		{
			nsIVariant beforeObj = null;
			beforeObj = Xpcom.QueryInterface<nsIVariant>(before.Instance);
			if (beforeObj == null)
				throw new ArgumentOutOfRangeException("before");
			try
			{
				Instance.Add(element.Instance, beforeObj);
			}
			finally
			{
				Xpcom.FreeComObject(ref beforeObj);
			}
		}

		public void Remove(int index)
		{
			Instance.Remove(index);
		}

	}
}
