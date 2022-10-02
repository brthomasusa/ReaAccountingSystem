using Grpc.Core;
using ReaAccountingSys.Shared.EmployeeService.gRPC.v1;
using EmployeeServiceBase = ReaAccountingSys.Shared.EmployeeService.gRPC.v1.EmployeeService.EmployeeServiceBase;

namespace ReaAccountingSys.Server.Services.HumanResources
{
    public class EmployeeGrpcService : EmployeeServiceBase
    {
        private readonly ILogger<EmployeeGrpcService> _logger;

        public EmployeeGrpcService(ILogger<EmployeeGrpcService> logger)
            : base()
            => _logger = logger;

        public virtual Task GetAll
        (
            GetEmployeesRequest request,
            IServerStreamWriter<EmployeeListItemResponse> responseStream,
            ServerCallContext context
        )
        {
            _logger.LogError("Not implementated");
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }
    }
}