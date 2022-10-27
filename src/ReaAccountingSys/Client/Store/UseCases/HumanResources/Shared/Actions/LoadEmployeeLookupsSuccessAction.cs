using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Actions
{
    public class LoadEmployeeLookupsSuccessAction
    {
        public LoadEmployeeLookupsSuccessAction
        (
            List<EmployeeManager>? managers,
            List<EmployeeTypes>? employeeTypes
        ) =>
            (EmployeeManagers, EmployeeTypes) = (managers, employeeTypes);

        public List<EmployeeManager>? EmployeeManagers { get; init; }
        public List<EmployeeTypes>? EmployeeTypes { get; init; }
    }
}