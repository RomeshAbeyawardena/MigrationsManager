using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    public class DefaultModuleStartup : ModuleBase, IModuleStartup
    {
        private readonly IServiceCollection services;
        private readonly IModuleRunner moduleRunner;
        private readonly IDisposable subscriber;
        public DefaultModuleStartup(IServiceCollection services, IModuleRunner moduleRunner)
        {
            this.services = services;
            this.moduleRunner = moduleRunner;
            this.subscriber = moduleRunner.State.Subscribe(moduleState);
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {            
            moduleRunner.Merge(services);
            return moduleRunner.Run(cancellationToken);
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return moduleRunner.Stop(cancellationToken);
        }

        public override void Dispose(bool dispose)
        {
            if(dispose)
            {
                moduleRunner.Dispose();
                subscriber.Dispose();
            }
            base.Dispose(dispose);
        }
    }
}
