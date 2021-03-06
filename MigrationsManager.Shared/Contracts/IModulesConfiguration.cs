using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IModulesConfiguration : IIncludeConfiguration
    {
        IEnumerable<IModuleConfiguration> Modules { get; set; }
    }
}
