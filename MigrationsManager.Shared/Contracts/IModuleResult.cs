using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IModuleResult
    {
        bool IsException { get; }
        object Result { get; }
        Exception Exception { get; }
    }
}
