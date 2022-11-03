using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.Events;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.ValueObjects;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.Shared.ReadModels.HumanResources;
using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.CommonValueObjects;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Application.Handlers.HumanResources
{
    public class EmployeeCreateDbInfoHandler : CommandHandler<CreateEmployeeCommand>
    {
        private readonly IWriteRepositoryManager _writeRepository;
        private readonly IReadRepositoryManager _readRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeCreateDbInfoHandler
        (
            IWriteRepositoryManager writeRepo,
            IReadRepositoryManager readRepo,
            IUnitOfWork uow
        )
            => (_writeRepository, _readRepository, _unitOfWork) = (writeRepo, readRepo, uow);

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

                OperationResult<bool> addResult = await _writeRepository.EmployeeAggregate.AddAsync(employee);

                if (addResult.Success)
                {
                    // If this is a new supervisor, set IsSupervisor flag
                    // on current supervisor to false. Enforce rule that a
                    // can only have one supervisor at a time                    
                    employee.GroupManagerChangedHandler += OnGroupManagerChangedHandler;
                    await employee.HandleNewSupervisor();

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

        private async Task OnGroupManagerChangedHandler(GroupManagerChangedEvent evnt)
        {
            GetEmployeeManagersParameters parameters = new();
            OperationResult<List<EmployeeManager>> result =
                await _readRepository.EmployeeAggregate.GetEmployeeManagers(parameters);

            if (result.Success)
            {
                var query = from mgr in result.Result
                            where mgr.EmployeeTypeId == (int)evnt.Employee.EmployeeType
                            select mgr.ManagerId;

                OperationResult<Employee> writeResult = await _writeRepository.EmployeeAggregate
                                                                              .GetByIdAsync(query
                                                                              .FirstOrDefault());

                if (writeResult.Success)
                {
                    Employee empl = writeResult.Result;
                    empl.UpdateSupervisorId(EntityGuidID.Create(evnt.Employee.SupervisorId));
                    empl.UpdateIsSupervisor(false);
                    OperationResult<bool> updateResult = _writeRepository.EmployeeAggregate.Update(empl);
                }
            }
        }

        private string GetExceptionMessage(Exception ex)
                => ex.InnerException == null ? ex.Message : ex.InnerException.Message;
    }
}

