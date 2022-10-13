using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions
{
    public class SearchEmployeesByNameSuccessAction
    {
        public SearchEmployeesByNameSuccessAction
        (
            PagingResponse<EmployeeListItem> employees,
            string searchTerm,
            string filterName,
            int pageSize
        )
        {
            CurrentEmployeeList = employees;
            SearchTerm = searchTerm;
            EmployeeListFilter = filterName;
            PageSize = pageSize;
        }

        public PagingResponse<EmployeeListItem> CurrentEmployeeList { get; }
        public string SearchTerm { get; }
        public string EmployeeListFilter { get; }
        public int PageSize { get; }
    }
}