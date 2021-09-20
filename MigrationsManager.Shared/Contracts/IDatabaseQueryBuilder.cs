using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IDatabaseQueryBuilder
    {
        string Name { get; }
        string CreateTable(ITableConfiguration tableConfiguration, IEnumerable<IDataColumn> dataColumns);
        string CreateField(ITableConfiguration tableConfiguration, IDataColumn dataColumn);
        string DropField(ITableConfiguration tableConfiguration, IDataColumn dataColumn);
        string DropTable(ITableConfiguration tableConfiguration, IDataColumn dataColumn);
        string TableExists(ITableConfiguration tableConfiguration);
        string ColumnExists(ITableConfiguration tableConfiguration); 
    }
}
