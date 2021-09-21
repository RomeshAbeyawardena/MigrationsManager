using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Factories;
using MigrationsManager.Shared.Extensions;
using System;
using System.Collections.Generic;

namespace MigrationsManager.Core.Defaults.Factories
{
    [RegisterService]
    public class DefaultDbTypeDefinitionsFactory : DictionaryBase<string, IDbTypeDefinitions>, IDbTypeDefinitionsFactory
    {
        public DefaultDbTypeDefinitionsFactory(IEnumerable<IKeyValuePair<string, IDbTypeDefinitions>> definitions)
        {
            this.AddRange(definitions);
        }

        public IDbTypeDefinitions GetDbTypeDefinitions(string name)
        {
            if(this.TryGetValue(name, out var dbTypeDefinitions))
            {
                return dbTypeDefinitions;
            }

            throw new NullReferenceException();
        }
    }
}
