using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Defaults
{
    public class DefaultModulesConfiguration : IModulesConfiguration
    {
        public IEnumerable<DefaultModuleConfiguration> Modules { get; set; }
        public string IncludePath { get; set; }
        IEnumerable<IModuleConfiguration> IModulesConfiguration.Modules { get => Modules; set => throw new NotSupportedException(); }
    }
}
