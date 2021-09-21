using MigrationsManager.Shared.Base;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults.Builders
{
    public class DefaultDictionaryBuilder<TKey, TValue> : DictionaryBase<TKey, TValue>, IDictionaryBuilder<TKey, TValue>
    {
        public IDictionary<TKey, TValue> Dictionary => this;

        public IDictionaryBuilder<TKey, TValue> Add(IKeyValuePair<TKey, TValue> keyValuePair)
        {
            Add(keyValuePair.GetKeyValuePair());
            return this;
        }

        IDictionaryBuilder<TKey, TValue> IDictionaryBuilder<TKey, TValue>.Add(TKey key, TValue value)
        {
            Add(key, value);
            return this;
        }

        IDictionaryBuilder<TKey, TValue> IDictionaryBuilder<TKey, TValue>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Add(keyValuePair);
            return this;
        }
    }
}
