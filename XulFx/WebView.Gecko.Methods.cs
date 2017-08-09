using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Gecko.DOM;
using Gecko.Interfaces;
using Gecko.Interop;
using Gecko.IO;

// This file contains shared methods for any GUI (XUL, WPF, WinForms)
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
		protected virtual string BeforeLinkTraversal(string originalTarget, nsIURI linkURI, nsIDOMNode linkNode, bool isAppTab)
		{
			return originalTarget;
		}


		#region IWebView

		/// <summary>
		/// Loads the document at the specified Uniform Resource Locator (URL) into the WebView control, replacing the previous document.
		/// </summary>
		/// <param name="urlString">The URL of the document to load.</param>
		public virtual void Navigate(string urlString)
		{
			AssertNotPreview();
			Navigate(urlString, GeckoLoadFlags.None, null, null, null);
		}

		/// <summary>
		/// Loads the document at the location indicated by the specified Uri into the WebBrowser control, replacing the previous document.
		/// </summary>
		/// <param name="url">The URL of the document to load.</param>
		public virtual void Navigate(Uri url)
		{
			AssertNotPreview();
			Navigate(url.AbsoluteUri, GeckoLoadFlags.None, null, null, null);
		}

		/// <summary>
		/// Reloads the document currently displayed in the WebView control by checking the server for an updated version.
		/// </summary>
		public virtual void Reload()
		{
			Reload(GeckoLoadFlags.None);
		}
		
		/// <summary>
		/// Reloads the document currently displayed in the WebView control using the specified reload flags.
		/// </summary>
		public virtual void Reload(GeckoLoadFlags flags)
		{
			AssertNotPreview();
			WebNav.Reload(flags);
		}

		/// <summary>
		/// Navigates the WebView control to the previous page in the navigation history, if one is available.
		/// </summary>
		/// <returns>true if the navigation succeeds; false if a previous page in the navigation history is not available.</returns>
		public virtual bool GoBack()
		{
			AssertNotPreview();

			try
			{
				GeckoWebNavigation webNav = this.WebNav;
				if (webNav.CanGoBack)
				{
					webNav.GoBack();
					return true;
				}
			}
			catch (COMException) { }
			return false;
		}

		/// <summary>
		/// Navigates the WebView control to the next page in the navigation history, if one is available.
		/// </summary>
		/// <returns>true if the navigation succeeds; false if a subsequent page in the navigation history is not available.</returns>
		public virtual bool GoForward()
		{
			AssertNotPreview();

			try
			{
				GeckoWebNavigation webNav = this.WebNav;
				if (webNav.CanCanGoForward)
				{
					webNav.GoForward();
					return true;
				}
			}
			catch (COMException) { }
			return false;
		}

		/// <summary>
		/// Navigates the WebView control to the home page of the current user.
		/// </summary>
		public virtual void GoHome()
		{
			Navigate("about:blank");
		}

		/// <summary>
		/// Navigates the WebView control to the default search page of the current user.
		/// </summary>
		public void GoSearch()
		{
			Navigate("https://google.com/");
		}

		/// <summary>
		/// Cancels any pending navigation and stops any dynamic page elements, such as background sounds and animations.
		/// </summary>
		public virtual void Stop()
		{
			if (this.WebNav == null)
				return;

			try
			{
				this.WebNav.Stop(nsIWebNavigationConsts.STOP_ALL);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == GeckoError.NS_ERROR_UNEXPECTED)
					return;
				throw;
			}
		}

		#endregion

		public void LoadHtml(string content, string url)
		{
			this.Window.LoadContent(content, url, "text/html");
		}

		/// <summary>
		///  Navigates to the specified URL using the given load flags, referrer and post data
		///  In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
		/// </summary>
		/// <param name="url">The url to navigate to.  If the url is empty or null, the browser does not navigate and the method returns false.</param>
		/// <param name="loadFlags">Flags which specify how the page is loaded.</param>
		/// <param name="referrer">The referring URL, or null.</param>
		/// <param name="postData">post data and headers, or null</param>
		/// <param name="headers">headers, or null</param>
		/// <returns>true if Navigate started. false otherwise.</returns>
		public bool Navigate(string url, GeckoLoadFlags loadFlags, string referrer, MimeInputStream postData, MimeInputStream headers)
		{
			if (string.IsNullOrEmpty(url))
				return false;

			//// added these from http://code.google.com/p/geckofx/issues/detail?id=5 so that it will work even if browser isn't currently shown
			//if (!IsHandleCreated) CreateHandle();
			//if (IsBusy) this.Stop();


			//if (!IsHandleCreated)
			//	throw new InvalidOperationException("Cannot call Navigate() before the window handle is created.");

			//// WebNav.LoadURI throws an exception if we try to open a file that doesn't exist...
			//Uri created;
			//if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out created) && created.IsAbsoluteUri && created.IsFile)
			//{
			//	if (!File.Exists(created.LocalPath) && !Directory.Exists(created.LocalPath))
			//		return false;
			//}

			this.WebNav.LoadURI(url, loadFlags, referrer, postData, headers);
			
			return true;
		}

		/// <summary>
		///  Navigates to the specified URL using the given load flags, referrer and post data
		///  In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
		/// </summary>
		/// <param name="url">The url to navigate to.  If the url is empty or null, the browser does not navigate and the method returns false.</param>
		/// <param name="loadFlags">Flags which specify how the page is loaded.</param>
		/// <param name="referrer">The referring URL, or null.</param>
		/// <param name="referrerPolicy">The referrer policy.</param>
		/// <param name="postData">post data and headers, or null</param>
		/// <param name="headers">headers, or null</param>
		/// <returns>true if Navigate started. false otherwise.</returns>
		public bool Navigate(string url, GeckoLoadFlags loadFlags, string referrer, ReferrerPolicy referrerPolicy, MimeInputStream postData, MimeInputStream headers)
		{
			if (string.IsNullOrEmpty(url))
				return false;

			this.WebNav.LoadURIWithOptions(url, loadFlags, referrer, referrerPolicy, postData, headers, null);

			return true;
		}
	}
}
