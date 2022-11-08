using ReaAccountingSys.SharedKernel.Interfaces;

namespace ReaAccountingSys.Core.HumanResources.EmployeeAggregate.Events
{
    public readonly record struct GroupMgrChangedEvent
    (
        object Sender,
        GroupManagerChangedEventArgs EventArgs
    ) : IDomainEvent;

}