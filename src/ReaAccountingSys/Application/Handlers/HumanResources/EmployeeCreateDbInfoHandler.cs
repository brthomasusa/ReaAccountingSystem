using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.Events;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.ValueObjects;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.CommonValueObjects;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Application.Handlers.HumanResources
{
    public class EmployeeCreateDbInfoHandler : CommandHandler<CreateEmployeeCommand>
    {
        // The delegate below can be replaced with Func<Employee, Task<OperationResult<bool>>> (Func<T,TResult>)
        // private delegate Task<OperationResult<bool>> HandleEmployeeAsync(Employee employee);

        private readonly IWriteRepositoryManager _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeCreateDbInfoHandler
        (
            IWriteRepositoryManager writeRepo,
            IUnitOfWork uow
        )
            => (_writeRepository, _unitOfWork) = (writeRepo, uow);

        public override async Task<OperationResult<bool>> Handle(CreateEmployeeCommand command)
        {
            try
            {
                Employee employee = Employee.Create
                (
                    EntityGuidID.Create(command.WriteModel.EmployeeId),
                    (EmployeeTypeEnum)Enum.ToObject(typeof(EmployeeTypeEnum), command.WriteModel.EmployeeType),
                    EntityGuidID.Create(command.WriteModel.SupervisorId),
                    PersonName.Create(command.WriteModel.LastName!, command.WriteModel.FirstName!, command.WriteModel.MiddleInitial!),
                    SocialSecurityNumber.Create(command.WriteModel.SSN!),
                    EmailAddress.Create(command.WriteModel.EmailAddress!),
                    PhoneNumber.Create(command.WriteModel.Telephone!),
                    Address.Create(command.WriteModel.AddressLine1!, command.WriteModel.AddressLine2!, command.WriteModel.City!, command.WriteModel.StateCode!, command.WriteModel.Zipcode!),
                    MaritalStatus.Create(command.WriteModel.MaritalStatus!),
                    TaxExemption.Create(command.WriteModel.Exemptions),
                    PayRate.Create(command.WriteModel.PayRate),
                    StartDate.Create(command.WriteModel.StartDate),
                    command.WriteModel.IsActive,
                    command.WriteModel.IsSupervisor
                );

                OperationResult<bool>? createEmplResult = null;
                OperationResult<bool>? updateMgrStatusResult = null;

                if (employee.IsSupervisor)
                {
                    // Enforce rule that a group can only have one supervisor at a time.
                    // If this is a new supervisor, set IsSupervisor flag of current supervisor
                    // to false and set their SupervisorId to the id of this new employee.

                    Task<OperationResult<bool>> createTask = _writeRepository.EmployeeAggregate.AddAsync(employee);
                    createEmplResult = createTask.Result;

                    Task<OperationResult<bool>> updateTask = ImplementOneManagerPerGroupRule(employee);
                    updateMgrStatusResult = updateTask.Result;

                    await createTask;
                    await updateTask;

                    if ((createEmplResult is null || !createEmplResult.Success) ||
                        (updateMgrStatusResult is null || !updateMgrStatusResult.Success))
                    {
                        string createErrMsg = !string.IsNullOrEmpty(createEmplResult!.NonSuccessMessage) ? createEmplResult!.NonSuccessMessage : "Create new employee manager failed!";
                        string updateErrMsg = !string.IsNullOrEmpty(updateMgrStatusResult!.NonSuccessMessage) ? updateMgrStatusResult!.NonSuccessMessage : "Update existing manager status failed!";

                        System.Text.StringBuilder builder = new();

                        if (createErrMsg is not null)
                            builder.Append(createErrMsg);

                        if (updateErrMsg is not null)
                            builder.Append(updateErrMsg);

                        return OperationResult<bool>.CreateFailure($"{builder.ToString()}");
                    }
                }
                else
                {
                    Task<OperationResult<bool>> createTask = _writeRepository.EmployeeAggregate.AddAsync(employee);
                    createEmplResult = createTask.Result;

                    await createTask;

                    if ((createEmplResult is null || !createEmplResult.Success))
                    {
                        string createErrMsg = !string.IsNullOrEmpty(createEmplResult!.NonSuccessMessage) ? createEmplResult!.NonSuccessMessage : "Create new employee info failed!";

                        return OperationResult<bool>.CreateFailure($"{createErrMsg!}");
                    }
                }

                await _unitOfWork.Commit();

                if (Next is not null)
                {
                    await Next.Handle(command);
                }

                return OperationResult<bool>.CreateSuccessResult(true);
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(GetExceptionMessage(ex));
            }
        }

        private async Task<OperationResult<bool>> ImplementOneManagerPerGroupRule(Employee employee)
        {
            try
            {
                OperationResult<Employee> getResult =
                    await _writeRepository.EmployeeAggregate.GetByConditionAsync(emp => emp.IsSupervisor && emp.EmployeeType == employee.EmployeeType, true);

                if (getResult.Success)
                {
                    Employee empl = getResult.Result;
                    empl.UpdateSupervisorId(EntityGuidID.Create(employee.SupervisorId));
                    empl.UpdateIsSupervisor(false);

                    OperationResult<bool> updateResult = _writeRepository.EmployeeAggregate.Update(empl);
                    if (updateResult.Success)
                        return OperationResult<bool>.CreateSuccessResult(true);

                    return OperationResult<bool>.CreateFailure(updateResult.NonSuccessMessage!);
                }
                else
                {
                    return OperationResult<bool>.CreateFailure(getResult.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(GetExceptionMessage(ex));
            }
        }

        private string GetExceptionMessage(Exception ex)
                => ex.InnerException == null ? ex.Message : ex.InnerException.Message;
    }
}

