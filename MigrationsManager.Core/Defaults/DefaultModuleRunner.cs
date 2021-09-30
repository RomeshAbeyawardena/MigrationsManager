using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Defaults;
using MigrationsManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    /// <inheritdoc cref="IModuleRunner" />
    [RegisterService(ServiceLifetime.Transient)]
    public class DefaultModuleRunner : ModuleBase, IModuleRunner
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly IServiceCollection services;
        private readonly IServiceProvider serviceProvider;
        private readonly IModuleOptions moduleOptions;
        private IEnumerable<IModule> modules;
        private IServiceProvider builtModuleServices;
        private IModuleServiceProvider moduleServiceProvider;
        private readonly Dictionary<Type, IModule> modulesCache;
        private readonly ITaskQueue moduleTaskQueue;
        private readonly List<IDisposable> subscribers;
        private IModuleServiceProvider ModuleServiceProvider
        {
            get
            {
                if (moduleServiceProvider == null)
                {
                    moduleServiceProvider = new DefaultModuleServiceProvider(serviceProvider, BuiltModuleServices);
                }

                return moduleServiceProvider;
            }
        }

        private IServiceProvider BuiltModuleServices
        {
            get
            {
                if (builtModuleServices == null)
                {
                    builtModuleServices = services.BuildServiceProvider();
                }

                return builtModuleServices;
            }
        }

        private static IEnumerable<Type> GetModuleTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(a => a.GetTypes().Where(type => type.GetInterfaces().Any(interfaceType => interfaceType == typeof(IModule))));
        }

        private IModule Activate(Type type)
        {
            if (modulesCache.TryGetValue(type, out var module))
            {
                return module;
            }

            var defaultConstructor = type.GetConstructors().FirstOrDefault(a => a.IsPublic && a.IsConstructor);

            if (defaultConstructor == null)
            {
                module = Activator.CreateInstance(type) as IModule;
            }
            else
            {
                var parameters = defaultConstructor.GetParameters()
                    .Select(a => ModuleServiceProvider.GetRequiredService(a.ParameterType))
                    .ToArray();

                module = Activator.CreateInstance(type, parameters) as IModule;
                module.AddParameters(parameters);
            }
            subscribers.Add(module.ResultState.Subscribe(OnNext, OnCompleted));
            subscribers.Add(module.State.Subscribe(moduleState));
            module.ResolveDependencies(moduleServiceProvider);

            return module;
        }

        private void OnCompleted(Exception obj)
        {
            throw obj;
        }

        private void OnNext(IModuleResult obj)
        {
            if(obj.IsException && obj.Haltable)
            {
                cancellationTokenSource.Cancel();
            }

            SetResult(obj);
        }

        private void RegisterServices(Type type)
        {
            var configureServicesMethod = type.GetMethod("ConfigureServices", BindingFlags.Public | BindingFlags.Static);

            configureServicesMethod?.Invoke(null, new[] { services });
        }

        public DefaultModuleRunner(IServiceProvider serviceProvider, IModuleOptions moduleOptions)
        {
            cancellationTokenSource = new CancellationTokenSource();
            moduleTaskQueue = new DefaultTaskQueue<IModuleResult>();
            services = new ServiceCollection();
            modulesCache = new Dictionary<Type, IModule>();
            subscribers = new List<IDisposable>();
            this.serviceProvider = serviceProvider;
            this.moduleOptions = moduleOptions;
        }

        public void Configure(Action<IServiceCollection> configureServices)
        {
            configureServices(services);
        }

        public override async Task OnRun(CancellationToken cancellationToken)
        {
            services.AddSingleton(s => ModuleServiceProvider);

            services.AddSingleton(s => GetModuleTypes(moduleOptions.ModuleAssembliesOptions.GetAssemblies(a => a.Injectable && a.Discoverable))
                .Select(Activate));

            var moduleTypes = GetModuleTypes(moduleOptions.ModuleAssembliesOptions.GetAssemblies(a => a.OnStartup && a.Discoverable));

            moduleTypes.ForEach(RegisterServices);

            modules = moduleTypes.Select(Activate);
            var taskList = new List<Task>();
            modules.ForEach(m => moduleTaskQueue.TryAdd((c) => m.Run(c)));

            while (moduleTaskQueue.Dequeue(out var resultTask))
            {
                taskList.Add(resultTask?.Invoke(cancellationTokenSource.Token));
            }

            await Task.WhenAll(taskList);
        }

        public override async Task OnStop(CancellationToken cancellationToken)
        {
            await Task.WhenAll(modules.Select(a => a.Stop(cancellationToken)));
        }

        public override void Dispose(bool dispose)
        {
            if (dispose)
            {
                modules.ForEach(m => m.Dispose());
                subscribers.ForEach(m => m.Dispose());
            }

            base.Dispose(dispose);
        }

        public void Merge(IServiceCollection services)
        {
            foreach (var service in services)
            {
                if (!this.services.Contains(service))
                {
                    this.services.Add(service);
                }
            }
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
