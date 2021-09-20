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
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>())
                .AsImplementedInterfaces());
        }

        public static IServiceCollection AddMigration(IServiceCollection service, string migrationName, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> build)
        {
            return service.AddScoped(a => build(a, a.GetRequiredService<IMigrationConfigurator>()));
        }
    }
}
