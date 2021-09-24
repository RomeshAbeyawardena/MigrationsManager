using Dapper;
using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Builders
{
    [RegisterService(ServiceLifetime.Transient)]
    public class DefaultMigrationQueryBuilder : IMigrationQueryBuilder
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IDatabaseQueryBuilderFactory databaseQueryBuilderFactory;
        private readonly IMigrationManager migrationsManager;
        private IServiceScope scope;

        public DefaultMigrationQueryBuilder(
            IServiceProvider serviceProvider,
            IDatabaseQueryBuilderFactory databaseQueryBuilderFactory, 
            IMigrationManager migrationsManager)
        {
            this.serviceProvider = serviceProvider;
            this.databaseQueryBuilderFactory = databaseQueryBuilderFactory;
            this.migrationsManager = migrationsManager;
        }

        public string BuildMigrations(string dbType)
        {
            var queryBuilder = databaseQueryBuilderFactory.GetDatabaseQueryBuilder(dbType);
            var queryStringBuilder = new StringBuilder();
            while(migrationsManager.TryTake(out var migrationOptions))
            {
                scope = serviceProvider.CreateScope();
                var dbConnection = migrationOptions.DbConnectionFactory(scope.ServiceProvider);
                dbConnection.Open();
                var tableConfigurations = migrationOptions.TableConfiguration.Select(t => t.Value);

                foreach(var tableConfiguration in tableConfigurations)
                {
                    if (!dbConnection.ExecuteScalar<bool>(queryBuilder.TableExists(tableConfiguration)))
                    {
                        queryStringBuilder.AppendLine(queryBuilder.CreateTable(tableConfiguration, tableConfiguration.DataColumns));
                    }
                    else
                    {
                        foreach (var column in tableConfiguration.DataColumns)
                        {
                            if(!dbConnection.ExecuteScalar<bool>(queryBuilder.ColumnExists(tableConfiguration, column.Name)))
                            {
                                queryStringBuilder.AppendLine(queryBuilder.CreateField(tableConfiguration, column));
                            }
                        }
                    }
                }
            }

            return queryStringBuilder.ToString();
        }

        public void Dispose()
        {
            scope.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
