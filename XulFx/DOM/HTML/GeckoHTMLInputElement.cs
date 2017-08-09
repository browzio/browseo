using System;
using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.CustomMarshalers;
using System.Collections.Generic;
using System.IO;

namespace Gecko.DOM.HTML
{
	public sealed class GeckoHTMLInputElement : GeckoHTMLElement<nsIDOMHTMLInputElement>
	{
		public static GeckoHTMLInputElement Create(nsIDOMHTMLInputElement element)
		{
			return new GeckoHTMLInputElement(element);
		}

		private GeckoHTMLInputElement(nsIDOMHTMLInputElement element)
			: base(element)
		{

		}

		public string DefaultValue
		{
			get { return nsString.Get(Instance.GetDefaultValueAttribute); }
			set { nsString.Set(Instance.SetDefaultValueAttribute, value); }
		}

		public bool DefaultChecked
		{
			get { return Instance.GetDefaultCheckedAttribute(); }
			set { Instance.SetDefaultCheckedAttribute(value); }
		}

		public GeckoHTMLFormElement Form
		{
			get { return Instance.GetFormAttribute().Wrap(GeckoHTMLFormElement.Create); }
		}

		public string Accept
		{
			get { return nsString.Get(Instance.GetAcceptAttribute); }
			set { nsString.Set(Instance.SetAcceptAttribute, value); }
		}

		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		public string Alt
		{
			get { return nsString.Get(Instance.GetAltAttribute); }
			set { nsString.Set(Instance.SetAltAttribute, value); }
		}

		public bool Checked
		{
			get { return Instance.GetCheckedAttribute(); }
			set { Instance.SetCheckedAttribute(value); }
		}

		public bool Disabled
		{
			get { return Instance.GetDisabledAttribute(); }
			set { Instance.SetDisabledAttribute(value); }
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

		public bool ReadOnly
		{
			get { return Instance.GetReadOnlyAttribute(); }
			set { Instance.SetReadOnlyAttribute(value); }
		}

		public int Size
		{
			get { return (int)Instance.GetSizeAttribute(); }
			set { Instance.SetSizeAttribute((uint)value); }
		}

		public string Src
		{
			get { return nsString.Get(Instance.GetSrcAttribute); }
			set { nsString.Set(Instance.SetSrcAttribute, value); }
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
			set { nsString.Set(Instance.SetTypeAttribute, value); }
		}

		public string UseMap
		{
			get { return nsString.Get(Instance.GetUseMapAttribute); }
			set { nsString.Set(Instance.SetUseMapAttribute, value); }
		}

		public string Value
		{
			get { return nsString.Get(Instance.GetValueAttribute); }
			set { nsString.Set(Instance.SetValueAttribute, value); }
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

		public int TextLength
		{
			get { return Instance.GetTextLengthAttribute(); }
		}

		public void Select()
		{
			Instance.Select();
		}

		/// <summary>
		/// Sets the names of the files that selected on an HTML input element.
		/// </summary>
		/// <param name="fileNames">The array of file names to apply to the element.</param>
		public void SetFiles(string[] fileNames)
		{
			if (!"file".Equals(this.Type, StringComparison.OrdinalIgnoreCase))
				throw new InvalidOperationException();

			if (fileNames == null)
				fileNames = new string[0];

			var files = new List<IntPtr>(fileNames.Length);
			try
			{
				foreach (string fileName in fileNames)
				{
					if (!File.Exists(fileName))
					{
						string path = Path.GetFullPath(fileName);
						throw new FileNotFoundException(string.Format("Could not find file '{0}'.", path), path);
					}
					files.Add(WStringMarshaler.Instance.MarshalManagedToNative(fileName));
				}
				Instance.MozSetFileNameArray(files.ToArray(), (uint)files.Count);
			}
			finally
			{
				foreach (IntPtr pName in files)
					WStringMarshaler.Instance.CleanUpNativeData(pName);
			}
			
		}
	}
}

