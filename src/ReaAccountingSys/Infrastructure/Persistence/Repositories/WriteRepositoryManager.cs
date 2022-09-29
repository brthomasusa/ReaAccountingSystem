using ReaAccountingSys.Infrastructure.Persistence.DatabaseContext;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces;
using ReaAccountingSys.Infrastructure.Persistence.Interfaces.HumanResources;
using ReaAccountingSys.Infrastructure.Persistence.Repositories.HumanResources;

namespace ReaAccountingSys.Infrastructure.Persistence.Repositories
{
    public class WriteRepositoryManager : IWriteRepositoryManager
    {
        private readonly AppDbContext _efcoreCtx;
        private readonly Lazy<IEmployeeAggregateWriteRepository> _employeeWriteRepo;

        public WriteRepositoryManager(AppDbContext ctx)
        {
            _efcoreCtx = ctx;
            _employeeWriteRepo = new Lazy<IEmployeeAggregateWriteRepository>(() => new EmployeeAggregateWriteRepository(_efcoreCtx));
        }

        public IEmployeeAggregateWriteRepository EmployeeAggregate => _employeeWriteRepo.Value;
    }
}