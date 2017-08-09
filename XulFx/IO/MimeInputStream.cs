using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.IO
{
	public sealed class MimeInputStream
		: InputStream
	{
		private new nsIMIMEInputStream _instance;

		public static MimeInputStream Create()
		{
			return Xpcom.CreateInstance<nsIMIMEInputStream>(Contracts.MimeInputStream)
				.Wrap(MimeInputStream.Create);
		}

		public static MimeInputStream Create(nsIMIMEInputStream instance)
		{
			return new MimeInputStream(instance);
		}

		private MimeInputStream(nsIMIMEInputStream instance)
			: base(instance)
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			_instance = null;
			base.Dispose(disposing);
		}

		public new nsIMIMEInputStream Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _instance;
			}
		}

		public bool AddContentLength
		{
			get { return Instance.GetAddContentLengthAttribute(); }
			set { Instance.SetAddContentLengthAttribute(value); }
		}

		public void AddHeader(string name, string value)
		{
			Instance.AddHeader(name, value);
		}

		public void SetData(InputStream stream)
		{
			Instance.SetData(stream.Instance);
		}

		public void SetData(string data)
		{
			if (!string.IsNullOrEmpty(data))
			{
				using (var stringInputStream = StringInputStream.Create(data))
				{
					Instance.SetData(stringInputStream.Instance);
				}
			}
		}

	}
}