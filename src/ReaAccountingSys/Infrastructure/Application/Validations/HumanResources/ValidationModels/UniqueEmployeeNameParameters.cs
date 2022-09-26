namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels
{
    public readonly record struct UniqueEmployeeNameParameters(string FirstName, string LastName, string? MiddleInitial);
}