using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Client.Store.State.HumanResources
{
    public class EmployeesState : RootState
    {
        public EmployeesState
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
            EmployeeWriteModel? editModel
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
        }

        public PagingResponse<EmployeeListItem>? EmployeeList { get; init; }
        public EmployeeReadModel? EmployeeDetailModel { get; init; }
        public EmployeeWriteModel? EmployeeCreateModel { get; init; }
        public EmployeeWriteModel? EmployeeEditModel { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string CreatePagePath { get; init; }
        public string EmployeeListFilter { get; init; }
    }
}