using MediatR;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Queries.HumanResources
{
    public record GetEmployeesByNameAndStatusQry(GetEmployeesByNameAndStatusParameters QueryParameters) : IRequest<OperationResult<PagedList<EmployeeListItem>>>;
}