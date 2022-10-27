using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Reducers
{
    public static class CreateNewEmployeeReducer
    {
        [ReducerMethod]
        public static CreateEmployeeState ReduceEmployeeSubmitAction
        (
            CreateEmployeeState state,
            EmployeeSubmitAction action
        ) =>
             new CreateEmployeeState
                (
                    true,
                    state.CurrentErrorMessage,
                    state.Submitted,
                    action.Model
                );

        [ReducerMethod]
        public static CreateEmployeeState ReduceEmployeeSubmitSuccessAction
        (
            CreateEmployeeState state,
            EmployeeSubmitAction action
        ) =>
             new CreateEmployeeState
                (
                    false,
                    state.CurrentErrorMessage,
                    true,
                    new()
                );

        [ReducerMethod]
        public static CreateEmployeeState ReduceEmployeeSubmitFailureAction
        (
            CreateEmployeeState state,
            EmployeeSubmitFailureAction action
        ) =>
             new CreateEmployeeState
                (
                    false,
                    action.ErrorMessage,
                    state.Submitted,
                    state.Model
                );
    }
}