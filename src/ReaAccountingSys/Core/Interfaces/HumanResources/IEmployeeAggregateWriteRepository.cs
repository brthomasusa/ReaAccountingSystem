using ReaAccountingSys.SharedKernel.Interfaces;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;

namespace ReaAccountingSys.Core.Interfaces.HumanResources
{
    public interface IEmployeeAggregateWriteRepository : IAggregateRootRepository<Employee>
    {
        Task<OperationResult<bool>> DeleteTimeCardAsync(Guid timeCardId);
    }
}