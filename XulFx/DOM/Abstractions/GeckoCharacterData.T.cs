using System;
using Gecko.Interfaces;
using Gecko.Interop;

namespace Gecko.DOM.Abstractions
{
	public abstract class GeckoCharacterData<TInterface> : GeckoNode<nsIDOMCharacterData>
		where TInterface : class, nsIDOMCharacterData
	{
		private new TInterface _instance;

		internal static GeckoNode<nsIDOMCharacterData> Create(nsIDOMCharacterData instance)
		{
			//bool found = true;
			try
			{
				GeckoNode<nsIDOMCharacterData> wrapper;
				if (TryCast<nsIDOMComment, GeckoComment>(instance, GeckoComment.Create, out wrapper))
					return wrapper;

				if (TryCast<nsIDOMText, GeckoText>(instance, GeckoText.Create, out wrapper))
					return wrapper;

				//found = false;
			}
			finally
			{
				//if(found) Xpcom.FreeComObject(ref instance);
			}
			throw new NotImplementedException();
		}

		private static bool TryCast<TDOMInterface, TWrapper>(nsIDOMCharacterData instance, Func<TDOMInterface, TWrapper> create, out GeckoNode<nsIDOMCharacterData> wrapper)
			where TDOMInterface : class, nsIDOMCharacterData
			where TWrapper : GeckoNode<nsIDOMCharacterData>
		{
			var comObj = Xpcom.QueryInterface<TDOMInterface>(instance);
			if (comObj != null)
			{
				wrapper = create(comObj);
				return true;
			}
			wrapper = null;
			return false;
		}

		protected GeckoCharacterData(TInterface instance)
			: base(Xpcom.QueryInterface<nsIDOMCharacterData>(instance))
		{

		}

		protected override void Dispose(bool disposing)
		{
			Xpcom.FreeComObject(ref _instance);
			base.Dispose(disposing);
		}

		public new TInterface Instance
		{
			get
			{
				Xpcom.AssertCorrectThread();
				if (_instance == null)
					throw new ObjectDisposedException(this.GetType().Name);

				return _instance;
			}
		}

		public string Data
		{
			get { return nsString.Get(Instance.GetDataAttribute); }
			set { nsString.Set(Instance.SetDataAttribute, value); }
		}

		public uint Length
		{
			get { return Instance.GetLengthAttribute(); }
		}


		public string SubstringData(uint offset, uint count)
		{
			return nsString.Get(Instance.SubstringData, offset, count);
		}

		public void AppendData(string arg)
		{
			nsString.Set(Instance.AppendData, arg);
		}

		public void InsertData(uint offset, string arg)
		{
			nsString.Set(Instance.InsertData, offset, arg);
		}

		public void DeleteData(uint offset, uint count)
		{
			base.Instance.DeleteData(offset, count);
		}

		public void ReplaceData(uint offset, uint count, string arg)
		{
			nsString.Set(Instance.ReplaceData, offset, count, arg);
		}
	}
}
