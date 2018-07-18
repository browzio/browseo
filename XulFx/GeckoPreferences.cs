using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using System.Linq;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Runtime.InteropServices;
using Gecko;

namespace Old.Gecko
{
	/// <summary>
	/// Provides access to Gecko preferences.
	/// </summary>
	public sealed class GeckoPreferences : ComObject<nsIPrefBranch>
	{
		private static ComObject<nsIPrefService> _prefService;
		private static GeckoPreferences _userBranch;
		private static GeckoPreferences _defaultBranch;

		#region static members

		public static ComObject<nsIPrefService> PrefService
		{
			get { return _prefService ?? (_prefService = new ComObject<nsIPrefService>(Xpcom.GetService<nsIPrefService>(Contracts.PreferencesService))); }
		}

		/// <summary>
		/// Gets the preferences defined for the current user.
		/// </summary>
		public static GeckoPreferences User
		{
			get { return _userBranch ?? (_userBranch = new GeckoPreferences(PrefService.Instance.GetBranch(""))); }
		}

		/// <summary>
		/// Gets the set of preferences used as defaults when no user preference is set.
		/// </summary>
		public static GeckoPreferences Default
		{
			get { return _defaultBranch ?? (_defaultBranch = new GeckoPreferences(PrefService.Instance.GetDefaultBranch(""))); }
		}

		#endregion static members

		


		private GeckoPreferences(nsIPrefBranch prefBranch)
			: base(prefBranch)
		{

		}		

		/// <summary>
		/// Reads all User preferences from the specified file.
		/// </summary>
		/// <param name="filename">Required. The name of the file from which preferences are read.  May not be null.</param>
		public static void Load(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				throw new ArgumentException("filename");

			else if (!File.Exists(filename))
				throw new FileNotFoundException();
			using (var file = Xpcom.OpenFile(filename))
			{
				PrefService.Instance.ReadUserPrefs(file.Instance);
			}
		}

		/// <summary>
		/// Saves all User preferences to the specified file.
		/// </summary>
		/// <param name="filename">Required. The name of the file to which preferences are saved.  May not be null.</param>
		public static void Save(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				throw new ArgumentException("filename");
			using (var file = Xpcom.OpenFile(filename))
			{
				PrefService.Instance.SavePrefFile(file.Instance);
			}
		}

		/// <summary>
		/// Resets all preferences to their default values.
		/// </summary>
		public void Reset()
		{
			if (this == _defaultBranch)
				PrefService.Instance.ResetPrefs();
			else
				PrefService.Instance.ResetUserPrefs();
		}

		const int PREF_INVALID = 0;
		const int PREF_STRING = 32;
		const int PREF_INT = 64;
		const int PREF_BOOL = 128;

		/// <summary>
		/// Gets or sets the preference with the given name.
		/// </summary>
		/// <param name="prefName">Required. The name of the preference to get or set.</param>
		/// <returns></returns>
		public object this[string prefName]
		{
			get
			{
				int type = Instance.GetPrefType(prefName);
				switch (type)
				{
					case PREF_INVALID:
						return null;
					case PREF_STRING:
						object comObject = null;
						nsISupportsString aString = null;
						Guid guid = typeof(nsISupportsString).GUID;
						IntPtr pUnk = Instance.GetComplexValue(prefName, ref guid);
						try
						{
							comObject = Marshal.GetObjectForIUnknown(pUnk);
							aString = Xpcom.QueryInterface<nsISupportsString>(comObject);
							return nsString.Get(aString.GetDataAttribute);
						}
						finally
						{
							Xpcom.FreeComObject(ref comObject);
							Xpcom.FreeComObject(ref aString);
						}
					case PREF_INT:
						return Instance.GetIntPref(prefName);
					case PREF_BOOL: 
						return Instance.GetBoolPref(prefName);
				}
				throw new ArgumentException("prefName");
			}
			set
			{
				if (string.IsNullOrEmpty(prefName))
					throw new ArgumentException("prefName");
				else if (value == null)
					throw new ArgumentNullException("value");

				int existingType = Instance.GetPrefType(prefName);
				int assignedType = GetValueType(value);

				if (existingType != 0 && existingType != assignedType)
				{
					throw new InvalidCastException(string.Format(
						"A {0} value may not be assigned to '{1}' because it is already defined as {2}.",
						value.GetType().Name,
						prefName,
						GetPreferenceType(prefName).Name));
				}
				switch (assignedType)
				{
					case PREF_STRING:
						Guid guid = typeof(nsISupportsString).GUID;
						nsISupports aValueAsIUnknown = null;
						var aValue = Xpcom.CreateInstance<nsISupportsString>(Contracts.SupportsString);
						try
						{
							nsString.Set(aValue.SetDataAttribute, (string)value);
							aValueAsIUnknown = Xpcom.QueryInterface<nsISupports>(aValue);
							Instance.SetComplexValue(prefName, ref guid, aValueAsIUnknown);
						}
						finally
						{
							Xpcom.FreeComObject(ref aValueAsIUnknown);
							Xpcom.FreeComObject(ref aValue);
						}
						break;
					case PREF_INT:
						Instance.SetIntPref(prefName, (int)value);
						break;
					case PREF_BOOL:
						Instance.SetBoolPref(prefName, (bool)value);
						break;
				}
			}
		}

