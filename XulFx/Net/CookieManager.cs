using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gecko.Net
{
	public sealed class CookieManager : ComObject<nsICookieManager2>, IGeckoObjectWrapper, ICollection<Cookie>
	{
		private nsIXulfxCookieManager _helper;

		public static CookieManager GetService()
		{
			return Xpcom.GetService<nsICookieManager2>(Contracts.CookieManager).Wrap(CookieManager.Create);
		}

		private static CookieManager Create(nsICookieManager2 instance)
		{
			return new CookieManager(instance);
		}

		private CookieManager(nsICookieManager2 instance)
			: base(instance)
		{

		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _helper);
			base.Dispose(disposing);
		}

		private nsIXulfxCookieManager Helper
		{
			get
			{
				if (_helper == null)
				{
					if (this.Instance == null)
						throw new ObjectDisposedException(this.GetType().Name);
					_helper = Xpcom.GetService<nsIXulfxCookieManager>(Contracts.XulfxCookieManager);
				}
				return _helper;
			}
		}

		public void Add(string host, string path, string name, string value, bool isSecure, bool isHttpOnly, bool isSession, DateTime expiry)
		{
			using (nsAUTF8String aHost = new nsAUTF8String(host), aPath = new nsAUTF8String(path))
			{
				using (nsACString aName = new nsACString(name), aValue = new nsACString(value))
				{
					this.Helper.Add(aHost, aPath, aName, aValue, isSecure, isHttpOnly, isSession, (long)Utils.ToSecondsSinceEpoch(expiry));
				}
			}
		}



		public int CountCookiesFromHost(string host)
		{
			// int is big for cookie count :)

			using (var aHost = new nsAUTF8String(host))
			{
				return (int)Instance.CountCookiesFromHost(aHost);
			}
		}

		public IEnumerable<Cookie> GetCookiesFromHost(string host)
		{
			using(var aHost = new nsAUTF8String(host))
			{
				using (var enumerator = new GeckoSimpleEnumerator<nsICookie2, Cookie>(Helper.GetCookiesFromHost(aHost), Cookie.Create))
				{
					while (enumerator.MoveNext())
						yield return enumerator.Current;
				}
			}
			
		}

		public void ImportCookies(string filename)
		{
			if (!File.Exists(filename))
				throw new FileNotFoundException(filename);

			using (var file = Xpcom.OpenFile(filename))
			{
				Instance.ImportCookies(file.Instance);
			}
		}

		public void Remove(string host, string name, string path, bool blocked)
		{
			using (nsAUTF8String aHost = new nsAUTF8String(host), aPath = new nsAUTF8String(path))
			{
				using (var aName = new nsACString(name))
				{
					Helper.Remove(aHost, aName, aPath, blocked);
				}
			}
		}

		public void RemoveAll()
		{
			this.Clear();
		}



		public IEnumerator<Cookie> GetEnumerator()
		{
			return new GeckoSimpleEnumerator<nsICookie2, Cookie>(Instance.GetEnumeratorAttribute(), Cookie.Create);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void Add(Cookie item)
		{
			Add(item.Host, item.Path, item.Name, item.Value, item.IsSecure, item.IsHttpOnly, item.IsSession, item.Expiry);
		}

		public void Clear()
		{
			Instance.RemoveAll();
		}

		public bool Contains(Cookie item)
		{
			return Helper.CookieExists(item.Instance);
		}

		public void CopyTo(Cookie[] array, int destIndex)
		{
			if (array == null)
				throw new ArgumentNullException("array");

			var buffer = new Cookie[array.Length - destIndex];
			int i = 0;
			foreach(Cookie item in this)
			{
				if(buffer.Length <= i)
					throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");
				buffer[i++] = item;
			}

			Array.Copy(buffer, 0, array, destIndex, buffer.Length);
		}

		public int Count
		{
			get
			{
				int count = 0;
				nsISimpleEnumerator enumerator = Instance.GetEnumeratorAttribute();
				try
				{
					while (enumerator.HasMoreElements())
					{
						count++;
						nsISupports obj = enumerator.GetNext();
						Xpcom.FreeComObject(ref obj);
					}
				}
				finally
				{
					Xpcom.FreeComObject(ref enumerator);
				}
				return count;
			}
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(Cookie item)
		{
			if (this.Contains(item))
			{
				this.Remove(item.Host, item.Name, item.Path, false);
				return true;
			}
			return false;
		}
	}
}
