using System.Linq.Expressions;

using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Infrastructure.Persistence.Interfaces
{
    public interface IAggregateRootRepository<TAggregateRootEntity>
    {
        Task<OperationResult<TAggregateRootEntity>> GetByIdAsync(Guid id);
        Task<OperationResult<TAggregateRootEntity>> GetByConditionAsync(Expression<Func<TAggregateRootEntity, bool>> expression, bool trackChanges);
        Task<OperationResult<bool>> Exists(Guid id);
        Task<OperationResult<bool>> AddAsync(TAggregateRootEntity entity);
        OperationResult<bool> Update(TAggregateRootEntity entity);
        OperationResult<bool> Delete(TAggregateRootEntity entity);
    }
}