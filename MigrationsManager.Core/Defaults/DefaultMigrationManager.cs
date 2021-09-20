using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    [RegisterService]
    public class DefaultMigrationManager : IMigrationManager
    {
        private readonly Dictionary<string, Func<IServiceProvider, IMigrationOptions>> migrationOptionsDictionary;

        public void Add(string name, Func<IServiceProvider, IMigrationOptions> migrationOptions)
        {
            migrationOptionsDictionary.Add(name, migrationOptions);
        }

        public IMigrationOptions GetMigrationOptions(string name, IServiceProvider service)
        {
            if(migrationOptionsDictionary.TryGetValue(name, out var options))
            {
                return options?.Invoke(service);
            }

            return null;
        }
    }
}