		public void SetLocalizedString(string prefName, string value)
		{
			if (string.IsNullOrEmpty(prefName))
				throw new ArgumentException("prefName");
			else if (value == null)
				throw new ArgumentNullException("value");

			Guid guid = typeof(nsISupportsString).GUID;
			nsISupports aValueAsIUnknown = null;
			var aValue = Xpcom.CreateInstance<nsIPrefLocalizedString>(Contracts.PrefLocalizedString);
			try
			{
				aValue.SetDataAttribute(value);
				aValueAsIUnknown = Xpcom.QueryInterface<nsISupports>(aValue);
				Instance.SetComplexValue(prefName, ref guid, aValueAsIUnknown);
			}
			finally
			{
				Xpcom.FreeComObject(ref aValueAsIUnknown);
				Xpcom.FreeComObject(ref aValue);
			}
		}

		public string GetLocalizedString(string prefName)
		{
			if (string.IsNullOrEmpty(prefName))
				throw new ArgumentException("prefName");

			try
			{
				object comObject = null;
				nsIPrefLocalizedString aString = null;
				Guid guid = typeof(nsIPrefLocalizedString).GUID;
				IntPtr pUnk = Instance.GetComplexValue(prefName, ref guid);
				try
				{
					comObject = Marshal.GetObjectForIUnknown(pUnk);
					aString = Xpcom.QueryInterface<nsIPrefLocalizedString>(comObject);
					return aString.GetDataAttribute();
				}
				finally
				{
					Xpcom.FreeComObject(ref comObject);
					Xpcom.FreeComObject(ref aString);
				}
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode != GeckoError.NS_ERROR_MALFORMED_URI)
					throw;
				return this[prefName] as string;
			}
		}

		int GetValueType(object value)
		{
			if (value is int)
				return PREF_INT;
			else if (value is string)
				return PREF_STRING;
			else if (value is bool)
				return PREF_BOOL;

			throw new ArgumentException("Gecko preferences must be either a String, Int32, or Boolean value.", "prefName");
		}

		/// <summary>
		/// Gets the <see cref="Type"/> of the specified preference.
		/// </summary>
		/// <param name="name">Required. The name of the preference whose type is returned.</param>
		/// <returns></returns>
		public Type GetPreferenceType(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");

			switch (Instance.GetPrefType(name))
			{
				case PREF_STRING: return typeof(string);
				case PREF_INT: return typeof(int);
				case PREF_BOOL: return typeof(bool);
			}
			return null;
		}

		/// <summary>
		/// Sets whether the specified preference is locked. Locking a preference will cause the preference service to always return the default value regardless of whether there is a user set value or not.
		/// </summary>
		/// <param name="name">Required. The preference to lock or unlock.</param>
		/// <param name="locked">True if the preference should be locked; otherwise, false, and the preference is unlocked.</param>
		public void SetLocked(string name, bool locked)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");

			if (locked)
				Instance.LockPref(name);
			else
				Instance.UnlockPref(name);
		}

		/// <summary>
		/// Gets whether the specified preference is locked.
		/// </summary>
		/// <param name="name">Required. The preference whose lock status is returned.</param>
		/// <returns></returns>
		public bool GetLocked(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");

			return Instance.PrefIsLocked(name);
		}

		/// <summary>
		/// Clear user preferences
		/// </summary>
		/// <param name="name">Required. The preference to lock or unlock.</param>
		public void Clear(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");

			Instance.ClearUserPref(name);
		}
	}
}
