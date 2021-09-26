using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Defaults.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Defaults
{
    public class DefaultModuleConfiguration : IModuleConfiguration
    {
        public bool Enabled { get; set; }
        public string ModuleName { get; set; }
        public string AssemblyName { get; set; }
        public string FileName { get; set; }
        public DefaultAssemblyOptions Options { get; set; }
        IAssemblyOptions IModuleConfiguration.Options { get => Options; set => throw new NotImplementedException(); }
    }
}
