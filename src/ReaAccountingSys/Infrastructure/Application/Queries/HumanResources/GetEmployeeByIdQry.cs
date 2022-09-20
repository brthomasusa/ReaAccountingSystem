using MediatR;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Queries.HumanResources
{
    public record GetEmployeeByIdQry(GetEmployeeParameter QueryParameters) : IRequest<OperationResult<EmployeeReadModel>>;
}