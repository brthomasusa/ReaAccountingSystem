// using ReaAccountingSys.Shared.Readmodels.CashManagement;
// using ReaAccountingSys.Shared.Readmodels.Financing;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
// using ReaAccountingSys.Shared.WriteModels.CashManagement;
// using ReaAccountingSys.Shared.WriteModels.Financing;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Shared.WriteModels
{
    public static class ModelExtensions
    {
        public static EmployeeWriteModel Map(this EmployeeReadModel input)
        {
            return new()
            {
                EmployeeId = input.EmployeeId,
                SupervisorId = input.SupervisorId,
                EmployeeType = input.EmployeeTypeId,
                FirstName = input.FirstName!,
                LastName = input.LastName!,
                MiddleInitial = input.MiddleInitial!,
                Telephone = input.Telephone!,
                EmailAddress = input.EmailAddress!,
                SSN = input.SSN!,
                AddressLine1 = input.AddressLine1!,
                AddressLine2 = input.AddressLine2!,
                City = input.City!,
                StateCode = input.StateCode!,
                Zipcode = input.Zipcode!,
                MaritalStatus = input.MaritalStatus!,
                Exemptions = input.Exemptions,
                PayRate = input.PayRate,
                StartDate = input.StartDate,
                IsActive = input.IsActive,
                IsSupervisor = input.IsSupervisor
            };
        }
    }
}