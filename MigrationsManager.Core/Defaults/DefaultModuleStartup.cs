﻿using Microsoft.Extensions.DependencyInjection;
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

        public DefaultModuleStartup(IServiceCollection services, IModuleRunner moduleRunner)
        {
            this.services = services;
            this.moduleRunner = moduleRunner;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .BuildServiceProvider();
        }

        public override Task Run(CancellationToken cancellationToken)
        {
            ConfigureServices(services);
            moduleRunner.Merge(services);
            return moduleRunner.Run(cancellationToken);
        }

        public override Task Stop(CancellationToken cancellationToken)
        {
            return moduleRunner.Stop(cancellationToken);
        }
    }
}
