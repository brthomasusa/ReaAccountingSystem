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

        protected async override Task OnInitializedAsync()
        {
            if (_createEmployeeState!.Value.Model is null)
            {
                EmployeeWriteModel model = new()
                {
                    EmployeeId = Guid.NewGuid(),
                    IsActive = true
                };

                _facade!.InitializeEmployeeCreateModel(model);
                _employeeModel = _createEmployeeState.Value.Model;
            }

            await base.OnInitializedAsync();
        }

        private async Task OnSave()
        {
            if (!await _validations!.ValidateAll())
                return;

            _isLoading = true;

            _isLoading = false;
        }
    }
}