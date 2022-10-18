using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Reducers
{
    public static class GetEmployeeDetailsActionReducer
    {
        [ReducerMethod]
        public static EmployeesState ReduceSearchEmployeesByNameAction
        (
            EmployeesState state,
            GetEmployeeDetailsAction _
        ) =>
             new EmployeesState
                (
                    true,
                    null,
                    state.EmployeeList,
                    state.EmployeeDetailModel,
                    state.PageNumber,
                    state.PageSize,
                    state.CreatePagePath,
                    state.EmployeeListFilter,
                    state.EmployeeCreateModel,
                    state.EmployeeEditModel,
                    state.SearchTerm
                );

        [ReducerMethod]
        public static EmployeesState ReduceSearchEmployeesByNameSuccessAction
        (
            EmployeesState state,
            GetEmployeeDetailsSuccessAction action
        ) =>
             new EmployeesState
                (
                    false,
                    null,
                    state.EmployeeList,
                    action.EmployeeDetailModel,
                    state.PageNumber,
                    state.PageSize,
                    state.CreatePagePath,
                    state.EmployeeListFilter!,
                    state.EmployeeCreateModel,
                    state.EmployeeEditModel,
                    state.SearchTerm
                );

        [ReducerMethod]
        public static EmployeesState ReduceSearchEmployeesByNameFailureAction
        (
            EmployeesState state,
            GetEmployeeDetailsFailureAction action
        ) =>
             new EmployeesState
                (
                    false,
                    action.ErrorMessage,
                    null,
                    state.EmployeeDetailModel,
                    state.PageNumber,
                    state.PageSize,
                    state.CreatePagePath,
                    state.EmployeeListFilter,
                    state.EmployeeCreateModel,
                    state.EmployeeEditModel,
                    state.SearchTerm
                );
    }
}