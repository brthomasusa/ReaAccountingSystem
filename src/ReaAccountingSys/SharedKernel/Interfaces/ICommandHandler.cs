using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.SharedKernel.Interfaces
{
    public interface ICommandHandler<TCommand>
    {
        Task<OperationResult<bool>> Handle(TCommand command);

        void SetNext(ICommandHandler<TCommand> next);
    }
}