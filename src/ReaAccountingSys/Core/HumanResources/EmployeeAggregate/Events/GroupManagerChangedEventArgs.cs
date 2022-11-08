namespace ReaAccountingSys.Core.HumanResources.EmployeeAggregate.Events
{
    public class GroupManagerChangedEventArgs : EventArgs
    {
        public GroupManagerChangedEventArgs(Employee employee)
            => Employee = employee;

        public Employee Employee { get; init; }
    }
}