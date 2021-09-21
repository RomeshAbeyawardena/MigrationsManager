using MigrationsManager.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Runner
{
    [Migration(true)]
    public class User
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
    }
}
