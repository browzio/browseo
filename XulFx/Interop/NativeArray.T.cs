using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Gecko.Interop
{

	public enum NativeArrayItemType
	{
		ConstPtr,
		Ptr,
		Interface,
		Value64Bit,
		Value32Bit,
		Value16Bit,
		Value8Bit,
	}

	public class NativeArray<T> : IList<T>, IDisposable
	{
		private IntPtr _mem;
		private readonly uint _count;
		private readonly NativeArrayItemType _itemType;
		private readonly Func<IntPtr, object> _nativeToManagedFun;
		private readonly Func<object, IntPtr> _managedToNativeFun;

		/// <summary>
		/// An indexed collection of elements. Provides basic functionality for
		/// retrieving elements at a specific position, searching for
		/// elements. Indexes are zero-based, such that the last element in the
		/// array is stored at the index length-1.
		/// </summary>
		/// <param name="mem">Address of array (pointer to first element in array).</param>
		/// <param name="count">The number of elements in the array.</param>
		/// <param name="itemType"></param>
		/// <param name="nativeToManagedFun"><see cref="System.IntPtr"/> to T marshaler.</param>
		/// <param name="managedToNativeFun">T to <see cref="System.IntPtr"/> marshaler.</param>
		public NativeArray(IntPtr mem, uint count,
			NativeArrayItemType itemType,
			Func<IntPtr, object> nativeToManagedFun,
			Func<object, IntPtr> managedToNativeFun)
		{
			if (mem == IntPtr.Zero)
				throw new ArgumentOutOfRangeException("mem");

			_mem = mem;
			_count = count;
			_itemType = itemType;
			_nativeToManagedFun = nativeToManagedFun;
			_managedToNativeFun = managedToNativeFun;
		}

		protected NativeArrayItemType ItemType
		{
			get { return _itemType; }
		}

		protected unsafe IntPtr GetItemPtr(int index)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);
			return Marshal.ReadIntPtr((IntPtr)unchecked(((byte*)_mem) + IntPtr.Size * index));
		}

		protected unsafe void SetItemPtr(int index, IntPtr value)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);
			Marshal.WriteIntPtr((IntPtr)unchecked(((byte*)_mem) + IntPtr.Size * index), value);
		}

		protected unsafe byte GetItem8bit(int index)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);
			return Marshal.ReadByte((IntPtr)unchecked(((byte*)_mem) + index));
		}

		protected unsafe void SetItem8bit(int index, byte value)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);

			Marshal.WriteByte((IntPtr)unchecked(((byte*)_mem) + index), value);
		}

		protected unsafe short GetItem16bit(int index)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);
			return Marshal.ReadInt16((IntPtr)unchecked(((byte*)_mem) + (index << 1)));
		}

		protected unsafe void SetItem16bit(int index, short value)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);

			Marshal.WriteInt16((IntPtr)unchecked(((byte*)_mem) + (index << 1)), value);
		}

		protected unsafe int GetItem32bit(int index)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);
			return Marshal.ReadInt32((IntPtr)unchecked(((byte*)_mem) + (index << 2)));
		}

		protected unsafe void SetItem32bit(int index, int value)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);

			Marshal.WriteInt32((IntPtr)unchecked(((byte*)_mem) + (index << 2)), value);
		}

		protected unsafe long GetItem64bit(int index)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);
			return Marshal.ReadInt64((IntPtr)unchecked(((byte*)_mem) + (index << 3)));
		}

		protected unsafe void SetItem64bit(int index, long value)
		{
			if (index < 0 || index >= _count)
				throw new IndexOutOfRangeException();
			if (_mem == IntPtr.Zero)
				throw new ObjectDisposedException(this.GetType().Name);

			Marshal.WriteInt64((IntPtr)unchecked(((byte*)_mem) + (index << 3)), value);
		}


		public virtual int IndexOf(T item)
		{
			for (int i = (int)_count - 1; i >= 0; i--)
				if (item.Equals(this[i]))
					return i;
			return -1;
		}

		public virtual void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		public virtual void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public virtual T this[int index]
		{
			get
			{
				switch (_itemType)
				{
					case NativeArrayItemType.Value8Bit:
						return (T)Convert.ChangeType(GetItem8bit(index), typeof(T));
					case NativeArrayItemType.Value16Bit:
						return (T)Convert.ChangeType(GetItem16bit(index), typeof(T));
					case NativeArrayItemType.Value32Bit:
						return (T)Convert.ChangeType(GetItem32bit(index), typeof(T));
					case NativeArrayItemType.Value64Bit:
						return (T)Convert.ChangeType(GetItem64bit(index), typeof(T));
				}
				if (typeof(T) == typeof(IntPtr))
					return (T)(object)GetItemPtr(index);
				else if (_nativeToManagedFun != null)
					return (T)_nativeToManagedFun(GetItemPtr(index));

				throw new NotSupportedException();
			}
			set
			{
				switch (_itemType)
				{
					case NativeArrayItemType.Value8Bit:
						SetItem8bit(index, Convert.ToByte(value));
						break;
					case NativeArrayItemType.Value16Bit:
						SetItem16bit(index, Convert.ToInt16(value));
						break;
					case NativeArrayItemType.Value32Bit:
						SetItem32bit(index, Convert.ToInt32(value));
						break;
					case NativeArrayItemType.Value64Bit:
						SetItem64bit(index, Convert.ToInt64(value));
						break;
					default:
						if (typeof(T) == typeof(IntPtr))
							SetItemPtr(index, (IntPtr)(object)value);
						else if (_managedToNativeFun != null)
							SetItemPtr(index, _managedToNativeFun(value));
						else
							throw new NotSupportedException();
						break;
				}
			}
		}

		public virtual void Add(T item)
		{
			throw new NotSupportedException();
		}

		public virtual void Clear()
		{
			throw new NotSupportedException();
		}

		public virtual bool Contains(T item)
		{
			foreach (T aItem in this)
				if (aItem.Equals(item))
					return true;
			return false;
		}

		public virtual void CopyTo(T[] array, int destIndex)
		{
			if (array == null)
				throw new ArgumentNullException("array");
			if (destIndex < 0)
				throw new ArgumentOutOfRangeException("destIndex");
			if (array.Length < destIndex + _count)
				throw new ArgumentException("Destination array was not long enough. Check destIndex and length, and the array's lower bounds.");

			for (int i = this.Count - 1; i >= 0; i--)
				array[i] = this[i];
		}

		public int Count
		{
			get { return (int)_count; }
		}

		public virtual bool IsReadOnly
		{
			get { return false; }
		}

		public virtual bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < _count; i++)
				yield return this[i];
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void Dispose()
		{
			if (_mem != IntPtr.Zero)
			{
				switch (_itemType)
				{
					case NativeArrayItemType.Ptr:
						for (int i = 0; i < _count; i++)
							Xpcom.Free(GetItemPtr(i));
						break;
					case NativeArrayItemType.Interface:
						for (int i = 0; i < _count; i++)
							Marshal.Release(GetItemPtr(i));
						break;
				}
				Xpcom.Free(_mem);
				_mem = IntPtr.Zero;
			}
		}
	}

}