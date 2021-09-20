using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Core;
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
        public static IServiceCollection AddMigrationServices(IServiceCollection services)
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

        public static IServiceCollection AddMigration(IServiceCollection service, string migrationName, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> build)
        {
            return service.AddScoped(s => ConfigureMigration(s, migrationName, build));
        }

        public static IMigrationOptions ConfigureMigration(IServiceProvider serviceProvider, string migrationName, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> build)
        {

            var migrationManager = serviceProvider.GetRequiredService<IMigrationManager>();
            var migrationConfigurator = serviceProvider.GetRequiredService<IMigrationConfigurator>();
            var migrationOptions = build(serviceProvider, migrationConfigurator);
            migrationManager.Add(migrationName, migrationOptions);
            return migrationOptions;
        }
    }
}
