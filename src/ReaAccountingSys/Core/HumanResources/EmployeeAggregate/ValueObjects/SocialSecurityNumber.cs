using System.Text.RegularExpressions;
using ReaAccountingSys.SharedKernel;

namespace ReaAccountingSys.Core.HumanResources.EmployeeAggregate.ValueObjects
{
    public class SocialSecurityNumber : ValueObject
    {
        public string? Value { get; }

        protected SocialSecurityNumber() { }

        private SocialSecurityNumber(string ssn)
            : this()
        {
            Value = ssn;
        }

        public static implicit operator string(SocialSecurityNumber self) => self.Value!;

        public static SocialSecurityNumber Create(string ssn)
        {
            CheckValidity(ssn);
            return new SocialSecurityNumber(ssn);
        }

        private static void CheckValidity(string value)
        {
            if (!Regex.IsMatch(value, @"^(?!219099999|078051120)(?!666|000|9\d{2})\d{3}(?!00)\d{2}(?!0{4})\d{4}$"))
            {
                throw new ArgumentException("Invalid social security number!", nameof(value));
            }
        }
    }
}