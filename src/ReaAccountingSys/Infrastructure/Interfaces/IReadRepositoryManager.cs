using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;

namespace ReaAccountingSys.Infrastructure.Interfaces
{
    public interface IReadRepositoryManager
    {
        IEmployeeAggregateReadRepository EmployeeAggregate { get; }
        IEmployeeAggregateValidationRepository EmployeeValidation { get; }
    }
}