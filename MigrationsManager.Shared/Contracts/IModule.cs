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
    /// Represents a module to run a unit of work
    /// </summary>
    public interface IModule : IDisposable
    {
        /// <summary>
        /// Runs a unit of work within this <see cref="IModule"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Run(CancellationToken cancellationToken);
        /// <summary>
        /// Stops the unit of work prior completion
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Stop(CancellationToken cancellationToken);
    }
}
