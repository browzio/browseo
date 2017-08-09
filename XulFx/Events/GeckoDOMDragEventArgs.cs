using Gecko.Interfaces;
using System;


namespace Gecko
{
	public sealed class GeckoDOMDragEventArgs
		: GeckoDOMMouseEventArgs
	{
		private nsIDOMDragEvent _instance;

		public static GeckoDOMDragEventArgs Create(nsIDOMDragEvent instance)
		{
			return new GeckoDOMDragEventArgs(instance);
		}

		private GeckoDOMDragEventArgs(nsIDOMDragEvent instance)
			: base(instance)
		{
			_instance = instance;
		}

		protected override void Dispose(bool disposing)
		{
			_instance = null;
			base.Dispose(disposing);
		}

		public new nsIDOMDragEvent Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ArgumentNullException();
				return _instance;
			}
		}
	}
}
