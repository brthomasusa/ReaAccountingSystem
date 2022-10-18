using ReaAccountingSys.Client.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Client.Store.UseCases.HumanResources.GetEmployeeDetails.Actions
{
    public class GetEmployeeDetailsSuccessAction
    {
        public GetEmployeeDetailsSuccessAction(EmployeeReadModel detailModel)
            => EmployeeDetailModel = detailModel;

        public EmployeeReadModel EmployeeDetailModel { get; init; }
    }
}