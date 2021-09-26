using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IModulesConfiguration
    {
        IEnumerable<IModuleConfiguration> Modules { get; set; }
    }
}
