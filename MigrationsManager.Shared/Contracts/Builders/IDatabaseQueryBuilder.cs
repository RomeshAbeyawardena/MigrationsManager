using System;
using System.Collections.Generic;

namespace MigrationsManager.Shared.Contracts.Builders
{
    public interface IDatabaseQueryBuilder
    {
        /// <summary>
        /// Gets the name of the <see cref="IDatabaseQueryBuilder"/>
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Generates a command string to create a table
        /// </summary>
        /// <param name="tableConfiguration"></param>
        /// <param name="dataColumns"></param>
        /// <returns></returns>
        string CreateTable(ITableConfiguration tableConfiguration, IEnumerable<IDataColumn> dataColumns);

        /// <summary>
        /// Generates a command string to add a field to a table
        /// </summary>
        /// <param name="tableConfiguration"></param>
        /// <param name="dataColumn"></param>
        /// <returns></returns>
        string CreateField(ITableConfiguration tableConfiguration, IDataColumn dataColumn);

        /// <summary>
        /// Generates a command to drop an existing field field from a table
        /// </summary>
        /// <param name="tableConfiguration"></param>
        /// <param name="dataColumn"></param>
        /// <returns></returns>
        string DropField(ITableConfiguration tableConfiguration, IDataColumn dataColumn);

        /// <summary>
        /// Generates a command to drop a table
        /// </summary>
        /// <param name="tableConfiguration"></param>
        /// <returns></returns>
        string DropTable(ITableConfiguration tableConfiguration);

        /// <summary>
        /// Generates a query to determine whether a table already exists
        /// </summary>
        /// <param name="tableConfiguration"></param>
        /// <returns></returns>
        string TableExists(ITableConfiguration tableConfiguration);

        /// <summary>
        /// Generates a query to determine whether a column within a table already exists
        /// </summary>
        /// <param name="tableConfiguration"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        string ColumnExists(ITableConfiguration tableConfiguration, string columnName);

        /// <summary>
        /// Gets a DbType from a <see cref="Type"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetDbType(Type type);

        /// <summary>
        /// Gets a DbType from a text type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetDbType(string type);
    }
}
