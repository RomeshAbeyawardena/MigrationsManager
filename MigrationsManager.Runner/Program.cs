using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MigrationsManager.Core.Defaults.Options;
using MigrationsManager.Extensions;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Contracts.Factories;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Migratons manager runner");

            var serviceCollections = new ServiceCollection();
            var services = serviceCollections
                .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .AddJsonFile("app.settings.json").Build())
                .AddLogging(c => c.AddConsole());

            await services
                .AddModules(b => b.ConfigureAssemblies(c =>
                {
                    c.AddAssembly<Program>(new DefaultAssemblyOptions { Discoverable = true, OnStartup = true });
                    c.AddAssembly("D:\\dev\\source\\MigrationsManager\\Tests\\MigrationManager.TestDomainProject\\bin\\Debug\\netstandard2.0\\MigrationManager.TestDomainProject.dll", 
                        new DefaultAssemblyOptions { Discoverable = true, OnStartup = true }); 
                }))
                .Build()
                .Run(CancellationToken.None);

        }
    }
}
