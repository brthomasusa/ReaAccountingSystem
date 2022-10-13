using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Reducers
{
    public static class SearchEmployeesByNameActionReducer
    {
        [ReducerMethod]
        public static EmployeesState ReduceSearchEmployeesByNameAction
        (
            EmployeesState state,
            SearchEmployeesByNameAction _
        ) =>
             new EmployeesState
                (
                    true,
                    null,
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

        [ReducerMethod]
        public static EmployeesState ReduceSearchEmployeesByNameSuccessAction
        (
            EmployeesState state,
            SearchEmployeesByNameSuccessAction action
        ) =>
             new EmployeesState
                (
                    false,
                    null,
                    action.CurrentEmployeeList,
                    state.EmployeeDetailModel,
                    state.PageNumber,
                    action.PageSize,
                    state.CreatePagePath,
                    action.EmployeeListFilter!,
                    state.EmployeeCreateModel,
                    state.EmployeeEditModel,
                    action.SearchTerm
                );

        [ReducerMethod]
        public static EmployeesState ReduceSearchEmployeesByNameFailureAction
        (
            EmployeesState state,
            SearchEmployeesByNameFailureAction action
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