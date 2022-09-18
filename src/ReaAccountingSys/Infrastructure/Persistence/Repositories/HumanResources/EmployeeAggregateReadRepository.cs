using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Repositories.HumanResources
{
    public class EmployeeAggregateReadRepository : IEmployeeAggregateReadRepository
    {
        /* 
            Queries for domain objects defined in ReaAccountingSys.Core.HumanResources.EmployeeAggregate
        */

        public Task<OperationResult<bool>> Exists(Guid employeeId)
            => throw new NotImplementedException();

        public Task<OperationResult<List<Employee>>> GetAllAsync()
            => throw new NotImplementedException();

        public Task<OperationResult<Employee>> GetByIdAsync(Guid employeeId)
            => throw new NotImplementedException();

        /* 
            Queries for read models defined in ReaAccountingSys.Shared.ReadModels.HumanResources
        */

        public Task<OperationResult<EmployeeReadModel>> GetReadModelById(GetEmployeeParameter queryParameter)
            => throw new NotImplementedException();

        public Task<PagedList<OperationResult<EmployeeListItem>>> GetAllListItems(GetEmployeesParameters queryParameter)
            => throw new NotImplementedException();

        public Task<PagedList<OperationResult<EmployeeListItem>>> GetAllListItemsByStatus(GetEmployeesByStatusParameters queryParameter)
            => throw new NotImplementedException();

        public Task<PagedList<OperationResult<EmployeeListItem>>> GetAllListItemsByName(GetEmployeesByLastNameParameters queryParameter)
            => throw new NotImplementedException();

        public Task<PagedList<OperationResult<EmployeeListItem>>> GetAllListItemsByNameAndStatus(GetEmployeesByLastNameAndStatusParameters queryParameter)
            => throw new NotImplementedException();
    }
}