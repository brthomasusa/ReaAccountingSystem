using Grpc.Core;
using ReaAccountingSys.Shared.EmployeeService.gRPC.v1;
using EmployeeServiceBase = ReaAccountingSys.Shared.EmployeeService.gRPC.v1.EmployeeService.EmployeeServiceBase;

namespace ReaAccountingSys.Server.Services.HumanResources
{
    public class EmployeeGrpcService : EmployeeServiceBase
    {
        public virtual Task GetAll
        (
            GetEmployeesRequest request,
            IServerStreamWriter<EmployeeListItemResponse> responseStream,
            ServerCallContext context
        )
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }
    }
}