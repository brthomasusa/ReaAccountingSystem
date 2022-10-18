using Microsoft.AspNetCore.Components;
using Blazorise.Snackbar;
using Fluxor;
using Grpc.Net.Client;

using ReaAccountingSys.Client.Services.Fluxor.HumanResources;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Server.gRPC.HumanResources;

namespace ReaAccountingSys.Client.HumanResources.Pages
{
    public partial class EmployeesListPage
    {
        private Guid _selectedEmployeeId;
        private string _placeHolderTextForSearch = "Search by employee's last name";
        private string? _snackBarMessage;
        private Snackbar? _snackbar;
        private EmployeeReadModelResponse? _selectedEmployee;
        private Func<int, int, Task> _pagerChangedEventHandler => GetEmployees;
        [Inject] private StateFacade? _facade { get; set; }
        [Inject] private IState<EmployeesState>? _employeeState { get; set; }

        [Inject] public GrpcChannel? Channel { get; set; }
        [Inject] public NavigationManager? NavManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Console.WriteLine("OnInitializedAsync");
            if (_employeeState!.Value.EmployeeList is null)
            {
                _facade!.GetEmployees
                    (
                        _employeeState!.Value.EmployeeListFilter,
                        _employeeState.Value.PageNumber,
                        _employeeState.Value.PageSize
                    );
            }

            await InvokeAsync(StateHasChanged);
            await base.OnInitializedAsync();
        }

        private async Task GetEmployees(int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(_employeeState!.Value.SearchTerm))
            {
                await GetFilteredEmployeeList(pageNumber, pageSize);
            }
            else
            {
                await SearchEmployeesByName
                    (
                        _employeeState!.Value.SearchTerm,
                        _employeeState!.Value.EmployeeListFilter,
                        pageNumber,
                        pageSize
                    );
            }
        }

        private async Task GetFilteredEmployeeList(int pageNumber, int pageSize)
        {
            _facade!.GetEmployees
                (
                    _employeeState!.Value.EmployeeListFilter,
                    pageNumber,
                    pageSize
                );

            await Task.CompletedTask;
        }

        private async Task SearchEmployeesByName(string searchTerm, string filterName, int pageNumber, int pageSize)
        {
            _facade!.SearchEmployeesByLastName
                (
                    searchTerm,
                    filterName,
                    pageNumber,
                    pageSize
                );

            await Task.CompletedTask;
        }

        private async Task OnSearchChanged(string searchTerm)
        {
            await SearchEmployeesByName
                (
                    searchTerm,
                    _employeeState!.Value.EmployeeListFilter,
                    _employeeState!.Value.PageNumber,
                    _employeeState!.Value.PageSize
                );
        }

        private async Task GetFilteredEmployeeList(string filterName)
        {
            _facade!.GetEmployees
            (
                filterName,
                1,
                _employeeState!.Value.PageSize
            );

            await Task.CompletedTask;
        }

        private async Task OnFilterChanged(string filterName)
        {
            if (string.IsNullOrEmpty(_employeeState!.Value.SearchTerm))
            {
                await GetFilteredEmployeeList(filterName);
            }
            else
            {
                await SearchEmployeesByName
                    (
                        _employeeState!.Value.SearchTerm,
                        filterName,
                        _employeeState!.Value.PageNumber,
                        _employeeState!.Value.PageSize
                    );
            }
        }

        private void OnActionItemClicked(string action, Guid employeeId)
        {
            NavManager!.NavigateTo
            (
                action switch
                {
                    "Edit" => $"HumanResouces/Pages/EmployeeEditPage/{employeeId}",
                    _ => throw new ArgumentOutOfRangeException(nameof(action), $"Unexpected menu item: {action}"),
                }
            );
        }

        private void ShowDetailDialog(Guid employeeId)
            => _selectedEmployeeId = employeeId;

        private void ShowDeleteDialog(string employeeId)
        {
            _facade!.GetEmployeeDetails(employeeId);
        }

        private async Task OnDeleteDialogClosed(string action)
        {
            if (action == "deleted")
            {
                _snackBarMessage = $"Employee information for {_selectedEmployee!.EmployeeFullName} was successfully deleted.";
                await GetEmployees(1, 5);
                await _snackbar!.Show();
            }
        }
    }
}