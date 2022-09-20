using MediatR;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate;
using ReaAccountingSys.Core.HumanResources.EmployeeAggregate.ValueObjects;
using ReaAccountingSys.Core.Interfaces.HumanResources;
using ReaAccountingSys.Infrastructure.Application.Commands.HumanResources;
using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.SharedKernel.CommonValueObjects;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Infrastructure.Application.Handlers.HumanResources
{
    public class CreateEmployeeCmdHandler : IRequestHandler<CreateEmployeeCmd, OperationResult<bool>>
    {
        private readonly IEmployeeAggregateWriteRepository _writeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCmdHandler(IEmployeeAggregateWriteRepository repo, IUnitOfWork uow)
            => (_writeRepo, _unitOfWork) = (repo, uow);

        public async Task<OperationResult<bool>> Handle(CreateEmployeeCmd request, CancellationToken cancellationToken)
        {
            try
            {
                Employee employee = new Employee
                (
                    EntityGuidID.Create(request.WriteModel.EmployeeId),
                    (EmployeeTypeEnum)Enum.ToObject(typeof(EmployeeTypeEnum), request.WriteModel.EmployeeType),
                    EntityGuidID.Create(request.WriteModel.SupervisorId),
                    PersonName.Create(request.WriteModel.LastName!, request.WriteModel.FirstName!, request.WriteModel.MiddleInitial!),
                    SocialSecurityNumber.Create(request.WriteModel.SSN!),
                    EmailAddress.Create(request.WriteModel.EmailAddress!),
                    PhoneNumber.Create(request.WriteModel.Telephone!),
                    Address.Create(request.WriteModel.AddressLine1!, request.WriteModel.AddressLine2!, request.WriteModel.City!, request.WriteModel.StateCode!, request.WriteModel.Zipcode!),
                    MaritalStatus.Create(request.WriteModel.MaritalStatus!),
                    TaxExemption.Create(request.WriteModel.Exemptions),
                    PayRate.Create(request.WriteModel.PayRate),
                    StartDate.Create(request.WriteModel.StartDate),
                    request.WriteModel.IsActive,
                    request.WriteModel.IsSupervisor
                );

                OperationResult<bool> addResult = await _writeRepo.AddAsync(employee);

                if (addResult.Success)
                {
                    await _unitOfWork.Commit();
                    return OperationResult<bool>.CreateSuccessResult(true);
                }
                else
                {
                    return OperationResult<bool>.CreateFailure(addResult.NonSuccessMessage!);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.CreateFailure(ex.Message);
            }
            throw new NotImplementedException();
        }
    }
}