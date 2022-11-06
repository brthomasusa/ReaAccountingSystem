using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.EventArguments;
using ReaAccountingSys.SharedKernel.Interfaces;

namespace ReaAccountingSys.Core.HumanResources.EmployeeAggregate.Events
{
    public readonly record struct GroupMgrChangedEvent
    (
        object Sender,
        GroupMgrChangedEventArgs EventArgs
    ) : IDomainEvent;

}