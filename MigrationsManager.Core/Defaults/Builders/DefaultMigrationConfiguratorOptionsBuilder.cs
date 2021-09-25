using MigrationsManager.Core.Options;
using MigrationsManager.Shared.Attributes;
using MigrationsManager.Shared.Contracts;
using MigrationsManager.Shared.Contracts.Builders;
using MigrationsManager.Shared.Defaults;
using MigrationsManager.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MigrationsManager.Core.Defaults.Builders
{
    /// <inheritdoc cref="IMigrationConfiguratorOptionsBuilder"/>
    public class DefaultMigrationConfiguratorOptionsBuilder : IMigrationConfiguratorOptionsBuilder
    {
        private string defaultSchema = "dbo";
        private readonly IServiceProvider serviceProvider;
        private readonly List<Type> types;
        private readonly Dictionary<Type, ITableConfiguration> tableConfiguration;
        private Func<IServiceProvider, IDbConnection> dbConnectionFactory;

        private bool HasMigrationAttributeAndEnabled(Type type)
        {
            var migrationAttribute = type.GetCustomAttribute<MigrationAttribute>();

            return migrationAttribute != null && migrationAttribute.Enabled;
        }

        internal DefaultMigrationConfiguratorOptionsBuilder(IServiceProvider serviceProvider, List<Type> types, Dictionary<Type, ITableConfiguration> tableConfiguration)
        {
            this.serviceProvider = serviceProvider;
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
                    .OrderBy(OrderbyOrderId)
                    .ToArray();
            }

            AddTypes(types);
            return this;
        }

        private object OrderbyOrderId(Type arg)
        {
            var migrationAttribute = arg.GetCustomAttribute<MigrationAttribute>();
            if (migrationAttribute == null)
            {
                return arg.Name;
            }

            return migrationAttribute.OrderId;
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
            var sw = new Stopwatch();
            sw.Start();
            var migrationOptions = new DefaultMigrationOptions(types, tableConfiguration, dbConnectionFactory);
            migrationOptions.Set(a => a.DbConnectionFactory, dbConnectionFactory);
            foreach(var type in types)
            {
                var dataColumns = new List<IDataColumn>();

                var tableConfiguration = new DefaultTableConfiguration(type.Name, defaultSchema, dataColumns);
                dataColumns.AddRange(type.GetDataColumns(propertyInfo => new DefaultDataColumn(tableConfiguration, propertyInfo)));
                var tableAttribute = type.GetCustomAttribute<TableAttribute>();

                if (tableAttribute != null)
                {
                    tableConfiguration = new DefaultTableConfiguration(tableAttribute.Schema, tableAttribute.Name, dataColumns);
                }

                this.tableConfiguration.Add(type, tableConfiguration);
            }
            sw.Stop();

            Console.WriteLine("Migration build took {0}", sw.Elapsed);
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

        public IMigrationConfiguratorOptionsBuilder AddAssembly(Func<IServiceProvider, Assembly> assembly, bool? exclusive = true)
        {
            return AddAssembly(assembly.Invoke(serviceProvider), exclusive);
        }
    }
}
