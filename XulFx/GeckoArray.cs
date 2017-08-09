using System;
using Gecko.Interfaces;
using Gecko.Interop;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gecko
{
	sealed class GeckoArray<T, TInterface> : ComObject<nsIArray>, IGeckoObjectWrapper, IList<T>
		where T : class, IGeckoObjectWrapper, IComObject
		where TInterface : class
	{
		private Guid _uuid;
		private Func<TInterface, T> _createItem;
		private ReadOnlyCollection<T> _collection;

		internal static GeckoArray<T, TInterface> Create(nsIArray instance, Func<TInterface, T> createItem)
		{
			return new GeckoArray<T, TInterface>(instance, createItem);
		}

		private GeckoArray(nsIArray instance, Func<TInterface, T> createItem)
			: base(instance)
		{
			_uuid = typeof(TInterface).GUID;
			_createItem = createItem;
		}

		public ReadOnlyCollection<T> AsReadOnlyCollection()
		{
			if (_collection == null)
				_collection = new ReadOnlyCollection<T>(this);
			return _collection;
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		public void Add(T item)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		public void Clear()
		{
			throw new NotSupportedException("Collection was of a fixed size.");
		}

		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
					throw new IndexOutOfRangeException();
				IntPtr pInterface = Instance.QueryElementAt((uint)index, ref _uuid);
				T wrapper;
				try
				{
					wrapper = ((TInterface)Marshal.GetObjectForIUnknown(pInterface)).Wrap(_createItem);
				}
				finally
				{
					Marshal.Release(pInterface);
				}
				return wrapper;
			}
			set
			{

			}
		}

		public int IndexOf(T item)
		{
			var itemInstance = Xpcom.QueryInterface<nsISupports>(item.NativeInstance);
			try
			{
				return (int)Instance.IndexOf(0, itemInstance);
			}
			catch (COMException e)
			{
				if (e.ErrorCode == GeckoError.NS_ERROR_FAILURE)
					return -1;
				throw;
			}
			finally
			{
				Xpcom.FreeComObject(ref itemInstance);
			}
		}

		public bool Contains(T item)
		{
			return IndexOf(item) != -1;
		}

		public void CopyTo(T[] array, int destIndex)
		{
			if (array == null)
				throw new ArgumentNullException("array");
			if (this.Count + destIndex < array.Length)
				throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");

			for (int i = this.Count - 1; i >= 0; i--)
			{
				array[destIndex + i] = this[i];
			}
		}

		public int Count
		{
			get { return (int)Instance.GetLengthAttribute(); }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new GeckoSimpleEnumerator<TInterface, T>(Instance.Enumerate(), _createItem);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (T element in this)
				yield return element;
		}
	}
}
