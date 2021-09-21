using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IDbTypeDefinitions
    {
        string GetType(Type type);
        string GetType(string type);
    }
}
