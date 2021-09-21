using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Builders
{
    [RegisterService]
    public class SqlDatabaseQueryBuilder : IDatabaseQueryBuilder
    {
        private readonly IDbTypeDefinitions dbTypeDefinitions;

        public string Name => "Sql";

        public SqlDatabaseQueryBuilder(IDbTypeDefinitionsFactory dbTypeDefinitionFactory)
        {
            dbTypeDefinitions = dbTypeDefinitionFactory.GetDbTypeDefinitions(Name);
        }

        public string ColumnExists(ITableConfiguration tableConfiguration, string columnName)
        {
            return $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS " +
                $"WHERE table_name = '{tableConfiguration.TableName}' " +
                $"AND TABLE_SCHEMA = '{tableConfiguration.Schema}' AND column_name = '{columnName}'";
        }

        public string CreateField(ITableConfiguration tableConfiguration, IDataColumn dataColumn)
        {
            var queryBuilder = new StringBuilder($"ALTER TABLE [{tableConfiguration.Schema}][{tableConfiguration.TableName}]");
            queryBuilder.AppendLine($"ADD COLUMN [{dataColumn.Name}] {dataColumn.Type}");
            return queryBuilder.ToString();
        }

        public string CreateTable(ITableConfiguration tableConfiguration, IEnumerable<IDataColumn> dataColumns)
        {
            var queryBuilder = new StringBuilder($"CREATE TABLE [{tableConfiguration.Schema}].[{tableConfiguration.TableName}] (");

            foreach (var dataColumn in dataColumns)
            {
                queryBuilder.AppendLine($",[{dataColumn.Name}] {dataColumn.Type}");

                if (tableConfiguration.PrimaryKey.Equals(dataColumn.Name, StringComparison.InvariantCultureIgnoreCase))
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
            throw new NotImplementedException();
        }
    }
}
