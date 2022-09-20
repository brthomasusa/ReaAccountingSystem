using MediatR;
using ReaAccountingSys.Infrastructure.Application.Queries.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Handlers.HumanResources
{
    public class GetEmployeeByNameAndStatusHandler : IRequestHandler<GetEmployeesByNameAndStatusQry, OperationResult<PagedList<EmployeeListItem>>>
    {
        private readonly IEmployeeAggregateReadRepository _readRepository;

        public GetEmployeeByNameAndStatusHandler(IEmployeeAggregateReadRepository repo)
            => _readRepository = repo;

        public async Task<OperationResult<PagedList<EmployeeListItem>>> Handle
        (
            GetEmployeesByNameAndStatusQry request,
            CancellationToken cancellationToken
        )
            => await _readRepository.GetAllListItemsByNameAndStatus(request.QueryParameters);
    }
}