using GoogleDateTime = Google.Protobuf.WellKnownTypes.Timestamp;
using EmptyType = Google.Protobuf.WellKnownTypes.Empty;
using ReaAccountingSys.Server.gRPC.HumanResources;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Server.GrpcServices.HumanResources
{
    public static class ModelMappings
    {
        public static EmployeeReadModelResponse ToResponse(this EmployeeReadModel model)
        {
            if (model is not null)
            {
                return new()
                {
                    EmployeeId = model.EmployeeId.ToString(),
                    SupervisorId = model.SupervisorId.ToString(),
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
                    AddressLine2 = (model.AddressLine2 != null ? model.AddressLine2 : ""),
                    City = model.City,
                    StateCode = model.StateCode,
                    Zipcode = model.Zipcode,
                    MaritalStatus = model.MaritalStatus,
                    Exemptions = model.Exemptions,
                    PayRate = Decimal.ToDouble(model.PayRate),
                    StartDate = GoogleDateTime.FromDateTime(model.StartDate.ToUniversalTime()),
                    IsActive = model.IsActive,
                    IsSupervisor = model.IsSupervisor,
                    CreatedDate = GoogleDateTime.FromDateTime(model.CreatedDate.ToUniversalTime()),
                    LastModifiedDate = GoogleDateTime.FromDateTime(model.LastModifiedDate.ToUniversalTime())
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert a null model to a response.");
            }
        }

        public static ReaAccountingSys.Server.gRPC.HumanResources.EmployeeListItem ToResponse
        (
            this ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeListItem model
        )
        {
            if (model is not null)
            {
                return new()
                {
                    EmployeeId = model.EmployeeId.ToString(),
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

        public static EmployeeWriteModel ToWriteModel(this EmployeeWriteModelRequest request)
        {
            if (request is not null)
            {
                return new()
                {
                    EmployeeId = new Guid(request.EmployeeId),
                    EmployeeType = request.EmployeeType,
                    SupervisorId = new Guid(request.SupervisorId),
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    MiddleInitial = string.IsNullOrEmpty(request.MiddleInitial) ? null : request.MiddleInitial,
                    SSN = request.SSN,
                    EmailAddress = request.EmailAddress,
                    Telephone = request.Telephone,
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = string.IsNullOrEmpty(request.AddressLine2) ? null : request.AddressLine2,
                    City = request.City,
                    StateCode = request.StateCode,
                    Zipcode = request.Zipcode,
                    MaritalStatus = request.MaritalStatus,
                    Exemptions = request.Exemptions,
                    PayRate = (decimal)request.PayRate,
                    StartDate = request.StartDate.ToDateTime(),
                    IsActive = request.IsActive,
                    IsSupervisor = request.IsSupervisor
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert a null request to a write model.");
            }
        }

        public static ReaAccountingSys.Server.gRPC.HumanResources.EmployeeManager ToResponse
        (
            this ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeManager manager
        )
        {
            if (manager is not null)
            {
                return new()
                {
                    ManagerId = manager.ManagerId.ToString(),
                    ManagerFullName = manager.ManagerFullName,
                    Group = manager.Group
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert a null model to a response.");
            }
        }

        public static ReaAccountingSys.Server.gRPC.HumanResources.EmployeeType ToResponse
        (
            this ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeTypes model
        )
        {
            if (model is not null)
            {
                return new()
                {
                    EmployeeTypeId = model.EmployeeTypeId,
                    EmployeeTypeName = model.EmployeeTypeName
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert a null model to a response.");
            }
        }
    }
}