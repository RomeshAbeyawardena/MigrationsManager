using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Defaults
{
    public class DefaultModuleResult : IModuleResult
    {
        public bool IsException => Exception != null;
        public object Result { get; }
        public static IModuleResult Success(object value = null) => new DefaultModuleResult(value);
        public static IModuleResult Failed(Exception exception, bool haltable = false) => new DefaultModuleResult(null, exception, haltable);

        public DefaultModuleResult(object result, Exception exception = null, bool haltable = false)
        {
            Result = result;
            Exception = exception;
            Haltable = haltable;
        }

        public DefaultModuleResult(Exception exception)
            : this(null, exception)
        {

        }

        public Exception Exception { get; }

        public bool Haltable { get; }
    }
}
