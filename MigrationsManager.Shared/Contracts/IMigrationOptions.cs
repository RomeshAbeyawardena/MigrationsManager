using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IMigrationOptions
    {
        IEnumerable<Type> Types { get; }
        IDictionary<Type, ITableConfiguration> TableConfiguration { get; }
    }
}
