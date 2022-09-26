using ReaAccountingSys.Infrastructure.Interfaces;
using ReaAccountingSys.Infrastructure.Interfaces.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.Infrastructure.Persistence.Repositories.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Repositories
{
    public class ReadRepositoryManager : IReadRepositoryManager
    {
        private readonly DapperContext _dapperCtx;
        private readonly Lazy<IEmployeeAggregateReadRepository> _employeeRepo;

        public ReadRepositoryManager(DapperContext context)
        {
            _dapperCtx = context;
            _employeeRepo = new Lazy<IEmployeeAggregateReadRepository>(() => new EmployeeAggregateReadRepository(_dapperCtx));
        }

        public IEmployeeAggregateReadRepository EmployeeAggregate => _employeeRepo.Value;
    }
}