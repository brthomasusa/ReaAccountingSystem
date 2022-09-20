using MediatR;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Queries.HumanResources
{
    public record GetEmployeesByStatusQry(GetEmployeesByStatusParameters QueryParameters) : IRequest<OperationResult<PagedList<EmployeeListItem>>>;
}