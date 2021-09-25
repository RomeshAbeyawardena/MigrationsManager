using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts.Builders
{
    public interface IModuleConfigurationBuilder
    {
        IModuleConfigurationBuilder ConfigureAssemblies(Action<IModuleAssemblyOptions> configure);
        IModuleConfigurationBuilder ConfigureAssemblies(Action<IModuleAssemblyLocatorOptions> configure);
        IModuleStartup Build();
        IModuleStartup Build(Action<IModuleConfigurationBuilder> configure);
    }
}
