using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    public interface IKeyValuePair<TKey, TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
        KeyValuePair<TKey, TValue> GetKeyValuePair();
    }
}
