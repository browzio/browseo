using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko
{
	public sealed class GeckoTextInputProcessor : ComObject<nsITextInputProcessor>, IGeckoObjectWrapper
	{
		public static GeckoTextInputProcessor Create(nsITextInputProcessor instance)
		{
			return new GeckoTextInputProcessor(instance);
		}

		private GeckoTextInputProcessor(nsITextInputProcessor instance)
			: base(instance)
	{

		}

		public GeckoTextInputProcessor()
			: base(Xpcom.CreateInstance<nsITextInputProcessor>(Contracts.TextInputProcessor))
		{
			this.QueryInterface<nsITextInputProcessor>().Wrap<nsITextInputProcessor, GeckoTextInputProcessor>(null);
		}

		/// <summary>
		/// Gets true if this instance was dispatched compositionstart but hasn&apos;t
		/// dispatched compositionend yet.
		/// </summary>
		public bool HasComposition
		{
			get { return Instance.GetHasCompositionAttribute(); }
		}

		/// <summary>
		/// When you create an instance, you must call beginInputTransaction() first
		/// except when you created the instance for automated tests.
		/// </summary>
		/// <param name="window">
		/// A DOM window. The instance will look for a top level widget from this.
		/// </param>
		/// <param name="callback">
		/// Callback interface which handles requests to IME and notifications to IME.
		/// This must not be null.
		/// </param>
		/// <returns>
		/// If somebody uses internal text input service for a composition, this returns
		/// false. Otherwise, returns true. I.e., only your TIP can create composition
		/// when this returns true. If this returns false, your TIP should wait next chance.
		/// </returns>
		public bool BeginInputTransaction(GeckoWindow window, nsITextInputProcessorCallback callback)
		{
			if (window == null)
				throw new ArgumentNullException("window");
			if (callback == null)
				throw new ArgumentNullException("callback");

			return Instance.BeginInputTransaction(window.MozInstance, callback);
		}

		/// <summary>
		/// Dispatches compositionstart event explicitly.
		/// IME does NOT need to call this typically since compositionstart event
		/// is automatically dispatched by sendPendingComposition() if
		/// compositionstart event hasn&apos;t been dispatched yet.  If this is called
		/// when compositionstart has already been dispatched, this throws an
		/// exception.
		/// </summary>
		/// <param name="keyboardEvent">
		/// Key event which causes starting composition.
		/// If its type value is &quot;keydown&quot;, this method
		/// dispatches only keydown event first.  Otherwise,
		/// dispatches keydown first and keyup at last.
		/// </param>
		/// <returns>
		/// Returns true if composition starts normally.
		/// Otherwise, returns false because it might be
		/// canceled by the web application.
		/// </returns>
		public bool StartComposition(GeckoKeyboardEvent keyboardEvent)
		{
			if(keyboardEvent == null)
				return Instance.StartComposition(null, 0, 0); 
			return Instance.StartComposition(keyboardEvent.Instance, 0, 1);
		}

		/// <summary>
		/// Dispatches compositionstart event explicitly.
		/// IME does NOT need to call this typically since compositionstart event
		/// is automatically dispatched by sendPendingComposition() if
		/// compositionstart event hasn&apos;t been dispatched yet.  If this is called
		/// when compositionstart has already been dispatched, this throws an
		/// exception.
		/// </summary>
		/// <param name="keyboardEvent">
		/// Key event which causes starting composition.
		/// If its type value is &quot;keydown&quot;, this method
		/// dispatches only keydown event first.  Otherwise,
		/// dispatches keydown first and keyup at last.
		/// </param>
		/// <param name="keyFlags">
		/// See KEY_* constants.
		/// </param>
		/// <returns>
		/// Returns true if composition starts normally.
		/// Otherwise, returns false because it might be
		/// canceled by the web application.
		/// </returns>
		public bool StartComposition(GeckoKeyboardEvent keyboardEvent, uint keyFlags)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("event");

			return Instance.StartComposition(keyboardEvent.Instance, keyFlags, 2);
		}

		/// <summary>
		/// Set new composition string. Pending composition will be flushed by
		/// a call of flushPendingComposition(). However, if the new composition
		/// string isn&apos;t empty, you need to call appendClauseToPendingComposition() to
		/// fill all characters of compositionString with one or more clauses before flushing.
		/// Note that if you need to commit or cancel composition, use
		/// commitComposition(), commitCompositionWith() or cancelComposition().
		/// </summary>
		/// <param name="compositionString">New pending composition string.</param>
		/// <remarks>This must be called before every call of flushPendingComposition().</remarks>
		public void SetPendingCompositionString(string compositionString)
		{
			nsString.Set(Instance.SetPendingCompositionString, compositionString);
		}

		/// <summary>
		/// Appends a clause to the pending composition string which is set
		/// by setPendingCompositionString(). Sum of length must be same as
		/// the length of compositionString of setPendingCompositionString().
		/// </summary>
		/// <param name="length">
		/// The length of appending clause.
		/// </param>
		/// <param name="attribute">
		/// The attribute of appending clause. Must be one of ATTR_* constants.
		/// </param>
		public void AppendClauseToPendingComposition(int length, uint attribute)
		{
			if (length < 0)
				throw new ArgumentOutOfRangeException("length");
			Instance.AppendClauseToPendingComposition((uint)length, attribute);
		}

		/// <summary>
		/// Sets caret position in the composition string set by setPendingCompositionString().
		/// This is optional, i.e., you may not call this before flushPendingComposition().
		/// </summary>
		/// <param name="offset">
		/// Caret offset in the composition string. This value must be between 0 and
		/// the length of compositionString of setPendingCompositionString().
		/// </param>
		public void SetCaretInPendingComposition(int offset)
		{
			if (offset < 0)
				throw new ArgumentOutOfRangeException("offset");
			Instance.SetCaretInPendingComposition((uint)offset);
		}

		/// <summary>
		/// Flushes the pending composition which are set by setPendingCompositionString(),
		/// appendClauseToPendingComposition(), and setCaretInPendingComposition() on
		/// the focused editor.
		/// </summary>
		/// <param name="keyboardEvent">
		/// When the modifying composition is caused by a key operation, this should be specified.
		/// If this is specified, keyboard events may be dispatched when Gecko thinks that they
		/// should be.
		/// </param>
		/// <returns>
		/// When there is no composition which was created by the instance and if compositionstart
		/// is consumed by web contents, i.e., failed to start new composition, this returns false.
		/// Otherwise, it returns true.
		/// </returns>
		public bool FlushPendingComposition(GeckoKeyboardEvent keyboardEvent)
		{
			if (keyboardEvent == null)
			{
				Instance.FlushPendingComposition(null, 0, 0);
			}
			return Instance.FlushPendingComposition(keyboardEvent.Instance, 0, 1);
		}

		/// <summary>
		/// Flushes the pending composition which are set by setPendingCompositionString(),
		/// appendClauseToPendingComposition(), and setCaretInPendingComposition() on
		/// the focused editor.
		/// </summary>
		/// <param name="keyboardEvent">
		/// When the modifying composition is caused by a key operation, this should be specified.
		/// If this is specified, keyboard events may be dispatched when Gecko thinks that they
		/// should be.
		/// </param>
		/// <param name="keyFlags">
		/// See KEY_* constants. If this is not specified, the value is assumed as 0.
		/// </param>
		/// <returns>
		/// When there is no composition which was created by the instance and if compositionstart
		/// is consumed by web contents, i.e., failed to start new composition, this returns false.
		/// Otherwise, it returns true.
		/// </returns>
		public bool FlushPendingComposition(GeckoKeyboardEvent keyboardEvent, uint keyFlags)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("keyboardEvent");

			return Instance.FlushPendingComposition(keyboardEvent.Instance, keyFlags, 2);
		}

		/// <summary>
		/// Commits the composition with the last composition string. If you call this
		/// when there is no composition, this throws an exception.
		/// </summary>
		/// <param name="keyboardEvent">
		/// When the committing composition is caused by a key operation, this should be specified.
		/// If this is specified, keyboard events may be dispatched when Gecko thinks that they should be.
		/// </param>
		public void CommitComposition(GeckoKeyboardEvent keyboardEvent)
		{
			if (keyboardEvent == null)
			{
				Instance.CommitComposition(null, 0, 0);
				return;
			}
			Instance.CommitComposition(keyboardEvent.Instance, 0, 1);
		}

		/// <summary>
		/// Commits the composition with the last composition string. If you call this
		/// when there is no composition, this throws an exception.
		/// </summary>
		/// <param name="keyboardEvent">
		/// When the committing composition is caused by a key operation, this should be specified.
		/// If this is specified, keyboard events may be dispatched when Gecko thinks that they should be.
		/// </param>
		/// <param name="keyFlags">
		/// See KEY_* constants. If this is not specified, the value is assumed as 0.
		/// </param>
		public void CommitComposition(GeckoKeyboardEvent keyboardEvent, uint keyFlags)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("keyboardEvent");

			Instance.CommitComposition(keyboardEvent.Instance, keyFlags, 2);
		}

		/// <summary>
		/// Commits the composition with the specified string. If you call this when there
		/// is no composition, this starts composition automatically and commits the
		/// composition with specified string immediately.
		/// </summary>
		/// <param name="commitString">
		/// The commit string.
		/// </param>
		/// <param name="keyboardEvent">
		/// When the committing composition is caused by a key operation, this should be
		/// specified. If this is specified, keyboard events may be dispatched when Gecko
		/// thinks that they should be.
		/// </param>
		/// <returns>
		/// If this couldn't insert the string when there is no composition (e.g., compositionstart
		/// is consumed by the web contents), it returns false. The other cases return true.
		/// </returns>
		public bool CommitCompositionWith(string commitString, GeckoKeyboardEvent keyboardEvent)
		{
			if (commitString == null)
				throw new ArgumentNullException("commitString");

			using (var aCommitString = new nsAString(commitString))
			{
				if (keyboardEvent == null)
				{
					return Instance.CommitCompositionWith(aCommitString, null, 0, 1);
				}
				return Instance.CommitCompositionWith(aCommitString, keyboardEvent.Instance, 0, 2);
			}
		}

		/// <summary>
		/// Commits the composition with the specified string. If you call this when there
		/// is no composition, this starts composition automatically and commits the
		/// composition with specified string immediately.
		/// </summary>
		/// <param name="commitString">
		/// The commit string.
		/// </param>
		/// <param name="keyboardEvent">
		/// When the committing composition is caused by a key operation, this should be
		/// specified. If this is specified, keyboard events may be dispatched when Gecko
		/// thinks that they should be.
		/// </param>
		/// <param name="keyFlags">
		/// See KEY_* constants. If this is not specified, the value is assumed as 0.
		/// </param>
		/// <returns>
		/// If this couldn't insert the string when there is no composition (e.g., compositionstart
		/// is consumed by the web contents), it returns false. The other cases return true.
		/// </returns>
		public void CommitCompositionWith(string commitString, GeckoKeyboardEvent keyboardEvent, uint keyFlags)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("keyboardEvent");

			using (var aCommitString = new nsAString(commitString))
			{
				Instance.CommitCompositionWith(aCommitString, keyboardEvent.Instance, keyFlags, 3);
			}
		}

		/// <summary>
		/// Cancels the composition which was created by the instance. If you call this
		/// when there is no composition, this throws an exception.
		/// </summary>
		/// <param name="keyboardEvent">
		/// When the committing composition is caused by a key operation, this should be
		/// specified. If this is specified, keyboard events may be dispatched when Gecko
		/// thinks that they should be.
		/// </param>
		public void CancelComposition(GeckoKeyboardEvent keyboardEvent)
		{
			if (keyboardEvent == null)
			{
				Instance.CancelComposition(null, 0, 0);
			}
			else
			{
				Instance.CancelComposition(keyboardEvent.Instance, 0, 1);
			}
		}

		/// <summary>
		/// Cancels the composition which was created by the instance. If you call this
		/// when there is no composition, this throws an exception.
		/// </summary>
		/// <param name="keyboardEvent">
		/// When the committing composition is caused by a key operation, this should be
		/// specified. If this is specified, keyboard events may be dispatched when Gecko
		/// thinks that they should be.
		/// </param>
		/// <param name="keyFlags">
		/// See KEY_* constants. If this is not specified, the value is assumed as 0.
		/// </param>
		public void CancelComposition(GeckoKeyboardEvent keyboardEvent, uint keyFlags)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("keyboardEvent");

			Instance.CancelComposition(keyboardEvent.Instance, keyFlags, 2);
		}


		/// <summary>
		/// Dispatches a keydown event and some keypress event when all of
		/// or some of them are necessary.
		/// </summary>
		/// <param name="keyboardEvent">
		/// The KeyboardEvent which is initialized with its constructor.
		/// </param>
		/// <returns>
		/// KEYEVENT_NOT_CONSUMED, if the keydown event nor
		/// the following keypress event(s) are consumed.
		/// KEYDOWN_IS_CONSUMED, if the keydown event is
		/// consumed. No keypress event will be dispatched in
		/// this case.
		/// KEYPRESS_IS_CONSUMED, if the keypress event(s) is
		/// consumed when dispatched.
		/// Note that keypress event is always consumed by
		/// native code for the printable keys (indicating the
		/// default action has been taken).
		/// </returns>
		public uint KeyDown(GeckoKeyboardEvent keyboardEvent)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("keyboardEvent");

			return Instance.Keydown(keyboardEvent.Instance, 0, 1);
		}

		/// <summary>
		/// Dispatches a keydown event and some keypress event when all of
		/// or some of them are necessary.
		/// </summary>
		/// <param name="keyboardEvent">
		/// The GeckoKeyboardEvent which is initialized with its constructor.
		/// </param>
		/// <param name="keyFlags">
		/// See KEY_* constants. If this is not specified, the value is assumed as 0.
		/// </param>
		/// <returns>
		/// KEYEVENT_NOT_CONSUMED, if the keydown event nor
		/// the following keypress event(s) are consumed.
		/// KEYDOWN_IS_CONSUMED, if the keydown event is
		/// consumed. No keypress event will be dispatched in
		/// this case.
		/// KEYPRESS_IS_CONSUMED, if the keypress event(s) is
		/// consumed when dispatched.
		/// Note that keypress event is always consumed by
		/// native code for the printable keys (indicating the
		/// default action has been taken).
		/// </returns>
		public uint KeyDown(GeckoKeyboardEvent keyboardEvent, uint keyFlags)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("keyboardEvent");

			return Instance.Keydown(keyboardEvent.Instance, keyFlags, 2);
		}

		/// <summary>
		/// Dispatches a keyup event when it's necessary.
		/// </summary>
		/// <param name="keyboardEvent">
		/// The GeckoKeyboardEvent which is initialized with its constructor.
		/// </param>
		/// <returns>
		/// Returns true if keyup event isn't consumed by a call of preventDefault().
		/// </returns>
		public bool KeyUp(GeckoKeyboardEvent keyboardEvent)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("keyboardEvent");

			return Instance.Keyup(keyboardEvent.Instance, 0, 1);
		}

		/// <summary>
		/// Dispatches a keyup event when it's necessary.
		/// </summary>
		/// <param name="keyboardEvent">
		/// The GeckoKeyboardEvent which is initialized with its constructor.
		/// </param>
		/// <param name="keyFlags">
		/// See KEY_* constants. If this is not specified, the value is assumed as 0.
		/// </param>
		/// <returns>
		/// Returns true if keyup event isn't consumed by a call of preventDefault().
		/// </returns>
		public bool KeyUp(GeckoKeyboardEvent keyboardEvent, uint keyFlags)
		{
			if (keyboardEvent == null)
				throw new ArgumentNullException("keyboardEvent");

			return Instance.Keyup(keyboardEvent.Instance, keyFlags, 2);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modifierKeyName">
		/// A modifier key name which is defined by DOM Level 3 KeyboardEvents key Values.
		/// </param>
		/// <returns>
		/// Returns true if the modifier state is active. Otherwise, false.
		/// </returns>
		public bool GetModifierState(string modifierKeyName)
		{
			if (modifierKeyName == null)
				throw new ArgumentNullException("modifierKeyName");

			return nsString.Pass(Instance.GetModifierState, modifierKeyName);
		}

		/// <summary>
		/// Shares modifier state of another instance. After sharing modifier state
		/// between two or more instances, modifying modifier state on of this affects
		/// other instances. Therefore, this is useful if you want to create
		/// TextInputProcessor instance in every DOM window or something.
		/// </summary>
		/// <param name="textInputProcessor">
		/// Another instance. If this is null, the instance stops sharing modifier
		/// state with other instances and clears all modifier state.
		/// </param>
		public void ShareModifierStateOf(GeckoTextInputProcessor textInputProcessor)
		{
			Instance.ShareModifierStateOf(textInputProcessor != null ? textInputProcessor.Instance : null);
		}
	}
}
