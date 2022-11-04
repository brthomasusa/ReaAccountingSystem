namespace ReaAccountingSys.Core.HumanResources.EmployeeAggregate.EventArguments
{
    public class GroupMgrChangedEventArgs : EventArgs
    {
        public GroupMgrChangedEventArgs(Employee employee)
            => Employee = employee;

        public Employee Employee { get; init; }
    }
}