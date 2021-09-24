﻿using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults.Options
{
    public class ModuleAssemblyOptions : DictionaryBase<Assembly, IAssemblyOptions>, IModuleAssemblyOptions
    {
        public ModuleAssemblyOptions()
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

        public IEnumerable<Assembly> GetAssemblies(Func<IAssemblyOptions, bool> filterOptions)
        {
            return dictionary
                .Where((k) => filterOptions(k.Value))
                .Select(a => a.Key);
        }
    }
}
