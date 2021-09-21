using Microsoft.Extensions.DependencyInjection;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Contracts.Factories;
using System;
using System.Linq;

namespace MigrationsManager.Core.Defaults.Factories
{
    [RegisterService]
    public class DefaultDatabaseQueryBuilderFactory : IDatabaseQueryBuilderFactory
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultDatabaseQueryBuilderFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IDatabaseQueryBuilder GetDatabaseQueryBuilder(string name)
        {
            var databaseQueryBuilders = serviceProvider.GetServices<IDatabaseQueryBuilder>();
            return databaseQueryBuilders.FirstOrDefault(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
