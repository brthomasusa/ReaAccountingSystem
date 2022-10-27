using Fluxor;
using ReaAccountingSys.Client.Store.State.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources
{
    public class GetEmployeeNode : Feature<GetEmployeesState>
    {
        public override string GetName() => "GetEmployeeNode";

        protected override GetEmployeesState GetInitialState() =>
            new GetEmployeesState
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