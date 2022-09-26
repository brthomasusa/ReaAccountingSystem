using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Infrastructure.Interfaces.HumanResources
{
    public interface IEmployeeAggregateValidationRepository
    {
        Task<OperationResult<UniqueEmployeeNameModel>> VerifyEmployeeNameIsUnique(UniqueEmployeeNameParameters queryParameter);
        Task<OperationResult<UniqueEmployeeEmailModel>> VerifyEmployeeEmailIsUnique(UniqueEmployeeEmailParameters queryParameter);
        Task<OperationResult<UniqueEmployeeSSNModel>> VerifyEmployeeSSNIsUnique(UniqueEmployeeSSNParameters queryParameter);
    }
}