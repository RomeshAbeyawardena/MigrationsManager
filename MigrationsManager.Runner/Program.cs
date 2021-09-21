using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Extensions;
using MigrationsManager.Shared.Contracts;
using System;

namespace MigrationsManager.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serviceCollections = new ServiceCollection();
            var services = serviceCollections
                .AddMigrationServices()
                .AddMigration("default", Build)
                .BuildServiceProvider();

            var migrationManager = services.GetRequiredService<IMigrationManager>();
        }

        private static IMigrationOptions Build(IServiceProvider serviceProvider, IMigrationConfigurator migrationConfigurator)
        {
            return migrationConfigurator
                .Configure(b => b.AddAssembly<Program>(true)).Build();
        }
    }
}
