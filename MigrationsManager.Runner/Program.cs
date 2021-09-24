using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Extensions;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Contracts.Factories;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace MigrationsManager.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serviceCollections = new ServiceCollection();
            using var services = serviceCollections
                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .AddJsonFile("app.settings.json").Build())
                .AddMigrationServices()
                .AddMigration("default", Build)
                .BuildServiceProvider();
            var sw = new Stopwatch();
            sw.Start();
            var migrationQueryBuilder = services.GetService<IMigrationQueryBuilder>();
            var sql = migrationQueryBuilder.BuildMigrations("sql");

            sw.Stop();

            Console.WriteLine("Build and sql generation took {0} to generate:\r\n\t{1}", sw.Elapsed, sql);
        }

        private static IMigrationOptions Build(IServiceProvider serviceProvider, IMigrationConfigurator migrationConfigurator)
        {
            return migrationConfigurator
                .Configure(b => b.AddAssembly(GetAssembly)
                .ConfigureDbConnectionFactory(ConfigureDbConnection))
                .Build();
        }

        private static Assembly GetAssembly(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return Assembly.LoadFile(configuration.GetSection("assembly").Value);
        }

        private static IDbConnection ConfigureDbConnection(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return serviceProvider.GetRequiredService<IDbConnectionFactory>().GetDbConnection(configuration.GetConnectionString("default"));
        }
    }
}
