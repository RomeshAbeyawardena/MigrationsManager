using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    /// <summary>
    /// Represents a top-level module used to run modules detected during the configuration process
    /// </summary>
    public interface IModuleStartup : IModule
    {
        
    }
}
