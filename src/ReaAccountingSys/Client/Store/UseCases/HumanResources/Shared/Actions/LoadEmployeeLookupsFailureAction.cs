using ReaAccountingSys.Client.Store.UseCases.Shared.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Actions
{
    public class LoadEmployeeLookupsFailureAction : FailureAction
    {
        public LoadEmployeeLookupsFailureAction(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}