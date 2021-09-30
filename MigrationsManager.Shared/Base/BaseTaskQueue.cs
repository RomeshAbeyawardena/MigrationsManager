using MigrationsManager.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Base
{
    public abstract class BaseTaskQueue : ITaskQueue
    {
        protected IProducerConsumerCollection<Func<Task>> Queue { get; }

        protected BaseTaskQueue(IProducerConsumerCollection<Func<Task>> queue)
        {
            this.Queue = queue;
        }

        int ICollection.Count => Queue.Count;

        bool ICollection.IsSynchronized => Queue.IsSynchronized;

        object ICollection.SyncRoot => Queue.SyncRoot;

        void IProducerConsumerCollection<Func<Task>>.CopyTo(Func<Task>[] array, int index)
        {
            Queue.CopyTo(array, index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            Queue.CopyTo(array, index);
        }

        public abstract bool Dequeue(out Func<Task> task);

        IEnumerator<Func<Task>> IEnumerable<Func<Task>>.GetEnumerator()
        {
            return Queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Queue.GetEnumerator();
        }

        Func<Task>[] IProducerConsumerCollection<Func<Task>>.ToArray()
        {
            return Queue.ToArray();
        }

        bool IProducerConsumerCollection<Func<Task>>.TryAdd(Func<Task> item)
        {
            return Queue.TryAdd(item);
        }

        bool IProducerConsumerCollection<Func<Task>>.TryTake(out Func<Task> item)
        {
            return Queue.TryTake(out item);
        }
    }
}
