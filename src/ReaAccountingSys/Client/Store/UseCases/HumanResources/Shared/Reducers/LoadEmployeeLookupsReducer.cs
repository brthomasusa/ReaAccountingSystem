using Fluxor;
using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Reducers
{
    public static class LoadEmployeeLookupsReducer
    {
        [ReducerMethod]
        public static GetEmployeesState ReduceLoadEmployeeLookupsAction
        (
            GetEmployeesState state,
            LoadEmployeeLookupsAction _
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
        public static GetEmployeesState ReduceLoadEmployeeLookupsSuccessAction
        (
            GetEmployeesState state,
            LoadEmployeeLookupsSuccessAction action
        ) =>
             new GetEmployeesState
                (
                    false,
                    null,
                    state.EmployeeList,
                    state.EmployeeDetailModel,
                    state.PageNumber,
                    state.PageSize,
                    state.CreatePagePath,
                    state.EmployeeListFilter!,
                    state.EmployeeCreateModel,
                    state.EmployeeEditModel,
                    state.SearchTerm,
                    action.EmployeeManagers,
                    action.EmployeeTypes
                );

        [ReducerMethod]
        public static GetEmployeesState ReduceGetEmployeesFailureAction
        (
            GetEmployeesState state,
            LoadEmployeeLookupsFailureAction action
        )
        {
            return new GetEmployeesState
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
}