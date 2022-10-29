using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources
{
    public class CreateEmployeeNode : Feature<CreateEmployeeState>
    {
        public override string GetName() => "CreateEmployeeNode";

        protected override CreateEmployeeState GetInitialState() =>
            new CreateEmployeeState
                (
                    false,
                    null,
                    false,
                    null
                );
    }
}