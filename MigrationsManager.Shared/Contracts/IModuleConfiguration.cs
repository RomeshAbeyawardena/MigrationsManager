using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IModuleConfiguration
    {
        bool Enabled { get; set; }
        string ModuleName { get; set; }
        string AssemblyName { get; set; }
        string FileName { get; set; }
        IAssemblyOptions Options { get; set; }
    }
}
