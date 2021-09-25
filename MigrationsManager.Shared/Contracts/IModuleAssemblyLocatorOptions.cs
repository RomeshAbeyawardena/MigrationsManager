namespace MigrationsManager.Shared.Contracts
{
    /// <summary>
    /// Represents options used to locate assemblies to pull <see cref="IModule">modules</see> from
    /// </summary>
    public interface IModuleAssemblyLocatorOptions : IModuleAssemblyOptions
    {
        /// <summary>
        /// Adds an assembly location
        /// </summary>
        /// <param name="assemblyNameOrFilePath"></param>
        /// <param name="assemblyOptions"></param>
        /// <returns></returns>
        IModuleAssemblyOptions AddAssembly(string assemblyNameOrFilePath, IAssemblyOptions assemblyOptions);
    }
}
