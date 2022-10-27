using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.SearchEmployeesByName.Reducers
{
    public static class SearchEmployeesByNameActionReducer
    {
        [ReducerMethod]
        public static GetEmployeesState ReduceSearchEmployeesByNameAction
        (
            GetEmployeesState state,
            SearchEmployeesByNameAction _
        ) =>
             new GetEmployeesState
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
                    state.SearchTerm,
                    state.EmployeeManagers,
                    state.EmployeeTypes
                );

        [ReducerMethod]
        public static GetEmployeesState ReduceSearchEmployeesByNameSuccessAction
        (
            GetEmployeesState state,
            SearchEmployeesByNameSuccessAction action
        ) =>
             new GetEmployeesState
                (
                    false,
                    null,
                    action.CurrentEmployeeList,
                    state.EmployeeDetailModel,
                    action.PageNumber,
                    action.PageSize,
                    state.CreatePagePath,
                    action.EmployeeListFilter!,
                    state.EmployeeCreateModel,
                    state.EmployeeEditModel,
                    action.SearchTerm,
                    state.EmployeeManagers,
                    state.EmployeeTypes
                );

        [ReducerMethod]
        public static GetEmployeesState ReduceSearchEmployeesByNameFailureAction
        (
            GetEmployeesState state,
            SearchEmployeesByNameFailureAction action
        ) =>
             new GetEmployeesState
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
                    state.SearchTerm,
                    state.EmployeeManagers,
                    state.EmployeeTypes
                );
    }
}