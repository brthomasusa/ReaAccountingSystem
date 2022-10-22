using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Reducers
{
    public static class InitializeEmployeeWriteModelReducer
    {
        [ReducerMethod]
        public static EmployeesState ReduceInitializeEmployeeWriteModelAction
        (
            EmployeesState state,
            InitializeEmployeeWriteModelAction action
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
                    state.EmployeeListFilter,
                    action.CreateModel,
                    state.EmployeeEditModel,
                    state.SearchTerm,
                    state.EmployeeManagers,
                    state.EmployeeTypes
                );
    }
}