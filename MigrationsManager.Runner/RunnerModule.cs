using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MigrationsManager.Extensions;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MigrationsManager.Runner
{
    public class RunnerModule : ModuleBase, IModule
    {
        [Resolve]
        private ILogger<RunnerModule> Logger { get; set; }
        private readonly IMigrationQueryBuilder migrationQueryBuilder;

        private static IMigrationOptions Build(IServiceProvider serviceProvider, IMigrationConfigurator migrationConfigurator)
        {
            return migrationConfigurator
                .Configure(b => b.AddAssembly(GetAssembly)
                .ConfigureDbConnectionFactory(ConfigureDbConnection))
                .Build();
        }

        private static Assembly GetAssembly(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<RunnerModule>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var assemblyPath = configuration.GetSection("assembly").Value;
            logger.LogInformation($"Loading assembly: {assemblyPath}");
            return Assembly.LoadFile(assemblyPath);
        }

        private static IDbConnection ConfigureDbConnection(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return serviceProvider.GetRequiredService<IDbConnectionFactory>()
                .GetDbConnection(configuration.GetConnectionString("default"));
        }

        public RunnerModule(IMigrationQueryBuilder migrationQueryBuilder)
        {
            this.migrationQueryBuilder = migrationQueryBuilder;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMigrationServices()
                .AddMigration("default", Build);
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();
            sw.Start();
            var sql = migrationQueryBuilder.BuildMigrations("sql");
            sw.Stop();
            Logger.LogInformation("Build and sql generation took {0} to generate:\r\n\t{1}", sw.Elapsed, sql);
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
