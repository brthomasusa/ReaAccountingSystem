using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.Shared.Reducers
{
    public static class LoadEmployeeLookupsReducer
    {
        [ReducerMethod]
        public static EmployeesState ReduceLoadEmployeeLookupsAction
        (
            EmployeesState state,
            LoadEmployeeLookupsAction _
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
                    state.SearchTerm,
                    state.EmployeeManagers,
                    state.EmployeeTypes
                );

        [ReducerMethod]
        public static EmployeesState ReduceLoadEmployeeLookupsSuccessAction
        (
            EmployeesState state,
            LoadEmployeeLookupsSuccessAction action
        ) =>
             new EmployeesState
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
        public static EmployeesState ReduceGetEmployeesFailureAction
        (
            EmployeesState state,
            LoadEmployeeLookupsFailureAction action
        )
        {
            return new EmployeesState
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