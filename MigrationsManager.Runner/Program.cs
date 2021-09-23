using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Extensions;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Contracts.Factories;
using System;
using System.Data;

namespace MigrationsManager.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serviceCollections = new ServiceCollection();
            var services = serviceCollections
                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddJsonFile("app.settings.json").Build())
                .AddMigrationServices()
                .AddMigration("default", Build)
                .BuildServiceProvider();

            var migrationQueryBuilder = services.GetService<IMigrationQueryBuilder>();
            var sql = migrationQueryBuilder.BuildMigrations("sql");
        }

        private static IMigrationOptions Build(IServiceProvider serviceProvider, IMigrationConfigurator migrationConfigurator)
        {
            return migrationConfigurator
                .Configure(b => b.AddAssembly<Program>(true)
                .ConfigureDbConnectionFactory(ConfigureDbConnection))
                .Build();
        }

        private static IDbConnection ConfigureDbConnection(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return serviceProvider.GetRequiredService<IDbConnectionFactory>().GetDbConnection(configuration.GetConnectionString("default"));
        }
    }
}
