using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Actions;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.CreateNewEmployee.Reducers
{
    public static class CreateNewEmployeeReducer
    {
        [ReducerMethod]
        public static CreateEmployeeState ReduceEmployeeCreateModelInitAction
        (
            CreateEmployeeState state,
            InitializeEmployeeWriteModelAction action
        ) =>
             new CreateEmployeeState
                (
                    false,
                    null,
                    false,
                    action.CreateModel
                );

        [ReducerMethod]
        public static CreateEmployeeState ReduceSetEmployeeCreateModelToNullAction
        (
            CreateEmployeeState state,
            SetEmployeeCreateModelToNullAction action
        ) =>
             new CreateEmployeeState
                (
                    false,
                    null,
                    false,
                    null
                );

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
                    state.Model
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
                    null,
                    true,
                    null
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