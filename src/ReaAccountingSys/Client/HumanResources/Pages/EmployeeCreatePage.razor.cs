using Microsoft.AspNetCore.Components;
using Blazorise;
using Blazorise.Snackbar;
using Fluxor;
using FluentValidation;

using ReaAccountingSys.Client.HumanResources.Validators;
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
        private Validations? _validations;
        private Snackbar? _snackbar;
        private string? _snackBarMessage;
        private EmployeeWriteModel? _employeeModel;
        [Inject] private EmployeeAggregateStateFacade? _facade { get; set; }
        [Inject] private IState<GetEmployeesState>? _getEmployeeState { get; set; }
        [Inject] private IState<CreateEmployeeState>? _createEmployeeState { get; set; }
        [Inject] private IMessageService? _messageService { get; set; }
        [Inject] private NavigationManager? _navManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            if (_createEmployeeState!.Value.Model is null)
            {
                await InitEmployeeWriteModel();
            }

            await base.OnInitializedAsync();
        }

        private async Task InitEmployeeWriteModel()
        {
            EmployeeWriteModel model = new()
            {
                EmployeeId = Guid.NewGuid(),
                IsActive = true
            };

            _facade!.InitializeEmployeeCreateModel(model);

            _employeeModel = _createEmployeeState!.Value.Model;

            await Task.CompletedTask;
        }

        private async Task OnSave()
        {
            if (!await _validations!.ValidateAll())
                return;

            _isLoading = true;
            await _facade!.CreateNewEmployee(_employeeModel!);
            _isLoading = false;

            if (_createEmployeeState!.Value.Submitted &&
                string.IsNullOrWhiteSpace(_createEmployeeState!.Value.CurrentErrorMessage!))
            {
                _snackBarMessage = $"New employee information for {_employeeModel!.FirstName} {_employeeModel!.LastName} was successfully created.";
                await _snackbar!.Show();
            }
            else
            {
                await _messageService!.Error($"Error while creating employee: {_createEmployeeState!.Value.CurrentErrorMessage!}", "Error");
            }
        }

        private async Task OnCancel()
        {
            _facade!.SetEmployeeCreateModelToNull();
            _navManager!.NavigateTo(_returnUri);

            await Task.CompletedTask;
        }
    }
}