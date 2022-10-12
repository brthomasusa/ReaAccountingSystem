using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions
{
    public class GetAllEmployeesSuccessAction
    {
        public GetAllEmployeesSuccessAction
        (
            PagingResponse<EmployeeListItem> employees,
            string filterName,
            int pageSize
        )
        {
            CurrentEmployeeList = employees;
            EmployeeListFilter = filterName;
            PageSize = pageSize;
        }

        public PagingResponse<EmployeeListItem>? CurrentEmployeeList { get; }
        public string? EmployeeListFilter { get; }
        public int PageSize { get; }
    }
}