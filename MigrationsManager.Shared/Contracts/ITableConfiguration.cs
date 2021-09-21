using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface ITableConfiguration
    {
        string PrimaryKey { get; }
        string TableName { get; }
        string Schema { get; }
        IEnumerable<IDataColumn> DataColumns { get; }
    }
}
