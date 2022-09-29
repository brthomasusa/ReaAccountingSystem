using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Application.Validations.HumanResources.BusinessRules;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.Utilities;
using ValidationResult = ReaAccountingSys.SharedKernel.ValidationResult;

namespace ReaAccountingSys.Application.Handlers.HumanResources
{
    public class EmployeeBusinessRuleValidationHandler : CommandHandler<CreateEmployeeCommand>
    {
        private readonly IReadRepositoryManager _repository;

        public EmployeeBusinessRuleValidationHandler(IReadRepositoryManager repo)
            => _repository = repo;

        public override async Task<OperationResult<bool>> Handle(CreateEmployeeCommand command)
        {
            EmployeeNameMustBeUnique verifyNameIsUnique = new(_repository);
            EmployeeEmailMustBeUnique verifyEmailIsUnique = new(_repository);
            EmployeeSSNMustBeUnique verifySSNIsUnique = new(_repository);

            verifyNameIsUnique.SetNext(verifyEmailIsUnique);
            verifyEmailIsUnique.SetNext(verifySSNIsUnique);

            ValidationResult result = await verifyNameIsUnique.Validate(command.WriteModel);

            if (Next is not null)
            {
                await Next.Handle(command);
            }

            if (result.IsValid)
            {
                return OperationResult<bool>.CreateSuccessResult(true);
            }

            return OperationResult<bool>.CreateFailure(result.Messages[0]);
        }
    }
}