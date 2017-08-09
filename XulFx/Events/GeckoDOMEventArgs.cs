using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Gecko
{
	public class GeckoDOMEventArgs : HandledEventArgs, IDisposable, IGeckoObjectWrapper, IComObject
	{
		private nsIDOMEvent _instance;
		private GeckoObjectCache.CacheKey _cacheKey;

		public static GeckoDOMEventArgs Create(nsIDOMEvent instance)
		{
			bool found = true;
			try
			{
				var uiEvent = Xpcom.QueryInterface<nsIDOMUIEvent>(instance);
				if (uiEvent != null)
					return GeckoDOMUIEventArgs.Create(uiEvent);

				found = false;
			}
			finally
			{
				if (found)
					Xpcom.FreeComObject(ref instance);
			}
			switch (nsString.Get(instance.GetTypeAttribute))
			{
				case "hashchange":
					return GeckoDOMHashChangeEventArgs.Create(instance);
			}
			return new GeckoDOMEventArgs(instance);
		}

		protected GeckoDOMEventArgs(nsIDOMEvent instance)
		{
			if (instance == null)
				throw new ArgumentNullException("instance");

			_instance = instance;
			_cacheKey = GeckoObjectCache.Set(instance, this);
		}

		~GeckoDOMEventArgs()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			GeckoObjectCache.Remove(_cacheKey);

			Xpcom.FreeComObject(ref _instance);
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		public void Dispose()
		{
			if (_instance != null)
			{
				Dispose(true);
			}
		}


		public nsIDOMEvent Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _instance;
			}
		}

		object IComObject.NativeInstance
		{
			get { return _instance; }
		}

		Type IComObject.GetComObjectType()
		{
			return typeof(GeckoDOMEventArgs);
		}

		public string Type
		{
			get { return nsString.Get(Instance.GetTypeAttribute); }
		}

		public bool Bubbles
		{
			get { return Instance.GetBubblesAttribute(); }
		}

		public bool Cancelable
		{
			get { return Instance.GetCancelableAttribute(); }
		}

		public bool GetDefaultPrevented
		{
			get { return Instance.GetDefaultPreventedAttribute(); }
		}

		public ulong Timestamp
		{
			get { return Instance.GetTimeStampAttribute(); }
		}

		public EventPhase EventPhase
		{
			get { return (EventPhase)Instance.GetEventPhaseAttribute(); }
		}

		public GeckoDOMEventTarget CurrentTarget
		{
			get { return Instance.GetCurrentTargetAttribute().Wrap(GeckoDOMEventTarget.Create); }
		}

		/// <summary>
		/// Gets the final destination of the event.
		/// </summary>
		public GeckoDOMEventTarget Target
		{
			get { return Instance.GetTargetAttribute().Wrap(GeckoDOMEventTarget.Create); }
		}

		/// <summary>
		/// If an event is cancelable, the preventDefault method is used to
		/// signify that the event is to be canceled, meaning any default action
		/// normally taken by the implementation as a result of the event will
		/// not occur. If, during any stage of event flow, the preventDefault
		/// method is called the event is canceled. Any default action associated
		/// with the event will not occur. Calling this method for a
		/// non-cancelable event has no effect. Once preventDefault has been
		/// called it will remain in effect throughout the remainder of the
		/// event's propagation. This method may be used during any stage of
		/// event flow.
		/// </summary>
		public void PreventDefault()
		{
			Instance.PreventDefault();
		}

		/// <summary>
		/// The stopPropagation method is used prevent further propagation of an
		/// event during event flow. If this method is called by any
		/// EventListener the event will cease propagating through the tree. The
		/// event will complete dispatch to all listeners on the current
		/// EventTarget before event flow stops. This method may be used during
		/// any stage of event flow.
		/// </summary>
		public void StopPropagation()
		{
			Instance.StopPropagation();
		}	

		public void StopImmediatePropagation()
		{
			Instance.StopImmediatePropagation();
		}

	}

}
