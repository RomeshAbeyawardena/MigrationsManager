using System.Collections.Generic;

namespace MigrationsManager.Shared.Contracts
{
    public interface IKeyValuePair<TKey, TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
        KeyValuePair<TKey, TValue> GetKeyValuePair();
    }
}
