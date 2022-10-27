using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;


namespace ReaAccountingSys.Client.Store.State.HumanResources
{
    public class GetEmployeesState : RootState
    {
        public GetEmployeesState
        (
            bool isLoading,
            string? currentErrorMessage,
            PagingResponse<EmployeeListItem>? currentEmployeeList,
            EmployeeReadModel? currentEmployee,
            int pageNumber,
            int pageSize,
            string createPageHref,
            string employeeListFilter,
            EmployeeWriteModel? createModel,
            EmployeeWriteModel? editModel,
            string searchTerm,
            List<EmployeeManager>? managers,
            List<EmployeeTypes>? employeeTypes
        ) : base(isLoading, currentErrorMessage)
        {
            EmployeeList = currentEmployeeList;
            EmployeeDetailModel = currentEmployee;
            PageNumber = pageNumber;
            PageSize = pageSize;
            CreatePagePath = createPageHref;
            EmployeeListFilter = employeeListFilter;
            EmployeeCreateModel = createModel;
            EmployeeEditModel = editModel;
            SearchTerm = searchTerm;
            EmployeeManagers = managers;
            EmployeeTypes = employeeTypes;
        }

        public PagingResponse<EmployeeListItem>? EmployeeList { get; init; }
        public List<EmployeeManager>? EmployeeManagers { get; init; }
        public List<EmployeeTypes>? EmployeeTypes { get; init; }
        public EmployeeReadModel? EmployeeDetailModel { get; init; }
        public EmployeeWriteModel? EmployeeCreateModel { get; init; }
        public EmployeeWriteModel? EmployeeEditModel { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string CreatePagePath { get; init; }
        public string EmployeeListFilter { get; init; }
        public string SearchTerm { get; init; }
    }
}
