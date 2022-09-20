using MediatR;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Queries.HumanResources
{
    public record GetEmployeeTypesQry(GetEmployeeTypesParameters QueryParameters) : IRequest<OperationResult<List<EmployeeTypes>>>;
}