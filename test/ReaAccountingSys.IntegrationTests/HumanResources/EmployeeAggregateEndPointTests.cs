#pragma warning disable CS8600, CS8602
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

using ReaAccountingSys.IntegrationTests.Base;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.IntegrationTests.HumanResources
{
    [Trait("Integration", "TestServer")]
    public class EmployeeAggregateEndPointTests : IntegrationTest
    {
        public EmployeeAggregateEndPointTests(ApiWebApplicationFactory fixture) : base(fixture)
        { }

        [Fact]
        public async Task List_EmployeesController_ShouldSucceed()
        {
            var pagingParams = new { Page = 1, PageSize = 15 };

            var queryParams = new Dictionary<string, string?>
            {
                ["page"] = pagingParams.Page.ToString(),
                ["pageSize"] = pagingParams.PageSize.ToString()
            };

            List<EmployeeListItem> response = await _client
                .GetFromJsonAsync<List<EmployeeListItem>>(QueryHelpers.AddQueryString($"{_urlRoot}/employees/list", queryParams));

            Assert.Equal(14, response.Count);
        }

        [Fact]
        public async Task List_EmployeesController_Active_ShouldSucceed()
        {
            var pagingParams = new { EmployeementStatus = true, Page = 1, PageSize = 15 };

            var queryParams = new Dictionary<string, string?>
            {
                ["employeementStatus"] = pagingParams.EmployeementStatus.ToString(),
                ["page"] = pagingParams.Page.ToString(),
                ["pageSize"] = pagingParams.PageSize.ToString()
            };

            List<EmployeeListItem> response = await _client
                .GetFromJsonAsync<List<EmployeeListItem>>(QueryHelpers.AddQueryString($"{_urlRoot}/employees/list/bystatus", queryParams));

            Assert.Equal(14, response.Count);
        }

        [Fact]
        public async Task List_EmployeesController_Inactive_ShouldSucceed()
        {
            var pagingParams = new { EmployeementStatus = false, Page = 1, PageSize = 15 };

            var queryParams = new Dictionary<string, string?>
            {
                ["employeementStatus"] = pagingParams.EmployeementStatus.ToString(),
                ["page"] = pagingParams.Page.ToString(),
                ["pageSize"] = pagingParams.PageSize.ToString()
            };

            List<EmployeeListItem> response = await _client
                .GetFromJsonAsync<List<EmployeeListItem>>(QueryHelpers.AddQueryString($"{_urlRoot}/employees/list/bystatus", queryParams));

            int count = response.Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Search_EmployeesController_ShouldSucceed()
        {
            var pagingParams = new { LastName = "San", Page = 1, PageSize = 15 };

            var queryParams = new Dictionary<string, string?>
            {
                ["lastName"] = pagingParams.LastName,
                ["page"] = pagingParams.Page.ToString(),
                ["pageSize"] = pagingParams.PageSize.ToString()
            };

            List<EmployeeListItem> response = await _client
                .GetFromJsonAsync<List<EmployeeListItem>>(QueryHelpers.AddQueryString($"{_urlRoot}/employees/search", queryParams));

            int count = response.Count;
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Search_EmployeesController_Active_ShouldSucceed()
        {
            var pagingParams = new { LastName = "San", EmployeementStatus = true, Page = 1, PageSize = 15 };

            var queryParams = new Dictionary<string, string?>
            {
                ["lastName"] = pagingParams.LastName,
                ["employeementStatus"] = pagingParams.EmployeementStatus.ToString(),
                ["page"] = pagingParams.Page.ToString(),
                ["pageSize"] = pagingParams.PageSize.ToString()
            };

            List<EmployeeListItem> response = await _client
                .GetFromJsonAsync<List<EmployeeListItem>>(QueryHelpers.AddQueryString($"{_urlRoot}/employees/search/bystatus", queryParams));

            int count = response.Count;
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Search_EmployeesController_Inactive_ShouldSucceed()
        {
            var pagingParams = new { LastName = "San", EmployeementStatus = false, Page = 1, PageSize = 15 };

            var queryParams = new Dictionary<string, string?>
            {
                ["lastName"] = pagingParams.LastName,
                ["employeementStatus"] = pagingParams.EmployeementStatus.ToString(),
                ["page"] = pagingParams.Page.ToString(),
                ["pageSize"] = pagingParams.PageSize.ToString()
            };

            List<EmployeeListItem> response = await _client
                .GetFromJsonAsync<List<EmployeeListItem>>(QueryHelpers.AddQueryString($"{_urlRoot}/employees/search/bystatus", queryParams));

            int count = response.Count;
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Managers_EmployeesController_ShouldSucceed()
        {
            using var response = await _client.GetAsync($"{_urlRoot}/employees/managers",
                                                        HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();
            var employeeManagers = await JsonSerializer.DeserializeAsync<List<EmployeeManager>>(jsonResponse, _options);

            Assert.Equal(6, employeeManagers.Count);
        }














    }
}