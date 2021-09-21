using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigrationsManager.Shared.Contracts
{
    /// <summary>
    /// Represents a class that can configure a migration on specific <see cref="Type">types</see> or all <see cref="Type">types</see> within a list of <see cref="System.Reflection.Assembly">assemblies</see>
    /// </summary>
    public interface IMigrationConfigurator
    {
        /// <summary>
        /// Configures the migration options using a default <see cref="IMigrationConfiguratorOptionsBuilder"/>
        /// </summary>
        /// <param name="configure">Configures the <see cref="IMigrationConfigurator"></see></param>
        /// <returns></returns>
        IMigrationConfigurator Configure(Action<IMigrationConfiguratorOptionsBuilder> configure);
        IMigrationOptions Build();
    }
}
