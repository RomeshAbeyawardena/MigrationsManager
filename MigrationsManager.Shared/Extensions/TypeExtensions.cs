using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MigrationsManager.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<IDataColumn> GetDataColumns(this Type valueType, Func<PropertyInfo, IDataColumn> newDataColumn)
        {
            var dataColumnList = new List<IDataColumn>();
            foreach (var property in valueType.GetProperties())
            {
                dataColumnList.Add(newDataColumn(property));
            }

            return dataColumnList;
        }
    }
}
