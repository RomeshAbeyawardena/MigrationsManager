using Microsoft.Extensions.Logging;
using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationManager.TestDomainProject
{
    public class RemoteModule : ModuleBase
    {
        private readonly ILogger<RemoteModule> logger;

        public RemoteModule(ILogger<RemoteModule> logger)
        {
            this.logger = logger;
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            logger.LogInformation("Running"); ;
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
