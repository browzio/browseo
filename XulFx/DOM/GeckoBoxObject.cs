using Gecko.DOM.Abstractions;
using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko.DOM
{
	public class GeckoBoxObject : ComObject<nsIBoxObject>, IGeckoObjectWrapper
	{
		public static GeckoBoxObject Create(nsIBoxObject instance)
		{
			return new GeckoBoxObject(instance);
		}

		protected GeckoBoxObject(nsIBoxObject instance)
			: base(instance)
		{

		}

		public GeckoElement Element
		{
			get { return Instance.GetElementAttribute().Wrap(GeckoElement.Create); }
		}

		public int X
		{
			get { return Instance.GetXAttribute(); }
		}

		public int Y
		{
			get { return Instance.GetYAttribute(); }
		}

		public int ScreenX
		{
			get { return Instance.GetScreenXAttribute(); }
		}

		public int ScreenY
		{
			get { return Instance.GetScreenYAttribute(); }
		}

		public int Width
		{
			get { return Instance.GetWidthAttribute(); }
		}

		public int Height
		{
			get { return Instance.GetHeightAttribute(); }
		}

		public string this[string propertyName]
		{
			get
			{
				return Instance.GetProperty(propertyName);
			}
			set
			{
				Instance.SetProperty(propertyName, value);
			}
		}

		public nsISupports GetPropertyAsObject(string propertyName)
		{
			return Instance.GetPropertyAsSupports(propertyName);
		}

		public void SetPropertyAsObject(string propertyName, nsISupports value)
		{
			Instance.SetPropertyAsSupports(propertyName, value);
		}

		public void RemoveProperty(string propertyName)
		{
			Instance.RemoveProperty(propertyName);
		}

		public GeckoElement ParentBox
		{
			get { return Instance.GetParentBoxAttribute().Wrap(GeckoElement.Create); }
		}

		public GeckoElement FirstChild
		{
			get { return Instance.GetFirstChildAttribute().Wrap(GeckoElement.Create); }
		}

		public GeckoElement LastChild
		{
			get { return Instance.GetLastChildAttribute().Wrap(GeckoElement.Create); }
		}

		public GeckoElement NextSibling
		{
			get { return Instance.GetNextSiblingAttribute().Wrap(GeckoElement.Create); }
		}

		public GeckoElement PreviousSibling
		{
			get { return Instance.GetPreviousSiblingAttribute().Wrap(GeckoElement.Create); }
		}

	}
}
