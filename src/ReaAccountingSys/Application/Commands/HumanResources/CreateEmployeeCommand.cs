using ReaAccountingSys.Application.Interfaces;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Application.Commands.HumanResources
{
    public readonly record struct CreateEmployeeCommand(EmployeeWriteModel WriteModel) : ICommand;
}