using ReaAccountingSys.Shared.ValidationModels.HumanResources;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Infrastructure.Persistence.Interfaces.HumanResources
{
    public interface IEmployeeAggregateValidationRepository
    {
        Task<OperationResult<UniqueEmployeeNameModel>> VerifyEmployeeNameIsUnique(UniqueEmployeeNameParameters queryParameter);
        Task<OperationResult<UniqueEmployeeEmailModel>> VerifyEmployeeEmailIsUnique(UniqueEmployeeEmailParameters queryParameter);
        Task<OperationResult<UniqueEmployeeSSNModel>> VerifyEmployeeSSNIsUnique(UniqueEmployeeSSNParameters queryParameter);
    }
}