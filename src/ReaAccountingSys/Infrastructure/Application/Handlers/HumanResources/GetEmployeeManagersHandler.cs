using MediatR;
using ReaAccountingSys.Infrastructure.Application.Queries.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Handlers.HumanResources
{
    public class GetEmployeeManagersHandler : IRequestHandler<GetEmployeeManagersQry, OperationResult<List<EmployeeManager>>>
    {
        private readonly IEmployeeAggregateReadRepository _readRepository;

        public GetEmployeeManagersHandler(IEmployeeAggregateReadRepository repo)
            => _readRepository = repo;

        public async Task<OperationResult<List<EmployeeManager>>> Handle
        (
            GetEmployeeManagersQry request,
            CancellationToken cancellationToken
        )
            => await _readRepository.GetEmployeeManagers(request.QueryParameters);
    }
}