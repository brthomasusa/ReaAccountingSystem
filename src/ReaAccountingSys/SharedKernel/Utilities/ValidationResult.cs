using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReaAccountingSys.SharedKernel.Utilities
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Messages { get; set; } = new();
    }
}