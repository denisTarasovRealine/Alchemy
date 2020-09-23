using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;

namespace DataStructures
{
    public class LOHLessList<T> : IDisposable
    {
        private bool _disposed;
        private int _slotNum;

        private Slot[] temp = new Slot[17];

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(LOHLessList<T>));

            var local = temp[_slotNum];

            if (local.Items == null) local.Items = ArrayPool<ValueContainer<T>>.Shared.Rent(16 << _slotNum);

            local.Items[local.Index++] = new ValueContainer<T> { Value = item };
            temp[_slotNum] = local;

            if (local.Items.Length == local.Index) _slotNum++;

            Count++;
        }

        public bool Remove(T item)
        {
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

        public void Dispose()
        {
            _disposed = true;

            for (var i = 0; i < temp.Length; i++)
            {
                Slot slot = temp[i];
                if (slot.Items == null) continue;

                ArrayPool<ValueContainer<T>>.Shared.Return(slot.Items, true);
                slot.Items = null;
                temp[i] = slot;
            }

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