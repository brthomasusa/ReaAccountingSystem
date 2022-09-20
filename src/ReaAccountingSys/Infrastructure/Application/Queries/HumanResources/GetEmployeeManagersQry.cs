using MediatR;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Queries.HumanResources
{
    public record GetEmployeeManagersQry(GetEmployeeManagersParameters QueryParameters) : IRequest<OperationResult<List<EmployeeManager>>>;
}