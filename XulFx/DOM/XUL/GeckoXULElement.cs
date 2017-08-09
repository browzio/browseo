using Gecko.DOM.Abstractions;
using Gecko.DOM.XUL;
using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko.DOM
{
	public class GeckoXULElement : GeckoElement<nsIDOMXULElement>
	{
		public static GeckoXULElement Create(nsIDOMXULElement xulElement)
		{
			return new GeckoXULElement(xulElement);
		}

		private static GeckoXULElement TryCast<TInterface, TWrapper>(nsIDOMXULElement instance, Func<TInterface, TWrapper> create)
			where TInterface : class
			where TWrapper : GeckoXULElement
		{
			var obj = Xpcom.QueryInterface<TInterface>(instance);
			if (obj != null)
			{
				return (GeckoXULElement)create(obj);
			}
			return null;
		}

		protected GeckoXULElement(nsIDOMXULElement xulElement)
			: base(xulElement)
		{

		}


		public string ClassName
		{
			get { return nsString.Get(Instance.GetClassNameAttribute); }
			set { nsString.Set(Instance.SetClassNameAttribute, value); }
		}

		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		public string Dir
		{
			get { return nsString.Get(Instance.GetDirAttribute); }
			set { nsString.Set(Instance.SetDirAttribute, value); }
		}

		public string Flex
		{
			get { return nsString.Get(Instance.GetFlexAttribute); }
			set { nsString.Set(Instance.SetFlexAttribute, value); }
		}

		public string FlexGroup
		{
			get { return nsString.Get(Instance.GetFlexGroupAttribute); }
			set { nsString.Set(Instance.SetFlexGroupAttribute, value); }
		}

		public string Ordinal
		{
			get { return nsString.Get(Instance.GetOrdinalAttribute); }
			set { nsString.Set(Instance.SetOrdinalAttribute, value); }
		}

		public string Orient
		{
			get { return nsString.Get(Instance.GetOrientAttribute); }
			set { nsString.Set(Instance.SetOrientAttribute, value); }
		}

		public string Pack
		{
			get { return nsString.Get(Instance.GetPackAttribute); }
			set { nsString.Set(Instance.SetPackAttribute, value); }
		}

		public bool Hidden
		{
			get { return Instance.GetHiddenAttribute(); }
			set { Instance.SetHiddenAttribute(value); }
		}

		public bool Collapsed
		{
			get { return Instance.GetHiddenAttribute(); }
			set { Instance.SetHiddenAttribute(value); }
		}

		public string Observes
		{
			get { return nsString.Get(Instance.GetObservesAttribute); }
			set { nsString.Set(Instance.SetObservesAttribute, value); }
		}

		public string Menu
		{
			get { return nsString.Get(Instance.GetMenuAttribute); }
			set { nsString.Set(Instance.SetMenuAttribute, value); }
		}

		public string ContextMenu
		{
			get { return nsString.Get(Instance.GetContextMenuAttribute); }
			set { nsString.Set(Instance.SetContextMenuAttribute, value); }
		}

		public string ToolTip
		{
			get { return nsString.Get(Instance.GetPackAttribute); }
			set { nsString.Set(Instance.SetPackAttribute, value); }
		}

		public string Width
		{
			get { return nsString.Get(Instance.GetWidthAttribute); }
			set { nsString.Set(Instance.SetWidthAttribute, value); }
		}

		public string Height
		{
			get { return nsString.Get(Instance.GetHeightAttribute); }
			set { nsString.Set(Instance.SetHeightAttribute, value); }
		}

		public string MinWidth
		{
			get { return nsString.Get(Instance.GetMinWidthAttribute); }
			set { nsString.Set(Instance.SetMinWidthAttribute, value); }
		}

		public string MinHeight
		{
			get { return nsString.Get(Instance.GetMinHeightAttribute); }
			set { nsString.Set(Instance.SetMinHeightAttribute, value); }
		}

		public string MaxWidth
		{
			get { return nsString.Get(Instance.GetMaxWidthAttribute); }
			set { nsString.Set(Instance.SetMaxWidthAttribute, value); }
		}

		public string MaxHeight
		{
			get { return nsString.Get(Instance.GetMaxHeightAttribute); }
			set { nsString.Set(Instance.SetMaxHeightAttribute, value); }
		}

		public string Persist
		{
			get { return nsString.Get(Instance.GetPersistAttribute); }
			set { nsString.Set(Instance.SetPersistAttribute, value); }
		}

		public string Left
		{
			get { return nsString.Get(Instance.GetLeftAttribute); }
			set { nsString.Set(Instance.SetLeftAttribute, value); }
		}

		public string Top
		{
			get { return nsString.Get(Instance.GetTopAttribute); }
			set { nsString.Set(Instance.SetTopAttribute, value); }
		}

		public string Datasources
		{
			get { return nsString.Get(Instance.GetDatasourcesAttribute); }
			set { nsString.Set(Instance.SetDatasourcesAttribute, value); }
		}

		public string Ref
		{
			get { return nsString.Get(Instance.GetRefAttribute); }
			set { nsString.Set(Instance.SetRefAttribute, value); }
		}

		public string TooltipText
		{
			get { return nsString.Get(Instance.GetTooltipTextAttribute); }
			set { nsString.Set(Instance.SetTooltipTextAttribute, value); }
		}

		public string StatusText
		{
			get { return nsString.Get(Instance.GetStatusTextAttribute); }
			set { nsString.Set(Instance.SetStatusTextAttribute, value); }
		}

		public bool AllowEvents
		{
			get { return Instance.GetAllowEventsAttribute(); }
			set { Instance.SetAllowEventsAttribute(value); }
		}

		public nsIRDFCompositeDataSource Database
		{
			get { return Instance.GetDatabaseAttribute(); }
		}

		//readonly attribute nsIRDFCompositeDataSource database;
		//readonly attribute nsIXULTemplateBuilder     builder;
		//readonly attribute nsIRDFResource            resource;
		//readonly attribute nsIControllers            controllers;

		public GeckoBoxObject BoxObject
		{
			get { return Instance.GetBoxObjectAttribute().Wrap(GeckoBoxObject.Create); }
		}

		public void Focus()
		{
			Instance.Focus();
		}

		public void Blur()
		{
			Instance.Blur();
		}

		public void Click()
		{
			Instance.Click();
		}

		public void DoCommand()
		{
			Instance.DoCommand();
		}

		public GeckoNodeCollection GetElementsByAttibute(string name, string value)
		{
			if (name == null)
				throw new ArgumentNullException("name");

			if (name.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("name");

			return nsString.Pass(Instance.GetElementsByAttribute, name, value).Wrap(GeckoNodeCollection.Create);
		}

		public GeckoNodeCollection GetElementsByAttibuteNS(string namespaceURI, string name, string value)
		{
			if (namespaceURI == null)
				throw new ArgumentNullException("namespaceURI");

			if (namespaceURI.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("namespaceURI");

			if (name == null)
				throw new ArgumentNullException("name");

			if (name.IsEmptyOrWhiteSpace())
				throw new ArgumentOutOfRangeException("name");

			return nsString.Pass(Instance.GetElementsByAttributeNS, namespaceURI, name, value).Wrap(GeckoNodeCollection.Create);
		}

	}
}
