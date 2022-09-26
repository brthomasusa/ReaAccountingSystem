using System.Data;
using Dapper;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels;

namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationQueries
{
    public class VerifyEmployeeSSNIsNUniqueQuery
    {
        public async static Task<OperationResult<UniqueEmployeeSSNModel>> Query(UniqueEmployeeSSNParameters queryParameters, DapperContext ctx)
        {
            try
            {
                var sql = EmployeeAggregateValidationSqlStatements.IsUniqueEmployeeSSN;

                var parameters = new DynamicParameters();
                parameters.Add("ssn", queryParameters.SSN, DbType.String);

                using (var connection = ctx.CreateConnection())
                {
                    int count = await connection.ExecuteScalarAsync<int>(sql, parameters);
                    UniqueEmployeeSSNModel model = new(IsUnique: count > 0 ? false : true);

                    return OperationResult<UniqueEmployeeSSNModel>.CreateSuccessResult(model);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<UniqueEmployeeSSNModel>.CreateFailure(ex.Message);
            }
        }
    }
}