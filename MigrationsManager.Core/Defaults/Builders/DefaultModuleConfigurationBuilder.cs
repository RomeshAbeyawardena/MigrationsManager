using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Core.Defaults.Options;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults.Builders
{
    public class DefaultModuleConfigurationBuilder : IModuleConfigurationBuilder
    {
        private readonly IServiceCollection services;
        private readonly DefaultModuleAssemblyLocatorOptions moduleAssemblyOptions;

        public DefaultModuleConfigurationBuilder(IServiceCollection services)
        {
            moduleAssemblyOptions = new DefaultModuleAssemblyLocatorOptions();
            this.services = services;
        }

        public IModuleStartup Build()
        {
            services.AddSingleton<IDictionary<Assembly, IAssemblyOptions>>(moduleAssemblyOptions);
            return new DefaultModuleStartup(services, new DefaultModuleRunner(services.BuildServiceProvider(), 
                new DefaultModuleOptions(moduleAssemblyOptions)));
        }

        public IModuleStartup Build(Action<IModuleConfigurationBuilder> configure)
        {
            configure?.Invoke(this);
            return Build();
        }

        public IModuleConfigurationBuilder ConfigureAssemblies(Action<IModuleAssemblyOptions> configure)
        {
            configure(moduleAssemblyOptions);
            return this;
        }

        public IModuleConfigurationBuilder ConfigureAssemblies(Action<IModuleAssemblyLocatorOptions> configure)
        {
            configure(moduleAssemblyOptions);
            return this;
        }
    }
}
