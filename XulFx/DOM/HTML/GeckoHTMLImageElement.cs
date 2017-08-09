using Gecko.DOM.Abstractions;
using Gecko.Interfaces;

namespace Gecko.DOM.HTML
{
	/// <summary>
	/// Provides special properties and methods (beyond the regular HTMLElement interface it also
	/// has available to it by inheritance) for manipulating the layout and presentation of &lt;img&gt; elements.
	/// </summary>
	public sealed class GeckoHTMLImageElement : GeckoHTMLElement<nsIDOMHTMLImageElement>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public static GeckoHTMLImageElement Create(nsIDOMHTMLImageElement element)
		{
			return new GeckoHTMLImageElement(element);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="element"></param>
		private GeckoHTMLImageElement(nsIDOMHTMLImageElement element)
			: base(element)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get { return nsString.Get(Instance.GetNameAttribute); }
			set { nsString.Set(Instance.SetNameAttribute, value); }
		}

		/// <summary>
		/// Gets and sets a string indicating the alignment of the image with respect to the surrounding context.
		/// </summary>
		public string Align
		{
			get { return nsString.Get(Instance.GetAlignAttribute); }
			set { nsString.Set(Instance.SetAlignAttribute, value); }
		}

		/// <summary>
		/// Gets and sets a string that reflects the alt HTML attribute, indicating fallback context for the image.
		/// </summary>
		public string Alt
		{
			get { return nsString.Get(Instance.GetAltAttribute); }
			set { nsString.Set(Instance.SetAltAttribute, value); }
		}

		/// <summary>
		/// Gets and sets a string that gives the width of the border around the image. This is now deprecated and the CSS border property should be used instead.
		/// </summary>
		public string Border
		{
			get { return nsString.Get(Instance.GetBorderAttribute); }
			set { nsString.Set(Instance.SetBorderAttribute, value); }
		}

		/// <summary>
		/// Gets and sets a value that reflects the height HTML attribute, indicating the rendered height of the image in CSS pixels.
		/// </summary>
		public uint Height
		{
			get { return Instance.GetHeightAttribute(); }
			set { Instance.SetHeightAttribute(value); }
		}

		/// <summary>
		/// Gets and sets a value representing the space to the left and right of the image.
		/// </summary>
		public int Hspace
		{
			get { return Instance.GetHspaceAttribute(); }
			set { Instance.SetHspaceAttribute(value); }
		}

		/// <summary>
		/// Gets and sets a value that reflects the ismap HTML attribute, indicating that the image is part of a server-side image map.
		/// </summary>
		public bool IsMap
		{
			get { return Instance.GetIsMapAttribute(); }
			set { Instance.SetIsMapAttribute(value); }
		}

		/// <summary>
		/// Gets and sets a string representing the URI of a long description of the image.
		/// </summary>
		public string LongDesc
		{
			get { return nsString.Get(Instance.GetLongDescAttribute); }
			set { nsString.Set(Instance.SetLongDescAttribute, value); }
		}

		/// <summary>
		/// Gets and sets a string that reflects the src HTML attribute, containing the full URL of the image including base URI.
		/// </summary>
		public string Src
		{
			get { return nsString.Get(Instance.GetSrcAttribute); }
			set { nsString.Set(Instance.SetSrcAttribute, value); }
		}

		/// <summary>
		/// Gets and sets a string that reflects the usemap HTML attribute, containing a partial URL of a map element.
		/// </summary>
		public string UseMap
		{
			get { return nsString.Get(Instance.GetUseMapAttribute); }
			set { nsString.Set(Instance.SetUseMapAttribute, value); }
		}

		/// <summary>
		/// Gets and sets a value representing the space above and below the image
		/// </summary>
		public int Vspace
		{
			get { return Instance.GetVspaceAttribute(); }
			set { Instance.SetVspaceAttribute(value); }
		}

		/// <summary>
		/// Gets and sets a value that reflects the width HTML attribute, indicating the rendered width of the image in CSS pixels.
		/// </summary>
		public uint Width
		{
			get { return Instance.GetWidthAttribute(); }
			set { Instance.SetWidthAttribute(value); }
		}

		/// <summary>
		/// Gets a value representing the intrinsic width of the image in CSS pixels, if it is available; otherwise, 0.
		/// </summary>
		public int NaturalWidth
		{
			get { return (int)Instance.GetNaturalWidthAttribute(); }
		}

		/// <summary>
		/// Gets a value representing the intrinsic height of the image in CSS pixels, if it is available; otherwise, 0.
		/// </summary>
		public int NaturalHeight
		{
			get { return (int)Instance.GetNaturalHeightAttribute(); }
		}

		/// <summary>
		/// Gets a value that is true if the browser has finished fetching the image,
		/// whether successful or not. Also returns true if the image has no src value.
		/// </summary>
		public bool Complete
		{
			get { return Instance.GetCompleteAttribute(); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string CurrentSrc
		{
			get { return nsString.Get(Instance.GetCurrentSrcAttribute); }
		}

		/// <summary>
		/// Gets a string representing a reference to a low-quality (but faster to load) copy of the image.
		/// </summary>
		public string Lowsrc
		{
			get { return nsString.Get(Instance.GetLowsrcAttribute); }
			set { nsString.Set(Instance.SetLowsrcAttribute, value); }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Sizes
		{
			get { return nsString.Get(Instance.GetSizesAttribute); }
			set { nsString.Set(Instance.SetSizesAttribute, value); }
		}

		/// <summary>
		/// Gets and sets a string reflecting the srcset HTML attribute, containing a list of candidate images,
		/// separated by a comma (&quot;,&quot;, U+002C COMMA). A candidate image is a URL followed by a &quot;w&quot;
		/// with the width of the images, or an &quot;x&quot; followed by the pixel density.
		/// </summary>
		public string SrcSet
		{
			get { return nsString.Get(Instance.GetSrcsetAttribute); }
			set { nsString.Set(Instance.SetSrcsetAttribute, value); }
		}

		/// <summary>
		/// Gets a value representing the horizontal offset from the nearest layer. This property mimics an old Netscape 4 behavior.
		/// </summary>
		public int X
		{
			get { return Instance.GetXAttribute(); }
		}

		/// <summary>
		/// Gets a value representing the vertical offset from the nearest layer. This property mimics an old Netscape 4 behavior.
		/// </summary>
		public int Y
		{
			get { return Instance.GetYAttribute(); }
		}

	}
}

