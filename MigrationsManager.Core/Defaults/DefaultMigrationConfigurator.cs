using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;

namespace MigrationsManager.Core.Defaults
{
    /// <inheritdoc cref="IMigrationConfigurator" />
    /// 
    [RegisterService]
    public class DefaultMigrationConfigurator : IMigrationConfigurator
    {
        private readonly IMigrationConfiguratorOptionsBuilder builder;

        public DefaultMigrationConfigurator()
        {
            builder = new DefaultMigrationConfiguratorOptionsBuilder(new List<Type>(), new Dictionary<Type, ITableConfiguration>());
        }

        public IMigrationOptions Build()
        {
            return builder.Build();
        }

        public IMigrationConfigurator Configure(Action<IMigrationConfiguratorOptionsBuilder> configure)
        {
            configure(builder);
            return this;
        }
    }
}
