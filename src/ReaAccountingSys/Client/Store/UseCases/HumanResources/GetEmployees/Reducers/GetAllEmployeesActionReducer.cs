using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployees.Reducers
{
    public static class GetAllEmployeesActionReducer
    {
        [ReducerMethod]
        public static GetEmployeesState ReduceGetEmployeesAction
        (
            GetEmployeesState state,
            GetEmployeesAction _
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
        public static GetEmployeesState ReduceGetEmployeesSuccessAction
        (
            GetEmployeesState state,
            GetAllEmployeesSuccessAction action
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
        public static GetEmployeesState ReduceGetEmployeesFailureAction
        (
            GetEmployeesState state,
            GetAllEmployeesFailureAction action
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