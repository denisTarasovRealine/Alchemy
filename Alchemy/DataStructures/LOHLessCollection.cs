using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using DataStructures.Properties;


namespace DataStructures
{
    public class LOHLessCollection<T> : ICollection<T>, IDisposable
    {
        private int _version;
        private bool _disposed;
        private int _slotNum;

        // todo capacity
        // todo rename
        private Slot[] temp = new Slot[17];

        public int Count { get; private set; }
        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(LOHLessCollection<T>));

            var local = temp[_slotNum];

            if (local.Items == null) local.Items = ArrayPool<ValueContainer<T>>.Shared.Rent(16 << _slotNum);

            local.Items[local.Index++] = new ValueContainer<T> { Value = item };
            temp[_slotNum] = local;

            if (local.Items.Length == local.Index) _slotNum++;

            Count++;
            _version++;
        }

        public bool Remove(T item)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(LOHLessCollection<T>));

            for (var i = 0; i < temp.Length; i++)
            {
                Slot slot = temp[i];
                if (slot.Items == null) continue;

                int index = IndexOf(slot.Items, item);
                if (index < 0) continue;

                slot.Items[index] = new ValueContainer<T>
                {
                    Removed = true,
                    Value = default
                };
                temp[i] = slot;

                Count--;
                _version++;

                return true;
            }

            return false;

            int IndexOf(ValueContainer<T>[] array, T value)
            {
                for (int index = 0; index < array.Length; ++index)
                {
                    if (EqualityComparer<T>.Default.Equals(array[index].Value, value))
                        return index;
                }
                return -1;
            }
        }

        public void Clear()
        {
            for (var i = 0; i < temp.Length; i++)
            {
                Slot slot = temp[i];
                if (slot.Items == null) continue;

                ArrayPool<ValueContainer<T>>.Shared.Return(slot.Items, true);
                slot.Items = null;
                slot.Index = 0;
                temp[i] = slot;
            }
            Count = 0;
        }

        public bool Contains(T item)
        {
            // TODO think about a bloom filter
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(LOHLessCollection<T>));

            foreach (T item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(LOHLessCollection<T>));
            if (Count == 0) yield break;

            int version = _version;

            for (var i = 0; i < temp.Length; i++)
            {
                Slot slot = temp[i];
                if (slot.Items == null) continue;

                foreach (ValueContainer<T> valueContainer in slot.Items)
                {
                    if (version != _version) throw new InvalidOperationException(Resources.InvalidOperation_EnumFailedVersion);

                    if (valueContainer.Removed) continue;
                    yield return valueContainer.Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            _disposed = true;

            Clear();

            temp = null;
        }

        private struct ValueContainer<TValue>
        {
            public bool Removed;
            public TValue Value;
        }

        private struct Slot
        {
            public int Index;
            public ValueContainer<T>[] Items;
        }
    }
}