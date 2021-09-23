using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Defaults
{
    public class DefaultForeignKey : IForeignKey
    {
        public DefaultForeignKey(ITableConfiguration tableConfiguration, ITableConfiguration foreignTableConfiguration, string columnName)
        {
            TableConfiguration = tableConfiguration;
            ForeignTableConfiguration = foreignTableConfiguration;
            ColumnName = columnName;
        }

        public string ColumnName { get; }
        public ITableConfiguration TableConfiguration { get; }
        public ITableConfiguration ForeignTableConfiguration { get; }
    }
}
