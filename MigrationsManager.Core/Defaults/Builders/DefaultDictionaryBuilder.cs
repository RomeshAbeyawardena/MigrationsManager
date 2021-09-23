using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using System.Collections.Generic;

namespace MigrationsManager.Core.Defaults.Builders
{
    public class DefaultDictionaryBuilder<TKey, TValue> : DictionaryBase<TKey, TValue>, IDictionaryBuilder<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Dictionary => dictionary;

        public IDictionaryBuilder<TKey, TValue> Add(IKeyValuePair<TKey, TValue> keyValuePair)
        {
            base.Add(keyValuePair.GetKeyValuePair());
            return this;
        }

        IDictionaryBuilder<TKey, TValue> IDictionaryBuilder<TKey, TValue>.Add(TKey key, TValue value)
        {
            base.Add(key, value);
            return this;
        }

        IDictionaryBuilder<TKey, TValue> IDictionaryBuilder<TKey, TValue>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            base.Add(keyValuePair);
            return this;
        }
    }
}
