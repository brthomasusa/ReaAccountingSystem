using ReaAccountingSys.Client.Store.UseCases.Shared.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Actions
{
    public class GetEmployeeDetailsFailureAction : FailureAction
    {
        public GetEmployeeDetailsFailureAction(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}