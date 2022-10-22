using Microsoft.AspNetCore.Components;
using Blazorise;
using Blazorise.Snackbar;
using Fluxor;
using Grpc.Net.Client;

using ReaAccountingSys.Client.Services.Fluxor.HumanResources;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Server.gRPC.HumanResources;

namespace ReaAccountingSys.Client.HumanResources.Pages
{
    public partial class EmployeesCreatePage
    {
        private const string _returnUri = "HumanResouces/Pages/EmployeesListPage";
        private const string _formTitle = "Create Employee Info";
        private string? _snackBarMessage;
        [Inject] private EmployeeAggregateStateFacade? _facade { get; set; }
        [Inject] private IState<EmployeesState>? _employeeState { get; set; }

        [Inject] public IMessageService? MessageService { get; set; }

        private async Task<OperationResult<bool>> Save()
        {
            return await Task.FromResult<OperationResult<bool>>(OperationResult<bool>.CreateSuccessResult(true));
        }
    }
}