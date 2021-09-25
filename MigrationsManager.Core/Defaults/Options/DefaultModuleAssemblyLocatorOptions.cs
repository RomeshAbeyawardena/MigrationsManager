using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults.Options
{
    public class DefaultModuleAssemblyLocatorOptions : DefaultModuleAssemblyOptions, IModuleAssemblyLocatorOptions
    {
        public IModuleAssemblyOptions AddAssembly(string assemblyFilenameOrFilePath, IAssemblyOptions assemblyOptions)
        {
            Assembly.LoadFrom(assemblyFilenameOrFilePath);
            return this;
        }
    }
}
