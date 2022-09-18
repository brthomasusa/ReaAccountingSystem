using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;

namespace ReaAccountingSys.Core.Interfaces.HumanResources
{
    public interface IEmployeeBaseReadRepository
    {
        Task<OperationResult<bool>> Exists(Guid employeeId);
        Task<OperationResult<List<Employee>>> GetAllAsync();
        Task<OperationResult<Employee>> GetByIdAsync(Guid employeeId);
    }
}