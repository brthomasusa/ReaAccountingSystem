using Dapper;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Dapper.HumanResources.Queries
{
    public class GetEmployeeManagersQuery
    {
        public async static Task<OperationResult<List<EmployeeManager>>> Query(GetEmployeeManagersParameters queryParameters, DapperContext ctx)
        {
            try
            {
                var sql = EmployeeAggregateQueries.SelectEmployeeManagers;

                using (var connection = ctx.CreateConnection())
                {
                    var managers = await connection.QueryAsync<EmployeeManager>(sql);
                    return OperationResult<List<EmployeeManager>>.CreateSuccessResult(managers.ToList());
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<EmployeeManager>>.CreateFailure(ex.Message);
            }
        }
    }
}