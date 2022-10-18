using GoogleDateTime = Google.Protobuf.WellKnownTypes.Timestamp;
using ReadModelEmployeeListItem = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeListItem;
using ReadModelEmployeeReadModel = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeReadModel;
using GrpcEmployeeListItem = ReaAccountingSys.Server.gRPC.HumanResources.EmployeeListItem;
using GrpcEmployeeReadModel = ReaAccountingSys.Server.gRPC.HumanResources.EmployeeReadModelResponse;

using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Server.gRPC.HumanResources;

namespace ReaAccountingSys.Client.Utilities.Mappers
{
    internal static class EmployeeAggregateMappers
    {
        internal static ReadModelEmployeeListItem MapToEmployeeListItem(this GrpcEmployeeListItem model)
        {
            if (model is not null)
            {
                return new()
                {
                    EmployeeId = new Guid(model.EmployeeId),
                    EmployeeFullName = model.EmployeeFullName,
                    Telephone = model.Telephone,
                    IsActive = model.IsActive,
                    IsSupervisor = model.IsSupervisor,
                    ManagerFullName = model.ManagerFullName,
                    TimeCards = model.TimeCards
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert a null model to a response.");
            }
        }

        internal static EmployeeReadModel MapToEmployeeReadModel(this EmployeeReadModelResponse model)
        {
            if (model is not null)
            {
                return new()
                {
                    EmployeeId = new Guid(model.EmployeeId),
                    SupervisorId = new Guid(model.SupervisorId),
                    EmployeeTypeId = model.EmployeeTypeId,
                    EmployeeTypeName = model.EmployeeTypeName,
                    ManagerFullName = model.ManagerFullName,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    MiddleInitial = model.MiddleInitial,
                    EmployeeFullName = model.EmployeeFullName,
                    SSN = model.SSN,
                    EmailAddress = model.EmailAddress,
                    Telephone = model.Telephone,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2!,
                    City = model.City,
                    StateCode = model.StateCode,
                    Zipcode = model.Zipcode,
                    MaritalStatus = model.MaritalStatus,
                    Exemptions = model.Exemptions,
                    PayRate = (decimal)model.PayRate,
                    StartDate = model.StartDate.ToDateTime(),
                    IsActive = model.IsActive,
                    IsSupervisor = model.IsSupervisor,
                    CreatedDate = model.CreatedDate.ToDateTime(),
                    LastModifiedDate = model.LastModifiedDate.ToDateTime()
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert a null model to a response.");
            }
        }

        internal static string ConvertEmptyDate(DateTime date)
            => date == default ? string.Empty : date.ToShortDateString();
    }
}