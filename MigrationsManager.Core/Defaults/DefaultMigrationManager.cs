using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Core.Defaults
{
    [RegisterService]
    public class DefaultMigrationManager : IMigrationManager
    {
        private readonly Dictionary<string, Func<IServiceProvider, IMigrationOptions>> migrationOptionsDictionary;
        private ConcurrentQueue<Func<IServiceProvider, IMigrationOptions>> migrations;
        private bool isReadOnly;
        private ConcurrentQueue<Func<IServiceProvider, IMigrationOptions>> Migrations 
        { 
            get 
            {
                if (migrations == null)
                {
                    migrations = new ConcurrentQueue<Func<IServiceProvider, IMigrationOptions>>(migrationOptionsDictionary.ToArray().Select(a => a.Value));
                    isReadOnly = true;
                }
                return migrations; 
            } 
        }

        public int Count => Migrations.Count;

        public bool IsSynchronized => true;

        public object SyncRoot => new object();

        public void Add(string name, Func<IServiceProvider, IMigrationOptions> migrationOptions)
        {
            if (!isReadOnly)
            {
                migrationOptionsDictionary.Add(name, migrationOptions);
            }

            throw new NotSupportedException("Migration manager is in read-only mode, it can only be used for retrieving migration options, in its current state");
        }


        public IMigrationOptions GetMigrationOptions(string name, IServiceProvider service)
        {
            if(migrationOptionsDictionary.TryGetValue(name, out var options))
            {
                return options?.Invoke(service);
            }

            return null;
        }

        public void CopyTo(Func<IServiceProvider, IMigrationOptions>[] array, int index)
        {
            throw new NotSupportedException();
        }

        public Func<IServiceProvider, IMigrationOptions>[] ToArray()
        {
            return Migrations.ToArray();
        }

        public bool TryAdd(Func<IServiceProvider, IMigrationOptions> item)
        {
            throw new NotSupportedException();
        }

        public bool TryTake(out Func<IServiceProvider, IMigrationOptions> item)
        {
            return Migrations.TryDequeue(out item);
        }

        public IEnumerator<Func<IServiceProvider, IMigrationOptions>> GetEnumerator()
        {
            return Migrations.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
