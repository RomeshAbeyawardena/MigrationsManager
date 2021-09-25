using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults.Options
{
    public class DefaultModuleAssemblyOptions : DictionaryBase<Assembly, IAssemblyOptions>, 
        IModuleAssemblyOptions, 
        IModuleAssemblyLocatorOptions
    {
        public DefaultModuleAssemblyOptions()
        {

        }

        public IModuleAssemblyOptions AddAssembly(Assembly assembly, IAssemblyOptions assemblyOptions)
        {
            Add(assembly, assemblyOptions);
            return this;
        }

        public IModuleAssemblyOptions AddAssembly<T>(IAssemblyOptions assemblyOptions)
        {
            return AddAssembly(typeof(T).Assembly, assemblyOptions);
        }

        public IModuleAssemblyOptions AddAssembly(string assemblyNameorFilePath, IAssemblyOptions assemblyOptions)
        {
            return AddAssembly(Assembly.LoadFrom(assemblyNameorFilePath), assemblyOptions);
        }

        public IEnumerable<Assembly> GetAssemblies(Func<IAssemblyOptions, bool> filterOptions)
        {
            return dictionary
                .Where((k) => filterOptions(k.Value))
                .Select(a => a.Key);
        }
    }
}
