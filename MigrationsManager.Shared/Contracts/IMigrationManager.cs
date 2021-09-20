using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IMigrationManager : IProducerConsumerCollection<Func<IServiceProvider, IMigrationOptions>>
    {
        IMigrationOptions GetMigrationOptions(string name, IServiceProvider serviceProvider);
        void Add(string name, Func<IServiceProvider, IMigrationOptions> migrationOptions);
    }
}
