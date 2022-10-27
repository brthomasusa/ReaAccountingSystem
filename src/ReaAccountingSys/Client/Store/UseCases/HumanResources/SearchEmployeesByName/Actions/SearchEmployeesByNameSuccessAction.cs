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
            int pageSize,
            int pageNumber
        )
        {
            CurrentEmployeeList = employees;
            SearchTerm = searchTerm;
            EmployeeListFilter = filterName;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public PagingResponse<EmployeeListItem> CurrentEmployeeList { get; }
        public string SearchTerm { get; }
        public string EmployeeListFilter { get; }
        public int PageSize { get; }
        public int PageNumber { get; }
    }
}