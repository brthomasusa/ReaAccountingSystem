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

        private async Task OnSelectedJobTitleChanged(int value)
        {
            IEnumerable<Guid>? managers = null;

            if (_employeeModel!.IsSupervisor)
            {
                managers = from mgr in _getEmployeeState!.Value.EmployeeManagers
                           where mgr.ManagerFullName == "Ken J Sanchez"
                           select mgr.ManagerId;
            }
            else
            {
                managers = from mgr in _getEmployeeState!.Value.EmployeeManagers
                           where mgr.EmployeeTypeId == value
                           select mgr.ManagerId;
            }

            _employeeModel!.SupervisorId = managers.FirstOrDefault();
            _employeeModel!.EmployeeType = value;
            await Task.CompletedTask;
        }

        private async Task OnIsSupervisorChanged(bool isSupervisor)
        {
            _employeeModel!.IsSupervisor = isSupervisor;

            IEnumerable<Guid>? managers = null;

            if (isSupervisor)
            {
                managers = from mgr in _getEmployeeState!.Value.EmployeeManagers
                           where mgr.ManagerFullName == "Ken J Sanchez"
                           select mgr.ManagerId;

                _employeeModel!.SupervisorId = managers!.FirstOrDefault();
            }
            else if (_employeeModel!.EmployeeType > 0)
            {
                managers = from mgr in _getEmployeeState!.Value.EmployeeManagers
                           where mgr.EmployeeTypeId == _employeeModel!.EmployeeType
                           select mgr.ManagerId;

                _employeeModel!.SupervisorId = managers!.FirstOrDefault();
            }

            await Task.CompletedTask;
        }

        private void ValidateJobTitle(ValidatorEventArgs e)
        {
            var EmployeeTypeId = -1;

            bool isInt = int.TryParse(e.Value.ToString(), out EmployeeTypeId);
            int lowerBoune = _getEmployeeState!.Value.EmployeeTypes!.Min(t => t.EmployeeTypeId);
            int upperBoune = _getEmployeeState!.Value.EmployeeTypes!.Max(t => t.EmployeeTypeId);

            bool isValid = (isInt && (EmployeeTypeId >= lowerBoune && EmployeeTypeId <= upperBoune));

            e.Status = isValid ? ValidationStatus.Success : ValidationStatus.Error;
        }

        private void ValidateIsSupervisor(ValidatorEventArgs e)
        {
            e.Status = ValidationStatus.Success;
        }
    }
}