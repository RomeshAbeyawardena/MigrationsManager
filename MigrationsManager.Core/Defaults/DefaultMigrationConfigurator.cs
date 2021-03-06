using MigrationsManager.Core.Defaults.Builders;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
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

        public DefaultMigrationConfigurator(IServiceProvider serviceProvider)
        {
            builder = new DefaultMigrationConfiguratorOptionsBuilder(serviceProvider, new List<Type>(), new Dictionary<Type, ITableConfiguration>());
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
