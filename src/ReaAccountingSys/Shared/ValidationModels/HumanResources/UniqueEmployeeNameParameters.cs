namespace ReaAccountingSys.Shared.ValidationModels.HumanResources
{
    public readonly record struct UniqueEmployeeNameParameters(string FirstName, string LastName, string? MiddleInitial);
}