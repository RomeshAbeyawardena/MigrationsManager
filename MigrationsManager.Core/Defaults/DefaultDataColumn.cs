using MigrationsManager.Shared.Contracts;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MigrationsManager.Core.Defaults
{
    public class DefaultDataColumn : IDataColumn
    {
        public DefaultDataColumn(PropertyInfo property)
        {
            Name = property.Name;
            Type = property.PropertyType;

            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

            if(columnAttribute != null)
            {
                Name = columnAttribute.Name ?? Name;
                TypeName = columnAttribute.TypeName;
                
            }

            var maximumAttribute = property.GetCustomAttribute<MaxLengthAttribute>();

            if(maximumAttribute != null)
            {
                Length = maximumAttribute.Length;
            }

            var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();

            if(defaultValueAttribute != null)
            {
                DefaultValue = defaultValueAttribute.Value;
            }
        }

        public string Name { get; }
        public string TypeName { get; }
        public Type Type { get; }
        public object DefaultValue { get; }

        public int? Length { get; }
    }
}
