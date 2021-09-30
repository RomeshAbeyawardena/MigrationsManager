using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Defaults
{
    public class DefaultTaskQueue : BaseTaskQueue
    {
        public DefaultTaskQueue()
            : base(new ConcurrentQueue<Func<CancellationToken, Task>>())
        {

        }

        public override bool Dequeue(out Func<CancellationToken, Task> task)
        {
            return base.Queue.TryTake(out task);
        }
    }

    public class DefaultTaskQueue<TResult> : DefaultTaskQueue, ITaskQueue<TResult>
    {
        public bool Dequeue(out Func<CancellationToken,Task<TResult>> task)
        {
            task = null;
            var hasItems = base.Queue.TryTake(out var baseTask);

            if (hasItems)
            {
                task = (Func<CancellationToken, Task<TResult>>)baseTask;
            }

            return hasItems;
        }

        public bool TryAdd(Func<CancellationToken, Task<TResult>> item)
        {
            return Queue.TryAdd(item);
        }
    }
}
