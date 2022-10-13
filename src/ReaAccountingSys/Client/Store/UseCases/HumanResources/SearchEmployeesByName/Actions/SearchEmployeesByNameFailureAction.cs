using ReaAccountingSys.Client.Store.UseCases.Shared.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions
{
    public class SearchEmployeesByNameFailureAction : FailureAction
    {
        public SearchEmployeesByNameFailureAction(string errorMessage)
            : base(errorMessage)
        {

        }
    }
}