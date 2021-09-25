using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts.Builders
{
    /// <summary>
    /// Represents a builder to build multiple module configurations
    /// </summary>
    public interface IModuleConfigurationBuilder
    {
        /// <summary>
        /// Configures locally-loaded assemblies for extraction of <see cref="IModule">modules</see>
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        IModuleConfigurationBuilder ConfigureAssemblies(Action<IModuleAssemblyOptions> configure);
        /// <summary>
        /// Configures remote assemblies for extraction of <see cref="IModule">modules</see>
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        IModuleConfigurationBuilder ConfigureAssemblies(Action<IModuleAssemblyLocatorOptions> configure);
        /// <summary>
        /// Builds the module configuration
        /// </summary>
        /// <returns></returns>
        IModuleStartup Build();
        /// <summary>
        /// Builds the module configuration
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        IModuleStartup Build(Action<IModuleConfigurationBuilder> configure);
    }
}
