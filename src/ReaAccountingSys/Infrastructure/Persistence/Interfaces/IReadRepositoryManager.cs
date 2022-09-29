using ReaAccountingSys.Infrastructure.Persistence.Interfaces.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Interfaces
{
    public interface IReadRepositoryManager
    {
        IEmployeeAggregateReadRepository EmployeeAggregate { get; }
        IEmployeeAggregateValidationRepository EmployeeValidation { get; }
    }
}