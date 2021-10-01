using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    /// <summary>
    /// Represents a module runner used to build, configure and run modules
    /// </summary>
    public interface IModuleRunner : IModule
    {
        /// <summary>
        /// Merges an <see cref="IServiceCollection"/> into the existing <see cref="IServiceCollection"/> to be consumed by the module
        /// </summary>
        /// <param name="services"></param>
        void Merge(IServiceCollection services);

        /// <summary>
        /// Configures services to be used globally by all modules
        /// </summary>
        /// <param name="configureServices"></param>
        void Configure(Action<IServiceCollection> configureServices);
    }
}
