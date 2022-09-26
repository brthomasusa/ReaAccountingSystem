using ReaAccountingSys.IntegrationTests.Base;
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
    }
}