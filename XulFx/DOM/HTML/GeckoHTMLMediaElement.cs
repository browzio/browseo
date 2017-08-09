using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	public class GeckoHTMLMediaElement : GeckoHTMLElement<nsIDOMHTMLMediaElement>
	{
		protected GeckoHTMLMediaElement(nsIDOMHTMLMediaElement instance)
			: base(instance)
		{

		}

		//public GeckoMediaError Error
		//{
		//	get { return Instance.GetErrorAttribute().Wrap(); }
		//}

		public string Src
		{
			get { return nsString.Get(Instance.GetSrcAttribute); }
			set { nsString.Set(Instance.SetSrcAttribute, value); }
		}

		// public nsIDOMMediaStream mozSrcObject { get; set; }

		public string CurrentSrc
		{
			get { return nsString.Get(Instance.GetCurrentSrcAttribute); }
		}

		public int NetworkState
		{
			get { return Instance.GetNetworkStateAttribute(); }
		}

		public string Preload
		{
			get { return nsString.Get(Instance.GetPreloadAttribute); }
			set { nsString.Set(Instance.SetPreloadAttribute, value); }
		}

		// readonly attribute nsIDOMTimeRanges buffered;

		public void Load()
		{
			Instance.Load();
		}

		public string CanPlayType(string type)
		{
			return nsString.Get(Instance.CanPlayType, type);
		}

		public int ReadyState
		{
			get { return Instance.GetReadyStateAttribute(); }
		}

		public bool Seeking
		{
			get { return Instance.GetSeekingAttribute(); }
		}

		public double CurrentTime
		{
			get { return Instance.GetCurrentTimeAttribute(); }
			set { Instance.SetCurrentTimeAttribute(value); }
		}

		public double Duration
		{
			get { return Instance.GetDurationAttribute(); }
		}

		public bool Paused
		{
			get { return Instance.GetPausedAttribute(); }
		}

		public double DefaultPlaybackRate
		{
			get { return Instance.GetDefaultPlaybackRateAttribute(); }
			set { Instance.SetDefaultPlaybackRateAttribute(value); }
		}

		public double PlaybackRate
		{
			get { return Instance.GetPlaybackRateAttribute(); }
			set { Instance.SetPlaybackRateAttribute(value); }
		}

		// attribute boolean mozPreservesPitch;
		//readonly attribute nsIDOMTimeRanges played;
		//readonly attribute nsIDOMTimeRanges seekable;

		public bool Ended
		{
			get { return Instance.GetEndedAttribute(); }
		}

		public bool Autoplay
		{
			get { return Instance.GetAutoplayAttribute(); }
			set { Instance.SetAutoplayAttribute(value); }
		}

		public bool Loop
		{
			get { return Instance.GetLoopAttribute(); }
			set { Instance.SetLoopAttribute(value); }
		}

		public void Play()
		{
			Instance.Play();
		}

		public void Pause()
		{
			Instance.Pause();
		}

		public bool Controls
		{
			get { return Instance.GetControlsAttribute(); }
			set { Instance.SetControlsAttribute(value); }
		}

		public double Volume
		{
			get { return Instance.GetVolumeAttribute(); }
			set { Instance.SetVolumeAttribute(value); }
		}

		public bool Muted
		{
			get { return Instance.GetMutedAttribute(); }
			set { Instance.SetMutedAttribute(value); }
		}

		public bool DefaultMuted
		{
			get { return Instance.GetDefaultMutedAttribute(); }
			set { Instance.SetDefaultMutedAttribute(value); }
		}


	}
}
