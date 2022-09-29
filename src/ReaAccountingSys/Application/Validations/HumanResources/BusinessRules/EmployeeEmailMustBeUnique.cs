using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.Utilities;
using ValidationResult = ReaAccountingSys.SharedKernel.ValidationResult;
using ReaAccountingSys.Shared.ValidationModels.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;

namespace ReaAccountingSys.Application.Validations.HumanResources.BusinessRules
{
    public class EmployeeEmailMustBeUnique : BusinessRule<EmployeeWriteModel>
    {
        private readonly IReadRepositoryManager _repository;

        public EmployeeEmailMustBeUnique(IReadRepositoryManager repo)
            => _repository = repo;

        public override async Task<ValidationResult> Validate(EmployeeWriteModel employee)
        {
            ValidationResult validationResult = new();

            UniqueEmployeeEmailParameters queryParameters = new(employee.EmailAddress!);
            OperationResult<UniqueEmployeeEmailModel> result =
                await _repository.EmployeeValidation.VerifyEmployeeEmailIsUnique(queryParameters);

            if (result.Success)
            {
                if (result.Result.IsUnique)
                {
                    validationResult.IsValid = true;

                    if (Next is not null)
                    {
                        validationResult = await Next.Validate(employee);
                    }
                }
                else
                {
                    string msg = @"An employee with the same email address is alread in the database.";



                    validationResult.Messages.Add(msg);
                }
            }
            else
            {
                validationResult.Messages.Add(result.NonSuccessMessage!);
            }

            return validationResult;
        }
    }
}