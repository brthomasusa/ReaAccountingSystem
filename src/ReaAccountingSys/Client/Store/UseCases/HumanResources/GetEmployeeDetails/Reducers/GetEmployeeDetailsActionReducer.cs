using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Reducers
{
    public static class GetEmployeeDetailsActionReducer
    {
        [ReducerMethod]
        public static GetEmployeesState ReduceSearchEmployeesByNameAction
        (
            GetEmployeesState state,
            GetEmployeeDetailsAction _
        ) =>
             new GetEmployeesState
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
                    state.SearchTerm,
                    state.EmployeeManagers,
                    state.EmployeeTypes
                );

        [ReducerMethod]
        public static GetEmployeesState ReduceSearchEmployeesByNameSuccessAction
        (
            GetEmployeesState state,
            GetEmployeeDetailsSuccessAction action
        ) =>
             new GetEmployeesState
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
                    state.SearchTerm,
                    state.EmployeeManagers,
                    state.EmployeeTypes
                );

        [ReducerMethod]
        public static GetEmployeesState ReduceSearchEmployeesByNameFailureAction
        (
            GetEmployeesState state,
            GetEmployeeDetailsFailureAction action
        ) =>
             new GetEmployeesState
                (
                    false,
                    action.ErrorMessage,
                    state.EmployeeList,
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