using SharedKernel.Interfaces;
using SharedKernel.Utilities;

namespace SharedKernel
{
    public abstract class BusinessRule<T> : IBusinessRule<T>
    {
        protected IBusinessRule<T>? Next { get; private set; }

        public void SetNext(IBusinessRule<T> next)
        {
            Next = next;
        }

        public virtual async Task<ValidationResult> Validate(T request)
        {
            ValidationResult validationResult = new();

            if (Next is not null)
            {
                await Next.Validate(request);
            }

            return validationResult;
        }
    }
}