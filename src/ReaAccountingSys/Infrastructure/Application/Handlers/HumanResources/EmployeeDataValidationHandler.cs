using System.Text;
using ReaAccountingSys.Infrastructure.Application.Commands.HumanResources;
using ReaAccountingSys.Shared.WriteModels.HumanResources;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Infrastructure.Application.Handlers.HumanResources
{
    public class EmployeeDataValidationHandler : CommandHandler<CreateEmployeeCommand>
    {
        public override async Task<OperationResult<bool>> Handle(CreateEmployeeCommand command)
        {
            EmployeeWriteModelValidator validation = new();
            var result = await validation.ValidateAsync(command.WriteModel);

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