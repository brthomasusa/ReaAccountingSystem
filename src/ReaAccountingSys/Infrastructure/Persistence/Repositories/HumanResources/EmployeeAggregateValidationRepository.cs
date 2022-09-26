using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels;
using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationQueries;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Infrastructure.Persistence.Repositories.HumanResources
{
    public class EmployeeAggregateValidationRepository : IEmployeeAggregateValidationRepository
    {
        private readonly DapperContext _dapperCtx;

        public EmployeeAggregateValidationRepository(DapperContext ctx) => _dapperCtx = ctx;

        public async Task<OperationResult<UniqueEmployeeNameModel>> VerifyEmployeeNameIsUnique(UniqueEmployeeNameParameters queryParameter)
            => await VerifyEmployeeNameIsUniqueQuery.Query(queryParameter, _dapperCtx);

        public async Task<OperationResult<UniqueEmployeeEmailModel>> VerifyEmployeeEmailIsUnique(UniqueEmployeeEmailParameters queryParameter)
            => await VerifyEmployeeEmaillsUniqueQuery.Query(queryParameter, _dapperCtx);

        public async Task<OperationResult<UniqueEmployeeSSNModel>> VerifyEmployeeSSNIsUnique(UniqueEmployeeSSNParameters queryParameter)
            => await VerifyEmployeeSSNIsNUniqueQuery.Query(queryParameter, _dapperCtx);
    }
}