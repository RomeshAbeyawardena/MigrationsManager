using MigrationsManager.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MigrationsManager.Shared.Base
{
    public class CollectionBase<T> : IConcurrentList<T>, IList<T>
    {
        private readonly List<T> list;

        public object SyncRoot { get; }

        public CollectionBase(bool isReadOnly = false)
        {
            SyncRoot = new object();
            IsReadOnly = isReadOnly;
            list = new List<T>();
        }

        public void InLock(Action<List<T>> action)
        {
            lock (SyncRoot)
            {
                action?.Invoke(list);
            }
        }

        public TResult InLock<TResult>(Func<List<T>, TResult> action)
        {
            lock (SyncRoot)
            {
                return action.Invoke(list);
            }
        }

        public T this[int index] { 
            get => list[index]; 
            set => InLock(l => l[index] = value); 
        }

        public int Count => list.Count;

        public bool IsReadOnly { get; }

        public void Add(T item)
        {
            InLock(l => l.Add(item));
        }

        public void Clear()
        {
            InLock(l => list.Clear());
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            InLock(l => l.Insert(index, item));
        }

        public bool Remove(T item)
        {
            return InLock(l => l.Remove(item));
        }

        public void RemoveAt(int index)
        {
            InLock(l => l.RemoveAt(index));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
