using MigrationsManager.Shared.Contracts;
using System.Collections.Generic;

namespace MigrationsManager.Shared.Defaults
{
    public class DefaultTableConfiguration : ITableConfiguration
    {
        public string PrimaryKey { get; set; }
        public string TableName { get; }
        public string Schema { get; }
        public IEnumerable<IDataColumn> DataColumns { get; }

        public DefaultTableConfiguration(string tableName, string schema, IEnumerable<IDataColumn> dataColumns)
        {
            TableName = tableName;
            Schema = schema;
            DataColumns = dataColumns;
        }

    }
}
