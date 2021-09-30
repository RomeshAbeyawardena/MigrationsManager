using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Subjects;
using MigrationsManager.Shared.Defaults;

namespace MigrationsManager.Shared.Base
{
    /// <inheritdoc cref="IModule"/>
    public abstract class ModuleBase : IModule
    {
        private readonly List<object> parameters;

        protected readonly ISubject<ModuleEventArgs> moduleState;

        public IObservable<ModuleEventArgs> State => moduleState;
        
        protected IModuleResult ReportError(Exception exception)
        {
            OnError(exception);
            return DefaultModuleResult.Failed(exception);
        }

        protected ModuleBase()
        {
            parameters = new List<object>();
            moduleState = new Subject<ModuleEventArgs>();
        }

        public virtual void AddParameters(IEnumerable<object> parameters)
        {
            this.parameters.AddRange(parameters);
        }

        public virtual void OnStarted(ModuleEventArgs e, CancellationToken cancellationToken)
        {
            OnRun(cancellationToken);
            moduleState.OnNext(e);
        }

        public virtual void OnStopped(ModuleEventArgs e, CancellationToken cancellationToken)
        {
            OnStop(cancellationToken);
            moduleState.OnNext(e);
            moduleState.OnCompleted();
        }

        public virtual void OnError(Exception exception)
        {
            moduleState.OnError(exception);
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

        public virtual Task Run(CancellationToken cancellationToken)
        {
            OnStarted(new ModuleEventArgs(this, true), cancellationToken);
            Result = DefaultModuleResult.Success(Result);
            return Task.CompletedTask;
        }

        public virtual Task Stop(CancellationToken cancellationToken)
        {
            OnStopped(new ModuleEventArgs(this, false), cancellationToken);
            return Task.CompletedTask;
        }

        public abstract Task OnRun(CancellationToken cancellationToken);
        public abstract Task OnStop(CancellationToken cancellationToken);

        public IModuleResult Result { get; protected set; }
    }
}
