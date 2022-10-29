using Fluxor;
using Blazorise;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Actions;
using ReaAccountingSys.Server.gRPC.HumanResources;
using Manager = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeManager;
using EmpType = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeTypes;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Effects
{
    public class LoadEmployeeLookupsEffect : Effect<LoadEmployeeLookupsAction>
    {
        private GrpcChannel? _channel;
        private IMessageService? _messageService;

        public LoadEmployeeLookupsEffect(GrpcChannel channel, IMessageService messageSvc)
            => (_channel, _messageService) = (channel, messageSvc);

        public override async Task HandleAsync(LoadEmployeeLookupsAction action, IDispatcher dispatcher)
        {
            try
            {
                List<Manager> managers = await GetEmployeesManagersAsync();

                List<EmpType> employeeTypes = await GetEmployeesTypesAsync();

                dispatcher.Dispatch(new LoadEmployeeLookupsSuccessAction(managers, employeeTypes));
            }
            catch (Exception ex)
            {
                await _messageService!.Error($"{ex.Message}", "System Error");
                dispatcher.Dispatch(new LoadEmployeeLookupsFailureAction(ex.Message));
            }
        }

        private async Task<List<Manager>> GetEmployeesManagersAsync()
        {
            var client = new EmployeeService.EmployeeServiceClient(_channel);
            EmployeeManagerResponse grpcResponse = await client.GetManagersAsync(new Empty());

            List<Manager> managers = new();
            grpcResponse.EmployeeManagers.ToList().ForEach(item =>
            {
                managers.Add(new Manager()
                {
                    ManagerId = new Guid(item.ManagerId),
                    ManagerFullName = item.ManagerFullName,
                    Group = item.Group
                });
            });

            return managers;
        }

        private async Task<List<EmpType>> GetEmployeesTypesAsync()
        {
            var client = new EmployeeService.EmployeeServiceClient(_channel);
            EmployeeTypeResponse grpcResponse = await client.GetTypesAsync(new Empty());

            List<EmpType> employeeTypes = new();
            grpcResponse.EmployeeTypes.ToList().ForEach(item =>
            {
                employeeTypes.Add(new EmpType()
                {
                    EmployeeTypeId = item.EmployeeTypeId,
                    EmployeeTypeName = item.EmployeeTypeName
                });
            });

            return employeeTypes;
        }
    }
}