using Dapper;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Queries.HumanResources
{
    public class GetEmployeeTypesQuery
    {
        public async static Task<OperationResult<List<EmployeeTypes>>> Query(GetEmployeeTypesParameters queryParameters, DapperContext ctx)
        {
            try
            {
                var sql = EmployeeAggregateQueries.SelectEmployeeTypes;

                using (var connection = ctx.CreateConnection())
                {
                    var employeeTypes = await connection.QueryAsync<EmployeeTypes>(sql);
                    return OperationResult<List<EmployeeTypes>>.CreateSuccessResult(employeeTypes.ToList());
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<EmployeeTypes>>.CreateFailure(ex.Message);
            }
        }
    }
}