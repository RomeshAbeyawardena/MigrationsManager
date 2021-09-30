using MigrationsManager.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Base
{
    public abstract class BaseTaskQueue : ITaskQueue
    {
        protected IProducerConsumerCollection<Func<CancellationToken, Task>> Queue { get; }

        protected BaseTaskQueue(IProducerConsumerCollection<Func<CancellationToken, Task>> queue)
        {
            this.Queue = queue;
        }

        int ICollection.Count => Queue.Count;

        bool ICollection.IsSynchronized => Queue.IsSynchronized;

        object ICollection.SyncRoot => Queue.SyncRoot;

        void IProducerConsumerCollection<Func<CancellationToken, Task>>.CopyTo(Func<CancellationToken, Task>[] array, int index)
        {
            Queue.CopyTo(array, index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            Queue.CopyTo(array, index);
        }

        public abstract bool Dequeue(out Func<CancellationToken, Task> task);

        IEnumerator<Func<CancellationToken, Task>> IEnumerable<Func<CancellationToken, Task>>.GetEnumerator()
        {
            return Queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Queue.GetEnumerator();
        }

        Func<CancellationToken, Task>[] IProducerConsumerCollection<Func<CancellationToken, Task>>.ToArray()
        {
            return Queue.ToArray();
        }

        bool IProducerConsumerCollection<Func<CancellationToken, Task>>.TryAdd(Func<CancellationToken, Task> item)
        {
            return Queue.TryAdd(item);
        }

        bool IProducerConsumerCollection<Func<CancellationToken, Task>>.TryTake(out Func<CancellationToken, Task> item)
        {
            return Queue.TryTake(out item);
        }
    }
}
