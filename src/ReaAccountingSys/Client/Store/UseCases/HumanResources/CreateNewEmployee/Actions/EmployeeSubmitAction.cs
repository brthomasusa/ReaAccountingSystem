using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Actions
{
    public readonly record struct EmployeeSubmitAction(EmployeeWriteModel CreateModel);
}