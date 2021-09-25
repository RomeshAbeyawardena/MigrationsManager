using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    /// <inheritdoc cref="IModuleRunner" />
    [RegisterService]
    public class DefaultModuleRunner : ModuleBase, IModuleRunner
    {
        private readonly IServiceCollection services;
        private readonly IServiceProvider serviceProvider;
        private readonly IModuleOptions moduleOptions;
        private IEnumerable<IModule> modules;
        private IServiceProvider builtModuleServices;
        private IModuleServiceProvider moduleServiceProvider;
        private readonly Dictionary<Type, IModule> modulesCache;

        private IModuleServiceProvider ModuleServiceProvider
        {
            get
            {
                if(moduleServiceProvider == null)
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
                if(builtModuleServices == null)
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
                return Activator.CreateInstance(type) as IModule;
            }

            var parameters = defaultConstructor.GetParameters()
                .Select(a => ModuleServiceProvider.GetRequiredService(a.ParameterType))
                .ToArray();

            return Activator.CreateInstance(type, parameters) as IModule;
        }

        private void RegisterServices(Type type)
        {
            var configureServicesMethod = type.GetMethod("ConfigureServices", BindingFlags.Public | BindingFlags.Static);

            configureServicesMethod?.Invoke(null, new[] { services });
        }

        public DefaultModuleRunner(IServiceProvider serviceProvider, IModuleOptions moduleOptions)
        {
            services = new ServiceCollection();
            modulesCache = new Dictionary<Type, IModule>();
            this.serviceProvider = serviceProvider;
            this.moduleOptions = moduleOptions;
        }

        public void Configure(Action<IServiceCollection> configureServices)
        {
            configureServices(services);
        }

        public override Task Run(CancellationToken cancellationToken)
        {
            services.AddSingleton(s => ModuleServiceProvider);

            services.AddSingleton(s => GetModuleTypes(moduleOptions.ModuleAssembliesOptions.GetAssemblies(a => a.Injectable && a.Discoverable))
                .Select(Activate));

            var moduleTypes = GetModuleTypes(moduleOptions.ModuleAssembliesOptions.GetAssemblies(a => a.OnStartup && a.Discoverable));

            moduleTypes.ForEach(RegisterServices);

            modules =  moduleTypes.Select(Activate);

            return Task.WhenAll(modules.Select(m => m.Run(cancellationToken)));
        }

        public override Task Stop(CancellationToken cancellationToken)
        {
            return Task.WhenAll(modules.Select(a => a.Stop(cancellationToken)));
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
