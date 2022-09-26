using ReaAccountingSys.Infrastructure.Application.Commands.HumanResources;
using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources;
using ReaAccountingSys.IntegrationTests.Base;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.IntegrationTests.HumanResources
{
    public class EmployeeValidationTests : TestBase
    {
        [Fact]
        public void Constructor_EmployeeDataValidationHandler_ShouldSucceed()
        {
            EmployeeDataValidationProcessor handler = new(new EmployeeWriteModelValidator());

            Assert.NotNull(handler);
        }

        [Fact]
        public async void Handle_EmployeeDataValidationHandler_ValidModel_ShouldSucceed()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeDataValidationProcessor handler = new(new EmployeeWriteModelValidator());

            OperationResult<bool> result = await handler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Handle_EmployeeDataValidationHandler_InvalidModel_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.PayRate = 6.00M;
            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeDataValidationProcessor handler = new(new EmployeeWriteModelValidator());

            OperationResult<bool> result = await handler.Handle(command);

            Assert.False(result.Success);
        }

    }
}