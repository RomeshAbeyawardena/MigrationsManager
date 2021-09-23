using System;

namespace MigrationsManager.Shared.Attributes
{
    /// <summary>
    /// Specifies whether migration scanning should included or excluded on a specific class
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class MigrationAttribute : Attribute
    {
        
        public MigrationAttribute(bool enableMigrations = true, int orderId = 0)
        {
            Enabled = enableMigrations;
            OrderId = orderId;
        }

        /// <summary>
        /// Gets whether migrations should be enabled
        /// </summary>
        public bool Enabled { get; }
        public int OrderId { get; }
    }
}
