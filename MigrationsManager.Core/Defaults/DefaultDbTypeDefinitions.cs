using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;

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
            if(definitions.TryGetValue(type, out var value))
            {
                return value;
            }

            throw new NullReferenceException();
        }

        public string GetType(string type)
        {
            return GetType(Type.GetType(type));
        }
    }
}
