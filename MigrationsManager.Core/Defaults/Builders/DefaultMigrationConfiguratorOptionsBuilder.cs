using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MigrationsManager.Core.Defaults.Builders
{
    /// <inheritdoc cref="IMigrationConfiguratorOptionsBuilder"/>
    public class DefaultMigrationConfiguratorOptionsBuilder : IMigrationConfiguratorOptionsBuilder
    {
        private string defaultSchema = "dbo";
        private readonly List<Type> types;
        private readonly Dictionary<Type, ITableConfiguration> tableConfiguration;
        private Func<IServiceProvider, IDbConnection> dbConnectionFactory;

        private bool HasMigrationAttributeAndEnabled(Type type)
        {
            var migrationAttribute = type.GetCustomAttribute<MigrationAttribute>();

            return migrationAttribute != null && migrationAttribute.Enabled;
        }

        internal DefaultMigrationConfiguratorOptionsBuilder(List<Type> types, Dictionary<Type, ITableConfiguration> tableConfiguration)
        {
            this.types = types;
            this.tableConfiguration = tableConfiguration;
        }

        public IMigrationConfiguratorOptionsBuilder AddAssemblies(params Assembly[] assemblies)
        {
            foreach(var assembly in assemblies)
            {
                AddAssembly(assembly, true);
            }

            return this;
        }

        public IMigrationConfiguratorOptionsBuilder AddAssembly(Assembly assembly, bool? exclusive)
        {
            var types = assembly.GetTypes();

            if (exclusive.HasValue && exclusive.Value)
            {
                types = types
                    .Where(HasMigrationAttributeAndEnabled)
                    .ToArray();
            }

            AddTypes(types);
            return this;
        }

        public IMigrationConfiguratorOptionsBuilder AddType(Type type)
        {
            types.Add(type);
            return this;
        }

        public IMigrationConfiguratorOptionsBuilder AddType<T>()
        {
            return AddType(typeof(T));
        }

        public IMigrationConfiguratorOptionsBuilder AddTypes(params Type[] types)
        {
            this.types.AddRange(types);
            return this;
        }

        public IMigrationOptions Build()
        {
            var migrationOptions = new DefaultMigrationOptions(types, tableConfiguration, dbConnectionFactory);
            migrationOptions.Set(a => a.DbConnectionFactory, dbConnectionFactory);
            foreach(var type in types)
            {
                var dataColumns = type.GetDataColumns(propertyInfo => new DefaultDataColumn(propertyInfo));
                var tableAttribute = type.GetCustomAttribute<TableAttribute>();

                var tableConfiguration = new DefaultTableConfiguration(type.Name, defaultSchema, dataColumns);

                if (tableAttribute != null)
                {
                    tableConfiguration = new DefaultTableConfiguration(tableAttribute.Schema, tableAttribute.Name, dataColumns);
                }

                var keyAttribute = type.GetCustomAttribute<KeyAttribute>();
                if (keyAttribute != null)
                {
                    tableConfiguration.PrimaryKey = tableAttribute.Name;
                }

                this.tableConfiguration.Add(type, tableConfiguration);
            }

            return migrationOptions;
        }

        public IMigrationConfiguratorOptionsBuilder SetDefaultSchema(string schemaName)
        {
            defaultSchema = schemaName;
            return this;
        }

        public IMigrationConfiguratorOptionsBuilder ConfigureDbConnectionFactory(Func<IServiceProvider, IDbConnection> dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            return this;
        }

        public IMigrationConfiguratorOptionsBuilder AddAssembly<T>(bool? exclusive = true)
        {
            return AddAssembly(typeof(T).Assembly, exclusive);
        }
    }
}
