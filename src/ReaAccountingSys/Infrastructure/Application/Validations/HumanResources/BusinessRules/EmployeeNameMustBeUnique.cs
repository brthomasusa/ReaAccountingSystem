using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.ValidationModels;
using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.Utilities;
using ValidationResult = ReaAccountingSys.SharedKernel.ValidationResult;

namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.BusinessRules
{
    public class EmployeeNameMustBeUnique : BusinessRule<EmployeeWriteModel>
    {
        private readonly IReadRepositoryManager _repository;

        public EmployeeNameMustBeUnique(IReadRepositoryManager repo)
            => _repository = repo;

        public override async Task<ValidationResult> Validate(EmployeeWriteModel employee)
        {
            ValidationResult validationResult = new();

            UniqueEmployeeNameParameters queryParameters = new(employee.FirstName!, employee.LastName!, employee.MiddleInitial);
            OperationResult<UniqueEmployeeNameModel> result =
                await _repository.EmployeeValidation.VerifyEmployeeNameIsUnique(queryParameters);

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
                    string msg = @"An employee with the same name is alread in the database.";



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