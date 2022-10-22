using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources
{
    public class EmployeeNode : Feature<EmployeesState>
    {
        public override string GetName() => "EmployeeAggregateNode";

        protected override EmployeesState GetInitialState() =>
            new EmployeesState
                (
                    false,
                    null,
                    null,
                    null,
                    1,
                    5,
                    @"/HumanResouces/Pages/EmployeeCreatePage",
                    "all",
                    null,
                    null,
                    string.Empty,
                    null,
                    null
                );
    }
}