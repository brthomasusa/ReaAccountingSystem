using ReaAccountingSys.Client.Store.UseCases.Shared.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions
{
    public class GetAllEmployeesFailureAction : FailureAction
    {
        public GetAllEmployeesFailureAction(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}