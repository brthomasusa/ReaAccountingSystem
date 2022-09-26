using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.Infrastructure.Persistence.Repositories.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Repositories
{
    public class ReadRepositoryManager : IReadRepositoryManager
    {
        private readonly DapperContext _dapperCtx;
        private readonly Lazy<IEmployeeAggregateReadRepository> _employeeReadRepo;
        private readonly Lazy<IEmployeeAggregateValidationRepository> _employeeValidationRepo;

        public ReadRepositoryManager(DapperContext context)
        {
            _dapperCtx = context;
            _employeeReadRepo = new Lazy<IEmployeeAggregateReadRepository>(() => new EmployeeAggregateReadRepository(_dapperCtx));
            _employeeValidationRepo = new Lazy<IEmployeeAggregateValidationRepository>(() => new EmployeeAggregateValidationRepository(_dapperCtx));
        }

        public IEmployeeAggregateReadRepository EmployeeAggregate => _employeeReadRepo.Value;

        public IEmployeeAggregateValidationRepository EmployeeValidation => _employeeValidationRepo.Value;
    }
}