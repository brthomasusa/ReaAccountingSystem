using ReaAccountingSys.Infrastructure.Application.Validations.HumanResources.BusinessRules;
using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ValidationResult = ReaAccountingSys.SharedKernel.ValidationResult;

namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources
{
    public class CreateEmployeeRulesValidationProcessor : ValidatorBase<EmployeeWriteModel, IReadRepositoryManager>
    {
        public CreateEmployeeRulesValidationProcessor
        (
            EmployeeWriteModel writeModel,
            IReadRepositoryManager readRepositoryManager

        ) : base(writeModel, readRepositoryManager)
        {

        }

        public override async Task<ValidationResult> Validate()
        {
            EmployeeNameMustBeUnique verifyNameIsUnique = new(RepositoryManager);
            EmployeeEmailMustBeUnique verifyEmailIsUnique = new(RepositoryManager);
            EmployeeSSNMustBeUnique verifySSNIsUnique = new(RepositoryManager);

            verifyNameIsUnique.SetNext(verifyEmailIsUnique);
            verifyEmailIsUnique.SetNext(verifySSNIsUnique);

            return await verifyNameIsUnique.Validate(WriteModel);
        }
    }
}