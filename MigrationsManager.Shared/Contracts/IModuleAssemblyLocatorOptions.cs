namespace MigrationsManager.Shared.Contracts
{
    public interface IModuleAssemblyLocatorOptions : IModuleAssemblyOptions
    {
        IModuleAssemblyOptions AddAssembly(string assemblyNameOrFilePath, IAssemblyOptions assemblyOptions);
    }
}
