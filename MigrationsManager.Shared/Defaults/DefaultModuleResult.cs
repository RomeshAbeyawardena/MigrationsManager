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
        public static IModuleResult Failed(Exception exception) => new DefaultModuleResult(exception);

        public DefaultModuleResult(object result, Exception exception = null)
        {
            Result = result;
            Exception = exception;
        }

        public DefaultModuleResult(Exception exception)
            : this(null, exception)
        {

        }

        public Exception Exception { get; }
    }
}
