using Fluxor;
using Blazorise;
using Grpc.Net.Client;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Actions;
using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Client.Utilities.Mappers;
using ReaAccountingSys.Server.gRPC.HumanResources;

using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Effects
{
    public class CreateNewEmployeeEffect : Effect<EmployeeSubmitAction>
    {
        private GrpcChannel? _channel;
        private IMessageService? _messageService;
        private IState<GetEmployeesState>? _getEmployeeState;

        public CreateNewEmployeeEffect
        (
            GrpcChannel channel,
            IMessageService messageSvc,
            IState<GetEmployeesState> employeeState
        )
        {
            _channel = channel;
            _messageService = messageSvc;
            _getEmployeeState = employeeState;
        }

        public override async Task HandleAsync(EmployeeSubmitAction action, IDispatcher dispatcher)
        {
            try
            {
                EmployeeWriteModelRequest request = action.CreateModel!.MapToEmployeeWriteModelRequest();
                var client = new EmployeeService.EmployeeServiceClient(_channel);
                var grpcResponse = await client.CreateAsync(request);

                if (!string.IsNullOrEmpty(_getEmployeeState!.Value.SearchTerm))
                {
                    dispatcher.Dispatch(new SearchEmployeesByNameAction
                    (
                        _getEmployeeState!.Value.SearchTerm,
                        _getEmployeeState!.Value.EmployeeListFilter,
                        _getEmployeeState!.Value.PageNumber,
                        _getEmployeeState!.Value.PageSize
                    ));
                }
                else
                {
                    dispatcher.Dispatch(new GetEmployeesAction
                    (
                        _getEmployeeState!.Value.EmployeeListFilter,
                        _getEmployeeState!.Value.PageNumber,
                        _getEmployeeState!.Value.PageSize
                    ));
                }

                dispatcher.Dispatch(new EmployeeSubmitSuccessAction());
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex}", "System Error");
                dispatcher.Dispatch(new EmployeeSubmitFailureAction(ex.Message));
            }
        }
    }
}