using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Client.Store.State.HumanResources
{
    public class CreateEmployeeState : RootState
    {
        public CreateEmployeeState
        (
            bool submitting,
            string? currentErrorMessage,
            bool submitted,
            EmployeeWriteModel model
        ) : base(submitting, currentErrorMessage)
        {
            Submitting = submitting;
            Submitted = submitted;
            Model = model;
        }

        public bool Submitting { get; init; }
        public bool Submitted { get; init; }
        public EmployeeWriteModel Model { get; init; }
    }
}