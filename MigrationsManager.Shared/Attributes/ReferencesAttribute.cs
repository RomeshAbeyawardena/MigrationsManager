using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ReferencesAttribute : Attribute
    {
        public ReferencesAttribute(Type type, string fieldOrPropertyName)
        {
            ParentType = type;
            FieldOrPropertyName = fieldOrPropertyName;
        }

        public ReferencesAttribute(string tableName, string columnName, string schemaName = default)
        {
            IsDbInfoResolved = true;
            TableName = tableName;
            ColumnName = columnName;
            SchemaName = schemaName;
        }

        public string TableName { get; }
        public string ColumnName { get; }
        public string SchemaName { get; }
        public bool IsDbInfoResolved { get; }
        public Type ParentType { get; }
        public string FieldOrPropertyName { get; }
        public ITableConfiguration TableConfiguration => new DefaultTableConfiguration(TableName, SchemaName, Array.Empty<IDataColumn>());
    }
}
