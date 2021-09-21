using System.Collections.Generic;

namespace MigrationsManager.Shared.Contracts.Factories
{
    public interface IDbTypeDefinitionsFactory : IDictionary<string, IDbTypeDefinitions>
    {
        IDbTypeDefinitions GetDbTypeDefinitions(string name);
    }
}
