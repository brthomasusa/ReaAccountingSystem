using ReaAccountingSys.Server.gRPC.HumanResources;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Server.GrpcServices.HumanResources
{
    public static class ParameterMappings
    {
        public static GetEmployeeParameter ToParam(this GetEmployeeRequest request)
        {
            if (request is not null)
            {
                return new()
                {
                    EmployeeID = new Guid(request.EmployeeId)
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert null request to a query parameter.");
            }
        }

        public static GetEmployeesParameters ToParam(this GetEmployeesRequest request)
        {
            if (request is not null)
            {
                return new()
                {
                    Page = !string.IsNullOrEmpty(request.PageToken) ? int.Parse(request.PageToken) : 0,
                    PageSize = request.PageSize
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert null request to a query parameter.");
            }
        }

        public static GetEmployeesByStatusParameters ToParam(this GetEmployeesStatusRequest request)
        {
            if (request is not null)
            {
                return new()
                {
                    EmployeementStatus = request.EmployeementStatus,
                    Page = !string.IsNullOrEmpty(request.PageToken) ? int.Parse(request.PageToken) : 0,
                    PageSize = request.PageSize
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert null request to a query parameter.");
            }
        }

        public static GetEmployeesByLastNameParameters ToParam(this GetEmployeesByLastNameRequest request)
        {
            if (request is not null)
            {
                return new()
                {
                    LastName = request.LastName!,
                    Page = !string.IsNullOrEmpty(request.PageToken) ? int.Parse(request.PageToken) : 0,
                    PageSize = request.PageSize
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert null request to a query parameter.");
            }
        }

        public static GetEmployeesByNameAndStatusParameters ToParam(this GetEmployeesByNameAndStatusRequest request)
        {
            if (request is not null)
            {
                return new()
                {
                    LastName = request.LastName!,
                    EmployeementStatus = request.EmployeementStatus,
                    Page = !string.IsNullOrEmpty(request.PageToken) ? int.Parse(request.PageToken) : 0,
                    PageSize = request.PageSize
                };
            }
            else
            {
                throw new ArgumentNullException("Can not convert null request to a query parameter.");
            }
        }
    }
}