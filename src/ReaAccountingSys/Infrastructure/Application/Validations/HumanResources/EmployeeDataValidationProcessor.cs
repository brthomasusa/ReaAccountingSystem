using System.Text;
using FluentValidation;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.Utilities;
using ReaAccountingSys.Infrastructure.Application.Commands.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;

namespace ReaAccountingSys.Infrastructure.Application.Validations.HumanResources
{
    public class EmployeeDataValidationProcessor : CommandHandler<CreateEmployeeCommand>
    {
        private readonly IValidator<EmployeeWriteModel> _validator;

        public EmployeeDataValidationProcessor(IValidator<EmployeeWriteModel> validator)
            : base()
            => _validator = validator;

        public override async Task<OperationResult<bool>> Handle(CreateEmployeeCommand command)
        {
            var result = await _validator.ValidateAsync(command.WriteModel);

            if (result.IsValid)
            {
                if (Next is not null)
                {
                    return await Next.Handle(command);
                }

                return OperationResult<bool>.CreateSuccessResult(true);
            }
            else
            {
                StringBuilder builder = new();
                builder.AppendLine("Create employee failed. Reason: ");

                result.Errors.ToList().ForEach
                (
                    error => builder.AppendLine($"{error.ErrorMessage}")
                );

                return OperationResult<bool>.CreateFailure(builder.ToString());
            }
        }
    }
}