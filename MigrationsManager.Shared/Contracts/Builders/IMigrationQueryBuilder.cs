using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts.Builders
{
    public interface IMigrationQueryBuilder : IDisposable
    {
        string BuildMigrations(string dbType);
    }
}
