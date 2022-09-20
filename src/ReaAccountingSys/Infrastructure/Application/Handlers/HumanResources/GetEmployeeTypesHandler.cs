using MediatR;
using ReaAccountingSys.Infrastructure.Application.Queries.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Handlers.HumanResources
{
    public class GetEmployeeTypesHandler : IRequestHandler<GetEmployeeTypesQry, OperationResult<List<EmployeeTypes>>>
    {
        private readonly IEmployeeAggregateReadRepository _readRepository;

        public GetEmployeeTypesHandler(IEmployeeAggregateReadRepository repo)
            => _readRepository = repo;

        public async Task<OperationResult<List<EmployeeTypes>>> Handle
        (
            GetEmployeeTypesQry request,
            CancellationToken cancellationToken
        )
            => await _readRepository.GetEmployeeTypes(request.QueryParameters);
    }
}