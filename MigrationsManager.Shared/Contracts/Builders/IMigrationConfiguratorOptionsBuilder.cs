using System;
using System.Data;
using System.Reflection;

namespace MigrationsManager.Shared.Contracts.Builders
{
    /// <summary>
    /// Represents the migration options builder to configure a Migration
    /// </summary>
    public interface IMigrationConfiguratorOptionsBuilder
    {
        /// <summary>
        /// Configures a factory to get an instance of the <see cref="IDbConnection"/> to be used by this migration instance
        /// </summary>
        /// <param name="connectionFactory">The factory to obtain the <see cref="IDbConnection"/></param>
        /// <returns></returns>
        IMigrationConfiguratorOptionsBuilder ConfigureDbConnectionFactory(Func<IServiceProvider, IDbConnection> connectionFactory);
        /// <summary>
        /// Configures the default schema used by this migration instance
        /// </summary>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        IMigrationConfiguratorOptionsBuilder SetDefaultSchema(string schemaName);
        /// <summary>
        /// Adds all specified types to this migration instance
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        IMigrationConfiguratorOptionsBuilder AddTypes(params Type[] types);

        /// <summary>
        /// Adds a specific type to this migration instance
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that should be added</param>
        /// <returns></returns>
        IMigrationConfiguratorOptionsBuilder AddType(Type type);

        /// <summary>
        /// Adds a specific type to this migration instance
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> that should be added</typeparam>
        /// <returns></returns>
        IMigrationConfiguratorOptionsBuilder AddType<T>();

        /// <summary>
        /// Adds all types within the specified <see cref="Assembly"/>
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to include</param>
        /// <param name="exclusive">Determines whether all classes within the <see cref="Assembly"/> should be included or just ones marked with <see cref="MigrationsManager.Shared.Attributes.MigrationAttribute"/></param>
        /// <returns></returns>
        IMigrationConfiguratorOptionsBuilder AddAssembly(Assembly assembly, bool? exclusive = true);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IMigrationConfiguratorOptionsBuilder AddAssembly<T>(bool? exclusive = true);

        /// <summary>
        /// Adds all types within the specified <see cref="Assembly"/>
        /// </summary>
        /// <param name="assemblies">The <see cref="Assembly"/> to include</param>
        /// <returns></returns>
        IMigrationConfiguratorOptionsBuilder AddAssemblies(params Assembly[] assemblies);

        /// <summary>
        /// Builds an instance of <see cref="IMigrationOptions" /> with the specified options
        /// </summary>
        /// <returns></returns>
        IMigrationOptions Build();
    }
}
