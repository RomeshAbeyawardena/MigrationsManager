namespace MigrationsManager.Shared.Contracts
{
    public interface IConcurrentList<T>
    {
        object SyncRoot { get; }
    }
}
