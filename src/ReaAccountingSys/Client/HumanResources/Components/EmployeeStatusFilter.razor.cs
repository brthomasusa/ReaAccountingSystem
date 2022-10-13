using Microsoft.AspNetCore.Components;
using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;

namespace ReaAccountingSys.Client.HumanResources.Components
{
    public partial class EmployeeStatusFilter
    {
        private string? _selectedFilter;

        [Parameter] public EventCallback<string> FilterSetEventHandler { get; set; }
        [Inject] private IState<EmployeesState>? _employeeState { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _selectedFilter = _employeeState!.Value.EmployeeListFilter switch
            {
                "all" => "All ",
                "active" => "Active",
                "inactive" => "Inactive",
                _ => "Unknown filter"
            };

            await Task.CompletedTask;
        }

        private async Task OnFilterChanged(string filterName)
        {
            _selectedFilter = filterName switch
            {
                "all" => "All ",
                "active" => "Active",
                "inactive" => "Inactive",
                _ => "Unknown filter"
            };

            await FilterSetEventHandler.InvokeAsync(filterName);
        }
    }
}