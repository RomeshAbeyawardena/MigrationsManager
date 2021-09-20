using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Builders
{
    public class SqlDatabaseQueryBuilder : IDatabaseQueryBuilder
    {
        public string Name => "Sql";

        public string ColumnExists(ITableConfiguration tableConfiguration)
        {
            throw new NotImplementedException();
        }

        public string CreateField(ITableConfiguration tableConfiguration, IDataColumn dataColumn)
        {
            throw new NotImplementedException();
        }

        public string CreateTable(ITableConfiguration tableConfiguration, IEnumerable<IDataColumn> dataColumns)
        {
            var queryBuilder = new StringBuilder($"CREATE TABLE [{tableConfiguration.Schema}].[{tableConfiguration.TableName}] (");

            foreach (var dataColumn in dataColumns)
            {
                
                queryBuilder.Append($",[{dataColumn.Name}] {dataColumn.Type}");
            }

            queryBuilder.Append($")");

            return queryBuilder.ToString();
        }

        public string DropField(ITableConfiguration tableConfiguration, IDataColumn dataColumn)
        {
            throw new NotImplementedException();
        }

        public string DropTable(ITableConfiguration tableConfiguration, IDataColumn dataColumn)
        {
            throw new NotImplementedException();
        }

        public string TableExists(ITableConfiguration tableConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
