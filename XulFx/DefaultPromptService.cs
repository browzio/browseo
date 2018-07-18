#region ***** BEGIN LICENSE BLOCK *****
/* Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Skybound Software code.
 *
 * The Initial Developer of the Original Code is Skybound Software.
 * Portions created by the Initial Developer are Copyright (C) 2008-2009
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 */
#endregion END LICENSE BLOCK

using Gecko.Interfaces;
using Gecko.Services;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko
{
	[ContractID(Contracts.PromptService)]
	sealed class PromptServiceFactory
		: BaseNsFactory<PromptServiceFactory>, nsIFactory
	{
		public IntPtr CreateInstance(nsISupports aOuter, ref Guid iid)
		{
			IntPtr result = IntPtr.Zero;
			IntPtr iUnknownForObject = Marshal.GetIUnknownForObject(DefaultPromptFactory.PromptGetter());
			try
			{
				Marshal.ThrowExceptionForHR(Marshal.QueryInterface(iUnknownForObject, ref iid, out result));
			}
			finally
			{
				Marshal.Release(iUnknownForObject);
			}
			return result;
		}

		public void LockFactory(bool @lock)
		{

		}
	}

	[ContractID(Contracts.Prompter)]
	sealed class PromptFactoryFactory
		: GenericOneClassNsFactory<PromptFactoryFactory, DefaultPromptFactory>
	{

	}

	/// <summary>
	/// A wrapper of XULRunner's default nsIPromptFactory implementation, i.e. @mozilla.org/prompter;1
	/// </summary>
	public sealed class DefaultPromptFactory : nsIPromptFactory
	{
		private static nsIPromptFactory _nativePromptFactory;
		private static nsIPromptService _nativePromptService;
		private static DefaultPromptService _defaultPromptService;
		private static Func<DefaultPromptService> _promptGetter;

        public static void Init()
		{
			if (_nativePromptFactory == null)
				_nativePromptFactory = Xpcom.GetService<nsIPromptFactory>(Contracts.Prompter);
			if (_nativePromptService == null)
				_nativePromptService = Xpcom.GetService<nsIPromptService>(Contracts.PromptService);
			if (_defaultPromptService == null)
				_defaultPromptService = new DefaultPromptService();
			PromptFactoryFactory.Register();
			PromptServiceFactory.Register();
		}

		/// <summary>
		/// Wrapper of nsIPromptFactory.GetPrompt(nsIDOMWindow aParent, ref Guid iid)
		/// </summary>
		/// <typeparam name="TPrompt">prompt type, may be nsIPrompt, nsIAuthPrompt, or nsIAuthPrompt2</typeparam>
		/// <param name="aParent">window object</param>
		/// <returns></returns>
		public static TPrompt GetPrompt<TPrompt>(mozIDOMWindowProxy aParent = null)
			where TPrompt : class
		{
			var iid = (GuidAttribute)typeof(TPrompt).GetCustomAttributes(typeof(GuidAttribute), false)[0];
			var g = new Guid(iid.Value);
			var ptr = _nativePromptFactory.GetPrompt(aParent, ref g);
			TPrompt prompt = null;
			if (ptr == IntPtr.Zero)
				Marshal.ThrowExceptionForHR(GeckoError.NS_ERROR_NOT_AVAILABLE);

			try
			{
				prompt = (TPrompt)Marshal.GetTypedObjectForIUnknown(ptr, typeof(TPrompt));
			}
			finally
			{
				Marshal.Release(ptr);
			}
			return prompt;
		}

		public static nsIPromptService NativePromptService
		{
			get { return _nativePromptService; }
		}

		public static Func<DefaultPromptService> PromptGetter
		{
			get { return _promptGetter ?? new Func<DefaultPromptService>(() => _defaultPromptService); }
			set { _promptGetter = value; }
		}

		public IntPtr GetPrompt(mozIDOMWindowProxy aParent, ref Guid iid)
		{
			IntPtr result = IntPtr.Zero;
			IntPtr iUnknownForObject = Marshal.GetIUnknownForObject(DefaultPromptFactory.PromptGetter());
			int hresult = Marshal.QueryInterface(iUnknownForObject, ref iid, out result);
			Marshal.Release(iUnknownForObject);
			Marshal.ThrowExceptionForHR(hresult);
			return result;
		}
	}

	public class DefaultPromptService : nsIPrompt, nsIAuthPrompt2, nsIAuthPrompt, nsIPromptService2
	{
		public virtual void Alert(string dialogTitle, string text)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				prompt.Alert(dialogTitle, text);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual void AlertCheck(string dialogTitle, string text, string checkMsg, ref bool checkValue)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				prompt.AlertCheck(dialogTitle, text, checkMsg, ref checkValue);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool Confirm(string dialogTitle, string text)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				return prompt.Confirm(dialogTitle, text);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool ConfirmCheck(string dialogTitle, string text, string checkMsg, ref bool checkValue)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				return prompt.ConfirmCheck(dialogTitle, text, checkMsg, ref checkValue);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual int ConfirmEx(string dialogTitle, string text, uint buttonFlags, string button0Title, string button1Title, string button2Title, string checkMsg, ref bool checkValue)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				return prompt.ConfirmEx(dialogTitle, text, buttonFlags, button0Title, button1Title, button2Title, checkMsg, ref checkValue);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool Prompt(string dialogTitle, string text, ref string value, string checkMsg, ref bool checkValue)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				return prompt.Prompt(dialogTitle, text, ref value, checkMsg, ref checkValue);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool PromptPassword(string dialogTitle, string text, ref string password, string checkMsg, ref bool checkValue)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				return prompt.PromptPassword(dialogTitle, text, ref password, checkMsg, ref checkValue);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool PromptUsernameAndPassword(string dialogTitle, string text, ref string username, ref string password, string checkMsg, ref bool checkValue)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				return prompt.PromptUsernameAndPassword(dialogTitle, text, ref username, ref password, checkMsg, ref checkValue);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool Select(string dialogTitle, string text, uint count, IntPtr[] selectList, out int outSelection)
		{
			nsIPrompt prompt = DefaultPromptFactory.GetPrompt<nsIPrompt>();
			try
			{
				return prompt.Select(dialogTitle, text, count, selectList, out outSelection);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool PromptAuth(nsIChannel aChannel, uint level, nsIAuthInformation authInfo)
		{
			nsIAuthPrompt2 prompt = DefaultPromptFactory.GetPrompt<nsIAuthPrompt2>();
			try
			{
				return prompt.PromptAuth(aChannel, level, authInfo);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual nsICancelable AsyncPromptAuth(nsIChannel aChannel, nsIAuthPromptCallback aCallback, nsISupports aContext, uint level, nsIAuthInformation authInfo)
		{
			nsIAuthPrompt2 prompt = DefaultPromptFactory.GetPrompt<nsIAuthPrompt2>();
			try
			{
				return prompt.AsyncPromptAuth(aChannel, aCallback, aContext, level, authInfo);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool Prompt(string dialogTitle, string text, string passwordRealm, uint savePassword, string defaultText, out string result)
		{
			nsIAuthPrompt prompt = DefaultPromptFactory.GetPrompt<nsIAuthPrompt>();
			try
			{
				return prompt.Prompt(dialogTitle, text, passwordRealm, savePassword, defaultText, out result);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool PromptUsernameAndPassword(string dialogTitle, string text, string passwordRealm, uint savePassword, ref string user, ref string pwd)
		{
			nsIAuthPrompt prompt = DefaultPromptFactory.GetPrompt<nsIAuthPrompt>();
			try
			{
				return prompt.PromptUsernameAndPassword(dialogTitle, text, passwordRealm, savePassword, ref user, ref pwd);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		public virtual bool PromptPassword(string dialogTitle, string text, string passwordRealm, uint savePassword, ref string pwd)
		{
			nsIAuthPrompt prompt = DefaultPromptFactory.GetPrompt<nsIAuthPrompt>();
			try
			{
				return prompt.PromptPassword(dialogTitle, text, passwordRealm, savePassword, ref pwd);
			}
			finally
			{
				Xpcom.FreeComObject(ref prompt);
			}
		}

		#region nsIPromptService2

		public virtual void Alert(mozIDOMWindowProxy aParent, string aDialogTitle, string aText)
		{
			Alert(aDialogTitle, aText);
		}

		public virtual void AlertCheck(mozIDOMWindowProxy aParent, string aDialogTitle, string aText, string aCheckMsg, ref bool aCheckState)
		{
			AlertCheck(aDialogTitle, aText, aCheckMsg, ref aCheckState);
		}

		public virtual bool Confirm(mozIDOMWindowProxy aParent, string aDialogTitle, string aText)
		{
			return Confirm(aDialogTitle, aText);
		}

		public virtual bool ConfirmCheck(mozIDOMWindowProxy aParent, string aDialogTitle, string aText, string aCheckMsg, ref bool aCheckState)
		{
			return ConfirmCheck(aDialogTitle, aText, aCheckMsg, ref aCheckState);
		}

		public virtual int ConfirmEx(mozIDOMWindowProxy aParent, string aDialogTitle, string aText, uint aButtonFlags, string aButton0Title, string aButton1Title, string aButton2Title, string aCheckMsg, ref bool aCheckState)
		{
			return ConfirmEx(aDialogTitle, aText, aButtonFlags, aButton0Title, aButton1Title, aButton2Title, aCheckMsg, ref aCheckState);
		}

		public virtual bool Prompt(mozIDOMWindowProxy aParent, string aDialogTitle, string aText, ref string aValue, string aCheckMsg, ref bool aCheckState)
		{
			return Prompt(aDialogTitle, aText, ref aValue, aCheckMsg, ref aCheckState);
		}

		public virtual bool PromptUsernameAndPassword(mozIDOMWindowProxy aParent, string aDialogTitle, string aText, ref string aUsername, ref string aPassword, string aCheckMsg, ref bool aCheckState)
		{
			return PromptUsernameAndPassword(aDialogTitle, aText, ref aUsername, ref aPassword, aCheckMsg, ref aCheckState);
		}

		public virtual bool PromptPassword(mozIDOMWindowProxy aParent, string aDialogTitle, string aText, ref string aPassword, string aCheckMsg, ref bool aCheckState)
		{
			return PromptPassword(aDialogTitle, aText, ref aPassword, aCheckMsg, ref aCheckState);
		}

		public virtual bool Select(mozIDOMWindowProxy aParent, string aDialogTitle, string aText, uint aCount, IntPtr[] aSelectList, out int aOutSelection)
		{
			return Select(aDialogTitle, aText, aCount, aSelectList, out aOutSelection);
		}

		public virtual bool PromptAuth(mozIDOMWindowProxy aParent, nsIChannel aChannel, uint level, nsIAuthInformation authInfo, string checkboxLabel, ref bool checkValue)
		{
			return PromptAuth(aChannel, level, authInfo);
		}

		public virtual nsICancelable AsyncPromptAuth(mozIDOMWindowProxy aParent, nsIChannel aChannel, nsIAuthPromptCallback aCallback, nsISupports aContext, uint level, nsIAuthInformation authInfo, string checkboxLabel, ref bool checkValue)
		{
			return AsyncPromptAuth(aChannel, aCallback, aContext, level, authInfo);
		}

		#endregion
	}
}