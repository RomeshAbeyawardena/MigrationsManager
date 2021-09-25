using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Base
{
    public abstract class ModuleBase : IModule
    {
        public virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                Stop(CancellationToken.None).Wait();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract Task Run(CancellationToken cancellationToken);

        public abstract Task Stop(CancellationToken cancellationToken);
    }
}
