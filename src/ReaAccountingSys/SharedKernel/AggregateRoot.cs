using ReaAccountingSys.SharedKernel.Interfaces;

namespace ReaAccountingSys.SharedKernel
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot
    {

    }
}
