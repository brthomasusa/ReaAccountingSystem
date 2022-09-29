using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Application.Handlers.HumanResources;
using ReaAccountingSys.Application.Validations.HumanResources.BusinessRules;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.Infrastructure.Persistence.Repositories;
using ReaAccountingSys.IntegrationTests.Base;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;
using ValidationResult = ReaAccountingSys.SharedKernel.ValidationResult;

namespace ReaAccountingSys.IntegrationTests.HumanResources
{
    public class EmployeeValidationTests : TestBase
    {
        private readonly IReadRepositoryManager _repository;

        public EmployeeValidationTests() : base()
            => _repository = new ReadRepositoryManager(_dapperCtx);

        /*     Employee data validation (FluentValidation)     */

        [Fact]
        public async void Handle_EmployeeDataValidationHandler_ValidModel_ShouldSucceed()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeDataValidationHandler handler = new();

            OperationResult<bool> result = await handler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Handle_EmployeeDataValidationHandler_InvalidModel_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.PayRate = 6.00M;
            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeDataValidationHandler handler = new();

            OperationResult<bool> result = await handler.Handle(command);

            Assert.False(result.Success);
        }

        /*     Employee business rules validation     */

        [Fact]
        public async void Validate_EmployeeNameMustBeUnique_ValidModel_ShouldSucceed()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            EmployeeNameMustBeUnique rule = new(_repository);

            ValidationResult result = await rule.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Validate_EmployeeNameMustBeUnique_DuplicateName_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.FirstName = "Jeffery";
            model.MiddleInitial = "W";
            model.LastName = "Beck";

            EmployeeNameMustBeUnique rule = new(_repository);

            ValidationResult result = await rule.Validate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async void Validate_EmployeeEmailMustBeUnique_ValidModel_ShouldSucceed()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            EmployeeEmailMustBeUnique rule = new(_repository);

            ValidationResult result = await rule.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Validate_EmployeeEmailMustBeUnique_DuplicateEmail_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.EmailAddress = "jeffery.beck@pipefitterssupply.com";

            EmployeeEmailMustBeUnique rule = new(_repository);

            ValidationResult result = await rule.Validate(model);

            Assert.False(result.IsValid);
        }

        [Fact]
        public async void Validate_EmployeeSSNMustBeUnique_ValidModel_ShouldSucceed()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            EmployeeSSNMustBeUnique rule = new(_repository);

            ValidationResult result = await rule.Validate(model);

            Assert.True(result.IsValid);
        }

        [Fact]
        public async void Validate_EmployeeSSNMustBeUnique_DuplicateSSN_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.SSN = "825559874";

            EmployeeSSNMustBeUnique rule = new(_repository);

            ValidationResult result = await rule.Validate(model);

            Assert.False(result.IsValid);
        }

        /*     Employee chained business rules validation     */

        [Fact]
        public async void Handle_EmployeeBusinessRuleValidationHandler_ValidModel_ShouldSucceed()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeBusinessRuleValidationHandler handler = new(_repository);

            OperationResult<bool> result = await handler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Handle_EmployeeBusinessRuleValidationHandler_DuplicateName_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.FirstName = "Jeffery";
            model.MiddleInitial = "W";
            model.LastName = "Beck";

            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeBusinessRuleValidationHandler handler = new(_repository);

            OperationResult<bool> result = await handler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public async void Handle_EmployeeBusinessRuleValidationHandler_DuplicateEmail_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.EmailAddress = "jeffery.beck@pipefitterssupply.com";

            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeBusinessRuleValidationHandler handler = new(_repository);

            OperationResult<bool> result = await handler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public async void Handle_EmployeeBusinessRuleValidationHandler_DuplicateSSN_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.SSN = "825559874";

            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeBusinessRuleValidationHandler handler = new(_repository);

            OperationResult<bool> result = await handler.Handle(command);

            Assert.False(result.Success);
        }

    }
}