using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

// This file contains shared events for non-WPF GUI
#if WINDOWS
namespace Gecko.Windows
{	
	partial class WebView
#else
namespace Gecko
{
	partial class WindowlessWebView
#endif
	{
		#region Event Keys

		private static readonly object CanGoBackChangedEvent = new object();
		private static readonly object CanGoForwardChangedEvent = new object();
		// ProgressChanged
		private static readonly object RequestProgressChangedEvent = new object();
		private static readonly object ProgressChangedEvent = new object();

		// Windows
		private static readonly object WindowSetBoundsEvent = new object();
		private static readonly object WindowClosedEvent = new object();
		private static readonly object WindowCloseEvent = new object();
		// StatusTextChanged
		private static readonly object StatusTextChangedEvent = new object();
		// DocumentTitleChanged
		private static readonly object DocumentTitleChangedEvent = new object();
		// ShowContextMenu
		private static readonly object ShowContextMenuEvent = new object();
		// ObserveHttpModifyRequest
		private static readonly object ObserveHttpModifyRequestEvent = new object();

		#endregion

		#region Navigation events

		#region public event EventHandler CanGoBackChanged

		/// <summary>
		/// Occurs when the value of the <see cref="CanGoBack"/> property is changed.
		/// </summary>
		[Category("Property Changed")]
		[Description("Occurs when the value of the CanGoBack property is changed.")]
		public event EventHandler CanGoBackChanged
		{
			add { Events.AddHandler(CanGoBackChangedEvent, value); }
			remove { Events.RemoveHandler(CanGoBackChangedEvent, value); }
		}

		/// <summary>Raises the <see cref="CanGoBackChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnCanGoBackChanged(EventArgs e)
		{
			var evnt = (EventHandler)Events[CanGoBackChangedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler CanGoForwardChanged

		/// <summary>
		/// Occurs when the value of the <see cref="CanGoForward"/> property is changed.
		/// </summary>
		[Category("Property Changed")]
		[Description("Occurs when the value of the CanGoForward property is changed.")]
		public event EventHandler CanGoForwardChanged
		{
			add { Events.AddHandler(CanGoForwardChangedEvent, value); }
			remove { Events.RemoveHandler(CanGoForwardChangedEvent, value); }
		}

		/// <summary>Raises the <see cref="CanGoForwardChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnCanGoForwardChanged(EventArgs e)
		{
			var evnt = (EventHandler)Events[CanGoForwardChangedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#endregion


		#region public event GeckoProgressEventHandler ProgressChanged

		/// <summary>
		/// Occurs when the control has updated progress information.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs when the control has updated progress information.")]
		public event EventHandler<GeckoProgressEventArgs> ProgressChanged
		{
			add { Events.AddHandler(ProgressChangedEvent, value); }
			remove { Events.RemoveHandler(ProgressChangedEvent, value); }
		}


		/// <summary>Raises the <see cref="ProgressChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnProgressChanged(GeckoProgressEventArgs e)
		{
			var evnt = (EventHandler<GeckoProgressEventArgs>)Events[ProgressChangedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion



		//#region public event GeckoWindowSetBoundsEventHandler WindowSetBounds



		//public event EventHandler<GeckoWindowSetBoundsEventArgs> WindowSetBounds
		//{
		//	add { Events.AddHandler(WindowSetBoundsEvent, value); }
		//	remove { Events.RemoveHandler(WindowSetBoundsEvent, value); }
		//}

		///// <summary>Raises the <see cref="WindowSetBounds"/> event.</summary>
		///// <param name="e">The data for the event.</param>
		//protected virtual void OnWindowSetBounds(GeckoWindowSetBoundsEventArgs e)
		//{
		//	var evnt = (EventHandler<GeckoWindowSetBoundsEventArgs>)Events[WindowSetBoundsEvent];
		//	if (evnt != null) evnt(this, e);
		//}

		//#endregion

		#region Window events

		#region public event EventHandler WindowClose
		public event EventHandler<GeckoDOMEventArgs> WindowClose
		{
			add { Events.AddHandler(WindowCloseEvent, value); }
			remove { Events.RemoveHandler(WindowCloseEvent, value); }
		}


		/// <summary>Raises the <see cref="WindowClosed"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnWindowClose(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[WindowCloseEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event EventHandler WindowClosed
		public event EventHandler<GeckoDOMEventArgs> WindowClosed
		{
			add { Events.AddHandler(WindowClosedEvent, value); }
			remove { Events.RemoveHandler(WindowClosedEvent, value); }
		}


		/// <summary>Raises the <see cref="WindowClosed"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnWindowClosed(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[WindowClosedEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#endregion

		#region public event EventHandler StatusTextChanged

		/// <summary>
		/// Occurs when the value of the <see cref="StatusText"/> property is changed.
		/// </summary>
		[Category("Property Changed")]
		[Description("Occurs when the value of the StatusText property is changed.")]
		public event EventHandler StatusTextChanged
		{
			add { Events.AddHandler(StatusTextChangedEvent, value); }
			remove { Events.RemoveHandler(StatusTextChangedEvent, value); }
		}

		/// <summary>Raises the <see cref="StatusTextChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnStatusTextChanged(EventArgs e)
		{
			var evnt = (EventHandler)Events[StatusTextChangedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler DocumentTitleChanged

		/// <summary>
		/// Occurs when the value of the <see cref="DocumentTitle"/> property is changed.
		/// </summary>
		[Category("Property Changed")]
		[Description("Occurs when the value of the DocumentTitle property is changed.")]
		public event EventHandler DocumentTitleChanged
		{
			add { Events.AddHandler(DocumentTitleChangedEvent, value); }
			remove { Events.RemoveHandler(DocumentTitleChangedEvent, value); }
		}

		/// <summary>Raises the <see cref="DocumentTitleChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDocumentTitleChanged(EventArgs e)
		{
			var evnt = (EventHandler)Events[DocumentTitleChangedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		//#region public event GeckoContextMenuEventHandler ShowContextMenu

		//public event EventHandler<GeckoContextMenuEventArgs> ShowContextMenu
		//{
		//	add { Events.AddHandler(ShowContextMenuEvent, value); }
		//	remove { Events.RemoveHandler(ShowContextMenuEvent, value); }
		//}

		///// <summary>Raises the <see cref="ShowContextMenu"/> event.</summary>
		///// <param name="e">The data for the event.</param>
		//protected virtual void OnShowContextMenu(GeckoContextMenuEventArgs e)
		//{
		//	var evnt = (EventHandler<GeckoContextMenuEventArgs>)Events[ShowContextMenuEvent];
		//	if (evnt != null) evnt(this, e);
		//}

		//#endregion




	}
}
