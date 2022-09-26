using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Commands.HumanResources
{
    public readonly record struct CreateEmployeeCommand(EmployeeWriteModel WriteModel) : ICommand;
}