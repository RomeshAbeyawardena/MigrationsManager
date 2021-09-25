using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults.Options
{
    public class DefaultModuleOptions : IModuleOptions
    {
        public DefaultModuleOptions(IModuleAssemblyOptions moduleAssembliesOptions)
        {
            ModuleAssembliesOptions = moduleAssembliesOptions;
        }
        public IModuleAssemblyOptions ModuleAssembliesOptions { get; }
    }
}
