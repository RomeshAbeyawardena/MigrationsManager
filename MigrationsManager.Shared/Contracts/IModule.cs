using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IModule : IDisposable
    {
        Task Run(CancellationToken cancellationToken);
        Task Stop(CancellationToken cancellationToken);
    }
}
