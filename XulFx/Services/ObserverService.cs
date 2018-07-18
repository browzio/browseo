using Gecko.Interfaces;
using Gecko.Interop;
using System;

namespace Gecko.Services.Old
{
	public sealed class ObserverService : ComObject<nsIObserverService>, IGeckoObjectWrapper
	{
		public static ObserverService GetService()
		{
			return Xpcom.GetService<nsIObserverService>(Contracts.ObserverService).Wrap(ObserverService.Create);
		}

		private static ObserverService Create(nsIObserverService instance)
		{
			return new ObserverService(instance);
		}

		private ObserverService(nsIObserverService instance)
			: base(instance)
		{

		}


		/// <summary>
		/// Registers a given listener for a notifications regarding the specified topic.
		/// </summary>
		/// <param name="observer">
		/// The interface pointer which will receive notifications.
		/// </param>
		/// <param name="topic">
		/// The notification topic or subject.
		/// </param>
		/// <param name="ownsWeak">
		/// If set to false, the <see cref="Gecko.Interfaces.nsIObserverService"/>
		/// will hold a strong reference to observer.  If set to true and observer
		/// supports the <see cref="Gecko.Interfaces.nsIWeakReference"/> interface,
		/// a weak reference will be held. Otherwise an error will be returned.
		/// </param>
		public void AddObserver(nsIObserver observer, string topic, bool ownsWeak)
		{
			if (observer == null)
				throw new ArgumentNullException("observer");
			if (topic == null)
				throw new ArgumentNullException("topic");

			Instance.AddObserver(observer, topic, ownsWeak);
		}

		/// <summary>
		/// Unregisters a given listener from notifications regarding the specified topic.
		/// </summary>
		/// <param name="observer">
		/// The interface pointer which will stop recieving notifications.
		/// </param>
		/// <param name="topic">
		/// The notification topic or subject.
		/// </param>
		public void RemoveObserver(nsIObserver observer, string topic)
		{
			if (observer == null)
				throw new ArgumentNullException("observer");
			if (topic == null)
				throw new ArgumentNullException("topic");

			Instance.RemoveObserver(observer, topic);
		}


		/// <summary>
		/// Notifies all registered listeners of the given topic.
		/// </summary>
		/// <param name="subject">
		/// Notification specific interface pointer.
		/// </param>
		/// <param name="topic">
		/// The notification topic or subject.
		/// </param>
		/// <param name="someData">
		/// Notification specific wide string.
		/// </param>
		public void NotifyObservers(nsISupports subject, string topic, string someData)
		{
			if (topic == null)
				throw new ArgumentNullException("topic");

			Instance.NotifyObservers(subject, topic, someData);
		}


	}
}
