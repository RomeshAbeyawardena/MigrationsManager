using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Runner
{
    public class RunnerModule : IModule
    {
        public Task Run(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Stop(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
