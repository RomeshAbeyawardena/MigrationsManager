using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Attributes
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class AllowNullsAttribute : Attribute
    {
        public AllowNullsAttribute(bool enabled = true)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; }
    }
}
