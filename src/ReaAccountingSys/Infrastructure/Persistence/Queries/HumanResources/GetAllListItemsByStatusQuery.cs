using System.Data;
using Dapper;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Queries.HumanResources
{
    public class GetAllListItemsByStatusQuery
    {
        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public async static Task<OperationResult<PagedList<EmployeeListItem>>> Query(GetEmployeesByStatusParameters queryParameters, DapperContext ctx)
        {
            try
            {
                var sql = EmployeeAggregateQueries.SelectAllEmployeeListItems +
                    " WHERE ee.IsActive = @IsActive" +
                    " ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial" +
                    " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                var parameters = new DynamicParameters();
                parameters.Add("IsActive", (queryParameters.EmployeementStatus ? 1 : 0), DbType.Int32);
                parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
                parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

                var totalRecordsSql = $"SELECT COUNT(EmployeeId) FROM HumanResources.Employees WHERE IsActive = @IsActive";

                using (var connection = ctx.CreateConnection())
                {
                    int count = await connection.ExecuteScalarAsync<int>(totalRecordsSql, parameters);
                    var items = await connection.QueryAsync<EmployeeListItem>(sql, parameters);
                    var pagedList = PagedList<EmployeeListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);

                    return OperationResult<PagedList<EmployeeListItem>>.CreateSuccessResult(pagedList);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<PagedList<EmployeeListItem>>.CreateFailure(ex.Message);
            }
        }
    }
}