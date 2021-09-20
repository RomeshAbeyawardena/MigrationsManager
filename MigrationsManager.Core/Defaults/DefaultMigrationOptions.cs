using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;

namespace MigrationsManager.Core.Defaults
{
    public class DefaultMigrationOptions : IMigrationOptions
    {
        internal DefaultMigrationOptions(IEnumerable<Type> types, IDictionary<Type, ITableConfiguration> tableConfiguration)
        {
            Types = types;
            TableConfiguration = tableConfiguration;
        }

        public IEnumerable<Type> Types { get; }

        public IDictionary<Type, ITableConfiguration> TableConfiguration { get; }
    }
}
