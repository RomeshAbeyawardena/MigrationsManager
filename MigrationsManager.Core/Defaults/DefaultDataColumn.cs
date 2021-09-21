using MigrationsManager.Shared.Contracts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MigrationsManager.Core.Defaults
{
    public class DefaultDataColumn : IDataColumn
    {
        public DefaultDataColumn(PropertyInfo property)
        {
            Name = property.Name;
            Type = property.PropertyType.Name;

            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

            if(columnAttribute != null)
            {
                Name = columnAttribute.Name ?? Name;
                Type = columnAttribute.TypeName ?? Type;
            }

            var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();

            if(defaultValueAttribute != null)
            {
                DefaultValue = defaultValueAttribute.Value;
            }
        }

        public string Name { get; }
        public string Type { get; }
        public object DefaultValue { get; }
    }
}
