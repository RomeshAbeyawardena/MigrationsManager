using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<IKeyValuePair<TKey, TValue>> values)
        {
            foreach (var value in values)
            {
                dictionary.Add(value.GetKeyValuePair());
            }
        }
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach(var value in values)
            {
                dictionary.Add(value);
            }
        }
    }
}
