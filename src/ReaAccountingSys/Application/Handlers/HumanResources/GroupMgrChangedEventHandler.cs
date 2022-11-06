#pragma warning disable CS8600

using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.Events;
using ReaAccountingSys.SharedKernel.Interfaces;

namespace ReaAccountingSys.Application.Handlers.HumanResources
{
    public class GroupMgrChangedEventHandler : IDomainEventHandler<GroupMgrChangedEvent>
    {
        public void Handle(GroupMgrChangedEvent evnt)
        {
            Employee employee = evnt.Sender as Employee;
            Console.WriteLine($"Employee: {evnt.EventArgs.Employee.EmployeeName.FirstName} {evnt.EventArgs.Employee.EmployeeName.LastName}");

            //     OperationResult<Employee> result =
            //         await _writeRepository.EmployeeAggregate.GetByConditionAsync(emp => emp.IsSupervisor && emp.EmployeeType == EmployeeTypeEnum.Accountant, true);

            //     if (result.Success)
            //     {
            //         Employee empl = result.Result;
            //         empl.UpdateSupervisorId(EntityGuidID.Create(evnt.Employee.SupervisorId));
            //         empl.UpdateIsSupervisor(false);
            //         OperationResult<bool> updateResult = _writeRepository.EmployeeAggregate.Update(empl);            
        }
    }
}