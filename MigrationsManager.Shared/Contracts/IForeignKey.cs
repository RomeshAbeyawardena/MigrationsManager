using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IForeignKey
    {
        ITableConfiguration TableConfiguration { get; }
        ITableConfiguration ForeignTableConfiguration { get; }
        string ColumnName { get; }
    }
}
