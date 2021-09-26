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
    [RegisterService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped)]
    public class SqlDbConnectionFactory : IDbConnectionFactory
    {
        private IDbConnection dbConnection;
        public IDbConnection GetDbConnection(string connectionString)
        {
            return dbConnection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            dbConnection?.Dispose();
        }
    }
}
