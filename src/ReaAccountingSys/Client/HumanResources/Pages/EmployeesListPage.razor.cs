using Microsoft.AspNetCore.Components;
using Blazorise;
using Blazorise.Snackbar;
using Fluxor;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

using ReaAccountingSys.Client.Services.Fluxor.HumanResources;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Client.Utilities.Mappers;
using ReaAccountingSys.Server.gRPC.HumanResources;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReadModelEmployeeListItem = ReaAccountingSys.Shared.ReadModels.HumanResources.EmployeeListItem;
using GrpcEmployeeListItem = ReaAccountingSys.Server.gRPC.HumanResources.EmployeeListItem;


namespace ReaAccountingSys.Client.HumanResources.Pages
{
    public partial class EmployeesListPage
    {
        private bool _showDeleteDialog;
        private Guid _selectedEmployeeId;
        private string _placeHolderTextForSearch = "Search by employee's last name";
        private string? _snackBarMessage;
        private Snackbar? _snackbar;
        private List<ReadModelEmployeeListItem>? _employeeList;
        private EmployeeReadModelResponse? _selectedEmployee;
        private MetaData? _metaData;
        private Func<int, int, Task> _pagerChangedEventHandler => GetEmployees;
        [Inject] private StateFacade? _facade { get; set; }
        [Inject] private IState<EmployeesState>? _employeeState { get; set; }

        [Inject] public GrpcChannel? Channel { get; set; }
        [Inject] public IMessageService? MessageService { get; set; }
        [Inject] public NavigationManager? NavManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            if (_employeeState!.Value.EmployeeList is null)
            {
                _facade!.GetEmployees
                    (
                        _employeeState!.Value.EmployeeListFilter,
                        _employeeState.Value.PageNumber,
                        _employeeState.Value.PageSize
                    );
            }

            await base.OnInitializedAsync();
        }

        private async Task GetEmployees(int pageNumber, int pageSize)
        {
            _showDeleteDialog = false;

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
            _showDeleteDialog = false;

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

        private async Task GetEmployee(Guid emploeeId)
        {
            GetEmployeeRequest request = new() { EmployeeId = emploeeId.ToString() };
            var client = new EmployeeService.EmployeeServiceClient(Channel);
            var grpcResponse = await client.GetByIdAsync(request);

            _showDeleteDialog = true;
            _selectedEmployee = grpcResponse;
            await InvokeAsync(StateHasChanged);
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

        private void ShowDetailDialog(Guid employeeId) => _selectedEmployeeId = employeeId;

        private async Task OnDeleteDialogClosed(string action)
        {
            if (action == "deleted")
            {
                _snackBarMessage = $"Employee information for {_selectedEmployee!.EmployeeFullName} was successfully deleted.";
                await GetEmployees(1, 5);
                await _snackbar!.Show();
            }

            _showDeleteDialog = false;
        }
    }
}