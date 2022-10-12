namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions
{
    public readonly record struct GetEmployeesAction(string Filter, int PageNumber, int PageSize);
}