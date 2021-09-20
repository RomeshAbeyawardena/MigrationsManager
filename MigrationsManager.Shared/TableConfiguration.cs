using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared
{
    public class TableConfiguration : ITableConfiguration
    {
        public string PrimaryKey { get; set; }
        public string TableName { get; }
        public string Schema { get; }

        internal TableConfiguration(string tableName, string schema)
        {
            TableName = tableName;
            Schema = schema;
        }

        
    }
}
