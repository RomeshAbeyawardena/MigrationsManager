using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IMigrationManager : IProducerConsumerCollection<IMigrationOptions>
    {
        IMigrationOptions GetMigrationOptions(string name);
        void Add(string name, IMigrationOptions migrationOptions);
    }
}
