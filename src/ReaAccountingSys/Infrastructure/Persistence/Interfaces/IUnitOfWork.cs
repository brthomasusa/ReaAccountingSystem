namespace ReaAccountingSys.Infrastructure.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}