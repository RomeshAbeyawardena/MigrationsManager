using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts.Factories
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetDbConnection(string connectionString);
    }
}
