using MediatR;

using ReaAccountingSys.Infrastructure.Application.Queries.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;


namespace ReaAccountingSys.Infrastructure.Application.Handlers.HumanResources
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQry, OperationResult<PagedList<EmployeeListItem>>>
    {
        private readonly IEmployeeAggregateReadRepository _readRepository;

        public GetEmployeesHandler(IEmployeeAggregateReadRepository repo)
            => _readRepository = repo;

        public async Task<OperationResult<PagedList<EmployeeListItem>>> Handle
        (
            GetEmployeesQry request,
            CancellationToken cancellationToken
        )
            => await _readRepository.GetAllListItems(request.QueryParameters);
    }
}