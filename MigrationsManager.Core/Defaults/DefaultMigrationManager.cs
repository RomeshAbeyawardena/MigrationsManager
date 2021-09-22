using MigrationsManager.Shared.Extensions;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MigrationsManager.Core.Defaults
{
    [RegisterService]
    public class DefaultMigrationManager : IMigrationManager
    {
        public DefaultMigrationManager(IEnumerable<IKeyValuePair<string, IMigrationOptions>> migrationOptionPairs)
        {
            migrationOptionsDictionary = new Dictionary<string, IMigrationOptions>();
            migrationOptionsDictionary.AddRange(migrationOptionPairs);
        }

        private readonly Dictionary<string, IMigrationOptions> migrationOptionsDictionary;
        private ConcurrentQueue<IMigrationOptions> migrations;
        private bool isReadOnly;
        public ConcurrentQueue<IMigrationOptions> Migrations 
        { 
            get 
            {
                if (migrations == null)
                {
                    migrations = new ConcurrentQueue<IMigrationOptions>(migrationOptionsDictionary.ToArray().Select(a => a.Value));
                    isReadOnly = true;
                }
                return migrations; 
            } 
        }

        public int Count => Migrations.Count;

        public bool IsSynchronized => true;

        public object SyncRoot => new object();

        public void Add(string name, IMigrationOptions migrationOptions)
        {
            if (!isReadOnly)
            {
                migrationOptionsDictionary.Add(name, migrationOptions);
            }

            throw new NotSupportedException("Migration manager is in read-only mode, it can only be used for retrieving migration options, in its current state");
        }


        public IMigrationOptions GetMigrationOptions(string name)
        {
            if(migrationOptionsDictionary.TryGetValue(name, out var options))
            {
                return options;
            }

            return null;
        }

        public void CopyTo(IMigrationOptions[] array, int index)
        {
            throw new NotSupportedException();
        }

        public IMigrationOptions[] ToArray()
        {
            return Migrations.ToArray();
        }

        public bool TryAdd(IMigrationOptions item)
        {
            throw new NotSupportedException();
        }

        public bool TryTake(out IMigrationOptions item)
        {
            return Migrations.TryDequeue(out item);
        }

        public IEnumerator<IMigrationOptions> GetEnumerator()
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
