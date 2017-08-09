using System;
using Gecko.Interop;
using Gecko.Interfaces;

namespace Gecko.WebAPI
{
	/// <summary>
	/// The Web Performance API
	/// </summary>
	public sealed class GeckoPerformance : ComObject<nsISupports>, IGeckoObjectWrapper
	{
		private nsIXulfxWEBAPIPerfomanceHelper _helper;

		public static GeckoPerformance Create(nsISupports instance)
		{
			return new GeckoPerformance(instance);
		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _helper);
			base.Dispose(disposing);
		}

		private GeckoPerformance(nsISupports instance)
			: base(instance)
		{
			_helper = Xpcom.CreateInstance<nsIXulfxWEBAPIPerfomanceHelper>(Contracts.XulfxWebApiPerformance);
			_helper.Init(instance);
		}

		public nsIXulfxWEBAPIPerfomanceHelper Helper
		{
			get
			{
				if (_helper == null)
					throw new ObjectDisposedException(this.GetType().Name);
				return _helper;
			}
		}


		public GeckoPerformanceTiming Timing
		{
			get { return Helper.GetTimingAttribute().Wrap(GeckoPerformanceTiming.Create); }
		}

		public GeckoPerformanceNavigation Navigation
		{
			get { return Helper.GetNavigationAttribute().Wrap(GeckoPerformanceNavigation.Create); }
		}

		/// <summary>
		/// Returns the number of milliseconds, accurate to a thousandth of a millisecond,
		/// from the start of document navigation to the time the now method was called.
		/// </summary>
		public double Now()
		{
			return Helper.Now();
		}

		/// <summary>
		/// Records a performance mark, which is a time value associated with a name.
		/// </summary>
		/// <param name="name">Name associated with the performance mark.</param>
		public void Mark(string name)
		{
			nsString.Set(Helper.Mark, name);
		}

		public void ClearResourceTimings()
		{
			Helper.ClearResourceTimings();
		}

		public void SetResourceTimingBufferSize(uint maxSize)
		{
			Helper.SetResourceTimingBufferSize(maxSize);
		}



	}
}
