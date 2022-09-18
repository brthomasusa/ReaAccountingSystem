using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.Core.Interfaces
{
    public interface IAggregateRootRepository<TAggregateRootEntity>
    {
        Task<OperationResult<bool>> AddAsync(TAggregateRootEntity entity);
        OperationResult<bool> Update(TAggregateRootEntity entity);
        OperationResult<bool> Delete(TAggregateRootEntity entity);
    }
}