using ReaAccountingSys.SharedKernel.Interfaces;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.SharedKernel
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        protected ICommandHandler<TCommand>? Next { get; private set; }

        public void SetNext(ICommandHandler<TCommand> next)
        {
            Next = next;
        }

        public virtual async Task<OperationResult<bool>> Handle(TCommand command)
        {
            if (Next is not null)
            {
                await Next.Handle(command);
            }

            return OperationResult<bool>.CreateSuccessResult(true);
        }
    }
}