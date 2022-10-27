using Microsoft.AspNetCore.Components;
using Blazorise;
using Blazorise.Snackbar;
using Fluxor;

using ReaAccountingSys.Client.Services.Fluxor.HumanResources;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Client.HumanResources.Pages
{
    public partial class EmployeeCreatePage
    {
        private const string _returnUri = "HumanResouces/Pages/EmployeesListPage";
        private const string _formTitle = "Create Employee Info";
        private bool _isLoading;
        private Snackbar? _snackbar;
        private string? _snackBarMessage;
        private EmployeeWriteModel? _employeeModel;
        [Inject] private EmployeeAggregateStateFacade? _facade { get; set; }
        [Inject] private IState<GetEmployeesState>? _getEmployeeState { get; set; }
        [Inject] private IState<CreateEmployeeState>? _createEmployeeState { get; set; }

        protected async override Task OnInitializedAsync()
        {
            if (_createEmployeeState is not null)
            {
                _employeeModel = _createEmployeeState.Value.Model;
            }
            await base.OnInitializedAsync();
        }

        private async Task<OperationResult<bool>> OnSave()
        {
            return await Task.FromResult<OperationResult<bool>>(OperationResult<bool>.CreateSuccessResult(true));
        }
    }
}