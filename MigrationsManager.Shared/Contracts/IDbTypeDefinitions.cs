using System;

namespace MigrationsManager.Shared.Contracts
{
    public interface IDbTypeDefinitions
    {
        string GetType(Type type);
        string GetType(string type);
    }
}
