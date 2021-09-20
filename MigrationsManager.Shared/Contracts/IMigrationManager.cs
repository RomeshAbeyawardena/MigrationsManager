using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IMigrationManager
    {
        IMigrationOptions GetMigrationOptions(string name);
        void Add(string name, Func<IServiceProvider, IMigrationOptions> migrationOptions);
    }
}
