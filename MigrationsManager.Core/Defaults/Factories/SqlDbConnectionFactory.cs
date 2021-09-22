using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults.Factories
{
    [RegisterService]
    public class SqlDbConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection GetDbConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
