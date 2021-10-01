using Microsoft.AspNetCore.Hosting;
using MigrationsManager.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Runner
{
    public class WebHostModule : ModuleBase
    {
        public override Task OnRun(CancellationToken cancellationToken)
        {
            WebHostBuilder
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
