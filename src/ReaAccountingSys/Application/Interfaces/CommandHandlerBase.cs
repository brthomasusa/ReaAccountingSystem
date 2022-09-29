using ReaAccountingSys.Core.Interfaces;
using ReaAccountingSys.Application.Interfaces;
using ReaAccountingSys.Shared.Interfaces;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.Utilities;
using ValidationResult = ReaAccountingSys.SharedKernel.ValidationResult;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;

namespace ReaAccountingSys.Application.Interfaces
{
    public abstract class CommandHandlerBase<TCommand, TRepository, TService, TEntity>
        where TCommand : ICommand
        where TRepository : IAggregateRootRepository<TEntity>
        where TService : IValidationService
    {
        public CommandHandlerBase
        (
            TCommand command,
            TRepository repository,
            TService validationService,
            IUnitOfWork uow
        )
        {
            Command = command;
            Repository = repository;
            ValidationService = validationService;
            UnitOfWork = uow;
        }

        protected TCommand Command { get; init; }

        protected TRepository Repository { get; init; }

        protected TService ValidationService { get; init; }

        protected IUnitOfWork UnitOfWork { get; init; }

        public async Task<OperationResult<bool>> Process()
        {
            ValidationResult validationResult = await Validate();

            if (validationResult.IsValid)
            {
                OperationResult<bool> result = await ProcessCommand();

                if (result.Success)
                {
                    return OperationResult<bool>.CreateSuccessResult(true);
                }
                else
                {
                    return OperationResult<bool>.CreateFailure(result.NonSuccessMessage!);
                }
            }
            else
            {
                return OperationResult<bool>.CreateFailure(validationResult.Messages[0]);
            }
        }

        protected abstract Task<ValidationResult> Validate();

        protected abstract Task<OperationResult<bool>> ProcessCommand();
    }
}