using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Base
{
    /// <inheritdoc cref="IModule"/>
    public abstract class ModuleBase : IModule
    {
        private readonly List<object> parameters;
        public event EventHandler<ModuleEventArgs> Started;
        public event EventHandler<ModuleEventArgs> Stopped;

        protected ModuleBase()
        {
            parameters = new List<object>();
        }

        public virtual void AddParameters(IEnumerable<object> parameters)
        {
            this.parameters.AddRange(parameters);
        }

        public virtual void OnStarted(ModuleEventArgs e)
        {
            Started?.Invoke(this, e);
        }

        public virtual void OnStopped(ModuleEventArgs e)
        {
            Stopped?.Invoke(this, e);
        }

        /// <inheritdoc cref="IDisposable"/>
        public virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                Stop(CancellationToken.None).Wait();

                parameters.Where(a => a is IDisposable)
                    .Select(a => a as IDisposable)
                    .ForEach(a => a.Dispose());
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
