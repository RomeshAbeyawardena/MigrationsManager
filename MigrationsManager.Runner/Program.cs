using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            Console.WriteLine("Migratons manager runner");

            var serviceCollections = new ServiceCollection();
            using var services = serviceCollections
                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .AddJsonFile("app.settings.json").Build())
                .AddLogging(c => c.AddConsole())
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
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var assemblyPath = configuration.GetSection("assembly").Value;
            logger.LogInformation($"Loading assembly: {assemblyPath}");
            return Assembly.LoadFile(assemblyPath);
        }

        private static IDbConnection ConfigureDbConnection(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return serviceProvider.GetRequiredService<IDbConnectionFactory>().GetDbConnection(configuration.GetConnectionString("default"));
        }
    }
}
