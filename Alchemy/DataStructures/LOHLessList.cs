using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public class LOHLessList<T> : IDisposable
    {
        private bool _disposed;
        private int _slotNum;

        private Slot[] temp = new Slot[17];

        struct Slot
        {
            public int Index;
            public T[] Items;
        }

        public void Add(T item)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(LOHLessList<T>));

            Slot local = temp[_slotNum];

            if (local.Items == null)
            {
                local.Items = ArrayPool<T>.Shared.Rent(16 << _slotNum);
            }

            local.Items[local.Index++] = item;
            temp[_slotNum] = local;

            if (local.Items.Length == local.Index)
            {
                _slotNum++;
            }
        }



        public void Dispose()
        {
            _disposed = true;

            for (int i = 0; i < temp.Length; i++)
            {
                Slot slot = temp[i];
                if (slot.Items == null) continue;

                ArrayPool<T>.Shared.Return(slot.Items, true);
                slot.Items = null;
                temp[i] = slot;
            }

            temp = null;
        }
    }
}
