using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IModuleAssemblyOptions
    {
        IModuleAssemblyOptions AddAssembly(Assembly assembly, IAssemblyOptions assemblyOptions);
        IModuleAssemblyOptions AddAssembly<T>(IAssemblyOptions assemblyOptions);
        IEnumerable<Assembly> GetAssemblies(Func<IAssemblyOptions, bool> filterOptions);
    }
}
