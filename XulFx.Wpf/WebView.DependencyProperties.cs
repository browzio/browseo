using System;
using System.Windows;
using Gecko;

namespace Gecko.Windows
{
	partial class WebView
	{
		#region IWebView

		private static readonly DependencyPropertyKey IsBusyPropertyKey = DependencyProperty.RegisterReadOnly("IsBusy", typeof(bool), typeof(WebView), new PropertyMetadata(false));
		public static readonly DependencyProperty IsBusyProperty = IsBusyPropertyKey.DependencyProperty;
		public virtual bool IsBusy
		{
			get { return (bool)GetValue(IsBusyProperty); }
			private set { SetValue(IsBusyPropertyKey, value); }
		}

		private static readonly DependencyPropertyKey CanGoBackPropertyKey = DependencyProperty.RegisterReadOnly("CanGoBack", typeof(bool), typeof(WebView), new PropertyMetadata(false));
		public static readonly DependencyProperty CanGoBackProperty = CanGoBackPropertyKey.DependencyProperty;
		public virtual bool CanGoBack
		{
			get { return (bool)GetValue(CanGoBackProperty); }
			private set { SetValue(CanGoBackPropertyKey, value); }
		}

		private static readonly DependencyPropertyKey CanGoForwardPropertyKey = DependencyProperty.RegisterReadOnly("CanGoForward", typeof(bool), typeof(WebView), new PropertyMetadata(false));
		public static readonly DependencyProperty CanGoForwardProperty = CanGoForwardPropertyKey.DependencyProperty;
		public virtual bool CanGoForward
		{
			get { return (bool)GetValue(CanGoForwardProperty); }
			private set { SetValue(CanGoForwardPropertyKey, value); }
		}

		private static readonly DependencyPropertyKey StatusTextPropertyKey = DependencyProperty.RegisterReadOnly("StatusText", typeof(string), typeof(WebView), new PropertyMetadata(string.Empty));
		public static readonly DependencyProperty StatusTextProperty = StatusTextPropertyKey.DependencyProperty;
		public virtual string StatusText
		{
			get { return (string)GetValue(StatusTextProperty); }
			private set { SetValue(StatusTextPropertyKey, value); }
		}

		public static readonly DependencyProperty UrlProperty = DependencyProperty.Register("Url", typeof(Uri), typeof(WebView), new PropertyMetadata(new Uri("about:blank"), OnUrlPropertyChanged));
		private bool _dontCallNavigateOnUrlChanged;
		public Uri Url
		{
			get { return (Uri)GetValue(UrlProperty); }
			set { SetValue(UrlProperty, value); }
		}

		private static void OnUrlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WebView webView = ((WebView)d);
			if (!webView._dontCallNavigateOnUrlChanged)
				webView.Navigate(((Uri)e.NewValue) ?? new Uri("about:blank"));
		}

		#endregion IWebView

	}
}
