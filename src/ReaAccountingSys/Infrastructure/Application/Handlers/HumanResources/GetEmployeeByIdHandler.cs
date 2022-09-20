using MediatR;

using ReaAccountingSys.Infrastructure.Application.Queries.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Handlers.HumanResources
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQry, OperationResult<EmployeeReadModel>>
    {
        private readonly IEmployeeAggregateReadRepository _readRepository;

        public GetEmployeeByIdHandler(IEmployeeAggregateReadRepository repo)
            => _readRepository = repo;

        public async Task<OperationResult<EmployeeReadModel>> Handle
        (
            GetEmployeeByIdQry request,
            CancellationToken cancellationToken
        )
            => await _readRepository.GetReadModelById(request.QueryParameters);
    }
}