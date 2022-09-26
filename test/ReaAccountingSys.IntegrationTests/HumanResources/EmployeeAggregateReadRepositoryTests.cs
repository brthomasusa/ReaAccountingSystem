using ReaAccountingSys.IntegrationTests.Base;
using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels;
using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationQueries;
using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.Infrastructure.Persistence.Repositories;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.IntegrationTests.HumanResources
{
    [Trait("Integration", "EmployeeAggregateReadRepository")]
    public class EmployeeAggregateReadRepositoryTests : TestBaseDapper
    {
        private readonly IReadRepositoryManager _readRepository;

        public EmployeeAggregateReadRepositoryTests()
            => _readRepository = new ReadRepositoryManager(_dapperCtx);

        [Fact]
        public async Task GetReadModelById_EmployeeAggregateReadRepository_ShouldSucceed()
        {
            Guid agentID = new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E");
            GetEmployeeParameter qryParam = new() { EmployeeID = agentID };

            OperationResult<EmployeeReadModel> result = await _readRepository.EmployeeAggregate.GetReadModelById(qryParam);

            Assert.True(result.Success);
            Assert.Equal(agentID, result.Result.EmployeeId);
            Assert.Equal("Ken J Sanchez", result.Result.ManagerFullName);
            Assert.Equal("Ken J Sanchez", result.Result.EmployeeFullName);
        }

        [Fact]
        public async Task GetEmployeeManagers_EmployeeAggregateReadRepository_ShouldSucceed()
        {
            GetEmployeeManagersParameters queryParameters = new();
            OperationResult<List<EmployeeManager>> result = await _readRepository.EmployeeAggregate.GetEmployeeManagers(queryParameters);

            Assert.True(result.Success);
            int count = result.Result.Count;
            Assert.Equal(6, count);
        }

        [Fact]
        public async Task GetAllListItems_EmployeeAggregateReadRepository_ShouldSucceed()
        {
            GetEmployeesParameters queryParameters = new() { Page = 1, PageSize = 15 };
            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItems(queryParameters);

            Assert.True(result.Success);
            int count = result.Result.Count;
            Assert.Equal(14, count);
        }

        [Fact]
        public async Task GetAllListItemsByStatus_EmployeeAggregateReadRepository_Active_ShouldSucceed()
        {
            GetEmployeesByStatusParameters queryParameters = new() { EmployeementStatus = true, Page = 1, PageSize = 15 };
            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItemsByStatus(queryParameters);

            Assert.True(result.Success);
            int count = result.Result.Count;
            Assert.Equal(14, count);
        }

        [Fact]
        public async Task GetAllListItemsByStatus_EmployeeAggregateReadRepository_Inactive_ShouldSucceed()
        {
            GetEmployeesByStatusParameters queryParameters = new() { EmployeementStatus = false, Page = 1, PageSize = 15 };
            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItemsByStatus(queryParameters);

            Assert.True(result.Success);
            int count = result.Result.Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task GetAllListItemsByName_EmployeeAggregateReadRepository_ShouldSucceed()
        {
            GetEmployeesByLastNameParameters queryParameters = new() { LastName = "san", Page = 1, PageSize = 15 };
            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItemsByName(queryParameters);

            Assert.True(result.Success);
            int count = result.Result.Count;
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task GetAllListItemsByNameAndStatus_EmployeeAggregateReadRepository_Active_ShouldSucceed()
        {
            GetEmployeesByNameAndStatusParameters queryParameters
                = new() { LastName = "san", EmployeementStatus = true, Page = 1, PageSize = 15 };

            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItemsByNameAndStatus(queryParameters);

            Assert.True(result.Success);
            int count = result.Result.Count;
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task GetAllListItemsByNameAndStatus_EmployeeAggregateReadRepository_Inactive_ShouldSucceed()
        {
            GetEmployeesByNameAndStatusParameters queryParameters
                = new() { LastName = "san", EmployeementStatus = false, Page = 1, PageSize = 15 };

            OperationResult<PagedList<EmployeeListItem>> result = await _readRepository.EmployeeAggregate.GetAllListItemsByNameAndStatus(queryParameters);

            Assert.True(result.Success);
            int count = result.Result.Count;
            Assert.Equal(0, count);
        }

        /*    Validation queries   */

        [Fact]
        public async Task VerifyEmployeeNameIsUnique_EmployeeAggregateValidationRepository_ShouldReturnFalse()
        {
            UniqueEmployeeNameParameters queryParameters = new("Sharon", "Salavaria", "C");

            OperationResult<UniqueEmployeeNameModel> result = await _readRepository.EmployeeValidation.VerifyEmployeeNameIsUnique(queryParameters);

            Assert.True(result.Success);
            Assert.False(result.Result.IsUnique);
        }

        [Fact]
        public async Task VerifyEmployeeNameIsUnique_EmployeeAggregateValidationRepository_ShouldReturnTrue()
        {
            UniqueEmployeeNameParameters queryParameters = new("Joey", "Blowhard", "C");

            OperationResult<UniqueEmployeeNameModel> result = await _readRepository.EmployeeValidation.VerifyEmployeeNameIsUnique(queryParameters);

            Assert.True(result.Success);
            Assert.True(result.Result.IsUnique);
        }

        [Fact]
        public async Task VerifyEmployeeEmailIsUnique_EmployeeAggregateValidationRepository_ShouldReturnFalse()
        {
            UniqueEmployeeEmailParameters queryParameters = new("sharon.Salavaria@pipefitterssupply.com");

            OperationResult<UniqueEmployeeEmailModel> result = await _readRepository.EmployeeValidation.VerifyEmployeeEmailIsUnique(queryParameters);

            Assert.True(result.Success);
            Assert.False(result.Result.IsUnique);
        }

        [Fact]
        public async Task VerifyEmployeeEmailIsUnique_EmployeeAggregateValidationRepository_ShouldReturnTrue()
        {
            UniqueEmployeeEmailParameters queryParameters = new("peter.pan@pipefitterssupply.com");

            OperationResult<UniqueEmployeeEmailModel> result = await _readRepository.EmployeeValidation.VerifyEmployeeEmailIsUnique(queryParameters);

            Assert.True(result.Success);
            Assert.True(result.Result.IsUnique);
        }

        [Fact]
        public async Task VerifyEmployeeSSNIsUnique_EmployeeAggregateValidationRepository_ShouldReturnFalse()
        {
            UniqueEmployeeSSNParameters queryParameters = new("825559874");

            OperationResult<UniqueEmployeeSSNModel> result = await _readRepository.EmployeeValidation.VerifyEmployeeSSNIsUnique(queryParameters);

            Assert.True(result.Success);
            Assert.False(result.Result.IsUnique);
        }

        [Fact]
        public async Task VerifyEmployeeSSNIsUnique_EmployeeAggregateValidationRepository_ShouldReturnTrue()
        {
            UniqueEmployeeSSNParameters queryParameters = new("825559111");

            OperationResult<UniqueEmployeeSSNModel> result = await _readRepository.EmployeeValidation.VerifyEmployeeSSNIsUnique(queryParameters);

            Assert.True(result.Success);
            Assert.True(result.Result.IsUnique);
        }
    }
}