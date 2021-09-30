using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface ITaskQueue<TResult> : ITaskQueue
    {
        bool Dequeue(out Func<Task<TResult>> task);
        bool TryAdd(Func<Task<TResult>> item);
    }

    public interface ITaskQueue : IProducerConsumerCollection<Func<Task>>
    {
        bool Dequeue(out Func<Task> task);
    }
}
