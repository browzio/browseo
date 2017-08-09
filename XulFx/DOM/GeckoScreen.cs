using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM
{
	public sealed class GeckoScreen : ComObject<nsIDOMScreen>, IGeckoObjectWrapper
	{
		public static GeckoScreen Create(nsIDOMScreen instance)
		{
			return new GeckoScreen(instance);
		}

		private GeckoScreen(nsIDOMScreen instance)
			: base(instance)
		{

		}

		public int AvailHeigth
		{
			get { return Instance.GetAvailHeightAttribute(); }
		}

		public int AvailLeft
		{
			get { return Instance.GetAvailLeftAttribute(); }
		}

		public int AvailTop
		{
			get { return Instance.GetAvailTopAttribute(); }
		}

		public int AvailWidth
		{
			get { return Instance.GetAvailWidthAttribute(); }
		}

		public int ColorDepth
		{
			get { return Instance.GetColorDepthAttribute(); }
		}

		public int Heigth
		{
			get { return Instance.GetHeightAttribute(); }
		}

		public int Left
		{
			get { return Instance.GetLeftAttribute(); }
		}

		public int PixelDepth
		{
			get { return Instance.GetPixelDepthAttribute(); }
		}

		public int Top
		{
			get { return Instance.GetTopAttribute(); }
		}

		public int Width
		{
			get { return Instance.GetWidthAttribute(); }
		}

		public string Orientation
		{
			get { return nsString.Get(Instance.GetMozOrientationAttribute); }
		}

	}
}
