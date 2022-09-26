using System.Data;
using Dapper;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels;

namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationQueries
{
    public class VerifyEmployeeNameIsUniqueQuery
    {
        public async static Task<OperationResult<UniqueEmployeeNameModel>> Query(UniqueEmployeeNameParameters queryParameters, DapperContext ctx)
        {
            try
            {
                var sql = EmployeeAggregateValidationSqlStatements.IsUniqueEmployeeName;

                var parameters = new DynamicParameters();
                parameters.Add("fname", queryParameters.FirstName, DbType.String);
                parameters.Add("lname", queryParameters.LastName, DbType.String);

                if (queryParameters.MiddleInitial is not null)
                {
                    sql += " AND MiddleInitial = @mi";
                    parameters.Add("mi", queryParameters.MiddleInitial, DbType.String);
                }

                using (var connection = ctx.CreateConnection())
                {
                    int count = await connection.ExecuteScalarAsync<int>(sql, parameters);
                    UniqueEmployeeNameModel model = new(IsUnique: count > 0 ? false : true);

                    return OperationResult<UniqueEmployeeNameModel>.CreateSuccessResult(model);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<UniqueEmployeeNameModel>.CreateFailure(ex.Message);
            }
        }
    }
}