using MigrationsManager.Shared.Contracts.Builders;

namespace MigrationsManager.Shared.Contracts.Factories
{
    public interface IDatabaseQueryBuilderFactory
    {
        IDatabaseQueryBuilder GetDatabaseQueryBuilder(string name);
    }
}
