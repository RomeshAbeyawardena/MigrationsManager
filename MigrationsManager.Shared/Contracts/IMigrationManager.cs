using System.Collections.Concurrent;

namespace MigrationsManager.Shared.Contracts
{
    public interface IMigrationManager : IProducerConsumerCollection<IMigrationOptions>
    {
        IMigrationOptions GetMigrationOptions(string name);
        void Add(string name, IMigrationOptions migrationOptions);
    }
}
