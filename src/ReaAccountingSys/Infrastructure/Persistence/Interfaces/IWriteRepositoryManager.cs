using ReaAccountingSys.Infrastructure.Persistence.Interfaces.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Interfaces
{
    public interface IWriteRepositoryManager
    {
        IEmployeeAggregateWriteRepository EmployeeAggregate { get; }
    }
}