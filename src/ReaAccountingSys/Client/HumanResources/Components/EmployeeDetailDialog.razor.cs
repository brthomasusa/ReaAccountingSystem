using Microsoft.AspNetCore.Components;
using Blazorise;
using Fluxor;

using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Services.Fluxor.HumanResources;

namespace ReaAccountingSys.Client.HumanResources.Components
{
    public partial class EmployeeDetailDialog
    {
        private Modal? _detailModalRef;
        private string selectedTab = "generalInfo";
        [Inject] private IState<GetEmployeesState>? _employeeState { get; set; }
        [Inject] private EmployeeAggregateStateFacade? _facade { get; set; }

        [Parameter] public Guid EmployeeId { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            if (EmployeeId != default)
            {
                if (_employeeState!.Value.EmployeeDetailModel is null || _employeeState!.Value.EmployeeDetailModel!.EmployeeId != EmployeeId)
                {
                    _facade!.GetEmployeeDetails(EmployeeId.ToString());
                }

                if (_detailModalRef is not null)
                {
                    await _detailModalRef!.Show();
                }

                await InvokeAsync(StateHasChanged);
            }

            await base.OnParametersSetAsync();
        }

        private Task OnSelectedTabChanged(string name)
        {
            selectedTab = name;

            return Task.CompletedTask;
        }

        private async Task CloseDialog() => await _detailModalRef!.Hide();

        private string ConvertIsActiveToString()
            => _employeeState!.Value.EmployeeDetailModel!.IsActive ? "Active" : "Inactive";

        private string ConvertIsSupervisorToString()
            => _employeeState!.Value.EmployeeDetailModel!.IsSupervisor ? "Yes" : "No";

        private string HideEmptyDate()
            => (_employeeState!.Value.EmployeeDetailModel!.LastModifiedDate != default) ?
                    _employeeState!.Value.EmployeeDetailModel!.LastModifiedDate.ToShortDateString() :
                    "";

        private string ConvertCurrencyToString()
            => string.Format("{0:C}", _employeeState!.Value.EmployeeDetailModel!.PayRate);
    }
}