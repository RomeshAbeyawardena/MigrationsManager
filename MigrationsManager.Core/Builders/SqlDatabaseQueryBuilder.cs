using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MigrationsManager.Core.Builders
{
    [RegisterService]
    public class SqlDatabaseQueryBuilder : IDatabaseQueryBuilder
    {
        private const string DefaultLength = "MAX";
        private readonly IDbTypeDefinitions dbTypeDefinitions;

        public string Name => "Sql";

        public SqlDatabaseQueryBuilder(IDbTypeDefinitionsFactory dbTypeDefinitionFactory)
        {
            dbTypeDefinitions = dbTypeDefinitionFactory.GetDbTypeDefinitions(Name);
        }

        public string ColumnExists(ITableConfiguration tableConfiguration, string columnName)
        {
            return $"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE table_name = '{tableConfiguration.TableName}' " +
                $"AND TABLE_SCHEMA = '{tableConfiguration.Schema}' AND column_name = '{columnName}') SELECT 1 ELSE SELECT 0";
        }

        public string CreateField(ITableConfiguration tableConfiguration, IDataColumn dataColumn)
        {
            var queryBuilder = new StringBuilder($"ALTER TABLE [{tableConfiguration.Schema}][{tableConfiguration.TableName}]");
            queryBuilder.AppendLine($"ADD COLUMN [{dataColumn.Name}] {GetDbType(dataColumn.Type)}");
            return queryBuilder.ToString();
        }

        public string CreateTable(ITableConfiguration tableConfiguration, IEnumerable<IDataColumn> dataColumns)
        {
            var queryBuilder = new StringBuilder($"CREATE TABLE [{tableConfiguration.Schema}].[{tableConfiguration.TableName}] (");

            foreach (var dataColumn in dataColumns)
            {
                var dbType = GetDbType(dataColumn.Type)?.Replace("#length", dataColumn.Length?.ToString() ?? DefaultLength);
                queryBuilder.AppendLine($",[{dataColumn.Name}] {dbType}");

                if (tableConfiguration.PrimaryKey != null && tableConfiguration.PrimaryKey.Equals(dataColumn.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    queryBuilder.AppendLine($"\tCONSTRAINT PK_{tableConfiguration.TableName.ToUpper()}_{dataColumn.Name}");
                }

                if (!string.IsNullOrWhiteSpace(dataColumn.DefaultValue?.ToString()))
                {
                    queryBuilder.AppendLine($"\tCONSTRAINT DF_{tableConfiguration.TableName}_{dataColumn} DEFAULT {dataColumn.DefaultValue}");
                }
            }

            queryBuilder.AppendLine($")");

            return queryBuilder.ToString();
        }

        public string DropField(ITableConfiguration tableConfiguration, IDataColumn dataColumn)
        {
            var queryBuilder = new StringBuilder($"ALTER TABLE [{tableConfiguration.Schema}][{tableConfiguration.TableName}]");
            queryBuilder.AppendLine($"DROP COLUMN [{dataColumn.Name}]");
            return queryBuilder.ToString();
        }

        public string DropTable(ITableConfiguration tableConfiguration)
        {
            return $"DROP TABLE [{tableConfiguration.Schema}].[{tableConfiguration.TableName}]";
        }

        public string GetDbType(Type type)
        {
            return dbTypeDefinitions.GetType(type);
        }

        public string GetDbType(string type)
        {
            return GetDbType(Type.GetType(type));
        }

        public string TableExists(ITableConfiguration tableConfiguration)
        {
            return $"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE table_name = '{tableConfiguration.TableName}' " +
                $"AND TABLE_SCHEMA = '{tableConfiguration.Schema}') SELECT 1 ELSE SELECT 0";
        }
    }
}
