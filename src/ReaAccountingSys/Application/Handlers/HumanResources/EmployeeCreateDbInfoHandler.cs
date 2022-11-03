using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.Events;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.ValueObjects;
using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.CommonValueObjects;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Application.Handlers.HumanResources
{
    public class EmployeeCreateDbInfoHandler : CommandHandler<CreateEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _repository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeCreateDbInfoHandler(IWriteRepositoryManager repo, IUnitOfWork uow)
            => (_repository, _unitOfWork) = (repo, uow);

        public override async Task<OperationResult<bool>> Handle(CreateEmployeeCommand command)
        {
            try
            {
                Employee employee = new Employee
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

                OperationResult<bool> addResult = await _repository.EmployeeAggregate.AddAsync(employee);

                if (addResult.Success)
                {
                    // If this is a new supervisor, set IsSupervisor flag
                    // on current supervisor to false. Enforce rule that a
                    // can only have one supervisor at a time                    
                    employee.GroupManagerChangedHandler += OnGroupManagerChanged;
                    employee.HandleNewSupervisor();

                    await _unitOfWork.Commit();

                    if (Next is not null)
                    {
                        await Next.Handle(command);
                    }

                    return OperationResult<bool>.CreateSuccessResult(true);
                }
                else
                {
                    return OperationResult<bool>.CreateFailure(addResult.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(GetExceptionMessage(ex));
            }
        }

        private void OnGroupManagerChanged(GroupManagerChangedEvent evnt)
        {

        }

        private string GetExceptionMessage(Exception ex)
                => ex.InnerException == null ? ex.Message : ex.InnerException.Message;
    }
}