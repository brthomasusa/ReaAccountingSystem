using MediatR;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Infrastructure.Application.Commands.HumanResources
{
    public record CreateEmployeeCmd(EmployeeWriteModel WriteModel) : IRequest<OperationResult<bool>>;
}