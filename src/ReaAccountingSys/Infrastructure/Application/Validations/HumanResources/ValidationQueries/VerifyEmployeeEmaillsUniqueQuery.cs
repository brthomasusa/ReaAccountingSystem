using System.Data;
using Dapper;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels;

namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationQueries
{
    public class VerifyEmployeeEmaillsUniqueQuery
    {
        public async static Task<OperationResult<UniqueEmployeeEmailModel>> Query(UniqueEmployeeEmailParameters queryParameters, DapperContext ctx)
        {
            try
            {
                var sql = EmployeeAggregateValidationSqlStatements.IsUniqueEmployeeEmail;

                var parameters = new DynamicParameters();
                parameters.Add("email", queryParameters.EmailAddress, DbType.String);

                using (var connection = ctx.CreateConnection())
                {
                    int count = await connection.ExecuteScalarAsync<int>(sql, parameters);
                    UniqueEmployeeEmailModel model = new(IsUnique: count > 0 ? false : true);

                    return OperationResult<UniqueEmployeeEmailModel>.CreateSuccessResult(model);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<UniqueEmployeeEmailModel>.CreateFailure(ex.Message);
            }
        }
    }
}