using ReaAccountingSys.Application.Commands.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Application.Handlers.HumanResources
{
    public class CreateEmployeeCommandHandler
    {
        private readonly IReadRepositoryManager _readRepository;
        private readonly IWriteRepositoryManager _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler
        (
            IReadRepositoryManager readRepository,
            IWriteRepositoryManager writeRepository,
            IUnitOfWork unitOfWork
        )
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<bool>> Handle(CreateEmployeeCommand command)
        {
            try
            {
                EmployeeDataValidationHandler dataValidator = new();
                EmployeeBusinessRuleValidationHandler businessRuleValidator = new(_readRepository);
                EmployeeCreateDbInfoHandler createDbRecordHandler = new(_writeRepository, _unitOfWork);

                dataValidator.SetNext(businessRuleValidator);
                businessRuleValidator.SetNext(createDbRecordHandler);

                return await dataValidator.Handle(command);
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