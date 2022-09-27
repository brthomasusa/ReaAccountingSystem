using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;

namespace ReaAccountingSys.Infrastructure.Interfaces
{
    public class DomainObjectStateProcessor<TCommand> //: ICommandHandler<TCommand>
    {
        // private readonly IEmployeeAggregateReadRepository _readRepository;

        // private ICommandHandler<TCommand>? _nextProcessor;

        // public DomainObjectStateProcessor(IEmployeeAggregateReadRepository repo, ICommandHandler<TCommand> next)
        // {
        //     _readRepository = repo;
        //     _nextProcessor = next;
        // }

        // public Task Handle(TCommand command)
        // {
        //     if (_nextProcessor is not null)
        //         _nextProcessor.Handle(command);

        //     return Task.CompletedTask;
        // }
    }
}