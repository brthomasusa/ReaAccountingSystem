using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Core.Interfaces.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.Infrastructure.Persistence.Queries.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Repositories.HumanResources
{
    public class EmployeeAggregateReadRepository : IEmployeeAggregateReadRepository
    {
        private readonly DapperContext _dapperCtx;

        public EmployeeAggregateReadRepository(DapperContext ctx) => _dapperCtx = ctx;

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

        public async Task<OperationResult<EmployeeReadModel>> GetReadModelById(GetEmployeeParameter queryParameter)
            => await GetReadModelByIdQuery.Query(queryParameter, _dapperCtx);

        public async Task<OperationResult<PagedList<EmployeeListItem>>> GetAllListItems(GetEmployeesParameters queryParameter)
            => await GetEmployeeListItemsQuery.Query(queryParameter, _dapperCtx);

        public async Task<OperationResult<PagedList<EmployeeListItem>>> GetAllListItemsByStatus(GetEmployeesByStatusParameters queryParameter)
            => await GetAllListItemsByStatusQuery.Query(queryParameter, _dapperCtx);

        public async Task<OperationResult<PagedList<EmployeeListItem>>> GetAllListItemsByName(GetEmployeesByLastNameParameters queryParameter)
            => await GetAllListItemsByNameQuery.Query(queryParameter, _dapperCtx);

        public async Task<OperationResult<PagedList<EmployeeListItem>>> GetAllListItemsByNameAndStatus(GetEmployeesByNameAndStatusParameters queryParameter)
            => await GetAllListItemsByNameAndStatusQuery.Query(queryParameter, _dapperCtx);

        public async Task<OperationResult<List<EmployeeManager>>> GetEmployeeManagers(GetEmployeeManagersParameters queryParameter)
            => await GetEmployeeManagersQuery.Query(queryParameter, _dapperCtx);
    }
}