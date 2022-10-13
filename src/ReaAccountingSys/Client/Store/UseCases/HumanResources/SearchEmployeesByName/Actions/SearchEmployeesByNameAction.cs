

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions
{
    public readonly record struct SearchEmployeesByNameAction(string SearchTerm, string Filter, int PageNumber, int PageSize);
}