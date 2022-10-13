using Fluxor;
using Blazorise;
using Grpc.Net.Client;

using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Client.Utilities.Mappers;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions;
using ReaAccountingSys.Server.gRPC.HumanResources;
using ReaAccountingSys.Shared.ReadModels;
using ReadModelEmployeeListItem = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeListItem;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Effects
{
    public class SearchEmployeesByNameEffect : Effect<SearchEmployeesByNameAction>
    {
        private GrpcChannel? _channel;
        private IMessageService? _messageService;

        public SearchEmployeesByNameEffect(GrpcChannel channel, IMessageService messageSvc)
            => (_channel, _messageService) = (channel, messageSvc);

        public override async Task HandleAsync(SearchEmployeesByNameAction action, IDispatcher dispatcher)
        {
            if (action.Filter == "all")
            {
                await SearchEmployeesByLastName(action, dispatcher);
            }
            else
            {
                await SearchEmployeesByLastNameAndStatus(action, dispatcher);
            }
        }

        private async Task SearchEmployeesByLastName
        (
            SearchEmployeesByNameAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                GetEmployeesByLastNameRequest request = new()
                {
                    LastName = action.SearchTerm,
                    PageSize = action.PageSize,
                    PageToken = action.PageNumber.ToString()
                };

                var client = new EmployeeService.EmployeeServiceClient(_channel);
                var grpcResponse = await client.SearchByNameAsync(request);

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

                PagingResponse<ReadModelEmployeeListItem> pagedResponse =
                    new() { Items = employees, MetaData = metaData };

                dispatcher.Dispatch(new SearchEmployeesByNameSuccessAction(pagedResponse, action.SearchTerm, action.Filter, action.PageSize));
            }
            catch (Exception e)
            {
                await _messageService!.Error($"{e.Message}", "System Error");
                dispatcher.Dispatch(new SearchEmployeesByNameFailureAction(e.Message));
            }
        }

        private async Task SearchEmployeesByLastNameAndStatus
        (
            SearchEmployeesByNameAction action,
            IDispatcher dispatcher
        )
        {
            try
            {
                GetEmployeesByNameAndStatusRequest request = new()
                {
                    LastName = action.SearchTerm,
                    EmployeementStatus = (action.Filter == "active" ? true : false),
                    PageSize = action.PageSize,
                    PageToken = action.PageNumber.ToString()
                };

                var client = new EmployeeService.EmployeeServiceClient(_channel);
                var grpcResponse = await client.SearchByNameAndStatusAsync(request);

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

                dispatcher.Dispatch(new SearchEmployeesByNameSuccessAction(pagedResponse, action.SearchTerm, action.Filter, action.PageSize));
            }
            catch (Exception e)
            {
                await _messageService!.Error($"{e.Message}", "System Error");
                dispatcher.Dispatch(new SearchEmployeesByNameFailureAction(e.Message));
            }
        }
    }
}