using MigrationsManager.Shared.Contracts;
using System;

namespace MigrationsManager.Shared
{
    /// <inheritdoc cref="IMigrationConfigurator" />
    public class MigrationConfigurator : IMigrationConfigurator
    {
        public IMigrationConfigurator Configure(Action<IMigrationConfiguratorOptionsBuilder> configure)
        {
            throw new NotImplementedException();
        }
    }
}
