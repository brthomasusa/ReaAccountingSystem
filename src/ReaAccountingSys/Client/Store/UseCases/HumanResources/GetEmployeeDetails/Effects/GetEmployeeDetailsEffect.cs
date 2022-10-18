using Fluxor;
using Blazorise;
using Grpc.Net.Client;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Actions;
using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Client.Utilities.Mappers;
using ReaAccountingSys.Server.gRPC.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Effects
{
    public class GetEmployeeDetailsEffect : Effect<GetEmployeeDetailsAction>
    {
        private GrpcChannel? _channel;
        private IMessageService? _messageService;

        public GetEmployeeDetailsEffect(GrpcChannel channel, IMessageService messageSvc)
            => (_channel, _messageService) = (channel, messageSvc);

        public override async Task HandleAsync(GetEmployeeDetailsAction action, IDispatcher dispatcher)
        {
            try
            {
                GetEmployeeRequest request = new() { EmployeeId = action.employeeId };
                var client = new EmployeeService.EmployeeServiceClient(_channel);
                var grpcResponse = await client.GetByIdAsync(request);

                dispatcher.Dispatch(new GetEmployeeDetailsSuccessAction(grpcResponse.MapToEmployeeReadModel()));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new GetEmployeeDetailsFailureAction(ex.Message));
            }
        }
    }
}