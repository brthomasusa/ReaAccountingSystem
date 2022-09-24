using System.Data;
using Dapper;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Shared.ReadModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Queries.HumanResources
{
    public class GetReadModelByIdQuery
    {
        public async static Task<OperationResult<EmployeeReadModel>> Query(GetEmployeeParameter queryParameters, DapperContext ctx)
        {
            try
            {
                var sql = EmployeeAggregateQueries.SelectReadModel +
                " WHERE ee.EmployeeId = @ID";

                var parameters = new DynamicParameters();
                parameters.Add("ID", queryParameters.EmployeeID, DbType.Guid);

                using (var connection = ctx.CreateConnection())
                {
                    EmployeeReadModel detail = await connection.QueryFirstOrDefaultAsync<EmployeeReadModel>(sql, parameters);
                    return OperationResult<EmployeeReadModel>.CreateSuccessResult(detail);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<EmployeeReadModel>.CreateFailure(ex.Message);
            }
        }
    }
}