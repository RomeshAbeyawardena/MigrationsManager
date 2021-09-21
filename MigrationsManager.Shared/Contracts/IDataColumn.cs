namespace MigrationsManager.Shared.Contracts
{
    public interface IDataColumn
    {
        string Name { get; }
        string Type { get;}
        object DefaultValue { get; }
    }
}
