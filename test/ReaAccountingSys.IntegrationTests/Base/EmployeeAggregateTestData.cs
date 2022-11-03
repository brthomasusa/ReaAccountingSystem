using System;
using System.Collections.Generic;
using ReaAccountingSys.Shared;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.SharedKernel.CommonValueObjects;

namespace ReaAccountingSys.IntegrationTests.Base
{
    public class EmployeeAggregateTestData
    {
        public static EmployeeWriteModel GetEmployeeWriteModelCreate() =>
            new EmployeeWriteModel()
            {
                EmployeeId = Guid.NewGuid(),
                EmployeeType = 1,
                SupervisorId = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                LastName = "Trumpster",
                FirstName = "Ivanika",
                MiddleInitial = "I",
                SSN = "437679876",
                EmailAddress = "ivanika.trumpster@pipefitterssupply.com",
                Telephone = "732-555-5555",
                AddressLine1 = "139th Street NW",
                AddressLine2 = "B1",
                City = "Edison",
                StateCode = "NJ",
                Zipcode = "08837",
                MaritalStatus = "M",
                Exemptions = 3,
                PayRate = 25.00M,
                StartDate = new DateTime(2022, 2, 13),
                IsActive = true,
                IsSupervisor = true
            };

        public static EmployeeWriteModel GetEmployeeWriteModelEdit() =>
            new EmployeeWriteModel()
            {
                EmployeeId = new Guid("aedc617c-d035-4213-b55a-dae5cdfca366"),
                EmployeeType = 5,
                SupervisorId = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                LastName = "Goldberg",
                FirstName = "Jozef",
                MiddleInitial = "P",
                SSN = "036889999",
                EmailAddress = "jozef.goldberg@pipefitterssupply.com",
                Telephone = "469-321-1234",
                AddressLine1 = "6667 Melody Lane",
                AddressLine2 = "Apt 2",
                City = "Dallas",
                StateCode = "TX",
                Zipcode = "75231",
                MaritalStatus = "S",
                Exemptions = 1,
                PayRate = 29.00M,
                StartDate = new DateTime(2013, 2, 28),
                IsActive = true,
                IsSupervisor = true
            };

        public static EmployeeWriteModel GetEmployeeWriteModel_WayneCarter() =>
            new EmployeeWriteModel()
            {
                EmployeeId = new Guid("5c60f693-bef5-e011-a485-80ee7300c695"),
                EmployeeType = 3,
                SupervisorId = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                LastName = "Carter",
                FirstName = "Wayne",
                MiddleInitial = "L",
                SSN = "423789999",
                Telephone = "972-523-1234",
                AddressLine1 = "321 Fort Worth Ave",
                City = "Dallas",
                StateCode = "TX",
                Zipcode = "75211",
                MaritalStatus = "M",
                Exemptions = 3,
                PayRate = 40.00M,
                StartDate = new DateTime(2013, 2, 28),
                IsActive = true,
                IsSupervisor = true
            };
    }
}