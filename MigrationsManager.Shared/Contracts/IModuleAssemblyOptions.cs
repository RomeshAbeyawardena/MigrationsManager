using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    /// <summary>
    /// Represents options containing assemblies to pull <see cref="IModule">modules</see> from
    /// </summary>
    public interface IModuleAssemblyOptions
    {
        /// <summary>
        /// Adds an assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="assemblyOptions"></param>
        /// <returns></returns>
        IModuleAssemblyOptions AddAssembly(Assembly assembly, IAssemblyOptions assemblyOptions);
        /// <summary>
        /// Adds an assembly
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyOptions"></param>
        /// <returns></returns>
        IModuleAssemblyOptions AddAssembly<T>(IAssemblyOptions assemblyOptions);

        /// <summary>
        /// Gets an assembly based on an <see cref="IAssemblyOptions"/> filter
        /// </summary>
        /// <param name="filterOptions"></param>
        /// <returns></returns>
        IEnumerable<Assembly> GetAssemblies(Func<IAssemblyOptions, bool> filterOptions);
    }
}
