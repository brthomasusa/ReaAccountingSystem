using ReaAccountingSys.IntegrationTests.Base;
using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Application.Handlers.HumanResources;
using ReaAccountingSys.Shared.ValidationModels.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.Infrastructure.Persistence.Repositories;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.IntegrationTests.HumanResources
{
    [Trait("Integration", "EmployeeAggregateWriteRepository")]
    public class EmployeeAggregateWriteRepositoryTests : TestBase
    {
        private readonly IReadRepositoryManager _readRepository;
        private readonly IWriteRepositoryManager _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeAggregateWriteRepositoryTests()
        {
            _readRepository = new ReadRepositoryManager(_dapperCtx);
            _writeRepository = new WriteRepositoryManager(_dbContext);
            _unitOfWork = new AppUnitOfWork(_dbContext);
        }

        [Fact]
        public async void Handle_EmployeeCreateDbRecordHandler_ValidModel_ShouldSucceed()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeCreateDbInfoHandler dbInfoHandler = new(_writeRepository, _unitOfWork);

            OperationResult<bool> result = await dbInfoHandler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Handle_EmployeeCreateDbRecordHandler_DuplicateName_ShouldFail()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.FirstName = "Jeffery";
            model.MiddleInitial = "W";
            model.LastName = "Beck";

            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            EmployeeCreateDbInfoHandler dbInfoHandler = new(_writeRepository, _unitOfWork);

            OperationResult<bool> result = await dbInfoHandler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public async void Handle_CreateEmployeeCommandHandler_ValidModel_ShouldSucceed()
        {
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            CreateEmployeeCommandHandler handler = new(_readRepository, _writeRepository, _unitOfWork);

            OperationResult<bool> result = await handler.Handle(command);

            Assert.True(result.Success);
        }

        [Fact]
        public async void Handle_CreateEmployeeCommandHandler_InvalidSalary_ShouldFail()
        {
            // FluentValidation test failure
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.PayRate = 50.00M;

            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            CreateEmployeeCommandHandler handler = new(_readRepository, _writeRepository, _unitOfWork);

            OperationResult<bool> result = await handler.Handle(command);

            Assert.False(result.Success);
        }

        [Fact]
        public async void Handle_CreateEmployeeCommandHandler_DuplicateSSN_ShouldFail()
        {
            // Business rule failure
            EmployeeWriteModel model = EmployeeAggregateTestData.GetEmployeeWriteModelCreate();
            model.SSN = "825559874";

            CreateEmployeeCommand command = new CreateEmployeeCommand { WriteModel = model };
            CreateEmployeeCommandHandler handler = new(_readRepository, _writeRepository, _unitOfWork);

            OperationResult<bool> result = await handler.Handle(command);

            Assert.False(result.Success);
        }
    }
}