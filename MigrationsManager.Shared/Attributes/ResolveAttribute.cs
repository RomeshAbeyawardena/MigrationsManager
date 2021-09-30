using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class ResolveAttribute : Attribute
    {
        public ResolveAttribute(Type resolverType = null)
        {
            ResolverType = resolverType;
        }

        public Type ResolverType { get; }
    }
}
