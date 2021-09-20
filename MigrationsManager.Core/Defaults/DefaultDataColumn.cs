using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    public class DefaultDataColumn : IDataColumn
    {
        public DefaultDataColumn(PropertyInfo property)
        {
            Name = property.Name;
            Type = property.PropertyType.Name;
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
