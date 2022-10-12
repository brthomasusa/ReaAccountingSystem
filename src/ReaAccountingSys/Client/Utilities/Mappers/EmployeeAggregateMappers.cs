using System;
using ReadModelEmployeeListItem = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeListItem;
using GrpcEmployeeListItem = ReaAccountingSys.Server.gRPC.HumanResources.EmployeeListItem;

namespace ReaAccountingSys.Client.Utilities.Mappers
{
    internal static class EmployeeAggregateMappers
    {
        internal static ReadModelEmployeeListItem MapToEmployeeListItem(GrpcEmployeeListItem model)
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

        internal static string ConvertEmptyDate(DateTime date)
            => date == default ? string.Empty : date.ToShortDateString();
    }
}