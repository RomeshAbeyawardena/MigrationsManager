using System.Collections.Generic;

namespace MigrationsManager.Shared.Contracts.Builders
{
    public interface IDictionaryBuilder<TKey, TValue> : IDictionary<TKey, TValue>
    {
        new IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);
        new IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValuePair);
        IDictionaryBuilder<TKey, TValue> Add(IKeyValuePair<TKey, TValue> keyValuePair);

        IDictionary<TKey, TValue> Dictionary { get; }
    }
}
