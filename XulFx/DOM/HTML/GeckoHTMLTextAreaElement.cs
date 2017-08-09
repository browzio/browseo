using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLTextAreaElement : GeckoHTMLElement<nsIDOMHTMLTextAreaElement>
	{
		public static GeckoHTMLTextAreaElement Create(nsIDOMHTMLTextAreaElement element)
		{
			return new GeckoHTMLTextAreaElement(element);
		}

		private GeckoHTMLTextAreaElement(nsIDOMHTMLTextAreaElement element)
			: base(element)
		{

		}

		#region nsIDOMHTMLTextAreaElement members

		public bool Autofocus
		{
			get { return Instance.GetAutofocusAttribute(); }
			set { Instance.SetAutofocusAttribute(value); }
		}

		public uint Cols
		{
			get { return Instance.GetColsAttribute(); }
			set { Instance.SetColsAttribute(value); }
		}

		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
		}

		public GeckoHTMLFormElement Form
		{
			get { return Instance.GetFormAttribute().Wrap(GeckoHTMLFormElement.Create); }
		}

		public int MaxLength
		{
			get { return Instance.GetMaxLengthAttribute(); }
			set { Instance.SetMaxLengthAttribute(value); }
		}

		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		public string Placeholder
		{
			get { return nsString.Get(Instance.GetPlaceholderAttribute); }
			set { nsString.Set(Instance.SetPlaceholderAttribute, value); }
		}

		public bool ReadOnly
		{
			get { return Instance.GetReadOnlyAttribute(); }
			set { Instance.SetReadOnlyAttribute(value); }
		}

		public bool Required
		{
			get { return Instance.GetRequiredAttribute(); }
			set { Instance.SetRequiredAttribute(value); }
		}

		public uint Rows
		{
			get { return Instance.GetRowsAttribute(); }
			set { Instance.SetRowsAttribute(value); }
		}

		public string Wrap
		{
			get { return nsString.Get(Instance.GetWrapAttribute); }
			set { nsString.Set(Instance.SetWrapAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
		}

		public string DefaultValue
		{
			get { return nsString.Get(Instance.GetDefaultValueAttribute); }
			set { nsString.Set(Instance.SetDefaultValueAttribute, value); }
		}

		public string Value
		{
			get { return nsString.Get(Instance.GetValueAttribute); }
			set { nsString.Set(Instance.SetValueAttribute, value); }
		}

		public int TextLength
		{
			get { return Instance.GetTextLengthAttribute(); }
		}

		public bool WillValidate
		{
			get { return Instance.GetWillValidateAttribute(); }
		}

		public string ValidationMessage
		{
			get { return nsString.Get(Instance.GetValidationMessageAttribute); }
		}

		public bool CheckValidity()
		{
			return Instance.CheckValidity();
		}

		public void SetCustomValidity(string error)
		{
			nsString.Set(Instance.SetCustomValidity, error);
		}

		public void Select()
		{
			Instance.Select();
		}

		public int SelectionStart
		{
			get { return Instance.GetSelectionStartAttribute(); }
			set { Instance.SetSelectionStartAttribute(value); }
		}

		public int SelectionEnd
		{
			get { return Instance.GetSelectionEndAttribute(); }
			set { Instance.SetSelectionEndAttribute(value); }
		}

		public void SetSelectionRange(int selectionStart, int selectionEnd, string direction)
		{
			using (var aDirection = new nsAString(direction))
			{
				Instance.SetSelectionRange(selectionStart, selectionEnd, aDirection);
			}
		}

		public string SelectionDirection
		{
			get { return nsString.Get(Instance.GetSelectionDirectionAttribute); }
			set { nsString.Set(Instance.SetSelectionDirectionAttribute, value); }
		}

		#endregion
	}
}

