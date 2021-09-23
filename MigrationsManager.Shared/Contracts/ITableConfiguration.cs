using System.Collections.Generic;

namespace MigrationsManager.Shared.Contracts
{
    public interface ITableConfiguration
    {
        string PrimaryKey { get; set; }
        string TableName { get; }
        string Schema { get; }
        IEnumerable<IDataColumn> DataColumns { get; }
    }
}
