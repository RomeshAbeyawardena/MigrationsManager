using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IModuleRunner : IModule
    {
        void Merge(IServiceCollection services);
        void Configure(Action<IServiceCollection> configureServices);
    }
}
