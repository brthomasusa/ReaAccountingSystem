using ReaAccountingSys.SharedKernel;

namespace ReaAccountingSys.Core.HumanResources.EmployeeAggregate.ValueObjects
{
    public class TaxExemption : ValueObject
    {
        public int Value { get; }

        protected TaxExemption() { }

        private TaxExemption(int value) : this() => Value = value;

        public static implicit operator int(TaxExemption self) => self.Value;

        public static TaxExemption Create(int exemptions)
        {
            CheckValidity(exemptions);
            return new TaxExemption(exemptions);
        }

        private static void CheckValidity(int value)
        {
            if (value < 0 || value > 11)
            {
                throw new ArgumentException("Number of exemptions must be between zero and eleven.", nameof(value));
            }
        }
    }
}