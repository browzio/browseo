using System;

namespace Gecko
{
	public interface IEventedWebView : IWebView
	{
		event EventHandler<GeckoNavigatingEventArgs> Navigating;
		event EventHandler<GeckoNavigatedEventArgs> Navigated;
		event EventHandler<GeckoDocumentCompletedEventArgs> DocumentCompleted;
	}
}
