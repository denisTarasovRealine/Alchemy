using System;
using System.Collections.Generic;
using System.Threading;

namespace DataStructures
{
    public class AlchemyReadWriteLock
    {



        //static inline bool AtomicTryLock(TAtomic* a)
        //{
        //    return AtomicCas(a, 1, 0);
        //}

        //static inline bool AtomicTryAndTryLock(TAtomic* a)
        //{
        //    return (AtomicGet(*a) == 0) && AtomicTryLock(a);
        //}

        //public DisposableContainer UseReadLock()
        //{
        //    return new DisposableContainer(this);
        //}
        private long _readBarrier;

        public void TryEnterReadLock()
        {
            long local = Interlocked.Read( ref _readBarrier);
            if (local == 0)
            {
                
            }

        }

        public void ExitReadLock()
        {

        }

        //public struct DisposableContainer : IDisposable
        //{
        //    private readonly AlchemyReadWriteLock _lock;

        //    public DisposableContainer(AlchemyReadWriteLock alchemyLock)
        //    {
        //        _lock = alchemyLock;
        //    }

        //    public void Dispose()
        //    {
        //        _lock.ExitReadLock();
        //    }
        //}




    }

    public class ConcurrentSet<T>
    {
        private readonly AlchemyReadWriteLock _alchemyLock = new AlchemyReadWriteLock(); 
        
        public void Contains(T item)
        {
            //using (_alchemyLock.UseReadLock())
            {
                

            }
        }
    }
}
