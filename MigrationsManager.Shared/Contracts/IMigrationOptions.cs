using System;
using System.Collections.Generic;

namespace MigrationsManager.Shared.Contracts
{
    public interface IMigrationOptions
    {
        IEnumerable<Type> Types { get; }
        IDictionary<Type, ITableConfiguration> TableConfiguration { get; }
    }
}
