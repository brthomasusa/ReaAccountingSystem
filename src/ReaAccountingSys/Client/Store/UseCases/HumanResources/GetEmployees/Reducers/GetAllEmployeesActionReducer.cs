using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Reducers
{
    public static class GetAllEmployeesActionReducer
    {
        [ReducerMethod]
        public static EmployeesState ReduceGetEmployeesAction
        (
            EmployeesState state,
            GetEmployeesAction _
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
        public static EmployeesState ReduceGetEmployeesSuccessAction
        (
            EmployeesState state,
            GetAllEmployeesSuccessAction action
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
                    action.SearchTerm,
                    state.EmployeeManagers,
                    state.EmployeeTypes
                );

        [ReducerMethod]
        public static EmployeesState ReduceGetEmployeesFailureAction
        (
            EmployeesState state,
            GetAllEmployeesFailureAction action
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