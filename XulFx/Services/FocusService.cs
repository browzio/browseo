using System;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.DOM;
using System.Runtime.InteropServices;

namespace Gecko.Services
{
	/// <summary>
	/// The focus manager deals with all focus related behaviour. Only one element
	/// in the entire application may have the focus at a time; this element
	/// receives any keyboard events. While there is only one application-wide
	/// focused element, each GeckoWindow maintains a reference to the element
	/// that would be focused if the window was active.
	/// 
	/// If the window&apos;s reference is to a frame element (iframe, browser,
	/// editor), then the child window contains the element that is currently
	/// focused. If the window&apos;s reference is to a root element, then the root is
	/// focused. If a window&apos;s reference is null, then no element is focused, yet
	/// the window is still focused.
	/// 
	/// The blur event is fired on an element when it loses the application focus.
	/// After this blur event, if the focus is moving away from a document, two
	/// additional blur events are fired on the old document and window containing
	/// the focus respectively.
	/// 
	/// When a new document is focused, two focus events are fired on the new
	/// document and window respectively. Then the focus event is fired on an
	/// element when it gains the application focus.
	/// 
	/// A special case is that the root element may be focused, yet does not
	/// receive the element focus and blur events. Instead a focus outline may be
	/// drawn around the document.
	/// 
	/// Blur and focus events do not bubble as per the W3C DOM Events spec.
	/// </summary>
	public sealed class FocusService : ComObject<nsIFocusManager>, IGeckoObjectWrapper
	{
		public static FocusService GetService()
		{
			return Xpcom.GetService<nsIFocusManager>(Contracts.FocusManager).Wrap(FocusService.Create);
		}

		private static FocusService Create(nsIFocusManager instance)
		{
			return new FocusService(instance);
		}

		private FocusService(nsIFocusManager instance)
			: base(instance)
		{

		}

		/// <summary>
		/// The most active (frontmost) window, or null if no window that is part of
		/// the application is active. Setting the activeWindow raises it, and
		/// focuses the current child window&apos;s current element, if any.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">The window cannot be NULL.</exception>
		/// <exception cref="System.ArgumentOutOfRangeException">The window is not a non-top-level window.</exception>
		public GeckoWindow ActiveWindow
		{
			get
			{
				return Instance.GetActiveWindowAttribute().Wrap(GeckoWindow.Create);
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				try
				{
					Instance.SetActiveWindowAttribute(value.MozWindowProxy);
				}
				catch(COMException ex)
				{
					if (ex.ErrorCode == GeckoError.NS_ERROR_INVALID_ARG)
						throw new ArgumentOutOfRangeException("value");
					throw;
				}
			}
		}

		/// <summary>
		/// The child window within the activeWindow that is focused. This will
		/// always be activeWindow, a child window of activeWindow or null if no
		/// child window is focused. Setting the focusedWindow changes the focused
		/// window and raises the toplevel window it is in. If the current focus
		/// within the new focusedWindow is a frame element, then the focusedWindow
		/// will actually be set to the child window and the current element within
		/// that set as the focused element. This process repeats downwards until a
		/// non-frame element is found.
		/// </summary>
		public GeckoWindow FocusedWindow
		{
			get
			{
				return Instance.GetFocusedWindowAttribute().Wrap(GeckoWindow.Create);
			}
			set
			{
				Instance.SetFocusedWindowAttribute(value != null ? value.MozWindowProxy : null);
			}
		}

		/// <summary>
		/// The element that is currently focused. This will always be an element
		/// within the document loaded in focusedWindow or null if no element in that
		/// document is focused.
		/// </summary>
		public GeckoElement FocusedElement
		{
			get { return Instance.GetFocusedElementAttribute().Wrap(GeckoElement.Create); }
		}

		/// <summary>
		/// Returns the method that was used to focus the element in window. This
		/// will either be 0, FLAG_BYMOUSE or FLAG_BYKEY. If window is null, then
		/// the current focusedWindow will be used by default. This has the result
		/// of retrieving the method that was used to focus the currently focused
		/// element.
		/// </summary>
		public GeckoFocusFlags GetLastFocusMethod(GeckoWindow window)
		{
			return (GeckoFocusFlags)Instance.GetLastFocusMethod(window != null ? window.MozWindowProxy : null);
		}

		/// <summary>
		/// Changes the focused element reference within the window containing
		/// element to element.
		/// </summary>
		public void SetFocus(GeckoElement element, GeckoFocusFlags flags)
		{
			if (element == null)
				throw new ArgumentNullException("element");

			Instance.SetFocus(element.Instance, (uint)flags);
		}

