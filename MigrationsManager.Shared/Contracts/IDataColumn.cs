using System;

namespace MigrationsManager.Shared.Contracts
{
    public interface IDataColumn
    {
        string Name { get; }
        Type Type { get;}
        int? Length { get; }
        string TypeName { get; }
        object DefaultValue { get; }
        bool IsRequired { get; }
    }
}
