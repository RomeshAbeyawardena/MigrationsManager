using System.Collections.Concurrent;

namespace MigrationsManager.Shared.Contracts
{
    public interface IMigrationManager : IProducerConsumerCollection<IMigrationOptions>
    {
        ConcurrentQueue<IMigrationOptions> Migrations { get; }
        IMigrationOptions GetMigrationOptions(string name);
        void Add(string name, IMigrationOptions migrationOptions);
    }
}
