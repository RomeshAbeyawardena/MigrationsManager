using System;
using System.Collections.Generic;

namespace MigrationsManager.Shared.Contracts
{
    public interface IEventDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        event EventHandler<DictionaryEventArgs<TValue>> EventOccurred;
    }
}
