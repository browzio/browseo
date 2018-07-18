using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

// This file contains shared events for any GUI (XUL, WPF, WinForms)
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
		// Navigation
		private static readonly object NavigatingEvent = new object();
		private static readonly object NavigatedEvent = new object();
		private static readonly object FrameNavigatingEvent = new object();
		private static readonly object FrameNavigatedEvent = new object();
		private static readonly object DocumentCompletedEvent = new object();
		private static readonly object RedirectingEvent = new object();
		// History
		private static readonly object HistoryNewEntryEvent = new object();
		private static readonly object HistoryGoBackEvent = new object();
		private static readonly object HistoryGoForwardEvent = new object();
		private static readonly object HistoryReloadEvent = new object();
		private static readonly object HistoryGotoIndexEvent = new object();
		private static readonly object HistoryPurgeEvent = new object();
		// Windows
		private static readonly object CreateWindowEvent = new object();
		// Security
		private static readonly object NSSErrorEvent = new object();
		// Dom
		private static readonly object DomKeyDownEvent = new object();
		private static readonly object DomKeyUpEvent = new object();
		private static readonly object DomKeyPressEvent = new object();
		private static readonly object DomMouseDownEvent = new object();
		private static readonly object DomMouseUpEvent = new object();
		private static readonly object DomMouseOverEvent = new object();
		private static readonly object DomMouseOutEvent = new object();
		private static readonly object DomMouseMoveEvent = new object();
		private static readonly object DomContextMenuEvent = new object();
		private static readonly object DomMouseScrollEvent = new object();
		private static readonly object DomSubmitEvent = new object();
		private static readonly object DomCompositionStartEvent = new object();
		private static readonly object DomCompositionEndEvent = new object();
		private static readonly object DomFocusEvent = new object();
		private static readonly object DomBlurEvent = new object();
		private static readonly object LoadEvent = new object();
		private static readonly object DOMContentLoadedEvent = new object();
		private static readonly object ReadyStateChangeEvent = new object();
		private static readonly object HashChangeEvent = new object();
		private static readonly object DomContentChangedEvent = new object();
		private static readonly object DomClickEvent = new object();
		private static readonly object DomDoubleClickEvent = new object();
		private static readonly object DomDragStartEvent = new object();
		private static readonly object DomDragEnterEvent = new object();
		private static readonly object DomDragOverEvent = new object();
		private static readonly object DomDragLeaveEvent = new object();
		private static readonly object DomDragEvent = new object();
		private static readonly object DomDropEvent = new object();
		private static readonly object DomDragEndEvent = new object();
		private static readonly object DomScrollEvent = new object();
		private static readonly object DomWheelEvent = new object();
		private static readonly object DOMWindowCreatedEvent = new object();
		private static readonly object FullscreenChangeEvent = new object();

		#region Navigation events

		#region public event EventHandler<GeckoNavigatingEventArgs> Navigating

		/// <summary>
		/// Occurs before the browser navigates to a new page.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs before the browser navigates to a new page.")]
		public event EventHandler<GeckoNavigatingEventArgs> Navigating
		{
			add { Events.AddHandler(NavigatingEvent, value); }
			remove { Events.RemoveHandler(NavigatingEvent, value); }
		}

		/// <summary>Raises the <see cref="Navigating"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnNavigating(GeckoNavigatingEventArgs e)
		{
			var evnt = ((EventHandler<GeckoNavigatingEventArgs>)Events[NavigatingEvent]);
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoNavigatedEventArgs> Navigated

		/// <summary>
		/// Occurs after the browser has navigated to a new page.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs after the browser has navigated to a new page.")]
		public event EventHandler<GeckoNavigatedEventArgs> Navigated
		{
			add { Events.AddHandler(NavigatedEvent, value); }
			remove { Events.RemoveHandler(NavigatedEvent, value); }
		}

		/// <summary>Raises the <see cref="Navigated"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnNavigated(GeckoNavigatedEventArgs e)
		{
			var evnt = (EventHandler<GeckoNavigatedEventArgs>)Events[NavigatedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoRedirectingEventArgs> Redirecting

		/// <summary>
		/// Occurs before the browser redirects to a new page.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs before the browser redirects to a new page.")]
		public event EventHandler<GeckoNavigatingEventArgs> Redirecting
		{
			add
			{
				Events.AddHandler(RedirectingEvent, value);
			}
			remove
			{
				Events.RemoveHandler(RedirectingEvent, value);
			}
		}

		/// <summary>Raises the <see cref="Redirecting"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnRedirecting(GeckoNavigatingEventArgs e)
		{
			var evnt = ((EventHandler<GeckoNavigatingEventArgs>)Events[RedirectingEvent]);
			if (evnt != null)
				evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoNavigatingEventArgs> FrameNavigating

		/// <summary>
		/// Occurs before the browser navigates to a new frame.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs before the browser navigates to a new frame.")]
		public event EventHandler<GeckoNavigatingEventArgs> FrameNavigating
		{
			add { Events.AddHandler(FrameNavigatingEvent, value); }
			remove { Events.RemoveHandler(FrameNavigatingEvent, value); }
		}

		/// <summary>Raises the <see cref="FrameNavigating"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnFrameNavigating(GeckoNavigatingEventArgs e)
		{
			var evnt = ((EventHandler<GeckoNavigatingEventArgs>)Events[FrameNavigatingEvent]);
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoNavigatedEventArgs> FrameNavigated

		/// <summary>
		/// Occurs after the browser has navigated to a new frame.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs after the browser has navigated to a new frame.")]
		public event EventHandler<GeckoNavigatedEventArgs> FrameNavigated
		{
			add { Events.AddHandler(FrameNavigatedEvent, value); }
			remove { Events.RemoveHandler(FrameNavigatedEvent, value); }
		}

		/// <summary>Raises the <see cref="FrameNavigated"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnFrameNavigated(GeckoNavigatedEventArgs e)
		{
			var evnt = (EventHandler<GeckoNavigatedEventArgs>)Events[FrameNavigatedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler DocumentCompleted<GeckoDocumentCompletedEventArgs>

		/// <summary>
		/// Occurs after the browser has finished parsing a new page and updated the <see cref="Document"/> property.
		/// </summary>
		[Category("Navigation")]
		[Description("Occurs after the browser has finished parsing a new page and updated the Document property.")]
		public event EventHandler<GeckoDocumentCompletedEventArgs> DocumentCompleted
		{
			add { Events.AddHandler(DocumentCompletedEvent, value); }
			remove { Events.RemoveHandler(DocumentCompletedEvent, value); }
		}

		/// <summary>Raises the <see cref="DocumentCompleted"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDocumentCompleted(GeckoDocumentCompletedEventArgs e)
		{
			var evnt = (EventHandler<GeckoDocumentCompletedEventArgs>)Events[DocumentCompletedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#endregion Navigation events

		#region History events

		#region public event EventHandler<GeckoHistoryEventArgs> HistoryNewEntry

		[Category("History")]
		public event EventHandler<GeckoHistoryEventArgs> HistoryNewEntry
		{
			add { Events.AddHandler(HistoryNewEntryEvent, value); }
			remove { Events.RemoveHandler(HistoryNewEntryEvent, value); }
		}

		/// <summary>Raises the <see cref="HistoryNewEntry"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryNewEntry(GeckoHistoryEventArgs e)
		{
			var evnt = (EventHandler<GeckoHistoryEventArgs>)Events[HistoryNewEntryEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoHistoryEventArgs> HistoryGoBack

		[Category("History")]
		public event EventHandler<GeckoHistoryEventArgs> HistoryGoBack
		{
			add { Events.AddHandler(HistoryGoBackEvent, value); }
			remove { Events.RemoveHandler(HistoryGoBackEvent, value); }
		}

		/// <summary>Raises the <see cref="HistoryGoBack"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryGoBack(GeckoHistoryEventArgs e)
		{
			var evnt = (EventHandler<GeckoHistoryEventArgs>)Events[HistoryGoBackEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoHistoryEventArgs> HistoryGoForward

		[Category("History")]
		public event EventHandler<GeckoHistoryEventArgs> HistoryGoForward
		{
			add { Events.AddHandler(HistoryGoForwardEvent, value); }
			remove { Events.RemoveHandler(HistoryGoForwardEvent, value); }
		}

		/// <summary>Raises the <see cref="HistoryGoForward"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryGoForward(GeckoHistoryEventArgs e)
		{
			var evnt = (EventHandler<GeckoHistoryEventArgs>)Events[HistoryGoForwardEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event EventHandler<GeckoHistoryEventArgs> HistoryReload

		[Category("History")]
		public event EventHandler<GeckoHistoryEventArgs> HistoryReload
		{
			add { Events.AddHandler(HistoryReloadEvent, value); }
			remove { Events.RemoveHandler(HistoryReloadEvent, value); }
		}

		/// <summary>Raises the <see cref="HistoryReload"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryReload(GeckoHistoryEventArgs e)
		{
			var evnt = (EventHandler<GeckoHistoryEventArgs>)Events[HistoryReloadEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event GeckoHistoryGotoIndexEventHandler HistoryGotoIndex

		[Category("History")]
		public event EventHandler<GeckoHistoryGotoIndexEventArgs> HistoryGotoIndex
		{
			add { Events.AddHandler(HistoryGotoIndexEvent, value); }
			remove { Events.RemoveHandler(HistoryGotoIndexEvent, value); }
		}

		/// <summary>Raises the <see cref="HistoryGotoIndex"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryGotoIndex(GeckoHistoryGotoIndexEventArgs e)
		{
			var evnt = (EventHandler<GeckoHistoryGotoIndexEventArgs>)Events[HistoryGotoIndexEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event GeckoHistoryPurgeEventHandler HistoryPurge

		[Category("History")]
		public event EventHandler<GeckoHistoryPurgeEventArgs> HistoryPurge
		{
			add { Events.AddHandler(HistoryPurgeEvent, value); }
			remove { Events.RemoveHandler(HistoryPurgeEvent, value); }
		}

		/// <summary>Raises the <see cref="HistoryPurge"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHistoryPurge(GeckoHistoryPurgeEventArgs e)
		{
			var evnt = (EventHandler<GeckoHistoryPurgeEventArgs>)Events[HistoryPurgeEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#endregion

		#region Window events

		#region public event GeckoCreateWindowEventHandler CreateWindow

		public event EventHandler<GeckoCreateWindowEventArgs> CreateWindow
		{
			add { Events.AddHandler(CreateWindowEvent, value); }
			remove { Events.RemoveHandler(CreateWindowEvent, value); }
		}

		/// <summary>Raises the <see cref="CreateWindow"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnCreateWindow(GeckoCreateWindowEventArgs e)
		{
			var evnt = (EventHandler<GeckoCreateWindowEventArgs>)Events[CreateWindowEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion
		#endregion

		#region Dom EventHandlers

		#region Dom keyboard events
		#region public event GeckoDomKeyEventHandler DomKeyDown
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMKeyEventArgs> DomKeyDown
		{
			add { Events.AddHandler(DomKeyDownEvent, value); }
			remove { Events.RemoveHandler(DomKeyDownEvent, value); }
		}

		/// <summary>Raises the <see cref="DomKeyDown"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomKeyDown(GeckoDOMKeyEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMKeyEventArgs>)Events[DomKeyDownEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomKeyEventHandler DomKeyUp
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMKeyEventArgs> DomKeyUp
		{
			add { Events.AddHandler(DomKeyUpEvent, value); }
			remove { Events.RemoveHandler(DomKeyUpEvent, value); }
		}

		/// <summary>Raises the <see cref="DomKeyUp"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomKeyUp(GeckoDOMKeyEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMKeyEventArgs>)Events[DomKeyUpEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomKeyEventHandler DomKeyPress
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMKeyEventArgs> DomKeyPress
		{
			add { Events.AddHandler(DomKeyPressEvent, value); }
			remove { Events.RemoveHandler(DomKeyPressEvent, value); }
		}

		/// <summary>Raises the <see cref="DomKeyPress"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomKeyPress(GeckoDOMKeyEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMKeyEventArgs>)Events[DomKeyPressEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion
		#endregion

		#region Dom mouse events
		#region public event GeckoDomMouseEventHandler DomMouseDown
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomMouseDown
		{
			add { Events.AddHandler(DomMouseDownEvent, value); }
			remove { Events.RemoveHandler(DomMouseDownEvent, value); }
		}

		/// <summary>Raises the <see cref="DomMouseDown"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseDown(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomMouseDownEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomMouseUp
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomMouseUp
		{
			add { Events.AddHandler(DomMouseUpEvent, value); }
			remove { Events.RemoveHandler(DomMouseUpEvent, value); }
		}

		/// <summary>Raises the <see cref="DomMouseUp"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseUp(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomMouseUpEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomMouseOver
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomMouseOver
		{
			add { Events.AddHandler(DomMouseOverEvent, value); }
			remove { Events.RemoveHandler(DomMouseOverEvent, value); }
		}


		/// <summary>Raises the <see cref="DomMouseOver"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseOver(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)this.Events[DomMouseOverEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomMouseOut
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomMouseOut
		{
			add { Events.AddHandler(DomMouseOutEvent, value); }
			remove { Events.RemoveHandler(DomMouseOutEvent, value); }
		}

		/// <summary>Raises the <see cref="DomMouseOut"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseOut(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomMouseOutEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomMouseMove
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomMouseMove
		{
			add { Events.AddHandler(DomMouseMoveEvent, value); }
			remove { Events.RemoveHandler(DomMouseMoveEvent, value); }
		}

		/// <summary>Raises the <see cref="DomMouseMove"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseMove(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomMouseMoveEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DomContextMenu
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomContextMenu
		{
			add { Events.AddHandler(DomContextMenuEvent, value); }
			remove { Events.RemoveHandler(DomContextMenuEvent, value); }
		}

		/// <summary>Raises the <see cref="DomContextMenu"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomContextMenu(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomContextMenuEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomMouseEventHandler DOMMouseScroll
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomMouseScroll
		{
			add { Events.AddHandler(DomMouseScrollEvent, value); }
			remove { Events.RemoveHandler(DomMouseScrollEvent, value); }
		}

		/// <summary>Raises the <see cref="DomMouseScroll"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomMouseScroll(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomMouseScrollEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion
		#endregion

		#region public event GeckoDomEventHandler DomSubmit
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> DomSubmit
		{
			add
            {
                Events.AddHandler(DomSubmitEvent, value); }
			remove
            {
                Events.RemoveHandler(DomSubmitEvent, value); }
		}

		/// <summary>Raises the <see cref="DomSubmit"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomSubmit(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[DomSubmitEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomCompositionStart
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> DomCompositionStart
		{
			add { Events.AddHandler(DomCompositionStartEvent, value); }
			remove { Events.RemoveHandler(DomCompositionStartEvent, value); }
		}

		/// <summary>Raises the <see cref="DomCompositionStart"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomCompositionStart(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[DomCompositionStartEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomCompositionEnd
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> DomCompositionEnd
		{
			add { Events.AddHandler(DomCompositionEndEvent, value); }
			remove { Events.RemoveHandler(DomCompositionEndEvent, value); }
		}

		/// <summary>Raises the <see cref="DomCompositionEnd"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomCompositionEnd(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[DomCompositionEndEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomFocus
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> DomFocus
		{
			add { Events.AddHandler(DomFocusEvent, value); }
			remove { Events.RemoveHandler(DomFocusEvent, value); }
		}

		/// <summary>Raises the <see cref="DomFocus"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomFocus(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[DomFocusEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomBlur
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> DomBlur
		{
			add { Events.AddHandler(DomBlurEvent, value); }
			remove { Events.RemoveHandler(DomBlurEvent, value); }
		}

		/// <summary>Raises the <see cref="DomBlur"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomBlur(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[DomBlurEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler Load
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> Load
		{
			add { Events.AddHandler(LoadEvent, value); }
			remove { Events.RemoveHandler(LoadEvent, value); }
		}

		/// <summary>Raises the <see cref="LoadEvent"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnLoad(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[LoadEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DOMContentLoaded
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> DOMContentLoaded
		{
			add
			{
				Events.AddHandler(DOMContentLoadedEvent, value);
			}
			remove
			{
				Events.RemoveHandler(DOMContentLoadedEvent, value);
			}
		}

		/// <summary>Raises the <see cref="LoadEvent"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDOMContentLoaded(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[DOMContentLoadedEvent];
			if (evnt != null)
				evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler ReadyStateChange
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> ReadyStateChange
		{
			add
			{
				Events.AddHandler(ReadyStateChangeEvent, value);
			}
			remove
			{
				Events.RemoveHandler(ReadyStateChangeEvent, value);
			}
		}

		/// <summary>Raises the <see cref="LoadEvent"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnReadyStateChange(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[ReadyStateChangeEvent];
			if (evnt != null)
				evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler HashChange
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMHashChangeEventArgs> HashChange
		{
			add { Events.AddHandler(HashChangeEvent, value); }
			remove { Events.RemoveHandler(HashChangeEvent, value); }
		}

		/// <summary>Raises the <see cref="HashChangeEvent"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnHashChange(GeckoDOMHashChangeEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMHashChangeEventArgs>)Events[HashChangeEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region drag events

		// DragStart

		public event EventHandler<GeckoDOMDragEventArgs> DomDragStart
		{
			add { Events.AddHandler(DomDragStartEvent, value); }
			remove { Events.RemoveHandler(DomDragStartEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragStart"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragStart(GeckoDOMDragEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMDragEventArgs>)Events[DomDragStartEvent];
			if (evnt != null) evnt(this, e);
		}

		// DragEnter

		public event EventHandler<GeckoDOMDragEventArgs> DomDragEnter
		{
			add { Events.AddHandler(DomDragEnterEvent, value); }
			remove { Events.RemoveHandler(DomDragEnterEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragEnter"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragEnter(GeckoDOMDragEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMDragEventArgs>)Events[DomDragEnterEvent];
			if (evnt != null) evnt(this, e);
		}

		// DragOver

		public event EventHandler<GeckoDOMDragEventArgs> DomDragOver
		{
			add { Events.AddHandler(DomDragOverEvent, value); }
			remove { Events.RemoveHandler(DomDragOverEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragOver"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragOver(GeckoDOMDragEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMDragEventArgs>)Events[DomDragOverEvent];
			if (evnt != null) evnt(this, e);
		}

		// DragLeave

		public event EventHandler<GeckoDOMDragEventArgs> DomDragLeave
		{
			add { Events.AddHandler(DomDragLeaveEvent, value); }
			remove { Events.RemoveHandler(DomDragLeaveEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragLeave"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragLeave(GeckoDOMDragEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMDragEventArgs>)Events[DomDragLeaveEvent];
			if (evnt != null) evnt(this, e);
		}

		// Drag

		public event EventHandler<GeckoDOMDragEventArgs> DomDrag
		{
			add { Events.AddHandler(DomDragEvent, value); }
			remove { Events.RemoveHandler(DomDragEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDrag"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDrag(GeckoDOMDragEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMDragEventArgs>)Events[DomDragEvent];
			if (evnt != null) evnt(this, e);
		}

		// Drop

		public event EventHandler<GeckoDOMDragEventArgs> DomDrop
		{
			add { Events.AddHandler(DomDropEvent, value); }
			remove { Events.RemoveHandler(DomDropEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDrop"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDrop(GeckoDOMDragEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMDragEventArgs>)Events[DomDropEvent];
			if (evnt != null) evnt(this, e);
		}

		// DragEnd

		public event EventHandler<GeckoDOMDragEventArgs> DomDragEnd
		{
			add { Events.AddHandler(DomDragEndEvent, value); }
			remove { Events.RemoveHandler(DomDragEndEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDragEnd"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDragEnd(GeckoDOMDragEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMDragEventArgs>)Events[DomDragEndEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#region public event GeckoDomEventHandler DomContentChanged
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> DomContentChanged
		{
			add { Events.AddHandler(DomContentChangedEvent, value); }
			remove { Events.RemoveHandler(DomContentChangedEvent, value); }
		}

		/// <summary>Raises the <see cref="DomContentChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomContentChanged(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[DomContentChangedEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomClick
		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomClick
		{
			add { Events.AddHandler(DomClickEvent, value); }
			remove { Events.RemoveHandler(DomClickEvent, value); }
		}

		/// <summary>Raises the <see cref="DomClick"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomClick(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomClickEvent];
			if (evnt != null) evnt(this, e);
		}
		#endregion

		#region public event GeckoDomEventHandler DomDoubleClick

		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomDoubleClick
		{
			add { Events.AddHandler(DomDoubleClickEvent, value); }
			remove { Events.RemoveHandler(DomDoubleClickEvent, value); }
		}

		/// <summary>Raises the <see cref="DomDoubleClick"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomDoubleClick(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomDoubleClickEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion public event GeckoDomEventHandler DomDoubleClick

		#region public event DomScroll

		[Category("DOM Events")]
		public event EventHandler<GeckoDOMUIEventArgs> DomScroll
		{
			add { Events.AddHandler(DomScrollEvent, value); }
			remove { Events.RemoveHandler(DomScrollEvent, value); }
		}

		/// <summary>Raises the <see cref="DomScroll"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomScroll(GeckoDOMUIEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMUIEventArgs>)Events[DomScrollEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion public event DomScroll

		#region public event DomWheel

		[Category("DOM Events")]
		public event EventHandler<GeckoDOMMouseEventArgs> DomWheel
		{
			add { Events.AddHandler(DomWheelEvent, value); }
			remove { Events.RemoveHandler(DomWheelEvent, value); }
		}

		/// <summary>Raises the <see cref="DomScroll"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDomWheel(GeckoDOMMouseEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMMouseEventArgs>)Events[DomWheelEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion public event DomWheel

		#region public event DOMWindowCreated

		[Category("DOM Events")]
		public event EventHandler<GeckoDOMEventArgs> DOMWindowCreated
		{
			add { Events.AddHandler(DOMWindowCreatedEvent, value); }
			remove { Events.RemoveHandler(DOMWindowCreatedEvent, value); }
		}

		/// <summary>Raises the <see cref="DomScroll"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnDOMWindowCreated(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[DOMWindowCreatedEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion public event DOMWindowCreated

		#region public event GeckoDomEventHandler FullscreenChange

		public event EventHandler<GeckoDOMEventArgs> FullscreenChange
		{
			add { Events.AddHandler(FullscreenChangeEvent, value); }
			remove { Events.RemoveHandler(FullscreenChangeEvent, value); }
		}

		/// <summary>Raises the <see cref="FullscreenChangeEvent"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnFullscreenChange(GeckoDOMEventArgs e)
		{
			var evnt = (EventHandler<GeckoDOMEventArgs>)Events[FullscreenChangeEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion

		#endregion

		#region public event EventHandler<GeckoNSSErrorEventArgs> NSSError

		/// <summary>
		/// Occurs when the control has updated progress information.
		/// </summary>
		[Category("Security")]
		public event EventHandler<GeckoNSSErrorEventArgs> NSSError
		{
			add { Events.AddHandler(NSSErrorEvent, value); }
			remove { Events.RemoveHandler(NSSErrorEvent, value); }
		}


		/// <summary>Raises the <see cref="NSSError"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnNSSError(GeckoNSSErrorEventArgs e)
		{
			var evnt = (EventHandler<GeckoNSSErrorEventArgs>)Events[NSSErrorEvent];
			if (evnt != null) evnt(this, e);
		}

		#endregion
	}
}
