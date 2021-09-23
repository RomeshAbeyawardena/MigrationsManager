using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Attributes
{
    /// <summary>
    /// Marks a field or property as a nullable field in a data context
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class AllowNullsAttribute : Attribute
    {
        public AllowNullsAttribute(bool enabled = true)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// Determines whether this rule should apply
        /// </summary>
        public bool Enabled { get; }
    }
}
