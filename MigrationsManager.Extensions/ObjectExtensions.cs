using MigrationsManager.Core.Defaults;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;

namespace MigrationsManager.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<IDataColumn> GetDataColumns(this object value)
        {
            var dataColumnList = new List<IDataColumn>();
            var valueType = value.GetType();

            foreach(var property in valueType.GetProperties())
            {
                dataColumnList.Add(new DefaultDataColumn(property));
            }

            return dataColumnList;
        }
    }
}
