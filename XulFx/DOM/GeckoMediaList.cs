using Gecko.Interfaces;
using Gecko.Interop;
using System;
using System.Collections.Generic;

namespace Gecko.DOM
{
	public sealed class GeckoMediaList : ComObject<nsIDOMMediaList>, IGeckoObjectWrapper, IEnumerable<string>
	{
		public static GeckoMediaList Create(nsIDOMMediaList instance)
		{
			return new GeckoMediaList(instance);
		}

		private GeckoMediaList(nsIDOMMediaList instance)
			: base(instance)
		{

		}

		/// <summary>
		/// Gets the number of mediums in the list.
		/// </summary>
		public int Count
		{
			get { return (int)Instance.GetLengthAttribute(); }
		}

		/// <summary>
		/// Returns the medium at the given index in the list.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
					throw new ArgumentOutOfRangeException("index");
				using (var retval = new nsAString())
				{
					Instance.Item((uint)index, retval);
					return retval.ToString();
				}
			}
		}

		/// <summary>
		/// Appends the specified medium to the list.
		/// </summary>
		/// <param name="medium"></param>
		public void AppendMedium(string medium)
		{
			nsString.Set(Instance.AppendMedium, medium);
		}

		/// <summary>
		/// Deletes the specified medium from the list.
		/// </summary>
		/// <param name="medium"></param>
		public void DeleteMedium(string medium)
		{
			nsString.Set(Instance.DeleteMedium, medium);
		}

		/// <summary>
		/// Gets or sets the complete list of mediums as a single string.
		/// </summary>
		public string MediaText
		{
			get { return nsString.Get(Instance.GetMediaTextAttribute); }
			set { nsString.Set(Instance.SetMediaTextAttribute, value); }
		}

		public override string ToString()
		{
			return MediaText;
		}


		#region IEnumerable's

		public IEnumerator<string> GetEnumerator()
		{
			int length = this.Count;
			using (var retval = new nsAString())
			{
				for (uint i = 0; i < length; i++)
				{
					Instance.Item(i, retval);
					yield return retval.ToString();
				}
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion

	}
}
