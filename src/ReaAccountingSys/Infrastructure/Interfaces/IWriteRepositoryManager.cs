using ReaAccountingSys.Core.Interfaces.HumanResources;

namespace ReaAccountingSys.Infrastructure.Interfaces
{
    public interface IWriteRepositoryManager
    {
        IEmployeeAggregateWriteRepository EmployeeAggregate { get; }
    }
}