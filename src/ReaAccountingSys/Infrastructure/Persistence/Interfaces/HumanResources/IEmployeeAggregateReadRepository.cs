using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Infrastructure.Persistence.Interfaces.HumanResources
{
    public interface IEmployeeAggregateReadRepository
    {
        Task<OperationResult<EmployeeReadModel>> GetReadModelById(GetEmployeeParameter queryParameter);
        Task<OperationResult<PagedList<EmployeeListItem>>> GetAllListItems(GetEmployeesParameters queryParameter);
        Task<OperationResult<PagedList<EmployeeListItem>>> GetAllListItemsByStatus(GetEmployeesByStatusParameters queryParameter);
        Task<OperationResult<PagedList<EmployeeListItem>>> GetAllListItemsByName(GetEmployeesByLastNameParameters queryParameter);
        Task<OperationResult<PagedList<EmployeeListItem>>> GetAllListItemsByNameAndStatus(GetEmployeesByNameAndStatusParameters queryParameter);
        Task<OperationResult<List<EmployeeManager>>> GetEmployeeManagers(GetEmployeeManagersParameters queryParameters);
        Task<OperationResult<List<EmployeeTypes>>> GetEmployeeTypes(GetEmployeeTypesParameters queryParameters);
    }
}
