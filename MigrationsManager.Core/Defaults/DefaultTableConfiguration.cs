﻿using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    public class DefaultTableConfiguration : ITableConfiguration
    {
        public string PrimaryKey { get; set; }
        public string TableName { get; }
        public string Schema { get; }
        public IEnumerable<IDataColumn> DataColumns { get; }

        internal DefaultTableConfiguration(string tableName, string schema, IEnumerable<IDataColumn> dataColumns)
        {
            TableName = tableName;
            Schema = schema;
            DataColumns = dataColumns;
        }

    }
}
