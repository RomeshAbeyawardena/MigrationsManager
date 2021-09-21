using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Core;
using MigrationsManager.Core.Defaults;
using MigrationsManager.Core.Defaults.Builders;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigrationServices(this IServiceCollection services)
        {
            return services.Scan(s => s.FromAssembliesOf(typeof(This))
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Singleton))
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Scoped))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Transient))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }

        public static IServiceCollection AddMigration(this IServiceCollection service, string migrationName, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> build)
        {
            return service.AddSingleton(s => s.ConfigureMigration(migrationName, build));
        }

        public static IKeyValuePair<string, IMigrationOptions> ConfigureMigration(this IServiceProvider serviceProvider, string migrationName, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> build)
        {
            var migrationConfigurator = serviceProvider.GetRequiredService<IMigrationConfigurator>();
            var migrationOptions = build(serviceProvider, migrationConfigurator);
            return DefaultKeyValuePair.Create(migrationName, migrationOptions);
        }

        public static IServiceCollection AddDbTypeDefinitions(this IServiceCollection services, Action<IDictionaryBuilder<Type, string>> build)
        {
            var dictionaryBuilder = new DefaultDictionaryBuilder<Type, string>();

            build?.Invoke(dictionaryBuilder);
            services.AddSingleton(dictionaryBuilder.Dictionary);
        }
    }
}
