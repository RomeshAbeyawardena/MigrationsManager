using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Defaults
{
    public class DefaultTaskQueue : BaseTaskQueue
    {
        private ConcurrentQueue<Func<Task>> taskQueue;
        public DefaultTaskQueue()
            : base(new ConcurrentQueue<Func<Task>>())
        {

        }

        public override bool Dequeue(out Func<Task> task)
        {
            return base.Queue.TryTake(out task);
        }
    }

    public class DefaultTaskQueue<TResult> : DefaultTaskQueue, ITaskQueue<TResult>
    {
        public bool Dequeue(out Func<Task<TResult>> task)
        {
            task = null;
            var hasItems = base.Queue.TryTake(out var baseTask);

            if (hasItems)
            {
                task = (Func<Task<TResult>>)baseTask;
            }

            return hasItems;
        }

        public bool TryAdd(Func<Task<TResult>> item)
        {
            return Queue.TryAdd(item);
        }
    }
}
