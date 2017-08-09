using System;
using System.Windows;

namespace Gecko.Windows
{
	// This file contains only routed events
	partial class WebView
	{
		#region event CanGoBackChanged

		public static readonly RoutedEvent CanGoBackChangedEvent = EventManager.RegisterRoutedEvent(
			"CanGoBackChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WebView));

		public event RoutedEventHandler CanGoBackChanged
		{
			add { AddHandler(CanGoBackChangedEvent, value); }
			remove { RemoveHandler(CanGoBackChangedEvent, value); }
		}

		protected virtual void OnCanGoBackChanged(RoutedEventArgs e)
		{
			RaiseEvent(e);
		}

		#endregion event CanGoBackChanged

		#region event CanGoForwardChanged

		public static readonly RoutedEvent CanGoForwardChangedEvent = EventManager.RegisterRoutedEvent(
			"CanGoForwardChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WebView));

		public event RoutedEventHandler CanGoForwardChanged
		{
			add { AddHandler(CanGoForwardChangedEvent, value); }
			remove { RemoveHandler(CanGoForwardChangedEvent, value); }
		}

		protected virtual void OnCanGoForwardChanged(RoutedEventArgs e)
		{
			RaiseEvent(e);
		}

		#endregion event CanGoForwardChanged

		#region event StatusTextChanged

		public static readonly RoutedEvent StatusTextChangedEvent = EventManager.RegisterRoutedEvent(
			"StatusTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WebView));

		public event RoutedEventHandler StatusTextChanged
		{
			add { AddHandler(StatusTextChangedEvent, value); }
			remove { RemoveHandler(StatusTextChangedEvent, value); }
		}

		protected virtual void OnStatusTextChanged(RoutedEventArgs e)
		{
			RaiseEvent(e);
		}

		#endregion event StatusTextChanged

		#region event DocumentTitleChanged

		public static readonly RoutedEvent DocumentTitleChangedEvent = EventManager.RegisterRoutedEvent(
			"DocumentTitleChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WebView));

		public event RoutedEventHandler DocumentTitleChanged
		{
			add { AddHandler(DocumentTitleChangedEvent, value); }
			remove { RemoveHandler(DocumentTitleChangedEvent, value); }
		}

		protected virtual void OnDocumentTitleChanged(RoutedEventArgs e)
		{
			RaiseEvent(e);
		}

		#endregion event DocumentTitleChanged

		#region event WindowClosed

		public static readonly RoutedEvent WindowClosedEvent = EventManager.RegisterRoutedEvent(
			"WindowClosed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WebView));

		public event RoutedEventHandler WindowClosed
		{
			add { AddHandler(WindowClosedEvent, value); }
			remove { RemoveHandler(WindowClosedEvent, value); }
		}

		protected virtual void OnWindowClosed(RoutedEventArgs e)
		{
			RaiseEvent(e);
		}

		#endregion event WindowClosed


	}
}
