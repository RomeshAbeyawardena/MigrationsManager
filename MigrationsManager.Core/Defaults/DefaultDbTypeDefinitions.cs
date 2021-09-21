using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    public class DefaultDbTypeDefinitions : IDbTypeDefinitions
    {
        private readonly IDictionary<Type, string> definitions;

        public static IDbTypeDefinitions Create(IDictionary<Type, string> definitions)
        {
            return new DefaultDbTypeDefinitions(definitions);
        }

        internal DefaultDbTypeDefinitions(IDictionary<Type, string> definitions)
        {
            this.definitions = definitions;
        }

        public string GetType(Type type)
        {
            throw new NotImplementedException();
        }

        public string GetType(string type)
        {
            throw new NotImplementedException();
        }
    }
}
