using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Contracts.Factories;
using MigrationsManager.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MigrationsManager.Core.Builders
{
    [RegisterService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient)]
    public class SqlDatabaseQueryBuilder : IDatabaseQueryBuilder
    {
        private const string DefaultLength = "MAX";
        private readonly IDbTypeDefinitions dbTypeDefinitions;

        public string Name => "Sql";

        private static string BuildConstraint(ConstraintType constraintType, ITableConfiguration tableConfiguration, 
            IDataColumn dataColumn, IForeignKey foreignKey = null, string constraintName = default)
        {
            if (string.IsNullOrWhiteSpace(constraintName))
            {
                constraintName = $"{tableConfiguration.TableName.ToUpper()}_{dataColumn.Name}";
            }

            switch (constraintType)
            {
                case ConstraintType.PrimaryKey:
                    return $"\tCONSTRAINT PK_{constraintName} PRIMARY KEY";
                case ConstraintType.ForeignKey:
                    if(foreignKey == null)
                    {
                        throw new NullReferenceException();
                    }

                    return $"\tCONSTRAINT FK_{constraintName} REFERENCES [{foreignKey.ForeignTableConfiguration.Schema}].[{foreignKey.ForeignTableConfiguration.TableName}]([{foreignKey.ColumnName}])";
                case ConstraintType.Default:
                    return $"\tCONSTRAINT DF_{constraintName} DEFAULT {dataColumn.DefaultValue}";
                default:
                    throw new NotSupportedException();
            }
        }

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

        public string CreateField(ITableConfiguration tableConfiguration, IDataColumn dataColumn, bool isCreateTableSyntax = false)
        {
            var queryBuilder = new StringBuilder(isCreateTableSyntax 
                ? string.Empty 
                : $"ALTER TABLE [{tableConfiguration.Schema}].[{tableConfiguration.TableName}]" +
                $"{Environment.NewLine}ADD ");

            var nullArgument = dataColumn.IsRequired ? "NOT NULL" : "NULL";

            var dbType = GetDbType(dataColumn.Type)?.Replace("#length", dataColumn.Length?.ToString() ?? DefaultLength);
            queryBuilder.AppendLine($"[{dataColumn.Name}] {dbType} {nullArgument}");

            if (tableConfiguration.PrimaryKey != null && tableConfiguration.PrimaryKey.Equals(dataColumn.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                queryBuilder.AppendLine(CreateConstraint(ConstraintType.PrimaryKey, tableConfiguration, dataColumn, isCreateTableSyntax: true));
            }
            else if(dataColumn.ForeignKeys.Any())
            {
                foreach(var foreignKey in dataColumn.ForeignKeys)
                {
                    queryBuilder.AppendLine(CreateConstraint(ConstraintType.ForeignKey, tableConfiguration, dataColumn, foreignKey, true));
                }
            }


            if (!string.IsNullOrWhiteSpace(dataColumn.DefaultValue?.ToString()))
            {
                queryBuilder.AppendLine(CreateConstraint(ConstraintType.Default, tableConfiguration, dataColumn, isCreateTableSyntax: true));
            }

            return queryBuilder.ToString();
        }

        public string CreateTable(ITableConfiguration tableConfiguration, IEnumerable<IDataColumn> dataColumns)
        {
            var queryBuilder = new StringBuilder($"CREATE TABLE [{tableConfiguration.Schema}].[{tableConfiguration.TableName}] (");
            bool isFirst = true;
            foreach (var dataColumn in dataColumns)
            {
                if (!isFirst)
                {
                    queryBuilder.Append(", ");
                }
                else
                {
                    isFirst = false;
                }

                queryBuilder.AppendLine(CreateField(tableConfiguration, dataColumn, true));
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
                $"AND TABLE_SCHEMA = '{tableConfiguration.Schema}') " +
                "SELECT 1 ELSE SELECT 0";
        }

        public string CreateConstraint(ConstraintType constraintType, ITableConfiguration tableConfiguration, 
            IDataColumn dataColumn, IForeignKey foreignKey = null, bool isCreateTableSyntax = false, string constraintName = default)
        {
            var sqlQueryBuilder = new StringBuilder();
            if (!isCreateTableSyntax)
            {
                sqlQueryBuilder.Append($"ALTER TABLE [{tableConfiguration.Schema}].[{tableConfiguration.TableName}]" +
                    $"ADD ");
            }

            sqlQueryBuilder.Append(BuildConstraint(constraintType, tableConfiguration, dataColumn, foreignKey, constraintName));

            return sqlQueryBuilder.ToString();
        }
    }
}
