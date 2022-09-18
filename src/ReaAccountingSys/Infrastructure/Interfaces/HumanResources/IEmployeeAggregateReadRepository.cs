using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Core.Interfaces.HumanResources;

namespace ReaAccountingSys.Infrastructure.Interfaces.HumanResources
{
    public interface IEmployeeAggregateReadRepository : IEmployeeBaseReadRepository
    {
        Task<OperationResult<EmployeeReadModel>> GetReadModelById(GetEmployeeParameter queryParameter);
        Task<PagedList<OperationResult<EmployeeListItem>>> GetAllListItems(GetEmployeesParameters queryParameter);
        Task<PagedList<OperationResult<EmployeeListItem>>> GetAllListItemsByStatus(GetEmployeesByStatusParameters queryParameter);
        Task<PagedList<OperationResult<EmployeeListItem>>> GetAllListItemsByName(GetEmployeesByLastNameParameters queryParameter);
        Task<PagedList<OperationResult<EmployeeListItem>>> GetAllListItemsByNameAndStatus(GetEmployeesByLastNameAndStatusParameters queryParameter);
    }
}