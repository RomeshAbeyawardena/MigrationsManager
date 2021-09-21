using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IEventDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        event EventHandler<DictionaryEventArgs<TValue>> EventOccurred;
    }
}
