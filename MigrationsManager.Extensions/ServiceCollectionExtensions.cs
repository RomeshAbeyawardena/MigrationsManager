using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Core;
using MigrationsManager.Core.Defaults;
using MigrationsManager.Core.Defaults.Builders;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using System;

namespace MigrationsManager.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds modules to be loaded and prepared in memory.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="build"></param>
        /// <returns></returns>
        public static IModuleConfigurationBuilder AddModules(this IServiceCollection services,
            Action<IModuleConfigurationBuilder> build)
        {
            var defaultModuleConfigurationBuilder = new DefaultModuleConfigurationBuilder(services);

            build(defaultModuleConfigurationBuilder);

            return defaultModuleConfigurationBuilder;
        }

        public static IServiceCollection AddMigrationServices(this IServiceCollection services)
        {
            return services
                .Scan(s => s.FromAssembliesOf(typeof(This))
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Singleton))
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Scoped))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Transient))
                .AsImplementedInterfaces()
                .WithTransientLifetime())
                .AddDbTypeDefinitions("Sql", dictionaryBuilder => dictionaryBuilder
                    .Add(typeof(Guid), "UNIQUEIDENTIFIER")
                    .Add(typeof(short), "SMALLINT")
                    .Add(typeof(DateTimeOffset), "DATETIMEOFFSET")
                    .Add(typeof(DateTime), "DATETIME")
                    .Add(typeof(bool), "BIT")
                    .Add(typeof(byte), "TINYINT")
                    .Add(typeof(int), "INT")
                    .Add(typeof(string), "VARCHAR(#length)")
                    .Add(typeof(float), "FLOAT")
                    .Add(typeof(long), "BIGINT")
                    .Add(typeof(decimal), "DECIMAL(#length)"));
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

        public static IServiceCollection AddDbTypeDefinitions(this IServiceCollection services, string dbType, Action<IDictionaryBuilder<Type, string>> build)
        {
            var dictionaryBuilder = new DefaultDictionaryBuilder<Type, string>();

            build?.Invoke(dictionaryBuilder);
            return services.AddSingleton(DefaultKeyValuePair.Create(dbType, DefaultDbTypeDefinitions.Create(dictionaryBuilder.Dictionary)));
        }
    }
}
