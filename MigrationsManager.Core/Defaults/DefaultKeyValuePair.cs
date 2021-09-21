using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    public static class DefaultKeyValuePair
    {
        public static IKeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
        {
            return new DefaultKeyValuePair<TKey, TValue>(key, value);
        }

        public static IKeyValuePair<TKey, TValue> Create<TKey, TValue>(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return new DefaultKeyValuePair<TKey, TValue>(keyValuePair);
        }
    }
    public class DefaultKeyValuePair<TKey, TValue> : IKeyValuePair<TKey, TValue>
    {
        internal DefaultKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        internal DefaultKeyValuePair(KeyValuePair<TKey, TValue> keyValuePair)
            : this(keyValuePair.Key, keyValuePair.Value)
        {

        }

        public TKey Key { get; }

        public TValue Value { get; }

        public KeyValuePair<TKey, TValue> GetKeyValuePair()
        {
            return new KeyValuePair<TKey, TValue>(Key, Value);
        }
    }
}