		/// <summary>
		/// Move the focus to another element. If a startElement is specified, then
		/// movement is done relative to a startElement. If a startElement is null,
		/// then movement is done relative to the currently focused element. If no
		/// element is focused, focus the first focusable element within the
		/// document (or the last focusable element if aType is MOVEFOCUS_END). This
		/// method is equivalent to setting the focusedElement to the new element.
		/// 
		/// Specifying a startElement and using MOVEFOCUS_LAST is not currently
		/// implemented.
		/// 
		/// If no element is found, and a type is either MOVEFOCUS_ROOT or
		/// MOVEFOCUS_CARET, then the focus is cleared. If a type is any other value,
		/// the focus is not changed.
		/// 
		/// Returns the element that was focused.
		/// </summary>
		public GeckoElement MoveFocus(GeckoWindow window, GeckoElement startElement, GeckoMoveFocusType type, GeckoFocusFlags flags)
		{
			return Instance.MoveFocus(window != null ? window.MozWindowProxy : null, startElement != null ? startElement.Instance : null, unchecked((uint)type), unchecked((uint)flags)).Wrap(GeckoElement.Create);
		}

		/// <summary>
		/// Clears the focused element within window. If the current focusedWindow
		/// is a descendant of window, sets the current focusedWindow to window.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">The window is null.</exception>
		public void ClearFocus(GeckoWindow window)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			Instance.ClearFocus(window.MozWindowProxy);
		}

		/// <summary>
		/// Returns the currently focused element within a window. If a window is equal
		/// to the current value of focusedWindow, then the returned element will be
		/// the application-wide focused element (the value of focusedElement). The
		/// return value will be null if no element is focused.
		/// 
		/// If deep is true, then child frames are traversed and the return value
		/// may be the element within a child descendant window that is focused. If
		/// deep if false, then the return value will be the frame element if the
		/// focus is in a child frame.
		/// 
		/// The focusedWindow will be set to the currently focused descendant window of
		/// window, or to window if deep is false. This will be set even if no
		/// element is focused.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">The window is null.</exception>
		public GeckoElement GetFocusedElementForWindow(GeckoWindow window, bool deep, out GeckoWindow focusedWindow)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			mozIDOMWindowProxy aFocusedWindow;
			GeckoElement element = Instance.GetFocusedElementForWindow(window.MozWindowProxy, deep, out aFocusedWindow).Wrap(GeckoElement.Create);
			focusedWindow = aFocusedWindow.Wrap(GeckoWindow.Create);
			return element;
		}

		/// <summary>
		/// Moves the selection caret within aWindow to the current focus.
		/// </summary>
		public void MoveCaretToFocus(GeckoWindow window)
		{
			Instance.MoveCaretToFocus(window != null ? window.MozWindowProxy : null);
		}

		/// <summary>
		/// Check if given element is focusable.
		/// </summary>
		public bool ElementIsFocusable(GeckoElement element, GeckoFocusFlags flags)
		{
			return Instance.ElementIsFocusable(element.Instance, (uint)flags);
		}

		/// <summary>
		/// Called when a window has been raised.
		/// </summary>
		public void WindowRaised(GeckoWindow window)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			Instance.WindowRaised(window.MozWindowProxy);
		}

		/// <summary>
		/// Called when a window has been lowered.
		/// </summary>
		public void WindowLowered(GeckoWindow window)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			Instance.WindowLowered(window.MozWindowProxy);
		}

		/// <summary>
		/// Called when a new document in a window is shown.
		/// 
		/// If needsFocus is true, then focus events are expected to be fired on the
		/// window if this window is in the focused window chain.
		/// </summary>
		public void WindowShown(GeckoWindow window, bool needsFocus)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			Instance.WindowShown(window.MozWindowProxy, needsFocus);
		}

		/// <summary>
		/// Called when a document in a window has been hidden or otherwise can no
		/// longer accept focus.
		/// </summary>
		public void WindowHidden(GeckoWindow window)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			Instance.WindowHidden(window.MozWindowProxy);
		}

		/// <summary>
		/// Used in a child process to indicate that the parent window is now
		/// active or deactive.
		/// </summary>
		public void ParentActivated(GeckoWindow window, bool active)
		{
			if (window == null)
				throw new ArgumentNullException("window");

			Instance.ParentActivated(window.MozWindowProxy, active);
		}

	}
}
