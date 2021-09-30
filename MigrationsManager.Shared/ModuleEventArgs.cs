using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared
{
    public class ModuleEventArgs
    {
        public ModuleEventArgs(object moduleInstance, bool isRunning)
        {
            ModuleInstance = moduleInstance;
            IsRunning = isRunning;
        }

        public object ModuleInstance { get; }
        public bool IsRunning { get; set; }
    }
}
