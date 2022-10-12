using Fluxor;
using Blazorise;
using Grpc.Net.Client;

using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Client.Utilities.Mappers;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions;
using ReaAccountingSys.Server.gRPC.HumanResources;
using ReaAccountingSys.Shared.ReadModels;

using ReadModelEmployeeListItem = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeListItem;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Effects
{
    public class GetAllEmployeesEffect : Effect<GetEmployeesAction>
    {
        private GrpcChannel? _channel;
        private IMessageService? _messageService;

        public GetAllEmployeesEffect(GrpcChannel channel, IMessageService messageSvc)
            => (_channel, _messageService) = (channel, messageSvc);

        public override async Task HandleAsync(GetEmployeesAction action, IDispatcher dispatcher)
        {
            switch (action.Filter)
            {
                case "all":
                    await GetEmployeesUnfiltered(action, dispatcher);
                    break;
                case "active":
                case "inactive":
                    await GetEmployeesfiltered(action, dispatcher);
                    break;
                default:
                    await _messageService!.Error($"Unknown employee filter: {action.Filter}", "System Error");
                    break;
            }
        }

        private async Task GetEmployeesUnfiltered
        (
            GetEmployeesAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                GetEmployeesRequest request =
                    new() { PageSize = action.PageSize, PageToken = action.PageNumber.ToString() };

                var client = new EmployeeService.EmployeeServiceClient(_channel);
                var grpcResponse = await client.GetAllAsync(request);

                MetaData metaData = new()
                {
                    TotalCount = grpcResponse.MetaData["TotalCount"],
                    PageSize = grpcResponse.MetaData["PageSize"],
                    CurrentPage = grpcResponse.MetaData["CurrentPage"],
                    TotalPages = grpcResponse.MetaData["TotalPages"]
                };

                List<ReadModelEmployeeListItem> employees = new();

                grpcResponse.EmployeeListItems.ToList().ForEach(item =>
                {
                    employees.Add(EmployeeAggregateMappers.MapToEmployeeListItem(item));
                });
                Console.WriteLine(metaData.ToJson());
                PagingResponse<ReadModelEmployeeListItem> pagedResponse =
                    new() { Items = employees, MetaData = metaData };

                dispatcher.Dispatch(new GetAllEmployeesSuccessAction(pagedResponse, action.Filter, action.PageSize));
            }
            catch (Exception e)
            {
                await _messageService!.Error($"{e.Message}", "System Error");
                dispatcher.Dispatch(new GetAllEmployeesFailureAction(e.Message));
            }
        }

        private async Task GetEmployeesfiltered
        (
            GetEmployeesAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                GetEmployeesStatusRequest request = new()
                {
                    EmployeementStatus = (action.Filter == "active" ? true : false),
                    PageSize = action.PageSize,
                    PageToken = action.PageNumber.ToString()
                };

                var client = new EmployeeService.EmployeeServiceClient(_channel);
                var grpcResponse = await client.GetAllByStatusAsync(request);

                MetaData metaData = new()
                {
                    TotalCount = grpcResponse.MetaData["TotalCount"],
                    PageSize = grpcResponse.MetaData["PageSize"],
                    CurrentPage = grpcResponse.MetaData["CurrentPage"],
                    TotalPages = grpcResponse.MetaData["TotalPages"]
                };

                List<ReadModelEmployeeListItem> employees = new();

                grpcResponse.EmployeeListItems.ToList().ForEach(item =>
                {
                    employees.Add(EmployeeAggregateMappers.MapToEmployeeListItem(item));
                });

                Console.WriteLine($"GetEmployeesfiltered: {employees.ToJson()}");

                PagingResponse<ReadModelEmployeeListItem> pagedResponse =
                    new() { Items = employees, MetaData = metaData };

                dispatcher.Dispatch(new GetAllEmployeesSuccessAction(pagedResponse, action.Filter, action.PageSize));
            }
            catch (Exception e)
            {
                await _messageService!.Error($"{e.Message}", "System Error");
                dispatcher.Dispatch(new GetAllEmployeesFailureAction(e.Message));
            }
        }

    }
}