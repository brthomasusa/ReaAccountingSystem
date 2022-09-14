using ReaAccountingSys.SharedKernel.Utilities;

namespace ReaAccountingSys.SharedKernel.Interfaces
{
    public interface IBusinessRule<T>
    {
        void SetNext(IBusinessRule<T> next);

        Task<ValidationResult> Validate(T request);
    }
}