using MigrationsManager.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MigrationsManager.Shared.Base
{
    public abstract class DictionaryBase<TKey, TValue> : IDictionary<TKey, TValue>, IEventDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> dictionary;

        public event EventHandler<DictionaryEventArgs<TValue>> EventOccurred;
        
        protected DictionaryBase(bool isReadonly = false)
        {
            dictionary = new ConcurrentDictionary<TKey, TValue>();
            IsReadOnly = isReadonly;
        }

        public TValue this[TKey key] { get => dictionary[key]; set => dictionary[key] = value; }

        public ICollection<TKey> Keys => dictionary.Keys;

        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public bool IsReadOnly { get; }

        public void Add(TKey key, TValue value)
        {
            var succeeded = dictionary.TryAdd(key, value);
            OnEventOccurred(new DictionaryEventArgs<TValue>(Enumerations.EventType.Add, succeeded, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            var succeeded = dictionary.TryRemove(key, out var value);
            OnEventOccurred(new DictionaryEventArgs<TValue>(Enumerations.EventType.Remove, succeeded, value));
            return succeeded;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        protected virtual void OnEventOccurred(DictionaryEventArgs<TValue> eventArgs)
        {
            EventOccurred?.Invoke(this, eventArgs);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
